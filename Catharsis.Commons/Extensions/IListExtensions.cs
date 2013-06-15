using System;
using System.Collections;
using System.Collections.Generic;


namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IList{T}"/>.</para>
  ///   <seealso cref="IList{T}"/>
  /// </summary>
  public static class IListExtensions
  {
    /// <summary>
    ///   <para>Wraps specified list, restricting the type of elements it may hold. Only the elements that match the specified condition may be added to it.</para>
    ///   <seealso cref="AsImmutable{T}(IList{T})"/>
    ///   <seealso cref="AsNonNullable{T}(IList{T})"/>
    ///   <seealso cref="AsSized{T}(IList{T}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsMinSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsUnique{T}(IList{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">Target list for wrapping.</param>
    /// <param name="predicate">Predicate that specifies the condition for elements to match.</param>
    /// <returns>Wrapped list reference.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="list"/> or <paramref name="predicate"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped list contains any elements that do not match specified condition predicate, they are removed on wrapping.</para>
    ///   <para>The list of affected methods that may throw <see cref="ArgumentException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IList.Add(object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Add(T)"/></description></item>
    ///     <item><description><see cref="IList.Insert(int, object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Insert(int, T)"/></description></item>
    ///     <item><description><see cref="IList.this[int]"/></description></item>
    ///     <item><description><see cref="IList{T}.this[int]"/></description></item>
    ///   </list>
    /// </remarks>
    public static IList<T> AsConstrained<T>(this IList<T> list, Predicate<T> predicate)
    {
      Assertion.NotNull(list);
      Assertion.NotNull(predicate);

      return new ConstrainedList<T>(list, predicate);
    }

    /// <summary>
    ///   <para>Wraps specified list, making it "read-only", so that all methods that modify its state will throw a <see cref="NotSupportedException"/>.</para>
    ///   <seealso cref="AsNonNullable{T}(IList{T})"/>
    ///   <seealso cref="AsSized{T}(IList{T}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsMinSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsUnique{T}(IList{T})"/>
    ///   <seealso cref="AsConstrained{T}(IList{T}, Predicate{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">Target list for wrapping.</param>
    /// <returns>Wrapped list reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>The list of methods that throw <see cref="NotSupportedException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IList{T}.Add(T)"/></description></item>
    ///     <item><description><see cref="IList.Add(object)"/></description></item>
    ///     <item><description><see cref="IList.Insert(int, object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Insert(int, T)"/></description></item>
    ///     <item><description><see cref="IList.Remove(object)"/></description></item>
    ///     <item><description><see cref="IList{T}.RemoveAt(int)"/></description></item>
    ///     <item><description><see cref="IList{T}.this[int]"/></description></item>
    ///     <item><description><see cref="IList.this[int]"/></description></item>
    ///   </list>
    ///   <para>The <see cref="IList{T}.IsReadOnly"/> property for wrapped list will also be equal to <c>true</c>.</para>
    /// </remarks>
    public static IList<T> AsImmutable<T>(this IList<T> list)
    {
      Assertion.NotNull(list);

      return new ImmutableList<T>(list);
    }

    /// <summary>
    ///   <para>Wraps specified list, restricting its maximum size, so that it does not contain more than specified maximum number of elements.</para>
    ///   <seealso cref="AsImmutable{T}(IList{T})"/>
    ///   <seealso cref="AsNonNullable{T}(IList{T})"/>
    ///   <seealso cref="AsSized{T}(IList{T}, int?, int)"/>
    ///   <seealso cref="AsMinSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsUnique{T}(IList{T})"/>
    ///   <seealso cref="AsConstrained{T}(IList{T}, Predicate{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">Target list for wrapping.</param>
    /// <param name="size">list maximum size (number of elements allowed).</param>
    /// <returns>Wrapped list reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is a <c>null</c> reference.</exception>
    /// <exception cref="InvalidOperationException">If elements count is the wrapped list is greater than <paramref name="size"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IList.Add(object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Add(T)"/></description></item>
    ///     <item><description><see cref="IList.Insert(int, object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Insert(int, T)"/></description></item>
    ///   </list>
    /// </remarks>
    public static IList<T> AsMaxSized<T>(this IList<T> list, int size)
    {
      Assertion.NotNull(list);

      return new SizedList<T>(list, size);
    }


    /// <summary>
    ///   <para>Wraps specified list, restricting its minimum size, so that it does not contain less that specified minimum number of elements.</para>
    ///   <seealso cref="AsImmutable{T}(IList{T})"/>
    ///   <seealso cref="AsNonNullable{T}(IList{T})"/>
    ///   <seealso cref="AsSized{T}(IList{T}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsUnique{T}(IList{T})"/>
    ///   <seealso cref="AsConstrained{T}(IList{T}, Predicate{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">Target list for wrapping.</param>
    /// <param name="size">List minimum size (number of elements required).</param>
    /// <returns>Wrapped list reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is a <c>null</c> reference.</exception>
    /// <exception cref="InvalidOperationException">If elements count in the wrapped list is lesser than <paramref name="size"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IList.Remove(object)"/></description></item>
    ///     <item><description><see cref="IList{T}.RemoveAt(int)"/></description></item>
    ///   </list>
    /// </remarks>
    public static IList<T> AsMinSized<T>(this IList<T> list, int size)
    {
      Assertion.NotNull(list);

      return new SizedList<T>(list, null, size);
    }


    /// <summary>
    ///   <para>Wraps specified list, making it "not-nullable", so that all methods that modify its state do no accept <c>null</c> references.</para>
    ///   <seealso cref="AsImmutable{T}(IList{T})"/>
    ///   <seealso cref="AsSized{T}(IList{T}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsMinSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsUnique{T}(IList{T})"/>
    ///   <seealso cref="AsConstrained{T}(IList{T}, Predicate{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">Target list for wrapping.</param>
    /// <returns>Wrapped list reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped list contains any elements that are <c>null</c> references, they are removed on wrapping.</para>
    ///   <para>The list of affected methods is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IList.Add(object)"/></description></item>
    ///     <item><description><see cref="IList.Contains(object)"/></description></item>
    ///     <item><description><see cref="IList.IndexOf(object)"/></description></item>
    ///     <item><description><see cref="IList{T}.IndexOf(T)"/></description></item>
    ///     <item><description><see cref="IList.Insert(int, object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Insert(int, T)"/></description></item>
    ///     <item><description><see cref="IList.Remove(object)"/></description></item>
    ///     <item><description><see cref="IList.this[int]"/></description></item>
    ///     <item><description><see cref="IList{T}.this[int]"/></description></item>
    ///   </list>
    /// </remarks>
    public static IList<T> AsNonNullable<T>(this IList<T> list)
    {
      Assertion.NotNull(list);

      return new NonNullableList<T>(list);
    }

    /// <summary>
    ///   <para>Wraps specified list, restricting its maximum and minimum size, so that it does not contain less than specified minimum and more than specified maximum number of elements.</para>
    ///   <seealso cref="AsImmutable{T}(IList{T})"/>
    ///   <seealso cref="AsNonNullable{T}(IList{T})"/>
    ///   <seealso cref="AsMaxSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsMinSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsUnique{T}(IList{T})"/>
    ///   <seealso cref="AsConstrained{T}(IList{T}, Predicate{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">Target list for wrapping.</param>
    /// <param name="max">List maximum size (number of elements allowed).</param>
    /// <param name="min">List minimum size (number of elements required).</param>
    /// <returns>Wrapped list reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="max"/> is lesser than <paramref name="min"/>.</exception>
    /// <exception cref="InvalidOperationException">If elements count in the wrapped list is lesser than <paramref name="min"/> or greater than <paramref name="max"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IList.Add(object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Add(T)"/></description></item>
    ///     <item><description><see cref="IList.Insert(int, object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Insert(int, T)"/></description></item>
    ///     <item><description><see cref="IList.Remove(object)"/></description></item>
    ///     <item><description><see cref="IList{T}.RemoveAt(int)"/></description></item>
    ///   </list>
    /// </remarks>
    public static IList<T> AsSized<T>(this IList<T> list, int? max = null, int min = 0)
    {
      Assertion.NotNull(list);

      return new SizedList<T>(list, max, min);
    }

    /// <summary>
    ///   <para>Wraps specified list, so that wrapped list cannot contain any two elements that are considered equal according to the <c>Equals()</c> method.</para>
    ///   <seealso cref="AsImmutable{T}(IList{T})"/>
    ///   <seealso cref="AsNonNullable{T}(IList{T})"/>
    ///   <seealso cref="AsSized{T}(IList{T}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsMinSized{T}(IList{T}, int)"/>
    ///   <seealso cref="AsConstrained{T}(IList{T}, Predicate{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of list's elements.</typeparam>
    /// <param name="list">Target list for wrapping.</param>
    /// <returns>Wrapped list reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped list contains any two equal elements, only the first of them is added on wrapping.</para>
    ///   <para>The list of affected methods that may throw <see cref="InvalidOperationException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IList.Add(object)"/></description></item>
    ///     <item><description><see cref="IList{T}.Add(T)"/></description></item>
    ///     <item><description></description><see cref="IList.Insert(int, object)"/></item>
    ///     <item><description></description><see cref="IList{T}.Insert(int, T)"/></item>
    ///     <item><description></description><see cref="IList.this[int]"/></item>
    ///     <item><description><see cref="IList{T}.this[int]"/></description></item>
    ///   </list>
    /// </remarks>
    public static IList<T> AsUnique<T>(this IList<T> list)
    {
      Assertion.NotNull(list);

      return new UniqueList<T>(list);
    }
    
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