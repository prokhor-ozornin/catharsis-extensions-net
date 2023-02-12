using System.Collections.Specialized;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="NameValueCollection"/>
public static class NameValueCollectionExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static NameValueCollection AddRange(this NameValueCollection to, IEnumerable<(string Name, object Value)> from)
  {
    if (to is null) throw new ArgumentNullException(nameof(to));
    if (from is null) throw new ArgumentNullException(nameof(from));

    from.ForEach(tuple => to.Add(tuple.Name, tuple.Value?.ToInvariantString()));
    
    return to;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="to"></param>
  /// <param name="from"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static NameValueCollection AddRange(this NameValueCollection to, params (string Name, object Value)[] from) => to.AddRange(from as IEnumerable<(string Name, object Value)>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static NameValueCollection TryFinallyClear(this NameValueCollection collection, Action<NameValueCollection> action)
  {
    if (collection is null) throw new ArgumentNullException(nameof(collection));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return collection.TryFinally(action, collection => collection.Clear());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="collection"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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