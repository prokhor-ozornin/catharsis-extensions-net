using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for serialization/deserialization functionality.</para>
/// </summary>
public static class SerializationExtensions
{
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
  /// <param name="reader"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this XmlReader reader, params Type[] types)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    var serializer = new DataContractSerializer(typeof(T), types);

    return (T) serializer.ReadObject(reader);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="reader"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this TextReader reader, params Type[] types)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    using var xmlReader = reader.ToXmlReader(false);

    return xmlReader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="stream"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this Stream stream, params Type[] types)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var reader = stream.ToXmlReader(false);

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="file"></param>
  /// <param name="types"></param>
  /// <returns></returns>
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
  /// <param name="text"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this string text, params Type[] types)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var reader = text.ToXmlReader();

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsDataContract<T>(this Uri uri, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types) => uri is not null ? uri.DeserializeAsDataContractAsync<T>(timeout, headers, types).Result : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="uri"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsDataContractAsync<T>(this Uri uri, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var reader = await uri.ToXmlReaderAsync(timeout, headers?.AsArray()).ConfigureAwait(false);

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
  /// <param name="reader"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this XmlReader reader, params Type[] types)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    var serializer = new XmlSerializer(typeof(T), types);

    return (T) serializer.Deserialize(reader);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="reader"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this TextReader reader, params Type[] types)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    using var xmlReader = reader.ToXmlReader(false);

    return xmlReader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para>Deserializes XML contents of stream into object of specified type.</para>
  /// </summary>
  /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
  /// <param name="stream">Stream of XML data for deserialization.</param>
  /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
  /// <returns>Deserialized XML contents of source <paramref name="stream"/> as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
  public static T DeserializeAsXml<T>(this Stream stream, params Type[] types)
  {
    if (stream is null) throw new ArgumentNullException(nameof(stream));

    using var reader = stream.ToXmlReader(false);

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="file"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this FileInfo file, params Type[] types)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToXmlReader();

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="text"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this string text, params Type[] types)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var reader = text.ToXmlReader();

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  public static T DeserializeAsXml<T>(this Uri uri, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types) => uri is not null ? uri.DeserializeAsXmlAsync<T>(timeout, headers, types).Result : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="uri"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> DeserializeAsXmlAsync<T>(this Uri uri, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var reader = await uri.ToXmlReaderAsync(timeout, headers?.AsArray()).ConfigureAwait(false);

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument document, XmlWriter destination)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    document.Save(destination);

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument document, TextWriter destination)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(false);

    return document.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument document, Stream destination, Encoding encoding = null)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding, false);

    return document.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XmlDocument Serialize(this XmlDocument document, FileInfo destination, Encoding encoding = null)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding);
    
    return document.Serialize(writer);
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  public static string Serialize(this XmlDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    using var destination = new StringWriter();

    document.Serialize(destination);

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
  public static XmlDocument ToXmlDocument(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri is not null ? uri.ToXmlDocumentAsync(timeout, headers).Result : throw new ArgumentNullException(nameof(uri)); 
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<XmlDocument> ToXmlDocumentAsync(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var reader = await uri.ToXmlReaderAsync(timeout, headers).ConfigureAwait(false);

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument document, XmlWriter destination)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    document.Save(destination);

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument document, TextWriter destination)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(false);

    return document.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument document, Stream destination, Encoding encoding = null)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding, false);

    return document.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static XDocument Serialize(this XDocument document, FileInfo destination, Encoding encoding = null)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding);

    return document.Serialize(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  public static string Serialize(this XDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    using var destination = new StringWriter();

    document.Serialize(destination);

    return destination.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this XmlReader reader) => reader is not null ? XDocument.Load(reader, LoadOptions.None) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para>Deserialize XML contents from a <see cref="XmlReader"/> into <see cref="XDocument"/> object.</para>
  /// </summary>
  /// <param name="reader"><see cref="XmlReader"/> which is used to read XML text content from its underlying source.</param>
  /// <param name="cancellation"></param>
  /// <returns><see cref="XDocument"/> instance, constructed from XML contents which have been read through a <paramref name="reader"/>.</returns>
  public static async Task<XDocument> ToXDocumentAsync(this XmlReader reader, CancellationToken cancellation = default)
  {
    if (reader is null)
      throw new ArgumentNullException(nameof(reader));

    cancellation.ThrowIfCancellationRequested();

    return await XDocument.LoadAsync(reader, LoadOptions.None, cancellation).ConfigureAwait(false);
  }

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
  /// <returns></returns>
  public static XDocument ToXDocument(this string text) => text is not null ? XDocument.Parse(text) : throw new ArgumentNullException(nameof(text));

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
  /// <param name="headers"></param>
  /// <returns></returns>
  public static XDocument ToXDocument(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri is not null ? uri.ToXDocumentAsync(timeout, default, headers).Result : throw new ArgumentNullException(nameof(uri));

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

    using var reader = await uri.ToXmlReaderAsync(timeout, headers).ConfigureAwait(false);
    
    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }
}