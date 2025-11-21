namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="IReadOnlyDictionary{TKey, TValue}"/>
public static class IReadOnlyDictionaryExtensions
{
  /// <param name="dictionary"></param>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  extension<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary) where TKey : notnull
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    public IEnumerable<(TKey Key, TValue Value)> ToValueTuple(IComparer<TKey> comparer = null)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

      return comparer is not null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => pair.ToValueTuple()) : dictionary.Select(pair => pair.ToValueTuple());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="comparer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is <see langword="null"/>.</exception>
    public IEnumerable<Tuple<TKey, TValue>> ToTuple(IComparer<TKey> comparer = null)
    {
      if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));

      return comparer is not null ? dictionary.OrderBy(pair => pair.Key, comparer).Select(pair => pair.ToTuple()) : dictionary.Select(pair => pair.ToTuple());
    }
  }
}