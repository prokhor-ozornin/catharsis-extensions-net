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
  /// <param name="xml"></param>
  /// <returns></returns>
  public static byte[] Bytes(this XmlDocument xml)
  {
    using var stream = new MemoryStream();
    
    xml.Save(stream);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XmlDocument> Bytes(this XmlDocument xml, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    using var stream = await bytes.ToMemoryStream(cancellation);

    xml.Load(stream);

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<byte[]> Bytes(this XDocument xml, CancellationToken cancellation = default)
  {
    using var stream = new MemoryStream();

    await xml.SaveAsync(stream, SaveOptions.None, cancellation);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> Bytes(this XDocument xml, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    using var stream = await bytes.ToMemoryStream(cancellation);

    await XDocument.LoadAsync(stream, LoadOptions.None, cancellation);

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static string Text(this XmlDocument xml)
  {
    using var writer = new StringWriter();

    xml.Save(writer);

    return writer.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static XmlDocument Text(this XmlDocument xml, string text)
  {
    xml.LoadXml(text);

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> Text(this XDocument xml, CancellationToken cancellation = default)
  {
    await using var writer = new StringWriter();

    await xml.SaveAsync(writer, SaveOptions.None, cancellation);

    return writer.ToString();
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> Text(this XDocument xml, string text, CancellationToken cancellation = default)
  {
    using var reader = text.ToStringReader();

    await XDocument.LoadAsync(reader, LoadOptions.None, cancellation);

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static async Task<string> Text(this XmlReader reader) => await reader.ReadOuterXmlAsync();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static async Task<XmlWriter> Text(this XmlWriter writer, string text)
  {
    await writer.WriteRawAsync(text);

    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static XmlDocument UseTemporarily(this XmlDocument xml, Action<XmlDocument> action) => xml.UseFinally(action, xml => xml.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static XDocument UseTemporarily(this XDocument xml, Action<XDocument> action) => xml.UseFinally(action, xml => xml.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static async Task<XmlWriter> Print(this XmlWriter destination, object instance)
  {
    await destination.Text(instance.ToStringState());
    return destination;
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
  ///   <para>Creates <see cref="AsXmlReader"/> that wraps specified <see cref="TextReader"/> instance.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> that wraps XML data source.</param>
  /// <returns><see cref="AsXmlReader"/> instance that wraps a text <paramref name="reader"/>.</returns>
  public static XmlReader ToXmlReader(this TextReader reader) => XmlReader.Create(reader, new XmlReaderSettings { CloseInput = true, IgnoreComments = true, IgnoreWhitespace = true });

  /// <summary>
  ///   <para>Returns a <see cref="AsXmlReader"/> for reading XML data from specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Source stream to read from.</param>
  /// <param name="encoding"></param>
  /// <returns>XML reader instance that wraps <see cref="stream"/> stream.</returns>
  public static XmlReader ToXmlReader(this Stream stream, Encoding? encoding = null) => stream.ToStreamReader(encoding).ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlReader ToXmlReader(this FileInfo file, Encoding? encoding = null) => file.OpenRead().ToXmlReader(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlReader> ToXmlReader(this Uri uri, Encoding? encoding = null, TimeSpan? timeout = null, params (string Name, object? Value)[] headers) => (await uri.ToStream(timeout, headers)).ToXmlReader(encoding);

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
  ///   <para>Creates <see cref="XmlWriter"/> that wraps specified <see cref="AsTextWriter"/> instance.</para>
  /// </summary>
  /// <param name="writer"><see cref="AsTextWriter"/> that wraps XML data destination.</param>
  /// <returns><see cref="XmlWriter"/> instance that wraps a text <paramref name="writer"/>.</returns>
  public static XmlWriter ToXmlWriter(this TextWriter writer) => XmlWriter.Create(writer, new XmlWriterSettings { CloseOutput = true, Indent = true });

  /// <summary>
  ///   <para>Returns a <see cref="XmlWriter"/> for writing XML data to specified <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="stream">Target stream to write to.</param>
  /// <param name="encoding">Text encoding to use by <see cref="XmlWriter"/>. If not specified, default <see cref="Encoding.UTF8"/> will be used.</param>
  /// <returns>XML writer instance that wraps <see cref="stream"/> stream.</returns>
  public static XmlWriter ToXmlWriter(this Stream stream, Encoding? encoding = null) => stream.ToStreamWriter(encoding).ToXmlWriter();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlWriter ToXmlWriter(this FileInfo file, Encoding? encoding = null) => file.ToStream().ToXmlWriter(encoding);

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
  public static XmlDictionaryReader ToXmlDictionaryReader(this Stream stream, Encoding? encoding = null) => stream.ToXmlReader(encoding).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDictionaryReader ToXmlDictionaryReader(this FileInfo file, Encoding? encoding = null) => file.OpenRead().ToXmlDictionaryReader(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlDictionaryReader> ToXmlDictionaryReader(this Uri uri, Encoding? encoding = null, TimeSpan? timeout = null, params (string Name, object? Value)[] headers) => (await uri.ToStream(timeout, headers)).ToXmlDictionaryReader(encoding);

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
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this Stream stream, Encoding? encoding = null) => stream.ToXmlWriter(encoding).ToXmlDictionaryWriter();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this FileInfo file, Encoding? encoding = null) => file.ToStream().ToXmlDictionaryWriter(encoding);
}