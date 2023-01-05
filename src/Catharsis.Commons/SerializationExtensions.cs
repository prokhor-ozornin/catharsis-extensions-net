using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for serialization/deserialization functionality.</para>
/// </summary>
public static class SerializationExtensions
{
  /// <summary>
  ///   <para>Serializes an object, or graph of connected objects, to the given stream.</para>
  /// </summary>
  /// <param name="instance">The object at the root of the graph to serialize.</param>
  /// <param name="destination">The stream to which the graph is to be serialized.</param>
  /// <returns>Back reference to the current serialized object.</returns>
  /// <seealso cref="BinaryFormatter"/>
  public static T SerializeAsBinary<T>(this T instance, Stream destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    new BinaryFormatter().Serialize(destination, instance);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static T SerializeAsBinary<T>(T instance, FileInfo destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var stream = destination.ToWriteOnlyStream();

    return instance.SerializeAsBinary(stream);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static byte[] SerializeAsBinary(this object instance)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    using var destination = new MemoryStream();

    instance.SerializeAsBinary(destination);

    return destination.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static T DeserializeAsBinary<T>(this IEnumerable<byte> source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var stream = source.ToMemoryStream();

    return stream.DeserializeAsBinary<T>();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static T DeserializeAsBinary<T>(this Stream source) => source is not null ? (T) new BinaryFormatter().Deserialize(source) : throw new ArgumentNullException(nameof(source));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static T DeserializeAsBinary<T>(this FileInfo source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var stream = source.ToReadOnlyStream();

    return stream.DeserializeAsBinary<T>();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static T DeserializeAsBinary<T>(this Uri source, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => source.DeserializeAsBinaryAsync<T>(timeout, default, headers).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsBinaryAsync<T>(this IEnumerable<byte> source, CancellationToken cancellation = default)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    cancellation.ThrowIfCancellationRequested();

    using var stream = await source.ToMemoryStreamAsync(cancellation).ConfigureAwait(false);

    return stream.DeserializeAsBinary<T>();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsBinaryAsync<T>(this Uri source, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    cancellation.ThrowIfCancellationRequested();

    await using var stream = await source.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false);

    return stream.DeserializeAsBinary<T>();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsDataContract<T>(this T instance, XmlWriter destination, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    var serializer = new DataContractSerializer(typeof(T), types);

    serializer.WriteObject(destination, instance);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsDataContract<T>(this T instance, TextWriter destination, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(false);
   
    return instance.SerializeAsDataContract(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsDataContract<T>(this T instance, Stream destination, Encoding encoding = null, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding, false);
    
    return instance.SerializeAsDataContract(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsDataContract<T>(this T instance, FileInfo destination, Encoding encoding = null, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding);

    return instance.SerializeAsDataContract(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static string SerializeAsDataContract(this object instance, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    using var destination = new StringWriter();

    instance.SerializeAsDataContract(destination, types);

    return destination.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this XmlReader source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    var serializer = new DataContractSerializer(typeof(T), types);

    return (T) serializer.ReadObject(source);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this TextReader source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this Stream source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this FileInfo source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this string source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this Uri source, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types) => source.DeserializeAsDataContractAsync<T>(timeout, default, headers, types).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsDataContractAsync<T>(this Uri source, TimeSpan? timeout = null, CancellationToken cancellation = default, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    cancellation.ThrowIfCancellationRequested();

    using var reader = await source.ToXmlReaderAsync(timeout, cancellation, headers?.AsArray()).ConfigureAwait(false);

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsXml<T>(this T instance, XmlWriter destination, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    var serializer = new XmlSerializer(typeof(T), types);

    serializer.Serialize(destination, instance);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsXml<T>(this T instance, TextWriter destination, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(false);

    return instance.SerializeAsXml(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsXml<T>(this T instance, Stream destination, Encoding encoding = null, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding, false);
    
    return instance.SerializeAsXml(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsXml<T>(this T instance, FileInfo destination, Encoding encoding = null, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding);

    return instance.SerializeAsXml(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static string SerializeAsXml(this object instance, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    using var destination = new StringWriter();

    instance.SerializeAsXml(destination, types);

    return destination.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this XmlReader source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    var serializer = new XmlSerializer(typeof(T), types);

    return (T) serializer.Deserialize(source);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this TextReader source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para>Deserializes XML contents of stream into object of specified type.</para>
  /// </summary>
  /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
  /// <param name="source">Stream of XML data for deserialization.</param>
  /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
  /// <returns>Deserialized XML contents of source <paramref name="source"/> as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
  public static T DeserializeAsXml<T>(this Stream source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this FileInfo source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this string source, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this Uri source, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types) => source.DeserializeAsXmlAsync<T>(timeout, default, headers, types).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsXmlAsync<T>(this Uri source, TimeSpan? timeout = null, CancellationToken cancellation = default, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    cancellation.ThrowIfCancellationRequested();

    using var reader = await source.ToXmlReaderAsync(timeout, cancellation, headers?.AsArray()).ConfigureAwait(false);

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, XmlWriter destination)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    xml.Save(destination);

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, TextWriter destination)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(false);

    return xml.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, Stream destination, Encoding encoding = null)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding, false);

    return xml.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, FileInfo destination, Encoding encoding = null)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding);
    
    return xml.Serialize(writer);
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static string Serialize(this XmlDocument xml)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    using var destination = new StringWriter();

    xml.Serialize(destination);

    return destination.ToString();
  }

  /// <summary>
  ///   <para>Deserializes XML contents of stream into <see cref="XmlDocument"/> object.</para>
  /// </summary>
  /// <param name="reader">Stream of XML data for deserialization.</param>
  /// <returns>Deserialized XML contents of source <paramref name="reader"/> as instance of <see cref="XmlDocument"/> class.</returns>
  public static XmlDocument ToXmlDocument(this XmlReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    var document = new XmlDocument();

    document.Load(reader);

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    using var xmlReader = reader.ToXmlReader(false);

    return xmlReader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this Stream stream)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var reader = stream.ToXmlReader(false);
    
    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this FileInfo file)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToXmlReader();

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var reader = text.ToXmlReader();

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri.ToXmlDocumentAsync(timeout, default, headers).Result; 
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlDocument> ToXmlDocumentAsync(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    using var reader = await uri.ToXmlReaderAsync(timeout, cancellation, headers).ConfigureAwait(false);

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, XmlWriter destination)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    xml.Save(destination);

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, TextWriter destination)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(false);

    return xml.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, Stream destination, Encoding encoding = null)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding, false);

    return xml.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, FileInfo destination, Encoding encoding = null)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding);

    return xml.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <returns></returns>
  public static string Serialize(this XDocument xml)
  {
    if (xml is null) throw new ArgumentNullException(nameof(xml));

    using var destination = new StringWriter();

    xml.Serialize(destination);

    return destination.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this XmlReader reader) => reader is not null ? XDocument.Load(reader, LoadOptions.None) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    using var xmlReader = reader.ToXmlReader(false);
    
    return xmlReader.ToXDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this Stream stream)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var reader = stream.ToXmlReader(false);
    
    return reader.ToXDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this FileInfo file)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToXmlReader();

    return reader.ToXDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this string text) => text is not null ? XDocument.Parse(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var reader = uri.ToXmlReader(timeout, headers);

    return reader.ToXDocument();
  }

  /// <summary>
  ///   <para>Deserialize XML contents from a <see cref="XmlReader"/> into <see cref="XDocument"/> object.</para>
  /// </summary>
  /// <param name="reader"><see cref="XmlReader"/> which is used to read XML text content from its underlying source.</param>
  /// <param name="cancellation"></param>
  /// <returns><see cref="XDocument"/> instance, constructed from XML contents which have been read through a <paramref name="reader"/>.</returns>
  public static async Task<XDocument> ToXDocumentAsync(this XmlReader reader, CancellationToken cancellation = default)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    cancellation.ThrowIfCancellationRequested();

    return await XDocument.LoadAsync(reader, LoadOptions.None, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this TextReader reader, CancellationToken cancellation = default)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    cancellation.ThrowIfCancellationRequested();

    using var xmlReader = reader.ToXmlReader(false);

    return await xmlReader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="stream"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this Stream stream, CancellationToken cancellation = default)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    cancellation.ThrowIfCancellationRequested();

    using var reader = stream.ToXmlReader(false);

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
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
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this string text, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    cancellation.ThrowIfCancellationRequested();

    using var reader = text.ToXmlReader();

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    using var reader = await uri.ToXmlReaderAsync(timeout, cancellation, headers).ConfigureAwait(false);
    
    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }
}