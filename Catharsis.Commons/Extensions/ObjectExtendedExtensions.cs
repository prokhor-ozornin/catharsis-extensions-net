using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="object"/>.</para>
  ///   <seealso cref="object"/>
  /// </summary>
  public static class ObjectExtendedExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="destination"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="destination"/> is a <c>null</c> reference.</exception>
    public static object Binary(this object subject, Stream destination, bool close = false)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(destination);

      try
      {
        new BinaryFormatter().Serialize(destination, subject);
      }
      finally
      {
        if (close)
        {
          destination.Close();
        }
      }

      return subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static string Xml<T>(this T subject, IEnumerable<Type> types = null)
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
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string Xml<T>(this T subject, params Type[] types)
    {
      Assertion.NotNull(subject);

      return subject.Xml(types ?? Enumerable.Empty<Type>());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="destination"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="destination"/> is a <c>null</c> reference.</exception>
    public static T Xml<T>(this T subject, Stream destination, IEnumerable<Type> types = null)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(destination);

      destination.XmlWriter(encoding: Encoding.Unicode).Write(writer => subject.Xml(writer, types));
      return subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="destination"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="types"/> is a <c>null</c> reference.</exception>
    public static T Xml<T>(this T subject, Stream destination, params Type[] types)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(destination);

      return subject.Xml(destination, types ?? Enumerable.Empty<Type>());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static byte[] Binary(this object subject)
    {
      Assertion.NotNull(subject);

      return new MemoryStream().With(stream =>
      {
        subject.Binary(stream);
        return stream;
      }).ToArray();
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
    public static T Xml<T>(this T subject, TextWriter writer, IEnumerable<Type> types = null)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(writer);

      var serializer = types != null ? new XmlSerializer(typeof(T), types.ToArray()) : new XmlSerializer(typeof(T));
      writer.XmlWriter(encoding: Encoding.Unicode).Write(xmlWriter => serializer.Serialize(xmlWriter, subject));
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

      return subject.Xml(writer, types ?? Enumerable.Empty<Type>());
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
    public static T Xml<T>(this T subject, XmlWriter writer, IEnumerable<Type> types = null)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(writer);

      var serializer = types != null ? new XmlSerializer(typeof(T), types.ToArray()) : new XmlSerializer(typeof(T));
      serializer.Serialize(writer, subject);
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

      return subject.Xml(writer, types ?? Enumerable.Empty<Type>());
    }
  }
}