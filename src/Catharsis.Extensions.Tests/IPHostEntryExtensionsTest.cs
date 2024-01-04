using System.Net;
using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IPHostEntryExtensions"/>.</para>
/// </summary>
public sealed class IPHostEntryExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.Clone(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => IPHostEntryExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsAvailable(IPHostEntry, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailable_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsAvailable()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

    new IPHostEntry().IsAvailable().Should().BeFalse();
    new IPHostEntry { HostName = string.Empty, AddressList = [] }.IsAvailable().Should().BeFalse();

    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailable().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailable(TimeSpan.FromMilliseconds(1)).Should().BeTrue();

    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailable().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailable(TimeSpan.FromMilliseconds(1)).Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsAvailableAsync(IPHostEntry, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailableAsync_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsAvailableAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("host").Await();

    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
    AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

    new IPHostEntry().IsAvailableAsync().Await().Should().BeFalse();
    new IPHostEntry { HostName = string.Empty, AddressList = [] }.IsAvailableAsync().Await().Should().BeFalse();
    
    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailableAsync().Await().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.Loopback.ToString() }.IsAvailableAsync(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();

    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailableAsync().Await().Should().BeTrue();
    new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }.IsAvailableAsync(TimeSpan.FromMilliseconds(1)).Await().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsEmpty(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    new IPHostEntry().IsEmpty().Should().BeTrue();
    new IPHostEntry { HostName = string.Empty, AddressList = [] }.IsEmpty().Should().BeTrue();
    new IPHostEntry { HostName = "localhost" }.IsEmpty().Should().BeFalse();
    new IPHostEntry { AddressList = [IPAddress.Loopback]}.IsEmpty().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.ToEnumerable(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    foreach (var host in new[] { new IPHostEntry(), new IPHostEntry { AddressList = [] } })
    {
      host.ToEnumerable().Should().NotBeNull().And.BeSameAs(host.ToEnumerable()).And.Equal(host.AddressList ?? []);
    }
  }
}