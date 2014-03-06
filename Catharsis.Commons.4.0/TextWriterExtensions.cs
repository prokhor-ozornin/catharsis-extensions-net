using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="TextWriter"/>.</para>
  /// </summary>
  /// <seealso cref="TextWriter"/>
  public static class TextWriterExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="close"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is a <c>null</c> reference.</exception>
    public static XmlWriter XmlWriter(this TextWriter writer, bool close = false, Encoding encoding = null)
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