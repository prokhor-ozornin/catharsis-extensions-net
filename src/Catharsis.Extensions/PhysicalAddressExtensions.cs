using System.Net.NetworkInformation;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="PhysicalAddress"/>
public static class PhysicalAddressExtensions
{
  /// <param name="address">Physical address to be cloned.</param>
  extension(PhysicalAddress address)
  {
    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="PhysicalAddress"/> with the same address bytes as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="address"/> is <see langword="null"/>.</exception>
    public PhysicalAddress Clone() => address is not null ? new PhysicalAddress(address.GetAddressBytes()) : throw new ArgumentNullException(nameof(address));

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