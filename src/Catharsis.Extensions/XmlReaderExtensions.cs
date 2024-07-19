using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for XML types.</para>
/// </summary>
/// <seealso cref="XmlReader"/>
public static class XmlReaderExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
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
  /// <param name="reader"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static T DeserializeAsXml<T>(this XmlReader reader, params Type[] types)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    var serializer = new XmlSerializer(typeof(T), types);

    return (T) serializer.Deserialize(reader);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytesAsync(XmlReader, Encoding)"/>
  public static byte[] ToBytes(this XmlReader reader, Encoding encoding = null) => reader.ToText().ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytes(XmlReader, Encoding)"/>
  public static async Task<byte[]> ToBytesAsync(this XmlReader reader, Encoding encoding = null) => (await reader.ToTextAsync().ConfigureAwait(false)).ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTextAsync(XmlReader)"/>
  public static string ToText(this XmlReader reader) => reader?.ReadOuterXml() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(XmlReader)"/>
  public static async Task<string> ToTextAsync(this XmlReader reader) => reader is not null ? await reader.ReadOuterXmlAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static XmlDictionaryReader ToXmlDictionaryReader(this XmlReader reader) => reader is not null ? XmlDictionaryReader.CreateDictionaryReader(reader) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para>Deserializes XML contents of stream into <see cref="XmlDocument"/> object.</para>
  /// </summary>
  /// <param name="reader">Stream of XML data for deserialization.</param>
  /// <returns>Deserialized XML contents of source <paramref name="reader"/> as instance of <see cref="XmlDocument"/> class.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToXDocumentAsync(XmlReader, CancellationToken)"/>
  public static XDocument ToXDocument(this XmlReader reader) => reader is not null ? XDocument.Load(reader, LoadOptions.None) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para>Deserialize XML contents from a <see cref="XmlReader"/> into <see cref="XDocument"/> object.</para>
  /// </summary>
  /// <param name="reader"><see cref="XmlReader"/> which is used to read XML text content from its underlying source.</param>
  /// <param name="cancellation"></param>
  /// <returns><see cref="XDocument"/> instance, constructed from XML contents which have been read through a <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToXDocument(XmlReader)"/>
  public static async Task<XDocument> ToXDocumentAsync(this XmlReader reader, CancellationToken cancellation = default)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    cancellation.ThrowIfCancellationRequested();

    return await XDocument.LoadAsync(reader, LoadOptions.None, cancellation).ConfigureAwait(false);
  }

}