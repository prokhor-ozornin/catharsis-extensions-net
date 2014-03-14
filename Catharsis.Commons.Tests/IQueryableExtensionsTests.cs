using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IQueryableExtensions"/>.</para>
  /// </summary>
  public sealed class IQueryableExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="IQueryableExtensions.Paginate{T}(IQueryable{T}, int, int)"/></para>
    /// </summary>
    [Fact]
    public void Paginate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IQueryableExtensions.Paginate<object>(null));

      Assert.False(Enumerable.Empty<object>().AsQueryable().Paginate().Any());

      var sequence = new[] { "first", "second", "third" }.AsQueryable();
      Assert.Equal("first", sequence.Paginate(-1, 1).Single());
      Assert.Equal("first", sequence.Paginate(0, 1).Single());
      Assert.Equal("first", sequence.Paginate(1, 1).Single());
      Assert.True(sequence.Paginate(1, 2).SequenceEqual(new[] { "first", "second" }));
      Assert.True(sequence.Paginate(1, -1).SequenceEqual(sequence));
      Assert.True(sequence.Paginate(1, 0).SequenceEqual(sequence));
      Assert.Equal("second", sequence.Paginate(2, 1).Single());
      Assert.Equal("third", sequence.Paginate(2, 2).Single());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IQueryableExtensions.Random{T}(IQueryable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Random_Method()
    {
      Assert.Null(Enumerable.Empty<object>().AsQueryable().Random());

      var element = new object();
      Assert.True(ReferenceEquals(new[] { element }.AsQueryable().Random(), element));

      var elements = new[] { "first", "second" }.AsQueryable();
      Assert.True(elements.Contains(elements.Random()));
    }
  }
}