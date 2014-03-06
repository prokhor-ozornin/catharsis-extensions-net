using System;
using System.IO;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  /// </summary>
  /// <seealso cref="string"/>
  public static class StringXmlExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="xml"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="xml"/> is <see cref="string.Empty"/> string.</exception>
    public static T Xml<T>(this string xml, params Type[] types)
    {
      Assertion.NotEmpty(xml);

      return new StringReader(xml).Xml<T>(true, types);
    }
  }
}