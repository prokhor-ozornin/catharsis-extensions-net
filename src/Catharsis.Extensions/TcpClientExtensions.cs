using System.Net.Sockets;
using System.Text;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="TcpClient"/>
public static class TcpClientExtensions
{
  /// <param name="client"></param>
  extension(TcpClient client)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty(TcpClient)"/>
    public bool IsUnset => client is null || client.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="TcpClient"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <value>If the specified <paramref name="client"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset(TcpClient)"/>
    public bool IsEmpty => client?.ToEnumerable().IsEmpty() ?? throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    public TcpClient WithTimeout(TimeSpan? timeout)
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
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public TcpClient TryFinallyDisconnect(Action<TcpClient> action)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (action is null) throw new ArgumentNullException(nameof(action));

      client.Client.TryFinallyDisconnect(_ => action(client));

      return client;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(TcpClient, int, bool)"/>
    public IEnumerable<byte> ToEnumerable(bool close = false) => client?.GetStream().ToEnumerable(close) ?? throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(TcpClient, bool)"/>
    public IEnumerable<byte[]> ToEnumerable(int count, bool close = false) => client?.GetStream().ToEnumerable(count, close) ?? throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToAsyncEnumerable(TcpClient, int, bool)"/>
    public IAsyncEnumerable<byte> ToAsyncEnumerable(bool close = false) => client?.GetStream().ToAsyncEnumerable(close) ?? throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToAsyncEnumerable(TcpClient, bool)"/>
    public IAsyncEnumerable<byte[]> ToAsyncEnumerable(int count, bool close = false) => client?.GetStream().ToAsyncEnumerable(count, close) ?? throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(TcpClient)"/>
    public IEnumerable<byte> ToBytes() => client?.GetStream().ToBytes() ?? throw new ArgumentNullException(nameof(client));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(TcpClient)"/>
    public async IAsyncEnumerable<byte> ToBytesAsync()
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
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(TcpClient, Encoding)"/>
    public string ToText(Encoding encoding = null) => client.ToBytes().AsArray().ToText(encoding);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(TcpClient, Encoding)"/>
    public async ValueTask<string> ToTextAsync(Encoding encoding = null) => (await client.ToBytesAsync().ToArrayAsync().ConfigureAwait(false)).ToText(encoding);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytesAsync(TcpClient, IEnumerable{byte}, CancellationToken)"/>
    public TcpClient WriteBytes(IEnumerable<byte> bytes)
    {
      if (client is null) throw new ArgumentNullException(nameof(client));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      client.GetStream().WriteBytes(bytes);

      return client;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes(TcpClient, IEnumerable{byte})"/>
    public async Task<TcpClient> WriteBytesAsync(IEnumerable<byte> bytes, CancellationToken cancellation = default)
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
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTextAsync(TcpClient, string, Encoding, CancellationToken)"/>
    public TcpClient WriteText(string text, Encoding encoding = null) => client.WriteBytes(text.ToBytes(encoding));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="client"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText(TcpClient, string, Encoding)"/>
    public async Task<TcpClient> WriteTextAsync(string text, Encoding encoding = null, CancellationToken cancellation = default) => await client.WriteBytesAsync(text.ToBytes(encoding), cancellation).ConfigureAwait(false);
  }
}