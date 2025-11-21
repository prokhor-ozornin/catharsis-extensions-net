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
  /// <param name="document"></param>
  extension(XDocument document)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty(XDocument)"/>
    public bool IsUnset => document is null || document.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="XDocument"/> instance can be considered "empty", meaning it has no child nodes.</para>
    /// </summary>
    /// <value>If the specified <paramref name="document"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public bool IsEmpty => document?.ToEnumerable().IsEmpty() ?? throw new ArgumentNullException(nameof(document));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public XDocument Empty()
    {
      if (document is null) throw new ArgumentNullException(nameof(document));

      document.RemoveNodes();

      return document;
    }

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="XDocument"/> that has the same text content as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public XDocument Clone() => document is not null ? document.ToString().ToStringReader().TryFinallyDispose(XDocument.Load) : throw new ArgumentNullException(nameof(document));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public XDocument TryFinallyClear(Action<XDocument> action)
    {
      if (document is null) throw new ArgumentNullException(nameof(document));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return document.TryFinally(action, x => x.Empty());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="nodes"/> is <see langword="null"/>.</exception>
    public XDocument With(IEnumerable<object> nodes)
    {
      if (document is null) throw new ArgumentNullException(nameof(document));
      if (nodes is null) throw new ArgumentNullException(nameof(nodes));

      document.Add(nodes.AsArray());

      return document;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public XDocument With(params object[] nodes) => document.With(nodes as IEnumerable<object>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public XDocument Serialize(XmlWriter destination)
    {
      if (document is null) throw new ArgumentNullException(nameof(document));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      document.Save(destination);

      return document;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public XDocument Serialize(TextWriter destination)
    {
      if (document is null) throw new ArgumentNullException(nameof(document));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(false);

      return document.Serialize(writer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public XDocument Serialize(Stream destination, Encoding encoding = null)
    {
      if (document is null) throw new ArgumentNullException(nameof(document));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(encoding, false);

      return document.Serialize(writer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public XDocument Serialize(FileInfo destination, Encoding encoding = null)
    {
      if (document is null) throw new ArgumentNullException(nameof(document));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(encoding);

      return document.Serialize(writer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public string Serialize()
    {
      if (document is null) throw new ArgumentNullException(nameof(document));

      using var destination = new StringWriter();

      document.Serialize(destination);

      return destination.ToString();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public byte[] ToBytes()
    {
      if (document is null) throw new ArgumentNullException(nameof(document));

      using var stream = new MemoryStream();

      document.Save(stream, SaveOptions.None);

      return stream.ToArray();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public async Task<byte[]> ToBytesAsync(CancellationToken cancellation = default)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public string ToText()
    {
      if (document is null) throw new ArgumentNullException(nameof(document));

      using var writer = new StringWriter();

      document.Save(writer, SaveOptions.None);

      return writer.ToString();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public async Task<string> ToTextAsync(CancellationToken cancellation = default)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public XmlReader ToXmlReader() => document?.CreateReader() ?? throw new ArgumentNullException(nameof(document));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public XmlWriter ToXmlWriter() => document?.CreateWriter() ?? throw new ArgumentNullException(nameof(document));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
    public IEnumerable<XNode> ToEnumerable() => document?.Nodes() ?? throw new ArgumentNullException(nameof(document));
  }
}