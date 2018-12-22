using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for primitive types.</para>
  /// </summary>
  public static class PrimitiveTypesExtensions
  {
    /// <summary>
    ///   <para>Performs a logical AND operation between current <see cref="bool"/> value and another.</para>
    /// </summary>
    /// <param name="self">Current <see cref="bool"/> value ("left").</param>
    /// <param name="other">Second <see cref="bool"/> value ("right").</param>
    /// <returns>Result of logical operation.</returns>
    public static bool And(this bool self, bool other)
    {
      return self && other;
    }

    /// <summary>
    ///   <para>Performs a logical NOT operation with current <see cref="bool"/> value.</para>
    /// </summary>
    /// <param name="self">Current <see cref="bool"/> value.</param>
    /// <returns>Result of logical operation.</returns>
    public static bool Not(this bool self)
    {
      return !self;
    }

    /// <summary>
    ///   <para>Performs a logical OR operation between current <see cref="bool"/> value and another.</para>
    /// </summary>
    /// <param name="self">Current <see cref="bool"/> value ("left").</param>
    /// <param name="other">Second <see cref="bool"/> value ("right").</param>
    /// <returns>Result of logical operation.</returns>
    public static bool Or(this bool self, bool other)
    {
      return self || other;
    }

    /// <summary>
    ///   <para>Performs a logical XOR (Exclusive OR) operation between current <see cref="bool"/> value and another.</para>
    /// </summary>
    /// <param name="self">Current <see cref="bool"/> value ("left").</param>
    /// <param name="other">Second <see cref="bool"/> value ("right").</param>
    /// <returns>Result of logical operation.</returns>
    public static bool Xor(this bool self, bool other)
    {
      return self ^ other;
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