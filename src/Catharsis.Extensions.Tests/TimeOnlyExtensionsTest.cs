using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TimeOnlyExtensions"/>.</para>
/// </summary>
/// <seealso cref="TimeOnlyExtensions"/>
public sealed class TimeOnlyExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.Range(TimeOnly, TimeOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    using (new AssertionScope())
    {
      Test(DateTime.Now.ToTimeOnly());
      Test(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Test(TimeOnly time)
    {
      time.Range(time, TimeSpan.Zero).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.BeEmpty();
      time.Range(time, TimeSpan.FromTicks(1)).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.BeEmpty();
      time.Range(time, TimeSpan.FromTicks(-1)).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.BeEmpty();

      time.Range(time.Add(1.Milliseconds), 1.Milliseconds).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(time);
      time.Range(time.Add(-1.Milliseconds), 1.Milliseconds).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(time.Add(-1.Milliseconds));

      time.Range(time.Add(1.Milliseconds), 2.Milliseconds).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(time);
      time.Range(time.Add(-1.Milliseconds), 2.Milliseconds).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(1).And.Equal(time.Add(-1.Milliseconds));

      time.Range(time.Add(2.Milliseconds), 1.Milliseconds).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(time, time.Add(1.Milliseconds));
      time.Range(time.Add(-2.Milliseconds), 1.Milliseconds).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(time.Add(-2.Milliseconds), time.Add(-1.Milliseconds));

      time.Range(time.Add(3.Milliseconds), 2.Milliseconds).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(time, time.Add(2.Milliseconds));
      time.Range(time.Add(-3.Milliseconds), 2.Milliseconds).Should().BeAssignableTo<IEnumerable<TimeOnly>>().And.HaveCount(2).And.Equal(time.Add(-3.Milliseconds), time.Add(-1.Milliseconds));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.get_StartOfHour(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void StartOfHour_Property()
  {
    using (new AssertionScope())
    {
      Test(TimeOnly.MinValue);
      Test(TimeOnly.MaxValue);
      Test(DateTime.Now.ToTimeOnly());
      Test(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Test(TimeOnly time) => time.StartOfHour.Should().HaveHours(time.Hour).And.HaveMinutes(0).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.get_EndOfHour(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void EndOfHour_Property()
  {
    using (new AssertionScope())
    {
      Test(TimeOnly.MinValue);
      Test(TimeOnly.MaxValue);
      Test(DateTime.Now.ToTimeOnly());
      Test(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Test(TimeOnly time) => time.EndOfHour.Should().HaveHours(time.Hour).And.HaveMinutes(59).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.get_StartOfMinute(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void StartOfMinute_Property()
  {
    using (new AssertionScope())
    {
      Test(TimeOnly.MinValue);
      Test(TimeOnly.MaxValue);
      Test(DateTime.Now.ToTimeOnly());
      Test(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Test(TimeOnly time) => time.StartOfMinute.Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.get_EndOfMinute(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void EndOfMinute_Property()
  {
    using (new AssertionScope())
    {
      Test(TimeOnly.MinValue);
      Test(TimeOnly.MaxValue);
      Test(DateTime.Now.ToTimeOnly());
      Test(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Test(TimeOnly time) => time.EndOfMinute.Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.get_StartOfSecond(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void StartOfSecond_Property()
  {
    using (new AssertionScope())
    {
      Test(TimeOnly.MinValue);
      Test(TimeOnly.MaxValue);
      Test(DateTime.Now.ToTimeOnly());
      Test(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Test(TimeOnly time) => time.StartOfSecond.Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(time.Second).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TimeOnlyExtensions.get_EndOfSecond(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void EndOfSecond_Property()
  {
    using (new AssertionScope())
    {
      Test(TimeOnly.MinValue);
      Test(TimeOnly.MaxValue);
      Test(DateTime.Now.ToTimeOnly());
      Test(DateTime.UtcNow.ToTimeOnly());
    }

    return;

    static void Test(TimeOnly time) => time.EndOfSecond.Should().HaveHours(time.Hour).And.HaveMinutes(time.Minute).And.HaveSeconds(time.Second).And.HaveMilliseconds(999);
  }
}