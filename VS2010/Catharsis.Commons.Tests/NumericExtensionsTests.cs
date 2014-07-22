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

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Days(byte)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Days(short)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Days(int)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Days_Methods()
    {
      Assert.Equal(1, ((byte) 1).Days().TotalDays);
      Assert.Equal(1, ((short) 1).Days().TotalDays);
      Assert.Equal(1, 1.Days().TotalDays);
      Assert.Equal(0, ((byte) 0).Days().TotalMilliseconds);
      Assert.Equal(-1, ((short) -1).Days().TotalDays);
      Assert.Equal(-1, -1.Days().TotalDays);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Hours(byte)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Hours(short)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Hours(int)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Hours_Methods()
    {
      Assert.Equal(1, ((byte) 1).Hours().TotalHours);
      Assert.Equal(1, ((short) 1).Hours().TotalHours);
      Assert.Equal(1, 1.Hours().TotalHours);
      Assert.Equal(0, ((byte) 0).Hours().TotalMilliseconds);
      Assert.Equal(-1, ((short) -1).Hours().TotalHours);
      Assert.Equal(-1, -1.Hours().TotalHours);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Minutes(byte)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Minutes(short)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Minutes(int)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Minutes_Methods()
    {
      Assert.Equal(1, ((byte) 1).Minutes().TotalMinutes);
      Assert.Equal(1, ((short) 1).Minutes().TotalMinutes);
      Assert.Equal(1, 1.Minutes().TotalMinutes);
      Assert.Equal(0, ((byte) 0).Minutes().TotalMilliseconds);
      Assert.Equal(-1, ((short) -1).Minutes().TotalMinutes);
      Assert.Equal(-1, -1.Minutes().TotalMinutes);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Seconds(byte)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Seconds(short)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Seconds(int)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Seconds_Methods()
    {
      Assert.Equal(1, ((byte) 1).Seconds().TotalSeconds);
      Assert.Equal(1, ((short) 1).Seconds().TotalSeconds);
      Assert.Equal(1, 1.Seconds().TotalSeconds);
      Assert.Equal(0, ((byte) 0).Seconds().TotalMilliseconds);
      Assert.Equal(-1, ((short) -1).Seconds().TotalSeconds);
      Assert.Equal(-1, -1.Seconds().TotalSeconds);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Milliseconds(byte)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Milliseconds(short)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Milliseconds(int)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Milliseconds_Methods()
    {
      Assert.Equal(1, ((byte) 1).Milliseconds().TotalMilliseconds);
      Assert.Equal(1, ((short) 1).Milliseconds().TotalMilliseconds);
      Assert.Equal(1, 1.Milliseconds().TotalMilliseconds);
      Assert.Equal(0, ((byte) 0).Milliseconds().TotalMilliseconds);
      Assert.Equal(-1, ((short) -1).Milliseconds().TotalMilliseconds);
      Assert.Equal(-1, -1.Milliseconds().TotalMilliseconds);
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="NumericExtensions.Ceil(double)"/> method.</para>
    /// </summary>
    [Fact]
    public void Ceil_Method()
    {
      Assert.Equal(-2, -1.4.Ceil());
      Assert.Equal(-2, -1.5.Ceil());
      Assert.Equal(-2, -1.6.Ceil());
      Assert.Equal(0, 0.0.Ceil());
      Assert.Equal(2, 1.4.Ceil());
      Assert.Equal(2, 1.5.Ceil());
      Assert.Equal(2, 1.6.Ceil());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="NumericExtensions.Floor(double)"/> method.</para>
    /// </summary>
    [Fact]
    public void Floor_Method()
    {
      Assert.Equal(-1, -1.4.Floor());
      Assert.Equal(-1, -1.5.Floor());
      Assert.Equal(-1, -1.6.Floor());
      Assert.Equal(0, 0.0.Floor());
      Assert.Equal(1, 1.4.Floor());
      Assert.Equal(1, 1.5.Floor());
      Assert.Equal(1, 1.6.Floor());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="NumericExtensions.Power(double, double)"/> method.</para>
    /// </summary>
    [Fact]
    public void Power_Method()
    {
      Assert.Equal(0, 0.0.Power(1));
      Assert.Equal(1, 1.0.Power(0));
      Assert.Equal(4, 2.0.Power(2));
      Assert.Equal(Math.Pow(5, 3), 5.0.Power(3));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Round(double)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Round(decimal)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Round_Methods()
    {
      Assert.Equal(-1, -1.4.Round());
      Assert.Equal(-2, -1.5.Round());
      Assert.Equal(-2, -1.6.Round());
      Assert.Equal(0, 0.0.Round());
      Assert.Equal(1, 1.4.Round());
      Assert.Equal(2, 1.5.Round());
      Assert.Equal(2, 1.6.Round());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="NumericExtensions.Sqrt(double)"/> method.</para>
    /// </summary>
    [Fact]
    public void Sqrt_Method()
    {
      Assert.Equal(-1, -1.0.Sqrt());
      Assert.Equal(0, 0.0.Sqrt());
      Assert.Equal(2, 4.0.Sqrt());
      Assert.Equal(Math.Sqrt(5), 5.0.Sqrt());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Even(byte)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Even(short)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Even(int)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Even(long)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Even_Methods()
    {
      Assert.True(((byte) 0).Even());
      Assert.False(((byte) 1).Even());
      Assert.True(((byte) 2).Even());

      Assert.True(((short) -2).Even());
      Assert.False(((short) -1).Even());
      Assert.True(((short) 0).Even());
      Assert.False(((short) 1).Even());
      Assert.True(((short) 2).Even());

      Assert.True((-2).Even());
      Assert.False((-1).Even());
      Assert.True(0.Even());
      Assert.False(1.Even());
      Assert.True(2.Even());

      Assert.True(((long) -2).Even());
      Assert.False(((long) -1).Even());
      Assert.True(((long) 0).Even());
      Assert.False(((long) 1).Even());
      Assert.True(((long) 2).Even());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="NumericExtensions.Abs(short)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Abs(int)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Abs(long)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Abs(float)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Abs(double)"/></description></item>
    ///     <item><description><see cref="NumericExtensions.Abs(decimal)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Abs_Methods()
    {
      Assert.Equal(1, ((short) -1).Abs());
      Assert.Equal(0, ((short) 0).Abs());
      Assert.Equal(1, ((short) 1).Abs());
      Assert.Equal(1, ((int) -1).Abs());
      Assert.Equal(0, ((int) 0).Abs());
      Assert.Equal(1, ((int) 1).Abs());
      Assert.Equal(1, ((long) -1).Abs());
      Assert.Equal(0, ((long) 0).Abs());
      Assert.Equal(1, ((long) 1).Abs());
      Assert.Equal(1.0, ((float) -1.0).Abs());
      Assert.Equal(0, ((float) 0.0).Abs());
      Assert.Equal(1.0, ((float) 1.0).Abs());
      Assert.Equal(1.1, ((double) -1.1).Abs());
      Assert.Equal(0, ((double) 0.0).Abs());
      Assert.Equal(1.1, ((double) 1.1).Abs());
      Assert.Equal((decimal) 1.1, ((decimal) -1.1).Abs());
      Assert.Equal((decimal) 0, ((decimal) 0.0).Abs());
      Assert.Equal((decimal) 1.1, ((decimal) 1.1).Abs());
    }
  }
}