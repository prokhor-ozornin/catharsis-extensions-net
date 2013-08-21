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
    public static IEnumerable<Announcement> InAnnouncementsCategory(this IEnumerable<Announcement> announcements, AnnouncementsCategory category)
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
    public static IEnumerable<Announcement> OrderByAnnouncementsCategoryName(this IEnumerable<Announcement> announcements)
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
    public static IEnumerable<Announcement> OrderByAnnouncementsCategoryNameDescending(this IEnumerable<Announcement> announcements)
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
    ///   <para></para>
    /// </summary>
    /// <param name="announcements"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByCurrency(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderBy(announcement => announcement.Currency);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="announcements"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByCurrencyDescending(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderByDescending(announcement => announcement.Currency);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="announcements"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
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

      if (to.HasValue)
      {
        return announcements.Where(announcement => announcement != null && announcement.Price <= to.Value);
      }

      return announcements;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="announcements"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByPrice(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderBy(announcement => announcement.Price);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="announcements"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="announcements"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Announcement> OrderByPriceDescending(this IEnumerable<Announcement> announcements)
    {
      Assertion.NotNull(announcements);

      return announcements.OrderByDescending(announcement => announcement.Price);
    }
  }
}