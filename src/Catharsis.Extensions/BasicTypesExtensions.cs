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
  /// <param name="value"></param>
  /// <returns></returns>
  public static bool ToBoolean(this byte value) => value > 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  [CLSCompliant(false)]
  public static bool ToBoolean(this ushort value) => value > 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  [CLSCompliant(false)]
  public static bool ToBoolean(this uint value) => value > 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  [CLSCompliant(false)]
  public static bool ToBoolean(this ulong value) => value > 0;

  /// <param name="character">Character to repeat.</param>
  extension(char character)
  {
    /// <summary>
    ///   <para>Returns a string created by repeating a specified character given number of times.</para>
    /// </summary>
    /// <param name="count">Number of repeats.</param>
    /// <returns>Result string.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public string Repeat(int count) => count >= 0 ? new string(character, count) : throw new ArgumentOutOfRangeException(nameof(count));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => character != char.MinValue;
  }

  /// <param name="number"></param>
  extension(sbyte number)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <seealso cref="Math.Abs(sbyte)"/>
    [CLSCompliant(false)]
    public short Abs() => Math.Abs(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    [CLSCompliant(false)]
    public bool ToBoolean() => number > 0;
  }

  /// <param name="number">Source number.</param>
  extension(short number)
  {
    /// <summary>
    ///   <para>Returns the absolute value of 16-bit signed integer.</para>
    /// </summary>
    /// <returns>Absolute value of <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Abs(short)"/>
    public short Abs() => Math.Abs(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => number > 0;
  }

  /// <param name="number">Source number.</param>
  extension(int number)
  {
    /// <summary>
    ///   <para>Creates a time span object, representing a given number of days.</para>
    /// </summary>
    /// <value>Time span instance.</value>
    public TimeSpan Days => new(number, 0, 0, 0);

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of hours.</para>
    /// </summary>
    /// <value>Time span instance.</value>
    public TimeSpan Hours => new(number, 0, 0);

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of minutes.</para>
    /// </summary>
    /// <value>Time span instance.</value>
    public TimeSpan Minutes => new(0, number, 0);

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of seconds.</para>
    /// </summary>
    /// <value>Time span instance.</value>
    public TimeSpan Seconds => new(0, 0, number);

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
    /// </summary>
    /// <value>Time span instance.</value>
    public TimeSpan Milliseconds => new(0, 0, 0, 0, number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IEnumerable<object> Nulls => number.Objects<object>(() => null);

    /// <summary>
    ///   <para>Returns the absolute value of 32-bit signed integer.</para>
    /// </summary>
    /// <returns>Absolute value of <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Abs(int)"/>
    public int Abs() => Math.Abs(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    public IEnumerable<int> To(int to) => number <= to ? Enumerable.Range(number, to - number) : Enumerable.Range(to, number - to);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Times(int, Action{int})"/>
    public void Times(Action action)
    {
      if (action is null) throw new ArgumentNullException(nameof(action));
      if (number < 0) throw new ArgumentOutOfRangeException(nameof(number));

      number.Times(_ => action());
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Times(int, Action)"/>
    public void Times(Action<int> action)
    {
      if (action is null) throw new ArgumentNullException(nameof(action));
      if (number < 0) throw new ArgumentOutOfRangeException(nameof(number));

      for (var value = 0; value < number; value++)
      {
        action(value);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Objects{T}(int, Func{T})"/>
    /// <seealso cref="Objects{T}(int, Func{int, T})"/>
    public IEnumerable<T> Objects<T>() where T : new() => number >= 0 ? number.Objects(() => new T()) : throw new ArgumentOutOfRangeException(nameof(number));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="constructor"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="constructor"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Objects{T}(int)"/>
    /// <seealso cref="Objects{T}(int, Func{int, T})"/>
    public IEnumerable<T> Objects<T>(Func<T> constructor)
    {
      if (constructor is null) throw new ArgumentNullException(nameof(constructor));
      if (number < 0) throw new ArgumentOutOfRangeException(nameof(number));

      for (var i = 1; i <= number; i++)
      {
        yield return constructor();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="constructor"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="constructor"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Objects{T}(int)"/>
    /// <seealso cref="Objects{T}(int, Func{T})"/>
    public IEnumerable<T> Objects<T>(Func<int, T> constructor)
    {
      if (constructor is null) throw new ArgumentNullException(nameof(constructor));
      if (number < 0) throw new ArgumentOutOfRangeException(nameof(number));

      for (var i = 0; i < number; i++)
      {
        yield return constructor(i);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => number > 0;
  }

  /// <param name="number">Source number.</param>
  extension(long number)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    public TimeSpan Ticks => new(number);

    /// <summary>
    ///   <para>Returns the absolute value of 64-bit signed integer.</para>
    /// </summary>
    /// <returns>Absolute value of <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Abs(long)"/>
    public long Abs() => Math.Abs(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => number > 0;
  }

  /// <param name="number">Source number.</param>
  extension(float number)
  {
    /// <summary>
    ///   <para>Returns the absolute value of single-precision floating-point number.</para>
    /// </summary>
    /// <returns>Absolute value of <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Abs(float)"/>
    public float Abs() => Math.Abs(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public float Ceil() => (float) Math.Ceiling(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public float Floor() => (float) Math.Floor(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    /// <seealso cref="Math.Round(double)"/>
    /// <seealso cref="Round(double, int?)"/>
    /// <seealso cref="Round(decimal, int?)"/>
    public float Round(int? digits = null) => (float) (digits is not null ? Math.Round(number, digits.Value) : Math.Round(number));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="power"></param>
    /// <returns></returns>
    /// <seealso cref="Math.Pow(double, double)"/>
    /// <seealso cref="Power(double, double)"/>
    /// <seealso cref="Power(decimal, decimal)"/>
    public float Power(float power) => (float) Math.Pow(number, power);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => number > 0;
  }

  /// <param name="number">Source number.</param>
  extension(double number)
  {
    /// <summary>
    ///   <para>Returns the absolute value of double-precision floating-point number.</para>
    /// </summary>
    /// <returns>Absolute value of <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Abs(double)"/>
    public double Abs() => Math.Abs(number);

    /// <summary>
    ///   <para>Returns the smallest integer greater than or equal to the specified number.</para>
    /// </summary>
    /// <returns>The smallest integral value that is greater than or equal to <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Ceiling(double)"/>
    public double Ceil() => Math.Ceiling(number);

    /// <summary>
    ///   <para>Returns the largest integer less than or equal to the specified number.</para>
    /// </summary>
    /// <returns>The largest integer less than or equal to <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Floor(double)"/>
    public double Floor() => Math.Floor(number);

    /// <summary>
    ///   <para>Rounds a double-precision floating-point value to the nearest integral value.</para>
    /// </summary>
    /// <param name="digits"></param>
    /// <returns>The integer nearest <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Round(double)"/>
    /// <seealso cref="Round(float, int?)"/>
    /// <seealso cref="Round(decimal, int?)"/>
    public double Round(int? digits = null) => digits is not null ? Math.Round(number, digits.Value) : Math.Round(number);

    /// <summary>
    ///   <para>Returns a specified number raised to the specified power.</para>
    /// </summary>
    /// <param name="power">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The number <paramref name="number"/> raised to the power <paramref name="power"/>.</returns>
    /// <seealso cref="Math.Pow(double, double)"/>
    /// <seealso cref="Power(float, float)"/>
    /// <seealso cref="Power(decimal, decimal)"/>
    public double Power(double power) => Math.Pow(number, power);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => number > 0;
  }

  /// <param name="number">Source number.</param>
  extension(decimal number)
  {
    /// <summary>
    ///   <para>Returns the absolute value of a <see cref="decimal"/> number.</para>
    /// </summary>
    /// <returns>Absolute value of <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Abs(decimal)"/>
    public decimal Abs() => Math.Abs(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public decimal Ceil() => Math.Ceiling(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public decimal Floor() => Math.Floor(number);

    /// <summary>
    ///   <para>Rounds a decimal value to the nearest integral value.</para>
    /// </summary>
    /// <param name="digits"></param>
    /// <returns>The integer nearest <paramref name="number"/>.</returns>
    /// <seealso cref="Math.Round(decimal)"/>
    /// <seealso cref="Round(float, int?)"/>
    /// <seealso cref="Round(double, int?)"/>
    public decimal Round(int? digits = null) => digits is not null ? Math.Round(number, digits.Value) : Math.Round(number);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="power"></param>
    /// <returns></returns>
    /// <seealso cref="Power(float, float)"/>
    /// <seealso cref="Power(double, double)"/>
    public decimal Power(decimal power) => (decimal) Math.Pow((double) number, (double) power);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => number > 0;
  }
}