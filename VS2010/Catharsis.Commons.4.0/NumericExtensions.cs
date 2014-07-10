using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for primitive numeric types.</para>
  /// </summary>
  public static class NumericExtensions
  {
    /// <summary>
    ///   <para>Performs a decremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="from">Upper bound value to start iteration from. It must be greater or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Lower bound value to end iteration on. It must be lower or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DownTo(short, short, Action)"/>
    /// <seealso cref="DownTo(int, int, Action)"/>
    /// <seealso cref="DownTo(long, long, Action)"/>
    public static void DownTo(this byte from, byte to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs a decremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="from">Upper bound value to start iteration from. It must be greater or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Lower bound value to end iteration on. It must be lower or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DownTo(byte, byte, Action)"/>
    /// <seealso cref="DownTo(int, int, Action)"/>
    /// <seealso cref="DownTo(long, long, Action)"/>
    public static void DownTo(this short from, short to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs a decremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="from">Upper bound value to start iteration from. It must be greater or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Lower bound value to end iteration on. It must be lower or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DownTo(byte, byte, Action)"/>
    /// <seealso cref="DownTo(short, short, Action)"/>
    /// <seealso cref="DownTo(long, long, Action)"/>
    public static void DownTo(this int from, int to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs a decremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="from">Upper bound value to start iteration from. It must be greater or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Lower bound value to end iteration on. It must be lower or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DownTo(byte, byte, Action)"/>
    /// <seealso cref="DownTo(short, short, Action)"/>
    /// <seealso cref="DownTo(int, int, Action)"/>
    public static void DownTo(this long from, long to, Action action)
    {
      Assertion.NotNull(action);

      for (var value = from; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="count">Number of times to call a delegate.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Times(short, Action)"/>
    /// <seealso cref="Times(int, Action)"/>
    /// <seealso cref="Times(long, Action)"/>
    public static void Times(this byte count, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < count; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="count">Number of times to call a delegate.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Times(byte, Action)"/>
    /// <seealso cref="Times(int, Action)"/>
    /// <seealso cref="Times(long, Action)"/>
    public static void Times(this short count, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < count; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="count">Number of times to call a delegate.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Times(byte, Action)"/>
    /// <seealso cref="Times(short, Action)"/>
    /// <seealso cref="Times(long, Action)"/>
    public static void Times(this int count, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < count; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="count">Number of times to call a delegate.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Times(byte, Action)"/>
    /// <seealso cref="Times(short, Action)"/>
    /// <seealso cref="Times(int, Action)"/>
    public static void Times(this long count, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < count; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs an incremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="from">Lower bound value to start iteration from. It must be lower or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Upper bound value to end iteration on. It must be greater or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="UpTo(short, short, Action)"/>
    /// <seealso cref="UpTo(int, int, Action)"/>
    /// <seealso cref="UpTo(long, long, Action)"/>
    public static void UpTo(this byte from, byte to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs an incremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="from">Lower bound value to start iteration from. It must be lower or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Upper bound value to end iteration on. It must be greater or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="UpTo(byte, byte, Action)"/>
    /// <seealso cref="UpTo(int, int, Action)"/>
    /// <seealso cref="UpTo(long, long, Action)"/>
    public static void UpTo(this short from, short to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs an incremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="from">Lower bound value to start iteration from. It must be lower or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Upper bound value to end iteration on. It must be greater or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="UpTo(byte, byte, Action)"/>
    /// <seealso cref="UpTo(short, short, Action)"/>
    /// <seealso cref="UpTo(long, long, Action)"/>
    public static void UpTo(this int from, int to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs an incremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="from">Lower bound value to start iteration from. It must be lower or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Upper bound value to end iteration on. It must be greater or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="UpTo(byte, byte, Action)"/>
    /// <seealso cref="UpTo(short, short, Action)"/>
    /// <seealso cref="UpTo(int, int, Action)"/>
    public static void UpTo(this long from, long to, Action action)
    {
      Assertion.NotNull(action);

      for (var value = from; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of days.</para>
    /// </summary>
    /// <param name="count">Number of days.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Days(this byte count)
    {
      return new TimeSpan(count, 0, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of days.</para>
    /// </summary>
    /// <param name="count">Number of days.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Days(this short count)
    {
      return new TimeSpan(count, 0, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of days.</para>
    /// </summary>
    /// <param name="count">Number of days.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Days(this int count)
    {
      return new TimeSpan(count, 0, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of hours.</para>
    /// </summary>
    /// <param name="count">Number of hours.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Hours(this byte count)
    {
      return new TimeSpan(count, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of hours.</para>
    /// </summary>
    /// <param name="count">Number of hours.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Hours(this short count)
    {
      return new TimeSpan(count, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of hours.</para>
    /// </summary>
    /// <param name="count">Number of hours.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Hours(this int count)
    {
      return new TimeSpan(count, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of minutes.</para>
    /// </summary>
    /// <param name="count">Number of minutes.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Minutes(this byte count)
    {
      return new TimeSpan(0, count, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of minutes.</para>
    /// </summary>
    /// <param name="count">Number of minutes.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Minutes(this short count)
    {
      return new TimeSpan(0, count, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of minutes.</para>
    /// </summary>
    /// <param name="count">Number of minutes.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Minutes(this int count)
    {
      return new TimeSpan(0, count, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of seconds.</para>
    /// </summary>
    /// <param name="count">Number of seconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Seconds(this byte count)
    {
      return new TimeSpan(0, 0, count);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of seconds.</para>
    /// </summary>
    /// <param name="count">Number of seconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Seconds(this short count)
    {
      return new TimeSpan(0, 0, count);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of seconds.</para>
    /// </summary>
    /// <param name="count">Number of seconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Seconds(this int count)
    {
      return new TimeSpan(0, 0, count);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
    /// </summary>
    /// <param name="count">Number of milliseconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Milliseconds(this byte count)
    {
      return new TimeSpan(0, 0, 0, 0, count);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
    /// </summary>
    /// <param name="count">Number of milliseconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Milliseconds(this short count)
    {
      return new TimeSpan(0, 0, 0, 0, count);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
    /// </summary>
    /// <param name="count">Number of milliseconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Milliseconds(this int count)
    {
      return new TimeSpan(0, 0, 0, 0, count);
    }

    /// <summary>
    ///   <para>Returns the smallest integer greater than or equal to the specified number.</para>
    /// </summary>
    /// <param name="value">A double-precision floating-point number.</param>
    /// <returns>The smallest integral value that is greater than or equal to <paramref name="value"/>.</returns>
    /// <seealso cref="Math.Ceiling(double)"/>
    public static double Ceil(this double value)
    {
      return Math.Ceiling(value);
    }

    /// <summary>
    ///   <para>Returns the largest integer less than or equal to the specified number.</para>
    /// </summary>
    /// <param name="value">A double-precision floating-point number.</param>
    /// <returns>The largest integer less than or equal to <paramref name="value"/>.</returns>
    /// <seealso cref="Math.Floor(double)"/>
    public static double Floor(this double value)
    {
      return Math.Floor(value);
    }

    /// <summary>
    ///   <para>Returns a specified number raised to the specified power.</para>
    /// </summary>
    /// <param name="value">A double-precision floating-point number to be raised to a power.</param>
    /// <param name="power">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The number <paramref name="value"/> raised to the power <paramref name="power"/>.</returns>
    /// <seealso cref="Math.Pow(double, double)"/>
    public static double Power(this double value, double power)
    {
      return Math.Pow(value, power);
    }

    /// <summary>
    ///   <para>Rounds a double-precision floating-point value to the nearest integral value.</para>
    /// </summary>
    /// <param name="value">A double-precision floating-point number to be rounded.</param>
    /// <returns>The integer nearest <paramref name="value"/>.</returns>
    /// <seealso cref="Math.Round(double)"/>
    public static double Round(this double value)
    {
      return Math.Round(value);
    }

    /// <summary>
    ///   <para>Rounds a decimal value to the nearest integral value.</para>
    /// </summary>
    /// <param name="value">A decimal number to be rounded.</param>
    /// <returns>The integer nearest <paramref name="value"/>.</returns>
    /// <seealso cref="Math.Round(decimal)"/>
    public static decimal Round(this decimal value)
    {
      return Math.Round(value);
    }

    /// <summary>
    ///   <para>Returns the square root of a specified number.</para>
    /// </summary>
    /// <param name="value">Source number.</param>
    /// <returns>Square root of <paramref name="value"/>.</returns>
    /// <seealso cref="Math.Sqrt(double)"/>
    public static double Sqrt(this double value)
    {
      return Math.Sqrt(value);
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="value">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this byte value)
    {
      return value % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="value">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this short value)
    {
      return value % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="value">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this int value)
    {
      return value % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="value">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this long value)
    {
      return value % 2 == 0;
    }
  }
}