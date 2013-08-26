using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IDictionaryExtensions"/>.</para>
  /// </summary>
  public sealed class IDictionaryExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AddNext{TKey,TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/> method.</para>
    /// </summary>
    [Fact]
    public void AddNext_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AddNext(null, new object(), new object()));
      Assert.Throws<ArgumentNullException>(() =>
      {
        new Dictionary<object, object>().AddNext(null, new object());
      });

      IDictionary<string, string> first = new Dictionary<string, string>();
      var second = first.AddNext("key", "value");
      Assert.True(first.Count == 1);
      Assert.True(first.Single().Key == "key");
      Assert.True(first.Single().Value == "value");
      Assert.True(ReferenceEquals(first, second));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AsConstrained{TKey, TValue}(IDictionary{TKey, TValue}, Predicate{KeyValuePair{TKey,TValue}})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsConstrained_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AsConstrained<object, object>(null, x => true));
      Assert.Throws<ArgumentNullException>(() => new Dictionary<object, object>().AsConstrained(null));
      Assert.Throws<ArgumentException>(() => new Dictionary<object, object>().AsConstrained(x => false).Add("key", "value"));
      Assert.Throws<ArgumentException>(() => new Dictionary<object, object>().AsConstrained(x => false).Add(new KeyValuePair<object, object>("key", "value")));

      var dictionary = new Dictionary<object, object>().AsConstrained(x => true);
      dictionary.Add("key", "value");
      dictionary.Add(new KeyValuePair<object, object>("key2", "value2"));
      Assert.True(dictionary.Count == 2);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AsImmutable{TKey, TValue}(IDictionary{TKey, TValue})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsImmutable_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AsImmutable<object, object>(null));

      var dictionary = new Dictionary<object, object>().AsImmutable();
      Assert.True(dictionary.IsReadOnly);
      Assert.Throws<NotSupportedException>(() => dictionary.Add(new object(), new object()));
      Assert.Throws<NotSupportedException>(() => dictionary.Add(new KeyValuePair<object, object>()));
      Assert.Throws<NotSupportedException>(() => dictionary.Clear());
      Assert.Throws<NotSupportedException>(() => dictionary.Remove(new object()));
      Assert.Throws<NotSupportedException>(() => dictionary.Remove(new KeyValuePair<object, object>()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AsMaxSized{TKey, TValue}(IDictionary{TKey,TValue}, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsMaxSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AsMaxSized<object, object>(null, 0));
      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object>().AsMaxSized(-1).Add(new object(), new object()));
      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object>().AsMaxSized(0).Add(new object(), new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AsMinSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsMinSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AsMinSized<object, object>(null, 0));

      new Dictionary<object, object>().AsMinSized(-1).Add(new object(), new object());
      new Dictionary<object, object>().AsMinSized(0).Add(new object(), new object());

      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object>().AsMinSized(1));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AsNonNullable{TKey, TValue}(IDictionary{TKey, TValue})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsNonNullable_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AsNonNullable<object, object>(null));

      var dictionary = new Dictionary<object, object>().AsNonNullable();
      Assert.Throws<ArgumentNullException>(() => dictionary.Add(null, new object()));
      Assert.Throws<ArgumentNullException>(() => dictionary.Add(new object(), null));
      Assert.Throws<ArgumentNullException>(() => dictionary.ContainsKey(null));
      Assert.Throws<ArgumentNullException>(() => dictionary.CopyTo(null, 0));
      Assert.Throws<ArgumentNullException>(() => dictionary.Remove(null));

      Assert.Throws<ArgumentNullException>(() =>
      {
        object value;
        dictionary.TryGetValue(null, out value);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AsSized{TKey, TValue}(IDictionary{TKey, TValue}, int?, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void AsSized_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AsSized<object, object>(null, 0));
      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object> {{"key", "value"}}.AsSized(-1, -1));
      Assert.Throws<ArgumentException>(() => new Dictionary<object, object>().AsSized(1, 2));
      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object> {{"key", "value"}}.AsSized(0));
      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object>().AsSized(2, 1));
      Assert.Throws<ArgumentNullException>(() => new Dictionary<object, object>().AsSized(1).Add(null, new object()));
      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object> {{"key", "value"}}.AsSized(1).Add(new object(), new object()));
      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object> {{"key", "value"}}.AsSized(2, 1).Clear());
      Assert.Throws<InvalidOperationException>(() => new Dictionary<object, object> {{"key", "value"}}.AsSized(1, 1).Remove("key"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AsUnique{TKey, TValue}(IDictionary{TKey, TValue})"/> method.</para>
    /// </summary>
    [Fact]
    public void AsUnique_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AsUnique<object, object>(null));

      Assert.Throws<InvalidOperationException>(() =>
      {
        var dictionary = new Dictionary<object, object>().AsUnique();
        dictionary.Add("key", "value");
        dictionary.Add("key", "value");
      });

      var first = new Dictionary<string, string> { { "key", "value" } }.AsUnique();
      Assert.True(first.Count == 1);
      Assert.True(first.Single().Key == "key");
      Assert.True(first.Single().Value == "value");

      var second = new Dictionary<string, string> { { "key1", "value1" } }.AsUnique();
      second.Add("key2", "value2");
      second.Remove("key1");
      Assert.True(second.Count == 1);
      Assert.True(second.Single().Key == "key2");
      Assert.True(second.Single().Value == "value2");
    }
  }
}