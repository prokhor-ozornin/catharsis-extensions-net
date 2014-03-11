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
  }
}