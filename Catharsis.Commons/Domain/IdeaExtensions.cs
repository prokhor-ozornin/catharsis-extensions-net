using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Idea"/>.</para>
  ///   <seealso cref="Idea"/>
  /// </summary>
  public static class IdeaExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of ideas, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="ideas">Source sequence of ideas to filter.</param>
    /// <param name="category">Category of ideas to search for.</param>
    /// <returns>Filtered sequence of ideas with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ideas"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Idea> InIdeasCategory(this IEnumerable<Idea> ideas, IdeasCategory category)
    {
      Assertion.NotNull(ideas);

      return category != null ? ideas.Where(idea => idea != null && idea.Category.Id == category.Id) : ideas.Where(idea => idea != null && idea.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of ideas by category's name in ascending order.</para>
    /// </summary>
    /// <param name="ideas">Source sequence of ideas for sorting.</param>
    /// <returns>Sorted sequence of ideas.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ideas"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Idea> OrderByIdeasCategoryName(this IEnumerable<Idea> ideas)
    {
      Assertion.NotNull(ideas);

      return ideas.OrderBy(idea => idea.Category.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of ideas by category's name in descending order.</para>
    /// </summary>
    /// <param name="ideas">Source sequence of ideas for sorting.</param>
    /// <returns>Sorted sequence of ideas.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ideas"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Idea> OrderByIdeasCategoryNameDescending(this IEnumerable<Idea> ideas)
    {
      Assertion.NotNull(ideas);

      return ideas.OrderByDescending(idea => idea.Category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ideas"></param>
    /// <param name="inspiredBy"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ideas"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Idea> InspiredByIdea(this IEnumerable<Idea> ideas, Idea inspiredBy)
    {
      Assertion.NotNull(ideas);
 
      return inspiredBy != null ? ideas.Where(idea => idea != null && idea.InspiredBy.Id == inspiredBy.Id) : ideas.Where(idea => idea != null && idea.InspiredBy.Id == null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ideas"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ideas"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Idea> OrderByInspiredByIdeaName(this IEnumerable<Idea> ideas)
    {
      Assertion.NotNull(ideas);

      return ideas.OrderBy(idea => idea.InspiredBy.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ideas"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="ideas"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Idea> OrderByInspiredByIdeaNameDescending(this IEnumerable<Idea> ideas)
    {
      Assertion.NotNull(ideas);

      return ideas.OrderByDescending(idea => idea.InspiredBy.Name);
    }
  }
}