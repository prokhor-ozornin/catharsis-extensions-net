using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IListExtensions"/>.</para>
  /// </summary>
  public sealed class IListExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.InsertNext{T}(IList{T}, int, T)"/> method.</para>
    /// </summary>
    [Fact]
    public void InsertNext_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.InsertNext(null, 0, new object()));

      var first = new List<string>();
      var second = first.InsertNext(0, "test");
      Assert.Equal(1, first.Count);
      Assert.Equal("test", first.Single());
      Assert.True(ReferenceEquals(first, second));
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.RemoveAtNext{T}(IList{T}, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void RemoveAtNext_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.RemoveAtNext<object>(null, 0));

      var first = new List<string> {"1", "2"};
      var second = first.RemoveAtNext(0);
      Assert.Equal(1, first.Count);
      Assert.Equal("2", first.Single());
      Assert.True(ReferenceEquals(first, second));
    }
  }
}