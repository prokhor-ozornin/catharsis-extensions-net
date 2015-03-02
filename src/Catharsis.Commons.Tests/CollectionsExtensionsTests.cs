using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="CollectionsExtensions"/>.</para>
  /// </summary>
  public sealed class CollectionsExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="CollectionsExtensions.Add{T}(ICollection{T}, IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Add_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Add(null, Enumerable.Empty<object>()));
      Assert.Throws<ArgumentNullException>(() => new object[0].Add(null));

      ICollection<string> first = new HashSet<string> {"first"};
      ICollection<string> second = new List<string> {"second"};

      first.Add(second);
      Assert.Equal(2, first.Count);
      Assert.Equal("first", first.ElementAt(0));
      Assert.Equal("second", first.ElementAt(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CollectionsExtensions.Each{T}(IEnumerable{T}, Action{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Each_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Each<object>(null, x => {}));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().Each(null));

      var strings = new [] { "first", "second", "third" };
      var list = new List<string>();
      Assert.True(ReferenceEquals(strings.Each(list.Add), strings));
      Assert.Equal(3, list.Count);
      Assert.Equal("first", list[0]);
      Assert.Equal("second", list[1]);
      Assert.Equal("third", list[2]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CollectionsExtensions.Join{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Join_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Join<object>(null, "separator"));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().Join(null));

      Assert.Equal(string.Empty, Enumerable.Empty<object>().Join("separator"));
      Assert.Equal("1", new[] { 1 }.Join(","));
      Assert.Equal("123", new[] { 1, 2, 3 }.Join(string.Empty));
      Assert.Equal("1,2,3", new [] { 1, 2, 3 }.Join(","));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CollectionsExtensions.Paginate{T}(IEnumerable{T}, int, int)"/></para>
    /// </summary>
    [Fact]
    public void Paginate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Paginate<object>(null));

      Assert.False(Enumerable.Empty<object>().Paginate().Any());

      var sequence = new[] { "first", "second", "third" };
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
    ///   <para>Performs testing of <see cref="CollectionsExtensions.Random{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Random_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Random<object>(null));

      Assert.Null(Enumerable.Empty<object>().Random());

      var element = new object();
      Assert.True(ReferenceEquals(new[] { element }.Random(), element));

      var elements = new[] { "first", "second" };
      Assert.True(elements.Contains(elements.Random()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CollectionsExtensions.Remove{T}(ICollection{T}, IEnumerable{T})"/> method.</para>
    /// </summary> 
    [Fact]
    public void Remove_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.Remove(null, Enumerable.Empty<object>()));
      Assert.Throws<ArgumentNullException>(() => new object[0].Remove(null));

      var first = new HashSet<string> {"1", "2", "3"};
      var second = new List<string> {"2", "4"};
      first.Remove(second);
      Assert.Equal(2, first.Count);
      Assert.Equal("1", first.ElementAt(0));
      Assert.Equal("3", first.ElementAt(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="CollectionsExtensions.ToListString{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void ToListString_Method()
    {
      Assert.Throws<ArgumentNullException>(() => CollectionsExtensions.ToListString<object>(null));

      Assert.Equal("[]", Enumerable.Empty<object>().ToListString());
      Assert.Equal("[1]", new[] { 1 }.ToListString());
      Assert.Equal("[1, 2, 3]", new [] { 1, 2, 3 }.ToListString());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="EnumerableExtensions.ToSet{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void ToSet_Method()
    {
      Assert.Throws<ArgumentNullException>(() => EnumerableExtensions.ToSet<object>(null));

      Assert.False(Enumerable.Empty<object>().ToSet().Any());
      var set = new [] { 1, 1, 2, 3, 4, 5, 5 }.ToSet();
      Assert.Equal(5, set.Count);
      for (var i = 1; i <= 5; i++)
      {
        Assert.Equal(i, set.ElementAt(i - 1));
      }
    }
  }
}