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
    ///   <para></para>
    /// </summary>
    /// <param name="profiles"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="profiles"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Profile> WithType(this IEnumerable<Profile> profiles, string type)
    {
      Assertion.NotNull(profiles);

      return profiles.Where(profile => profile != null && profile.Type == type);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="profiles"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="profiles"/> is a <c>null</c> reference.</exception>
    public static Profile WithUsername(this IEnumerable<Profile> profiles, string username)
    {
      Assertion.NotNull(profiles);

      return profiles.FirstOrDefault(profile => profile != null && profile.Username == username);
    }
  }
}