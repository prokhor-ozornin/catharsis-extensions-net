using System;
using System.Collections.Generic;
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
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is a <c>null</c> reference.</exception>
    public static byte[] Bytes(this Uri uri, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      return new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.Each(parameter => web.QueryString.Add(parameter.Key, parameter.Value));
        }

        if (headers != null)
        {
          headers.Each(header => web.Headers.Add(header.Key, header.Value));
        }

        return web.DownloadData(uri);
      });
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="file"></param>
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="file"/> is <see cref="string.Empty"/> string.</exception>
    public static Uri DownloadFile(this Uri uri, string file, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);
      Assertion.NotEmpty(file);

      new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.Each(parameter => web.QueryString.Add(parameter.Key, parameter.Value));
        }

        if (headers != null)
        {
          headers.Each(header => web.Headers.Add(header.Key, header.Value));
        }

        web.DownloadFile(uri, file);
      });

      return uri;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is a <c>null</c> reference.</exception>
    public static string DownloadFile(this Uri uri, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      var file = Path.GetRandomFileName();
      uri.DownloadFile(file, parameters, headers);
      return file;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Stream Stream(this Uri uri, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      var web = new WebClient();

      if (parameters != null)
      {
        parameters.Each(parameter => web.QueryString.Add(parameter.Key, parameter.Value));
      }

      if (headers != null)
      {
        headers.Each(header => web.Headers.Add(header.Key, header.Value));
      }
      
      return web.OpenRead(uri);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="encoding"></param>
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TextReader TextReader(this Uri uri, Encoding encoding = null, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      return uri.Stream(parameters, headers).TextReader(encoding);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string Text(this Uri uri, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      return new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.Each(parameter => web.QueryString.Add(parameter.Key, parameter.Value));
        }

        if (headers != null)
        {
          headers.Each(header => web.Headers.Add(header.Key, header.Value));
        }

        return web.DownloadString(uri);
      });
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="data"></param>
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="data"/> is a <c>null</c> reference.</exception>
    public static Uri Upload(this Uri uri, byte[] data, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(data);

      new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.Each(parameter => web.QueryString.Add(parameter.Key, parameter.Value));
        }

        if (headers != null)
        {
          headers.Each(header => web.Headers.Add(header.Key, header.Value));
        }

        web.UploadData(uri, data);
      });

      return uri;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="data"></param>
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="data"/> is a <c>null</c> reference.</exception>
    public static Uri Upload(this Uri uri, string data, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(data);

      new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.Each(parameter => web.QueryString.Add(parameter.Key, parameter.Value));
        }

        if (headers != null)
        {
          headers.Each(header => web.Headers.Add(header.Key, header.Value));
        }

        web.UploadString(uri, data);
      });
    
      return uri;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="file"></param>
    /// <param name="parameters"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="file"/> is <see cref="string.Empty"/> string.</exception>
    public static Uri UploadFile(this Uri uri, string file, IEnumerable<KeyValuePair<string, string>> parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(file);

      new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.Each(parameter => web.QueryString.Add(parameter.Key, parameter.Value));
        }

        if (headers != null)
        {
          headers.Each(header => web.Headers.Add(header.Key, header.Value));
        }

        web.UploadFile(uri, file);
      });

      return uri;
    }
  }
}