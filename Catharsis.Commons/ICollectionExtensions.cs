using System;
using System.Collections.Generic;

namespace Catharsis.Commons
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