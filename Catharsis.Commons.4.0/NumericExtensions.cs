using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public static class NumericExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void DownTo(this byte from, byte to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void DownTo(this short from, short to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///    <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void DownTo(this int from, int to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void DownTo(this long from, long to, Action action)
    {
      Assertion.NotNull(action);

      for (var value = from; value >= to; value--)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void Times(this byte count, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < count; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void Times(this short count, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < count; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void Times(this int count, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < count; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void Times(this long count, Action action)
    {
      Assertion.NotNull(action);

      for (long i = 0; i < count; i++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void UpTo(this byte from, byte to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static void UpTo(this short from, short to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void UpTo(this int from, int to, Action action)
    {
      Assertion.NotNull(action);

      for (long value = from; value <= to; value++)
      {
        action();
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentNullException"></exception>
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