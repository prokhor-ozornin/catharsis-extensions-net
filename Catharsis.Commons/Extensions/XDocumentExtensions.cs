using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XDocument"/>.</para>
  ///   <seealso cref="XDocument"/>
  /// </summary>
  public static class XDocumentExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="document"/> is a <c>null</c> reference.</exception>
    public static IDictionary<string, object> Dictionary(this XDocument document)
    {
      Assertion.NotNull(document);

      return document.Root.Dictionary();
    }
  }
}