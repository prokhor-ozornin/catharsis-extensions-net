using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="CategoryExtensions"/>.</para>
  /// </summary>
  public sealed class CategoryExtensionsTests
  {
    private sealed class TestCategory : Category
    {
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CategoryExtensions.WithParent{T}(IEnumerable{T}, T)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithParent_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CategoryExtensions.WithParent(null, new TestCategory()));

      Assert.False(Enumerable.Empty<Category>().WithParent(null).Any());
      Assert.False(Enumerable.Empty<Category>().WithParent(new TestCategory()).Any());
      Assert.True(new[] { null, new TestCategory(), new TestCategory { Parent = new TestCategory() }, new TestCategory { Parent = new TestCategory { Id = 1 } }, new TestCategory { Parent = new TestCategory { Id = 2 } } }.WithParent(new TestCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CategoryExtensions.Root{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Root_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CategoryExtensions.Root<TestCategory>(null));

      Assert.True(new[] { null, new TestCategory(), new TestCategory { Parent = new TestCategory() }, new TestCategory { Parent = new TestCategory { Id = 1 } }, new TestCategory { Parent = new TestCategory { Id = 2 } } }.Root().Count() == 1);
    }
  }
}