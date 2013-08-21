using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="User"/>.</para>
  ///   <seealso cref="User"/>
  /// </summary>
  public static class UserExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of users, returning the one with specified name.</para>
    /// </summary>
    /// <param name="users">Source sequence of users to filter.</param>
    /// <param name="username">Unique user name to search for.</param>
    /// <returns>Either user with specified unique name, or a <c>null</c> reference if no such user could be found.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="users"/> or <paramref name="username"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="username"/> is <see cref="string.Empty"/> string.</exception>
    public static User WithUsername(this IEnumerable<User> users, string username)
    {
      Assertion.NotNull(users);
      Assertion.NotEmpty(username);

      return users.FirstOrDefault(user => user != null && user.Username == username);
    }
  }
}