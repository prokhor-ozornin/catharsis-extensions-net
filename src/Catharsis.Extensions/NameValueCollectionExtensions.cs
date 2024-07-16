using System.Collections.Specialized;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="NameValueCollection"/>
public static class NameValueCollectionExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="NameValueCollection"/> that contains the same elements as the original.</para>
  /// </summary>
  /// <param name="collection">Collection to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
  public static NameValueCollection Clone(this NameValueCollection collection) => collection is not null ? new NameValueCollection(collection) : throw new ArgumentNullException(nameof(collection));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <param name="elements"></param>
  /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="collection"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(NameValueCollection, ValueTuple{string, object}[])"/>
  public static NameValueCollection With(this NameValueCollection collection, IEnumerable<(string Name, object Value)> elements)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));
    if (elements is null) throw new ArgumentNullException(nameof(elements));

    foreach (var element in elements)
    {
      collection.Add(element.Name, element.Value?.ToInvariantString());
    }

    return collection;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <param name="elements"></param>
  /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(NameValueCollection, IEnumerable{ValueTuple{string, object}})"/>
  public static NameValueCollection With(this NameValueCollection collection, params (string Name, object Value)[] elements) => collection.With(elements as IEnumerable<(string Name, object Value)>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <param name="elements"></param>
  /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="collection"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(NameValueCollection, string[])"/>
  public static NameValueCollection Without(this NameValueCollection collection, IEnumerable<string> elements)
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
  /// <param name="collection"></param>
  /// <param name="elements"></param>
  /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(NameValueCollection, IEnumerable{string})"/>
  public static NameValueCollection Without(this NameValueCollection collection, params string[] elements) => collection.Without(elements as IEnumerable<string>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection">Collection to be cleared.</param>
  /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
  public static NameValueCollection Empty(this NameValueCollection collection)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));

    collection.Clear();
    
    return collection;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="collection"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="collection"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static NameValueCollection TryFinallyClear(this NameValueCollection collection, Action<NameValueCollection> action)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return collection.TryFinally(action, x => x.Clear());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
  public static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));

    var result = new Dictionary<string, string>();

    for (var i = 0; i < collection.Count; i++)
    {
      var key = collection.GetKey(i);

      if (key is not null)
      {
        result.Add(key, collection.Get(i));
      }
    }

    return result;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is <see langword="null"/>.</exception>
  public static IEnumerable<(string Name, string Value)> ToValueTuple(this NameValueCollection collection)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));

    for (var i = 0; i < collection.Count; i++)
    {
      var key = collection.GetKey(i);

      if (key is not null)
      {
        yield return (key, collection.Get(i));
      }
    }
  }
}