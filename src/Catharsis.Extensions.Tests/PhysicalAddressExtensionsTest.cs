using System.Net.NetworkInformation;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="PhysicalAddressExtensions"/>.</para>
/// </summary>
public sealed class PhysicalAddressExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="PhysicalAddressExtensions.ToBytes(PhysicalAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((PhysicalAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    foreach (var address in new[] {PhysicalAddress.None, new PhysicalAddress(RandomBytes)})
    {
      address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.Equal(address.GetAddressBytes());
    }
  }
}