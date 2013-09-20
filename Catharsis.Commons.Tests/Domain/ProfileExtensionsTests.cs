using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ProfileExtensions"/>.</para>
  /// </summary>
  public sealed class ProfileExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ProfileExtensions.WithType(IEnumerable{Profile}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithType_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ProfileExtensions.WithType(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<Profile>().WithType(null));
      Assert.Throws<ArgumentException>(() => Enumerable.Empty<Profile>().WithType(string.Empty));

      Assert.True(new[] { null, new Profile { Type = "Type" }, null, new Profile { Type = "Type_2" } }.WithType("Type").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ProfileExtensions.WithUsername(IEnumerable{Profile}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithUsername_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ProfileExtensions.WithUsername(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<Profile>().WithUsername(null));
      Assert.Throws<ArgumentException>(() => Enumerable.Empty<Profile>().WithUsername(string.Empty));

      Assert.True(new[] { null, new Profile { Username = "Username" }, null, new Profile { Username = "Username_2" } }.WithUsername("Username") != null);
    }
  }
}