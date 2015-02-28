using System;
using System.IO;
using System.Text;
using System.Xml;
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
    ///   <para>Serializes given object or graph into XML string.</para>
    /// </summary>
    /// <typeparam name="T">Type of object to be serialized.</typeparam>
    /// <param name="self">Object (or objects graph with a root element) to be serialized.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for serialization purposes.</param>
    /// <returns>Serialized XML contents of <paramref name="self"/> instance.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="ToXml{T}(T, Stream, Encoding, Type[])"/>
    /// <seealso cref="ToXml{T}(T, TextWriter, Type[])"/>
    /// <seealso cref="ToXml{T}(T, XmlWriter, Type[])"/>
    public static string ToXml<T>(this T self, params Type[] types)
    {
      Assertion.NotNull(self);

      using (var writer = new StringWriter())
      {
        self.ToXml(writer, types);
        return writer.ToString();
      }
    }

    /// <summary>
    ///   <para>Serializes given object or graph and writes XML content into specified <see cref="Stream"/>.</para>
    /// </summary>
    /// <typeparam name="T">Type of object to be serialized.</typeparam>
    /// <param name="self">Object (or objects graph with a root element) to be serialized.</param>
    /// <param name="destination">Destination stream to which serialized XML data is to be written.</param>
    /// <param name="encoding">Encoding to be used for transformation between bytes and characters when writing to a <paramref name="destination"/> stream. If not specified, default encoding will be used.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for serialization purposes.</param>
    /// <returns>Back reference to the currently serialized object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="types"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="ToXml{T}(T, Type[])"/>
    /// <seealso cref="ToXml{T}(T, TextWriter, Type[])"/>
    /// <seealso cref="ToXml{T}(T, XmlWriter, Type[])"/>
    public static T ToXml<T>(this T self, Stream destination, Encoding encoding = null, params Type[] types)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(destination);

      destination.XmlWriter(encoding: encoding).Write(writer => self.ToXml(writer, types));
      return self;
    }

    /// <summary>
    ///   <para>Serializes given object or graph and writes XML content using specified <see cref="TextWriter"/>.</para>
    /// </summary>
    /// <typeparam name="T">Type of object to be serialized.</typeparam>
    /// <param name="self">Object (or objects graph with a root element) to be serialized.</param>
    /// <param name="writer"><see cref="TextWriter"/> to be used for writing XML content into its underlying destination.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for serialization purposes.</param>
    /// <returns>Back reference to the currently serialized object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="writer"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="ToXml{T}(T, Type[])"/>
    /// <seealso cref="ToXml{T}(T, Stream, Encoding, Type[])"/>
    /// <seealso cref="ToXml{T}(T, XmlWriter, Type[])"/>
    public static T ToXml<T>(this T self, TextWriter writer, params Type[] types)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(writer);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      writer.XmlWriter(encoding: Encoding.UTF8).Write(xmlWriter => serializer.Serialize(xmlWriter, self));
      return self;
    }

    /// <summary>
    ///   <para>Serializes given object or graph and writes XML content using specified <see cref="XmlWriter"/>.</para>
    /// </summary>
    /// <typeparam name="T">Type of object to be serialized.</typeparam>
    /// <param name="self">Object (or objects graph with a root element) to be serialized.</param>
    /// <param name="writer"><see cref="XmlWriter"/> to be used for writing XML content into its underlying destination.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for serialization purposes.</param>
    /// <returns>Back reference to the currently serialized object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="writer"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="ToXml{T}(T, Type[])"/>
    /// <seealso cref="ToXml{T}(T, Stream, Encoding, Type[])"/>
    /// <seealso cref="ToXml{T}(T, TextWriter, Type[])"/>
    public static T ToXml<T>(this T self, XmlWriter writer, params Type[] types)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(writer);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      serializer.Serialize(writer, self);
      return self;
    }
  }
}