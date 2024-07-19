using System.Net.Sockets;
using System.Text;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="TcpClient"/>
public static class TcpClientExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(TcpClient)"/>
  public static bool IsUnset(this TcpClient client) => client is null || client.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="TcpClient"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="client">TCP client instance for evaluation.</param>
  /// <returns>If the specified <paramref name="client"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(TcpClient)"/>
  public static bool IsEmpty(this TcpClient client) => client?.ToEnumerable().IsEmpty() ?? throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="timeout"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  public static TcpClient WithTimeout(this TcpClient client, TimeSpan? timeout)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));

    if (timeout is not null)
    {
      client.ReceiveTimeout = (int) timeout.Value.TotalMilliseconds;
      client.SendTimeout = (int) timeout.Value.TotalMilliseconds;
    }

    return client;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static TcpClient TryFinallyDisconnect(this TcpClient client, Action<TcpClient> action)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));
    if (action is null) throw new ArgumentNullException(nameof(action));

    client.Client.TryFinallyDisconnect(_ => action(client));

    return client;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToEnumerable(TcpClient, int, bool)"/>
  public static IEnumerable<byte> ToEnumerable(this TcpClient client, bool close = false) => client?.GetStream().ToEnumerable(close) ?? throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="count"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToEnumerable(TcpClient, bool)"/>
  public static IEnumerable<byte[]> ToEnumerable(this TcpClient client, int count, bool close = false) => client?.GetStream().ToEnumerable(count, close) ?? throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToAsyncEnumerable(TcpClient, int, bool)"/>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this TcpClient client, bool close = false) => client?.GetStream().ToAsyncEnumerable(close) ?? throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="count"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToAsyncEnumerable(TcpClient, bool)"/>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this TcpClient client, int count, bool close = false) => client?.GetStream().ToAsyncEnumerable(count, close) ?? throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytesAsync(TcpClient)"/>
  public static IEnumerable<byte> ToBytes(this TcpClient client) => client?.GetStream().ToBytes() ?? throw new ArgumentNullException(nameof(client));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytes(TcpClient)"/>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this TcpClient client)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));

    await foreach (var element in client.GetStream().ToBytesAsync().ConfigureAwait(false))
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTextAsync(TcpClient, Encoding)"/>
  public static string ToText(this TcpClient client, Encoding encoding = null) => client.ToBytes().AsArray().ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(TcpClient, Encoding)"/>
  public static async Task<string> ToTextAsync(this TcpClient client, Encoding encoding = null) => (await client.ToBytesAsync().ToArrayAsync().ConfigureAwait(false)).ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="bytes"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="tcp"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytesAsync(TcpClient, IEnumerable{byte}, CancellationToken)"/>
  public static TcpClient WriteBytes(this TcpClient client, IEnumerable<byte> bytes)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    client.GetStream().WriteBytes(bytes);

    return client;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytes(TcpClient, IEnumerable{byte})"/>
  public static async Task<TcpClient> WriteBytesAsync(this TcpClient client, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    await client.GetStream().WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return client;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTextAsync(TcpClient, string, Encoding, CancellationToken)"/>
  public static TcpClient WriteText(this TcpClient client, string text, Encoding encoding = null) => client.WriteBytes(text.ToBytes(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="client"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteText(TcpClient, string, Encoding)"/>
  public static async Task<TcpClient> WriteTextAsync(this TcpClient client, string text, Encoding encoding = null, CancellationToken cancellation = default) => await client.WriteBytesAsync(text.ToBytes(encoding), cancellation).ConfigureAwait(false);
}