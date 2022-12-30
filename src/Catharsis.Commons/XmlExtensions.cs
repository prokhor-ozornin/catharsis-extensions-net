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
    count.Times(() => reader.Read());
    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static async Task<T> Print<T>(this T instance, XmlWriter destination)
  {
    await destination.WriteText(instance.ToStateString()).ConfigureAwait(false);
    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static XmlDocument TryFinallyClear(this XmlDocument xml, Action<XmlDocument> action) => xml.TryFinally(action, xml => xml.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static XDocument TryFinallyClear(this XDocument xml, Action<XDocument> action) => xml.TryFinally(action, xml => xml.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static IEnumerable<XmlNode> ToEnumerable(this XmlDocument xml) => xml.ChildNodes.Cast<XmlNode>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static IEnumerable<XNode> ToEnumerable(this XDocument xml) => xml.Nodes();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this TextReader reader) => XmlReader.Create(reader, new XmlReaderSettings { CloseInput = true, IgnoreComments = true, IgnoreWhitespace = true });

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this Stream stream, Encoding encoding = null) => stream.ToStreamReader(encoding).ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this FileInfo file, Encoding encoding = null) => file.ToReadOnlyStream().ToXmlReader(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlReader> ToXmlReader(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => (await uri.ToStream(timeout, headers).ConfigureAwait(false)).ToXmlReader(encoding);

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
  public static XmlReader ToXmlReader(this XDocument xml) => xml.CreateReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static XmlWriter ToXmlWriter(this TextWriter writer) => XmlWriter.Create(writer, new XmlWriterSettings { CloseOutput = true, Indent = true });

  /// <summary>
  ///   <para>Returns a <see cref="XmlWriter"/> for writing XML data to specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Target stream to write to.</param>
  /// <param name="encoding">Text encoding to use by <see cref="XmlWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <returns>XML writer instance that wraps <see cref="stream"/> stream.</returns>
  public static XmlWriter ToXmlWriter(this Stream stream, Encoding encoding = null) => stream.ToStreamWriter(encoding).ToXmlWriter();

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
  public static XmlWriter ToXmlWriter(this XDocument xml) => xml.CreateWriter();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this XmlReader reader) => XmlDictionaryReader.CreateDictionaryReader(reader);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this TextReader reader) => reader.ToXmlReader().ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this Stream stream, Encoding encoding = null) => stream.ToXmlReader(encoding).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this FileInfo file, Encoding encoding = null) => file.ToReadOnlyStream().ToXmlDictionaryReader(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlDictionaryReader> ToXmlDictionaryReader(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => (await uri.ToStream(timeout, headers).ConfigureAwait(false)).ToXmlDictionaryReader(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this string text) => text.ToStringReader().ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this XmlWriter writer) => XmlDictionaryWriter.CreateDictionaryWriter(writer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this TextWriter writer) => writer.ToXmlWriter().ToXmlDictionaryWriter();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this Stream stream, Encoding encoding = null) => stream.ToXmlWriter(encoding).ToXmlDictionaryWriter();

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
  public static async Task<byte[]> ToBytes(this XmlReader reader, Encoding encoding = null) => (await reader.ToText().ConfigureAwait(false)).ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this XmlDocument xml)
  {
    using var stream = new MemoryStream();

    xml.Save(stream);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> ToBytes(this XDocument xml, CancellationToken cancellation = default)
  {
    using var stream = new MemoryStream();

    await xml.SaveAsync(stream, SaveOptions.None, cancellation).ConfigureAwait(false);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static async Task<string> ToText(this XmlReader reader) => await reader.ReadOuterXmlAsync().ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static string ToText(this XmlDocument xml)
  {
    using var writer = new StringWriter();

    xml.Save(writer);

    return writer.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> ToText(this XDocument xml, CancellationToken cancellation = default)
  {
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
  public static async Task<XmlWriter> WriteBytes(this XmlWriter destination, IEnumerable<byte> bytes, Encoding encoding = null) => await destination.WriteText(bytes.AsArray().ToText(encoding)).ConfigureAwait(false);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static async Task<XmlWriter> WriteText(this XmlWriter destination, string text)
  {
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
  public static async Task<IEnumerable<byte>> WriteTo(this IEnumerable<byte> bytes, XmlWriter destination, Encoding encoding = null)
  {
    await destination.WriteBytes(bytes, encoding).ConfigureAwait(false);
    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static async Task<string> WriteTo(this string text, XmlWriter destination)
  {
    await destination.WriteText(text).ConfigureAwait(false);
    return text;
  }
}