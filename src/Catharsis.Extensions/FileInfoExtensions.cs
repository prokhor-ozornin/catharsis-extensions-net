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
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(FileInfo)"/>
  public static bool IsUnset(this FileInfo file) => file is null || file.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="FileInfo"/> instance can be considered "empty", meaning it either doesn't exist or its size is zero.</para>
  /// </summary>
  /// <param name="file">File instance for evaluation.</param>
  /// <returns>If the specified <paramref name="file"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(FileInfo)"/>
  public static bool IsEmpty(this FileInfo file) => file is not null ? !file.Exists || file.Length == 0 : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="directory"/> is <see langword="null"/>.</exception>
  public static bool InDirectory(this FileInfo file, DirectoryInfo directory)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));
    if (directory is null) throw new ArgumentNullException(nameof(directory));

    return directory.Files(null, true).Contains(file);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static FileInfo CreateWithPath(this FileInfo file)
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
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="LinesAsync(FileInfo, Encoding)"/>
  public static string[] Lines(this FileInfo file, Encoding encoding = null)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToStreamReader(encoding);

    return reader.Lines().AsArray();
  }

  /// <summary>
  ///   <para>Reads text content of a file and returns it as a list of strings, using default system-dependent string separator.</para>
  /// </summary>
  /// <param name="file">File to read text from.</param>
  /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
  /// <returns>List of strings which have been read from a <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Lines(FileInfo, Encoding)"/>
  public static async IAsyncEnumerable<string> LinesAsync(this FileInfo file, Encoding encoding = null)
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
  /// <param name="file"></param>
  /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static FileInfo AsReadOnly(this FileInfo file)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    file.IsReadOnly = true;

    return file;
  }

  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="FileInfo"/> that will point to the same file as the original.</para>
  /// </summary>
  /// <param name="file">File instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static FileInfo Clone(this FileInfo file) => file is not null ? new FileInfo(file.ToString()) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para>Erases all content from a file, making it a zero-length one.</para>
  /// </summary>
  /// <param name="file">File to be cleared.</param>
  /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static FileInfo Empty(this FileInfo file)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var stream = file.Create();

    file.Refresh();

    return file;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="TryFinallyDelete(FileInfo, Action{FileInfo})"/>
  public static FileInfo TryFinallyClear(this FileInfo file, Action<FileInfo> action)
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
  /// <param name="file"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="TryFinallyClear(FileInfo, Action{FileInfo})"/>
  public static FileInfo TryFinallyDelete(this FileInfo file, Action<FileInfo> action)
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
  /// <param name="file"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static T DeserializeAsDataContract<T>(this FileInfo file, params Type[] types)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToXmlReader();

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="file"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static T DeserializeAsXml<T>(this FileInfo file, params Type[] types)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToXmlReader();

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="bytes"></param>
  /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytesAsync(FileInfo, IEnumerable{byte}, CancellationToken)"/>
  public static FileInfo WriteBytes(this FileInfo file, IEnumerable<byte> bytes)
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
  /// <param name="file"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytes(FileInfo, IEnumerable{byte})"/>
  public static async Task<FileInfo> WriteBytesAsync(this FileInfo file, IEnumerable<byte> bytes, CancellationToken cancellation = default)
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
  /// <param name="file"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns>Back self-reference to the given <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTextAsync(FileInfo, string, Encoding, CancellationToken)"/>
  public static FileInfo WriteText(this FileInfo file, string text, Encoding encoding = null)
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
  /// <param name="file"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteText(FileInfo, string, Encoding)"/>
  public static async Task<FileInfo> WriteTextAsync(this FileInfo file, string text, Encoding encoding = null, CancellationToken cancellation = default)
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
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytesAsync(FileInfo)"/>
  public static IEnumerable<byte> ToBytes(this FileInfo file) => file?.ToReadOnlyStream().ToBytes(true) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///  <para>Reads entire contents of file and returns it as a byte array.</para>
  /// </summary>
  /// <param name="file">File to read data from.</param>
  /// <returns>Byte content of specified <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytes(FileInfo)"/>
  public static IAsyncEnumerable<byte> ToBytesAsync(this FileInfo file) => file?.ToReadOnlyStream().ToBytesAsync(true) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTextAsync(FileInfo, Encoding)"/>
  public static string ToText(this FileInfo file, Encoding encoding = null)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToStreamReader(encoding);

    return reader.ToText();
  }

  /// <summary>
  ///   <para>Reads text content of a file and returns it as a string.</para>
  /// </summary>
  /// <param name="file">File to read text from.</param>
  /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
  /// <returns>Text contents of a <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(FileInfo, Encoding)"/>
  public static async Task<string> ToTextAsync(this FileInfo file, Encoding encoding = null)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToStreamReader(encoding);

    return await reader.ToTextAsync().ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static FileStream ToStream(this FileInfo file) => file?.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static FileStream ToReadOnlyStream(this FileInfo file) => file?.Open(FileMode.Open, FileAccess.Read, FileShare.Read) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static FileStream ToWriteOnlyStream(this FileInfo file) => file?.Open(FileMode.Append, FileAccess.Write, FileShare.None) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static StreamReader ToStreamReader(this FileInfo file, Encoding encoding = null) => file is not null ? new StreamReader(file.FullName, encoding ?? Encoding.Default) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static StreamWriter ToStreamWriter(this FileInfo file, Encoding encoding = null) => file is not null ? new StreamWriter(file.FullName, true, encoding ?? Encoding.Default, 1024) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static XmlReader ToXmlReader(this FileInfo file) => file.ToReadOnlyStream().ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static XmlDictionaryReader ToXmlDictionaryReader(this FileInfo file) => file.ToReadOnlyStream().ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static XmlWriter ToXmlWriter(this FileInfo file, Encoding encoding = null) => file.ToWriteOnlyStream().ToXmlWriter(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this FileInfo file, Encoding encoding = null) => file.ToStream().ToXmlDictionaryWriter(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  public static XmlDocument ToXmlDocument(this FileInfo file)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToXmlReader();

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToXDocumentAsync(FileInfo, CancellationToken)"/>
  public static XDocument ToXDocument(this FileInfo file)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToXmlReader();

    return reader.ToXDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToXDocument(FileInfo)"/>
  public static async Task<XDocument> ToXDocumentAsync(this FileInfo file, CancellationToken cancellation = default)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    cancellation.ThrowIfCancellationRequested();

    using var reader = file.ToXmlReader();

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static bool ToBoolean(this FileInfo file) => file is not null && file.Exists;
}