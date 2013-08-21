using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="BlogEntry"/>.</para>
  ///   <seealso cref="BlogEntry"/>
  /// </summary>
  public static class BlogEntryExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of blog entries, leaving those belonging to specified blog.</para>
    /// </summary>
    /// <param name="entries">Source sequence of entries to filter.</param>
    /// <param name="blog">Blog to search for.</param>
    /// <returns>Filtered sequence of entries in specified blog.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="entries"/> or <paramref name="blog"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<BlogEntry> InBlog(this IEnumerable<BlogEntry> entries, Blog blog)
    {
      Assertion.NotNull(entries);
      Assertion.NotNull(blog);

      return entries.Where(entry => entry != null && entry.Blog.Id == blog.Id);
    }

    /// <summary>
    ///   <para>Sorts sequence of blog entries by blog's name in ascending order.</para>
    /// </summary>
    /// <param name="entries">Source sequence of entries for sorting.</param>
    /// <returns>Sorted sequence of entries.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entries"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<BlogEntry> OrderByBlogName(this IEnumerable<BlogEntry> entries)
    {
      Assertion.NotNull(entries);

      return entries.OrderBy(entry => entry.Blog.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of blog entries by blog's name in descending order.</para>
    /// </summary>
    /// <param name="entries">Source sequence of entries for sorting.</param>
    /// <returns>Sorted sequence of entries.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entries"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<BlogEntry> OrderByBlogNameDescending(this IEnumerable<BlogEntry> entries)
    {
      Assertion.NotNull(entries);

      return entries.OrderByDescending(entry => entry.Blog.Name);
    }
  }
}