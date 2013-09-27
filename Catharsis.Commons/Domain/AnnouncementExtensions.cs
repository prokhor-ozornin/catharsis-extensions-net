using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Announcement"/>.</para>
  ///   <seealso cref="Announcement"/>
  /// </summary>
  public static class AnnouncementExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of announcements, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements to filter.</param>
    /// <param name="category">Category of announcements to search for.</param>
    /// <returns>Filtered sequence of announcements with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> InCategory(this IEnumerable<Announcement> announcements, AnnouncementsCategory category)
    {
      Assertion.NotNull(announcements);

      return category != null ? announcements.Where(announcement => announcement != null && announcement.Category.Id == category.Id) : announcements.Where(announcement => announcement != null && announcement.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of announcements by category's name in ascending order.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements for sorting.</param>
    /// <returns>Sorted sequence of announcements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByCategoryName(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderBy(announcement => announcement.Category.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of announcements by category's name in descending order.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements for sorting.</param>
    /// <returns>Sorted sequence of announcements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByCategoryNameDescending(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderByDescending(announcement => announcement.Category.Name);
    }

    /// <summary>
    ///   <para>Filters sequence of announcements, leaving those in specified currency.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements to filter.</param>
    /// <param name="currency">Currency of announcements to search for.</param>
    /// <returns>Filtered sequence of announcements with specified currency.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> WithCurrency(this IEnumerable<Announcement> announcements, string currency)
    {
      Assertion.NotNull(announcements);

      return announcements.Where(announcement => announcement != null && announcement.Currency == currency);
    }

    /// <summary>
    ///   <para>Sorts sequence of announcements by currency's ISO code in ascending order.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements for sorting.</param>
    /// <returns>Sorted sequence of announcements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByCurrency(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderBy(announcement => announcement.Currency);
    }

    /// <summary>
    ///   <para>Sorts sequence of announcements by currency's ISO code in descending order.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements for sorting.</param>
    /// <returns>Sorted sequence of announcements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByCurrencyDescending(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderByDescending(announcement => announcement.Currency);
    }

    /// <summary>
    ///   <para>Filters sequence of announcements, leaving those with a price in specified range.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements to filter.</param>
    /// <param name="from">Lower bound of price range.</param>
    /// <param name="to">Upper bound of price range.</param>
    /// <returns>Filtered sequence of announcements with a price between <paramref name="from"/> and <paramref name="to"/> inclusively.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> WithPrice(this IEnumerable<Announcement> announcements, decimal? from = null, decimal? to = null)
    {
      Assertion.NotNull(announcements);

      if (from.HasValue && to.HasValue)
      {
        return announcements.Where(announcement => announcement != null && announcement.Price >= from.Value && announcement.Price <= to.Value);
      }

      if (@from.HasValue)
      {
        return announcements.Where(announcement => announcement != null && announcement.Price >= from.Value);
      }

      return to.HasValue ? announcements.Where(announcement => announcement != null && announcement.Price <= to.Value) : announcements;
    }

    /// <summary>
    ///   <para>Sorts sequence of announcements by price in ascending order.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements for sorting.</param>
    /// <returns>Sorted sequence of announcements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByPrice(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderBy(announcement => announcement.Price);
    }

    /// <summary>
    ///   <para>Sorts sequence of announcements by price in descending order.</para>
    /// </summary>
    /// <param name="announcements">Source sequence of announcements for sorting.</param>
    /// <returns>Sorted sequence of announcements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByPriceDescending(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderByDescending(announcement => announcement.Price);
    }
  }
}