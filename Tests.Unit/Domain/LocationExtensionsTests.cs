using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="LocationExtensions"/>.</para>
  /// </summary>
  public sealed class LocationExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="LocationExtensions.WithAddress(IEnumerable{Location}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithAddress_Method()
    {
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.WithAddress(null, string.Empty));

      Assert.False(Enumerable.Empty<Location>().WithAddress(null).Any());
      Assert.False(Enumerable.Empty<Location>().WithAddress(string.Empty).Any());
      Assert.True(new[] { null, new Location { Address = "Address" }, null, new Location { Address = "Address_2" } }.WithAddress("Address").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="LocationExtensions.WithPostalCode(IEnumerable{Location}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithPostalCode_Method()
    {
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.WithPostalCode(null, string.Empty));

      Assert.False(Enumerable.Empty<Location>().WithPostalCode(null).Any());
      Assert.False(Enumerable.Empty<Location>().WithPostalCode(string.Empty).Any());
      Assert.True(new[] { null, new Location { PostalCode = "PostalCode" }, null, new Location { PostalCode = "PostalCode_2" } }.WithPostalCode("PostalCode").Count() == 1);
    }
  }
}