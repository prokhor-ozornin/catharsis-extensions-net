namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for numeric and math-related types.</para>
/// </summary>
/// <seealso cref="Math"/>
public static class NumericExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  public static IEnumerable<int> To(this int from, int to) => from <= to ? Enumerable.Range(from, to - from) : Enumerable.Range(to, from - to);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="count"></param>
  /// <param name="action"></param>
  public static void Times(this int count, Action action)
  {
    if (action is null) throw new ArgumentNullException(nameof(action));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    count.Times(_ => action());
  }

  /// <summary>
  ///   <para>Calls given delegate specified number of times.</para>
  /// </summary>
  /// <param name="count">Number of times to call a delegate.</param>
  /// <param name="action">Delegate that represents a method to be called.</param>
  public static void Times(this int count, Action<int> action)
  {
    if (action is null) throw new ArgumentNullException(nameof(action));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    for (var value = 0; value < count; value++)
    {
      action(value);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static (T Min, T Max) MinMax<T>(this T left, T right) where T : IComparable => left.CompareTo(right) <= 0 ? (left, right) : (right, left);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<object> Nulls(this int count)
  {
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects<object>(() => null);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<T> Objects<T>(this int count) where T : new()
  {
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => new T());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="count"></param>
  /// <param name="constructor"></param>
  /// <returns></returns>
  public static IEnumerable<T> Objects<T>(this int count, Func<T> constructor)
  {
    if (constructor is null) throw new ArgumentNullException(nameof(constructor));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    for (var i = 1; i <= count; i++)
    {
      yield return constructor();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="count"></param>
  /// <param name="constructor"></param>
  /// <returns></returns>
  public static IEnumerable<T> Objects<T>(this int count, Func<int, T> constructor)
  {
    if (constructor is null) throw new ArgumentNullException(nameof(constructor));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    for (var i = 0; i < count; i++)
    {
      yield return constructor(i);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <param name="digits"></param>
  /// <returns></returns>
  public static float Round(this float number, int? digits = null) => (float) (digits != null ? Math.Round(number, digits.Value) : Math.Round(number));

  /// <summary>
  ///   <para>Rounds a double-precision floating-point value to the nearest integral value.</para>
  /// </summary>
  /// <param name="number">A double-precision floating-point number to be rounded.</param>
  /// <param name="digits"></param>
  /// <returns>The integer nearest <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Round(double)"/>
  public static double Round(this double number, int? digits = null) => digits != null ? Math.Round(number, digits.Value) : Math.Round(number);

  /// <summary>
  ///   <para>Rounds a decimal value to the nearest integral value.</para>
  /// </summary>
  /// <param name="number">A decimal number to be rounded.</param>
  /// <param name="digits"></param>
  /// <returns>The integer nearest <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Round(decimal)"/>
  public static decimal Round(this decimal number, int? digits = null) => digits != null ? Math.Round(number, digits.Value) : Math.Round(number);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <param name="power"></param>
  /// <returns></returns>
  /// <seealso cref="Math.Pow(double, double)"/>
  public static float Power(this float number, float power) => (float) Math.Pow(number, power);

  /// <summary>
  ///   <para>Returns a specified number raised to the specified power.</para>
  /// </summary>
  /// <param name="number">A double-precision floating-point number to be raised to a power.</param>
  /// <param name="power">A double-precision floating-point number that specifies a power.</param>
  /// <returns>The number <paramref name="number"/> raised to the power <paramref name="power"/>.</returns>
  /// <seealso cref="Math.Pow(double, double)"/>
  public static double Power(this double number, double power) => Math.Pow(number, power);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <param name="power"></param>
  /// <returns></returns>
  public static decimal Power(this decimal number, decimal power) => (decimal) Math.Pow((double) number, (double) power);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <returns></returns>
  public static short Abs(this sbyte number) => Math.Abs(number);
  
  /// <summary>
  ///   <para>Returns the absolute value of 16-bit signed integer.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  public static short Abs(this short number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of 32-bit signed integer.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  public static int Abs(this int number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of 64-bit signed integer.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  public static long Abs(this long number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of single-precision floating-point number.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  public static float Abs(this float number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of double-precision floating-point number.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  public static double Abs(this double number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of a <see cref="decimal"/> number.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  public static decimal Abs(this decimal number) => Math.Abs(number);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <returns></returns>
  public static float Ceil(this float number) => (float) Math.Ceiling(number);

  /// <summary>
  ///   <para>Returns the smallest integer greater than or equal to the specified number.</para>
  /// </summary>
  /// <param name="number">A double-precision floating-point number.</param>
  /// <returns>The smallest integral value that is greater than or equal to <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Ceiling(double)"/>
  public static double Ceil(this double number) => Math.Ceiling(number);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <returns></returns>
  /// <seealso cref="Math.Ceiling(double)"/>
  public static decimal Ceil(this decimal number) => Math.Ceiling(number);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <returns></returns>
  /// <seealso cref="Math.Floor(double)"/>
  public static float Floor(this float number) => (float) Math.Floor(number);

  /// <summary>
  ///   <para>Returns the largest integer less than or equal to the specified number.</para>
  /// </summary>
  /// <param name="number">A double-precision floating-point number.</param>
  /// <returns>The largest integer less than or equal to <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Floor(double)"/>
  public static double Floor(this double number) => Math.Floor(number);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <returns></returns>
  /// <seealso cref="Math.Floor(double)"/>
  public static decimal Floor(this decimal number) => Math.Floor(number);
}