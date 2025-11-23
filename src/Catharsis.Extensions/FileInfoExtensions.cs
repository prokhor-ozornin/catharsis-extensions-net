using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for filesystem I/O related types.</para>
/// </summary>
/// <seealso cref="FileInfo"/>
public static class FileInfoExtensions
{
  /// <param name="file"></param>
  extension(FileInfo file)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => file is null || file.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="FileInfo"/> instance can be considered "empty", meaning it either doesn't exist or its size is zero.</para>
    /// </summary>
    /// <value>If the specified <paramref name="file"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset"/>
    public bool IsEmpty => file is not null ? !file.Exists || file.Length == 0 : throw new ArgumentNullException(nameof(file));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="directory"/> is <see langword="null"/>.</exception>
    public bool InDirectory(DirectoryInfo directory)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));
      if (directory is null) throw new ArgumentNullException(nameof(directory));

      return directory.ListFiles(null, true).Contains(file);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public FileInfo CreateWithPath()
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      if (file.Exists)
      {
        return file;
      }

      if (file.DirectoryName is not null)
      {
        Directory.CreateDirectory(file.DirectoryName);
      }

      using var stream = file.Create();

      return file;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="LinesAsync(FileInfo, Encoding)"/>
    public string[] Lines(Encoding encoding = null)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var reader = file.ToStreamReader(encoding);

      return reader.Lines().AsArray();
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string[] Lines => file.Lines();

    /// <summary>
    ///   <para>Reads text content of a file and returns it as a list of strings, using default system-dependent string separator.</para>
    /// </summary>
    /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
    /// <returns>List of strings which have been read from a <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Lines(FileInfo, Encoding)"/>
    public async IAsyncEnumerable<string> LinesAsync(Encoding encoding = null)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var reader = file.ToStreamReader(encoding);

      await foreach (var line in reader.LinesAsync().ConfigureAwait(false))
      {
        yield return line;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public FileInfo AsReadOnly()
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      file.IsReadOnly = true;

      return file;
    }

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="FileInfo"/> that will point to the same file as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public FileInfo Clone() => file is not null ? new FileInfo(file.ToString()) : throw new ArgumentNullException(nameof(file));

    /// <summary>
    ///   <para>Erases all content from a file, making it a zero-length one.</para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public FileInfo Empty()
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var stream = file.Create();

      file.Refresh();

      return file;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryFinallyDelete(FileInfo, Action{FileInfo})"/>
    public FileInfo TryFinallyClear(Action<FileInfo> action)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return file.TryFinally(info =>
      {
        info.CreateWithPath();
        action(info);
      }, info => info.Empty());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryFinallyClear(FileInfo, Action{FileInfo})"/>
    public FileInfo TryFinallyDelete(Action<FileInfo> action)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return file.TryFinally(info =>
      {
        info.CreateWithPath();
        action(info);
      }, info =>
      {
        if (info.Exists)
        {
          info.IsReadOnly = false;
          info.Delete();
        }
      });
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public T DeserializeAsDataContract<T>(params Type[] types)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var reader = file.ToXmlReader();

      return reader.DeserializeAsDataContract<T>(types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public T DeserializeAsXml<T>(params Type[] types)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var reader = file.ToXmlReader();

      return reader.DeserializeAsXml<T>(types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytesAsync(FileInfo, IEnumerable{byte}, CancellationToken)"/>
    public FileInfo WriteBytes(IEnumerable<byte> bytes)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      try
      {
        using var stream = file.ToWriteOnlyStream();

        stream.WriteBytes(bytes);

        return file;
      }
      finally
      {
        file.Refresh();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes(FileInfo, IEnumerable{byte})"/>
    public async Task<FileInfo> WriteBytesAsync(IEnumerable<byte> bytes, CancellationToken cancellation = default)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      cancellation.ThrowIfCancellationRequested();

      try
      {
        await using var stream = file.ToWriteOnlyStream();

        await stream.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

        return file;
      }
      finally
      {
        file.Refresh();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTextAsync(FileInfo, string, Encoding, CancellationToken)"/>
    public FileInfo WriteText(string text, Encoding encoding = null)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));
      if (text is null) throw new ArgumentNullException(nameof(text));

      try
      {
        using var writer = file.ToStreamWriter(encoding);
        writer.WriteText(text);

        return file;
      }
      finally
      {
        file.Refresh();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText(FileInfo, string, Encoding)"/>
    public async Task<FileInfo> WriteTextAsync(string text, Encoding encoding = null, CancellationToken cancellation = default)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));
      if (text is null) throw new ArgumentNullException(nameof(text));

      cancellation.ThrowIfCancellationRequested();

      try
      {
        await using var writer = file.ToStreamWriter(encoding);

        await writer.WriteTextAsync(text, cancellation).ConfigureAwait(false);

        return file;
      }
      finally
      {
        file.Refresh();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(FileInfo)"/>
    public IEnumerable<byte> ToBytes() => file?.ToReadOnlyStream().ToBytes(true) ?? throw new ArgumentNullException(nameof(file));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public byte[] Bytes => file.ToBytes().ToArray();

    /// <summary>
    ///  <para>Reads entire contents of file and returns it as a byte array.</para>
    /// </summary>
    /// <returns>Byte content of specified <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(FileInfo)"/>
    public IAsyncEnumerable<byte> ToBytesAsync() => file?.ToReadOnlyStream().ToBytesAsync(true) ?? throw new ArgumentNullException(nameof(file));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(FileInfo, Encoding)"/>
    public string ToText(Encoding encoding = null)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var reader = file.ToStreamReader(encoding);

      return reader.ToText();
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string Text => file.ToText();

    /// <summary>
    ///   <para>Reads text content of a file and returns it as a string.</para>
    /// </summary>
    /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
    /// <returns>Text contents of a <paramref name="file"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(FileInfo, Encoding)"/>
    public async Task<string> ToTextAsync(Encoding encoding = null)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var reader = file.ToStreamReader(encoding);

      return await reader.ToTextAsync().ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public FileStream ToStream() => file?.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None) ?? throw new ArgumentNullException(nameof(file));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public FileStream Stream => file.ToStream();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public FileStream ToReadOnlyStream() => file?.Open(FileMode.Open, FileAccess.Read, FileShare.Read) ?? throw new ArgumentNullException(nameof(file));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public FileStream ToWriteOnlyStream() => file?.Open(FileMode.Append, FileAccess.Write, FileShare.None) ?? throw new ArgumentNullException(nameof(file));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public StreamReader ToStreamReader(Encoding encoding = null) => file is not null ? new StreamReader(file.FullName, encoding ?? Encoding.Default) : throw new ArgumentNullException(nameof(file));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public StreamWriter ToStreamWriter(Encoding encoding = null) => file is not null ? new StreamWriter(file.FullName, true, encoding ?? Encoding.Default, 1024) : throw new ArgumentNullException(nameof(file));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public XmlReader ToXmlReader() => file.ToReadOnlyStream().ToXmlReader();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public XmlDictionaryReader ToXmlDictionaryReader() => file.ToReadOnlyStream().ToXmlDictionaryReader();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public XmlWriter ToXmlWriter(Encoding encoding = null) => file.ToWriteOnlyStream().ToXmlWriter(encoding);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public XmlDictionaryWriter ToXmlDictionaryWriter(Encoding encoding = null) => file.ToStream().ToXmlDictionaryWriter(encoding);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    public XmlDocument ToXmlDocument()
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var reader = file.ToXmlReader();

      return reader.ToXmlDocument();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocumentAsync(FileInfo, CancellationToken)"/>
    public XDocument ToXDocument()
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      using var reader = file.ToXmlReader();

      return reader.ToXDocument();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocument(FileInfo)"/>
    public async Task<XDocument> ToXDocumentAsync(CancellationToken cancellation = default)
    {
      if (file is null) throw new ArgumentNullException(nameof(file));

      cancellation.ThrowIfCancellationRequested();

      using var reader = file.ToXmlReader();

      return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => file is not null && file.Exists;
  }
}