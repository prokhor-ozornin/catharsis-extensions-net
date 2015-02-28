using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XDocument"/>.</para>
  /// </summary>
  /// <seealso cref="XDocument"/>
  public static class XDocumentExtensions
  {
    /// <summary>
    ///   <para>Translates specified <see cref="XDocument"/> into a dictionary.</para>
    ///   <para>Attributes in XML document are translated to string keys and values, nodes become dictionaries with string keys themselves.</para>
    /// </summary>
    /// <param name="self"><see cref="XDocument"/>, whose structure is to be converted to <see cref="IDictionary{string, object}"/> instance.</param>
    /// <returns>Dictionary that follows the structure of <see cref="XDocument"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="XElementExtensions.Dictionary(XElement)"/>
    public static IDictionary<string, object> Dictionary(this XDocument self)
    {
      Assertion.NotNull(self);

      return self.Root.Dictionary();
    }
  }
}