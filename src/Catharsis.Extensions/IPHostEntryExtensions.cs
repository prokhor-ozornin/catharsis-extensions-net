using System.Net;
using System.Net.NetworkInformation;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="IPHostEntry"/>
public static class IPHostEntryExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="IPHostEntry"/> with the same properties as the original.</para>
  /// </summary>
  /// <param name="host">Host entry to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="host"/> is <see langword="null"/>.</exception>
  public static IPHostEntry Clone(this IPHostEntry host) => host is not null ? new IPHostEntry 
  {
    HostName = host.HostName, 
    AddressList = host.AddressList,
    Aliases = host.Aliases
  } : throw new ArgumentNullException(nameof(host));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="host"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsAvailableAsync(IPHostEntry, TimeSpan?)"/>
  public static bool IsAvailable(this IPHostEntry host, TimeSpan? timeout = null)
  {
    if (host is null) throw new ArgumentNullException(nameof(host));

    var address = host.HostName.IsUnset() ? host.AddressList?.FirstOrDefault()?.ToString() : host.HostName;

    if (address is null)
    {
      return false;
    }

    using var ping = new Ping();

    var reply = timeout is not null ? ping.Send(address, (int) timeout.Value.TotalMilliseconds) : ping.Send(address);

    return reply?.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="host"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsAvailable(IPHostEntry, TimeSpan?)"/>
  public static async Task<bool> IsAvailableAsync(this IPHostEntry host, TimeSpan? timeout = null)
  {
    if (host is null) throw new ArgumentNullException(nameof(host));

    var address = host.HostName.IsUnset() ? host.AddressList?.FirstOrDefault()?.ToString() : host.HostName;

    if (address is null)
    {
      return false;
    }

    using var ping = new Ping();

    var reply = await (timeout is not null ? ping.SendPingAsync(address, (int) timeout.Value.TotalMilliseconds) : ping.SendPingAsync(address)).ConfigureAwait(false);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(IPHostEntry)"/>
  public static bool IsUnset(this IPHostEntry host) => host is null || host.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="IPHostEntry"/> instance can be considered "empty", meaning it has an "empty" name or a list of addresses.</para>
  /// </summary>
  /// <param name="host">Host instance for evaluation.</param>
  /// <returns>If the specified <paramref name="host"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="host"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(IPHostEntry)"/>
  public static bool IsEmpty(this IPHostEntry host) => host is not null ? host.HostName.IsUnset() && host.AddressList.IsUnset() : throw new ArgumentNullException(nameof(host));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="host"/> is <see langword="null"/>.</exception>
  public static IEnumerable<IPAddress> ToEnumerable(this IPHostEntry host) => host?.AddressList ?? throw new ArgumentNullException(nameof(host));
}