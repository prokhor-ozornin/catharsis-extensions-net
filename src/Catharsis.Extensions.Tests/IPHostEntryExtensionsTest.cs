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

    return;

    static void Validate(IPHostEntry original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<IPHostEntry>().And.NotBeSameAs(original).And.Be(original);
      clone.ToString().Should().Be(original.ToString());
      clone.AddressList.Should().Equal(original.AddressList);
      clone.Aliases.Should().Equal(original.Aliases);
      clone.HostName.Should().Be(original.HostName);
    }
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

    Validate(false, new IPHostEntry());
    Validate(false, new IPHostEntry { HostName = string.Empty, AddressList = [] });
    Validate(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
    Validate(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() }, TimeSpan.FromMilliseconds(1));
    Validate(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() });
    Validate(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }, TimeSpan.FromMilliseconds(1));

    return;

    static void Validate(bool result, IPHostEntry host, TimeSpan? timeout = null) => host.IsAvailable(timeout).Should().Be(result);
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

    Validate(false, new IPHostEntry());
    Validate(false, new IPHostEntry { HostName = string.Empty, AddressList = [] });

    Validate(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() });
    Validate(true, new IPHostEntry { HostName = IPAddress.Loopback.ToString() }, TimeSpan.FromMilliseconds(1));

    Validate(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() });
    Validate(true, new IPHostEntry { HostName = IPAddress.IPv6Loopback.ToString() }, TimeSpan.FromMilliseconds(1));
    
    return;

    static void Validate(bool result, IPHostEntry host, TimeSpan? timeout = null)
    {
      var task = host.IsAvailableAsync(timeout);
      task.Should().BeOfType<Task<bool>>();
      task.Await().Should().Be(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.IsEmpty(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    Validate(true, new IPHostEntry());
    Validate(true, new IPHostEntry { HostName = string.Empty, AddressList = [] });
    Validate(false, new IPHostEntry { HostName = "localhost" });
    Validate(false, new IPHostEntry { AddressList = [IPAddress.Loopback] });

    return;

    static void Validate(bool result, IPHostEntry host) => host.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IPHostEntryExtensions.ToEnumerable(IPHostEntry)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((IPHostEntry) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("host");

    new[] { new IPHostEntry(), new IPHostEntry { AddressList = [] } }.ForEach(host =>
    {
      host.ToEnumerable().Should().BeOfType<IEnumerable<IPAddress>>().And.BeSameAs(host.ToEnumerable()).And.Equal(host.AddressList ?? []);
    });

    return;

    static void Validate(IPHostEntry host)
    {
    }
  }
}