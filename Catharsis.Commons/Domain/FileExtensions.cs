using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="File"/>.</para>
  ///   <seealso cref="File"/>
  /// </summary>
  public static class FileExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of files, leaving those with specified MIME content type.</para>
    /// </summary>
    /// <param name="files">Source sequence of files to filter.</param>
    /// <param name="contentType">MIME content type of files to search for.</param>
    /// <returns>Filtered sequence of files with specified MIME content type.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> WithContentType(this IEnumerable<File> files, string contentType)
    {
      Assertion.NotNull(files);

      return files.Where(file => file != null && file.ContentType == contentType);
    }

    /// <summary>
    ///   <para>Sorts sequence of files by MIME content type in ascending order.</para>
    /// </summary>
    /// <param name="files">Source sequence of files for sorting.</param>
    /// <returns>Sorted sequence of files.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> OrderByContentType(this IEnumerable<File> files)
    {
      Assertion.NotNull(files);

      return files.OrderBy(file => file.ContentType);
    }

    /// <summary>
    ///   <para>Sorts sequence of files by MIME content type in descending order.</para>
    /// </summary>
    /// <param name="files">Source sequence of files for sorting.</param>
    /// <returns>Sorted sequence of files.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> OrderByContentTypeDescending(this IEnumerable<File> files)
    {
      Assertion.NotNull(files);

      return files.OrderByDescending(file => file.ContentType);
    }

    /// <summary>
    ///   <para>Filters sequence of files, leaving those with specified original name.</para>
    /// </summary>
    /// <param name="files">Source sequence of files to filter.</param>
    /// <param name="name">Original name of files to search for.</param>
    /// <returns>Filtered sequence of files.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> WithOriginalName(this IEnumerable<File> files, string name)
    {
      Assertion.NotNull(files);

      return files.Where(file => file != null && file.OriginalName == name);
    }

    /// <summary>
    ///   <para>Sorts sequence of files by original name in ascending order.</para>
    /// </summary>
    /// <param name="files">Source sequence of files for sorting.</param>
    /// <returns>Filtered sequence of files.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> OrderByOriginalName(this IEnumerable<File> files)
    {
      Assertion.NotNull(files);

      return files.OrderBy(file => file.OriginalName);
    }

    /// <summary>
    ///   <para>Sort sequence of files by original name in descending order.</para>
    /// </summary>
    /// <param name="files">Source sequence of files for sorting.</param>
    /// <returns>Sorted sequence of file.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> OrderByOriginalNameDescending(this IEnumerable<File> files)
    {
      Assertion.NotNull(files);

      return files.OrderByDescending(file => file.OriginalName);
    }
  }
}