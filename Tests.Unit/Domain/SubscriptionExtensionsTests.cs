using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="SubscriptionExtensions"/>.</para>
  /// </summary>
  public sealed class SubscriptionExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="SubscriptionExtensions.Active(IEnumerable{Subscription})"/> method.</para>
    /// </summary>
    [Fact]
    public void Active_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SubscriptionExtensions.Active(null));

      Assert.False(Enumerable.Empty<Subscription>().Active().Any());
      Assert.True(new[] { null, new Subscription { Active = true }, null, new Subscription { Active = false } }.Active().Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SubscriptionExtensions.Inactive(IEnumerable{Subscription})"/> method.</para>
    /// </summary>
    [Fact]
    public void Inactive_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SubscriptionExtensions.Inactive(null));

      Assert.False(Enumerable.Empty<Subscription>().Inactive().Any());
      Assert.True(new[] { null, new Subscription { Active = true }, null, new Subscription { Active = false } }.Inactive().Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SubscriptionExtensions.WithToken(IEnumerable{Subscription}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithToken_Method()
    {
      Assert.Throws<ArgumentNullException>(() => SubscriptionExtensions.WithToken(null, string.Empty));

      Assert.True(Enumerable.Empty<Subscription>().WithToken(null) == null);
      Assert.True(Enumerable.Empty<Subscription>().WithToken(string.Empty) == null);
      Assert.True(new[] { null, new Subscription { Token = "Token" }, null, new Subscription { Token = "Token_2" } }.WithToken("Token") != null);
    }
  }
}