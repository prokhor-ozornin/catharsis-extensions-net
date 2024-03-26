using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="BasicTypesExtensions"/>.</para>
/// </summary>
public sealed class BasicTypesExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Repeat(char, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Char_Repeat_Method()
  {
    AssertionExtensions.Should(() => char.MinValue.Repeat(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new[] { char.MinValue, char.MaxValue }.ForEach(character =>
    {
      Validate(character, count);
    });

    return;

    static void Validate(char character, int count)
    {
      var result = character.Repeat(count);
      result.Should().NotBeNull().And.NotBeSameAs(character.Repeat(count)).And.HaveLength(count);
      result.ToCharArray().Should().AllBeEquivalentTo(character);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.To(int, int)"/> method.</para>
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

    return;

    static void Validate(int from, int to)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="BasicTypesExtensions.Times(int, Action)"/></description></item>
  ///     <item><description><see cref="BasicTypesExtensions.Times(int, Action{int})"/></description></item>
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

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => 0.Times((Action<int>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      AssertionExtensions.Should(() => (-1).Times(_ => {})).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      var values = new List<int>();
      0.Times(values.Add);
      values.Should().BeEmpty();

      values = [];
      1.Times(values.Add);
      values.Should().Equal(0);

      values = [];
      count.Times(values.Add);
      values.Should().HaveCount(count).And.Equal(Enumerable.Range(0, count));

      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Nulls(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Nulls_Method()
  {
    AssertionExtensions.Should(() => (-1).Nulls()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    throw new NotImplementedException();

    return;

    static void Validate(int count)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="BasicTypesExtensions.Objects{T}(int)"/></description></item>
  ///     <item><description><see cref="BasicTypesExtensions.Objects{T}(int, Func{T})"/></description></item>
  ///     <item><description><see cref="BasicTypesExtensions.Objects{T}(int, Func{int, T})"/></description></item>
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

      static void Validate(int count)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => 0.Objects((Func<object>) null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("constructor");
      AssertionExtensions.Should(() => (-1).Objects<object>(() => null).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      0.Objects(() => new object()).Should().BeEmpty();
      1.Objects(() => Guid.Empty).Should().Equal(Guid.Empty);
      count.Objects(() => Guid.Empty).Should().HaveCount(count).And.AllBeEquivalentTo(Guid.Empty);

      static void Validate(int count)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => 0.Objects((Func<int, object>) null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("constructor");
      AssertionExtensions.Should(() => (-1).Objects<object>(_ => null).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      0.Objects(index => index).Should().BeEmpty();
      1.Objects(index => index).Should().Equal(0);
      count.Objects(index => index).Should().Equal(Enumerable.Range(0, count));

      static void Validate(int count)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Days(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Days_Method()
  {
    AssertionExtensions.Should(() => int.MinValue.Days()).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => int.MaxValue.Days()).ThrowExactly<ArgumentOutOfRangeException>();

    new[] { -1, 0, 1 }.ForEach(Validate);

    return;

    static void Validate(int count)
    {
      var days = count.Days();

      days.Days.Should().Be(count);
      days.Hours.Should().Be(0);
      days.Minutes.Should().Be(0);
      days.Seconds.Should().Be(0);
      days.Milliseconds.Should().Be(0);
      days.TotalDays.Should().Be(count);
      days.TotalHours.Should().Be(24 * count);
      days.TotalMinutes.Should().Be(24 * 60 * count);
      days.TotalSeconds.Should().Be(24 * 60 * 60 * count);
      days.TotalMilliseconds.Should().Be(24 * 60 * 60 * 1000 * count);
      days.Ticks.Should().Be(TimeSpan.TicksPerDay * count);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Hours(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Hours_Method()
  {
    AssertionExtensions.Should(() => int.MinValue.Hours()).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => int.MaxValue.Hours()).ThrowExactly<ArgumentOutOfRangeException>();

    new[] { -1, 0, 1 }.ForEach(Validate);

    return;

    static void Validate(int count)
    {
      var hours = count.Hours();

      hours.Days.Should().Be(0);
      hours.Hours.Should().Be(count);
      hours.Minutes.Should().Be(0);
      hours.Seconds.Should().Be(0);
      hours.Milliseconds.Should().Be(0);
      hours.TotalDays.Should().Be(count / 24.0);
      hours.TotalHours.Should().Be(count);
      hours.TotalMinutes.Should().Be(60 * count);
      hours.TotalSeconds.Should().Be(60 * 60 * count);
      hours.TotalMilliseconds.Should().Be(60 * 60 * 1000 * count);
      hours.Ticks.Should().Be(TimeSpan.TicksPerHour * count);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Minutes(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Minutes_Method()
  {
    new[] { -1, 0, 1 }.ForEach(Validate);

    return;

    static void Validate(int count)
    {
      var hours = count.Minutes();

      hours.Days.Should().Be(0);
      hours.Hours.Should().Be(0);
      hours.Minutes.Should().Be(count);
      hours.Seconds.Should().Be(0);
      hours.Milliseconds.Should().Be(0);
      hours.TotalDays.Should().Be(count / 1440.0);
      hours.TotalHours.Should().Be(count / 60.0);
      hours.TotalMinutes.Should().Be(count);
      hours.TotalSeconds.Should().Be(60 * count);
      hours.TotalMilliseconds.Should().Be(60 * 1000 * count);
      hours.Ticks.Should().Be(TimeSpan.TicksPerMinute * count);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Seconds(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Seconds_Method()
  {
    new[] { -1, 0, 1 }.ForEach(Validate);

    return;

    static void Validate(int count)
    {
      var seconds = count.Seconds();

      seconds.Days.Should().Be(0);
      seconds.Hours.Should().Be(0);
      seconds.Minutes.Should().Be(0);
      seconds.Seconds.Should().Be(count);
      seconds.Milliseconds.Should().Be(0);
      seconds.TotalDays.Should().Be(count / (24.0 * 60 * 60));
      seconds.TotalHours.Should().Be(count / (60.0 * 60));
      seconds.TotalMinutes.Should().Be(count / 60.0);
      seconds.TotalSeconds.Should().Be(count);
      seconds.TotalMilliseconds.Should().Be(1000 * count);
      seconds.Ticks.Should().Be(TimeSpan.TicksPerSecond * count);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Milliseconds(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Milliseconds_Method()
  {
    new[] { -1, 0, 1 }.ForEach(Validate);

    return;

    static void Validate(int count)
    {
      var milliseconds = count.Milliseconds();

      milliseconds.Days.Should().Be(0);
      milliseconds.Hours.Should().Be(0);
      milliseconds.Minutes.Should().Be(0);
      milliseconds.Seconds.Should().Be(0);
      milliseconds.Milliseconds.Should().Be(count);
      milliseconds.TotalDays.Should().Be(count / (24.0 * 60 * 60 * 1000));
      milliseconds.TotalHours.Should().Be(count / (60.0 * 60 * 1000));
      milliseconds.TotalMinutes.Should().Be(count / (60.0 * 1000));
      milliseconds.TotalSeconds.Should().Be(count / 1000.0);
      milliseconds.TotalMilliseconds.Should().Be(count);
      milliseconds.Ticks.Should().Be(TimeSpan.TicksPerMillisecond * count);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Ticks(long)"/> method.</para>
  /// </summary>
  [Fact]
  public void Ticks_Method()
  {
    new long[] { -1, 0, 1 }.ForEach(Validate);

    return;

    static void Validate(long count)
    {
      var ticks = count.Ticks();

      ticks.Days.Should().Be(0);
      ticks.Hours.Should().Be(0);
      ticks.Minutes.Should().Be(0);
      ticks.Seconds.Should().Be(0);
      ticks.Milliseconds.Should().Be((int) (count / (int) TimeSpan.TicksPerMillisecond));
      ticks.TotalDays.Should().Be(count / (double) TimeSpan.TicksPerDay);
      ticks.TotalHours.Should().Be(count / (double) TimeSpan.TicksPerHour);
      ticks.TotalMinutes.Should().Be(count / (double) TimeSpan.TicksPerMinute);
      ticks.TotalSeconds.Should().Be(count / (double) TimeSpan.TicksPerSecond);
      ticks.TotalMilliseconds.Should().Be(count / (double) TimeSpan.TicksPerMillisecond);
      ticks.Ticks.Should().Be(count);
      }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Round(float, int?)"/> method.</para>
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

    return;

    static void Validate(float original, float result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Round(double, int?)"/> method.</para>
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

    return;

    static void Validate(double original, double result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Round(decimal, int?)"/> method.</para>
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

    return;

    static void Validate(decimal original, decimal result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Power(float, float)"/> method.</para>
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

    return;

    static void Validate(float original, float result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Power(double, double)"/> method.</para>
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

    return;

    static void Validate(double original, double result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Power(decimal, decimal)"/> method.</para>
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

    return;

    static void Validate(decimal original, decimal result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Abs(sbyte)"/> method.</para>
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

    return;

    static void Validate(sbyte original, short result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Abs(short)"/> method.</para>
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

    return;

    static void Validate(short original, short result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Abs(int)"/> method.</para>
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

    return;

    static void Validate(int original, int result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Abs(long)"/> method.</para>
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

    return;

    static void Validate(long original, long result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Abs(float)"/> method.</para>
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

    return;

    static void Validate(float original, float result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Abs(double)"/> method.</para>
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

    return;

    static void Validate(double original, double result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Abs(decimal)"/> method.</para>
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

    return;

    static void Validate(decimal original, decimal result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Ceil(float)"/> method.</para>
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

    return;

    static void Validate(float original, float result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Ceil(double)"/> method.</para>
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

    return;

    static void Validate(double original, double result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Ceil(decimal)"/> method.</para>
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

    return;

    static void Validate(decimal original, decimal result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Floor(float)"/> method.</para>
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

    return;

    static void Validate(float original, float result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Floor(double)"/> method.</para>
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

    return;

    static void Validate(double original, double result)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BasicTypesExtensions.Floor(decimal)"/> method.</para>
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

    return;

    static void Validate(decimal original, decimal result)
    {
    }
  }
}