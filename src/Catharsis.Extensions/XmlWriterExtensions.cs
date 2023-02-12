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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this XmlWriter writer) => writer is not null ? XmlDictionaryWriter.CreateDictionaryWriter(writer) : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlWriter WriteBytes(this XmlWriter destination, IEnumerable<byte> bytes, Encoding encoding = null)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return destination.WriteText(bytes.AsArray().ToText(encoding));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<XmlWriter> WriteBytesAsync(this XmlWriter destination, IEnumerable<byte> bytes, Encoding encoding = null)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return await destination.WriteTextAsync(bytes.AsArray().ToText(encoding)).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlWriter WriteText(this XmlWriter destination, string text)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    destination.WriteRaw(text);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<XmlWriter> WriteTextAsync(this XmlWriter destination, string text)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    await destination.WriteRawAsync(text).ConfigureAwait(false);

    return destination;
  }
}