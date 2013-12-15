using System;
using System.Collections;
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
      Assert.True(first.Count == 2);
      Assert.True(first.ElementAt(0) == "first");
      Assert.True(first.ElementAt(1) == "second");
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
      Assert.True(first.Count == 1);
      Assert.True(first.Single() == "test");
      Assert.True(ReferenceEquals(first, second));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AsConstrained{T}(ICollection{T}, Predicate{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsConstrained_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AsConstrained<object>(null, x => true));
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AsConstrained(new object[] { }, null));
      Assert.Throws<ArgumentException>(() => new object[] { }.AsConstrained(x => false).Add(new object()));

      var collection = new HashSet<object>().AsConstrained(x => true);
      collection.Add(new object());
      Assert.True(collection.Count == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AsImmutable{T}(ICollection{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsImmutable_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AsImmutable<object>(null));

      ICollection<object> collection = new object[] { "1" }.AsImmutable();

      Assert.False(collection.To<ICollection>().IsSynchronized);
      Assert.True(collection.IsReadOnly);

      Assert.Throws<NotSupportedException>(() => collection.Add(new object()));
      Assert.Throws<NotSupportedException>(() => collection.Clear());
      Assert.Throws<NotSupportedException>(() => collection.Remove("1"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AsMaxSized{T}(ICollection{T}, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsMaxSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AsMaxSized<object>(null, 0));

      ICollection<object> collection = new object[0].AsMaxSized(0);
      Assert.False(collection.To<ICollection>().IsSynchronized);
      Assert.Throws<InvalidOperationException>(() => new object[] { }.AsMaxSized(-1).Add(new object()));
      Assert.Throws<InvalidOperationException>(() => new object[] { }.AsMaxSized(0).Add(new object()));
    }


    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AsMinSized{T}(ICollection{T}, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsMinSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AsMinSized<object>(null, 0));

      var collection = new HashSet<object>().AsMinSized(0);
      Assert.False(collection.To<ICollection>().IsSynchronized);

      new HashSet<object>().AsMinSized(-1).Add(new object());
      new HashSet<object>().AsMinSized(0).Add(new object());

      Assert.Throws<InvalidOperationException>(() => new HashSet<object>().AsMinSized(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AsNonNullable{T}(ICollection{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsNonNullable_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AsNonNullable<object>(null));

      ICollection<object> collection = new object[] { }.AsNonNullable();
      Assert.False(collection.To<ICollection>().IsSynchronized);
      Assert.Throws<ArgumentNullException>(() => collection.Add(null));
      Assert.Throws<ArgumentNullException>(() => collection.Contains(null));
      Assert.Throws<ArgumentNullException>(() => collection.CopyTo(null, 0));
      Assert.Throws<ArgumentNullException>(() => collection.Remove(null));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AsSized{T}(ICollection{T}, int?, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AsSized<object>(null, 0));

      ICollection<object> collection = new object[0].AsSized(0);
      Assert.False(collection.To<ICollection>().IsSynchronized);
      Assert.Throws<InvalidOperationException>(() => new object[] { "1" }.AsSized(-1, -1));
      Assert.Throws<ArgumentException>(() => new object[] { }.AsSized(1, 2));
      Assert.Throws<InvalidOperationException>(() => new object[] { "1" }.AsSized(0));
      Assert.Throws<InvalidOperationException>(() => new object[] { }.AsSized(2, 1));
      Assert.Throws<InvalidOperationException>(() => new object[] { "1" }.AsSized(1).Add(new object()));
      Assert.Throws<InvalidOperationException>(() => new object[] { "1" }.AsSized(2, 1).Clear());
      Assert.Throws<InvalidOperationException>(() => new object[] { "1" }.AsSized(1, 1).Remove("1"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ICollectionExtensions.AsUnique{T}(ICollection{T})"/> method.</para>
    /// </summary> 
    [Fact]
    public void AsUnique_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ICollectionExtensions.AsUnique<object>(null));

      Assert.Throws<InvalidOperationException>(() =>
      {
        ICollection<object> collection = new List<object>().AsUnique();
        collection.Add("test");
        collection.Add("test");
      });

      ICollection<string> first = new List<string> { "test", "test" }.AsUnique();
      Assert.False(first.To<ICollection>().IsSynchronized);
      Assert.True(first.Count == 1);
      Assert.True(first.Single() == "test");

      ICollection<string> second = new List<string> { "1" }.AsUnique();
      second.Add("2");
      second.Remove("1");
      Assert.True(second.Count == 1);
      Assert.True(second.Single() == "2");
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
      Assert.True(first.Count == 2);
      Assert.True(first.ElementAt(0) == "1");
      Assert.True(first.ElementAt(1) == "3");
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
      Assert.True(first.Count == 1);
      Assert.True(first.Single() == "2");
      Assert.True(ReferenceEquals(first, second));
    }
  }
}