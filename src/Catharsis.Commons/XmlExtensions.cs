using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for XML types.</para>
/// </summary>
/// <seealso cref="XmlReader"/>
/// <seealso cref="XmlWriter"/>
/// <seealso cref="XmlDocument"/>
/// <seealso cref="XDocument"/>
public static class XmlExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static bool IsEmpty(this XmlDocument xml) => xml.ToEnumerable().IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static bool IsEmpty(this XDocument xml) => xml.ToEnumerable().IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static XmlDocument Empty(this XmlDocument xml)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    xml.RemoveAll();

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static XDocument Empty(this XDocument xml)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    xml.RemoveNodes();

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static XmlReader Skip(this XmlReader reader, int count)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    count.Times(() => reader.Read());

    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static T Print<T>(this T instance, XmlWriter destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(instance.ToStateString());

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static async Task<T> PrintAsync<T>(this T instance, XmlWriter destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteTextAsync(instance.ToStateString()).ConfigureAwait(false);
    
    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static XmlDocument TryFinallyClear(this XmlDocument xml, Action<XmlDocument> action)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return xml.TryFinally(action, xml => xml.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static XDocument TryFinallyClear(this XDocument xml, Action<XDocument> action)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return xml.TryFinally(action, xml => xml.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static IEnumerable<XmlNode> ToEnumerable(this XmlDocument xml) => xml is not null ? xml.ChildNodes.Cast<XmlNode>() : throw new ArgumentNullException(nameof(xml));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static IEnumerable<XNode> ToEnumerable(this XDocument xml) => xml is not null ? xml.Nodes() : throw new ArgumentNullException(nameof(xml));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this TextReader reader, bool close = true) => reader is not null ? XmlReader.Create(reader, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true }) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this Stream stream, bool close = true) => stream is not null ? XmlReader.Create(stream, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true }) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this FileInfo file) => file.ToReadOnlyStream().ToXmlReader();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri.ToStream(timeout, headers).ToXmlReader();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this string text) => text.ToStringReader().ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this XDocument xml) => xml is not null ? xml.CreateReader() : throw new ArgumentNullException(nameof(xml));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlReader> ToXmlReaderAsync(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false)).ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static XmlWriter ToXmlWriter(this TextWriter writer, bool close = true) => writer is not null ? XmlWriter.Create(writer, new XmlWriterSettings { CloseOutput = close, Indent = true }) : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para>Returns a <see cref="XmlWriter"/> for writing XML data to specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Target stream to write to.</param>
  /// <param name="encoding">Text encoding to use by <see cref="XmlWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <param name="close"></param>
  /// <returns>XML writer instance that wraps <see cref="stream"/> stream.</returns>
  public static XmlWriter ToXmlWriter(this Stream stream, Encoding encoding = null, bool close = true) => stream is not null ? XmlWriter.Create(stream, new XmlWriterSettings { CloseOutput = close, Indent = true, Encoding = encoding ?? Encoding.Default }) : throw new ArgumentNullException(nameof(stream));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlWriter ToXmlWriter(this FileInfo file, Encoding encoding = null) => file.ToWriteOnlyStream().ToXmlWriter(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static XmlWriter ToXmlWriter(this XDocument xml) => xml is not null ? xml.CreateWriter() : throw new ArgumentNullException(nameof(xml));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this XmlReader reader) => reader is not null ? XmlDictionaryReader.CreateDictionaryReader(reader) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this TextReader reader, bool close = true) => reader.ToXmlReader(close).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this Stream stream, bool close = true) => stream.ToXmlReader(close).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this FileInfo file) => file.ToReadOnlyStream().ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this string text) => text.ToStringReader().ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri.ToStream(timeout, headers).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlDictionaryReader> ToXmlDictionaryReaderAsync(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false)).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this XmlWriter writer) => writer is not null ? XmlDictionaryWriter.CreateDictionaryWriter(writer) : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this TextWriter writer, bool close = true) => writer.ToXmlWriter(close).ToXmlDictionaryWriter();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="encoding"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this Stream stream, Encoding encoding = null, bool close = true) => stream.ToXmlWriter(encoding, close).ToXmlDictionaryWriter();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this FileInfo file, Encoding encoding = null) => file.ToStream().ToXmlDictionaryWriter(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this XmlReader reader, Encoding encoding = null) => reader.ToText().ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this XmlDocument xml)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    using var stream = new MemoryStream();

    xml.Save(stream);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this XDocument xml)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    using var stream = new MemoryStream();

    xml.Save(stream, SaveOptions.None);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<byte[]> ToBytesAsync(this XmlReader reader, Encoding encoding = null) => (await reader.ToTextAsync().ConfigureAwait(false)).ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> ToBytesAsync(this XDocument xml, CancellationToken cancellation = default)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    cancellation.ThrowIfCancellationRequested();

    using var stream = new MemoryStream();

    await xml.SaveAsync(stream, SaveOptions.None, cancellation).ConfigureAwait(false);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static string ToText(this XmlReader reader) => reader is not null ? reader.ReadOuterXml() : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static string ToText(this XmlDocument xml)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    using var writer = new StringWriter();

    xml.Save(writer);

    return writer.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static string ToText(this XDocument xml)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    using var writer = new StringWriter();

    xml.Save(writer, SaveOptions.None);

    return writer.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static async Task<string> ToTextAsync(this XmlReader reader) => reader is not null ? await reader.ReadOuterXmlAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(reader));
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToTextAsync(this XDocument xml, CancellationToken cancellation = default)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    cancellation.ThrowIfCancellationRequested();

    await using var writer = new StringWriter();

    await xml.SaveAsync(writer, SaveOptions.None, cancellation).ConfigureAwait(false);

    return writer.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlWriter WriteBytes(this XmlWriter destination, IEnumerable<byte> bytes, Encoding encoding = null) => destination.WriteText(bytes.AsArray().ToText(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static XmlWriter WriteText(this XmlWriter destination, string text)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    destination.WriteRaw(text);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<XmlWriter> WriteBytesAsync(this XmlWriter destination, IEnumerable<byte> bytes, Encoding encoding = null) => await destination.WriteTextAsync(bytes.AsArray().ToText(encoding)).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static async Task<XmlWriter> WriteTextAsync(this XmlWriter destination, string text)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    await destination.WriteRawAsync(text).ConfigureAwait(false);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, XmlWriter destination, Encoding encoding = null)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(bytes, encoding);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, XmlWriter destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, XmlWriter destination, Encoding encoding = null)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteBytesAsync(bytes, encoding).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static async Task<string> WriteToAsync(this string text, XmlWriter destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteTextAsync(text).ConfigureAwait(false);

    return text;
  }
}