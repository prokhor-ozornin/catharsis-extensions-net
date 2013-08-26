using System;
using System.Collections.Generic;
using System.Xml.Linq;
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
    ///   <para>Performs testing of <see cref="Subscription.AuthorId"/> property.</para>
    /// </summary>
    [Fact]
    public void AuthorId_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Subscription { AuthorId = null });
      Assert.Throws<ArgumentException>(() => new Subscription { AuthorId = string.Empty });
      Assert.True(new Subscription { AuthorId = "authorId" }.AuthorId == "authorId");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.Active"/> property.</para>
    /// </summary>
    [Fact]
    public void Active_Property()
    {
      Assert.True(new Subscription { Active = true }.Active);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.DateCreated"/> property.</para>
    /// </summary>
    [Fact]
    public void DateCreated_Property()
    {
      Assert.True(new Subscription { DateCreated = DateTime.MinValue }.DateCreated == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.Email"/> property.</para>
    /// </summary>
    [Fact]
    public void Email_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Subscription { Email = null });
      Assert.Throws<ArgumentException>(() => new Subscription { Email = string.Empty });
      Assert.True(new Subscription { Email = "email" }.Email == "email");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.ExpiredOn"/> property.</para>
    /// </summary>
    [Fact]
    public void ExpiredOn_Property()
    {
      Assert.True(new Subscription { ExpiredOn = DateTime.MinValue }.ExpiredOn == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.LastUpdated"/> property.</para>
    /// </summary>
    [Fact]
    public void LastUpdated_Property()
    {
      Assert.True(new Subscription { LastUpdated = DateTime.MaxValue }.LastUpdated == DateTime.MaxValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.Token"/> property.</para>
    /// </summary>
    [Fact]
    public void Token_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new Subscription { Token = null });
      Assert.Throws<ArgumentException>(() => new Subscription { Token = string.Empty });
      Assert.True(new Subscription { Token = "token" }.Token == "token");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Subscription.Type"/> property.</para>
    /// </summary>
    [Fact]
    public void Type_Property()
    {
      Assert.True(new Subscription { Type = 1 }.Type == 1);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Subscription()"/>
    ///   <seealso cref="Subscription(IDictionary{string, object})"/>
    ///   <seealso cref="Subscription(string, string, string, int, DateTime?)"/>
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

      Assert.Throws<ArgumentNullException>(() => new Subscription(null));
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
      Assert.True(subscription.ExpiredOn == new DateTime(2000, 1, 1));
      Assert.True(subscription.LastUpdated <= DateTime.UtcNow);
      Assert.True(subscription.Token != null);
      Assert.True(subscription.Type == 1);

      Assert.Throws<ArgumentNullException>(() => new Subscription(null, "authorId", "email"));
      Assert.Throws<ArgumentNullException>(() => new Subscription("id", null, "email"));
      Assert.Throws<ArgumentNullException>(() => new Subscription("id", "authorId", null));
      Assert.Throws<ArgumentException>(() => new Subscription(string.Empty, "authorId", "email"));
      Assert.Throws<ArgumentException>(() => new Subscription("id", string.Empty, "email"));
      Assert.Throws<ArgumentException>(() => new Subscription("id", "authorId", string.Empty));
      subscription = new Subscription("id", "authorId", "email@mail.ru", 1, new DateTime(2000, 1, 1));
      Assert.True(subscription.Id == "id");
      Assert.True(subscription.AuthorId == "authorId");
      Assert.True(subscription.Active);
      Assert.True(subscription.DateCreated <= DateTime.UtcNow);
      Assert.True(subscription.Email == "email@mail.ru");
      Assert.True(subscription.ExpiredOn == new DateTime(2000, 1, 1));
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

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Subscription"/> type.</para>
    /// </summary>
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

 /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Subscription.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Subscription.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Subscription.Xml(null));

      var xml = new XElement("Subscription",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("Active", true),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Email", "email"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Token", "token"),
        new XElement("Type", 1));
      var subscription = Subscription.Xml(xml);
      Assert.True(subscription.Id == "id");
      Assert.True(subscription.Active);
      Assert.True(subscription.AuthorId == "authorId");
      Assert.True(subscription.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(subscription.Email == "email");
      Assert.False(subscription.ExpiredOn.HasValue);
      Assert.True(subscription.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(subscription.Token == "token");
      Assert.True(subscription.Type == 1);
      Assert.True(new Subscription("id", "authorId", "email", 1) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue, Token = "token" }.Xml().ToString() == xml.ToString());
      Assert.True(Subscription.Xml(subscription.Xml()).Equals(subscription));

      xml = new XElement("Subscription",
        new XElement("Id", "id"),
        new XElement("AuthorId", "authorId"),
        new XElement("Active", true),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Email", "email"),
        new XElement("ExpiredOn", DateTime.MaxValue.ToRFC1123()),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Token", "token"),
        new XElement("Type", 1));
      subscription = Subscription.Xml(xml);
      Assert.True(subscription.Id == "id");
      Assert.True(subscription.Active);
      Assert.True(subscription.AuthorId == "authorId");
      Assert.True(subscription.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(subscription.Email == "email");
      Assert.True(subscription.ExpiredOn.GetValueOrDefault().ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(subscription.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(subscription.Token == "token");
      Assert.True(subscription.Type == 1);
      Assert.True(new Subscription("id", "authorId", "email", 1, DateTime.MaxValue) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue, Token = "token" }.Xml().ToString() == xml.ToString());
      Assert.True(Subscription.Xml(subscription.Xml()).Equals(subscription));
    }
  }
}