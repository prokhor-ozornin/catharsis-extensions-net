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
    ///   <para>Creates <see cref="XmlWriter"/> that wraps specified <see cref="TextWriter"/> instance.</para>
    /// </summary>
    /// <param name="self"><see cref="TextWriter"/> that wraps XML data destination.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> when wrapping <see cref="XmlWriter"/> will be closed.</param>
    /// <param name="encoding">Encoding to be used by <see cref="XmlWriter"/>. If not specified, default encoding will be used.</param>
    /// <returns><see cref="XmlWriter"/> instance that wraps a text <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlWriter"/>
    public static XmlWriter XmlWriter(this TextWriter self, bool close = false, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      var settings = new XmlWriterSettings { CloseOutput = close };
      if (encoding != null)
      {
        settings.Encoding = encoding;
      }

      return System.Xml.XmlWriter.Create(self, settings);
    }
  }
}