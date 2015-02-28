using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for primitive numeric types.</para>
  /// </summary>
  public static class NumericExtensions
  {
    /// <summary>
    ///   <para>Returns the absolute value of 16-bit signed integer.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static short Abs(this short self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of 32-bit signed integer.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static int Abs(this int self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of 64-bit signed integer.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static long Abs(this long self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of single-precision floating-point number.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static float Abs(this float self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of double-precision floating-point number.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static double Abs(this double self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of a <see cref="decimal"/> number.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static decimal Abs(this decimal self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the smallest integer greater than or equal to the specified number.</para>
    /// </summary>
    /// <param name="self">A double-precision floating-point number.</param>
    /// <returns>The smallest integral value that is greater than or equal to <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Ceiling(double)"/>
    public static double Ceil(this double self)
    {
      return Math.Ceiling(self);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of days.</para>
    /// </summary>
    /// <param name="self">Number of days.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Days(this byte self)
    {
      return new TimeSpan(self, 0, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of days.</para>
    /// </summary>
    /// <param name="self">Number of days.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Days(this short self)
    {
      return new TimeSpan(self, 0, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of days.</para>
    /// </summary>
    /// <param name="self">Number of days.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Days(this int self)
    {
      return new TimeSpan(self, 0, 0, 0);
    }

    /// <summary>
    ///   <para>Performs a decremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="self">Upper bound value to start iteration from. It must be greater or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Lower bound value to end iteration on. It must be lower or equal to the value of <paramref name="self"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DownTo(short, short, Action)"/>
    /// <seealso cref="DownTo(int, int, Action)"/>
    /// <seealso cref="DownTo(long, long, Action)"/>
    public static void DownTo(this byte self, byte to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = self; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs a decremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="self">Upper bound value to start iteration from. It must be greater or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Lower bound value to end iteration on. It must be lower or equal to the value of <paramref name="self"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DownTo(byte, byte, Action)"/>
    /// <seealso cref="DownTo(int, int, Action)"/>
    /// <seealso cref="DownTo(long, long, Action)"/>
    public static void DownTo(this short self, short to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = self; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs a decremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="self">Upper bound value to start iteration from. It must be greater or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Lower bound value to end iteration on. It must be lower or equal to the value of <paramref name="self"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DownTo(byte, byte, Action)"/>
    /// <seealso cref="DownTo(short, short, Action)"/>
    /// <seealso cref="DownTo(long, long, Action)"/>
    public static void DownTo(this int self, int to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = self; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs a decremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="self">Upper bound value to start iteration from. It must be greater or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Lower bound value to end iteration on. It must be lower or equal to the value of <paramref name="self"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DownTo(byte, byte, Action)"/>
    /// <seealso cref="DownTo(short, short, Action)"/>
    /// <seealso cref="DownTo(int, int, Action)"/>
    public static void DownTo(this long self, long to, Action action)
    {
      Assertion.NotNull(action);

      for (var value = self; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="self">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this byte self)
    {
      return self % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="self">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this short self)
    {
      return self % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="self">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this int self)
    {
      return self % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="self">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this long self)
    {
      return self % 2 == 0;
    }

    /// <summary>
    ///   <para>Returns the largest integer less than or equal to the specified number.</para>
    /// </summary>
    /// <param name="self">A double-precision floating-point number.</param>
    /// <returns>The largest integer less than or equal to <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Floor(double)"/>
    public static double Floor(this double self)
    {
      return Math.Floor(self);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of hours.</para>
    /// </summary>
    /// <param name="self">Number of hours.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Hours(this byte self)
    {
      return new TimeSpan(self, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of hours.</para>
    /// </summary>
    /// <param name="self">Number of hours.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Hours(this short self)
    {
      return new TimeSpan(self, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of hours.</para>
    /// </summary>
    /// <param name="self">Number of hours.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Hours(this int self)
    {
      return new TimeSpan(self, 0, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
    /// </summary>
    /// <param name="self">Number of milliseconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Milliseconds(this byte self)
    {
      return new TimeSpan(0, 0, 0, 0, self);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
    /// </summary>
    /// <param name="self">Number of milliseconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Milliseconds(this short self)
    {
      return new TimeSpan(0, 0, 0, 0, self);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of milliseconds.</para>
    /// </summary>
    /// <param name="self">Number of milliseconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Milliseconds(this int self)
    {
      return new TimeSpan(0, 0, 0, 0, self);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of minutes.</para>
    /// </summary>
    /// <param name="self">Number of minutes.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Minutes(this byte self)
    {
      return new TimeSpan(0, self, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of minutes.</para>
    /// </summary>
    /// <param name="self">Number of minutes.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Minutes(this short self)
    {
      return new TimeSpan(0, self, 0);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of minutes.</para>
    /// </summary>
    /// <param name="self">Number of minutes.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Minutes(this int self)
    {
      return new TimeSpan(0, self, 0);
    }

    /// <summary>
    ///   <para>Returns a specified number raised to the specified power.</para>
    /// </summary>
    /// <param name="self">A double-precision floating-point number to be raised to a power.</param>
    /// <param name="power">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The number <paramref name="self"/> raised to the power <paramref name="power"/>.</returns>
    /// <seealso cref="Math.Pow(double, double)"/>
    public static double Power(this double self, double power)
    {
      return Math.Pow(self, power);
    }

    /// <summary>
    ///   <para>Rounds a double-precision floating-point value to the nearest integral value.</para>
    /// </summary>
    /// <param name="self">A double-precision floating-point number to be rounded.</param>
    /// <returns>The integer nearest <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Round(double)"/>
    public static double Round(this double self)
    {
      return Math.Round(self);
    }

    /// <summary>
    ///   <para>Rounds a decimal value to the nearest integral value.</para>
    /// </summary>
    /// <param name="self">A decimal number to be rounded.</param>
    /// <returns>The integer nearest <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Round(decimal)"/>
    public static decimal Round(this decimal self)
    {
      return Math.Round(self);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of seconds.</para>
    /// </summary>
    /// <param name="self">Number of seconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Seconds(this byte self)
    {
      return new TimeSpan(0, 0, self);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of seconds.</para>
    /// </summary>
    /// <param name="self">Number of seconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Seconds(this short self)
    {
      return new TimeSpan(0, 0, self);
    }

    /// <summary>
    ///   <para>Creates a time span object, representing a given number of seconds.</para>
    /// </summary>
    /// <param name="self">Number of seconds.</param>
    /// <returns>Time span instance.</returns>
    public static TimeSpan Seconds(this int self)
    {
      return new TimeSpan(0, 0, self);
    }

    /// <summary>
    ///   <para>Returns the square root of a specified number.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Square root of <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Sqrt(double)"/>
    public static double Sqrt(this double self)
    {
      return Math.Sqrt(self);
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="self">Number of times to call a delegate.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Times(short, Action)"/>
    /// <seealso cref="Times(int, Action)"/>
    /// <seealso cref="Times(long, Action)"/>
    public static void Times(this byte self, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < self; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="self">Number of times to call a delegate.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Times(byte, Action)"/>
    /// <seealso cref="Times(int, Action)"/>
    /// <seealso cref="Times(long, Action)"/>
    public static void Times(this short self, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < self; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="self">Number of times to call a delegate.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Times(byte, Action)"/>
    /// <seealso cref="Times(short, Action)"/>
    /// <seealso cref="Times(long, Action)"/>
    public static void Times(this int self, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < self; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Calls given delegate specified number of times.</para>
    /// </summary>
    /// <param name="self">Number of times to call a delegate.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Times(byte, Action)"/>
    /// <seealso cref="Times(short, Action)"/>
    /// <seealso cref="Times(int, Action)"/>
    public static void Times(this long self, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < self; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs an incremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="self">Lower bound value to start iteration from. It must be lower or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Upper bound value to end iteration on. It must be greater or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="UpTo(short, short, Action)"/>
    /// <seealso cref="UpTo(int, int, Action)"/>
    /// <seealso cref="UpTo(long, long, Action)"/>
    public static void UpTo(this byte self, byte to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = self; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs an incremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="self">Lower bound value to start iteration from. It must be lower or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Upper bound value to end iteration on. It must be greater or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="UpTo(byte, byte, Action)"/>
    /// <seealso cref="UpTo(int, int, Action)"/>
    /// <seealso cref="UpTo(long, long, Action)"/>
    public static void UpTo(this short self, short to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = self; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs an incremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="self">Lower bound value to start iteration from. It must be lower or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Upper bound value to end iteration on. It must be greater or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="UpTo(byte, byte, Action)"/>
    /// <seealso cref="UpTo(short, short, Action)"/>
    /// <seealso cref="UpTo(long, long, Action)"/>
    public static void UpTo(this int self, int to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = self; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para>Performs an incremental iteration in given range with a step of 1 and calls a delegate on each iteration.</para>
    /// </summary>
    /// <param name="self">Lower bound value to start iteration from. It must be lower or equal to the value of <paramref name="to"/>.</param>
    /// <param name="to">Upper bound value to end iteration on. It must be greater or equal to the value of <paramref name="from"/>.</param>
    /// <param name="action">Delegate to call on each step of iteration.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="UpTo(byte, byte, Action)"/>
    /// <seealso cref="UpTo(short, short, Action)"/>
    /// <seealso cref="UpTo(int, int, Action)"/>
    public static void UpTo(this long self, long to, Action action)
    {
      Assertion.NotNull(action);

      for (var value = self; value <= to; value++)
      {
        action();
      }
    }
  }
}