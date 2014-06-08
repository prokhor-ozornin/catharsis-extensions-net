using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="object"/>.</para>
  /// </summary>
  /// <seealso cref="object"/>
  public static class ObjectXmlExtensions
  {
    /// <summary>
    ///   <para>Converts object to XML representation, returning XML content of the root element of the resulting XML document.</para>
    /// </summary>
    /// <param name="subject">Object to be converted to XML string.</param>
    /// <returns>XML result string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static string AsXml(this object subject)
    {
      Assertion.NotNull(subject);

      return XDocument.Parse(subject.Xml()).Root.Value;
    }

    /// <summary>
    ///   <para>Serializes given object or graph into XML string.</para>
    /// </summary>
    /// <typeparam name="T">Type of object to be serialized.</typeparam>
    /// <param name="subject">Object (or objects graph with a root element) to be serialized.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for serialization purposes.</param>
    /// <returns>Serialized XML contents of <paramref name="subject"/> instance.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="Xml{T}(T, Stream, Encoding, Type[])"/>
    /// <seealso cref="Xml{T}(T, TextWriter, Type[])"/>
    /// <seealso cref="Xml{T}(T, XmlWriter, Type[])"/>
    public static string Xml<T>(this T subject, params Type[] types)
    {
      Assertion.NotNull(subject);

      return new StringWriter().With(writer =>
      {
        subject.Xml(writer, types);
        return writer.ToString();
      });
    }

    /// <summary>
    ///   <para>Serializes given object or graph and writes XML content into specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <typeparam name="T">Type of object to be serialized.</typeparam>
    /// <param name="subject">Object (or objects graph with a root element) to be serialized.</param>
    /// <param name="destination">Destination stream to which serialized XML data is to be written.</param>
    /// <param name="encoding">Encoding to be used for transformation between bytes and characters when writing to a <paramref name="destination"/> stream. If not specified, default encoding will be used.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for serialization purposes.</param>
    /// <returns>Back reference to the currently serialized object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="types"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="Xml{T}(T, Type[])"/>
    /// <seealso cref="Xml{T}(T, TextWriter, Type[])"/>
    /// <seealso cref="Xml{T}(T, XmlWriter, Type[])"/>
    public static T Xml<T>(this T subject, Stream destination, Encoding encoding = null, params Type[] types)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(destination);

      destination.XmlWriter(encoding: encoding).Write(writer => subject.Xml(writer, types));
      return subject;
    }

    /// <summary>
    ///   <para>Serializes given object or graph and writes XML content using specified <see cref="TextWriter"/>.</para>
    /// </summary>
    /// <typeparam name="T">Type of object to be serialized.</typeparam>
    /// <param name="subject">Object (or objects graph with a root element) to be serialized.</param>
    /// <param name="writer"><see cref="TextWriter"/> to be used for writing XML content into its underlying destination.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for serialization purposes.</param>
    /// <returns>Back reference to the currently serialized object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="writer"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="Xml{T}(T, Type[])"/>
    /// <seealso cref="Xml{T}(T, Stream, Encoding, Type[])"/>
    /// <seealso cref="Xml{T}(T, XmlWriter, Type[])"/>
    public static T Xml<T>(this T subject, TextWriter writer, params Type[] types)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(writer);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      writer.XmlWriter(encoding: Encoding.UTF8).Write(xmlWriter => serializer.Serialize(xmlWriter, subject));
      return subject;
    }

    /// <summary>
    ///   <para>Serializes given object or graph and writes XML content using specified <see cref="XmlWriter"/>.</para>
    /// </summary>
    /// <typeparam name="T">Type of object to be serialized.</typeparam>
    /// <param name="subject">Object (or objects graph with a root element) to be serialized.</param>
    /// <param name="writer"><see cref="XmlWriter"/> to be used for writing XML content into its underlying destination.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for serialization purposes.</param>
    /// <returns>Back reference to the currently serialized object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="writer"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="Xml{T}(T, Type[])"/>
    /// <seealso cref="Xml{T}(T, Stream, Encoding, Type[])"/>
    /// <seealso cref="Xml{T}(T, TextWriter, Type[])"/>
    public static T Xml<T>(this T subject, XmlWriter writer, params Type[] types)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(writer);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      serializer.Serialize(writer, subject);
      return subject;
    }
  }
}