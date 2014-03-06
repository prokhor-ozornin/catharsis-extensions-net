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
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
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
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="types"/> is a <c>null</c> reference.</exception>
    public static T Xml<T>(this T subject, Stream destination, Encoding encoding = null, params Type[] types)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(destination);

      destination.XmlWriter(encoding: encoding).Write(writer => subject.Xml(writer, types));
      return subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="writer"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="writer"/> is a <c>null</c> reference.</exception>
    public static T Xml<T>(this T subject, TextWriter writer, params Type[] types)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(writer);

      var serializer = types != null ? new XmlSerializer(typeof(T), types) : new XmlSerializer(typeof(T));
      writer.XmlWriter(encoding: Encoding.UTF8).Write(xmlWriter => serializer.Serialize(xmlWriter, subject));
      return subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="writer"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="writer"/> is a <c>null</c> reference.</exception>
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