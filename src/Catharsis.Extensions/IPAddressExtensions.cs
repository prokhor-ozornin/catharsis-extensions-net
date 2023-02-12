using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="IPAddress"/>
public static class IPAddressExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsAvailable(this IPAddress address, TimeSpan? timeout = null)
  {
    if (address is null) throw new ArgumentNullException(nameof(address));

    using var ping = new Ping();

    var reply = timeout is not null ? ping.Send(address, (int) timeout.Value.TotalMilliseconds) : ping.Send(address);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<bool> IsAvailableAsync(this IPAddress address, TimeSpan? timeout = null)
  {
    if (address is null) throw new ArgumentNullException(nameof(address));

    using var ping = new Ping();

    var reply = await (timeout is not null ? ping.SendPingAsync(address, (int) timeout.Value.TotalMilliseconds) : ping.SendPingAsync(address)).ConfigureAwait(false);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IPAddress Min(this IPAddress left, IPAddress right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Address <= right.Address ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IPAddress Max(this IPAddress left, IPAddress right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Address >= right.Address ? left : right;
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsV4(this IPAddress address) => address is not null ? address.AddressFamily == AddressFamily.InterNetwork : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsV6(this IPAddress address) => address is not null ? address.AddressFamily == AddressFamily.InterNetworkV6 : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IPHostEntry ToIpHost(this IPAddress address) => address is not null ? new IPHostEntry { AddressList = new[] { address }, Aliases = Array.Empty<string>() } : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] ToBytes(this IPAddress address) => address?.GetAddressBytes() ?? throw new ArgumentNullException(nameof(address));
}