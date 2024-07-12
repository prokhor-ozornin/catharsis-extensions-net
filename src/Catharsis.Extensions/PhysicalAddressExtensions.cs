using System.Net.NetworkInformation;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="PhysicalAddress"/>
public static class PhysicalAddressExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="PhysicalAddress"/> with the same address bytes as the original.</para>
  /// </summary>
  /// <param name="address">Physical address to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  public static PhysicalAddress Clone(this PhysicalAddress address) => address is not null ? new PhysicalAddress(address.GetAddressBytes()) : throw new ArgumentNullException(nameof(address));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="address"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
  public static byte[] ToBytes(this PhysicalAddress address) => address?.GetAddressBytes() ?? throw new ArgumentNullException(nameof(address));
}