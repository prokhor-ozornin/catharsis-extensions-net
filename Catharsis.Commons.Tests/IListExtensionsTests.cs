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
    ///   <para>Performs testing of <see cref="IListExtensions.AsConstrained{T}(IList{T}, Predicate{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsConstrained_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.AsConstrained<object>(null, x => true));
      Assert.Throws<ArgumentNullException>(() => new List<object>().AsConstrained(null));
      Assert.Throws<ArgumentException>(() => new List<object>().AsConstrained(x => false).Add(new object()));

      Assert.Throws<ArgumentException>(() => new List<object>().AsConstrained(x => false).Insert(0, new object()));

      var list = new List<object>().AsConstrained(x => true);
      list.Add(new object());
      list.Insert(0, new object());
      Assert.True(list.Count == 2);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.AsImmutable{T}(IList{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsImmutable_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.AsImmutable<object>(null));

      var list = new List<object> { "1" }.AsImmutable();

      Assert.Throws<NotSupportedException>(() => list.Add(new object()));
      Assert.Throws<NotSupportedException>(() => list.Clear());
      Assert.Throws<NotSupportedException>(() => list.Remove("1"));
    }


    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.AsMaxSized{T}(IList{T}, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsMaxSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.AsMaxSized<object>(null, 0));

      var first = new List<object>().AsMaxSized(-1);

      Assert.Throws<InvalidOperationException>(() => first.Add(new object()));
      Assert.Throws<InvalidOperationException>(() => first.Insert(0, new object()));

      var second = new List<object>().AsMaxSized(0);
      Assert.Throws<InvalidOperationException>(() => second.Add(new object()));
      Assert.Throws<InvalidOperationException>(() => second.Insert(0, new object()));
    }


    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.AsMinSized{T}(IList{T}, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsMinSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.AsMinSized<object>(null, 0));

      new List<object>().AsMinSized(-1).Add(new object());
      new List<object>().AsMinSized(-1).Insert(0, new object());
      new List<object>().AsMinSized(0).Add(new object());
      new List<object>().AsMinSized(0).Insert(0, new object());

      Assert.Throws<InvalidOperationException>(() => new List<object>().AsMinSized(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.AsNonNullable{T}(IList{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsNonNullable_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.AsNonNullable<object>(null));

      var list = new List<object> { "1" }.AsNonNullable();

      Assert.Throws<ArgumentNullException>(() => list.Add(null));
      Assert.Throws<ArgumentNullException>(() => list.Contains(null));
      Assert.Throws<ArgumentNullException>(() => list.CopyTo(null, 0));
      Assert.Throws<ArgumentNullException>(() => list.IndexOf(null));
      Assert.Throws<ArgumentNullException>(() => list.Insert(0, null));
      Assert.Throws<ArgumentNullException>(() => list.Remove(null));
      Assert.Throws<ArgumentNullException>(() => list[0] = null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.AsSized{T}(IList{T}, int?, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.AsSized<object>(null, 0));
      Assert.Throws<InvalidOperationException>(() => new List<object> { "1" }.AsSized(-1, -1));
      Assert.Throws<ArgumentException>(() => new List<object>().AsSized(1, 2));
      Assert.Throws<InvalidOperationException>(() => new List<object> { "1" }.AsSized(0));
      Assert.Throws<InvalidOperationException>(() => new List<object>().AsSized(2, 1));
      Assert.Throws<InvalidOperationException>(() => new List<object> { "1" }.AsSized(1).Add(new object()));
      Assert.Throws<InvalidOperationException>(() => new List<object> { "1" }.AsSized(1).Insert(0, new object()));
      Assert.Throws<InvalidOperationException>(() => new List<object> { "1" }.AsSized(2, 1).Clear());
      Assert.Throws<InvalidOperationException>(() => new List<object> { "1" }.AsSized(1, 1).Remove("1"));
      Assert.Throws<InvalidOperationException>(() => new List<object> { "1" }.AsSized(1, 1).RemoveAt(0));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.AsUnique{T}(IList{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsUnique_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.AsUnique<object>(null));

      Assert.Throws<InvalidOperationException>(() =>
      {
        var list = new List<object>().AsUnique();
        list.Add("test");
        list.Insert(1, "test");
      });

      var first = new List<string> { "test", "test" }.AsUnique();
      Assert.True(first.Count == 1);
      Assert.True(first.Single() == "test");

      var second = new List<string> { "1" }.AsUnique();
      second.Add("2");
      second.Remove("2");
      Assert.True(second.Count == 1);
      Assert.True(second.Single() == "1");

      second.Insert(0, "2");
      second.RemoveAt(0);
      Assert.True(second.Count == 1);
      Assert.True(second.Single() == "1");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IListExtensions.InsertNext{T}(IList{T}, int, T)"/> method.</para>
    /// </summary>
    [Fact]
    public void InsertNext_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IListExtensions.InsertNext(null, 0, new object()));

      var first = new List<string>();
      var second = first.InsertNext(0, "test");
      Assert.True(first.Count == 1);
      Assert.True(first.Single() == "test");
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
      Assert.True(first.Count == 1);
      Assert.True(first.Single() == "2");
      Assert.True(ReferenceEquals(first, second));
    }
  }
}