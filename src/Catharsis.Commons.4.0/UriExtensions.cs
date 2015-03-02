using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

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
    /// <param name="self">The URI from which to download data.</param>
    /// <returns>Response of web server in a binary format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="Bytes(Uri, IDictionary{string, object})"/>
    /// <seealso cref="Bytes(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Bytes(Uri, object)"/>
    public static byte[] Bytes(this Uri self)
    {
      Assertion.NotNull(self);

      return self.Bytes((object) null);
    }

    /// <summary>
    ///   <para>Downloads the resource with the specified <see cref="Uri"/> address and returns the result in a binary form.</para>
    /// </summary>
    /// <param name="self">The URI from which to download data.</param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
    /// <returns>Response of web server in a binary format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="Bytes(Uri)"/>
    /// <seealso cref="Bytes(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Bytes(Uri, object)"/>
    public static byte[] Bytes(this Uri self, IDictionary<string, object> headers = null)
    {
      Assertion.NotNull(self);

      return self.Stream(headers).Bytes(true);
    }

    /// <summary>
    ///   <para>Downloads the resource with the specified <see cref="Uri"/> address and returns the result in a binary form.</para>
    /// </summary>
    /// <param name="self">The URI from which to download data.</param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
    /// <returns>Response of web server in a binary format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="Bytes(Uri)"/>
    /// <seealso cref="Bytes(Uri, IDictionary{string, object})"/>
    /// <seealso cref="Bytes(Uri, object)"/>
    public static byte[] Bytes(this Uri self, IDictionary<string, string> headers = null)
    {
      Assertion.NotNull(self);

      return self.Stream(headers).Bytes(true);
    }

    /// <summary>
    ///   <para>Downloads the resource with the specified <see cref="Uri"/> address and returns the result in a binary form.</para>
    /// </summary>
    /// <param name="self">The URI from which to download data.</param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names and values of object's public properties).</param>
    /// <returns>Response of web server in a binary format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="Bytes(Uri)"/>
    /// <seealso cref="Bytes(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Bytes(Uri, IDictionary{string, object})"/>
    public static byte[] Bytes(this Uri self, object headers = null)
    {
      Assertion.NotNull(self);

      return self.Bytes(headers != null ? headers.PropertiesMap() : null);
    }

    /// <summary>
    ///   <para>Modifies target URL address by adding by adding given list of name/value pairs to its Query component.</para>
    ///   <para>Names and values of query string parameters are URL-encoded.</para>
    /// </summary>
    /// <param name="self">URI address to be modified.</param>
    /// <param name="parameters">Map of name/value parameters for query string.</param>
    /// <returns>Updated URI address.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="parameters"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Query(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Query(Uri, object)"/>
    public static Uri Query(this Uri self, IDictionary<string, object> parameters)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(parameters);

      var builder = new UriBuilder(self);
      var query = parameters.Select(parameter => "{0}={1}".FormatSelf(parameter.Key.UrlEncode(), parameter.Value.ToStringInvariant().UrlEncode())).Join("&");
      builder.Query = builder.Query.Length > 1 ? builder.Query.Substring(1) + (query.IsEmpty() ? string.Empty : "&" + query) : query;
      return builder.Uri;
    }

    /// <summary>
    ///   <para>Modifies target URL address by adding by adding given list of name/value pairs to its Query component.</para>
    ///   <para>Names and values of query string parameters are URL-encoded.</para>
    /// </summary>
    /// <param name="self">URI address to be modified.</param>
    /// <param name="parameters">Map of name/value parameters for query string.</param>
    /// <returns>Updated URI address.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="parameters"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Query(Uri, object)"/>
    /// <seealso cref="Query(Uri, IDictionary{string, object})"/>
    public static Uri Query(this Uri self, IDictionary<string, string> parameters)
    {
      var builder = new UriBuilder(self);
      var query = parameters.Select(parameter => "{0}={1}".FormatSelf(parameter.Key.UrlEncode(), parameter.Value.UrlEncode())).Join("&");
      builder.Query = builder.Query.Length > 1 ? builder.Query.Substring(1) + (query.IsEmpty() ? string.Empty : "&" + query) : query;
      return builder.Uri;
    }

    /// <summary>
    ///   <para>Modifies target URL address by adding by adding given list of name/value pairs to its Query component.</para>
    ///   <para>Names and values of query string parameters are URL-encoded.</para>
    /// </summary>
    /// <param name="self">URI address to be modified.</param>
    /// <param name="parameters">Map of name/value parameters for query string (public properties of object).</param>
    /// <returns>Updated URI address.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="parameters"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Query(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Query(Uri, IDictionary{string, object})"/>
    public static Uri Query(this Uri self, object parameters)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(parameters);

      return self.Query(parameters.PropertiesMap());
    }

    /// <summary>
    ///   <para>Opens a readable stream for the data downloaded from a resource with the specified <see cref="Uri"/>.</para>
    /// </summary>
    /// <param name="self">The URI from which to download the data.</param>
    /// <returns><see cref="System.IO.Stream"/> to read web server's response data from HTTP connection.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="Stream(Uri, IDictionary{string, object})"/>
    /// <seealso cref="Stream(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Stream(Uri, object)"/>
    public static Stream Stream(this Uri self)
    {
      Assertion.NotNull(self);

      return self.Stream((object) null);
    }

    /// <summary>
    ///   <para>Opens a readable stream for the data downloaded from a resource with the specified <see cref="Uri"/>.</para>
    /// </summary>
    /// <param name="self">The URI from which to download the data.</param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
    /// <returns><see cref="System.IO.Stream"/> to read web server's response data from HTTP connection.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>This method uses the RETR command to download an FTP resource. For an HTTP resource, the GET method is used.</remarks>
    /// <seealso cref="Stream(Uri)"/>
    /// <seealso cref="Stream(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Stream(Uri, IDictionary{string, object})"/>
    public static Stream Stream(this Uri self, object headers = null)
    {
      Assertion.NotNull(self);

      return self.Stream(headers != null ? headers.PropertiesMap() : null);
    }

    /// <summary>
    ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
    /// </summary>
    /// <param name="self">The URI from which to download the data.</param>
    /// <returns>Web server's response in a text format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Text(Uri, IDictionary{string, object})"/>
    /// <seealso cref="Text(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Text(Uri, object)"/>
    public static string Text(this Uri self)
    {
      Assertion.NotNull(self);

      return self.Text((object) null);
    }

    /// <summary>
    ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
    /// </summary>
    /// <param name="self">The URI from which to download the data.</param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
    /// <returns>Web server's response in a text format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Text(Uri)"/>
    /// <seealso cref="Text(Uri, IDictionary{string, string})"/>
    /// <seealso cref="Text(Uri, object)"/>
    public static string Text(this Uri self, IDictionary<string, object> headers = null)
    {
      Assertion.NotNull(self);

      return self.Stream(headers).Text(true);
    }

    /// <summary>
    ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
    /// </summary>
    /// <param name="self">The URI from which to download the data.</param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
    /// <returns>Web server's response in a text format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Text(Uri)"/>
    /// <seealso cref="Text(Uri, object)"/>
    /// <seealso cref="Text(Uri, IDictionary{string, object}"/>
    public static string Text(this Uri self, IDictionary<string, string> headers = null)
    {
      Assertion.NotNull(self);

      return self.Stream(headers).Text(true);
    }

    /// <summary>
    ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
    /// </summary>
    /// <param name="self">The URI from which to download the data.</param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
    /// <returns>Web server's response in a text format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Text(Uri)"/>
    /// <seealso cref="Text(Uri, object)"/>
    /// <seealso cref="Text(Uri, IDictionary{string, object})"/>
    public static string Text(this Uri self, object headers = null)
    {
      Assertion.NotNull(self);

      return self.Text(headers != null ? headers.PropertiesMap() : null);
    }
  }
}