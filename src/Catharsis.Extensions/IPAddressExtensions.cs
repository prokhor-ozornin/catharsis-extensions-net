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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsV6(IPAddress)"/>
  public static bool IsV4(this IPAddress address) => address is not null ? address.AddressFamily == AddressFamily.InterNetwork : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsV4(IPAddress)"/>
  public static bool IsV6(this IPAddress address) => address is not null ? address.AddressFamily == AddressFamily.InterNetworkV6 : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsAvailableAsync(IPAddress, TimeSpan?)"/>
  public static bool IsAvailable(this IPAddress address, TimeSpan? timeout = null)
  {
    if (address is null) throw new ArgumentNullException(nameof(address));

    using var ping = new Ping();

    var reply = timeout is not null ? ping.Send(address, (int) timeout.Value.TotalMilliseconds) : ping.Send(address);

    return reply?.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsAvailable(IPAddress, TimeSpan?)"/>
  public static async Task<bool> IsAvailableAsync(this IPAddress address, TimeSpan? timeout = null)
  {
    if (address is null) throw new ArgumentNullException(nameof(address));

    using var ping = new Ping();

    var reply = await (timeout is not null ? ping.SendPingAsync(address, (int) timeout.Value.TotalMilliseconds) : ping.SendPingAsync(address)).ConfigureAwait(false);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="IPAddress"/> with the same address bytes as the original.</para>
  /// </summary>
  /// <param name="address">IP address to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  public static IPAddress Clone(this IPAddress address) => address is not null ? new IPAddress(address.GetAddressBytes()) : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  public static IPHostEntry ToIpHost(this IPAddress address) => address is not null ? new IPHostEntry { AddressList = [address], Aliases = [] } : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  public static byte[] ToBytes(this IPAddress address) => address?.GetAddressBytes() ?? throw new ArgumentNullException(nameof(address));
}