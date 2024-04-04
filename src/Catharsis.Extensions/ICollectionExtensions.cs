namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="ICollection{T}"/>
public static class ICollectionExtensions
{
  /// <summary>
  ///   <para>Sequentially adds all elements, returned by the enumerator, to the specified collection.</para>
  /// </summary>
  /// <typeparam name="T">Type of collection's elements.</typeparam>
  /// <param name="collection">Collection to which elements are added.</param>
  /// <param name="elements">Elements enumerator that provide elements for addition to the collection <paramref name="collection"/>.</param>
  /// <returns>Reference to the supplied collection <paramref name="collection"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> With<T>(this ICollection<T> collection, IEnumerable<T> elements)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));
    if (elements is null) throw new ArgumentNullException(nameof(elements));

    foreach (var element in elements)
    {
      collection.Add(element);
    }
    
    return collection;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="collection"></param>
  /// <param name="elements"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> With<T>(this ICollection<T> collection, params T[] elements) => collection.With(elements as IEnumerable<T>);

  /// <summary>
  ///   <para>Sequentially removes all elements, returned by the enumerator, from the specified collection, if it has it.</para>
  /// </summary>
  /// <typeparam name="T">Type of collection's elements.</typeparam>
  /// <param name="collection">Collection from which elements are removed.</param>
  /// <param name="elements">Elements enumerator that provider elements for removal from the collection <see cref="collection"/>.</param>
  /// <seealso cref="ICollection{T}.Remove(T)"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> Without<T>(this ICollection<T> collection, IEnumerable<T> elements)
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
  /// <typeparam name="T"></typeparam>
  /// <param name="collection"></param>
  /// <param name="elements"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> Without<T>(this ICollection<T> collection, params T[] elements) => collection.Without(elements as IEnumerable<T>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> Empty<T>(this ICollection<T> collection)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));

    collection.Clear();

    return collection;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="collection"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> TryFinallyClear<T>(this ICollection<T> collection, Action<ICollection<T>> action)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return collection.TryFinally(action, x => x.Clear());
  }
}