using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Country"/>.</para>
  ///   <seealso cref="Country"/>
  /// </summary>
  public static class CountryExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="countries">Source sequence of countries to filter.</param>
    /// <param name="isoCode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="countries"/> is a <c>null</c> reference.</exception>
    public static Country WithIsoCode(this IEnumerable<Country> countries, string isoCode)
    {
      Assertion.NotNull(countries);

      return countries.Single(entity => entity != null && entity.IsoCode == isoCode);
    }

    /// <summary>
    ///   <para>Sorts sequence of countries by ISO code in ascending order.</para>
    /// </summary>
    /// <param name="countries">Source sequence of countries for sorting.</param>
    /// <returns>Sorted sequence of countries.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="countries"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Country> OrderByIsoCode(this IEnumerable<Country> countries)
    {
      Assertion.NotNull(countries);

      return countries.OrderBy(country => country.IsoCode);
    }

    /// <summary>
    ///   <para>Sorts sequence of countries by ISO code in descending order.</para>
    /// </summary>
    /// <param name="countries">Source sequence of countries for sorting.</param>
    /// <returns>Sorted sequence of countries.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="countries"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Country> OrderByIsoCodeDescending(this IEnumerable<Country> countries)
    {
      Assertion.NotNull(countries);

      return countries.OrderByDescending(country => country.IsoCode);
    }
  }
}