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
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsAvailable(this IPHostEntry host, TimeSpan? timeout = null)
  {
    if (host is null) throw new ArgumentNullException(nameof(host));

    var address = host.HostName.IsEmpty() ? host.AddressList?.FirstOrDefault()?.ToString() : host.HostName;

    if (address is null)
    {
      return false;
    }

    using var ping = new Ping();

    var reply = timeout is not null ? ping.Send(address, (int) timeout.Value.TotalMilliseconds) : ping.Send(address);

    return reply.Status == IPStatus.Success;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<bool> IsAvailableAsync(this IPHostEntry host, TimeSpan? timeout = null)
  {
    if (host is null) throw new ArgumentNullException(nameof(host));

    var address = host.HostName.IsEmpty() ? host.AddressList?.FirstOrDefault()?.ToString() : host.HostName;

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
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this IPHostEntry host) => host is not null ? host.HostName.IsEmpty() && (host.AddressList is null || host.AddressList.IsEmpty()) : throw new ArgumentNullException(nameof(host));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="host"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<IPAddress> ToEnumerable(this IPHostEntry host) => host?.AddressList ?? throw new ArgumentNullException(nameof(host));
}