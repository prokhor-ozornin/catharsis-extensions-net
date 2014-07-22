using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XmlReader"/>.</para>
  /// </summary>
  /// <seealso cref="XmlReader"/>
  public static class XmlReaderExtensions
  {
    /// <summary>
    ///   <para>Translates XML content from specified <see cref="XmlReader"/> into a dictionary.</para>
    ///   <para>Attributes in XML document are translated to string keys and values, nodes become dictionaries with string keys themselves.</para>
    /// </summary>
    /// <param name="self"><see cref="XmlReader"/> used for reading of XML data which is to be converted to <see cref="IDictionary{string, object}"/> instance.</param>
    /// <param name="close">Whether to automatically close a <paramref name="self"/> after conversion of XML data is finished.</param>
    /// <returns>Dictionary that follows the structure of XML data from <see cref="XmlReader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static IDictionary<string, object> Dictionary(this XmlReader self, bool close = false)
    {
      Assertion.NotNull(self);

      IDictionary<string, object> result;
      try
      {
        result = XDocument.Load(self).Dictionary();
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
    ///   <para>Calls specified delegate action in a context of <see cref="XmlReader"/>, automatically closing it after delegate method completes, and returns the result of delegate method's call.</para>
    /// </summary>
    /// <typeparam name="READER">Type of <see cref="XmlReader"/> implementation.</typeparam>
    /// <typeparam name="RESULT">Type of result, returned by <paramref name="action"/> delegate.</typeparam>
    /// <param name="self"><see cref="XmlReader"/> object, in context of which <paramref name="action"/> method is to be called. This reader will be closed automatically when <paramref name="action"/>'s call finishes, no matter whether it was successful or not.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <returns>Value, returned by calling of <paramref name="action"/> delegate's method in a context of <paramref name="self"/> object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static RESULT Read<READER, RESULT>(this READER self, Func<READER, RESULT> action) where READER : XmlReader
    {
      Assertion.NotNull(self);
      Assertion.NotNull(action);

      var result = default(RESULT);
      try
      {
        result = action(self);
      }
      finally
      {
        self.Close();
      }

      return result;
    }
  }
}