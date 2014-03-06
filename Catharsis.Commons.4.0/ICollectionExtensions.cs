using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="ICollection{T}"/>.</para>
  /// </summary>
  /// <seealso cref="ICollection{T}"/>
  public static class ICollectionExtensions
  {
    /// <summary>
    ///   <para>Sequentially adds all elements, returned by the enumerator, to the specified collection.</para>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="self">Collection to which elements are added.</param>
    /// <param name="other">Elements enumerator that provide elements for addition to the collection <paramref name="self"/>.</param>
    /// <returns>Reference to the supplied collection <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="other"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="ICollection{T}.Add(T)"/>
    /// <seealso cref="RemoveAll{T}(ICollection{T}, IEnumerable{T})"/>
    public static ICollection<T> Add<T>(this ICollection<T> self, IEnumerable<T> other)
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
    ///   <para>Sequentially removes all elements, returned by the enumerator, from the specified collection, if it has it.</para>
    /// </summary>
    /// <typeparam name="T">Type of collection's elements.</typeparam>
    /// <param name="self">Collection from which elements are removed.</param>
    /// <param name="other">Elements enumerator that provider elements for removal from the collection <see cref="self"/>.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="other"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="ICollection{T}.Remove(T)"/>
    /// <seealso cref="Add{T}(ICollection{T}, IEnumerable{T})"/>
    public static ICollection<T> Remove<T>(this ICollection<T> self, IEnumerable<T> other)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(other);

      foreach (var item in other)
      {
        self.Remove(item);
      }

      return self;
    }
  }
}