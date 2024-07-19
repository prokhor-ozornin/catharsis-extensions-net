using System.Text;
using System.Xml;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for XML types.</para>
/// </summary>
/// <seealso cref="XmlWriter"/>
public static class XmlWriterExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytesAsync(XmlWriter, IEnumerable{byte}, Encoding)"/>
  public static XmlWriter WriteBytes(this XmlWriter writer, IEnumerable<byte> bytes, Encoding encoding = null)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return writer.WriteText(bytes.AsArray().ToText(encoding));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytes(XmlWriter, IEnumerable{byte}, Encoding)"/>
  public static async Task<XmlWriter> WriteBytesAsync(this XmlWriter writer, IEnumerable<byte> bytes, Encoding encoding = null)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return await writer.WriteTextAsync(bytes.AsArray().ToText(encoding)).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="text"></param>
  /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTextAsync(XmlWriter, string)"/>
  public static XmlWriter WriteText(this XmlWriter writer, string text)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));
    if (text is null) throw new ArgumentNullException(nameof(text));

    writer.WriteRaw(text);

    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteText(XmlWriter, string)"/>
  public static async Task<XmlWriter> WriteTextAsync(this XmlWriter writer, string text)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));
    if (text is null) throw new ArgumentNullException(nameof(text));

    await writer.WriteRawAsync(text).ConfigureAwait(false);

    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this XmlWriter writer) => writer is not null ? XmlDictionaryWriter.CreateDictionaryWriter(writer) : throw new ArgumentNullException(nameof(writer));
}