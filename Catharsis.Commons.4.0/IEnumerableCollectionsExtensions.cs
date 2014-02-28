using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IEnumerable{T}"/>.</para>
  ///   <seealso cref="IEnumerable{T}"/>
  /// </summary>
  public static class IEnumerableCollectionsExtensions
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is a <c>null</c> reference.</exception>
    public static ISet<T> ToSet<T>(this IEnumerable<T> enumerable)
    {
      Assertion.NotNull(enumerable);

      var set = new HashSet<T>();
      set.AddAll(enumerable);
      return set;
    }
  }
}