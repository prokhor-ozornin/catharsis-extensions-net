using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  ///   <seealso cref="Stream"/>
  /// </summary>
  public static class StreamXmlExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream"></param>
    /// <param name="close"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
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
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
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