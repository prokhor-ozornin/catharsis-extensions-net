using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Category"/>.</para>
  ///   <seealso cref="Category"/>
  /// </summary>
  public static class CategoryExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of categories, leaving those belonging to specified parent.</para>
    /// </summary>
    /// <typeparam name="T">Concrete type of subject categories.</typeparam>
    /// <param name="categories">Source sequence of categories to filter.</param>
    /// <param name="parent">Parent category to search for it's child categories, or a <c>null</c> reference to return categories without parent (root level).</param>
    /// <returns>Filtered sequence of direct children of specified parent category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="categories"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithParent<T>(this IEnumerable<T> categories, T parent) where T : Category
    {
      Assertion.NotNull(categories);

      return parent != null ? categories.Where(category => category != null && category.Parent != null && category.Parent.Id == parent.Id) : categories.Where(category => category != null && category.Parent == null);
    }

    /// <summary>
    ///   <para>Filters sequence of categories, leaving those without parent (root level).</para>
    /// </summary>
    /// <typeparam name="T">Concrete type of subject categories.</typeparam>
    /// <param name="categories">Source sequence of categories to filter.</param>
    /// <returns>Filtered sequence of top level (root) categories, having no parent.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="categories"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Root<T>(this IEnumerable<T> categories) where T : Category
    {
      Assertion.NotNull(categories);

      return categories.WithParent(null);
    }
  }
}