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
  /// <param name="address"></param>
  extension(IPAddress address)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsV6"/>
    public bool IsV4 => address is not null ? address.AddressFamily == AddressFamily.InterNetwork : throw new ArgumentNullException(nameof(address));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsV4"/>
    public bool IsV6 => address is not null ? address.AddressFamily == AddressFamily.InterNetworkV6 : throw new ArgumentNullException(nameof(address));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsAvailableAsync(IPAddress, TimeSpan?)"/>
    public bool IsAvailable(TimeSpan? timeout = null)
    {
      if (address is null) throw new ArgumentNullException(nameof(address));

      using var ping = new Ping();

      var reply = timeout is not null ? ping.Send(address, (int) timeout.Value.TotalMilliseconds) : ping.Send(address);

      return reply?.Status == IPStatus.Success;
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public bool IsAvailable => address.IsAvailable();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsAvailable(IPAddress, TimeSpan?)"/>
    public async Task<bool> IsAvailableAsync(TimeSpan? timeout = null)
    {
      if (address is null) throw new ArgumentNullException(nameof(address));

      using var ping = new Ping();

      var reply = await (timeout is not null ? ping.SendPingAsync(address, (int) timeout.Value.TotalMilliseconds) : ping.SendPingAsync(address)).ConfigureAwait(false);

      return reply.Status == IPStatus.Success;
    }

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="IPAddress"/> with the same address bytes as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
    public IPAddress Clone() => address is not null ? new IPAddress(address.GetAddressBytes()) : throw new ArgumentNullException(nameof(address));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
    public IPHostEntry ToIpHost() => address is not null ? new IPHostEntry { AddressList = [address], Aliases = [] } : throw new ArgumentNullException(nameof(address));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
    public byte[] ToBytes() => address?.GetAddressBytes() ?? throw new ArgumentNullException(nameof(address));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public byte[] Bytes => address.ToBytes();
  }
}