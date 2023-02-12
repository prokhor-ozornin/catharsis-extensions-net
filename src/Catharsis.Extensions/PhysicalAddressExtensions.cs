using System.Net.NetworkInformation;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="PhysicalAddress"/>
public static class PhysicalAddressExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] ToBytes(this PhysicalAddress address) => address?.GetAddressBytes() ?? throw new ArgumentNullException(nameof(address));
}