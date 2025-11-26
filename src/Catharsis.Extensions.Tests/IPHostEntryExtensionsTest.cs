using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IPHostEntryExtensions"/>.</para>
/// </summary>
/// <seealso cref="IPHostEntryExtensions"/>
public sealed class IPHostEntryExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.Availability"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPHostEntry) null).Availability()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().Availability()).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().Availability(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().Availability(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().Availability()).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().Availability(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().Availability(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

      Test(false, new IPHostEntry());
      Test(false, new IPHostEntry { HostName = string.Empty, AddressList = [] });
      Test(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Test(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() }, TimeSpan.FromMilliseconds(1));
      Test(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() });
      Test(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }, TimeSpan.FromMilliseconds(1));
    }

    return;

    static void Test(bool result, IPHostEntry host, TimeSpan? timeout = null) => host.Availability(timeout).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsAvailableAsync(IPHostEntry, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailableAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPHostEntry) null).IsAvailableAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("host").Await();

      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync()).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync(TimeSpan.Zero)).ThrowExactlyAsync<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailableAsync(TimeSpan.FromMilliseconds(-1))).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("timeout");

      Test(false, new IPHostEntry());
      Test(false, new IPHostEntry { HostName = string.Empty, AddressList = [] });

      Test(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Test(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() }, TimeSpan.FromMilliseconds(1));

      Test(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() });
      Test(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }, TimeSpan.FromMilliseconds(1));
    }

    return;

    static void Test(bool result, IPHostEntry host, TimeSpan? timeout = null)
    {
      var task = host.IsAvailableAsync(timeout);
      task.Should().BeAssignableTo<Task<bool>>();
      task.Await().Should().Be(result);
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsUnset(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      Test(true, null);
      Test(true, new IPHostEntry());
      Test(true, new IPHostEntry { HostName = string.Empty, AddressList = [] });
      Test(false, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Test(false, new IPHostEntry { AddressList = [IPAddress.Loopback] });
    }

    return;

    static void Test(bool result, IPHostEntry host) => host.IsUnset.Should().Be(host is null || host.IsEmpty).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsEmpty(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPHostEntry) null).IsEmpty).ThrowExactly<ArgumentNullException>().WithParameterName("host");

      Test(true, new IPHostEntry());
      Test(true, new IPHostEntry { HostName = string.Empty, AddressList = [] });
      Test(false, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Test(false, new IPHostEntry { AddressList = [IPAddress.Loopback] });
    }

    return;

    static void Test(bool result, IPHostEntry host) => host.IsEmpty.Should().Be(host.HostName.IsUnset && host.AddressList.IsUnset).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.Clone(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IPHostEntryExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("host");

      Test(new IPHostEntry());
      Test(new IPHostEntry { HostName = string.Empty, AddressList = [] });
      Test(new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Test(new IPHostEntry { AddressList = [IPAddress.Loopback] });
    }

    return;

    static void Test(IPHostEntry original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<IPHostEntry>().And.NotBeSameAs(original);
      clone.ToString().Should().Be(original.ToString());
      clone.AddressList.Should().Equal(original.AddressList);
      clone.Aliases.Should().Equal(original.Aliases);
      clone.HostName.Should().Be(original.HostName);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.ToEnumerable(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPHostEntry) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

      new[] { new IPHostEntry(), new IPHostEntry { AddressList = [] } }.ForEach(host =>
      {
        host.ToEnumerable().Should().BeOfType<IEnumerable<IPAddress>>().And.BeSameAs(host.ToEnumerable()).And.Equal(host.AddressList ?? []);
      });
    }

    return;

    static void Test(IPHostEntry host)
    {
    }
  }
}