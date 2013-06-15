using System;
using System.IO;
using System.Net;
using System.Text;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Uri"/>.</para>
  ///   <seealso cref="Uri"/>
  /// </summary>
  public static class UriExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static byte[] Bytes(this Uri uri)
    {
      Assertion.NotNull(uri);

      return new WebClient().With(web => web.DownloadData(uri));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="file"/> is <see cref="string.Empty"/> string.</exception>
    public static Uri DownloadFile(this Uri uri, string file)
    {
      Assertion.NotNull(uri);
      Assertion.NotEmpty(file);

      new WebClient().With(web => web.DownloadFile(uri, file));
      return uri;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is a <c>null</c> reference.</exception>
    public static string DownloadFile(this Uri uri)
    {
      Assertion.NotNull(uri);

      var file = Path.GetRandomFileName();
      uri.DownloadFile(file);
      return file;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Stream Stream(this Uri uri)
    {
      Assertion.NotNull(uri);

      return new WebClient().OpenRead(uri);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TextReader TextReader(this Uri uri, Encoding encoding = null)
    {
      Assertion.NotNull(uri);

      return uri.Stream().TextReader(encoding);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string Text(this Uri uri)
    {
      Assertion.NotNull(uri);

      return new WebClient().With(web => web.DownloadString(uri));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="data"/> is a <c>null</c> reference.</exception>
    public static Uri Upload(this Uri uri, byte[] data)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(data);

      new WebClient().With(web => web.UploadData(uri, data));
      return uri;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="data"/> is a <c>null</c> reference.</exception>
    public static Uri Upload(this Uri uri, string data)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(data);

      new WebClient().With(web => web.UploadString(uri, data));
      return uri;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="file"/> is <see cref="string.Empty"/> string.</exception>
    public static Uri UploadFile(this Uri uri, string file)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(file);

      new WebClient().With(web => web.UploadFile(uri, file));
      return uri;
    }
  }
}