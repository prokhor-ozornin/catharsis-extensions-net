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

      Assert.True(Enumerable.Empty<User>().WithUsername(null) == null);
      Assert.True(Enumerable.Empty<User>().WithUsername(string.Empty) == null);
      Assert.True(new[] { null, new User { Username = "Username" }, null, new User { Username = "Username_2" } }.WithUsername("Username") != null);
    }
  }
}