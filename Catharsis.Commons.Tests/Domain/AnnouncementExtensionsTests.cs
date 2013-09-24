using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="AnnouncementExtensions"/>.</para>
  /// </summary>
  public sealed class AnnouncementExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.InAnnouncementsCategory(IEnumerable{Announcement}, AnnouncementsCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InAnnouncementsCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.InAnnouncementsCategory(null, new AnnouncementsCategory()));

      Assert.False(Enumerable.Empty<Announcement>().InAnnouncementsCategory(null).Any());
      Assert.False(Enumerable.Empty<Announcement>().InAnnouncementsCategory(new AnnouncementsCategory()).Any());
      Assert.True(new[] { null, new Announcement { Category = new AnnouncementsCategory { Id = 1 } }, null, new Announcement { Category = new AnnouncementsCategory { Id = 2 } } }.InAnnouncementsCategory(new AnnouncementsCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.OrderByAnnouncementsCategoryName(IEnumerable{Announcement})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAnnouncementsCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.OrderByAnnouncementsCategoryName(null));
      Assert.Throws<NullReferenceException>(() => new Announcement[] { null }.OrderByAnnouncementsCategoryName().Any());

      var announcements = new[] { new Announcement { Category = new AnnouncementsCategory { Name = "Second" } }, new Announcement { Category = new AnnouncementsCategory { Name = "First" } } };
      Assert.True(announcements.OrderByAnnouncementsCategoryName().SequenceEqual(announcements.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.OrderByAnnouncementsCategoryNameDescending(IEnumerable{Announcement})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAnnouncementsCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.OrderByAnnouncementsCategoryNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Announcement[] { null }.OrderByAnnouncementsCategoryNameDescending().Any());

      var announcements = new[] { new Announcement { Category = new AnnouncementsCategory { Name = "First" } }, new Announcement { Category = new AnnouncementsCategory { Name = "Second" } } };
      Assert.True(announcements.OrderByAnnouncementsCategoryNameDescending().SequenceEqual(announcements.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.WithCurrency(IEnumerable{Announcement}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithCurrency_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.WithCurrency(null, "currency"));

      Assert.False(Enumerable.Empty<Announcement>().WithCurrency(null).Any());
      Assert.False(Enumerable.Empty<Announcement>().WithCurrency(string.Empty).Any());
      Assert.True(new[] { null, new Announcement { Currency = "Currency" }, null, new Announcement { Currency = "Currency_2" } }.WithCurrency("Currency").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.OrderByCurrency(IEnumerable{Announcement})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCurrency_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.OrderByCurrency(null));
      Assert.Throws<NullReferenceException>(() => new Announcement[] { null }.OrderByCurrency().Any());

      var announcements = new[] { new Announcement { Currency = "Second" }, new Announcement { Currency = "First" } };
      Assert.True(announcements.OrderByCurrency().SequenceEqual(announcements.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.OrderByCurrencyDescending(IEnumerable{Announcement})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCurrencyDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.OrderByCurrencyDescending(null));
      Assert.Throws<NullReferenceException>(() => new Announcement[] { null }.OrderByCurrencyDescending().Any());

      var announcements = new[] { new Announcement { Currency = "First" }, new Announcement { Currency = "Second" } };
      Assert.True(announcements.OrderByCurrencyDescending().SequenceEqual(announcements.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.WithPrice(IEnumerable{Announcement}, decimal?, decimal?)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithPrice_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.WithPrice(null));

      Assert.False(Enumerable.Empty<Announcement>().WithPrice(0, 0).Any());

      var entities = new[] { null, new Announcement { Price = 1 }, null, new Announcement { Price = 2 } };
      Assert.False(entities.WithPrice(0, 0).Any());
      Assert.True(entities.WithPrice(0, 1).Count() == 1);
      Assert.True(entities.WithPrice(1, 1).Count() == 1);
      Assert.True(entities.WithPrice(1, 2).Count() == 2);
      Assert.True(entities.WithPrice(2, 3).Count() == 1);
      Assert.False(entities.WithPrice(3, 3).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.OrderByPrice(IEnumerable{Announcement})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByPrice_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.OrderByPrice(null));
      Assert.Throws<NullReferenceException>(() => new Announcement[] { null }.OrderByPrice().Any());

      var announcements = new[] { new Announcement { Price = 2 }, new Announcement { Price = 1 } };
      Assert.True(announcements.OrderByPrice().SequenceEqual(announcements.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="AnnouncementExtensions.OrderByPriceDescending(IEnumerable{Announcement})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByPriceDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => AnnouncementExtensions.OrderByPriceDescending(null));
      Assert.Throws<NullReferenceException>(() => new Announcement[] { null }.OrderByPriceDescending().Any());

      var announcements = new[] { new Announcement { Price = 1 }, new Announcement { Price = 2 } };
      Assert.True(announcements.OrderByPriceDescending().SequenceEqual(announcements.Reverse()));
    }
  }
}