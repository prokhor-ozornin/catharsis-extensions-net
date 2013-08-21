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
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.WithAddress(Enumerable.Empty<Location>(), null));
      Assert.Throws<ArgumentException>(() => LocationExtensions.WithAddress(Enumerable.Empty<Location>(), string.Empty));

      Assert.True(new[] { null, new Location { Address = "Address" }, null, new Location { Address = "Address_2" } }.WithAddress("Address").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="LocationExtensions.OrderByAddress(IEnumerable{Location})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAddress_Method()
    {
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.OrderByAddress(null));

      Assert.Throws<NullReferenceException>(() => new Location[] { null }.OrderByAddress().Any());
      var entities = new[] { new Location { Address = "Second" }, new Location { Address = "First" } };
      Assert.True(entities.OrderByAddress().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="LocationExtensions.OrderByAddressDescending(IEnumerable{Location})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAddressDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.OrderByAddressDescending(null));

      Assert.Throws<NullReferenceException>(() => new Location[] { null }.OrderByAddressDescending().Any());
      var entities = new[] { new Location { Address = "First" }, new Location { Address = "Second" } };
      Assert.True(entities.OrderByAddressDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="LocationExtensions.InCity(IEnumerable{Location}, City)"/> method.</para>
    /// </summary>
    [Fact]
    public void InCity_Method()
    {
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.InCity(null, new City()));
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.InCity(Enumerable.Empty<Location>(), null));

      Assert.False(Enumerable.Empty<Location>().InCity(new City()).Any());
      Assert.True(new[] { null, new Location { City = new City { Id = "Id" } }, null, new Location { City = new City { Id = "Id_2" } } }.InCity(new City { Id = "Id" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="LocationExtensions.OrderByCityName(IEnumerable{Location})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCityName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.OrderByCityName(null));

      Assert.Throws<NullReferenceException>(() => new Location[] { null }.OrderByCityName().Any());
      var locations = new[] { new Location { City = new City { Name = "Second" } }, new Location { City = new City { Name = "First" } } };
      Assert.True(locations.OrderByCityName().SequenceEqual(locations.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="LocationExtensions.OrderByCityNameDescending(IEnumerable{Location})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCityNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => LocationExtensions.OrderByCityNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new Location[] { null }.OrderByCityNameDescending().Any());
      var locations = new[] { new Location { City = new City { Name = "First" } }, new Location { City = new City { Name = "Second" } } };
      Assert.True(locations.OrderByCityNameDescending().SequenceEqual(locations.Reverse()));
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