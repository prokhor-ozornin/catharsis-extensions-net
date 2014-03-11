using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Uri"/>.</para>
  /// </summary>
  /// <seealso cref="Uri"/>
  public static class UriExtensions
  {
    /// <summary>
    ///   <para>Downloads the resource with the specified <see cref="Uri"/> address and returns the result in a binary form.</para>
    /// </summary>
    /// <param name="uri">The URI from which to download data.</param>
    /// <param name="parameters">Optional set of URL query parameters, represented as public properties of the object.</param>
    /// <param name="headers">Optional set of additional headers (key-value pairs) to send alongside with request.</param>
    /// <returns>Response of web server in a binary format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="WebClient"/>
    public static byte[] Bytes(this Uri uri, object parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      return new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.GetType().GetProperties().Each(property => web.QueryString.Add(property.Name, parameters.Property(property.Name).ToString()));
        }

        if (headers != null)
        {
          headers.Each(header => web.Headers.Add(header.Key, header.Value));
        }

        return web.DownloadData(uri);
      });
    }

    /// <summary>
    ///   <para>Downloads the resource with the specified <see cref="Uri"/> to a local file.</para>
    /// </summary>
    /// <param name="uri">The URI from which to download data.</param>
    /// <param name="file">The name of the local file that is to receive the data.</param>
    /// <param name="parameters">Optional set of URL query parameters, represented as public properties of the object.</param>
    /// <param name="headers">Optional set of additional headers (key-value pairs) to send alongside with request.</param>
    /// <returns>Back reference to the current <see cref="Uri"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="file"/> is <see cref="string.Empty"/> string.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="WebClient"/>
    public static Uri DownloadFile(this Uri uri, string file, object parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);
      Assertion.NotEmpty(file);

      new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.GetType().GetProperties().Each(property => web.QueryString.Add(property.Name, parameters.Property(property.Name).ToString()));
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
    ///   <para>Opens a readable stream for the data downloaded from a resource with the specified <see cref="Uri"/>.</para>
    /// </summary>
    /// <param name="uri">The URI from which to download the data.</param>
    /// <param name="parameters">Optional set of URL query parameters, represented as public properties of the object.</param>
    /// <param name="headers">Optional set of additional headers (key-value pairs) to send alongside with request.</param>
    /// <returns><see cref="Stream"/> to read web server's response data from HTTP connection.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="WebClient"/>
    public static Stream Stream(this Uri uri, object parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      var web = new WebClient();

      if (parameters != null)
      {
        parameters.GetType().GetProperties().Each(property => web.QueryString.Add(property.Name, parameters.Property(property.Name).ToString()));
      }

      if (headers != null)
      {
        headers.Each(header => web.Headers.Add(header.Key, header.Value));
      }
      
      return web.OpenRead(uri);
    }

    /// <summary>
    ///   <para>Returns a <see cref="TextReader"/> to read data from a stream, opened for a resource with the specified <see cref="Uri"/>.</para>
    /// </summary>
    /// <param name="uri">The URI from which to download the data.</param>
    /// <param name="encoding">Encoding to be used by <see cref="TextReader"/> for conversion between response binary and text data. If not specified, default <see cref="Encoding.UTF8"/> encoding will be used.</param>
    /// <param name="parameters">Optional set of URL query parameters, represented as public properties of the object.</param>
    /// <param name="headers">Optional set of additional headers (key-value pairs) to send alongside with request.</param>
    /// <returns><see cref="TextReader"/> to read web server's response data from HTTP connection.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="WebClient"/>
    public static TextReader TextReader(this Uri uri, Encoding encoding = null, object parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      return uri.Stream(parameters, headers).TextReader(encoding);
    }

    /// <summary>
    ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
    /// </summary>
    /// <param name="uri">The URI from which to download the data.</param>
    /// <param name="parameters">Optional set of URL query parameters, represented as public properties of the object.</param>
    /// <param name="headers">Optional set of additional headers (key-value pairs) to send alongside with request.</param>
    /// <returns>Web server's response in a text format.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <seealso cref="WebClient"/>
    public static string Text(this Uri uri, object parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);

      return new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.GetType().GetProperties().Each(property => web.QueryString.Add(property.Name, parameters.Property(property.Name).ToString()));
        }

        if (headers != null)
        {
          headers.Each(header => web.Headers.Add(header.Key, header.Value));
        }

        return web.DownloadString(uri);
      });
    }

    /// <summary>
    ///   <para>Uploads binary data to a resource identifier by a <see cref="Uri"/>.</para>
    /// </summary>
    /// <param name="uri">The URI of the resource to receive the data.</param>
    /// <param name="data">Binary data to send to the resource.</param>
    /// <param name="parameters">Optional set of URL query parameters, represented as public properties of the object.</param>
    /// <param name="headers">Optional set of additional HTTP headers (key-value pairs) to send alongside with HTTP request.</param>
    /// <returns>Back reference to the current <see cref="Uri"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="data"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the STOR command to upload an FTP resource. For an HTTP resource, the POST method is used.</remarks>
    /// <seealso cref="WebClient"/>
    /// <seealso cref="Upload(Uri, string, object, IEnumerable{KeyValuePair{string, string}})"/>
    public static Uri Upload(this Uri uri, byte[] data, object parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(data);

      new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.GetType().GetProperties().Each(property => web.QueryString.Add(property.Name, parameters.Property(property.Name).ToString()));
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
    ///   <para>Uploads the specified string to the specified resource.</para>
    /// </summary>
    /// <param name="uri">The URI of the resource to receive the string. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method.</param>
    /// <param name="data">The string to be uploaded.</param>
    /// <param name="parameters">Optional set of URL query parameters, represented as public properties of the object.</param>
    /// <param name="headers">Optional set of additional HTTP headers (key-value pairs) to send alongside with HTTP request.</param>
    /// <returns>Web server's response in a text format.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="data"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="WebClient"/>
    /// <seealso cref="Upload(Uri, byte[], object , IEnumerable{KeyValuePair{string, string}})"/>
    public static Uri Upload(this Uri uri, string data, object parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(data);

      new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.GetType().GetProperties().Each(property => web.QueryString.Add(property.Name, parameters.Property(property.Name).ToString()));
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
    ///   <para>Uploads a local file to a resource with the specified <see cref="Uri"/>.</para>
    /// </summary>
    /// <param name="uri">The URI of the resource to receive the file.</param>
    /// <param name="file">The file to send to the resource.</param>
    /// <param name="parameters">Optional set of URL query parameters, represented as public properties of the object.</param>
    /// <param name="headers">Optional set of additional HTTP headers (key-value pairs) to send alongside with HTTP request.</param>
    /// <returns>Array of bytes, containing the body of the response from the resource.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="file"/> is <see cref="string.Empty"/> string.</exception>
    /// <remarks>This method uses the STOR command to upload an FTP resource. For an HTTP resource, the POST method is used.</remarks>
    /// <seealso cref="WebClient"/>
    public static Uri UploadFile(this Uri uri, string file, object parameters = null, IEnumerable<KeyValuePair<string, string>> headers = null)
    {
      Assertion.NotNull(uri);
      Assertion.NotNull(file);

      new WebClient().With(web =>
      {
        if (parameters != null)
        {
          parameters.GetType().GetProperties().Each(property => web.QueryString.Add(property.Name, parameters.Property(property.Name).ToString()));
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