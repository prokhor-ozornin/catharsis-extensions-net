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
  /// <param name="source">Stream of XML data for deserialization.</param>
  /// <returns>Deserialized XML contents of source <paramref name="source"/> as instance of <see cref="XmlDocument"/> class.</returns>
  public static XmlDocument ToXmlDocument(this XmlReader source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    var document = new XmlDocument();

    document.Load(source);

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this TextReader source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this Stream source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);
    
    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this FileInfo source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this string source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this Uri source, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => source.ToXmlDocumentAsync(timeout, default, headers).Result; 
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlDocument> ToXmlDocumentAsync(this Uri source, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = await source.ToXmlReaderAsync(timeout, cancellation, headers).ConfigureAwait(false);

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
  /// <param name="source"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this XmlReader source) => source is not null ? XDocument.Load(source, LoadOptions.None) : throw new ArgumentNullException(nameof(source));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this TextReader source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);
    
    return reader.ToXDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this Stream source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);
    
    return reader.ToXDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this FileInfo source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return reader.ToXDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this string source) => source is not null ? XDocument.Parse(source) : throw new ArgumentNullException(nameof(source));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this Uri source, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(timeout, headers);

    return reader.ToXDocument();
  }

  /// <summary>
  ///   <para>Deserialize XML contents from a <see cref="XmlReader"/> into <see cref="XDocument"/> object.</para>
  /// </summary>
  /// <param name="source"><see cref="XmlReader"/> which is used to read XML text content from its underlying source.</param>
  /// <param name="cancellation"></param>
  /// <returns><see cref="XDocument"/> instance, constructed from XML contents which have been read through a <paramref name="source"/>.</returns>
  public static async Task<XDocument> ToXDocumentAsync(this XmlReader source, CancellationToken cancellation = default) => source is not null ? await XDocument.LoadAsync(source, LoadOptions.None, cancellation).ConfigureAwait(false) : throw new ArgumentNullException(nameof(source));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this TextReader source, CancellationToken cancellation = default)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this Stream source, CancellationToken cancellation = default)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader(false);

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this FileInfo source, CancellationToken cancellation = default)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this string source, CancellationToken cancellation = default)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToXmlReader();

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocumentAsync(this Uri source, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = await source.ToXmlReaderAsync(timeout, cancellation, headers).ConfigureAwait(false);
    
    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }
}