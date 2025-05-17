using System.Net;
using System.Net.Sockets;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IPAddressExtensions"/>.</para>
/// </summary>
public sealed class IPAddressExtensionsTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsV4"/> method.</para>
  /// </summary>
  [Fact]
  public void Ip4_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IPAddressExtensions.IsV4(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");
    }

    throw new NotImplementedException();

    return;

    static void Test(bool result, IPAddress address) => address.IsV4().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.IsV6"/> method.</para>
  /// </summary>
  [Fact]
  public void Ip6_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IPAddressExtensions.IsV6(null)).ThrowExactly<ArgumentNullException>().WithParameterName("address");
    }

    throw new NotImplementedException();

    return;

    static void Test(bool result, IPAddress address) => address.IsV6().Should().Be(result);
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
  ///   <para>Performs testing of <see cref="IPAddressExtensions.Min(IPAddress, IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IPAddressExtensions.Min(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => IPAddress.Loopback.Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");
      AssertionExtensions.Should(() => IPAddress.Any.Min(IPAddress.IPv6Any)).ThrowExactly<SocketException>();
      AssertionExtensions.Should(() => IPAddress.Loopback.Min(IPAddress.IPv6Loopback)).ThrowExactly<SocketException>();
      AssertionExtensions.Should(() => IPAddress.None.Min(IPAddress.IPv6None)).ThrowExactly<SocketException>();

      Test(IPAddress.Any, IPAddress.Any, IPAddress.Loopback);
      Test(IPAddress.Loopback, IPAddress.Loopback, IPAddress.Broadcast);
      //Test(


  //    Test(IPAddress.Loopback, IPAddress.Loopback, IPAddress.Loopback);
  //    Test(IPAddress.Parse("10.0.0.1"), IPAddress.Parse("10.0.0.1"), IPAddress.Parse("10.0.0.2"));
    }

    return;

    static void Test(IPAddress result, IPAddress left, IPAddress right) => left.Min(right).Should().BeAssignableTo<IPAddress>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.Max(IPAddress, IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IPAddressExtensions.Max(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => IPAddress.Loopback.Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");
      AssertionExtensions.Should(() => IPAddress.Any.Max(IPAddress.IPv6Any)).ThrowExactly<SocketException>();
      AssertionExtensions.Should(() => IPAddress.Loopback.Max(IPAddress.IPv6Loopback)).ThrowExactly<SocketException>();
      AssertionExtensions.Should(() => IPAddress.None.Max(IPAddress.IPv6None)).ThrowExactly<SocketException>();

      Test(IPAddress.Loopback, IPAddress.Loopback, IPAddress.Loopback);
      Test(IPAddress.Parse("192.168.0.1"), IPAddress.Parse("192.168.0.1"), IPAddress.Parse("192.168.0.1"));
      Test(IPAddress.Parse("10.0.0.2"), IPAddress.Parse("10.0.0.1"), IPAddress.Parse("10.0.0.2"));
    }

    return;

    static void Test(IPAddress result, IPAddress left, IPAddress right) => left.Max(right).Should().BeOfType<IPAddress>().And.BeSameAs(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPAddressExtensions.MinMax(IPAddress, IPAddress)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IPAddressExtensions.MinMax(null, IPAddress.Loopback)).ThrowExactly<ArgumentNullException>().WithParameterName("min");
      AssertionExtensions.Should(() => IPAddress.Loopback.MinMax(null)).ThrowExactly<ArgumentNullException>().WithParameterName("max");
      AssertionExtensions.Should(() => IPAddress.Any.MinMax(IPAddress.IPv6Any)).ThrowExactly<SocketException>();
      AssertionExtensions.Should(() => IPAddress.Loopback.MinMax(IPAddress.IPv6Loopback)).ThrowExactly<SocketException>();
      AssertionExtensions.Should(() => IPAddress.None.MinMax(IPAddress.IPv6None)).ThrowExactly<SocketException>();
    }

    throw new NotImplementedException();

    return;

    static void Test(IPAddress min, IPAddress max) => min.MinMax(max).Should().Be((min, max));
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

      new[] { IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None, IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.None }.ForEach(Validate);
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