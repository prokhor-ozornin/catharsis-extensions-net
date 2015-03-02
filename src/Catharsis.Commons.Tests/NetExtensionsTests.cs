using System;
using System.Net;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="NetExtensions"/>.</para>
  /// </summary>
  public sealed class NetExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="NetExtensions.IPAddress(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void IPAddress_Method()
    {
      Assert.Throws<ArgumentNullException>(() => NetExtensions.IPAddress(null, new object()));

      Assert.Null(Convert.To.IPAddress(null));
      Assert.True(ReferenceEquals(Convert.To.IPAddress(IPAddress.Loopback), IPAddress.Loopback));
      Assert.Equal(IPAddress.Loopback, Convert.To.IPAddress(IPAddress.Loopback.ToString()));
      Assert.Null(Convert.To.IPAddress(new object()));
    }
  }
}