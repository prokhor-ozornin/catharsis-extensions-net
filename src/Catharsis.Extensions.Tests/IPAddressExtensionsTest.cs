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

    Validate(IPAddress.Any);
    Validate(IPAddress.Loopback);
    Validate(IPAddress.Broadcast);
    Validate(IPAddress.None);
    Validate(IPAddress.IPv6Any);
    Validate(IPAddress.IPv6Loopback);
    Validate(IPAddress.IPv6None);

    return;

    static void Validate(IPAddress original)
    {
      var clone = original.Clone();

      clone.Should().NotBeSameAs(original).And.Be(original);
      clone.ToString().Should().Be(original.ToString());
      clone.AddressFamily.Should().Be(original.AddressFamily);
      clone.IsIPv4MappedToIPv6.Should().Be(original.IsIPv4MappedToIPv6);
      clone.IsIPv6LinkLocal.Should().Be(original.IsIPv6LinkLocal);
      clone.IsIPv6SiteLocal.Should().Be(original.IsIPv6SiteLocal);
      clone.IsIPv6UniqueLocal.Should().Be(original.IsIPv6UniqueLocal);
      clone.IsIPv6Multicast.Should().Be(original.IsIPv6Multicast);
      clone.IsIPv6Teredo.Should().Be(original.IsIPv6Teredo);
    }
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

    Validate(true, IPAddress.Loopback);
    Validate(true, IPAddress.Loopback, TimeSpan.FromMilliseconds(1));

    Validate(true, IPAddress.IPv6Loopback);
    Validate(true, IPAddress.IPv6Loopback, TimeSpan.FromMilliseconds(1));

    return;

    static void Validate(bool isAvailable, IPAddress address, TimeSpan? timeout = null) => address.IsAvailable(timeout).Should().Be(isAvailable);
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

    Validate(true, IPAddress.Loopback);
    Validate(true, IPAddress.Loopback, TimeSpan.FromMilliseconds(1));

    Validate(true, IPAddress.IPv6Loopback);
    Validate(true, IPAddress.IPv6Loopback, TimeSpan.FromMilliseconds(1));

    return;

    static void Validate(bool isAvailable, IPAddress address, TimeSpan? timeout = null) => address.IsAvailableAsync(timeout).Await().Should().Be(isAvailable);
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

    return;

    static void Validate(IPAddress min, IPAddress max)
    {
    }
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

    return;

    static void Validate(IPAddress min, IPAddress max)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsV4"/> method.</para>
  /// </summary>
  [Fact]
  public void Ip4_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.IsV4(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    throw new NotImplementedException();

    return;

    static void Validate(IPAddress address, bool isV4) => address.IsV4();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsV6"/> method.</para>
  /// </summary>
  [Fact]
  public void Ip6_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.IsV6(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    throw new NotImplementedException();

    return;

    static void Validate(IPAddress address, bool isV6) => address.IsV6();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.ToIpHost(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIpHost_Method()
  {
    AssertionExtensions.Should(() => IPAddressExtensions.ToIpHost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

    new[] { IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None, IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.None }.ForEach(Validate);

    return;

    static void Validate(IPAddress address)
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

    new[] {IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None}.ForEach(address => Validate(address, 4));
    new[] { IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.IPv6None }.ForEach(address => Validate(address, 16));

    return;

    static void Validate(IPAddress address, int count) => address.ToBytes().Should().NotBeNull().And.NotBeSameAs(address.ToBytes()).And.HaveCount(count).And.Equal(address.GetAddressBytes());
  }
}