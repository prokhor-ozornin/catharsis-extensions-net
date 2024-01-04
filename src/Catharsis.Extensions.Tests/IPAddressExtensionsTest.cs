using System.Net;
using System.Net.Sockets;
using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IPAddressExtensions"/>.</para>
/// </summary>
public sealed class IPAddressExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.Clone(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsAvailable(IPAddress, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailable_Method()
  {
    AssertionExtensions.Should(() => ((IPAddress) null).IsAvailable()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

    IPAddress.Loopback.IsAvailable().Should().BeTrue();
    IPAddress.Loopback.IsAvailable(TimeSpan.FromMilliseconds(1)).Should().BeTrue();

    IPAddress.IPv6Loopback.IsAvailable().Should().BeTrue();
    IPAddress.IPv6Loopback.IsAvailable(TimeSpan.FromMilliseconds(1)).Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsAvailableAsync(IPAddress, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailableAsync_Method()
  {
    AssertionExtensions.Should(() => ((IPAddress) null).IsAvailableAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("address").Await();

    AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

    IPAddress.Loopback.IsAvailableAsync().Await().Should().BeTrue();
    IPAddress.Loopback.IsAvailableAsync(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();

    IPAddress.IPv6Loopback.IsAvailableAsync().Await().Should().BeTrue();
    IPAddress.IPv6Loopback.IsAvailableAsync(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.Min(IPAddress, IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.Min(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => IPAddress.Loopback.Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");
    AssertionExtensions.Should(() => IPAddress.Any.Min(IPAddress.IPv6Any)).ThrowExactly<SocketException>();
    AssertionExtensions.Should(() => IPAddress.Loopback.Min(IPAddress.IPv6Loopback)).ThrowExactly<SocketException>();
    AssertionExtensions.Should(() => IPAddress.None.Min(IPAddress.IPv6None)).ThrowExactly<SocketException>();
    
    var first = IPAddress.Loopback;
    var second = IPAddress.Loopback;
    first.Min(second).Should().BeSameAs(first);

    first = IPAddress.Parse("192.168.0.1");
    second = IPAddress.Parse("192.168.0.1");
    first.Min(second).Should().BeSameAs(first);

    first = IPAddress.Parse("10.0.0.1");
    second = IPAddress.Parse("10.0.0.2");
    first.Min(second).Should().BeSameAs(first);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.Max(IPAddress, IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.Max(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => IPAddress.Loopback.Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");
    AssertionExtensions.Should(() => IPAddress.Any.Max(IPAddress.IPv6Any)).ThrowExactly<SocketException>();
    AssertionExtensions.Should(() => IPAddress.Loopback.Max(IPAddress.IPv6Loopback)).ThrowExactly<SocketException>();
    AssertionExtensions.Should(() => IPAddress.None.Max(IPAddress.IPv6None)).ThrowExactly<SocketException>();

    var first = IPAddress.Loopback;
    var second = IPAddress.Loopback;
    first.Max(second).Should().BeSameAs(first);

    first = IPAddress.Parse("192.168.0.1");
    second = IPAddress.Parse("192.168.0.1");
    first.Max(second).Should().BeSameAs(first);

    first = IPAddress.Parse("10.0.0.1");
    second = IPAddress.Parse("10.0.0.2");
    first.Max(second).Should().BeSameAs(second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsV4"/> method.</para>
  /// </summary>
  [Fact]
  public void Ip4_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.IsV4(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsV6"/> method.</para>
  /// </summary>
  [Fact]
  public void Ip6_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.IsV6(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    throw new NotImplementedException();
  }


  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.ToIpHost(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIpHost_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.ToIpHost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    foreach (var address in new[] { IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None, IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.None })
    {
      var host = address.ToIpHost();
      host.Should().NotBeNull().And.NotBeSameAs(address.ToIpHost());
      host.AddressList.Should().Equal(address);
      host.Aliases.Should().BeEmpty();
      host.HostName.Should().BeNull();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.ToBytes(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((IPAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    foreach (var address in new[] {IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None})
    {
      address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.HaveCount(4).And.Equal(address.GetAddressBytes());
    }

    foreach (var address in new[] { IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.IPv6None })
    {
      address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.HaveCount(16).And.Equal(address.GetAddressBytes());
    }
  }
}