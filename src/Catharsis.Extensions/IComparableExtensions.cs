namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for numeric and math-related types.</para>
/// </summary>
/// <seealso cref="IComparable{T}"/>
public static class IComparableExtensions
{
  /// <param name="left"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(T left) where T : IComparable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="left"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Max{T}(T, T)"/>
    /// <seealso cref="MinMax{T}(T, T)"/>
    public T Min(T right) => left is not null ? left.CompareTo(right) <= 0 ? left : right : throw new ArgumentNullException(nameof(left));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="left"/> is <see langword="null"/>.</exception>
    /// <seealso cref=" Min{T}(T, T)"/>
    /// <seealso cref="MinMax{T}(T, T)"/>
    public T Max(T right) => left is not null ? left.CompareTo(right) > 0 ? left : right : throw new ArgumentNullException(nameof(left));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="left"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min{T}(T, T)"/>
    /// <seealso cref="Max{T}(T, T)"/>
    public (T Min, T Max) MinMax(T right) => left is not null ? left.CompareTo(right) <= 0 ? (left, right) : (right, left) : throw new ArgumentNullException(nameof(left));
  }

  /// <param name="comparable"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(T comparable) where T : IComparable<T>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsNegative{T}(T)"/>
    public bool IsPositive => comparable.CompareTo(default) > 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsPositive{T}(T)"/>
    public bool IsNegative => comparable.CompareTo(default) < 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    public bool IsDefault => comparable.CompareTo(default) == 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    public bool IsLesser(T right) => comparable.CompareTo(right) < 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    public bool IsLesserOrEqual(T right) => comparable.CompareTo(right) <= 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    public bool IsGreater(T right) => comparable.CompareTo(right) > 0;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    public bool IsGreaterOrEqual(T right) => comparable.CompareTo(right) >= 0;
  }
}