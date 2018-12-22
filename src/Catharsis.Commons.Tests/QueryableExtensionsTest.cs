using System;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class QueryableExtensionsTest
  {
    [Fact]
    public void paginate()
    {
      Assert.Throws<ArgumentNullException>(() => QueryableExtensions.Paginate<object>(null));

      Assert.Empty(Enumerable.Empty<object>().AsQueryable().Paginate());

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

    [Fact]
    public void random()
    {
      Assert.Throws<ArgumentNullException>(() => QueryableExtensions.Random<object>(null));

      Assert.Null(Enumerable.Empty<object>().AsQueryable().Random());

      var element = new object();
      Assert.True(ReferenceEquals(new[] { element }.AsQueryable().Random(), element));

      var elements = new[] { "first", "second" }.AsQueryable();
      Assert.True(elements.Contains(elements.Random()));
    }
  }
}