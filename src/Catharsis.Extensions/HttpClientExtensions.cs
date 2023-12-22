﻿namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="HttpClient"/>
public static class HttpClientExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpClient WithHeaders(this HttpClient http, IEnumerable<(string Name, object Value)> headers)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (headers is null) throw new ArgumentNullException(nameof(headers));

    headers.ForEach(header => http.DefaultRequestHeaders.Add(header.Name, header.Value?.ToInvariantString()));

    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpClient WithHeaders(this HttpClient http, params (string Name, object Value)[] headers) => http.WithHeaders(headers as IEnumerable<(string Name, object Value)>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpClient WithHeaders(this HttpClient http, IReadOnlyDictionary<string, object> headers)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (headers is null) throw new ArgumentNullException(nameof(headers));

    headers.ForEach(header => http.DefaultRequestHeaders.Add(header.Key, header.Value?.ToInvariantString()));

    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpClient WithTimeout(this HttpClient http, TimeSpan? timeout)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));

    if (timeout is not null)
    {
      http.Timeout = timeout.Value;
    }

    return http;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent ExecuteHead(this HttpClient http, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.ExecuteHeadAsync(uri).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> ExecuteHeadAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri), cancellation).ConfigureAwait(false);

    response.EnsureSuccessStatusCode();

    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent ExecuteGet(this HttpClient http, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.ExecuteGetAsync(uri).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> ExecuteGetAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    var response = await http.GetAsync(uri, cancellation).ConfigureAwait(false);

    response.EnsureSuccessStatusCode();

    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent ExecutePost(this HttpClient http, Uri uri, HttpContent content = null)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.ExecutePostAsync(uri, content).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> ExecutePostAsync(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    var response = await http.PostAsync(uri, content, cancellation).ConfigureAwait(false);

    response.EnsureSuccessStatusCode();

    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent ExecutePut(this HttpClient http, Uri uri, HttpContent content = null)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.ExecutePutAsync(uri, content).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> ExecutePutAsync(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    var response = await http.PutAsync(uri, content, cancellation).ConfigureAwait(false);

    response.EnsureSuccessStatusCode();

    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent ExecutePatch(this HttpClient http, Uri uri, HttpContent content = null)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.ExecutePatchAsync(uri, content).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> ExecutePatchAsync(this HttpClient http, Uri uri, HttpContent content = null, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    var response = await http.PatchAsync(uri, content, cancellation).ConfigureAwait(false);

    response.EnsureSuccessStatusCode();

    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent ExecuteDelete(this HttpClient http, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.ExecuteDeleteAsync(uri).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> ExecuteDeleteAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    var response = await http.DeleteAsync(uri, cancellation).ConfigureAwait(false);
    
    response.EnsureSuccessStatusCode();
    
    return response.Content;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Stream ToStream(this HttpClient http, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.ToStreamAsync(uri).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Stream> ToStreamAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    #if NET7_0_OR_GREATER
      return await http.GetStreamAsync(uri, cancellation).ConfigureAwait(false);
    #else
      return await http.GetStreamAsync(uri).ConfigureAwait(false);
    #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> ToBytes(this HttpClient http, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));
    
    return http.ToStream(uri).ToBytes(true);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this HttpClient http, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    await using var stream = await http.ToStreamAsync(uri);

    var result = stream.ToBytesAsync(true).ConfigureAwait(false);

    await foreach (var value in result)
    {
      yield return value;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this HttpClient http, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.ToTextAsync(uri).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<string> ToTextAsync(this HttpClient http, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    #if NET7_0_OR_GREATER
    return await http.GetStringAsync(uri, cancellation).ConfigureAwait(false);
    #else
      return await http.GetStringAsync(uri).ConfigureAwait(false);
    #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="bytes"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent WriteBytes(this HttpClient http, IEnumerable<byte> bytes, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.WriteBytesAsync(bytes, uri).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="bytes"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> WriteBytesAsync(this HttpClient http, IEnumerable<byte> bytes, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    using var content = new ByteArrayContent(bytes.AsArray());

    return await http.ExecutePostAsync(uri, content, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="text"></param>
  /// <param name="uri"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static HttpContent WriteText(this HttpClient http, string text, Uri uri)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    return http.WriteTextAsync(text, uri).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="http"></param>
  /// <param name="text"></param>
  /// <param name="uri"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<HttpContent> WriteTextAsync(this HttpClient http, string text, Uri uri, CancellationToken cancellation = default)
  {
    if (http is null) throw new ArgumentNullException(nameof(http));
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (uri is null) throw new ArgumentNullException(nameof(uri));

    cancellation.ThrowIfCancellationRequested();

    using var content = new StringContent(text);

    return await ExecutePostAsync(http, uri, content, cancellation).ConfigureAwait(false);
  }
}