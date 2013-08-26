using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="UserExtensions"/>.</para>
  /// </summary>
  public sealed class UserExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="UserExtensions.WithUsername(IEnumerable{User}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithUsername_Method()
    {
      Assert.Throws<ArgumentNullException>(() => UserExtensions.WithUsername(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => UserExtensions.WithUsername(Enumerable.Empty<User>(), null));
      Assert.Throws<ArgumentException>(() => UserExtensions.WithUsername(Enumerable.Empty<User>(), string.Empty));

      Assert.True(new[] { null, new User { Username = "Username" }, null, new User { Username = "Username_2" } }.WithUsername("Username") != null);
    }
  }
}