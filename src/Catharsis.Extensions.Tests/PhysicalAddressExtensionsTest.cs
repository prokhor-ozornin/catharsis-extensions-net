using System.Net.NetworkInformation;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
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
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => PhysicalAddressExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

      Validate(PhysicalAddress.None);
      Validate(new PhysicalAddress(Attributes.RandomBytes()));
    }

    return;

    static void Validate(PhysicalAddress original)
    {
      var clone = original.Clone();
      
      clone.Should().BeOfType<PhysicalAddress>().And.NotBeSameAs(original).And.Be(original);
      clone.ToString().Should().Be(original.ToString());
      clone.ToBytes().Should().BeOfType<byte[]>().And.Equal(original.ToBytes());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="PhysicalAddressExtensions.ToBytes(PhysicalAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((PhysicalAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

      Validate(PhysicalAddress.None, []);
      Attributes.RandomBytes().With(bytes => Validate(new PhysicalAddress(bytes), bytes));
    }

    return;

    static void Validate(PhysicalAddress address, IEnumerable<byte> bytes) => address.ToBytes().Should().BeOfType<byte[]>().And.Equal(bytes).And.Equal(address.GetAddressBytes());
  }
}