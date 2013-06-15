using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IEnumerableExtensions"/>.</para>
  /// </summary>
  public sealed class IEnumerableExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="IEnumerableExtensions.Each{T}(IEnumerable{T}, Action{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Each_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.Each<object>(null, x => {}));
      Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.Each(Enumerable.Empty<object>(), null));

      var strings = new [] { "first", "second", "third" };
      var list = new List<string>();
      strings.Each(list.Add);
      Assert.True(list.Count == 3);
      Assert.True(list[0] == "first");
      Assert.True(list[1] == "second");
      Assert.True(list[2] == "third");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IEnumerableExtensions.Join{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Join_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.Join<object>(null, "separator"));
      Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.Join(Enumerable.Empty<object>(), null));

      Assert.True(Enumerable.Empty<object>().Join("separator") == string.Empty);
      Assert.True(new[] { 1 }.Join(",") == "1");
      Assert.True(new[] { 1, 2, 3 }.Join(string.Empty) == "123");
      Assert.True(new [] { 1, 2, 3 }.Join(",") == "1,2,3");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToListString{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void ToListString_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ToListString<object>(null));

      Assert.True(Enumerable.Empty<object>().ToListString() == "[]");
      Assert.True(new[] { 1 }.ToListString() == "[1]", new[] { 1 }.ToListString());
      Assert.True(new [] { 1, 2, 3 }.ToListString() == "[1, 2, 3]");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToSet{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void ToSet_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IEnumerableExtensions.ToSet<object>(null));

      Assert.True(Enumerable.Empty<object>().ToSet().Count == 0);
      var set = new [] { 1, 1, 2, 3, 4, 5, 5 }.ToSet();
      Assert.True(set.Count == 5);
      for (var i = 1; i <= 5; i++)
      {
        Assert.True(set.ElementAt(i - 1) == i);
      }
    }
  }
}