using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IEnumerable{T}"/>.</para>
  /// </summary>
  /// <seealso cref="IEnumerable{T}"/>
  public static class IEnumerableSetExtensions
  {
    /// <summary>
    ///   <para>Converts sequence of elements into a set collection type.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in a sequence.</typeparam>
    /// <param name="self">Source sequence of elements.</param>
    /// <returns>Set collection which contains elements from <paramref name="self"/> sequence without dublicates. Order of elements in a set is not guaranteed to be the same as returned by <paramref name="self"/>'s enumerator.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static ISet<T> ToSet<T>(this IEnumerable<T> self)
    {
      Assertion.NotNull(self);

      var set = new HashSet<T>();
      set.Add(self);
      return set;
    }
  }
}