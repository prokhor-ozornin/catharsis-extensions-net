using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Subscription"/>.</para>
  /// </summary>
  public sealed class SubscriptionTests : EntityUnitTests<Subscription>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Subscription()"/>
    ///   <seealso cref="Subscription(IDictionary{string, object})"/>
    ///   <seealso cref="Subscription(string, string, int, DateTime?)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var subscription = new Subscription();
      Assert.True(subscription.Id == null);
      Assert.True(subscription.AuthorId == null);
      Assert.True(subscription.Active);
      Assert.True(subscription.DateCreated <= DateTime.UtcNow);
      Assert.True(subscription.Email == null);
      Assert.False(subscription.ExpiredOn.HasValue);
      Assert.True(subscription.LastUpdated <= DateTime.UtcNow);
      Assert.True(subscription.Token != null);
      Assert.True(subscription.Type == 0);

      subscription = new Subscription(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Active", false)
        .AddNext("Email", "email@mail.ru")
        .AddNext("ExpiredOn", new DateTime(2000, 1, 1))
        .AddNext("Type", 1));
      Assert.True(subscription.Id == "id");
      Assert.True(subscription.AuthorId == "authorId");
      Assert.False(subscription.Active);
      Assert.True(subscription.DateCreated <= DateTime.UtcNow);
      Assert.True(subscription.Email == "email@mail.ru");
      Assert.True(subscription.ExpiredOn.Value == new DateTime(2000, 1, 1));
      Assert.True(subscription.LastUpdated <= DateTime.UtcNow);
      Assert.True(subscription.Token != null);
      Assert.True(subscription.Type == 1);

      subscription = new Subscription("id", "authorId", "email@mail.ru", 1, new DateTime(2000, 1, 1));
      Assert.True(subscription.Id == "id");
      Assert.True(subscription.AuthorId == "authorId");
      Assert.True(subscription.Active);
      Assert.True(subscription.DateCreated <= DateTime.UtcNow);
      Assert.True(subscription.Email == "email@mail.ru");
      Assert.True(subscription.ExpiredOn.Value == new DateTime(2000, 1, 1));
      Assert.True(subscription.LastUpdated <= DateTime.UtcNow);
      Assert.True(subscription.Token != null);
      Assert.True(subscription.Type == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Subscription { Email = "email@mail.ru" }.ToString() == "email@mail.ru");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Email", new[] { "email@mail.ru", "email@mail_2.ru" })
        .AddNext("Type", new[] { (object) 1, (object) 2 }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.CompareTo(Subscription)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Subscription { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new Subscription { DateCreated = new DateTime(2000, 1, 1) }) == 0);
      Assert.True(new Subscription { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new Subscription { DateCreated = new DateTime(2000, 1, 2) }) < 0);
    }
  }
}