using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>
  ///     Set of extensions methods for interface <see cref="IEnumerable" />.
  ///   </para>
  ///   <seealso cref="IEnumerable" />
  /// </summary>
  public static class IEnumerableExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="enumerable"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
      Assertion.NotNull(enumerable);
      Assertion.NotNull(action);

      foreach (var value in enumerable)
      {
        action(value);
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
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
    ///   <para>Concatenates string representation of elements of the target enumeration. Concatenation result is placed in square brackets ("[]") with each element separated by comma character.</para>
    /// </summary>
    /// <param name="enumerable">Enumerator to iterate through set of elements which are target for concatenation.</param>
    /// <returns>Resulting concatenation string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="enumerable" /> is a <c>null</c> reference.</exception>
    public static string ToListString<T>(this IEnumerable<T> enumerable)
    {
      Assertion.NotNull(enumerable);

      return string.Format("[{0}]", enumerable.Join(", "));
    }
  }
}