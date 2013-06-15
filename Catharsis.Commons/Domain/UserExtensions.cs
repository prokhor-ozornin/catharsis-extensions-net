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
    ///   <para></para>
    /// </summary>
    /// <param name="users"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="users"/> is a <c>null</c> reference.</exception>
    public static User WithUsername(this IEnumerable<User> users, string username)
    {
      Assertion.NotNull(users);

      return users.FirstOrDefault(user => user != null && user.Username == username);
    }
  }
}