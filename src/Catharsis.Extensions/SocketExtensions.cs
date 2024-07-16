using System.Net.Sockets;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="Socket"/>
public static class SocketExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="socket"></param>
  /// <param name="timeout"></param>
  /// <returns>Back self-reference to the given <paramref name="socket"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="socket"/> is <see langword="null"/>.</exception>
  public static Socket WithTimeout(this Socket socket, TimeSpan? timeout)
  {
    if (socket is null) throw new ArgumentNullException(nameof(socket));

    if (timeout is null)
    {
      return socket;
    }

    socket.ReceiveTimeout = (int) timeout.Value.TotalMilliseconds;
    socket.SendTimeout = (int) timeout.Value.TotalMilliseconds;

    return socket;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="socket"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="socket"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="socket"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static Socket TryFinallyDisconnect(this Socket socket, Action<Socket> action)
  {
    if (socket is null) throw new ArgumentNullException(nameof(socket));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return socket.TryFinally(action, x => x.Disconnect(true));
  }
}