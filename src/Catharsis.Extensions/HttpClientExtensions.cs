namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="HttpClient"/>
public static class HttpClientExtensions
{
  /// <param name="client"></param>
  extension(HttpClient client)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    public HttpClient WithTimeout(TimeSpan? timeout)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));

      if (timeout is not null)
      {
        client.Timeout = timeout.Value;
      }

      return client;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="headers"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="headers"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WithHeaders(HttpClient, ValueTuple{string, object}[])"/>
    /// <seealso cref="WithHeaders(HttpClient, IReadOnlyDictionary{string, object})"/>
    public HttpClient WithHeaders(IEnumerable<(string Name, object Value)> headers)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (headers is null) throw new ArgumentNullException(nameof(headers));

      headers.ForEach(header => client.DefaultRequestHeaders.Add(header.Name, header.Value?.ToInvariantString()));

      return client;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="headers"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WithHeaders(HttpClient, IEnumerable{ValueTuple{string, object}})"/>
    /// <seealso cref="WithHeaders(HttpClient, IReadOnlyDictionary{string, object})"/>
    public HttpClient WithHeaders(params (string Name, object Value)[] headers) => client.WithHeaders(headers as IEnumerable<(string Name, object Value)>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="headers"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="headers"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WithHeaders(HttpClient, IEnumerable{ValueTuple{string, object}})"/>
    /// <seealso cref="WithHeaders(HttpClient, ValueTuple{string, object}[])"/>
    public HttpClient WithHeaders(IReadOnlyDictionary<string, object> headers)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (headers is null) throw new ArgumentNullException(nameof(headers));

      headers.ForEach(header => client.DefaultRequestHeaders.Add(header.Key, header.Value?.ToInvariantString()));

      return client;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecuteHeadAsync(HttpClient, Uri, CancellationToken)"/>
    public HttpContent ExecuteHead(Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.ExecuteHeadAsync(uri).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecuteHead(HttpClient, Uri)"/>
    public async Task<HttpContent> ExecuteHeadAsync(Uri uri, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri), cancellation).ConfigureAwait(false);

      response.EnsureSuccessStatusCode();

      return response.Content;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecuteGetAsync(HttpClient, Uri, CancellationToken)"/>
    public HttpContent ExecuteGet(Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.ExecuteGetAsync(uri).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecuteGet(HttpClient, Uri)"/>
    public async Task<HttpContent> ExecuteGetAsync(Uri uri, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      var response = await client.GetAsync(uri, cancellation).ConfigureAwait(false);

      response.EnsureSuccessStatusCode();

      return response.Content;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecutePostAsync(HttpClient, Uri, HttpContent, CancellationToken)"/>
    public HttpContent ExecutePost(Uri uri, HttpContent content = null)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.ExecutePostAsync(uri, content).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecutePost(HttpClient, Uri, HttpContent)"/>
    public async Task<HttpContent> ExecutePostAsync(Uri uri, HttpContent content = null, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      var response = await client.PostAsync(uri, content, cancellation).ConfigureAwait(false);

      response.EnsureSuccessStatusCode();

      return response.Content;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecutePutAsync(HttpClient, Uri, HttpContent, CancellationToken)"/>
    public HttpContent ExecutePut(Uri uri, HttpContent content = null)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.ExecutePutAsync(uri, content).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecutePut(HttpClient, Uri, HttpContent)"/>
    public async Task<HttpContent> ExecutePutAsync(Uri uri, HttpContent content = null, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      var response = await client.PutAsync(uri, content, cancellation).ConfigureAwait(false);

      response.EnsureSuccessStatusCode();

      return response.Content;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecuteDeleteAsync(HttpClient, Uri, CancellationToken)"/>
    public HttpContent ExecuteDelete(Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.ExecuteDeleteAsync(uri).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecuteDelete(HttpClient, Uri)"/>
    public async Task<HttpContent> ExecuteDeleteAsync(Uri uri, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      var response = await client.DeleteAsync(uri, cancellation).ConfigureAwait(false);
    
      response.EnsureSuccessStatusCode();
    
      return response.Content;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecutePatchAsync(HttpClient, Uri, HttpContent, CancellationToken)"/>
    public HttpContent ExecutePatch(Uri uri, HttpContent content = null)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.ExecutePatchAsync(uri, content).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ExecutePatch(HttpClient, Uri, HttpContent)"/>
    public async Task<HttpContent> ExecutePatchAsync(Uri uri, HttpContent content = null, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      var response = await client.PatchAsync(uri, content, cancellation).ConfigureAwait(false);

      response.EnsureSuccessStatusCode();

      return response.Content;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/>, <paramref name="bytes"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytesAsync(HttpClient, IEnumerable{byte}, Uri, CancellationToken)"/>
    public HttpContent WriteBytes(IEnumerable<byte> bytes, Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.WriteBytesAsync(bytes, uri).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="uri"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/>, <paramref name="bytes"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes(HttpClient, IEnumerable{byte}, Uri)"/>
    public async Task<HttpContent> WriteBytesAsync(IEnumerable<byte> bytes, Uri uri, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      using var content = new ByteArrayContent(bytes.AsArray());

      return await client.ExecutePostAsync(uri, content, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/>, <paramref name="text"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTextAsync(HttpClient, string, Uri, CancellationToken)"/>
    public HttpContent WriteText(string text, Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (text is null) throw new ArgumentNullException(nameof(text));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.WriteTextAsync(text, uri).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="uri"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/>, <paramref name="text"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText(HttpClient, string, Uri)"/>
    public async Task<HttpContent> WriteTextAsync(string text, Uri uri, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (text is null) throw new ArgumentNullException(nameof(text));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      using var content = new StringContent(text);

      return await ExecutePostAsync(client, uri, content, cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(HttpClient, Uri)"/>
    public IEnumerable<byte> ToBytes(Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));
    
      return client.ToStream(uri).ToBytes(true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(HttpClient, Uri)"/>
    public async IAsyncEnumerable<byte> ToBytesAsync(Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      await using var stream = await client.ToStreamAsync(uri);

      var result = stream.ToBytesAsync(true).ConfigureAwait(false);

      await foreach (var value in result)
      {
        yield return value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(HttpClient, Uri, CancellationToken)"/>
    public string ToText(Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.ToTextAsync(uri).Result;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(HttpClient, Uri)"/>
    public async Task<string> ToTextAsync(Uri uri, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      #if NET10_0_OR_GREATER
      return await client.GetStringAsync(uri, cancellation).ConfigureAwait(false);
      #else
      return await client.GetStringAsync(uri).ConfigureAwait(false);
      #endif
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToStreamAsync(HttpClient, Uri, CancellationToken)"/>
    public Stream ToStream(Uri uri)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      return client.ToStreamAsync(uri).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="uri"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToStream(HttpClient, Uri)"/>
    public async Task<Stream> ToStreamAsync(Uri uri, CancellationToken cancellation = default)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (uri is null) throw new ArgumentNullException(nameof(uri));

      cancellation.ThrowIfCancellationRequested();

      #if NET10_0_OR_GREATER
      return await client.GetStreamAsync(uri, cancellation).ConfigureAwait(false);
      #else
      return await client.GetStreamAsync(uri).ConfigureAwait(false);
      #endif
    }
  }
}