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
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="categories"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="categories"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithParent<T>(this IEnumerable<T> categories, T parent) where T : Category
    {
      Assertion.NotNull(categories);

      return parent != null ? categories.Where(category => category != null && category.Parent != null && category.Parent.Id == parent.Id) : categories.Where(category => category != null && category.Parent == null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="categories"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="categories"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Root<T>(this IEnumerable<T> categories) where T : Category
    {
      Assertion.NotNull(categories);

      return categories.WithParent(null);
    }
  }
}