using System;
using System.Xml;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XmlWriter"/>.</para>
  /// </summary>
  /// <seealso cref="XmlWriter"/>
  public static class XmlWriterExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="WRITER"></typeparam>
    /// <param name="writer"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static WRITER Write<WRITER>(this WRITER writer, Action<WRITER> action) where WRITER : XmlWriter
    {
      Assertion.NotNull(writer);
      Assertion.NotNull(action);

      try
      {
        action(writer);
      }
      finally
      {
        writer.Close();
      }

      return writer;
    }
  }
}