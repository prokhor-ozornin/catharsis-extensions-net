using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IList{T}"/>.</para>
  ///   <seealso cref="IList{T}"/>
  /// </summary>
  public static class IListExtensions
  {
    /// <summary>
    ///   <para>Inserts specified element to the list and returns list reference back to perform further operations.</para>
    ///   <seealso cref="IList{T}.Insert(int, T)"/>
    ///   <seealso cref="RemoveAtNext{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">List to insert element into.</param>
    /// <param name="index">The zero-based index at which element should be inserted.</param>
    /// <param name="item">Element that is being inserted.</param>
    /// <returns>Reference to the supplied list <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is a <c>null</c> reference.</exception>
    public static IList<T> InsertNext<T>(this IList<T> list, int index, T item)
    {
      Assertion.NotNull(list);

      list.Insert(index, item);
      return list;
    }

    /// <summary>
    ///   <para>Removes element at the specified index from the list and returns list reference back to perform further operations.</para>
    ///   <seealso cref="IList{T}.RemoveAt(int)"/>
    ///   <seealso cref="InsertNext{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">List to remove element from.</param>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <returns>Reference to the supplied list <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is a <c>null</c> reference.</exception>
    public static IList<T> RemoveAtNext<T>(this IList<T> list, int index)
    {
      Assertion.NotNull(list);

      list.RemoveAt(index);
      return list;
    }
  }
}