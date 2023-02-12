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
  /// <param name="to">Collection to which elements are added.</param>
  /// <param name="from">Elements enumerator that provide elements for addition to the collection <paramref name="to"/>.</param>
  /// <returns>Reference to the supplied collection <paramref name="to"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> AddRange<T>(this ICollection<T> to, IEnumerable<T> from)
  {
    if (to is null) throw new ArgumentNullException(nameof(to));
    if (from is null) throw new ArgumentNullException(nameof(from));

    from.ForEach(to.Add);
    
    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> AddRange<T>(this ICollection<T> to, params T[] from) => to.AddRange(from as IEnumerable<T>);

  /// <summary>
  ///   <para>Sequentially removes all elements, returned by the enumerator, from the specified collection, if it has it.</para>
  /// </summary>
  /// <typeparam name="T">Type of collection's elements.</typeparam>
  /// <param name="from">Collection from which elements are removed.</param>
  /// <param name="sequence">Elements enumerator that provider elements for removal from the collection <see cref="from"/>.</param>
  /// <seealso cref="ICollection{T}.Remove(T)"/>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> RemoveRange<T>(this ICollection<T> from, IEnumerable<T> sequence)
  {
    if (from is null) throw new ArgumentNullException(nameof(from));
    if (sequence is null) throw new ArgumentNullException(nameof(sequence));

    sequence.ForEach(element => from.Remove(element));
    
    return from;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="from"></param>
  /// <param name="sequence"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentNullException"></exception>
  public static ICollection<T> RemoveRange<T>(this ICollection<T> from, params T[] sequence) => from.RemoveRange(sequence as IEnumerable<T>);

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

    return collection.TryFinally(action, collection => collection.Clear());
  }
}