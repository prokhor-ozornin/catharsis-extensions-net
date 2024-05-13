using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for XML types.</para>
/// </summary>
/// <seealso cref="XDocument"/>
public static class XDocumentExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="nodes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XDocument With(this XDocument document, IEnumerable<object> nodes)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (nodes is null) throw new ArgumentNullException(nameof(nodes));

    document.Add(nodes.AsArray());

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="nodes"></param>
  /// <returns></returns>
  public static XDocument With(this XDocument document, params object[] nodes) => document.With(nodes as IEnumerable<object>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XDocument Clone(this XDocument document) => document is not null ? document.ToString().ToStringReader().TryFinallyDispose(XDocument.Load) : throw new ArgumentNullException(nameof(document));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsUnset(this XDocument document) => document is null || document.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this XDocument document) => document?.ToEnumerable().IsEmpty() ?? throw new ArgumentNullException(nameof(document));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XDocument Empty(this XDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    document.RemoveNodes();

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XDocument TryFinallyClear(this XDocument document, Action<XDocument> action)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return document.TryFinally(action, x => x.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<XNode> ToEnumerable(this XDocument document) => document?.Nodes() ?? throw new ArgumentNullException(nameof(document));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlReader ToXmlReader(this XDocument document) => document?.CreateReader() ?? throw new ArgumentNullException(nameof(document));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlWriter ToXmlWriter(this XDocument document) => document?.CreateWriter() ?? throw new ArgumentNullException(nameof(document));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] ToBytes(this XDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    using var stream = new MemoryStream();

    document.Save(stream, SaveOptions.None);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<byte[]> ToBytesAsync(this XDocument document, CancellationToken cancellation = default)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    cancellation.ThrowIfCancellationRequested();

    using var stream = new MemoryStream();

    await document.SaveAsync(stream, SaveOptions.None, cancellation).ConfigureAwait(false);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this XDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    using var writer = new StringWriter();

    document.Save(writer, SaveOptions.None);

    return writer.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<string> ToTextAsync(this XDocument document, CancellationToken cancellation = default)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    cancellation.ThrowIfCancellationRequested();

    await using var writer = new StringWriter();

    await document.SaveAsync(writer, SaveOptions.None, cancellation).ConfigureAwait(false);

    return writer.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  public static string Serialize(this XDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    using var destination = new StringWriter();

    document.Serialize(destination);

    return destination.ToString();
  }
}