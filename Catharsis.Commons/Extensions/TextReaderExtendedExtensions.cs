using System;
using System.IO;
using System.Xml;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="TextReader"/>.</para>
  ///   <seealso cref="TextReader"/>
  /// </summary>
  public static class TextReaderExtendedExtensions
  {
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