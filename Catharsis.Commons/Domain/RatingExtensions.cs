using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Rating"/>.</para>
  ///   <seealso cref="Rating"/>
  /// </summary>
  public static class RatingExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of ratings, leaving those belonging to given item.</para>
    /// </summary>
    /// <param name="ratings">Source sequence of ratings to filter.</param>
    /// <param name="item">Rated item to search for.</param>
    /// <returns>Filtered sequence of ratings, concerning specified item.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="ratings"/> or <paramref name="item"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Rating> WithItem(this IEnumerable<Rating> ratings, Item item)
    {
      Assertion.NotNull(ratings);
      Assertion.NotNull(item);

      return ratings.Where(rating => rating != null && rating.Item.Id == item.Id);
    }

    /// <summary>
    ///   <para>Filters sequence of ratings, leaving those with value in specified range.</para>
    /// </summary>
    /// <param name="ratings">Source sequence of ratings to filter.</param>
    /// <param name="from">Lower bound of value range.</param>
    /// <param name="to">Upper bound of value range.</param>
    /// <returns>Filtered sequence of ratings with duration ranging inclusively from <paramref name="from"/> to <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ratings"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Rating> WithValue(this IEnumerable<Rating> ratings, byte? from = null, byte? to = null)
    {
      Assertion.NotNull(ratings);

      if (from.HasValue && to.HasValue)
      {
        return ratings.Where(rating => rating != null && rating.Value >= from.Value && rating.Value <= to.Value);
      }

      if (from.HasValue)
      {
        return ratings.Where(rating => rating != null && rating.Value >= from.Value);
      }

      return to.HasValue ? ratings.Where(rating => rating != null && rating.Value <= to.Value) : ratings;
    }

    /// <summary>
    ///   <para>Sorts sequence of ratings by value in ascending order.</para>
    /// </summary>
    /// <param name="ratings">Source sequence of ratings for sorting.</param>
    /// <returns>Sorted sequence of ratings.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ratings"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Rating> OrderByValue(this IEnumerable<Rating> ratings)
    {
      Assertion.NotNull(ratings);

      return ratings.OrderBy(rating => rating.Value);
    }

    /// <summary>
    ///   <para>Sorts sequence of ratings by value in descending order.</para>
    /// </summary>
    /// <param name="ratings">Source sequence of ratings for sorting.</param>
    /// <returns>Sorted sequence of ratings.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ratings"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Rating> OrderByValueDescending(this IEnumerable<Rating> ratings)
    {
      Assertion.NotNull(ratings);

      return ratings.OrderByDescending(rating => rating.Value);
    }
  }
}