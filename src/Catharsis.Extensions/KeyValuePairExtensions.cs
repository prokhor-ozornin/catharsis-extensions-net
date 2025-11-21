namespace Catharsis.Extensions;

/// <summary>
///   <para></para>
/// </summary>
/// <seealso cref="KeyValuePair{TKey, TValue}"/>
public static class KeyValuePairExtensions
{
  /// <param name="pair"></param>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  extension<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public (TKey Key, TValue Value) ToValueTuple() => (pair.Key, pair.Value);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public Tuple<TKey, TValue> ToTuple() => new(pair.Key, pair.Value);
  }
}