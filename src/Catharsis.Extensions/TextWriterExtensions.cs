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

  /// <param name="writer"></param>
  extension(TextWriter writer)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    public XmlWriter ToXmlWriter(bool close = true) => writer is not null ? XmlWriter.Create(writer, new XmlWriterSettings { CloseOutput = close, Indent = true }) : throw new ArgumentNullException(nameof(writer));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    public XmlDictionaryWriter ToXmlDictionaryWriter(bool close = true) => writer.ToXmlWriter(close).ToXmlDictionaryWriter();
  }

  /// <param name="writer"></param>
  /// <typeparam name="TWriter"></typeparam>
  extension<TWriter>(TWriter writer) where TWriter : TextWriter
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytesAsync{TWriter}(TWriter, IEnumerable{byte}, Encoding, CancellationToken)"/>
    public TWriter WriteBytes(IEnumerable<byte> bytes, Encoding encoding = null)
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      return writer.WriteText(bytes.AsArray().ToText(encoding));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="encoding"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes{TWriter}(TWriter, IEnumerable{byte}, Encoding)"/>
    public async Task<TWriter> WriteBytesAsync(IEnumerable<byte> bytes, Encoding encoding = null, CancellationToken cancellation = default)
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      return await writer.WriteTextAsync(bytes.AsArray().ToText(encoding), cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTextAsync{TWriter}(TWriter, string, CancellationToken)"/>
    public TWriter WriteText(string text)
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));
      if (text is null) throw new ArgumentNullException(nameof(text));

      writer.Write(text);

      return writer;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText{TWriter}(TWriter, string)"/>
    public async Task<TWriter> WriteTextAsync(string text, CancellationToken cancellation = default)
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));
      if (text is null) throw new ArgumentNullException(nameof(text));

      cancellation.ThrowIfCancellationRequested();

      await writer.WriteAsync(text.ToReadOnlyMemory(), cancellation).ConfigureAwait(false);

      return writer;
    }
  }
}