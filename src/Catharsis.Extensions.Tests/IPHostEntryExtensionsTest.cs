using System.Net;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IPHostEntryExtensions"/>.</para>
/// </summary>
public sealed class IPHostEntryExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsAvailable(IPHostEntry, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAvailable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPHostEntry) null).IsAvailable()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.Any.ToIpHost().IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable()).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable(TimeSpan.Zero)).ThrowExactly<ArgumentException>().WithParameterName("address");
      AssertionExtensions.Should(() => IPAddress.IPv6Any.ToIpHost().IsAvailable(TimeSpan.FromMilliseconds(-1))).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("timeout");

      Validate(false, new IPHostEntry());
      Validate(false, new IPHostEntry { HostName = string.Empty, AddressList = [] });
      Validate(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Validate(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() }, TimeSpan.FromMilliseconds(1));
      Validate(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() });
      Validate(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }, TimeSpan.FromMilliseconds(1));
    }

    return;

    static void Validate(bool result, IPHostEntry host, TimeSpan? timeout = null) => host.IsAvailable(timeout).Should().Be(result);
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

      Validate(false, new IPHostEntry());
      Validate(false, new IPHostEntry { HostName = string.Empty, AddressList = [] });

      Validate(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Validate(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() }, TimeSpan.FromMilliseconds(1));

      Validate(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() });
      Validate(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }, TimeSpan.FromMilliseconds(1));
    }

    return;

    static void Validate(bool result, IPHostEntry host, TimeSpan? timeout = null)
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
      Validate(true, null);
      Validate(true, new IPHostEntry());
      Validate(true, new IPHostEntry { HostName = string.Empty, AddressList = [] });
      Validate(false, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Validate(false, new IPHostEntry { AddressList = [IPAddress.Loopback] });
    }

    return;

    static void Validate(bool result, IPHostEntry host) => host.IsUnset().Should().Be(host is null || host.IsEmpty()).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsEmpty(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IPHostEntry) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

      Validate(true, new IPHostEntry());
      Validate(true, new IPHostEntry { HostName = string.Empty, AddressList = [] });
      Validate(false, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Validate(false, new IPHostEntry { AddressList = [IPAddress.Loopback] });
    }

    return;

    static void Validate(bool result, IPHostEntry host) => host.IsEmpty().Should().Be(host.HostName.IsUnset() && host.AddressList.IsUnset()).And.Be(result);
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

      Validate(new IPHostEntry());
      Validate(new IPHostEntry { HostName = string.Empty, AddressList = [] });
      Validate(new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
      Validate(new IPHostEntry { AddressList = [IPAddress.Loopback] });
    }

    return;

    static void Validate(IPHostEntry original)
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

    static void Validate(IPHostEntry host)
    {
    }
  }
}