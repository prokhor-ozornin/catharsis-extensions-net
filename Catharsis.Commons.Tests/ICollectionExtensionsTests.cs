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
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AddAll{T}(ICollection{T}, IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void AddAll_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AddAll(null, Enumerable.Empty<object>()));
      Assert.Throws<ArgumentNullException>(() => new object[0].AddAll(null));

      ICollection<string> first = new HashSet<string> {"first"};
      ICollection<string> second = new List<string> {"second"};

      first.AddAll(second);
      Assert.Equal(2, first.Count);
      Assert.Equal("first", first.ElementAt(0));
      Assert.Equal("second", first.ElementAt(1));
    }


    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AddNext{T}(ICollection{T}, T)"/> method.</para>
    /// </summary>
    [Fact]
    public void AddNext_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AddNext(null, new object()));

      ICollection<string> first = new HashSet<string>();
      var second = first.AddNext("test");
      Assert.Equal(1, first.Count);
      Assert.Equal("test", first.Single());
      Assert.True(ReferenceEquals(first, second));
    }


    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.RemoveAll{T}(ICollection{T}, IEnumerable{T})"/> method.</para>
    /// </summary> 
    [Fact]
    public void RemoveAll_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.RemoveAll(null, Enumerable.Empty<object>()));
      Assert.Throws<ArgumentNullException>(() => new object[0].RemoveAll(null));

      var first = new HashSet<string> {"1", "2", "3"};
      var second = new List<string> {"2", "4"};
      first.RemoveAll(second);
      Assert.Equal(2, first.Count);
      Assert.Equal("1", first.ElementAt(0));
      Assert.Equal("3", first.ElementAt(1));
    }


    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.RemoveNext{T}(ICollection{T}, T)"/> method.</para>
    /// </summary>
    [Fact]
    public void RemoveNext_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.RemoveNext(null, new object()));

      ICollection<string> first = new HashSet<string> {"1", "2"};
      var second = first.RemoveNext("1");
      Assert.Equal(1, first.Count);
      Assert.Equal("2", first.Single());
      Assert.True(ReferenceEquals(first, second));
    }
  }
}