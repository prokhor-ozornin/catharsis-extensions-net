using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IEnumerable{T}"/>.</para>
  /// </summary>
  /// <seealso cref="IEnumerable{T}"/>
  public static class IEnumerableCollectionsExtensions
  {
    /// <summary>
    ///   <para>Converts sequence of elements into a set collection type.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in a sequence.</typeparam>
    /// <param name="enumerable">Source sequence of elements.</param>
    /// <returns>Set collection which contains elements from <paramref name="enumerable"/> sequence without dublicates. Order of elements in a set is not guaranteed to be the same as returned by <paramref name="enumerable"/>'s enumerator.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is a <c>null</c> reference.</exception>
    public static ISet<T> ToSet<T>(this IEnumerable<T> enumerable)
    {
      Assertion.NotNull(enumerable);

      var set = new HashSet<T>();
      set.Add(enumerable);
      return set;
    }
  }
}