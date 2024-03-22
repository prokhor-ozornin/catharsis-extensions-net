using System.Net.NetworkInformation;
using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="PhysicalAddressExtensions"/>.</para>
/// </summary>
public sealed class PhysicalAddressExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="PhysicalAddressExtensions.Clone(PhysicalAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => PhysicalAddressExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    throw new NotImplementedException();

    return;

    static void Validate(PhysicalAddress original)
    {
      var clone = original.Clone();
      
      clone.Should().NotBeSameAs(original).And.Be(original);
      clone.ToString().Should().Be(original.ToString());
      clone.GetAddressBytes().Should().Equal(original.GetAddressBytes());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="PhysicalAddressExtensions.ToBytes(PhysicalAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((PhysicalAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    new[] { PhysicalAddress.None, new PhysicalAddress(Attributes.RandomBytes()) }.ForEach(Validate);

    return;

    static void Validate(PhysicalAddress address) => address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.Equal(address.GetAddressBytes());
  }
}