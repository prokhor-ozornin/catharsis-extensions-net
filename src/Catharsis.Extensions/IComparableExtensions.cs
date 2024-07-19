namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for numeric and math-related types.</para>
/// </summary>
/// <seealso cref="IComparable{T}"/>
public static class IComparableExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="comparable"></param>
  /// <returns></returns>
  /// <seealso cref="IsNegative{T}(T)"/>
  public static bool IsPositive<T>(this T comparable) where T : struct, IComparable<T> => comparable.CompareTo(default) > 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="comparable"></param>
  /// <returns></returns>
  /// <seealso cref="IsPositive{T}(T)"/>
  public static bool IsNegative<T>(this T comparable) where T : struct, IComparable<T> => comparable.CompareTo(default) < 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="comparable"></param>
  /// <returns></returns>
  public static bool IsDefault<T>(this T comparable) where T : struct, IComparable<T> => comparable.CompareTo(default) == 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="left"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Max{T}(T, T)"/>
  /// <seealso cref="MinMax{T}(T, T)"/>
  public static T Min<T>(this T left, T right) where T : IComparable => left is not null ? left.CompareTo(right) <= 0 ? left : right : throw new ArgumentNullException(nameof(left));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="left"/> is <see langword="null"/>.</exception>
  /// <seealso cref=" Min{T}(T, T)"/>
  /// <seealso cref="MinMax{T}(T, T)"/>
  public static T Max<T>(this T left, T right) where T : IComparable => left is not null ? left.CompareTo(right) > 0 ? left : right : throw new ArgumentNullException(nameof(left));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="left"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Min{T}(T, T)"/>
  /// <seealso cref="Max{T}(T, T)"/>
  public static (T Min, T Max) MinMax<T>(this T left, T right) where T : IComparable => left is not null ? left.CompareTo(right) <= 0 ? (left, right) : (right, left) : throw new ArgumentNullException(nameof(left));
}