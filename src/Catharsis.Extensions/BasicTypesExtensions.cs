namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for numeric and math-related types.</para>
/// </summary>
/// <seealso cref="Math"/>
public static class BasicTypesExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <returns></returns>
  /// <seealso cref="Math.Abs(sbyte)"/>
  public static short Abs(this sbyte number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of 16-bit signed integer.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Abs(short)"/>
  public static short Abs(this short number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of 32-bit signed integer.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Abs(int)"/>
  public static int Abs(this int number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of 64-bit signed integer.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Abs(long)"/>
  public static long Abs(this long number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of single-precision floating-point number.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Abs(float)"/>
  public static float Abs(this float number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of double-precision floating-point number.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Abs(double)"/>
  public static double Abs(this double number) => Math.Abs(number);

  /// <summary>
  ///   <para>Returns the absolute value of a <see cref="decimal"/> number.</para>
  /// </summary>
  /// <param name="number">Source number.</param>
  /// <returns>Absolute value of <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Abs(decimal)"/>
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
  public static decimal Ceil(this decimal number) => Math.Ceiling(number);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <returns></returns>
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
  public static decimal Floor(this decimal number) => Math.Floor(number);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <param name="digits"></param>
  /// <returns></returns>
  /// <seealso cref="Math.Round(double)"/>
  /// <seealso cref="Round(double, int?)"/>
  /// <seealso cref="Round(decimal, int?)"/>
  public static float Round(this float number, int? digits = null) => (float) (digits is not null ? Math.Round(number, digits.Value) : Math.Round(number));

  /// <summary>
  ///   <para>Rounds a double-precision floating-point value to the nearest integral value.</para>
  /// </summary>
  /// <param name="number">A double-precision floating-point number to be rounded.</param>
  /// <param name="digits"></param>
  /// <returns>The integer nearest <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Round(double)"/>
  /// <seealso cref="Round(float, int?)"/>
  /// <seealso cref="Round(decimal, int?)"/>
  public static double Round(this double number, int? digits = null) => digits is not null ? Math.Round(number, digits.Value) : Math.Round(number);

  /// <summary>
  ///   <para>Rounds a decimal value to the nearest integral value.</para>
  /// </summary>
  /// <param name="number">A decimal number to be rounded.</param>
  /// <param name="digits"></param>
  /// <returns>The integer nearest <paramref name="number"/>.</returns>
  /// <seealso cref="Math.Round(decimal)"/>
  /// <seealso cref="Round(float, int?)"/>
  /// <seealso cref="Round(double, int?)"/>
  public static decimal Round(this decimal number, int? digits = null) => digits is not null ? Math.Round(number, digits.Value) : Math.Round(number);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <param name="power"></param>
  /// <returns></returns>
  /// <seealso cref="Math.Pow(double, double)"/>
  /// <seealso cref="Power(double, double)"/>
  /// <seealso cref="Power(decimal, decimal)"/>
  public static float Power(this float number, float power) => (float) Math.Pow(number, power);

  /// <summary>
  ///   <para>Returns a specified number raised to the specified power.</para>
  /// </summary>
  /// <param name="number">A double-precision floating-point number to be raised to a power.</param>
  /// <param name="power">A double-precision floating-point number that specifies a power.</param>
  /// <returns>The number <paramref name="number"/> raised to the power <paramref name="power"/>.</returns>
  /// <seealso cref="Math.Pow(double, double)"/>
  /// <seealso cref="Power(float, float)"/>
  /// <seealso cref="Power(decimal, decimal)"/>
  public static double Power(this double number, double power) => Math.Pow(number, power);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="number"></param>
  /// <param name="power"></param>
  /// <returns></returns>
  /// <seealso cref="Power(float, float)"/>
  /// <seealso cref="Power(double, double)"/>
  public static decimal Power(this decimal number, decimal power) => (decimal) Math.Pow((double) number, (double) power);
  
  /// <summary>
  ///   <para>Returns a string created by repeating a specified character given number of times.</para>
  /// </summary>
  /// <param name="character">Character to repeat.</param>
  /// <param name="count">Number of repeats.</param>
  /// <returns>Result string.</returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static string Repeat(this char character, int count) => count >= 0 ? new string(character, count) : throw new ArgumentOutOfRangeException(nameof(count));

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
  /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Times(int, Action{int})"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Times(int, Action)"/>
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
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<object> Nulls(this int count) => count.Objects<object>(() => null);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Objects{T}(int, Func{T})"/>
  /// <seealso cref="Objects{T}(int, Func{int, T})"/>
  public static IEnumerable<T> Objects<T>(this int count) where T : new() => count >= 0 ? count.Objects(() => new T()) : throw new ArgumentOutOfRangeException(nameof(count));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="count"></param>
  /// <param name="constructor"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="constructor"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Objects{T}(int)"/>
  /// <seealso cref="Objects{T}(int, Func{int, T})"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="constructor"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Objects{T}(int)"/>
  /// <seealso cref="Objects{T}(int, Func{T})"/>
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
  ///   <para>Creates a time span object, representing a given number of days.</para>
  /// </summary>
  /// <param name="count">Number of days.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Days(this int count) => new(count, 0, 0, 0);

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of hours.</para>
  /// </summary>
  /// <param name="count">Number of hours.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Hours(this int count) => new(count, 0, 0);

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of minutes.</para>
  /// </summary>
  /// <param name="count">Number of minutes.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Minutes(this int count) => new(0, count, 0);

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of seconds.</para>
  /// </summary>
  /// <param name="count">Number of seconds.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Seconds(this int count) => new(0, 0, count);

  /// <summary>
  ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
  /// </summary>
  /// <param name="count">Number of milliseconds.</param>
  /// <returns>Time span instance.</returns>
  public static TimeSpan Milliseconds(this int count) => new(0, 0, 0, 0, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="count"></param>
  /// <returns></returns>
  public static TimeSpan Ticks(this long count) => new(count);
}