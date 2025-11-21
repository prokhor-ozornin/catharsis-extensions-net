using System.Net;
using System.Net.Sockets;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IPAddressExtensions"/>.</para>
/// </summary>
/// <seealso cref="IPAddressExtensions"/>
public sealed class IPAddressExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsV4"/> method.</para>
  /// </summary>
  [Fact]
  public void Ip4_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => null.IsV4).ThrowExactly<ArgumentNullException>().WithParameterName("address");
    }

    throw new NotImplementedException();

    return;

    static void Test(bool result, IPAddress address) => address.IsV4.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsV6"/> method.</para>
  /// </summary>
  [Fact]
  public void Ip6_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => null.IsV6).ThrowExactly<ArgumentNullException>().WithParameterName("address");
    }

    throw new NotImplementedException();

    return;

    static void Test(bool result, IPAddress address) => address.IsV6.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsAvailable(IPAddress, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPAddress) null).IsAvailable()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

      AssertionExtensions.Should(() => IPAddress.Any.IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

      AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

      Test(true, IPAddress.Loopback);
      Test(true, IPAddress.IPv6Loopback);
    }

    return;

    static void Test(bool result, IPAddress address, TimeSpan? timeout = null) => address.IsAvailable(timeout).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsAvailableAsync(IPAddress, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailableAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPAddress) null).IsAvailableAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("address").Await();

      AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

      AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

      Test(true, IPAddress.Loopback);
      Test(true, IPAddress.IPv6Loopback);
    }

    return;

    static void Test(bool result, IPAddress address, TimeSpan? timeout = null)
    {
      var task = address.IsAvailableAsync(timeout);
      task.Dispose();
      task.Should().BeAssignableTo<Task<bool>>();
      task.Await().Should().Be(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.Clone(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IPAddressExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

      Test(IPAddress.Any);
      Test(IPAddress.Loopback);
      Test(IPAddress.Broadcast);
      Test(IPAddress.None);
      Test(IPAddress.IPv6Any);
      Test(IPAddress.IPv6Loopback);
      Test(IPAddress.IPv6None);
    }

    return;

    static void Test(IPAddress original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<IPAddress>().And.NotBeSameAs(original).And.Be(original);
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
  ///   <para>Performs testing of <see cref="IPAddressExtensions.ToIpHost(IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIpHost_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IPAddressExtensions.ToIpHost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");

      new[] { IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None, IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.None }.ForEach(Test);
    }

    return;

    static void Test(IPAddress address)
    {
      var host = address.ToIpHost();

      host.Should().BeOfType<IPHostEntry>();
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
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPAddress) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("address");

      new[] { IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None }.ForEach(address => Test(address, 4));
      new[] { IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.IPv6None }.ForEach(address => Test(address, 16));
    }

    return;

    static void Test(IPAddress address, int count) => address.ToBytes().Should().BeOfType<byte[]>().And.HaveCount(count).And.Equal(address.GetAddressBytes());
  }
}