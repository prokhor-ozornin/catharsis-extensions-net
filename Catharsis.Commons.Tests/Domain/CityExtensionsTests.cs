using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="CityExtensions"/>.</para>
  /// </summary>
  public sealed class CityExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="CityExtensions.WithCountry(IEnumerable{City}, Country)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithCountry_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CityExtensions.WithCountry(null, new Country()));

      Assert.False(Enumerable.Empty<City>().WithCountry(null).Any());
      Assert.False(Enumerable.Empty<City>().WithCountry(new Country()).Any());
      Assert.True(new[] { null, new City { Country = new Country { Id = "Id" } }, null, new City { Country = new Country { Id = "Id_2" } } }.WithCountry(new Country { Id = "Id" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CityExtensions.OrderByCountryName(IEnumerable{City})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCountryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CityExtensions.OrderByCountryName(null));
      Assert.Throws<NullReferenceException>(() => new City[] { null }.OrderByCountryName().Any());

      var cities = new[] { new City { Country = new Country { Name = "Second" } }, new City { Country = new Country { Name = "First" } } };
      Assert.True(cities.OrderByCountryName().SequenceEqual(cities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CityExtensions.OrderByCountryNameDescending(IEnumerable{City})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCountryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CityExtensions.OrderByCountryNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new City[] { null }.OrderByCountryNameDescending().Any());

      var cities = new[] { new City { Country = new Country { Name = "First" } }, new City { Country = new Country { Name = "Second" } } };
      Assert.True(cities.OrderByCountryNameDescending().SequenceEqual(cities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CityExtensions.WithRegion(IEnumerable{City}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithRegion_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CityExtensions.WithRegion(null, "Region"));

      Assert.False(Enumerable.Empty<City>().WithRegion(null).Any());
      Assert.False(Enumerable.Empty<City>().WithRegion(string.Empty).Any());
      Assert.True(new[] { null, new City { Region = "Region" }, null, new City { Region = "Region_2" } }.WithRegion("Region").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CityExtensions.OrderByRegion(IEnumerable{City})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByRegion_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CityExtensions.OrderByRegion(null));
      Assert.Throws<NullReferenceException>(() => new City[] { null }.OrderByRegion().Any());

      var cities = new[] { new City { Region = "Second" }, new City { Region = "First" } };
      Assert.True(cities.OrderByRegion().SequenceEqual(cities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CityExtensions.OrderByRegionDescending(IEnumerable{City})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByRegionDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CityExtensions.OrderByRegionDescending(null));
      Assert.Throws<NullReferenceException>(() => new City[] { null }.OrderByRegionDescending().Any());

      var cities = new[] { new City { Region = "First" }, new City { Region = "Second" } };
      Assert.True(cities.OrderByRegionDescending().SequenceEqual(cities.Reverse()));
    }
  }
}