using System;
using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="NumbersExtensions"/>.</para>
  /// </summary>
  public sealed class NumberExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumbersExtensions.DownTo(byte, byte, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.DownTo(short, short, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.DownTo(int, int, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.DownTo(long, long, Action)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void DownTo_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.DownTo((byte) 0, (byte) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.DownTo((short) 0, (short) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.DownTo((int) 0, (int) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.DownTo((long) 0, (long) 0, (Action) null));

      long sum = 0;
      byte.MaxValue.DownTo(1, () => sum += 1);
      Assert.True(sum == byte.MaxValue);

      sum = 0;
      ((short) byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.True(sum == byte.MaxValue);

      sum = 0;
      ((int) byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.True(sum == byte.MaxValue);

      sum = 0;
      ((long) byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.True(sum == byte.MaxValue);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumbersExtensions.Times(byte, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.Times(short, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.Times(int, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.Times(long, Action)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Times_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.Times((byte) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.Times((short) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.Times((int) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.Times((long)0, (Action) null));

      long sum = 0;
      byte.MinValue.Times(() => sum += 1);
      Assert.True(sum == 0);

      sum = 0;
      byte.MaxValue.Times(() => sum += 1);
      Assert.True(sum == byte.MaxValue);

      sum = 0;
      short.MinValue.Times(() => sum += 1);
      Assert.True(sum == 0);

      sum = 0;
      ((short) byte.MaxValue).Times(() => sum += 1);
      Assert.True(sum == byte.MaxValue);

      sum = 0;
      int.MinValue.Times(() => sum += 1);
      Assert.True(sum == 0);

      sum = 0;
      ((int) byte.MaxValue).Times(() => sum += 1);
      Assert.True(sum == byte.MaxValue, int.MaxValue.ToString());

      sum = 0;
      long.MinValue.Times(() => sum += 1);
      Assert.True(sum == 0);

      sum = 0;
      ((long) byte.MaxValue).Times(() => sum += 1);
      Assert.True(sum == byte.MaxValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="NumbersExtensions.ToInt64Bits(double)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToInt64Bits_Method()
    {
      var value = new Random().NextDouble();
      Assert.True(0.0.ToInt64Bits() == 0);
      Assert.True(value.ToInt64Bits() == BitConverter.DoubleToInt64Bits(value));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumbersExtensions.UpTo(byte, byte, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.UpTo(short, short, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.UpTo(int, int, Action)"/></description></item>
    ///     <item><description><see cref="NumbersExtensions.UpTo(long, long, Action)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void UpTo_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.UpTo((byte) 0, (byte) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.UpTo((short)0, (short)0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.UpTo((int)0, (int)0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => NumbersExtensions.UpTo((long)0, (long)0, (Action) null));

      long sum = 0;
      ((byte) 1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.True(sum == byte.MaxValue);

      sum = 0;
      ((short) 1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.True(sum == byte.MaxValue);

      sum = 0;
      ((int) 1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.True(sum == byte.MaxValue);

      sum = 0;
      ((long) 1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.True(sum == byte.MaxValue);
    }
  }
}