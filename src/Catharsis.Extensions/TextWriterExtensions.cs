using System.Text;
using System.Xml;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="TextWriter"/>
public static class TextWriterExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static TextWriter AsSynchronized(this TextWriter writer) => writer is not null ? TextWriter.Synchronized(writer) : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static XmlWriter ToXmlWriter(this TextWriter writer, bool close = true) => writer is not null ? XmlWriter.Create(writer, new XmlWriterSettings { CloseOutput = close, Indent = true }) : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static XmlDictionaryWriter ToXmlDictionaryWriter(this TextWriter writer, bool close = true) => writer.ToXmlWriter(close).ToXmlDictionaryWriter();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytesAsync{TWriter}(TWriter, IEnumerable{byte}, Encoding, CancellationToken)"/>
  public static TWriter WriteBytes<TWriter>(this TWriter destination, IEnumerable<byte> bytes, Encoding encoding = null) where TWriter : TextWriter
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return destination.WriteText(bytes.AsArray().ToText(encoding));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytes{TWriter}(TWriter, IEnumerable{byte}, Encoding)"/>
  public static async Task<TWriter> WriteBytesAsync<TWriter>(this TWriter destination, IEnumerable<byte> bytes, Encoding encoding = null, CancellationToken cancellation = default) where TWriter : TextWriter
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return await destination.WriteTextAsync(bytes.AsArray().ToText(encoding), cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTextAsync{TWriter}(TWriter, string, CancellationToken)"/>
  public static TWriter WriteText<TWriter>(this TWriter destination, string text) where TWriter : TextWriter
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    destination.Write(text);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteText{TWriter}(TWriter, string)"/>
  public static async Task<TWriter> WriteTextAsync<TWriter>(this TWriter destination, string text, CancellationToken cancellation = default) where TWriter : TextWriter
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteAsync(text.ToReadOnlyMemory(), cancellation).ConfigureAwait(false);

    return destination;
  }
}