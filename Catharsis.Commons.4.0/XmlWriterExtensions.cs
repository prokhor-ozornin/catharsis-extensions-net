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
    ///   <para>Calls specified delegate action in a context of target <see cref="XmlWriter"/>, automatically closing it after the call.</para>
    /// </summary>
    /// <typeparam name="WRITER">Type of <see cref="XmlWriter"/> implementation.</typeparam>
    /// <param name="writer"><see cref="XmlWriter"/> instance, in context of which <paramref name="action"/> method is to be called. It will be closed automatically no matter whether <paramref name="action"/> method call succeeds or fails.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <returns>Back reference to the current target <see cref="XmlWriter"/>.</returns>
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