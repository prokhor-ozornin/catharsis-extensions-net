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
    /// <param name="reader"><see cref="TextReader"/> which is used as a source for XML data.</param>
    /// <param name="close">Whether to automatically close <paramref name="reader"/> after deserialization process or leave it intact.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
    /// <returns>Deserialized XML contents of source <paramref name="reader"/> as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is a <c>null</c> reference.</exception>
    public static T Xml<T>(this TextReader reader, bool close = false, params Type[] types)
    {
      Assertion.NotNull(reader);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      try
      {
        return serializer.Deserialize(reader).To<T>();
      }
      finally
      {
        if (close)
        {
          reader.Close();
        }
      }
    }
  }
}