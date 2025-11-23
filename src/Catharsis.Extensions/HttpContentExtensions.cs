namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="HttpContent"/>
public static class HttpContentExtensions
{
  /// <param name="content"></param>
  extension(HttpContent content)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToStreamAsync(HttpContent, CancellationToken)"/>
    public Stream ToStream()
    {
      if (content is null) throw new ArgumentNullException(nameof(content));

      #if NET10_0_OR_GREATER
      return content.ReadAsStream();
      #else
      return content.ReadAsStreamAsync().Result;
      #endif
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public Stream Stream => content.ToStream();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToStream(HttpContent)"/>
    public async Task<Stream> ToStreamAsync(CancellationToken cancellation = default)
    {
      if (content is null) throw new ArgumentNullException(nameof(content));

      cancellation.ThrowIfCancellationRequested();

      #if NET10_0_OR_GREATER
      return await content.ReadAsStreamAsync(cancellation).ConfigureAwait(false);
      #else
      return await content.ReadAsStreamAsync().ConfigureAwait(false);
      #endif
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(HttpContent)"/>
    public IEnumerable<byte> ToBytes() => content?.ToStream().ToBytes() ?? throw new ArgumentNullException(nameof(content));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public byte[] Bytes => content.ToBytes().ToArray();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(HttpContent)"/>
    public async IAsyncEnumerable<byte> ToBytesAsync()
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(HttpContent, CancellationToken)"/>
    public string ToText() => content?.ToTextAsync().Result ?? throw new ArgumentNullException(nameof(content));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="content"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(HttpContent)"/>
    public async Task<string> ToTextAsync(CancellationToken cancellation = default)
    {
      if (content is null) throw new ArgumentNullException(nameof(content));

      cancellation.ThrowIfCancellationRequested();

      #if NET10_0_OR_GREATER
        return await content.ReadAsStringAsync(cancellation).ConfigureAwait(false);
      #else
        return await content.ReadAsStringAsync().ConfigureAwait(false);
      #endif
    }
  }
}