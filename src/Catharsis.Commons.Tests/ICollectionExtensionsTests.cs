using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ICollectionExtensions"/>.</para>
  /// </summary>
  public sealed class ICollectionExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.Add{T}(ICollection{T}, IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Add_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.Add(null, Enumerable.Empty<object>()));
      Assert.Throws<ArgumentNullException>(() => new object[0].Add(null));

      ICollection<string> first = new HashSet<string> {"first"};
      ICollection<string> second = new List<string> {"second"};

      first.Add(second);
      Assert.Equal(2, first.Count);
      Assert.Equal("first", first.ElementAt(0));
      Assert.Equal("second", first.ElementAt(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.Remove{T}(ICollection{T}, IEnumerable{T})"/> method.</para>
    /// </summary> 
    [Fact]
    public void Remove_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.Remove(null, Enumerable.Empty<object>()));
      Assert.Throws<ArgumentNullException>(() => new object[0].Remove(null));

      var first = new HashSet<string> {"1", "2", "3"};
      var second = new List<string> {"2", "4"};
      first.Remove(second);
      Assert.Equal(2, first.Count);
      Assert.Equal("1", first.ElementAt(0));
      Assert.Equal("3", first.ElementAt(1));
    }
  }
}