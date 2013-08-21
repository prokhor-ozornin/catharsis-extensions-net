using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="City"/>.</para>
  ///   <seealso cref="City"/>
  /// </summary>
  public static class CityExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of cities, leaving those located in specified country.</para>
    /// </summary>
    /// <param name="cities">Source sequence of cities to filter.</param>
    /// <param name="country">Country to search for.</param>
    /// <returns>Filtered sequence of cities in specified country.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<City> WithCountry(this IEnumerable<City> cities, Country country)
    {
      Assertion.NotNull(cities);

      return country != null ? cities.Where(city => city != null && city.Country.Id == country.Id) : cities.Where(city => city != null && city.Country == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of cities by country's name in ascending order.</para>
    /// </summary>
    /// <param name="cities">Source sequence of cities for sorting.</param>
    /// <returns>Sorted sequence of cities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<City> OrderByCountryName(this IEnumerable<City> cities)
    {
      Assertion.NotNull(cities);

      return cities.OrderBy(city => city.Country.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of cities by country's name in descending order.</para>
    /// </summary>
    /// <param name="cities">Source sequence of cities for sorting.</param>
    /// <returns>Sorted sequence of cities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<City> OrderByCountryNameDescending(this IEnumerable<City> cities)
    {
      Assertion.NotNull(cities);

      return cities.OrderByDescending(city => city.Country.Name);
    }

    /// <summary>
    ///   <para>Filters sequence of cities, leaving those located in specified region.</para>
    /// </summary>
    /// <param name="cities">Source sequence of cities to filter.</param>
    /// <param name="region">Region to search for.</param>
    /// <returns>Filtered sequence of cities in specified region.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<City> WithRegion(this IEnumerable<City> cities, string region)
    {
      Assertion.NotNull(cities);

      return cities.Where(city => city != null && city.Region == region);
    }

    /// <summary>
    ///   <para>Sorts sequence of cities by region in ascending order.</para>
    /// </summary>
    /// <param name="cities">Source sequence of cities for sorting.</param>
    /// <returns>Sorted sequence of cities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<City> OrderByRegion(this IEnumerable<City> cities)
    {
      Assertion.NotNull(cities);

      return cities.OrderBy(city => city.Region);
    }

    /// <summary>
    ///   <para>Sorts sequence of cities by region in descending order.</para>
    /// </summary>
    /// <param name="cities">Source sequence of cities for sorting.</param>
    /// <returns>Sorted sequence of cities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<City> OrderByRegionDescending(this IEnumerable<City> cities)
    {
      Assertion.NotNull(cities);

      return cities.OrderByDescending(city => city.Region);
    }
  }
}