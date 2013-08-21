using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Profile"/>.</para>
  ///   <seealso cref="Profile"/>
  /// </summary>
  public static class ProfileExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of profiles, leaving those of specified type.</para>
    /// </summary>
    /// <param name="profiles">Source sequence of profiles to filter.</param>
    /// <param name="type">Type of profiles to search for.</param>
    /// <returns>Filtered sequence of profiles of specified type.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="profiles"/> or <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="type"/> is <see cref="string.Empty"/> string.</exception>
    public static IEnumerable<Profile> WithType(this IEnumerable<Profile> profiles, string type)
    {
      Assertion.NotNull(profiles);
      Assertion.NotEmpty(type);

      return profiles.Where(profile => profile != null && profile.Type == type);
    }

    /// <summary>
    ///   <para>Filters sequence of profiles, returning the one with specified user name.</para>
    /// </summary>
    /// <param name="profiles">Source sequence of profiles to filter.</param>
    /// <param name="username">Unique user name to search for.</param>
    /// <returns>Either profile with specified unique user name, or a <c>null</c> reference if no such profile could be found.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="profiles"/> or <paramref name="username"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="username"/> is <see cref="string.Empty"/> string.</exception>
    public static Profile WithUsername(this IEnumerable<Profile> profiles, string username)
    {
      Assertion.NotNull(profiles);
      Assertion.NotEmpty(username);

      return profiles.FirstOrDefault(profile => profile != null && profile.Username == username);
    }
  }
}