using System.Net.Sockets;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="Socket"/>
public static class SocketExtensions
{
  /// <param name="socket"></param>
  extension(Socket socket)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns>Back self-reference to the given <paramref name="socket"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="socket"/> is <see langword="null"/>.</exception>
    public Socket WithTimeout(TimeSpan? timeout)
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
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="socket"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="socket"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public Socket TryFinallyDisconnect(Action<Socket> action)
    {
      if (socket is null) throw new ArgumentNullException(nameof(socket));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return socket.TryFinally(action, x => x.Disconnect(true));
    }
  }
}