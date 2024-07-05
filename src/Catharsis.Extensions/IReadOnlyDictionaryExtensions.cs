namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="IReadOnlyDictionary{TKey, TValue}"/>
public static class IReadOnlyDictionaryExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="dictionary"></param>
  /// <param name="comparer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
  public static IEnumerable<(TKey Key, TValue Value)> ToValueTuple<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer = null) where TKey : notnull
  {
    if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

    return comparer is not null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => (pair.Key, pair.Value)) : dictionary.Select(pair => (pair.Key, pair.Value));
  }
}