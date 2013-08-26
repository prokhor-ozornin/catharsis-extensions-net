using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XmlReader"/>.</para>
  ///   <seealso cref="XmlReader"/>
  /// </summary>
  public static class XmlReaderExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is a <c>null</c> reference.</exception>
    public static IDictionary<string, object> Dictionary(this XmlReader reader, bool close = false)
    {
      Assertion.NotNull(reader);

      IDictionary<string, object> result;
      try
      {
        result = XDocument.Load(reader).Dictionary();
      }
      finally
      {
        if (close)
        {
          reader.Close();
        }
      }
      return result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="READER"></typeparam>
    /// <typeparam name="RESULT"></typeparam>
    /// <param name="reader"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="reader"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static RESULT Read<READER, RESULT>(this READER reader, Func<READER, RESULT> action) where READER : XmlReader
    {
      Assertion.NotNull(reader);
      Assertion.NotNull(action);

      var result = default(RESULT);
      try
      {
        result = action(reader);
      }
      finally
      {
        reader.Close();
      }

      return result;
    }
  }
}