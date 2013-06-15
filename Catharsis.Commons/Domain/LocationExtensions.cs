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
    ///   <para></para>
    /// </summary>
    /// <param name="locations"></param>
    /// <param name="address"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="locations"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Location> WithAddress(this IEnumerable<Location> locations, string address)
    {
      Assertion.NotNull(locations);

      return locations.Where(location => location != null && location.Address == address);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="locations"></param>
    /// <param name="postalCode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="locations"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Location> WithPostalCode(this IEnumerable<Location> locations, string postalCode)
    {
      Assertion.NotNull(locations);

      return locations.Where(location => location != null && location.PostalCode == postalCode);
    }
  }
}