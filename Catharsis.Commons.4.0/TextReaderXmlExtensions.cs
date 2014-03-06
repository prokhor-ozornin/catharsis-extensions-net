using System;
using System.IO;
using System.Xml;
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
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="reader"></param>
    /// <param name="close"></param>
    /// <param name="types"></param>
    /// <returns></returns>
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

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is a <c>null</c> reference.</exception>
    public static XmlDocument XmlDocument(this TextReader reader, bool close = false)
    {
      Assertion.NotNull(reader);

      var document = new XmlDocument();
      try
      {
        document.Load(reader);
      }
      finally
      {
        if (close)
        {
          reader.Close();
        }
      }
      return document;
    }
  }
}