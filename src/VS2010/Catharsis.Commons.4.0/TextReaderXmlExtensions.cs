using System;
using System.IO;
using System.Xml.Serialization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="TextReader"/>.</para>
  /// </summary>
  /// <seealso cref="TextReader"/>
  public static class TextReaderXmlExtensions
  {
    /// <summary>
    ///   <para>Deserializes XML contents from <see cref="TextReader"/> into object of specified type.</para>
    /// </summary>
    /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
    /// <param name="self"><see cref="TextReader"/> which is used as a source for XML data.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> after deserialization process or leave it intact.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
    /// <returns>Deserialized XML contents of source <paramref name="self"/> as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static T AsXml<T>(this TextReader self, bool close = false, params Type[] types)
    {
      Assertion.NotNull(self);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      try
      {
        return serializer.Deserialize(self).To<T>();
      }
      finally
      {
        if (close)
        {
          self.Close();
        }
      }
    }
  }
}