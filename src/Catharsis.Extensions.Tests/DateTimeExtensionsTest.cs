using System.Globalization;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DateTimeExtensions"/>.</para>
/// </summary>
public sealed class DateTimeExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsByDate(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_EqualsByDate_Method()
  {
    var dates = new[] { DateTime.MinValue, DateTime.Now, DateTime.UtcNow };

    foreach (var date in dates)
    {
      var startOfDay = date.TruncateToDayStart();

      date.EqualsByDate(date).Should().BeTrue();
      date.EqualsByDate(startOfDay).Should().BeTrue();

      startOfDay.AddYears(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddMonths(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddDays(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddHours(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddMinutes(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddSeconds(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddMilliseconds(1).EqualsByDate(startOfDay).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsByDate(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_EqualsByDate_Method()
  {
    var dates = new[] { DateTimeOffset.MinValue, DateTimeOffset.Now, DateTimeOffset.UtcNow };

    foreach (var date in dates)
    {
      var startOfDay = date.TruncateToDayStart();

      date.EqualsByDate(date).Should().BeTrue();
      date.EqualsByDate(startOfDay).Should().BeTrue();

      startOfDay.AddYears(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddMonths(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddDays(1).EqualsByDate(startOfDay).Should().BeFalse();
      startOfDay.AddHours(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddMinutes(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddSeconds(1).EqualsByDate(startOfDay).Should().BeTrue();
      startOfDay.AddMilliseconds(1).EqualsByDate(startOfDay).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsByTime(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_EqualsByTime_Method()
  {
    var dates = new[] { DateTime.Now, DateTime.UtcNow };

    foreach (var date in dates)
    {
      var startOfDay = date.TruncateToDayStart();

      date.EqualsByTime(date).Should().BeTrue();
      date.EqualsByTime(startOfDay).Should().BeFalse();

      startOfDay.AddYears(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddYears(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddMonths(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddDays(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddHours(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddMinutes(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddSeconds(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddMilliseconds(1).EqualsByTime(startOfDay).Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.EqualsByTime(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_EqualsByTime_Method()
  {
    var dates = new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow };

    foreach (var date in dates)
    {
      var startOfDay = date.TruncateToDayStart();

      date.EqualsByTime(date).Should().BeTrue();
      date.EqualsByTime(startOfDay).Should().BeFalse();

      startOfDay.AddYears(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddMonths(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddDays(1).EqualsByTime(startOfDay).Should().BeTrue();
      startOfDay.AddHours(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddMinutes(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddSeconds(1).EqualsByTime(startOfDay).Should().BeFalse();
      startOfDay.AddMilliseconds(1).EqualsByTime(startOfDay).Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Min(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_Min_Method()
  {
    DateTime.MinValue.Min(DateTime.MinValue).Should().Be(DateTime.MinValue);
    DateTime.MinValue.Min(DateTime.MaxValue).Should().Be(DateTime.MinValue);

    DateTime.MaxValue.Min(DateTime.MaxValue).Should().Be(DateTime.MaxValue);
    DateTime.MaxValue.Min(DateTime.MinValue).Should().Be(DateTime.MinValue);

    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
    {
      date.Min(date).Should().Be(date);
      date.Add(TimeSpan.Zero).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromDays(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromHours(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromMinutes(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromSeconds(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromMilliseconds(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromTicks(1)).Min(date).Should().Be(date);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Min(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_Min_Method()
  {
    DateTimeOffset.MinValue.Min(DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue);
    DateTimeOffset.MinValue.Min(DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MinValue);

    DateTimeOffset.MaxValue.Min(DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue);
    DateTimeOffset.MaxValue.Min(DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue);

    foreach (var date in new[] {DateTimeOffset.Now, DateTimeOffset.UtcNow})
    {
      date.Min(date).Should().Be(date);
      date.Add(TimeSpan.Zero).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromDays(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromHours(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromMinutes(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromSeconds(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromMilliseconds(1)).Min(date).Should().Be(date);
      date.Add(TimeSpan.FromTicks(1)).Min(date).Should().Be(date);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Min(DateOnly, DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_Min_Method()
  {
    DateOnly.MinValue.Min(DateOnly.MinValue).Should().Be(DateOnly.MinValue);
    DateOnly.MinValue.Min(DateOnly.MaxValue).Should().Be(DateOnly.MinValue);

    DateOnly.MaxValue.Min(DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
    DateOnly.MaxValue.Min(DateOnly.MinValue).Should().Be(DateOnly.MinValue);

    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(0).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(1).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(1).Min(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(1).Min(dateOnly).Should().Be(dateOnly);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Min(TimeOnly, TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_Min_Method()
  {
    TimeOnly.MinValue.Min(TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
    TimeOnly.MinValue.Min(TimeOnly.MaxValue).Should().Be(TimeOnly.MinValue);

    TimeOnly.MaxValue.Min(TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);
    TimeOnly.MaxValue.Min(TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);

    var timeOnly = TimeOnly.MinValue;
    timeOnly.Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.Zero).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromDays(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromHours(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromMinutes(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromSeconds(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromMilliseconds(1)).Min(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromTicks(1)).Min(timeOnly).Should().Be(timeOnly);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Max(DateTime, DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_Max_Method()
  {
    DateTime.MinValue.Max(DateTime.MinValue).Should().Be(DateTime.MinValue);
    DateTime.MinValue.Max(DateTime.MaxValue).Should().Be(DateTime.MaxValue);

    DateTime.MaxValue.Max(DateTime.MaxValue).Should().Be(DateTime.MaxValue);
    DateTime.MaxValue.Max(DateTime.MinValue).Should().Be(DateTime.MaxValue);

    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
    {
      date.Max(date).Should().Be(date);
      date.Add(TimeSpan.Zero).Max(date).Should().Be(date);
      date.Add(TimeSpan.FromDays(1)).Max(date).Should().Be(date.Add(TimeSpan.FromDays(1)));
      date.Add(TimeSpan.FromHours(1)).Max(date).Should().Be(date.Add(TimeSpan.FromHours(1)));
      date.Add(TimeSpan.FromMinutes(1)).Max(date).Should().Be(date.Add(TimeSpan.FromMinutes(1)));
      date.Add(TimeSpan.FromSeconds(1)).Max(date).Should().Be(date.Add(TimeSpan.FromSeconds(1)));
      date.Add(TimeSpan.FromMilliseconds(1)).Max(date).Should().Be(date.Add(TimeSpan.FromMilliseconds(1)));
      date.Add(TimeSpan.FromTicks(1)).Max(date).Should().Be(date.Add(TimeSpan.FromTicks(1)));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Max(DateTimeOffset, DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_Max_Method()
  {
    DateTimeOffset.MinValue.Max(DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue);
    DateTimeOffset.MinValue.Max(DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue);

    DateTimeOffset.MaxValue.Max(DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue);
    DateTimeOffset.MaxValue.Max(DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MaxValue);

    foreach (var date in new[] {DateTimeOffset.Now, DateTimeOffset.UtcNow})
    {
      date.Max(date).Should().Be(date);
      date.Add(TimeSpan.Zero).Max(date).Should().Be(date);
      date.Add(TimeSpan.FromDays(1)).Max(date).Should().Be(date.Add(TimeSpan.FromDays(1)));
      date.Add(TimeSpan.FromHours(1)).Max(date).Should().Be(date.Add(TimeSpan.FromHours(1)));
      date.Add(TimeSpan.FromMinutes(1)).Max(date).Should().Be(date.Add(TimeSpan.FromMinutes(1)));
      date.Add(TimeSpan.FromSeconds(1)).Max(date).Should().Be(date.Add(TimeSpan.FromSeconds(1)));
      date.Add(TimeSpan.FromMilliseconds(1)).Max(date).Should().Be(date.Add(TimeSpan.FromMilliseconds(1)));
      date.Add(TimeSpan.FromTicks(1)).Max(date).Should().Be(date.Add(TimeSpan.FromTicks(1)));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Max(DateOnly, DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_Max_Method()
  {
    DateOnly.MinValue.Max(DateOnly.MinValue).Should().Be(DateOnly.MinValue);
    DateOnly.MinValue.Max(DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);

    DateOnly.MaxValue.Max(DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
    DateOnly.MaxValue.Max(DateOnly.MinValue).Should().Be(DateOnly.MaxValue);

    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
    {
      var dateOnly = DateOnly.FromDateTime(date);
      dateOnly.Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddMonths(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddDays(0).Max(dateOnly).Should().Be(dateOnly);
      dateOnly.AddYears(1).Max(dateOnly).Should().Be(dateOnly.AddYears(1));
      dateOnly.AddMonths(1).Max(dateOnly).Should().Be(dateOnly.AddMonths(1));
      dateOnly.AddDays(1).Max(dateOnly).Should().Be(dateOnly.AddDays(1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Max(TimeOnly, TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_Max_Method()
  {
    TimeOnly.MinValue.Max(TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
    TimeOnly.MinValue.Max(TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);

    TimeOnly.MaxValue.Max(TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);
    TimeOnly.MinValue.Max(TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);

    var timeOnly = TimeOnly.MinValue;
    timeOnly.Max(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.Zero).Max(timeOnly).Should().Be(timeOnly);
    timeOnly.Add(TimeSpan.FromDays(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromDays(1)));
    timeOnly.Add(TimeSpan.FromHours(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromHours(1)));
    timeOnly.Add(TimeSpan.FromMinutes(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromMinutes(1)));
    timeOnly.Add(TimeSpan.FromSeconds(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromSeconds(1)));
    timeOnly.Add(TimeSpan.FromMilliseconds(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromMilliseconds(1)));
    timeOnly.Add(TimeSpan.FromTicks(1)).Max(timeOnly).Should().Be(timeOnly.Add(TimeSpan.FromTicks(1)));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Range(DateTime, DateTime, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_Range_Method()
  {
    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
    {
      date.Range(date, TimeSpan.Zero).Should().BeEmpty();
      date.Range(date, TimeSpan.FromTicks(1)).Should().BeEmpty();
      date.Range(date, TimeSpan.FromTicks(-1)).Should().BeEmpty();

      date.Range(date.AddDays(1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(1), 1.Days())).And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-1), 1.Days())).And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(1), 2.Days())).And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-1), 2.Days())).And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(2), 1.Days())).And.HaveCount(2).And.Equal(date, date.AddDays(1));
      date.Range(date.AddDays(-2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-2), 1.Days())).And.HaveCount(2).And.Equal(date.AddDays(-2), date.AddDays(-1));

      date.Range(date.AddDays(3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(3), 2.Days())).And.HaveCount(2).And.Equal(date, date.AddDays(2));
      date.Range(date.AddDays(-3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-3), 2.Days())).And.HaveCount(2).And.Equal(date.AddDays(-3), date.AddDays(-1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Range(DateTimeOffset, DateTimeOffset, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_Range_Method()
  {
    foreach (var date in new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.Range(date, TimeSpan.Zero).Should().BeEmpty();
      date.Range(date, TimeSpan.FromTicks(1)).Should().BeEmpty();
      date.Range(date, TimeSpan.FromTicks(-1)).Should().BeEmpty();

      date.Range(date.AddDays(1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(1), 1.Days())).And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-1), 1.Days())).And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(1), 2.Days())).And.HaveCount(1).And.Equal(date);
      date.Range(date.AddDays(-1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-1), 2.Days())).And.HaveCount(1).And.Equal(date.AddDays(-1));

      date.Range(date.AddDays(2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(2), 1.Days())).And.HaveCount(2).And.Equal(date, date.AddDays(1));
      date.Range(date.AddDays(-2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-2), 1.Days())).And.HaveCount(2).And.Equal(date.AddDays(-2), date.AddDays(-1));

      date.Range(date.AddDays(3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(3), 2.Days())).And.HaveCount(2).And.Equal(date, date.AddDays(2));
      date.Range(date.AddDays(-3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(date.Range(date.AddDays(-3), 2.Days())).And.HaveCount(2).And.Equal(date.AddDays(-3), date.AddDays(-1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Range(DateOnly, DateOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_Range_Methods()
  {
    foreach (var date in new[] { DateTime.Now, DateTime.UtcNow })
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.Range(dateOnly, TimeSpan.Zero).Should().BeEmpty();
      dateOnly.Range(dateOnly, TimeSpan.FromTicks(1)).Should().BeEmpty();
      dateOnly.Range(dateOnly, TimeSpan.FromTicks(-1)).Should().BeEmpty();

      dateOnly.Range(dateOnly.AddDays(1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(1), 1.Days())).And.HaveCount(1).And.Equal(dateOnly);
      dateOnly.Range(dateOnly.AddDays(-1), 1.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(-1), 1.Days())).And.HaveCount(1).And.Equal(dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(1), 2.Days())).And.HaveCount(1).And.Equal(dateOnly);
      dateOnly.Range(dateOnly.AddDays(-1), 2.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(-1), 2.Days())).And.HaveCount(1).And.Equal(dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(2), 1.Days())).And.HaveCount(2).And.Equal(dateOnly, dateOnly.AddDays(1));
      dateOnly.Range(dateOnly.AddDays(-2), 1.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(-2), 1.Days())).And.HaveCount(2).And.Equal(dateOnly.AddDays(-2), dateOnly.AddDays(-1));

      dateOnly.Range(dateOnly.AddDays(3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(3), 2.Days())).And.HaveCount(2).And.Equal(dateOnly, dateOnly.AddDays(2));
      dateOnly.Range(dateOnly.AddDays(-3), 2.Days()).Should().NotBeNull().And.NotBeSameAs(dateOnly.Range(dateOnly.AddDays(-3), 2.Days())).And.HaveCount(2).And.Equal(dateOnly.AddDays(-3), dateOnly.AddDays(-1));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Range(TimeOnly, TimeOnly, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_Range_Methods()
  {
    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
    {
      var timeOnly = TimeOnly.FromDateTime(date);

      timeOnly.Range(timeOnly, TimeSpan.Zero).Should().BeEmpty();
      timeOnly.Range(timeOnly, TimeSpan.FromTicks(1)).Should().BeEmpty();
      timeOnly.Range(timeOnly, TimeSpan.FromTicks(-1)).Should().BeEmpty();

      timeOnly.Range(timeOnly.Add(1.Milliseconds()), 1.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(1.Milliseconds()), 1.Milliseconds())).And.HaveCount(1).And.Equal(timeOnly);
      timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 1.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 1.Milliseconds())).And.HaveCount(1).And.Equal(timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(1.Milliseconds()), 2.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(1.Milliseconds()), 2.Milliseconds())).And.HaveCount(1).And.Equal(timeOnly);
      timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 2.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(-1.Milliseconds()), 2.Milliseconds())).And.HaveCount(1).And.Equal(timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(2.Milliseconds()), 1.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(2.Milliseconds()), 1.Milliseconds())).And.HaveCount(2).And.Equal(timeOnly, timeOnly.Add(1.Milliseconds()));
      timeOnly.Range(timeOnly.Add(-2.Milliseconds()), 1.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(-2.Milliseconds()), 1.Milliseconds())).And.HaveCount(2).And.Equal(timeOnly.Add(-2.Milliseconds()), timeOnly.Add(-1.Milliseconds()));

      timeOnly.Range(timeOnly.Add(3.Milliseconds()), 2.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(3.Milliseconds()), 2.Milliseconds())).And.HaveCount(2).And.Equal(timeOnly, timeOnly.Add(2.Milliseconds()));
      timeOnly.Range(timeOnly.Add(-3.Milliseconds()), 2.Milliseconds()).Should().NotBeNull().And.NotBeSameAs(timeOnly.Range(timeOnly.Add(-3.Milliseconds()), 2.Milliseconds())).And.HaveCount(2).And.Equal(timeOnly.Add(-3.Milliseconds()), timeOnly.Add(-1.Milliseconds()));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Days(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Days_Method()
  {
    AssertionExtensions.Should(() => int.MinValue.Days()).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => int.MaxValue.Days()).ThrowExactly<ArgumentOutOfRangeException>();

    foreach (var count in new[] {-1, 0, 1})
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Hours(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Hours_Method()
  {
    AssertionExtensions.Should(() => int.MinValue.Hours()).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => int.MaxValue.Hours()).ThrowExactly<ArgumentOutOfRangeException>();

    foreach (var count in new[] {-1, 0, 1})
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Minutes(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Minutes_Method()
  {
    foreach (var count in new[] {-1, 0, 1})
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Seconds(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Seconds_Method()
  {
    foreach (var count in new[] {-1, 0, 1})
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Milliseconds(int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Milliseconds_Method()
  {
    foreach (var count in new[] {-1, 0, 1})
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
  ///   <para>Performs testing of <see cref="DateTimeExtensions.Ticks(long)"/> method.</para>
  /// </summary>
  [Fact]
  public void Ticks_Method()
  {
    foreach (var count in new[] { -1, 0, 1 })
    {
      var ticks = count.Ticks();

      ticks.Days.Should().Be(0);
      ticks.Hours.Should().Be(0);
      ticks.Minutes.Should().Be(0);
      ticks.Seconds.Should().Be(0);
      ticks.Milliseconds.Should().Be(count / (int) TimeSpan.TicksPerMillisecond);
      ticks.TotalDays.Should().Be(count / (double) TimeSpan.TicksPerDay);
      ticks.TotalHours.Should().Be(count / (double) TimeSpan.TicksPerHour);
      ticks.TotalMinutes.Should().Be(count / (double) TimeSpan.TicksPerMinute);
      ticks.TotalSeconds.Should().Be(count / (double) TimeSpan.TicksPerSecond);
      ticks.TotalMilliseconds.Should().Be(count / (double) TimeSpan.TicksPerMillisecond);
      ticks.Ticks.Should().Be(count);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsEmpty(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeSpan_IsEmpty_Method()
  {
    TimeSpan.MinValue.IsEmpty().Should().BeFalse();
    TimeSpan.MaxValue.IsEmpty().Should().BeFalse();

    TimeSpan.FromTicks(long.MinValue).IsEmpty().Should().BeFalse();
    TimeSpan.FromTicks(long.MaxValue).IsEmpty().Should().BeFalse();

    TimeSpan.Zero.IsEmpty().Should().BeTrue();
    TimeSpan.FromTicks(0).IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.InThePast(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeSpan_InThePast_Method()
  {
    var now = DateTime.UtcNow;

    TimeSpan.Zero.InThePast().Should().BeBefore(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
    TimeSpan.FromMinutes(1).InThePast().Should().BeBefore(DateTimeOffset.UtcNow).And.BeBefore(now).And.BeWithin(TimeSpan.Zero);
    TimeSpan.FromMinutes(-1).InThePast().Should().BeAfter(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.InTheFuture(TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeSpan_InTheFuture_Method()
  {
    var now = DateTime.UtcNow;

    TimeSpan.Zero.InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
    TimeSpan.FromMinutes(1).InTheFuture().Should().BeAfter(DateTimeOffset.UtcNow).And.BeAfter(now).And.BeWithin(TimeSpan.Zero);
    TimeSpan.FromMinutes(-1).InTheFuture().Should().BeBefore(DateTimeOffset.UtcNow).And.BeBefore(now).And.BeWithin(TimeSpan.Zero);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsPast(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_IsPast_Method()
  {
    foreach (var date in new[] {DateTime.Now, DateTime.UtcNow})
    {
      date.IsPast().Should().BeTrue();
      date.AddSeconds(-1).IsPast().Should().BeTrue();
      date.AddSeconds(1).IsPast().Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsPast(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_IsPast_Method()
  {
    foreach (var date in new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.IsPast().Should().BeTrue();
      date.AddSeconds(-1).IsPast().Should().BeTrue();
      date.AddSeconds(1).IsPast().Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsFuture(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_IsFuture_Method()
  {
    foreach (var date in new[] { DateTime.Now, DateTime.UtcNow })
    {
      date.IsFuture().Should().BeFalse();
      date.AddSeconds(-1).IsFuture().Should().BeFalse();
      date.AddSeconds(1).IsFuture().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsFuture(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_IsFuture_Method()
  {
    foreach (var date in new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.IsFuture().Should().BeFalse();
      date.AddSeconds(-1).IsFuture().Should().BeFalse();
      date.AddSeconds(1).IsFuture().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekday(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_IsWeekday_Method()
  {
    var now = DateTime.UtcNow;
    var dates = new DateTime[7].Fill(index => now.AddDays(index));

    dates.Count(date => date.IsWeekday()).Should().Be(5);
    dates.Single(date => date.DayOfWeek == DayOfWeek.Monday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Friday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday).IsWeekday().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday).IsWeekday().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekday(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_IsWeekday_Method()
  {
    var now = DateTimeOffset.UtcNow;
    var dates = new DateTimeOffset[7].Fill(index => now.AddDays(index));

    dates.Count(date => date.IsWeekday()).Should().Be(5);
    dates.Single(date => date.DayOfWeek == DayOfWeek.Monday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Friday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday).IsWeekday().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday).IsWeekday().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekday(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_IsWeekday_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    var dates = new DateOnly[7].Fill(now.AddDays);

    dates.Count(date => date.IsWeekday()).Should().Be(5);
    dates.Single(date => date.DayOfWeek == DayOfWeek.Monday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Friday).IsWeekday().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday).IsWeekday().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday).IsWeekday().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekend(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_IsWeekend_Method()
  {
    var now = DateTime.UtcNow;
    var dates = new DateTime[7].Fill(index => now.AddDays(index));

    dates.Count(date => date.IsWeekend()).Should().Be(2);
    dates.Single(date => date.DayOfWeek == DayOfWeek.Monday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Friday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday).IsWeekend().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday).IsWeekend().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekend(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_IsWeekend_Method()
  {
    var now = DateTimeOffset.UtcNow;
    var dates = new DateTimeOffset[7].Fill(index => now.AddDays(index));

    dates.Count(date => date.IsWeekend()).Should().Be(2);
    dates.Single(date => date.DayOfWeek == DayOfWeek.Monday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Friday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday).IsWeekend().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday).IsWeekend().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.IsWeekend(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_IsWeekend_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    var dates = new DateOnly[7].Fill(now.AddDays);

    dates.Count(date => date.IsWeekend()).Should().Be(2);
    dates.Single(date => date.DayOfWeek == DayOfWeek.Monday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Tuesday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Wednesday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Thursday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Friday).IsWeekend().Should().BeFalse();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Saturday).IsWeekend().Should().BeTrue();
    dates.Single(date => date.DayOfWeek == DayOfWeek.Sunday).IsWeekend().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToYearStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToYearStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToYearStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToYearStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(1).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearStart(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_TruncateToYearStart_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    now.TruncateToYearStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(1).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToMonthStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToMonthStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToMonthStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToMonthStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(1).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthStart(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_TruncateToMonthStart_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    now.TruncateToMonthStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToDayStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToDayStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToDayStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToDayStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToDayStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToDayStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(0).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToHourStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToHourStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(0).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToHourStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToHourStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(0).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_TruncateToHourStart_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToHourStart().Should().BeOnOrBefore(now).And.HaveHours(now.Hour).And.HaveMinutes(0).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToMinuteStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToMinuteStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToMinuteStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToMinuteStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(0).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_TruncateToMinuteStart_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToMinuteStart().Should().BeOnOrBefore(now).And.HaveHours(now.Hour).And.HaveMinutes(now.Minute).And.HaveSeconds(0).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondStart(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToSecondStart_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToSecondStart().Should().BeOnOrBefore(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(now.Second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondStart(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToSecondStart_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToSecondStart().Should().BeOnOrBefore(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(now.Second).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondStart(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_TruncateToSecondStart_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToSecondStart().Should().BeOnOrBefore(now).And.HaveHours(now.Hour).And.HaveMinutes(now.Minute).And.HaveSeconds(now.Second).And.HaveMilliseconds(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToYearEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToYearEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToYearEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToYearEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(12).And.HaveDay(31).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToYearEnd(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_TruncateToYearEnd_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    now.TruncateToYearEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(12).And.HaveDay(DateTime.DaysInMonth(now.Year, now.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToMonthEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToMonthEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(DateTime.DaysInMonth(now.Year, now.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToMonthEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToMonthEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(DateTime.DaysInMonth(now.Year, now.Month)).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMonthEnd(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_TruncateToMonthEnd_Method()
  {
    var now = DateTime.UtcNow.ToDateOnly();
    now.TruncateToMonthEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(DateTime.DaysInMonth(now.Year, now.Month));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToDayEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToDayEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToDayEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToDayEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToDayEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToDayEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(23).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToHourEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToHourEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(59).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToHourEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToHourEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(59).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToHourEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_TruncateToHourEnd_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToHourEnd().Should().BeOnOrAfter(now).And.HaveHours(now.Hour).And.HaveMinutes(59).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToMinuteEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToMinuteEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(59);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToMinuteEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToMinuteEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(59).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToMinuteEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_TruncateToMinuteEnd_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToMinuteEnd().Should().BeOnOrAfter(now).And.HaveHours(now.Hour).And.HaveMinutes(now.Minute).And.HaveSeconds(59).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondEnd(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_TruncateToSecondEnd_Method()
  {
    var now = DateTime.UtcNow;
    now.TruncateToSecondEnd().Should().BeOnOrAfter(now).And.BeIn(now.Kind).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(now.Second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondEnd(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_TruncateToSecondEnd_Method()
  {
    var now = DateTimeOffset.UtcNow;
    now.TruncateToSecondEnd().Should().BeOnOrAfter(now).And.HaveYear(now.Year).And.HaveMonth(now.Month).And.HaveDay(now.Day).And.HaveHour(now.Hour).And.HaveMinute(now.Minute).And.HaveSecond(now.Second).And.BeWithin(now.Offset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.TruncateToSecondEnd(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_TruncateToSecondEnd_Method()
  {
    var now = DateTime.UtcNow.ToTimeOnly();
    now.TruncateToSecondEnd().Should().BeOnOrAfter(now).And.HaveHours(now.Hour).And.HaveMinutes(now.Minute).And.HaveSeconds(now.Second).And.HaveMilliseconds(999);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTime(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_ToDateTime_Method()
  {
    foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.ToDateTime().Should().BeSameDateAs(date.ToDateTime()).And.BeSameDateAs(date.UtcDateTime).And.BeIn(DateTimeKind.Utc);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTime(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_ToDateTime_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.ToDateTime().Should()
              .BeSameDateAs(dateOnly.ToDateTime())
              .And
              .BeIn(DateTimeKind.Unspecified)
              .And
              .HaveYear(dateOnly.Year)
              .And
              .HaveMonth(dateOnly.Month)
              .And
              .HaveDay(dateOnly.Day)
              .And
              .HaveHour(0)
              .And
              .HaveMinute(0)
              .And
              .HaveSecond(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTime(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_ToDateTime_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      var timeOnly = TimeOnly.FromDateTime(date);
      var now = DateTime.UtcNow;

      timeOnly.ToDateTime().Should()
              .BeSameDateAs(timeOnly.ToDateTime())
              .And
              .BeIn(DateTimeKind.Utc)
              .And
              .HaveYear(now.Year)
              .And
              .HaveMonth(now.Month)
              .And
              .HaveDay(now.Day)
              .And
              .HaveHour(timeOnly.Hour)
              .And
              .HaveMinute(timeOnly.Minute)
              .And
              .HaveSecond(timeOnly.Second);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTimeOffset(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_ToDateTimeOffset_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      date.ToDateTimeOffset().Should().BeSameDateAs(date.ToDateTimeOffset()).And.BeSameDateAs(new DateTimeOffset(date.ToUniversalTime())).And.BeWithin(TimeSpan.Zero);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTimeOffset(DateOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_ToDateTimeOffset_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      var dateOnly = DateOnly.FromDateTime(date);

      dateOnly.ToDateTimeOffset().Should()
              .BeSameDateAs(dateOnly.ToDateTimeOffset())
              .And
              .HaveYear(dateOnly.Year)
              .And
              .HaveMonth(dateOnly.Month)
              .And
              .HaveDay(dateOnly.Day)
              .And
              .HaveHour(0)
              .And
              .HaveMinute(0)
              .And
              .HaveSecond(0)
              .And
              .BeWithin(TimeSpan.Zero);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateTimeOffset(TimeOnly)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_ToDateTimeOffset_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      var timeOnly = TimeOnly.FromDateTime(date);
      var now = DateTimeOffset.UtcNow;

      timeOnly.ToDateTimeOffset().Should()
              .BeSameDateAs(timeOnly.ToDateTime())
              .And
              .HaveYear(now.Year)
              .And
              .HaveMonth(now.Month)
              .And
              .HaveDay(now.Day)
              .And
              .HaveHour(timeOnly.Hour)
              .And
              .HaveMinute(timeOnly.Minute)
              .And
              .HaveSecond(timeOnly.Second)
              .And
              .BeWithin(TimeSpan.Zero);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_ToDateOnly_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      date.ToDateOnly().Should().Be(date.ToDateOnly()).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToDateOnly(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_ToDateOnly_Method()
  {
    foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.ToDateOnly().Should().Be(date.ToDateOnly()).And.HaveYear(date.Year).And.HaveMonth(date.Month).And.HaveDay(date.Day);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToTimeOnly(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_ToTimeOnly_Method()
  {
    foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
    {
      date.ToTimeOnly().Should().Be(date.ToTimeOnly()).And.HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToTimeOnly(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_ToTimeOnly_Method()
  {
    foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      date.ToTimeOnly().Should().Be(date.ToTimeOnly()).And.HaveHours(date.Hour).And.HaveMinutes(date.Minute).And.HaveSeconds(date.Second).And.HaveMilliseconds(date.Millisecond);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToIsoString(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_ToIsoString_Method()
  {
    var now = DateTime.Now;
    now.ToIsoString().Should().Be(now.ToUniversalTime().ToString("o"));
    DateTime.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture).Should().Be(now);

    now = DateTime.UtcNow;
    now.ToIsoString().Should().Be(now.ToString("o"));
    DateTime.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(now);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToIsoString(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_ToIsoString_Method()
  {
    var now = DateTimeOffset.Now;
    now.ToIsoString().Should().Be(now.ToUniversalTime().ToString("o"));
    DateTimeOffset.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture).Should().Be(now);

    now = DateTimeOffset.UtcNow;
    now.ToIsoString().Should().Be(now.ToString("o"));
    DateTimeOffset.ParseExact(now.ToIsoString(), "o", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(now);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToRfcString(DateTime)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_ToRfcString_Method()
  {
    var now = DateTime.Now;
    now.ToRfcString().Should().Be(now.ToUniversalTime().ToString("r"));
    DateTime.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture).Should().Be(now.ToUniversalTime().TruncateToSecondStart());

    now = DateTime.UtcNow;
    now.ToRfcString().Should().Be(now.ToString("r"));
    DateTime.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).Should().Be(now.TruncateToSecondStart());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DateTimeExtensions.ToRfcString(DateTimeOffset)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_ToRfcString_Method()
  {
    var now = DateTimeOffset.Now;
    now.ToRfcString().Should().Be(now.ToUniversalTime().ToString("r"));
    DateTimeOffset.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture).Should().Be(now.ToUniversalTime().TruncateToSecondStart());

    now = DateTimeOffset.UtcNow;
    now.ToRfcString().Should().Be(now.ToString("r"));
    DateTimeOffset.ParseExact(now.ToRfcString(), "r", CultureInfo.InvariantCulture).Should().Be(now.TruncateToSecondStart());
  }
}