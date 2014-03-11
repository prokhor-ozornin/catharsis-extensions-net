using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  /// </summary>
  /// <seealso cref="Stream"/>
  public static class StreamXmlExtensions
  {
    /// <summary>
    ///   <para>Deserializes XML contents of stream into object of specified type.</para>
    /// </summary>
    /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
    /// <param name="stream">Stream of XML data for deserialization.</param>
    /// <param name="close">Whether to automatically close <paramref name="stream"/> after deserialization process or leave it intact.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
    /// <returns>Deserialized XML contents of source <paramref name="stream"/> as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    public static T Xml<T>(this Stream stream, bool close = false, params Type[] types)
    {
      Assertion.NotNull(stream);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      try
      {
        return serializer.Deserialize(stream).To<T>();
      }
      finally
      {
        if (close)
        {
          stream.Close();
        }
      }
    }

    /// <summary>
    ///   <para>Deserializes XML contents of stream into <see cref="XmlDocument"/> object.</para>
    /// </summary>
    /// <param name="stream">Stream of XML data for deserialization.</param>
    /// <param name="close">Whether to automatically close <paramref name="stream"/> after deserialization process or leave it intact.</param>
    /// <returns>Deserialized XML contents of source <paramref name="stream"/> as instance of <see cref="XmlDocument"/> class.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlDocument"/>
    public static XmlDocument XmlDocument(this Stream stream, bool close = false)
    {
      Assertion.NotNull(stream);

      var document = new XmlDocument();
      try
      {
        document.Load(stream);
      }
      finally
      {
        if (close)
        {
          stream.Close();
        }
      }
      return document;
    }
  }
}