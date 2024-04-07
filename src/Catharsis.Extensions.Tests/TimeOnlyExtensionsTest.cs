using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TimeOnlyExtensions"/>.</para>
/// </summary>
public sealed class TimeOnlyExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.Range(TimeOnly, TimeOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    using (new AssertionScope())
    {
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time)
    {
      time.Range(time, TimeSpan.Zero).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.BeEmpty();
      time.Range(time, TimeSpan.FromTicks(1)).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.BeEmpty();
      time.Range(time, TimeSpan.FromTicks(-1)).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.BeEmpty();

      time.Range(time.Add(1.Milliseconds()), 1.Milliseconds()).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(time);
      time.Range(time.Add(-1.Milliseconds()), 1.Milliseconds()).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(time.Add(-1.Milliseconds()));

      time.Range(time.Add(1.Milliseconds()), 2.Milliseconds()).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(time);
      time.Range(time.Add(-1.Milliseconds()), 2.Milliseconds()).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(time.Add(-1.Milliseconds()));

      time.Range(time.Add(2.Milliseconds()), 1.Milliseconds()).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(time, time.Add(1.Milliseconds()));
      time.Range(time.Add(-2.Milliseconds()), 1.Milliseconds()).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(time.Add(-2.Milliseconds()), time.Add(-1.Milliseconds()));

      time.Range(time.Add(3.Milliseconds()), 2.Milliseconds()).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(time, time.Add(2.Milliseconds()));
      time.Range(time.Add(-3.Milliseconds()), 2.Milliseconds()).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(time.Add(-3.Milliseconds()), time.Add(-1.Milliseconds()));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.StartOfHour(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void StartOfHour_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.StartOfHour().Should().HaveHours(time.Hour).And.HaveMinutes(0).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.StartOfMinute(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void StartOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.StartOfMinute().Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.StartOfSecond(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void StartOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.StartOfSecond().Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(time.Second).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.EndOfHour(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void EndOfHour_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.EndOfHour().Should().HaveHours(time.Hour).And.HaveMinutes(59).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.EndOfMinute(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void EndOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.EndOfMinute().Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.EndOfSecond(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void EndOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.EndOfSecond().Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(time.Second).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.ToDateTime(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTime_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time)
    {
      var now = DateTimeOffset.UtcNow;
      time.ToDateTime().Should().BeIn(DateTimeKind.Utc).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(time.Hour).And.HaveMinute(time.Minute).And.HaveSecond(time.Second);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.ToDateTimeOffset(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time)
    {
      var now = DateTimeOffset.UtcNow;
      time.ToDateTimeOffset().Should().HaveOffset(default).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(time.Hour).And.HaveMinute(time.Minute).And.HaveSecond(time.Second).And.BeWithin(default);
    }
  }
}