using System;
using System.Xml.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="XContainer"/>.</para>
  ///   <seealso cref="XContainer"/>
  /// </summary>
  public static class XContainerExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="container"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="container"/> is a <c>null</c> reference.</exception>
    public static T AddContent<T>(this T container, params object[] content) where T : XContainer
    {
      Assertion.NotNull(container);

      container.Add(content);
      return container;
    }
  }
}