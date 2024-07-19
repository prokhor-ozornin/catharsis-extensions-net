namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="HttpContent"/>
public static class HttpContentExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToStreamAsync(HttpContent, CancellationToken)"/>
  public static Stream ToStream(this HttpContent content)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

    #if NET8_0
      return content.ReadAsStream();
    #else
      return content.ReadAsStreamAsync().Result;
    #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToStream(HttpContent)"/>
  public static async Task<Stream> ToStreamAsync(this HttpContent content, CancellationToken cancellation = default)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

    cancellation.ThrowIfCancellationRequested();

    #if NET8_0
      return await content.ReadAsStreamAsync(cancellation).ConfigureAwait(false);
    #else
        return await content.ReadAsStreamAsync().ConfigureAwait(false);
    #endif
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytesAsync(HttpContent)"/>
  public static IEnumerable<byte> ToBytes(this HttpContent content) => content?.ToStream().ToBytes() ?? throw new ArgumentNullException(nameof(content));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytes(HttpContent)"/>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this HttpContent content)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

    var result = content.ReadAsByteArrayAsync().ConfigureAwait(false);

    foreach (var value in await result)
    {
      yield return value;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTextAsync(HttpContent, CancellationToken)"/>
  public static string ToText(this HttpContent content) => content?.ToTextAsync().Result ?? throw new ArgumentNullException(nameof(content));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="content"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(HttpContent)"/>
  public static async Task<string> ToTextAsync(this HttpContent content, CancellationToken cancellation = default)
  {
    if (content is null) throw new ArgumentNullException(nameof(content));

    cancellation.ThrowIfCancellationRequested();

    #if NET8_0
      return await content.ReadAsStringAsync(cancellation).ConfigureAwait(false);
    #else
      return await content.ReadAsStringAsync().ConfigureAwait(false);
    #endif
  }
}