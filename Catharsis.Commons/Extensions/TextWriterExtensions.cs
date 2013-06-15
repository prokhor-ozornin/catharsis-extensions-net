using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="TextWriter"/>.</para>
  ///   <seealso cref="TextWriter"/>
  /// </summary>
  public static class TextWriterExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="WRITER"></typeparam>
    /// <param name="writer"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is a <c>null</c> reference.</exception>
    public static WRITER WriteObject<WRITER>(this WRITER writer, object subject) where WRITER : TextWriter
    {
      Assertion.NotNull(writer);

      if (subject != null)
      {
        writer.Write(subject);
      }

      return writer;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="encoding"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is a <c>null</c> reference.</exception>
    public static XmlWriter XmlWriter(this TextWriter writer, Encoding encoding = null, bool close = false)
    {
      Assertion.NotNull(writer);

      var settings = new XmlWriterSettings { CloseOutput = close };
      if (encoding != null)
      {
        settings.Encoding = encoding;
      }

      return System.Xml.XmlWriter.Create(writer, settings);
    }
  }
}