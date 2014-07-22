using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="TextReader"/>.</para>
  /// </summary>
  /// <seealso cref="TextReader"/>
  public static class TextReaderExtensions
  {
    /// <summary>
    ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a list of strings, using default system-dependent string separator.</para>
    /// </summary>
    /// <param name="self"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
    /// <returns>List of strings which have been read from a <paramref name="self"/>.</returns>
    /// <param name="close">Whether to close a <paramref name="self"/> after all texts have been read.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string[] Lines(this TextReader self, bool close = false)
    {
      Assertion.NotNull(self);

      return self.Text(close).Lines();
    }

    /// <summary>
    ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a string.</para>
    /// </summary>
    /// <param name="self"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
    /// <returns>Text content which have been read from a <paramref name="self"/>.</returns>
    /// <param name="close">Whether to close a <paramref name="self"/> after all text have been read.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string Text(this TextReader self, bool close = false)
    {
      Assertion.NotNull(self);

      var result = string.Empty;
      try
      {
        result = self.ReadToEnd();
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
    ///   <para>Deserialize XML contents from a <see cref="TextReader"/> into <see cref="XDocument"/> object.</para>
    /// </summary>
    /// <param name="self"><see cref="TextReader"/> which is used to read XML text content from its underlying source.</param>
    /// <param name="close">Whether to close <paramref name="self"/> after all XML content have been read and deserialized.</param>
    /// <returns><see cref="XDocument"/> instance, constructed from XML contents which have been read through a <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XDocument"/>
    public static XDocument XDocument(this TextReader self, bool close = false)
    {
      Assertion.NotNull(self);

      return System.Xml.XmlReader.Create(self, new XmlReaderSettings { CloseInput = close }).Read(System.Xml.Linq.XDocument.Load);
    }

    /// <summary>
    ///   <para>Creates <see cref="XmlReader"/> that wraps specified <see cref="TextReader"/> instance.</para>
    /// </summary>
    /// <param name="self"><see cref="TextReader"/> that wraps XML data source.</param>
    /// <param name="close">Whether to automatically close <paramref name="self"/> when wrapping <see cref="XmlReader"/> will be closed.</param>
    /// <returns><see cref="XmlReader"/> instance that wraps a text <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XmlReader"/>
    public static XmlReader XmlReader(this TextReader self, bool close = false)
    {
      Assertion.NotNull(self);

      return System.Xml.XmlReader.Create(self, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true });
    }
  }
}