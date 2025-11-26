namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for numeric and math-related types.</para>
/// </summary>
/// <seealso cref="IComparable{T}"/>
public static class IComparableExtensions
{
  /// <param name="comparable"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(T comparable) where T : IComparable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="comparable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Max{T}(T, T)"/>
    /// <seealso cref="MinMax{T}(T, T)"/>
    public T Min(T other) => comparable is not null ? comparable.CompareTo(other) <= 0 ? comparable : other : throw new ArgumentNullException(nameof(comparable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="comparable"/> is <see langword="null"/>.</exception>
    /// <seealso cref=" Min{T}(T, T)"/>
    /// <seealso cref="MinMax{T}(T, T)"/>
    public T Max(T other) => comparable is not null ? comparable.CompareTo(other) > 0 ? comparable : other : throw new ArgumentNullException(nameof(comparable));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="comparable"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min{T}(T, T)"/>
    /// <seealso cref="Max{T}(T, T)"/>
    public (T Min, T Max) MinMax(T other) => comparable is not null ? comparable.CompareTo(other) <= 0 ? (comparable, other) : (other, comparable) : throw new ArgumentNullException(nameof(comparable));
  }

  /// <param name="comparable"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(T comparable) where T : IComparable<T>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    public bool IsDefault => comparable.CompareTo(default) == 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsNegative"/>
    public bool IsPositive => comparable.CompareTo(default) > 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsPositive"/>
    public bool IsNegative => comparable.CompareTo(default) < 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsLesser(T other) => comparable.CompareTo(other) < 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsLesserOrEqual(T other) => comparable.CompareTo(other) <= 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsGreater(T other) => comparable.CompareTo(other) > 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsGreaterOrEqual(T other) => comparable.CompareTo(other) >= 0;
  }
}