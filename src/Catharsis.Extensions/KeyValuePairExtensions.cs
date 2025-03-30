namespace Catharsis.Extensions;

/// <summary>
///   <para></para>
/// </summary>
/// <seealso cref="KeyValuePair{TKey, TValue}"/>
public static class KeyValuePairExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="pair"></param>
  /// <returns></returns>
  public static (TKey Key, TValue Value) ToValueTuple<TKey, TValue>(this KeyValuePair<TKey, TValue> pair) => (pair.Key, pair.Value);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  /// <param name="pair"></param>
  /// <returns></returns>
  public static Tuple<TKey, TValue> ToTuple<TKey, TValue>(this KeyValuePair<TKey, TValue> pair) => new(pair.Key, pair.Value);
}