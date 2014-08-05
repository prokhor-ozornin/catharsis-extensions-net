using System;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TimeSpanExtensions"/>.</para>
  /// </summary>
  public sealed class TimeSpanExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="TimeSpanExtensions.FromNow(TimeSpan)"/> method.</para>
    /// </summary>
    [Fact]
    public void FromNow_Method()
    {
      var timespan = new TimeSpan(1, 0, 0, 0);
      Assert.True(timespan.FromNow().IsSameDate(DateTime.Now.Add(timespan)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TimeSpanExtensions.FromNowUtc(TimeSpan)"/> method.</para>
    /// </summary>
    [Fact]
    public void FromNowUtc_Method()
    {
      var timespan = new TimeSpan(1, 0, 0, 0);
      Assert.True(timespan.FromNowUtc().IsSameDate(DateTime.UtcNow.Add(timespan)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TimeSpanExtensions.BeforeNow(TimeSpan)"/> method.</para>
    /// </summary>
    [Fact]
    public void BeforeNow_Method()
    {
      var timespan = new TimeSpan(1, 0, 0, 0);
      Assert.True(timespan.BeforeNow().IsSameDate(DateTime.Now.Subtract(timespan)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TimeSpanExtensions.BeforeNowUtc(TimeSpan)"/> method.</para>
    /// </summary>
    [Fact]
    public void BeforeNowUtc_Method()
    {
      var timespan = new TimeSpan(1, 0, 0, 0);
      Assert.True(timespan.BeforeNowUtc().IsSameDate(DateTime.UtcNow.Subtract(timespan)));
    }
  }
}