using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="CountryExtensions"/>.</para>
  /// </summary>
  public sealed class CountryExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="CountryExtensions.WithIsoCode(IEnumerable{Country}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithIsoCode_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CountryExtensions.WithIsoCode(null, "IsoCode"));
      Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<Country>().WithIsoCode(null));
      Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<Country>().WithIsoCode(string.Empty));

      Assert.True(new[] { null, new Country { IsoCode = "IsoCode" }, null, new Country { IsoCode = "IsoCode_2" } }.WithIsoCode("IsoCode").Equals(new Country { IsoCode = "IsoCode" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CountryExtensions.OrderByIsoCode(IEnumerable{Country})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByIsoCode_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CountryExtensions.OrderByIsoCode(null));
      Assert.Throws<NullReferenceException>(() => new Country[] { null }.OrderByIsoCode().Any());

      var countries = new[] { new Country { IsoCode = "Second" }, new Country { IsoCode = "First" } };
      Assert.True(countries.OrderByIsoCode().SequenceEqual(countries.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CountryExtensions.OrderByIsoCodeDescending(IEnumerable{Country})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByIsoCodeDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CountryExtensions.OrderByIsoCodeDescending(null));
      Assert.Throws<NullReferenceException>(() => new Country[] { null }.OrderByIsoCodeDescending().Any());

      var countries = new[] { new Country { IsoCode = "First" }, new Country { IsoCode = "Second" } };
      Assert.True(countries.OrderByIsoCodeDescending().SequenceEqual(countries.Reverse()));
    }
  }
}