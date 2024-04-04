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
  /// <param name="tcp"></param>
  /// <returns></returns>
  public static bool IsUnset(this TcpClient tcp) => tcp is null || tcp.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this TcpClient tcp) => tcp?.ToEnumerable().IsEmpty() ?? throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TcpClient WithTimeout(this TcpClient tcp, TimeSpan? timeout)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    if (timeout is not null)
    {
      tcp.ReceiveTimeout = (int) timeout.Value.TotalMilliseconds;
      tcp.SendTimeout = (int) timeout.Value.TotalMilliseconds;
    }

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TcpClient TryFinallyDisconnect(this TcpClient tcp, Action<TcpClient> action)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));
    if (action is null) throw new ArgumentNullException(nameof(action));

    tcp.Client.TryFinallyDisconnect(_ => action(tcp));

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> ToEnumerable(this TcpClient tcp, bool close = false) => tcp?.GetStream().ToEnumerable(close) ?? throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="count"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte[]> ToEnumerable(this TcpClient tcp, int count, bool close = false) => tcp?.GetStream().ToEnumerable(count, close) ?? throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this TcpClient tcp, bool close = false) => tcp?.GetStream().ToAsyncEnumerable(close) ?? throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="count"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this TcpClient tcp, int count, bool close = false) => tcp?.GetStream().ToAsyncEnumerable(count, close) ?? throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> ToBytes(this TcpClient tcp) => tcp?.GetStream().ToBytes() ?? throw new ArgumentNullException(nameof(tcp));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this TcpClient tcp)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));

    await foreach (var element in tcp.GetStream().ToBytesAsync().ConfigureAwait(false))
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this TcpClient tcp, Encoding encoding = null) => tcp.ToBytes().AsArray().ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<string> ToTextAsync(this TcpClient tcp, Encoding encoding = null) => (await tcp.ToBytesAsync().ToArrayAsync().ConfigureAwait(false)).ToText(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TcpClient WriteBytes(this TcpClient tcp, IEnumerable<byte> bytes)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    tcp.GetStream().WriteBytes(bytes);

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<TcpClient> WriteBytesAsync(this TcpClient tcp, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (tcp is null) throw new ArgumentNullException(nameof(tcp));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    cancellation.ThrowIfCancellationRequested();

    await tcp.GetStream().WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return tcp;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TcpClient WriteText(this TcpClient tcp, string text, Encoding encoding = null) => tcp.WriteBytes(text.ToBytes(encoding));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="tcp"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<TcpClient> WriteTextAsync(this TcpClient tcp, string text, Encoding encoding = null, CancellationToken cancellation = default) => await tcp.WriteBytesAsync(text.ToBytes(encoding), cancellation).ConfigureAwait(false);
}