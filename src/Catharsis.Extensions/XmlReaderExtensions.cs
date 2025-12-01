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
  /// <param name="reader"></param>
  extension(XmlReader reader)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public byte[] Bytes => reader.ToBytes();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Text => reader.ToText();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public XmlReader Skip(int count)
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
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public T DeserializeAsDataContract<T>(params Type[] types)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      var serializer = new DataContractSerializer(typeof(T), types);

      return (T) serializer.ReadObject(reader);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public T DeserializeAsXml<T>(params Type[] types)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      var serializer = new XmlSerializer(typeof(T), types);

      return (T) serializer.Deserialize(reader);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(XmlReader, Encoding)"/>
    public byte[] ToBytes(Encoding encoding = null) => reader.ToText().ToBytes(encoding);
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(XmlReader, Encoding)"/>
    public async Task<byte[]> ToBytesAsync(Encoding encoding = null) => (await reader.ToTextAsync().ConfigureAwait(false)).ToBytes(encoding);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(XmlReader)"/>
    public string ToText() => reader?.ReadOuterXml() ?? throw new ArgumentNullException(nameof(reader));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(XmlReader)"/>
    public async Task<string> ToTextAsync() => reader is not null ? await reader.ReadOuterXmlAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public XmlDictionaryReader ToXmlDictionaryReader() => reader is not null ? XmlDictionaryReader.CreateDictionaryReader(reader) : throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para>Deserializes XML contents of stream into <see cref="XmlDocument"/> object.</para>
    /// </summary>
    /// <returns>Deserialized XML contents of source <paramref name="reader"/> as instance of <see cref="XmlDocument"/> class.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public XmlDocument ToXmlDocument()
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      var document = new XmlDocument();

      document.Load(reader);

      return document;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocumentAsync(XmlReader, CancellationToken)"/>
    public XDocument ToXDocument() => reader is not null ? XDocument.Load(reader, LoadOptions.None) : throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para>Deserialize XML contents from a <see cref="XmlReader"/> into <see cref="XDocument"/> object.</para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns><see cref="XDocument"/> instance, constructed from XML contents which have been read through a <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocument(XmlReader)"/>
    public async Task<XDocument> ToXDocumentAsync(CancellationToken cancellation = default)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      cancellation.ThrowIfCancellationRequested();

      return await XDocument.LoadAsync(reader, LoadOptions.None, cancellation).ConfigureAwait(false);
    }
  }
}