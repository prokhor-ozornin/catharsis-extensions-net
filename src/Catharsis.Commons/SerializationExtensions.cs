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
  /// <param name="offset"></param>
  /// <returns>Back reference to the current serialized object.</returns>
  /// <seealso cref="BinaryFormatter"/>
  public static T AsBinary<T>(this T instance, Stream destination, long? offset = null)
  {
    using var stream = destination;

    if (offset != null)
    {
      stream.MoveTo(offset.Value);
    }

    new BinaryFormatter().Serialize(stream, instance);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="offset"></param>
  /// <returns></returns>
  public static T AsBinary<T>(T instance, FileInfo destination, long? offset = null) => instance.AsBinary(destination.ToStream(), offset);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static byte[] AsBinary(this object instance)
  {
    using var destination = new MemoryStream();

    instance.AsBinary(destination);

    return destination.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public async static Task<T> AsBinary<T>(this IEnumerable<byte> source, CancellationToken cancellation = default) => (await source.ToMemoryStream(cancellation)).AsBinary<T>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static T AsBinary<T>(this Stream source)
  {
    using var stream = source;
    
    return (T) new BinaryFormatter().Deserialize(stream);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static T AsBinary<T>(this FileInfo source) => source.OpenRead().AsBinary<T>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> AsBinary<T>(this Uri source, TimeSpan? timeout = null, params (string Name, object? Value)[] headers) => (await source.ToStream(timeout, headers)).AsBinary<T>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T AsDataContract<T>(this T instance, XmlWriter destination, IEnumerable<Type>? types = null)
  {
    using var writer = destination;

    var serializer = new DataContractSerializer(typeof(T), new DataContractSerializerSettings { KnownTypes = types });

    serializer.WriteObject(writer, instance);

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
  public static T AsDataContract<T>(this T instance, TextWriter destination, IEnumerable<Type>? types = null) => instance.AsDataContract(destination.ToXmlDictionaryWriter(), types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T AsDataContract<T>(this T instance, Stream destination, Encoding? encoding = null, IEnumerable<Type>? types = null) => instance.AsDataContract(destination.ToXmlDictionaryWriter(encoding), types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T AsDataContract<T>(this T instance, FileInfo destination, Encoding? encoding = null, IEnumerable<Type>? types = null) => instance.AsDataContract(destination.OpenWrite(), encoding, types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static string AsDataContract(this object instance, IEnumerable<Type>? types = null)
  {
    using var destination = new StringWriter();

    instance.AsDataContract(destination, types);

    return destination.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsDataContract<T>(this XmlReader source, IEnumerable<Type>? types = null)
  {
    using var reader = source;

    var serializer = new DataContractSerializer(typeof(T), new DataContractSerializerSettings { KnownTypes = types });

    return (T?) serializer.ReadObject(reader);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsDataContract<T>(this TextReader source, IEnumerable<Type>? types = null) => source.ToXmlDictionaryReader().AsDataContract<T>(types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsDataContract<T>(this Stream source, Encoding? encoding = null, IEnumerable<Type>? types = null) => XmlDictionaryReader.CreateTextReader(source, encoding, XmlDictionaryReaderQuotas.Max, null).AsDataContract<T>(types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsDataContract<T>(this FileInfo source, Encoding? encoding = null, IEnumerable<Type>? types = null) => source.OpenRead().AsDataContract<T>(encoding, types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T?> AsDataContract<T>(this Uri source, IEnumerable<Type>? types = null, TimeSpan? timeout = null, params (string Name, object? Value)[] headers) => (await source.ToStream(timeout, headers)).AsDataContract<T>(null, types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsDataContract<T>(this string source, IEnumerable<Type>? types = null) => source.ToStringReader().AsDataContract<T>(types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T AsXml<T>(this T instance, XmlWriter destination, IEnumerable<Type>? types = null)
  {
    using var writer = destination;

    var serializer = new XmlSerializer(typeof(T), types?.AsArray());

    serializer.Serialize(writer, instance);

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
  public static T AsXml<T>(this T instance, TextWriter destination, IEnumerable<Type>? types = null) => instance.AsXml(destination.ToXmlWriter(), types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T AsXml<T>(this T instance, Stream destination, Encoding? encoding = null, IEnumerable<Type>? types = null) => instance.AsXml(destination.ToXmlWriter(encoding), types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T AsXml<T>(this T instance, FileInfo destination, Encoding? encoding = null, IEnumerable<Type>? types = null) => instance.AsXml(destination.OpenRead(), encoding, types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static string AsXml(this object instance, IEnumerable<Type>? types = null)
  {
    using var destination = new StringWriter();

    instance.AsXml(destination, types);

    return destination.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsXml<T>(this XmlReader source, IEnumerable<Type>? types = null)
  {
    using var reader = source;

    var serializer = new XmlSerializer(typeof(T), types?.AsArray());

    return (T?) serializer.Deserialize(reader);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsXml<T>(this TextReader source, IEnumerable<Type>? types = null) => source.ToXmlReader().AsXml<T>(types);

  /// <summary>
  ///   <para>Deserializes XML contents of stream into object of specified type.</para>
  /// </summary>
  /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
  /// <param name="source">Stream of XML data for deserialization.</param>
  /// <param name="encoding"></param>
  /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
  /// <returns>Deserialized XML contents of source <paramref name="source"/> as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
  public static T? AsXml<T>(this Stream source, Encoding? encoding = null, IEnumerable<Type>? types = null) => source.ToXmlReader(encoding).AsXml<T>(types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsXml<T>(this FileInfo source, Encoding? encoding = null, IEnumerable<Type>? types = null) => source.OpenRead().AsXml<T>(encoding, types);

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
  public static async Task<T?> AsXml<T>(this Uri source, Encoding? encoding = null, IEnumerable<Type>? types = null, TimeSpan? timeout = null, params (string Name, object? Value)[] headers) => (await source.ToStream(timeout, headers)).AsXml<T>(encoding, types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T? AsXml<T>(this string source, IEnumerable<Type>? types = null) => source.ToStringReader().AsXml<T>(types);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, XmlWriter destination)
  {
    using var writer = destination;

    xml.Save(writer);

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
  public static XmlDocument Serialize(this XmlDocument xml, Stream destination, Encoding? encoding = null) => xml.Serialize(destination.ToXmlWriter(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument xml, FileInfo destination, Encoding? encoding = null) => xml.Serialize(destination.ToStream(), encoding);
  
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
  public static XmlDocument AsXmlDocument(this XmlReader source)
  {
    using var xml = source;

    var document = new XmlDocument();

    document.Load(xml);

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XmlDocument AsXmlDocument(this TextReader source) => source.ToXmlReader().AsXmlDocument();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument AsXmlDocument(this Stream source, Encoding? encoding = null) => source.ToXmlReader(encoding).AsXmlDocument();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument AsXmlDocument(this FileInfo source, Encoding? encoding = null) => source.ToXmlReader(encoding).AsXmlDocument();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlDocument> AsXmlDocument(this Uri source, Encoding? encoding = null, TimeSpan? timeout = null, params (string Name, object? Value)[] headers) => (await source.ToXmlReader(encoding, timeout, headers)).AsXmlDocument();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static XmlDocument AsXmlDocument(this string source) => source.ToStringReader().AsXmlDocument();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, XmlWriter destination)
  {
    using var writer = destination;

    xml.Save(writer);

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
  public static XDocument Serialize(this XDocument xml, Stream destination, Encoding? encoding = null) => xml.Serialize(destination.ToXmlWriter(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="xml"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument xml, FileInfo destination, Encoding? encoding = null) => xml.Serialize(destination.ToStream(), encoding);

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
  public static async Task<XDocument> AsXDocument(this XmlReader source, CancellationToken cancellation = default)
  {
    using var reader = source;

    return await XDocument.LoadAsync(reader, LoadOptions.None, cancellation);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> AsXDocument(this TextReader source, CancellationToken cancellation = default) => await source.ToXmlReader().AsXDocument(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> AsXDocument(this Stream source, Encoding? encoding = null, CancellationToken cancellation = default) => await source.ToXmlReader(encoding).AsXDocument(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> AsXDocument(this FileInfo source, Encoding? encoding = null, CancellationToken cancellation = default) => await source.ToXmlReader(encoding).AsXDocument(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XDocument> AsXDocument(this Uri source, Encoding? encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object? Value)[] headers) => await (await source.ToXmlReader(encoding, timeout, headers)).AsXDocument(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<XDocument> AsXDocument(this string source, CancellationToken cancellation = default) => await source.ToStringReader().AsXDocument(cancellation);
}