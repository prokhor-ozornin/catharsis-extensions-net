using System.Text;
using System.Xml;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for XML types.</para>
/// </summary>
/// <seealso cref="XmlDocument"/>
public static class XmlDocumentExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(XmlDocument)"/>
  public static bool IsUnset(this XmlDocument document) => document is null || document.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="XmlDocument"/> instance can be considered "empty", meaning it has no child nodes.</para>
  /// </summary>
  /// <param name="document">XML document instance for evaluation.</param>
  /// <returns>If the specified <paramref name="document"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  public static bool IsEmpty(this XmlDocument document) => document?.ToEnumerable().IsEmpty() ?? throw new ArgumentNullException(nameof(document));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document">XML document to be cleared.</param>
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  public static XmlDocument Empty(this XmlDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    document.RemoveAll();

    return document;
  }
  
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="XmlDocument"/> that has the same text content as the original.</para>
  /// </summary>
  /// <param name="document">XML document instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  public static XmlDocument Clone(this XmlDocument document) => document is not null ? new XmlDocument().With(xml => xml.LoadXml(document.InnerXml)) : throw new ArgumentNullException(nameof(document));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static XmlDocument TryFinallyClear(this XmlDocument document, Action<XmlDocument> action)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return document.TryFinally(action, x => x.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="nodes"></param>
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="nodes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(XmlDocument, XmlNode[])"/>
  public static XmlDocument With(this XmlDocument document, IEnumerable<XmlNode> nodes)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (nodes is null) throw new ArgumentNullException(nameof(nodes));

    foreach (var node in nodes)
    {
      document.AppendChild(node);
    }

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="nodes"></param>
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(XmlDocument, IEnumerable{XmlNode})"/>
  public static XmlDocument With(this XmlDocument document, params XmlNode[] nodes) => document.With(nodes as IEnumerable<XmlNode>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="nodes"></param>
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="nodes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(XmlDocument, XmlNode[])"/>
  public static XmlDocument Without(this XmlDocument document, IEnumerable<XmlNode> nodes)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));
    if (nodes is null) throw new ArgumentNullException(nameof(nodes));

    foreach (var node in nodes)
    {
      document.RemoveChild(node);
    }

    return document;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="nodes"></param>
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(XmlDocument, IEnumerable{XmlNode})"/>
  public static XmlDocument Without(this XmlDocument document, params XmlNode[] nodes) => document.Without(nodes as IEnumerable<XmlNode>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <param name="destination"></param>
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
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
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
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
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
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
  /// <returns>Back self-reference to the given <paramref name="document"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="document"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  public static string Serialize(this XmlDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    using var destination = new StringWriter();

    document.Serialize(destination);

    return destination.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  public static IEnumerable<XmlNode> ToEnumerable(this XmlDocument document) => document?.ChildNodes.Cast<XmlNode>() ?? throw new ArgumentNullException(nameof(document));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  public static byte[] ToBytes(this XmlDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    using var stream = new MemoryStream();

    document.Save(stream);

    return stream.ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="document"/> is <see langword="null"/>.</exception>
  public static string ToText(this XmlDocument document)
  {
    if (document is null) throw new ArgumentNullException(nameof(document));

    using var writer = new StringWriter();

    document.Save(writer);

    return writer.ToString();
  }
}