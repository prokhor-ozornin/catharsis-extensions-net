using System;
using System.Collections.Generic;


namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="ICollection{T}"/>.</para>
  ///   <seealso cref="ICollection{T}"/>
  /// </summary>
  public static class ICollectionExtensions
  {
    /// <summary>
    ///   <para>Sequentially adds all elements, returned by the enumerator, to the specified collection.</para>
    ///   <seealso cref="ICollection{T}.Add(T)"/>
    ///   <seealso cref="RemoveAll{T}(ICollection{T}, IEnumerable{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="self">Collection to which elements are added.</param>
    /// <param name="other">Elements enumerator that provide elements for addition to the collection <paramref name="self"/>.</param>
    /// <returns>Reference to the supplied collection <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="other"/> is a <c>null</c> reference.</exception>
    public static ICollection<T> AddAll<T>(this ICollection<T> self, IEnumerable<T> other)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(other);

      foreach (var item in other)
      {
        self.Add(item);
      }

      return self;
    }


    /// <summary>
    ///   <para>Adds specified element to the collection and returns collection reference back to perform further operations.</para>
    ///   <seealso cref="ICollection{T}.Add(T)"/>
    ///   <seealso cref="RemoveNext{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Collection to add element to.</param>
    /// <param name="item">Element that is being added.</param>
    /// <returns>Reference to the supplied collection <paramref name="collection"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is a <c>null</c> reference.</exception>
    public static ICollection<T> AddNext<T>(this ICollection<T> collection, T item)
    {
      Assertion.NotNull(collection);

      collection.Add(item);
      return collection;
    }

    /// <summary>
    ///   <para>Wraps specified collection, restricting the type of elements it may hold. Only the elements that match the specified condition may be added to it.</para>
    ///   <seealso cref="AsImmutable{T}"/>
    ///   <seealso cref="AsNonNullable{T}"/>
    ///   <seealso cref="AsSized{T}"/>
    ///   <seealso cref="AsMaxSized{T}"/>
    ///   <seealso cref="AsMinSized{T}"/>
    ///   <seealso cref="AsUnique{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Target collection for wrapping.</param>
    /// <param name="predicate">Predicate that specifies the condition for elements to match.</param>
    /// <returns>Wrapped collection reference.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="collection"/> or <paramref name="predicate"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped collection contains any elements that do not match specified condition predicate, they are removed on wrapping.</para>
    ///   <para>The list of affected methods that may throw <see cref="ArgumentException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ICollection{T}.Add(T)"/></description></item>
    ///   </list>
    /// </remarks>
    public static ICollection<T> AsConstrained<T>(this ICollection<T> collection, Predicate<T> predicate)
    {
      Assertion.NotNull(collection);
      Assertion.NotNull(predicate);

      return new ConstrainedCollection<T>(collection, predicate);
    }

    /// <summary>
    ///   <para>Wraps specified collection, making it "read-only", so that all methods that modify its state will throw a <see cref="NotSupportedException"/>.</para>
    ///   <seealso cref="AsNonNullable{T}"/>
    ///   <seealso cref="AsSized{T}"/>
    ///   <seealso cref="AsMaxSized{T}"/>
    ///   <seealso cref="AsMinSized{T}"/>
    ///   <seealso cref="AsUnique{T}"/>
    ///   <seealso cref="AsConstrained{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Target collection for wrapping.</param>
    /// <returns>Wrapped collection reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>The list of methods that throw <see cref="NotSupportedException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ICollection{T}.Add(T)"/></description></item>
    ///     <item><description><see cref="ICollection{T}.Clear()"/></description></item>
    ///     <item><description><see cref="ICollection{T}.Remove(T)"/></description></item>
    ///   </list>
    ///   <para>The <see cref="ICollection{T}.IsReadOnly"/> property for wrapped collection will also be equal to <c>true</c>.</para>
    /// </remarks>
    public static ICollection<T> AsImmutable<T>(this ICollection<T> collection)
    {
      Assertion.NotNull(collection);

      return new ImmutableCollection<T>(collection);
    }

    /// <summary>
    ///   <para>Wraps specified collection, restricting its maximum size, so that it does not contain more than specified maximum number of elements.</para>
    ///   <seealso cref="AsImmutable{T}"/>
    ///   <seealso cref="AsNonNullable{T}"/>
    ///   <seealso cref="AsSized{T}"/>
    ///   <seealso cref="AsMinSized{T}"/>
    ///   <seealso cref="AsUnique{T}"/>
    ///   <seealso cref="AsConstrained{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Target collection for wrapping.</param>
    /// <param name="size">Collection maximum size (number of elements allowed).</param>
    /// <returns>Wrapped collection reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is a <c>null</c> reference.</exception>
    /// <exception cref="InvalidOperationException">If elements count is the wrapped collection is greater than <paramref name="size"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ICollection{T}.Add(T)"/></description></item>
    ///   </list>
    /// </remarks>
    public static ICollection<T> AsMaxSized<T>(this ICollection<T> collection, int size)
    {
      Assertion.NotNull(collection);

      return new SizedCollection<T>(collection, size);
    }


    /// <summary>
    ///   <para>Wraps specified collection, restricting its minimum size, so that it does not contain less that specified minimum number of elements.</para>
    ///   <seealso cref="AsImmutable{T}"/>
    ///   <seealso cref="AsNonNullable{T}"/>
    ///   <seealso cref="AsSized{T}"/>
    ///   <seealso cref="AsMaxSized{T}"/>
    ///   <seealso cref="AsUnique{T}"/>
    ///   <seealso cref="AsConstrained{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Target collection for wrapping.</param>
    /// <param name="size">Collection minimum size (number of elements required).</param>
    /// <returns>Wrapped collection reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is a <c>null</c> reference.</exception>
    /// <exception cref="InvalidOperationException">If elements count in the wrapped collection is lesser than <paramref name="size"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ICollection{T}.Clear()"/></description></item>
    ///     <item><description><see cref="ICollection{T}.Remove(T)"/></description></item>
    ///   </list>
    /// </remarks>
    public static ICollection<T> AsMinSized<T>(this ICollection<T> collection, int size)
    {
      Assertion.NotNull(collection);

      return new SizedCollection<T>(collection, null, size);
    }

    /// <summary>
    ///   <para>Wraps specified collection, making it "not-nullable", so that all methods that modify its state do no accept <c>null</c> references.</para>
    ///   <seealso cref="AsImmutable{T}"/>
    ///   <seealso cref="AsSized{T}"/>
    ///   <seealso cref="AsMaxSized{T}"/>
    ///   <seealso cref="AsMinSized{T}"/>
    ///   <seealso cref="AsUnique{T}"/>
    ///   <seealso cref="AsConstrained{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Target collection for wrapping.</param>
    /// <returns>Wrapped collection reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped collection contains any elements that are <c>null</c> references, they are removed on wrapping.</para>
    ///   <para>The list of affected methods is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ICollection{T}.Add(T)"/></description></item>
    ///     <item><description><see cref="ICollection{T}.Contains(T)"/></description></item>
    ///     <item><description><see cref="ICollection{T}.CopyTo(T[], int)"/></description></item>
    ///     <item><description><see cref="ICollection{T}.Remove(T)"/></description></item>
    ///   </list>
    /// </remarks>
    public static ICollection<T> AsNonNullable<T>(this ICollection<T> collection)
    {
      Assertion.NotNull(collection);

      return new NonNullableCollection<T>(collection);
    }

    /// <summary>
    ///   <para>Wraps specified collection, restricting its maximum and minimum size, so that it does not contain less than specified minimum and more than specified maximum number of elements.</para>
    ///   <seealso cref="AsImmutable{T}"/>
    ///   <seealso cref="AsNonNullable{T}"/>
    ///   <seealso cref="AsMaxSized{T}"/>
    ///   <seealso cref="AsMinSized{T}"/>
    ///   <seealso cref="AsUnique{T}"/>
    ///   <seealso cref="AsConstrained{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Target collection for wrapping.</param>
    /// <param name="max">Collection maximum size (number of elements allowed).</param>
    /// <param name="min">Collection minimum size (number of elements required).</param>
    /// <returns>Wrapped collection reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="max"/> is lesser than <paramref name="min"/>.</exception>
    /// <exception cref="InvalidOperationException">If elements count in the wrapped collection is lesser than <paramref name="min"/> or greater than <paramref name="max"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ICollection{T}.Add(T)"/></description></item>
    ///     <item><description><see cref="ICollection{T}.Clear()"/></description></item>
    ///     <item><description><see cref="ICollection{T}.Remove(T)"/></description></item>
    ///   </list>
    /// </remarks>
    public static ICollection<T> AsSized<T>(this ICollection<T> collection, int? max = null, int min = 0)
    {
      Assertion.NotNull(collection);

      return new SizedCollection<T>(collection, max, min);
    }

    /// <summary>
    ///   <para>Wraps specified collection, so that wrapped collection cannot contain any two elements that are considered equal according to the <c>Equals()</c> method.</para>
    ///   <seealso cref="AsImmutable{T}"/>
    ///   <seealso cref="AsNonNullable{T}"/>
    ///   <seealso cref="AsSized{T}"/>
    ///   <seealso cref="AsMaxSized{T}"/>
    ///   <seealso cref="AsMinSized{T}"/>
    ///   <seealso cref="AsConstrained{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Target collection for wrapping.</param>
    /// <returns>Wrapped collection reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped collection contains any two equal elements, only the first of them is added on wrapping.</para>
    ///   <para>The list of affected methods that may throw <see cref="InvalidOperationException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ICollection{T}.Add(T)"/></description></item>
    ///   </list>
    /// </remarks>
    public static ICollection<T> AsUnique<T>(this ICollection<T> collection)
    {
      Assertion.NotNull(collection);

      return new UniqueCollection<T>(collection);
    }


    /// <summary>
    ///   <para>Sequentially removes all elements, returned by the enumerator, from the specified collection, if it has it.</para>
    ///   <seealso cref="ICollection{T}.Remove(T)"/>
    ///   <seealso cref="AddAll{T}(ICollection{T}, IEnumerable{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="self">Collection from which elements are removed.</param>
    /// <param name="other">Elements enumerator that provider elements for removal from the collection <see cref="self"/>.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="other"/> is a <c>null</c> reference.</exception>
    public static ICollection<T> RemoveAll<T>(this ICollection<T> self, IEnumerable<T> other)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(other);

      foreach (var item in other)
      {
        self.Remove(item);
      }

      return self;
    }


    /// <summary>
    ///   <para>Removes specified element from the collection, it if exists, and returns collection reference back to perform further operations.</para>
    ///   <seealso cref="ICollection{T}.Remove(T)"/>
    ///   <seealso cref="AddContent{T}"/>
    ///   <seealso cref="ClearNext{T}"/>
    ///   <seealso cref="CopyToNext{T}"/>
    ///   <seealso cref="RemoveNext{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="collection">Collection to remove element from.</param>
    /// <param name="item">Element to be removed.</param>
    /// <returns>Reference to the supplied collection <see cref="collection"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is a <c>null</c> reference.</exception>
    public static ICollection<T> RemoveNext<T>(this ICollection<T> collection, T item)
    {
      Assertion.NotNull(collection);

      collection.Remove(item);
      return collection;
    }
  }
}