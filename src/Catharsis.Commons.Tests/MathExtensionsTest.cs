using System;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class MathExtensionsTest
  {
    [Fact]
    public void abs()
    {
      Assert.Equal(1, ((short)-1).Abs());
      Assert.Equal(0, ((short)0).Abs());
      Assert.Equal(1, ((short)1).Abs());
      Assert.Equal(1, ((int)-1).Abs());
      Assert.Equal(0, ((int)0).Abs());
      Assert.Equal(1, ((int)1).Abs());
      Assert.Equal(1, ((long)-1).Abs());
      Assert.Equal(0, ((long)0).Abs());
      Assert.Equal(1, ((long)1).Abs());
      Assert.Equal(1.0, ((float)-1.0).Abs());
      Assert.Equal(0, ((float)0.0).Abs());
      Assert.Equal(1.0, ((float)1.0).Abs());
      Assert.Equal(1.1, ((double)-1.1).Abs());
      Assert.Equal(0, ((double)0.0).Abs());
      Assert.Equal(1.1, ((double)1.1).Abs());
      Assert.Equal((decimal)1.1, ((decimal)-1.1).Abs());
      Assert.Equal((decimal)0, ((decimal)0.0).Abs());
      Assert.Equal((decimal)1.1, ((decimal)1.1).Abs());
    }

    [Fact]
    public void bytes()
    {
      Assert.Throws<ArgumentNullException>(() => MathExtensions.Bytes(null, 1));
      Assert.Throws<ArgumentException>(() => new Random().Bytes(-1));
      Assert.Throws<ArgumentException>(() => new Random().Bytes(0));

      const int Count = 100;
      Assert.Equal(Count, new Random().Bytes(Count).Length);
    }

    [Fact]
    public void ceil()
    {
      Assert.Equal(-2, -1.4.Ceil());
      Assert.Equal(-2, -1.5.Ceil());
      Assert.Equal(-2, -1.6.Ceil());
      Assert.Equal(0, 0.0.Ceil());
      Assert.Equal(2, 1.4.Ceil());
      Assert.Equal(2, 1.5.Ceil());
      Assert.Equal(2, 1.6.Ceil());
    }

    [Fact]
    public void even()
    {
      Assert.True(((byte)0).Even());
      Assert.False(((byte)1).Even());
      Assert.True(((byte)2).Even());

      Assert.True(((short)-2).Even());
      Assert.False(((short)-1).Even());
      Assert.True(((short)0).Even());
      Assert.False(((short)1).Even());
      Assert.True(((short)2).Even());

      Assert.True((-2).Even());
      Assert.False((-1).Even());
      Assert.True(0.Even());
      Assert.False(1.Even());
      Assert.True(2.Even());

      Assert.True(((long)-2).Even());
      Assert.False(((long)-1).Even());
      Assert.True(((long)0).Even());
      Assert.False(((long)1).Even());
      Assert.True(((long)2).Even());
    }

    [Fact]
    public void floor()
    {
      Assert.Equal(-1, -1.4.Floor());
      Assert.Equal(-1, -1.5.Floor());
      Assert.Equal(-1, -1.6.Floor());
      Assert.Equal(0, 0.0.Floor());
      Assert.Equal(1, 1.4.Floor());
      Assert.Equal(1, 1.5.Floor());
      Assert.Equal(1, 1.6.Floor());
    }

    [Fact]
    public void power()
    {
      Assert.Equal(0, 0.0.Power(1));
      Assert.Equal(1, 1.0.Power(0));
      Assert.Equal(4, 2.0.Power(2));
      Assert.Equal(Math.Pow(5, 3), 5.0.Power(3));
    }

    [Fact]
    public void round()
    {
      Assert.Equal(-1, -1.4.Round());
      Assert.Equal(-2, -1.5.Round());
      Assert.Equal(-2, -1.6.Round());
      Assert.Equal(0, 0.0.Round());
      Assert.Equal(1, 1.4.Round());
      Assert.Equal(2, 1.5.Round());
      Assert.Equal(2, 1.6.Round());
    }

    [Fact]
    public void sqrt()
    {
      Assert.Equal(-1, -1.0.Sqrt());
      Assert.Equal(0, 0.0.Sqrt());
      Assert.Equal(2, 4.0.Sqrt());
      Assert.Equal(Math.Sqrt(5), 5.0.Sqrt());
    }
  }
}