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
  /// <param name="uri"></param>
  extension(Uri uri)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="IsAvailableAsync(Uri, TimeSpan?, CancellationToken)"/>
    public bool IsAvailable(TimeSpan? timeout = null) => uri is not null ? uri.IsAvailableAsync(timeout).Result : throw new ArgumentNullException(nameof(uri));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public bool IsAvailable => uri.IsAvailable();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsAvailable(Uri, TimeSpan?)"/>
    public async Task<bool> IsAvailableAsync(TimeSpan? timeout = null, CancellationToken cancellation = default)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    public IReadOnlyDictionary<string, string> GetQuery()
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      var query = uri.Query;

      return query.IsUnset ? new Dictionary<string, string>() : HttpUtility.ParseQueryString(uri.Query).ToDictionary();
    }

    /// <summary>
    ///   <para>Resolves a host name or IP address part of target URL to <see cref="IPHostEntry"/> instance.</para>
    /// </summary>
    /// <returns><see cref="IPHostEntry"/> instance, containing information about host of source <see cref="Uri"/> address.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    public IPHostEntry GetHost() => uri is not null ? Dns.GetHostEntry(uri.DnsSafeHost) : throw new ArgumentNullException(nameof(uri));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="LinesAsync(Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/>
    public string[] Lines(Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      using var stream = uri.ToStream(timeout, headers);
      using var reader = stream.ToStreamReader(encoding);

      return reader.Lines().AsArray();
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string[] Lines => uri.Lines();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Lines(Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/>
    public async IAsyncEnumerable<string> LinesAsync(Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
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
    ///   <para>Creates a copy of the specified <see cref="Uri"/> with the same address as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    public Uri Clone() => uri is not null ? new Uri(uri.OriginalString) : throw new ArgumentNullException(nameof(uri));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <param name="headers"></param>
    /// <returns>Back self-reference to the given <paramref name="uri"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public Uri TryFinallyDelete(Action<Uri> action, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
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
    /// <typeparam name="T"></typeparam>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DeserializeAsDataContractAsync{T}(Uri, TimeSpan?, IEnumerable{ValueTuple{string, object}}, Type[])"/>
    public T DeserializeAsDataContract<T>(TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types) => uri is not null ? uri.DeserializeAsDataContractAsync<T>(timeout, headers, types).Result : throw new ArgumentNullException(nameof(uri));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DeserializeAsDataContract{T}(Uri, TimeSpan?, IEnumerable{ValueTuple{string, object}}, Type[])"/>
    public async Task<T> DeserializeAsDataContractAsync<T>(TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      using var reader = await uri.ToXmlReaderAsync(timeout, headers?.AsArray()).ConfigureAwait(false);

      return reader.DeserializeAsDataContract<T>(types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DeserializeAsXmlAsync{T}(Uri, TimeSpan?, IEnumerable{ValueTuple{string, object}}, Type[])"/>
    public T DeserializeAsXml<T>(TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types) => uri is not null ? uri.DeserializeAsXmlAsync<T>(timeout, headers, types).Result : throw new ArgumentNullException(nameof(uri));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DeserializeAsXml{T}(Uri, TimeSpan?, IEnumerable{ValueTuple{string, object}}, Type[])"/>
    public async Task<T> DeserializeAsXmlAsync<T>(TimeSpan? timeout = null, IEnumerable<(string Name, object Value)> headers = null, params Type[] types)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      using var reader = await uri.ToXmlReaderAsync(timeout, headers?.AsArray()).ConfigureAwait(false);

      return reader.DeserializeAsXml<T>(types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns>Back self-reference to the given <paramref name="uri"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytesAsync(Uri, IEnumerable{byte}, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/>
    public Uri WriteBytes(IEnumerable<byte> bytes, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      return uri.WriteBytesAsync(bytes, timeout, CancellationToken.None, headers).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes(Uri, IEnumerable{byte}, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<Uri> WriteBytesAsync(IEnumerable<byte> bytes, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      cancellation.ThrowIfCancellationRequested();

      if (uri.IsFile)
      {
        await uri.LocalPath.ToFile().WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);
      }
      else if (uri.Scheme == Uri.UriSchemeNetTcp)
      {
        using var tcp = new TcpClient(uri.Host, uri.Port).WithTimeout(timeout);
        await tcp.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);
      }
      else if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
      {
        using var http = new HttpClient().WithTimeout(timeout).WithHeaders(headers);
        await http.WriteBytesAsync(bytes, uri, cancellation).ConfigureAwait(false);
      }

      return uri;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns>Back self-reference to the given <paramref name="uri"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTextAsync(Uri, string, Encoding, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/>
    public Uri WriteText(string text, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));
      if (text is null) throw new ArgumentNullException(nameof(text));

      return uri.WriteTextAsync(text, encoding, timeout, CancellationToken.None, headers).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="uri"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText(Uri, string, Encoding, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<Uri> WriteTextAsync(string text, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));
      if (text is null) throw new ArgumentNullException(nameof(text));

      return await uri.WriteBytesAsync(text.AsArray().ToBytes(encoding), timeout, cancellation, headers).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(Uri, int, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<IEnumerable<byte>> ToEnumerable(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToEnumerable();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<IEnumerable<byte[]>> ToEnumerable(int count, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
    
      return (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToEnumerable(count);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToAsyncEnumerable(Uri, int, TimeSpan?, ValueTuple{string, object}[])"/>
    public async IAsyncEnumerable<byte> ToAsyncEnumerable(TimeSpan? timeout = null, params (string Name, object Value)[] headers)
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
    /// <param name="count"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="ToAsyncEnumerable(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async IAsyncEnumerable<byte[]> ToAsyncEnumerable(int count, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    public UriBuilder ToUriBuilder() => uri is not null ? new UriBuilder(uri) : throw new ArgumentNullException(nameof(uri));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    public MailMessage ToMailMessage()
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
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public IEnumerable<byte> ToBytes(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri?.ToStream(timeout, headers).ToBytes(true) ?? throw new ArgumentNullException(nameof(uri));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public byte[] Bytes => uri.ToBytes().ToArray();

    /// <summary>
    ///   <para>Downloads the resource with the specified <see cref="Uri"/> address and returns the result in a binary form.</para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names and values of object's public properties).</param>
    /// <returns>Response of web server in a binary format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async IAsyncEnumerable<byte> ToBytesAsync(TimeSpan? timeout = null, params (string Name, object Value)[] headers)
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
    /// <param name="encoding"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/>
    public string ToText(Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      using var stream = uri.ToStream(timeout, headers);
    
      return stream.ToText(encoding);
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string Text => uri.ToText();

    /// <summary>
    ///   <para>Downloads the requested resource as a <see cref="string"/>.</para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <param name="timeout"></param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
    /// <returns>Web server's response in a text format.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<string> ToTextAsync(Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      await using var stream = await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false);
    
      return await stream.ToTextAsync(encoding).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToStreamAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public Stream ToStream(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri is not null ? uri.ToStreamAsync(timeout, headers).Result : throw new ArgumentNullException(nameof(uri));

    /// <summary>
    ///   <para>Opens a readable stream for the data downloaded from a resource with the specified <see cref="Uri"/>.</para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers">Optional set of additional headers to send alongside with request (names/values).</param>
    /// <returns><see cref="System.IO.Stream"/> to read web server's response data from HTTP connection.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToStream(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<Stream> ToStreamAsync(TimeSpan? timeout = null, params (string Name, object Value)[] headers)
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
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXmlReaderAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public XmlReader ToXmlReader(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri.ToStream(timeout, headers).ToXmlReader();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXmlReader(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<XmlReader> ToXmlReaderAsync(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToXmlReader();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXmlDictionaryReaderAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public XmlDictionaryReader ToXmlDictionaryReader(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri.ToStream(timeout, headers).ToXmlDictionaryReader();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXmlDictionaryReader(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<XmlDictionaryReader> ToXmlDictionaryReaderAsync(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => (await uri.ToStreamAsync(timeout, headers).ConfigureAwait(false)).ToXmlDictionaryReader();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXmlDocumentAsync(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public XmlDocument ToXmlDocument(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri is not null ? uri.ToXmlDocumentAsync(timeout, headers).Result : throw new ArgumentNullException(nameof(uri));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXmlDocument(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<XmlDocument> ToXmlDocumentAsync(TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      using var reader = await uri.ToXmlReaderAsync(timeout, headers).ConfigureAwait(false);

      return reader.ToXmlDocument();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocumentAsync(Uri, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/>
    public XDocument ToXDocument(TimeSpan? timeout = null, params (string Name, object Value)[] headers) => uri is not null ? uri.ToXDocumentAsync(timeout, CancellationToken.None, headers).Result : throw new ArgumentNullException(nameof(uri));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocument(Uri, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<XDocument> ToXDocumentAsync(TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
    {
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      using var reader = await uri.ToXmlReaderAsync(timeout, headers).ConfigureAwait(false);

      return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
    }
  }
}