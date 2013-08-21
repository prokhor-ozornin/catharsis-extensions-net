using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Location"/>.</para>
  ///   <seealso cref="Location"/>
  /// </summary>
  public static class LocationExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of locations, leaving those with specified address.</para>
    /// </summary>
    /// <param name="locations">Source sequence of locations to filter.</param>
    /// <param name="address">Address of locations to search for.</param>
    /// <returns>Filtered sequence of locations with specified address.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="locations"/> or <paramref name="address"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="address"/> is <see cref="string.Empty"/> string.</exception>
    public static IEnumerable<Location> WithAddress(this IEnumerable<Location> locations, string address)
    {
      Assertion.NotNull(locations);
      Assertion.NotEmpty(address);

      return locations.Where(location => location != null && location.Address == address);
    }

    /// <summary>
    ///   <para>Sorts sequence of locations by address in ascending order.</para>
    /// </summary>
    /// <param name="locations">Source sequence of locations for sorting.</param>
    /// <returns>Sorted sequence of locations.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="locations"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Location> OrderByAddress(this IEnumerable<Location> locations)
    {
      Assertion.NotNull(locations);

      return locations.OrderBy(location => location.Address);
    }

    /// <summary>
    ///   <para>Sorts sequence of locations by address in descending order.</para>
    /// </summary>
    /// <param name="locations">Source sequence of locations for sorting.</param>
    /// <returns>Sorted sequence of locations.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="locations"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Location> OrderByAddressDescending(this IEnumerable<Location> locations)
    {
      Assertion.NotNull(locations);

      return locations.OrderByDescending(location => location.Address);
    }

    /// <summary>
    ///   <para>Filters sequence of locations, leaving those belonging to specified city.</para>
    /// </summary>
    /// <param name="locations">Source sequence of locations to filter.</param>
    /// <param name="city">City to search for.</param>
    /// <returns>Filtered sequence of locations in specified city.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="locations"/> or <paramref name="city"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Location> InCity(this IEnumerable<Location> locations, City city)
    {
      Assertion.NotNull(locations);
      Assertion.NotNull(city);

      return locations.Where(location => location != null && location.City.Id == city.Id);
    }

    /// <summary>
    ///   <para>Sorts sequence of locations by city's name in ascending order.</para>
    /// </summary>
    /// <param name="locations">Source sequence of locations for sorting.</param>
    /// <returns>Sorted sequence of locations.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="locations"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Location> OrderByCityName(this IEnumerable<Location> locations)
    {
      Assertion.NotNull(locations);

      return locations.OrderBy(location => location.City.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of locations by city's name in descending order.</para>
    /// </summary>
    /// <param name="locations">Source sequence of locations for sorting.</param>
    /// <returns>Sorted sequence of locations.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="locations"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Location> OrderByCityNameDescending(this IEnumerable<Location> locations)
    {
      Assertion.NotNull(locations);

      return locations.OrderByDescending(location => location.City.Name);
    }

    /// <summary>
    ///   <para>Filters sequence of locations, leaving those with specified postal code.</para>
    /// </summary>
    /// <param name="locations">Source sequence of locations to filter.</param>
    /// <param name="postalCode">Postal code of locations to search for.</param>
    /// <returns>Filtered sequence of locations with specified postal code.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="locations"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Location> WithPostalCode(this IEnumerable<Location> locations, string postalCode)
    {
      Assertion.NotNull(locations);

      return locations.Where(location => location != null && location.PostalCode == postalCode);
    }
  }
}