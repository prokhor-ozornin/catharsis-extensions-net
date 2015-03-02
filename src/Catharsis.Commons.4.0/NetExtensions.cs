using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of network-related extensions methods.</para>
  /// </summary>
  public static class NetExtensions
  {
    /// <summary>
    ///   <para>Resolves a host name or IP address part of target URL to <see cref="IPHostEntry"/> instance.</para>
    /// </summary>
    /// <param name="self">URL address to use.</param>
    /// <returns><see cref="IPHostEntry"/> instance, containing information about host of source <see cref="Uri"/> address.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static IPHostEntry Host(this Uri self)
    {
      Assertion.NotNull(self);

      return Dns.GetHostEntry(self.DnsSafeHost);
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="IPAddress"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="IPAddress"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static IPAddress IPAddress(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is IPAddress)
      {
        return subject as IPAddress;
      }

      IPAddress result;
      if (System.Net.IPAddress.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
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
    /// <seealso cref="Stream(Uri, object)"/>
    public static Stream Stream(this Uri self, IDictionary<string, object> headers = null)
    {
      Assertion.NotNull(self);

      if (self.Scheme == Uri.UriSchemeFile)
      {
        return new FileStream(self.LocalPath, FileMode.Open, FileAccess.Read);
      }

      var request = WebRequest.Create(self);

      if (headers != null)
      {
        foreach (var header in headers)
        {
          request.Headers[header.Key] = header.Value.ToStringInvariant();
        }
      }

      return request.GetResponse().GetResponseStream();
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
    /// <seealso cref="Stream(Uri, IDictionary{string, object})"/>
    /// <seealso cref="Stream(Uri, object)"/>
    public static Stream Stream(this Uri self, IDictionary<string, string> headers = null)
    {
      Assertion.NotNull(self);

      if (self.Scheme == Uri.UriSchemeFile)
      {
        return new FileStream(self.LocalPath, FileMode.Open, FileAccess.Read);
      }

      var request = WebRequest.Create(self);

      if (headers != null)
      {
        foreach (var header in headers)
        {
          request.Headers[header.Key] = header.Value;
        }
      }

      return request.GetResponse().GetResponseStream();
    }
  }
}