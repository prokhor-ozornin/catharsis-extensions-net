using System;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class PrimitiveTypesExtensionsTest
  {
    [Fact]
    public void and()
    {
      Assert.True(true.And(true));
      Assert.False(true.And(false));
      Assert.False(false.And(true));
      Assert.False(false.And(false));
    }

    [Fact]
    public void down_to()
    {
      Assert.Throws<ArgumentNullException>(() => ((byte)0).DownTo((byte)0, (Action)null));
      Assert.Throws<ArgumentNullException>(() => ((short)0).DownTo((short)0, (Action)null));
      Assert.Throws<ArgumentNullException>(() => ((int)0).DownTo((int)0, (Action)null));
      Assert.Throws<ArgumentNullException>(() => ((long)0).DownTo((long)0, (Action)null));

      long sum = 0;
      byte.MaxValue.DownTo(1, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((short)byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((int)byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((long)byte.MaxValue).DownTo(1, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);
    }

    [Fact]
    public void or()
    {
      Assert.True(true.Or(true));
      Assert.True(true.Or(false));
      Assert.True(false.Or(true));
      Assert.False(false.Or(false));
    }

    [Fact]
    public void not()
    {
      Assert.False(true.Not());
      Assert.True(false.Not());
    }

    [Fact]
    public void times()
    {
      Assert.Throws<ArgumentNullException>(() => ((byte)0).Times((Action)null));
      Assert.Throws<ArgumentNullException>(() => ((short)0).Times((Action)null));
      Assert.Throws<ArgumentNullException>(() => ((int)0).Times((Action)null));
      Assert.Throws<ArgumentNullException>(() => ((long)0).Times((Action)null));

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
      ((short)byte.MaxValue).Times(() => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      int.MinValue.Times(() => sum += 1);
      Assert.Equal(0, sum);

      sum = 0;
      ((int)byte.MaxValue).Times(() => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      long.MinValue.Times(() => sum += 1);
      Assert.Equal(0, sum);

      sum = 0;
      ((long)byte.MaxValue).Times(() => sum += 1);
      Assert.Equal(byte.MaxValue, sum);
    }

    [Fact]
    public void up_to()
    {
      Assert.Throws<ArgumentNullException>(() => ((byte)0).UpTo((byte)0, (Action)null));
      Assert.Throws<ArgumentNullException>(() => ((short)0).UpTo((short)0, (Action)null));
      Assert.Throws<ArgumentNullException>(() => ((int)0).UpTo((int)0, (Action)null));
      Assert.Throws<ArgumentNullException>(() => ((long)0).UpTo((long)0, (Action)null));

      long sum = 0;
      ((byte)1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((short)1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((int)1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);

      sum = 0;
      ((long)1).UpTo(byte.MaxValue, () => sum += 1);
      Assert.Equal(byte.MaxValue, sum);
    }

    [Fact]
    public void xor()
    {
      Assert.False(true.Xor(true));
      Assert.True(true.Xor(false));
      Assert.True(false.Xor(true));
      Assert.False(false.Xor(false));
    }
  }
}