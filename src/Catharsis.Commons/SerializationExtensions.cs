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
    using var destination = new MemoryStream();

    instance.SerializeAsBinary(destination);

    return destination.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsBinary<T>(this IEnumerable<byte> source, CancellationToken cancellation = default)
  {
    using var stream = await source.ToMemoryStream(cancellation);
    
    return stream.DeserializeAsBinary<T>();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static T DeserializeAsBinary<T>(this Stream source) => (T) new BinaryFormatter().Deserialize(source);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static T DeserializeAsBinary<T>(this FileInfo source)
  {
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
  public static async Task<T> DeserializeAsBinary<T>(this Uri source, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    await using var stream = await source.ToStream(timeout, headers);

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
  public static T SerializeAsDataContract<T>(this T instance, TextWriter destination, params Type[] types) => instance.SerializeAsDataContract(destination.ToXmlWriter(), types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsDataContract<T>(this T instance, Stream destination, Encoding encoding = null, params Type[] types) => instance.SerializeAsDataContract(destination.ToXmlWriter(encoding), types);

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
  public static T DeserializeAsDataContract<T>(this TextReader source, params Type[] types) => source.ToXmlReader().DeserializeAsDataContract<T>(types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this Stream source, Encoding encoding = null, params Type[] types) => source.ToXmlReader(encoding).DeserializeAsDataContract<T>(types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this FileInfo source, Encoding encoding = null, params Type[] types)
  {
    using var reader = source.ToXmlReader(encoding);

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsDataContract<T>(this Uri source, Encoding encoding = null, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
  {
    using var reader = await source.ToXmlReader(encoding, timeout, headers?.AsArray());

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
    using var reader = source.ToXmlReader();

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
  public static T SerializeAsXml<T>(this T instance, TextWriter destination, params Type[] types) => instance.SerializeAsXml(destination.ToXmlWriter(), types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T SerializeAsXml<T>(this T instance, Stream destination, Encoding encoding = null, params Type[] types) => instance.SerializeAsXml(destination.ToXmlWriter(encoding), types);

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
  public static T DeserializeAsXml<T>(this TextReader source, params Type[] types) => source.ToXmlReader().DeserializeAsXml<T>(types);

  /// <summary>
  ///   <para>Deserializes XML contents of stream into object of specified type.</para>
  /// </summary>
  /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
  /// <param name="source">Stream of XML data for deserialization.</param>
  /// <param name="encoding"></param>
  /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
  /// <returns>Deserialized XML contents of source <paramref name="source"/> as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
  public static T DeserializeAsXml<T>(this Stream source, Encoding encoding = null, params Type[] types) => source.ToXmlReader(encoding).DeserializeAsXml<T>(types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this FileInfo source, Encoding encoding = null, params Type[] types)
  {
    using var reader = source.ToXmlReader(encoding);

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsXml<T>(this Uri source, Encoding encoding = null, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
  {
    using var reader = await source.ToXmlReader(encoding, timeout, headers?.AsArray());

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
    using var reader = source.ToXmlReader();

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
    xml.Save(destination);

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, TextWriter destination) => xml.Serialize(destination.ToXmlWriter());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, Stream destination, Encoding encoding = null) => xml.Serialize(destination.ToXmlWriter(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, FileInfo destination, Encoding encoding = null)
  {
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
    var document = new XmlDocument();

    document.Load(source);

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this TextReader source) => source.ToXmlReader().ToXmlDocument();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this Stream source, Encoding encoding = null) => source.ToXmlReader(encoding).ToXmlDocument();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this FileInfo source, Encoding encoding = null)
  {
    using var reader = source.ToXmlReader(encoding);

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlDocument> ToXmlDocument(this Uri source, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    using var reader = await source.ToXmlReader(encoding, timeout, headers);

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XmlDocument ToXmlDocument(this string source)
  {
    using var reader = source.ToXmlReader();

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
    xml.Save(destination);

    return xml;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, TextWriter destination) => xml.Serialize(destination.ToXmlWriter());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, Stream destination, Encoding encoding = null) => xml.Serialize(destination.ToXmlWriter(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, FileInfo destination, Encoding encoding = null)
  {
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
    using var destination = new StringWriter();

    xml.Serialize(destination);

    return destination.ToString();
  }

  /// <summary>
  ///   <para>Deserialize XML contents from a <see cref="XmlReader"/> into <see cref="XDocument"/> object.</para>
  /// </summary>
  /// <param name="source"><see cref="XmlReader"/> which is used to read XML text content from its underlying source.</param>
  /// <param name="cancellation"></param>
  /// <returns><see cref="XDocument"/> instance, constructed from XML contents which have been read through a <paramref name="source"/>.</returns>
  public static async Task<XDocument> ToXDocument(this XmlReader source, CancellationToken cancellation = default) => await XDocument.LoadAsync(source, LoadOptions.None, cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocument(this TextReader source, CancellationToken cancellation = default) => await source.ToXmlReader().ToXDocument(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocument(this Stream source, Encoding encoding = null, CancellationToken cancellation = default) => await source.ToXmlReader(encoding).ToXDocument(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocument(this FileInfo source, Encoding encoding = null, CancellationToken cancellation = default)
  {
    using var reader = source.ToXmlReader(encoding);

    return await reader.ToXDocument(cancellation);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocument(this Uri source, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    using var reader = await source.ToXmlReader(encoding, timeout, headers);
    
    return await reader.ToXDocument(cancellation);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> ToXDocument(this string source, CancellationToken cancellation = default)
  {
    using var reader = source.ToXmlReader();

    return await reader.ToXDocument(cancellation);
  }
}