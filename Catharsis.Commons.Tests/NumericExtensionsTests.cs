using System;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="NumericExtensions"/>.</para>
  /// </summary>
  public sealed class NumericExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.DownTo(byte, byte, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.DownTo(short, short, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.DownTo(int, int, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.DownTo(long, long, Action)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void DownTo_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ((byte) 0).DownTo((byte) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => ((short) 0).DownTo((short) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => ((int) 0).DownTo((int) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => ((long) 0).DownTo((long) 0, (Action) null));

      long sum = 0;
      byte.MaxValue.DownTo(1, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((short) byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((int) byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((long) byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Times(byte, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Times(short, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Times(int, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Times(long, Action)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Times_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ((byte) 0).Times((Action) null));
      Assert.Throws<ArgumentNullException>(() => ((short) 0).Times((Action) null));
      Assert.Throws<ArgumentNullException>(() => ((int) 0).Times((Action) null));
      Assert.Throws<ArgumentNullException>(() => ((long)0).Times((Action) null));

      long sum = 0;
      byte.MinValue.Times(() => sum += 1);
      Assert.Equal(0, sum);

      sum = 0;
      byte.MaxValue.Times(() => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      short.MinValue.Times(() => sum += 1);
      Assert.Equal(0, sum);

      sum = 0;
      ((short) byte.MaxValue).Times(() => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      int.MinValue.Times(() => sum += 1);
      Assert.Equal(0, sum);

      sum = 0;
      ((int) byte.MaxValue).Times(() => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      long.MinValue.Times(() => sum += 1);
      Assert.Equal(0, sum);

      sum = 0;
      ((long) byte.MaxValue).Times(() => sum += 1);
      Assert.Equal(byte.MaxValue, sum);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.UpTo(byte, byte, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.UpTo(short, short, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.UpTo(int, int, Action)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.UpTo(long, long, Action)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void UpTo_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ((byte) 0).UpTo((byte) 0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => ((short)0).UpTo((short)0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => ((int)0).UpTo((int)0, (Action) null));
      Assert.Throws<ArgumentNullException>(() => ((long)0).UpTo((long)0, (Action) null));

      long sum = 0;
      ((byte) 1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((short) 1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((int) 1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((long) 1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);
    }
  }
}