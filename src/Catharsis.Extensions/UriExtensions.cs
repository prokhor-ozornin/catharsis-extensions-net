using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Extensions;

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
  /// <exception cref="ArgumentNullException"></exception>
  public static Uri Clone(this Uri uri) => uri is not null ? new Uri(uri.OriginalString) : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsAvailable(this Uri uri, TimeSpan? timeout = null) => uri is not null ? uri.IsAvailableAsync(timeout).Result : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<bool> IsAvailableAsync(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

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
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IReadOnlyDictionary<string, string> GetQuery(this Uri uri)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    var query = uri.Query;

    return query.IsUnset() ? new Dictionary<string, string>() : HttpUtility.ParseQueryString(uri.Query).ToDictionary();
  }

  /// <summary>
  ///   <para>Resolves a host name or IP address part of target URL to <see cref="IPHostEntry"/> instance.</para>
  /// </summary>
  /// <param name="uri">URL address to use.</param>
  /// <returns><see cref="IPHostEntry"/> instance, containing information about host of source <see cref="Uri"/> address.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IPHostEntry GetHost(this Uri uri) => uri is not null ? Dns.GetHostEntry(uri.DnsSafeHost) : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string[] Lines(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var stream = uri.ToStream(timeout, headers);
    using var reader = stream.ToStreamReader(encoding);

    return reader.Lines().AsArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async IAsyncEnumerable<string> LinesAsync(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    await using var stream = await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false);
    using var reader = stream.ToStreamReader(encoding);

    await foreach (var line in reader.LinesAsync().ConfigureAwait(false))
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
  /// <exception cref="ArgumentNullException"></exception>
  public static Uri TryFinallyDelete(this Uri uri, Action<Uri> action, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));
    if (action is null) throw new ArgumentNullException(nameof(action));

    cancellation.ThrowIfCancellationRequested();

    Action<Uri> finalizer = null;

    if (uri.IsFile)
    {
      finalizer = link => link.LocalPath.ToFile().TryFinallyDelete(_ => action(link));
    }
    else if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
    {
      async void Finalizer(Uri link)
      {
        using var http = new HttpClient().WithTimeout(timeout).WithHeaders(headers);
        await http.ExecuteDeleteAsync(link, cancellation).ConfigureAwait(false);
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
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<IEnumerable<byte>> ToEnumerable(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="count"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async Task<IEnumerable<byte[]>> ToEnumerable(this Uri uri, int count, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));
    if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
    
    return (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToEnumerable(count);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async IAsyncEnumerable<byte> ToAsyncEnumerable(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    await foreach (var element in (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToAsyncEnumerable().ConfigureAwait(false))
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
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async IAsyncEnumerable<byte[]> ToAsyncEnumerable(this Uri uri, int count, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));
    if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

    await foreach (var element in (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToAsyncEnumerable(count).ConfigureAwait(false))
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static UriBuilder ToUriBuilder(this Uri uri) => uri is not null ? new UriBuilder(uri) : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Stream ToStream(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri is not null ? uri.ToStreamAsync(timeout, headers).Result : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para>Opens a readable stream for the data downloaded from a resource with the specified <see cref="Uri"/>.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download the data.</param>
  /// <param name="timeout"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
  /// <returns><see cref="System.IO.Stream"/> to read web server's response data from HTTP connection.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Stream> ToStreamAsync(this Uri uri, TimeSpan? timeout = null,params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

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
      return await http.ToStreamAsync(uri).ConfigureAwait(false);
    }

    throw new InvalidOperationException($"Unsupported URI scheme: {uri.Scheme}");
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static MailMessage ToMailMessage(this Uri uri)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    var query = uri.GetQuery();

    var from = uri.Authority;
    var to = query["to"];
    var subject = query["subject"];
    var body = query["body"];
    var cc = query["cc"];
    var bcc = query["bcc"];

    var result = new MailMessage(from, to!, subject, body);

    if (cc is not null)
    {
      result.CC.Add(cc);
    }

    if (bcc is not null)
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
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> ToBytes(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri?.ToStream(timeout, headers).ToBytes(true) ?? throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para>Downloads the resource with the specified <see cref="Uri"/> address and returns the result in a binary form.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download data.</param>
  /// <param name="timeout"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names and values of object's public properties).</param>
  /// <returns>Response of web server in a binary format.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    await using var stream = await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false);

    await foreach (var value in stream.ToBytesAsync().ConfigureAwait(false))
    {
      yield return value;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var stream = uri.ToStream(timeout, headers);
    
    return stream.ToText(encoding);
  }

  /// <summary>
  ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
  /// </summary>
  /// <param name="uri">The URI from which to download the data.</param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
  /// <returns>Web server's response in a text format.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<string> ToTextAsync(this Uri uri, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    await using var stream = await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false);
    
    return await stream.ToTextAsync(encoding).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlReader ToXmlReader(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri.ToStream(timeout, headers).ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<XmlReader> ToXmlReaderAsync(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlDictionaryReader ToXmlDictionaryReader(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri.ToStream(timeout, headers).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<XmlDictionaryReader> ToXmlDictionaryReaderAsync(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlDocument ToXmlDocument(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri is not null ? uri.ToXmlDocumentAsync(timeout, headers).Result : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<XmlDocument> ToXmlDocumentAsync(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var reader = await uri.ToXmlReaderAsync(timeout, headers).ConfigureAwait(false);

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XDocument ToXDocument(this Uri uri, TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri is not null ? uri.ToXDocumentAsync(timeout, default, headers).Result : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<XDocument> ToXDocumentAsync(this Uri uri, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    using var reader = await uri.ToXmlReaderAsync(timeout, headers).ConfigureAwait(false);

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Uri WriteBytes(this Uri destination, IEnumerable<byte> bytes, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return destination.WriteBytesAsync(bytes, timeout, default, headers).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Uri> WriteBytesAsync(this Uri destination, IEnumerable<byte> bytes, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    if (destination.IsFile)
    {
      await destination.LocalPath.ToFile().WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);
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
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Uri WriteText(this Uri destination, string text, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    return destination.WriteTextAsync(text, encoding, timeout, default, headers).Result;
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
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Uri> WriteTextAsync(this Uri destination, string text, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    return await destination.WriteBytesAsync(text.AsArray().ToBytes(encoding), timeout, cancellation, headers).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T DeserializeAsDataContract<T>(this Uri uri, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types) => uri is not null ? uri.DeserializeAsDataContractAsync<T>(timeout, headers, types).Result : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="uri"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> DeserializeAsDataContractAsync<T>(this Uri uri, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var reader = await uri.ToXmlReaderAsync(timeout, headers?.AsArray()).ConfigureAwait(false);

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="uri"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T DeserializeAsXml<T>(this Uri uri, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types) => uri is not null ? uri.DeserializeAsXmlAsync<T>(timeout, headers, types).Result : throw new ArgumentNullException(nameof(uri));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="uri"></param>
  /// <param name="types"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> DeserializeAsXmlAsync<T>(this Uri uri, TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
  {
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    using var reader = await uri.ToXmlReaderAsync(timeout, headers?.AsArray()).ConfigureAwait(false);

    return reader.DeserializeAsXml<T>(types);
  }
}