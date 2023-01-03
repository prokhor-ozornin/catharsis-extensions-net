﻿using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for URI/URL type.</para>
/// </summary>
/// <seealso cref="Uri"/>
public static class UriExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public static bool IsAvailable(this Uri uri, TimeSpan? timeout = null) => uri.IsAvailableAsync(timeout).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<bool> IsAvailableAsync(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (uri.IsFile)
    {
      return uri.LocalPath.ToFile().Exists;
    }

    if (uri.Scheme == Uri.UriSchemeNetTcp)
    {
      return await uri.Host.ToIpHost().IsAvailableAsync();
    }

    if (uri.Scheme == Uri.UriSchemeHttp && uri.Scheme == Uri.UriSchemeHttps)
    {
      using var http = new HttpClient().WithTimeout(timeout);
      using var message = new HttpRequestMessage(HttpMethod.Head, uri);

      return (await http.SendAsync(message, cancellation).ConfigureAwait(false)).IsSuccessStatusCode;
    }

    throw new InvalidOperationException($"Unsupported URI scheme: {uri.Scheme}");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static UriBuilder Empty(this UriBuilder builder)
  {
    builder.Fragment = string.Empty;
    builder.Host = string.Empty;
    builder.Password = string.Empty;
    builder.Path = string.Empty;
    builder.Port = -1;
    builder.Query = string.Empty;
    builder.Scheme = string.Empty;
    builder.UserName = string.Empty;

    return builder;
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static IReadOnlyDictionary<string, string> GetQuery(this Uri uri)
  {
    var query = uri.Query;
    return query.IsEmpty() ? new Dictionary<string, string>() : HttpUtility.ParseQueryString(uri.Query).ToDictionary();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="parameters"></param>
  /// <returns></returns>
  public static UriBuilder WithQuery(this UriBuilder builder, IReadOnlyDictionary<string, object> parameters) => builder.WithQuery(parameters.ToValueTuple().AsArray());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="parameters"></param>
  /// <returns></returns>
  public static UriBuilder WithQuery(this UriBuilder builder, params (string Name, object Value)[] parameters)
  {
    var query = HttpUtility.ParseQueryString(builder.Query);

    foreach (var parameter in parameters)
    {
      query.Add(parameter.Name, parameter.Value?.ToInvariantString());
    }

    builder.Query = query.ToString() ?? string.Empty;

    return builder;
  }

  /// <summary>
  ///   <para>Resolves a host name or IP address part of target URL to <see cref="IPHostEntry"/> instance.</para>
  /// </summary>
  /// <param name="uri">URL address to use.</param>
  /// <returns><see cref="IPHostEntry"/> instance, containing information about host of source <see cref="Uri"/> address.</returns>
  public static IPHostEntry Host(this Uri uri) => Dns.GetHostEntry(uri.DnsSafeHost);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static IEnumerable<string> Lines(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    using var stream = uri.ToStream(timeout, headers);
    using var reader = stream.ToStreamReader(encoding);

    return reader.Lines();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<string> LinesAsync(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, [EnumeratorCancellation] CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    await using var stream = await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false);
    using var reader = stream.ToStreamReader(encoding);

    await foreach (var line in reader.LinesAsync().ConfigureAwait(false))
    {
      yield return line;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static T Print<T>(this T instance, Uri destination, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    using var stream = destination.ToStream(timeout, headers);

    return instance.Print(stream, encoding);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<T> PrintAsync<T>(this T instance, Uri destination, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    await using var stream = await destination.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false);

    return await instance.PrintAsync(stream, encoding, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="action"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static Uri TryFinallyDelete(this Uri uri, Action<Uri> action, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    Action<Uri> finalizer = null;

    if (uri.IsFile)
    {
      finalizer = uri => uri.LocalPath.ToFile().TryFinallyDelete(_ => action(uri));
    }
    else if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
    {
      async void Finalizer(Uri uri)
      {
        using var http = new HttpClient().WithTimeout(timeout).WithHeaders(headers);
        await http.ExecuteDeleteAsync(uri, cancellation).ConfigureAwait(false);
      }

      finalizer = Finalizer;
    }

    return uri.TryFinally(action, finalizer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> ToEnumerable(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false)).ToEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="count"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte[]>> ToEnumerable(this Uri uri, int count, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false)).ToEnumerable(count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> ToAsyncEnumerable(this Uri uri, TimeSpan? timeout = null, [EnumeratorCancellation] CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    await foreach (var element in (await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false)).ToAsyncEnumerable().ConfigureAwait(false))
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="count"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte[]> ToAsyncEnumerable(this Uri uri, int count, TimeSpan? timeout = null, [EnumeratorCancellation] CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    await foreach (var element in (await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false)).ToAsyncEnumerable(count).ConfigureAwait(false))
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static UriBuilder ToUriBuilder(this Uri uri) => new(uri);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static Stream ToStream(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri.ToStreamAsync(timeout, default, headers).Result;

  /// <summary>
  ///   <para>Opens a readable stream for the data downloaded from a resource with the specified <see cref="Uri"/>.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download the data.</param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
  /// <returns><see cref="System.IO.Stream"/> to read web server's response data from HTTP connection.</returns>
  public static async Task<Stream> ToStreamAsync(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (uri.IsFile)
    {
      return uri.LocalPath.ToFile().ToReadOnlyStream();
    }

    if (uri.Scheme == Uri.UriSchemeNetTcp)
    {
      using var tcp = new TcpClient(uri.Host, uri.Port).WithTimeout(timeout);
      return tcp.GetStream();
    }

    if (uri.Scheme == Uri.UriSchemeHttp && uri.Scheme == Uri.UriSchemeHttps)
    {
      using var http = new HttpClient().WithTimeout(timeout).WithHeaders(headers);
      return await http.ToStreamAsync(uri, cancellation).ConfigureAwait(false);
    }

    throw new InvalidOperationException($"Unsupported URI scheme: {uri.Scheme}");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <returns></returns>
  public static MailMessage ToMailMessage(this Uri uri)
  {
    var query = uri.GetQuery();

    var from = uri.Authority;
    var to = query["to"];
    var subject = query["subject"];
    var body = query["body"];
    var cc = query["cc"];
    var bcc = query["bcc"];

    var result = new MailMessage(from, to!, subject, body);

    if (cc != null)
    {
      result.CC.Add(cc);
    }

    if (bcc != null)
    {
      result.Bcc.Add(bcc);
    }

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    using var stream = uri.ToStream(timeout, headers);

    return stream.ToBytes();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static string ToText(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    using var stream = uri.ToStream(timeout, headers);
    return stream.ToText(encoding);
  }

  /// <summary>
  ///   <para>Downloads the resource with the specified <see cref="Uri"/> address and returns the result in a binary form.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download data.</param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names and values of object's public properties).</param>
  /// <returns>Response of web server in a binary format.</returns>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this Uri uri, TimeSpan? timeout = null, [EnumeratorCancellation] CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    await using var stream = await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false);

    await foreach (var value in stream.ToBytesAsync(cancellation).ConfigureAwait(false))
    {
      yield return value;
    }
  }

  /// <summary>
  ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download the data.</param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
  /// <returns>Web server's response in a text format.</returns>
  public static async Task<string> ToTextAsync(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    await using var stream = await uri.ToStreamAsync(timeout, cancellation, headers).ConfigureAwait(false);
    return await stream.ToTextAsync(encoding).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static Uri WriteBytes(this Uri destination, IEnumerable<byte> bytes, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => destination.WriteBytesAsync(bytes, timeout, default, headers).Result;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static Uri WriteText(this Uri destination, string text, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => destination.WriteBytes(text.AsArray().ToBytes(encoding), timeout, headers);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<Uri> WriteBytesAsync(this Uri destination, IEnumerable<byte> bytes, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (destination.IsFile)
    {
      await destination.LocalPath.ToFile().WriteBytesAsync(bytes).ConfigureAwait(false);
    }
    else if (destination.Scheme == Uri.UriSchemeNetTcp)
    {
      using var tcp = new TcpClient(destination.Host, destination.Port).WithTimeout(timeout);
      await tcp.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);
    }
    else if (destination.Scheme == Uri.UriSchemeHttp || destination.Scheme == Uri.UriSchemeHttps)
    {
      using var http = new HttpClient().WithTimeout(timeout).WithHeaders(headers);
      await http.WriteBytesAsync(bytes, destination, cancellation).ConfigureAwait(false);
    }

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<Uri> WriteTextAsync(this Uri destination, string text, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers) => await destination.WriteBytesAsync(text.AsArray().ToBytes(encoding), timeout, cancellation, headers).ConfigureAwait(false);

  /// <summary>
  ///   <para.
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, Uri destination, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    destination.WriteBytes(bytes, timeout, headers);
    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, Uri destination, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    destination.WriteBytes(text.ToBytes(encoding), timeout, headers);
    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, Uri destination, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    await destination.WriteBytesAsync(bytes, timeout, cancellation, headers).ConfigureAwait(false);
    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<string> WriteToAsync(this string text, Uri destination, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    await destination.WriteBytesAsync(text.ToBytes(encoding), timeout, cancellation, headers).ConfigureAwait(false);
    return text;
  }
}