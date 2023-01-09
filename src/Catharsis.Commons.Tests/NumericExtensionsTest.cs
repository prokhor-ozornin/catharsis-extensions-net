using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="NumericExtensions"/>.</para>
/// </summary>
public sealed class NumericExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.IsPositive{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPositive_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.IsNegative{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsNegative_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.IsDefault{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDefault_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.To(int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void To_Method()
  {
    AssertionExtensions.Should(() => int.MinValue.To(0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    int.MinValue.To(int.MinValue).Should().NotBeNull().And.BeSameAs(int.MinValue.To(int.MinValue)).And.BeEmpty();
    int.MaxValue.To(int.MaxValue).Should().NotBeNull().And.BeSameAs(int.MaxValue.To(int.MaxValue)).And.BeEmpty();
    0.To(0).Should().NotBeNull().And.BeSameAs(0.To(0)).And.BeEmpty();
    (-1).To(0).Should().NotBeNull().And.NotBeSameAs((-1).To(0)).And.Equal(-1);
    (-1).To(1).Should().NotBeNull().And.NotBeSameAs((-1).To(1)).And.Equal(-1, 0);
    0.To(1).Should().NotBeNull().And.NotBeSameAs(0.To(1)).And.Equal(0);
    0.To(2).Should().NotBeNull().And.NotBeSameAs(0.To(2)).And.Equal(0, 1);
    0.To(short.MaxValue).Should().NotBeNull().And.NotBeSameAs(0.To(short.MaxValue)).And.HaveCount(short.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NumericExtensions.Times(int, Action)"/></description></item>
  ///     <item><description><see cref="NumericExtensions.Times(int, Action{int})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Times_Methods()
  {
    const int count = 10000;

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => 0.Times((Action) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      AssertionExtensions.Should(() => (-1).Times(() => {})).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      var counter = 0;
      0.Times(() => counter++);
      counter.Should().Be(0);

      counter = 0;
      1.Times(() => counter++);
      counter.Should().Be(1);

      counter = 0;
      count.Times(() => counter++);
      counter.Should().Be(count);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => 0.Times((Action<int>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      AssertionExtensions.Should(() => (-1).Times((_ => {}))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      var values = new List<int>();
      0.Times(values.Add);
      values.Should().BeEmpty();

      values = new List<int>();
      1.Times(values.Add);
      values.Should().Equal(0);

      values = new List<int>();
      count.Times(values.Add);
      values.Should().HaveCount(count).And.Equal(Enumerable.Range(0, count));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Min{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    AssertionExtensions.Should(() => NumericExtensions.Min(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Max{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    AssertionExtensions.Should(() => NumericExtensions.Max(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.MinMax{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    AssertionExtensions.Should(() => NumericExtensions.MinMax(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Nulls(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Nulls_Method()
  {
    AssertionExtensions.Should(() => (-1).Nulls()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NumericExtensions.Objects{T}(int)"/></description></item>
  ///     <item><description><see cref="NumericExtensions.Objects{T}(int, Func{T})"/></description></item>
  ///     <item><description><see cref="NumericExtensions.Objects{T}(int, Func{int, T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Objects_Methods()
  {
    const int count = 10000;

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => (-1).Objects<object>()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      0.Objects<object>().Should().BeEmpty();
      1.Objects<Guid>().Should().Equal(Guid.Empty);
      count.Objects<Guid>().Should().HaveCount(count).And.AllBeEquivalentTo(Guid.Empty);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => 0.Objects((Func<object>) null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("constructor");
      AssertionExtensions.Should(() => (-1).Objects<object>(() => null).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      0.Objects(() => new object()).Should().BeEmpty();
      1.Objects(() => Guid.Empty).Should().Equal(Guid.Empty);
      count.Objects(() => Guid.Empty).Should().HaveCount(count).And.AllBeEquivalentTo(Guid.Empty);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => 0.Objects((Func<int, object>) null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("constructor");
      AssertionExtensions.Should(() => (-1).Objects<object>(_ => null).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      0.Objects(index => index).Should().BeEmpty();
      1.Objects(index => index).Should().Equal(0);
      count.Objects(index => index).Should().Equal(Enumerable.Range(0, count));
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Round(float, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Float_Round_Method()
  {
    float.MinValue.Round().Should().Be((float) Math.Round(float.MinValue));
    float.MaxValue.Round().Should().Be((float) Math.Round(float.MaxValue));
    float.Epsilon.Round().Should().Be(0);
    float.NaN.Round().Should().Be(float.NaN);
    float.NegativeInfinity.Round().Should().Be(float.NegativeInfinity);
    float.PositiveInfinity.Round().Should().Be(float.PositiveInfinity);

    ((float) -1.4).Round().Should().Be(-1);
    ((float) -1.5).Round().Should().Be(-2);
    ((float) 0).Round().Should().Be(0);
    ((float) 1.4).Round().Should().Be(1);
    ((float) 1.5).Round().Should().Be(2);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Round(double, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Double_Round_Method()
  {
    using (new AssertionScope())
    {
      double.MinValue.Round().Should().Be((double) Math.Round(double.MinValue));
      double.MaxValue.Round().Should().Be((double) Math.Round(double.MaxValue));
      double.Epsilon.Round().Should().Be(0);
      double.NaN.Round().Should().Be(double.NaN);
      double.NegativeInfinity.Round().Should().Be(double.NegativeInfinity);
      double.PositiveInfinity.Round().Should().Be(double.PositiveInfinity);

      ((double) -1.4).Round().Should().Be(-1);
      ((double) -1.5).Round().Should().Be(-2);
      ((double) 0).Round().Should().Be(0);
      ((double) 1.4).Round().Should().Be(1);
      ((double) 1.5).Round().Should().Be(2);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Round(decimal, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Decimal_Round_Method()
  {
    decimal.MinValue.Round().Should().Be((decimal) Math.Round(decimal.MinValue));
    decimal.MaxValue.Round().Should().Be((decimal) Math.Round(decimal.MaxValue));
    decimal.Zero.Round().Should().Be(decimal.Zero);
    decimal.MinusOne.Round().Should().Be(-1);
    decimal.One.Round().Should().Be(1);

    ((decimal) -1.4).Round().Should().Be(-1);
    ((decimal) -1.5).Round().Should().Be(-2);
    ((decimal) 0).Round().Should().Be(0);
    ((decimal) 1.4).Round().Should().Be(1);
    ((decimal) 1.5).Round().Should().Be(2);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Power(float, float)"/> method.</para>
  /// </summary>
  [Fact]
  public void Float_Power_Method()
  {
    /*
    0.0.Power(1).Should().Be(0);
    1.0.Power(0).Should().Be(1);
    2.0.Power(2).Should().Be(4);
    5.0.Power(3).Should().Be(Math.Pow(5, 3));
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Power(double, double)"/> method.</para>
  /// </summary>
  [Fact]
  public void Double_Power_Method()
  {
    /*
    0.0.Power(1).Should().Be(0);
    1.0.Power(0).Should().Be(1);
    2.0.Power(2).Should().Be(4);
    5.0.Power(3).Should().Be(Math.Pow(5, 3));
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Power(decimal, decimal)"/> method.</para>
  /// </summary>
  [Fact]
  public void Decimal_Power_Method()
  {
    /*
    0.0.Power(1).Should().Be(0);
    1.0.Power(0).Should().Be(1);
    2.0.Power(2).Should().Be(4);
    5.0.Power(3).Should().Be(Math.Pow(5, 3));
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Abs(sbyte)"/> method.</para>
  /// </summary>
  [Fact]
  public void Sbyte_Abs_Method()
  {
    /*
    ((float) -1.0).Abs().Should().Be((float) 1.0);
    ((float) 0.0).Abs().Should().Be((float) 0);
    ((float) 1.0).Abs().Should().Be((float) 1.0);
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Abs(short)"/> method.</para>
  /// </summary>
  [Fact]
  public void Short_Abs_Method()
  {
    /*
    ((short) -1).Abs().Should().Be(1);
    ((short) 0).Abs().Should().Be(0);
    ((short) 1).Abs().Should().Be(1);
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Abs(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Int_Abs_Method()
  {
    /*
    ((int) -1).Abs().Should().Be(1);
    ((int) 0).Abs().Should().Be(0);
    ((int) 1).Abs().Should().Be(1);
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Abs(long)"/> method.</para>
  /// </summary>
  [Fact]
  public void Long_Abs_Method()
  {
    /*
    ((long) -1).Abs().Should().Be(1);
    ((long) 0).Abs().Should().Be(0);
    ((long) 1).Abs().Should().Be(1);
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Abs(float)"/> method.</para>
  /// </summary>
  [Fact]
  public void Float_Abs_Method()
  {
    /*
    ((float) -1.0).Abs().Should().Be((float) 1.0);
    ((float) 0.0).Abs().Should().Be((float) 0);
    ((float) 1.0).Abs().Should().Be((float) 1.0);
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Abs(double)"/> method.</para>
  /// </summary>
  [Fact]
  public void Double_Abs_Method()
  {
    /*
    (-1.1).Abs().Should().Be(1.1);
    (0.0).Abs().Should().Be(0);
    (1.1).Abs().Should().Be(1.1);
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Abs(decimal)"/> method.</para>
  /// </summary>
  [Fact]
  public void Decimal_Abs_Method()
  {
    /*
    ((decimal) -1.1).Abs().Should().Be((decimal) 1.1);
    ((decimal) 0.0).Abs().Should().Be(0);
    ((decimal) 1.1).Abs().Should().Be((decimal) 1.1);
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Ceil(float)"/> method.</para>
  /// </summary>
  [Fact]
  public void Float_Ceil_Method()
  {
    /*(-1.4).Ceil().Should().Be(-2);
    (-1.5).Ceil().Should().Be(-2);
    (-1.6).Ceil().Should().Be(-2);
    0.0.Ceil().Should().Be(0);
    1.4.Ceil().Should().Be(2);
    1.5.Ceil().Should().Be(2);
    1.6.Ceil().Should().Be(2);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Ceil(double)"/> method.</para>
  /// </summary>
  [Fact]
  public void Double_Ceil_Method()
  {
    /*(-1.4).Ceil().Should().Be(-2);
    (-1.5).Ceil().Should().Be(-2);
    (-1.6).Ceil().Should().Be(-2);
    0.0.Ceil().Should().Be(0);
    1.4.Ceil().Should().Be(2);
    1.5.Ceil().Should().Be(2);
    1.6.Ceil().Should().Be(2);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Ceil(decimal)"/> method.</para>
  /// </summary>
  [Fact]
  public void Decimal_Ceil_Method()
  {
    /*(-1.4).Ceil().Should().Be(-2);
    (-1.5).Ceil().Should().Be(-2);
    (-1.6).Ceil().Should().Be(-2);
    0.0.Ceil().Should().Be(0);
    1.4.Ceil().Should().Be(2);
    1.5.Ceil().Should().Be(2);
    1.6.Ceil().Should().Be(2);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Floor(float)"/> method.</para>
  /// </summary>
  [Fact]
  public void Float_Floor_Method()
  {
    /*(-1.4).Floor().Should().Be(-1);
    (-1.5).Floor().Should().Be(-1);
    (-1.6).Floor().Should().Be(-1);
    0.0.Floor().Should().Be(0);
    1.4.Floor().Should().Be(1);
    1.5.Floor().Should().Be(1);
    1.6.Floor().Should().Be(1);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Floor(double)"/> method.</para>
  /// </summary>
  [Fact]
  public void Double_Floor_Method()
  {
    /*(-1.4).Floor().Should().Be(-1);
    (-1.5).Floor().Should().Be(-1);
    (-1.6).Floor().Should().Be(-1);
    0.0.Floor().Should().Be(0);
    1.4.Floor().Should().Be(1);
    1.5.Floor().Should().Be(1);
    1.6.Floor().Should().Be(1);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NumericExtensions.Floor(decimal)"/> method.</para>
  /// </summary>
  [Fact]
  public void Decimal_Floor_Method()
  {
    /*(-1.4).Floor().Should().Be(-1);
    (-1.5).Floor().Should().Be(-1);
    (-1.6).Floor().Should().Be(-1);
    0.0.Floor().Should().Be(0);
    1.4.Floor().Should().Be(1);
    1.5.Floor().Should().Be(1);
    1.6.Floor().Should().Be(1);*/

    throw new NotImplementedException();
  }
}