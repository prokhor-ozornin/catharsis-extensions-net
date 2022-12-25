using System.Net;
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
  /// <returns></returns>
  public static IDictionary<string, string?> Query(this Uri uri)
  {
    var query = uri.Query;
    return query.IsEmpty() ? new Dictionary<string, string?>() : HttpUtility.ParseQueryString(uri.Query).ToDictionary();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="parameters"></param>
  /// <returns></returns>
  public static UriBuilder WithQuery(this UriBuilder builder, IDictionary<string, object?> parameters) => builder.WithQuery(((string Name, object? Value)[]) parameters.ToTuple());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="parameters"></param>
  /// <returns></returns>
  public static UriBuilder WithQuery(this UriBuilder builder, params (string Name, object? Value)[] parameters)
  {
    var query = HttpUtility.ParseQueryString(builder.Query);

    foreach (var parameter in parameters)
    {
      query.Add(parameter.Name, parameter.Value?.ToStringInvariant());
    }

    builder.Query = query.ToString();

    return builder;
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
  ///   <para>Downloads the resource with the specified <see cref="Uri"/> address and returns the result in a binary form.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download data.</param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names and values of object's public properties).</param>
  /// <returns>Response of web server in a binary format.</returns>
  public static async IAsyncEnumerable<byte> Bytes(this Uri uri, TimeSpan? timeout = null, [EnumeratorCancellation] CancellationToken cancellation = default, params (string Name, object? Value)[] headers)
  {
    await using var stream = await uri.ToStream(timeout, headers);

    await foreach (var value in stream.Bytes(cancellation))
    {
      yield return value;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="bytes"></param>
  /// <param name="headers"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Uri> Bytes(this Uri uri, IEnumerable<byte> bytes, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object? Value)[] headers)
  {
    if (uri.IsFile)
    {
      await uri.LocalPath.ToFile().Bytes(bytes, cancellation);
    } 
    else if (uri.Scheme == Uri.UriSchemeNetTcp)
    {
      using var tcp = new TcpClient(uri.Host, uri.Port).Timeout(timeout);
      await tcp.Bytes(bytes, cancellation);
    }
    else if (uri.Scheme == Uri.UriSchemeMailto)
    {
      using var smtp = new SmtpClient(uri.Host, uri.Port).Timeout(timeout);

      var email = uri.ToMailMessage();

      if (email.Body.IsEmpty())
      {
        email.Body = bytes.Text();
      }

      #if NET6_0
        await smtp.SendMailAsync(email, cancellation);
      #else
        await smtp.SendMailAsync(email);
      #endif
    }
    else if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
    {
      using var http = new HttpClient().Timeout(timeout).Headers(headers);
      await http.Bytes(uri, bytes, cancellation);
    }

    return uri;
  }

  /// <summary>
  ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download the data.</param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
  /// <returns>Web server's response in a text format.</returns>
  public static async Task<string> Text(this Uri uri, Encoding? encoding = null, TimeSpan? timeout = null, params (string Name, object? Value)[] headers)
  {
    await using var stream = await uri.ToStream(timeout, headers);
    return await stream.Text(encoding);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="text"></param>
  /// <param name="headers"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Uri> Text(this Uri uri, string text, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object? Value)[] headers)
  {
    if (uri.IsFile)
    {
      await uri.LocalPath.ToFile().Text(text, cancellation);
    }
    else if (uri.Scheme == Uri.UriSchemeNetTcp)
    {
      using var tcp = new TcpClient(uri.Host, uri.Port).Timeout(timeout);
      await tcp.Text(text, cancellation);
    }
    else if (uri.Scheme == Uri.UriSchemeMailto)
    {
      using var smtp = new SmtpClient(uri.Host, uri.Port).Timeout(timeout);

      var email = uri.ToMailMessage();

      if (email.Body.IsEmpty())
      {
        email.Body = text;
      }
      
      #if NET6_0
        await smtp.SendMailAsync(email, cancellation);
      #else
       await smtp.SendMailAsync(email);
      #endif
    }
    else if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
    {
      using var http = new HttpClient().Timeout(timeout).Headers(headers);
      await http.Text(uri, text, cancellation);
    }

    return uri;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<string> Lines(this Uri uri, Encoding? encoding = null, TimeSpan? timeout = null, params (string Name, object? Value)[] headers)
  {
    await using var stream = await uri.ToStream(timeout, headers);
    using var reader = stream.ToStreamReader(encoding);

    await foreach (var line in reader.Lines())
    {
      yield return line;
    }
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
  public static Uri UseTemporarily(this Uri uri, Action<Uri> action, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object? Value)[] headers)
  {
    Action<Uri>? finalizer = null;

    if (uri.IsFile)
    {
      finalizer = uri => uri.LocalPath.ToFile().UseTemporarily(_ => action(uri));
    }
    else if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
    {
      async void Finalizer(Uri uri)
      {
        using var http = new HttpClient().Timeout(timeout).Headers(headers);
        await http.RequestDelete(uri, cancellation);
      }

      finalizer = Finalizer;
    }

    return uri.UseFinally(action, finalizer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <param name="headers"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Uri> Print(this Uri destination, object instance, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object? Value)[] headers)
  {
    await (await destination.ToStream(timeout, headers)).Print(instance, cancellation: cancellation);

    return destination;
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
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<bool> IsAvailable(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    using var http = new HttpClient().Timeout(timeout);
    var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri), cancellation);
    return response.IsSuccessStatusCode;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> ToEnumerable(this Uri uri, TimeSpan? timeout = null, params (string Name, object? Value)[] headers) => (await uri.ToStream(timeout, headers)).ToEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="count"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte[]>> ToEnumerable(this Uri uri, int count, TimeSpan? timeout = null, params (string Name, object? Value)[] headers) => (await uri.ToStream(timeout, headers)).ToEnumerable(count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte> ToAsyncEnumerable(this Uri uri, TimeSpan? timeout = null, params (string Name, object? Value)[] headers)
  {
    await foreach (var item in (await uri.ToStream(timeout, headers)).ToAsyncEnumerable())
    {
      yield return item;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="count"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<byte[]> ToAsyncEnumerable(this Uri uri, int count, TimeSpan? timeout = null, params (string Name, object? Value)[] headers)
  {
    await foreach (var item in (await uri.ToStream(timeout, headers)).ToAsyncEnumerable(count))
    {
      yield return item;
    }
  }

  /// <summary>
  ///   <para>Opens a readable stream for the data downloaded from a resource with the specified <see cref="Uri"/>.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download the data.</param>
  /// <param name="timeout"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
  /// <returns><see cref="System.IO.Stream"/> to read web server's response data from HTTP connection.</returns>
  public static async Task<Stream> ToStream(this Uri uri, TimeSpan? timeout = null, params (string Name, object? Value)[] headers)
  {
    if (uri.IsFile)
    {
      return uri.LocalPath.ToFile().OpenRead();
    }

    if (uri.Scheme == Uri.UriSchemeNetTcp)
    {
      using var tcp = new TcpClient(uri.Host, uri.Port).Timeout(timeout);
      return tcp.GetStream();
    }

    if (uri.Scheme == Uri.UriSchemeHttp && uri.Scheme == Uri.UriSchemeHttps)
    {
      using var http = new HttpClient().Timeout(timeout).Headers(headers);
      return await http.ToStream(uri);
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
    var query = uri.Query();

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
  /// <returns></returns>
  public static UriBuilder ToUriBuilder(this Uri uri) => new(uri);
}