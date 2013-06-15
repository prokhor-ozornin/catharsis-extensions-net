using System.Collections.Generic;
using System.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="File"/>.</para>
  ///   <seealso cref="File"/>
  /// </summary>
  public static class FileExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="files"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> WithContentType(this IEnumerable<File> files, string contentType)
    {
      Assertion.NotNull(files);

      return files.Where(file => file != null && file.ContentType == contentType);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> OrderByContentType(this IEnumerable<File> files)
    {
      Assertion.NotNull(files);

      return files.OrderBy(file => file.ContentType);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> OrderByContentTypeDescending(this IEnumerable<File> files)
    {
      Assertion.NotNull(files);

      return files.OrderByDescending(file => file.ContentType);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="files"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> WithOriginalName(this IEnumerable<File> files, string name)
    {
      Assertion.NotNull(files);

      return files.Where(file => file != null && file.OriginalName == name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> OrderByOriginalName(this IEnumerable<File> files)
    {
      Assertion.NotNull(files);

      return files.OrderBy(file => file.OriginalName);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="files"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<File> OrderByOriginalNameDescending(this IEnumerable<File> files)
    {
      Assertion.NotNull(files);

      return files.OrderByDescending(file => file.OriginalName);
    }
  }
}