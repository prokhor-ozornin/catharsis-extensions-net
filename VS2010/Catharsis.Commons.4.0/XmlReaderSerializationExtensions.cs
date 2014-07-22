using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XmlReader"/>.</para>
  /// </summary>
  /// <seealso cref="XmlReader"/>
  public static class XmlReaderSerializationExtensions
  {
    /// <summary>
    ///   <para>Deserializes XML data from <see cref="XmlReader"/> into object of specified type.</para>
    /// </summary>
    /// <param name="self"><see cref="XmlReader"/> used for retrieving XML data for deserialization.</param>
    /// <param name="type">Type of object which is to be the result of deserialization process.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> after deserialization process or leave it intact.</param>
    /// <returns>Deserialized XML contents from a <paramref name="self"/> as the object (or objects graph with a root element) of <paramref name="type"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="Deserialize{T}(XmlReader, IEnumerable{Type}, bool)"/>
    public static object Deserialize(this XmlReader self, Type type, IEnumerable<Type> types = null, bool close = false)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(type);

      object result;
      try
      {
        result = (types != null ? new XmlSerializer(type, types.ToArray()) : new XmlSerializer(type)).Deserialize(self);
      }
      finally
      {
        if (close)
        {
          self.Close();
        }
      }
      return result;
    }

    /// <summary>
    ///   <para>Deserializes XML data from <see cref="XmlReader"/> into object of specified type.</para>
    /// </summary>
    /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
    /// <param name="self"><see cref="XmlReader"/> used for retrieving XML data for deserialization.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> after deserialization process or leave it intact.</param>
    /// <returns>Deserialized XML contents from a <paramref name="self"/> as the object (or objects graph with a root element) of <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlSerializer"/>
    /// <seealso cref="Deserialize(XmlReader, Type, IEnumerable{Type}, bool)"/>
    public static T Deserialize<T>(this XmlReader self, IEnumerable<Type> types = null, bool close = false)
    {
      Assertion.NotNull(self);

      return self.Deserialize(typeof(T), types, close).As<T>();
    }
  }
}