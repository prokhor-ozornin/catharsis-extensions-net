namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="ICollection{T}"/>
public static class ICollectionExtensions
{
  /// <param name="collection"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(ICollection<T> collection)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="collection"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
    public ICollection<T> AddIfAbsent(IEnumerable<T> elements) => collection.With(elements, x => !collection.Contains(x));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
    public ICollection<T> AddIfAbsent(params T[] elements) => AddIfAbsent(collection, elements as IEnumerable<T>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
    public ICollection<T> Empty()
    {
      if (collection is null) throw new ArgumentNullException(nameof(collection));

      collection.Clear();

      return collection;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="collection"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public ICollection<T> TryFinallyClear(Action<ICollection<T>> action)
    {
      if (collection is null) throw new ArgumentNullException(nameof(collection));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return collection.TryFinally(action, x => x.Clear());
    }

    /// <summary>
    ///   <para>Sequentially adds all elements, returned by the enumerator, to the specified collection.</para>
    /// </summary>
    /// <param name="elements">Elements enumerator that provide elements for addition to the collection <paramref name="collection"/>.</param>
    /// <param name="predicate"></param>
    /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="collection"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With{T}(ICollection{T}, T[])"/>
    public ICollection<T> With(IEnumerable<T> elements, Predicate<T> predicate = null)
    {
      if (collection is null) throw new ArgumentNullException(nameof(collection));
      if (elements is null) throw new ArgumentNullException(nameof(elements));

      foreach (var element in elements)
      {
        if (predicate is null || predicate(element))
        {
          collection.Add(element);
        }
      }

      return collection;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With{T}(ICollection{T}, IEnumerable{T}, Predicate{T})"/>
    public ICollection<T> With(params T[] elements) => collection.With(elements as IEnumerable<T>);

    /// <summary>
    ///   <para>Sequentially removes all elements, returned by the enumerator, from the specified collection, if it has it.</para>
    /// </summary>
    /// <param name="elements">Elements enumerator that provider elements for removal from the collection <paramref name="collection"/>.</param>
    /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
    /// <seealso cref="ICollection{T}.Remove(T)"/>
    /// <exception cref="ArgumentNullException">If either <paramref name="collection"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without{T}(ICollection{T}, T[])"/>
    public ICollection<T> Without(IEnumerable<T> elements)
    {
      if (collection is null) throw new ArgumentNullException(nameof(collection));
      if (elements is null) throw new ArgumentNullException(nameof(elements));

      foreach (var element in elements)
      {
        collection.Remove(element);
      }

      return collection;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without{T}(ICollection{T}, IEnumerable{T})"/>
    public ICollection<T> Without(params T[] elements) => collection.Without(elements as IEnumerable<T>);
  }
}