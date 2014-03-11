using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for interface <see cref="IEnumerable" />.</para>
  /// </summary>
  /// <seealso cref="IEnumerable"/>
  public static class IEnumerableExtensions
  {
    /// <summary>
    ///   <para>Iterates through a sequence, calling a delegate for each element in it.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in a sequence.</typeparam>
    /// <param name="enumerable">Source sequence for iteration.</param>
    /// <param name="action">Delegate to be called for each element in a sequence.</param>
    /// <returns>Back reference to the current sequence.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
      Assertion.NotNull(enumerable);
      Assertion.NotNull(action);

      foreach (var value in enumerable)
      {
        action(value);
      }

      return enumerable;
    }
    
    /// <summary>
    ///   <para>Concatenates all elements in a sequence into a string, using specified separator.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in a sequence.</typeparam>
    /// <param name="enumerable">Source sequence of elements.</param>
    /// <param name="separator">String to use as a separator between concatenated elements from <paramref name="enumerable"/>.</param>
    /// <returns>String which is formed from string representation of each element in a <paramref name="enumerable"/> with a <paramref name="separator"/> between them.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="separator"/> is a <c>null</c> reference.</exception>
    public static string Join<T>(this IEnumerable<T> enumerable, string separator)
    {
      Assertion.NotNull(enumerable);
      Assertion.NotNull(separator);

      var sb = new StringBuilder();
      enumerable.Each(element => sb.AppendFormat("{0}{1}", element, separator));
      if (sb.Length > 0)
      {
        sb.Remove(sb.Length - separator.Length, separator.Length);
      }
      return sb.ToString();
    }

    /// <summary>
    ///   <para>Picks up random element from a specified sequence and returns it.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in a sequence.</typeparam>
    /// <param name="enumerable">Source sequence of elements.</param>
    /// <returns>Random member of <paramref name="enumerable"/> sequence.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is a <c>null</c> reference. If <paramref name="enumerable"/> contains no elements, returns <c>null</c>.</exception>
    public static T Random<T>(this IEnumerable<T> enumerable)
    {
      Assertion.NotNull(enumerable);

      var max = enumerable.Count();
      return (T)(max > 0 ? enumerable.ElementAt(new Random().Next(max)) : (object)null);
    }

    /// <summary>
    ///   <para>Concatenates all elements in a sequence into a string, using comma as a separator and placing the result inside a square brackets.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in a sequence.</typeparam>
    /// <param name="enumerable">Source sequence of elements.</param>
    /// <returns>String which is formed from string representation of each element in a <paramref name="enumerable"/> with a comma-character separator between them, all inside square brackets.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable" /> is a <c>null</c> reference.</exception>
    public static string ToListString<T>(this IEnumerable<T> enumerable)
    {
      Assertion.NotNull(enumerable);

      return string.Format("[{0}]", enumerable.Join(", "));
    }
  }
}