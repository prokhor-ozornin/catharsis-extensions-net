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
  /// <exception cref="ArgumentNullException"></exception>
  public static FileInfo Clone(this FileInfo file) => file is not null ? new FileInfo(file.ToString()) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this FileInfo file) => file is not null ? !file.Exists || file.Length == 0 : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para>Erases all content from a file, making it a zero-length one.</para>
  /// </summary>
  /// <param name="file">File to truncate.</param>
  /// <returns>Back reference to the current file.</returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
        info.Delete();
      }
    });
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static FileInfo AsReadOnly(this FileInfo file)
  {
    file.IsReadOnly = true;
    return file;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static FileStream ToStream(this FileInfo file) => file?.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static FileStream ToReadOnlyStream(this FileInfo file) => file?.Open(FileMode.Open, FileAccess.Read, FileShare.Read);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static FileStream ToWriteOnlyStream(this FileInfo file) => file?.Open(FileMode.Append, FileAccess.Write, FileShare.None) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static StreamReader ToStreamReader(this FileInfo file, Encoding encoding = null) => file is not null ? new StreamReader(file.FullName, encoding ?? Encoding.Default) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static StreamWriter ToStreamWriter(this FileInfo file, Encoding encoding = null) => file is not null ? new StreamWriter(file.FullName, true, encoding ?? Encoding.Default, 1024) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> ToBytes(this FileInfo file) => file?.ToReadOnlyStream().ToBytes(true) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///  <para>Reads entire contents of file and returns it as a byte array.</para>
  /// </summary>
  /// <param name="file">File to read data from.</param>
  /// <returns>Byte content of specified <paramref name="file"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IAsyncEnumerable<byte> ToBytesAsync(this FileInfo file) => file?.ToReadOnlyStream().ToBytesAsync(true) ?? throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlReader ToXmlReader(this FileInfo file) => file.ToReadOnlyStream().ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlDictionaryReader ToXmlDictionaryReader(this FileInfo file) => file.ToReadOnlyStream().ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlWriter ToXmlWriter(this FileInfo file, Encoding encoding = null) => file.ToWriteOnlyStream().ToXmlWriter(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this FileInfo file, Encoding encoding = null) => file.ToStream().ToXmlDictionaryWriter(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static FileInfo WriteBytes(this FileInfo destination, IEnumerable<byte> bytes)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    try
    {
      using var stream = destination.ToWriteOnlyStream();

      stream.WriteBytes(bytes);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<FileInfo> WriteBytesAsync(this FileInfo destination, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    try
    {
      await using var stream = destination.ToWriteOnlyStream();

      await stream.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static FileInfo WriteText(this FileInfo destination, string text, Encoding encoding = null)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    try
    {
      using var writer = destination.ToStreamWriter(encoding);
      writer.WriteText(text);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<FileInfo> WriteTextAsync(this FileInfo destination, string text, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    cancellation.ThrowIfCancellationRequested();

    try
    {
      await using var writer = destination.ToStreamWriter(encoding);

      await writer.WriteTextAsync(text, cancellation).ConfigureAwait(false);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="file"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  public static T DeserializeAsXml<T>(this FileInfo file, params Type[] types)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToXmlReader();

    return reader.DeserializeAsXml<T>(types);
  }
}