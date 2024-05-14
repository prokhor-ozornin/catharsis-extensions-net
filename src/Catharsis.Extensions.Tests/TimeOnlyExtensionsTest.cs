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
      Validate(DateTime.UtcNow.ToTimeOnly());
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
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.AtStartOfHour(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfHour_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
      Validate(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.AtStartOfHour().Should().HaveHours(time.Hour).And.HaveMinutes(0).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.AtStartOfMinute(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
      Validate(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.AtStartOfMinute().Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.AtStartOfSecond(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtStartOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
      Validate(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.AtStartOfSecond().Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(time.Second).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.AtEndOfHour(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfHour_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
      Validate(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.AtEndOfHour().Should().HaveHours(time.Hour).And.HaveMinutes(59).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.AtEndOfMinute(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfMinute_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
      Validate(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.AtEndOfMinute().Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.AtEndOfSecond(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void AtEndOfSecond_Method()
  {
    using (new AssertionScope())
    {
      Validate(TimeOnly.MinValue);
      Validate(TimeOnly.MaxValue);
      Validate(DateTime.Now.ToTimeOnly());
      Validate(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Validate(TimeOnly time) => time.AtEndOfSecond().Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(time.Second).And.HaveMilliseconds(999);
  }
}