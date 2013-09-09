using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="RatingExtensions"/>.</para>
  /// </summary>
  public sealed class RatingExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="RatingExtensions.WithItem(IEnumerable{Rating}, Item)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithItem_Method()
    {
      Assert.Throws<ArgumentNullException>(() => RatingExtensions.WithItem(null, new Item()));

      Assert.False(Enumerable.Empty<Rating>().WithItem(new Item()).Any());
      Assert.True(new[] { null, new Rating { Item = new Item { Id = "Id" } }, null, new Rating { Item = new Item { Id = "Id_2" } } }.WithItem(new Item { Id = "Id" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="RatingExtensions.WithValue(IEnumerable{Rating}, byte?, byte?)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithValue_Method()
    {
      Assert.Throws<ArgumentNullException>(() => RatingExtensions.WithValue(null));

      Assert.False(Enumerable.Empty<Rating>().WithValue(0, 0).Any());

      var ratings = new[] { null, new Rating { Value = 1 }, null, new Rating { Value = 2 } };
      Assert.False(ratings.WithValue(0, 0).Any());
      Assert.True(ratings.WithValue(0, 1).Count() == 1);
      Assert.True(ratings.WithValue(1, 1).Count() == 1);
      Assert.True(ratings.WithValue(1, 2).Count() == 2);
      Assert.True(ratings.WithValue(2, 3).Count() == 1);
      Assert.False(ratings.WithValue(3, 3).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="RatingExtensions.OrderByValue(IEnumerable{Rating})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByValue_Method()
    {
      Assert.Throws<ArgumentNullException>(() => RatingExtensions.OrderByValue(null));
      Assert.Throws<NullReferenceException>(() => new Rating[] { null }.OrderByValue().Any());

      var ratings = new[] { new Rating { Value = 2 }, new Rating { Value = 1 } };
      Assert.True(ratings.OrderByValue().SequenceEqual(ratings.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="RatingExtensions.OrderByValueDescending(IEnumerable{Rating})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByValueDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => RatingExtensions.OrderByValueDescending(null));
      Assert.Throws<NullReferenceException>(() => new Rating[] { null }.OrderByValueDescending().Any());

      var ratings = new[] { new Rating { Value = 1 }, new Rating { Value = 2 } };
      Assert.True(ratings.OrderByValue().SequenceEqual(ratings.Reverse()));
    }
  }
}