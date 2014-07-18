﻿using System;
using System.IO;
using System.Xml.Serialization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  /// </summary>
  /// <seealso cref="string"/>
  public static class StringXmlExtensions
  {
    /// <summary>
    ///   <para>Deserializes XML string text into object of specified type.</para>
    /// </summary>
    /// <typeparam name="T">Type of object which is to be the result of deserialization process.</typeparam>
    /// <param name="xml">XML data for deserialization.</param>
    /// <param name="types">Additional types to be used by <see cref="XmlSerializer"/> for deserialization purposes.</param>
    /// <returns>Deserialized XML contents of <paramref name="xml"/> string as the object (or objects graph with a root element) of type <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="xml"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="XmlSerializer"/>
    public static T AsXml<T>(this string xml, params Type[] types)
    {
      Assertion.NotEmpty(xml);

      return new StringReader(xml).AsXml<T>(true, types);
    }
  }
}