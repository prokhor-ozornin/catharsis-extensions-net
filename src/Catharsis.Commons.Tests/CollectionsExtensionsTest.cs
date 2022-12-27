using System.Collections.Specialized;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="CollectionsExtensions"/>.</para>
/// </summary>
public sealed class CollectionsExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.AddAll{T}(ICollection{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ICollection_AddAll_Method()
  {
    //AssertionExtensions.Should(() => CollectionsExtensions.AddAll(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => Array.Empty<object>().AddAll(null!)).ThrowExactly<ArgumentNullException>();

    var collection = new List<object?>();

    collection.AddAll(Enumerable.Empty<object?>()).Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();

    var elements = new object?[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };

    collection.AddAll(elements).Should().NotBeNull().And.BeSameAs(collection).And.Equal(elements);
    collection.AddAll(elements).Should().NotBeNull().And.BeSameAs(collection).And.Equal(elements.Concat(elements));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.AddAll(NameValueCollection, IEnumerable{(string Name, object? Value)})"/> method.</para>
  /// </summary>
  [Fact]
  public void NameValueCollection_AddAll_Method()
  {
    //AssertionExtensions.Should(() => CollectionsExtensions.AddAll(null!, Enumerable.Empty<(string Name, object? Value)>())).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => Array.Empty<object>().AddAll(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.RemoveAll{T}(ICollection{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ICollection_RemoveAll_Method()
  {
    //AssertionExtensions.Should(() => CollectionsExtensions.RemoveAll(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => Array.Empty<object>().RemoveAll(null!)).ThrowExactly<ArgumentNullException>();

    var collection = new List<object?>();
    collection.RemoveAll(Enumerable.Empty<object>()).Should().NotBeNull().And.BeSameAs(collection).And.Equal(collection);

    var elements = new object?[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };

    collection = new List<object?>(elements);
    collection.RemoveAll(Enumerable.Empty<object>()).Should().NotBeNull().And.BeSameAs(collection).And.Equal(elements);

    collection = new List<object?>(elements);
    collection.RemoveAll(elements).Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.Empty{T}(ICollection{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ICollection_Empty_Method()
  {
    void Validate<T>(ICollection<T> collection)
    {
      collection.Empty().Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => CollectionsExtensions.Empty<object>(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Array.Empty<object>().Empty()).ThrowExactly<NotSupportedException>();

      Validate(Array.Empty<object>().ToList());
      Validate(RandomObjects.ToList());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.Empty(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void NameValueCollection_Empty_Method()
  {
    void Validate(NameValueCollection collection)
    {
      collection.Empty().Should().NotBeNull().And.BeSameAs(collection);
      collection.Count.Should().Be(0);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => CollectionsExtensions.Empty<object>(null!)).ThrowExactly<ArgumentNullException>();

      Validate(new NameValueCollection());
      Validate(new NameValueCollection().AddAll(RandomObjects.Select(element => (element.GetType().FullName, (object?) element))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.UseTemporarily{T}(ICollection{T}, Action{ICollection{T}}"/> method.</para>
  /// </summary>
  [Fact]
  public void ICollection_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => ((ICollection<object>) null!).UseTemporarily(_ => {})).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => Array.Empty<object>().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Array.Empty<object>().UseTemporarily(_ => { })).ThrowExactly<NotSupportedException>();

    var collection = new List<object>();
    AssertionExtensions.Should(() => collection.ReadOnly().UseTemporarily(_ => { })).ThrowExactly<NotSupportedException>();
    collection.UseTemporarily(collection => collection.Add(new object())).Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.UseTemporarily(NameValueCollection, Action{NameValueCollection})"/> method.</para>
  /// </summary>
  [Fact]
  public void NameValueCollection_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => ((NameValueCollection) null!).UseTemporarily(_ => { })).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => new NameValueCollection().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    var collection = new NameValueCollection();
    collection.UseTemporarily(collection => collection.Add("key", "value")).Should().NotBeNull().And.BeSameAs(collection);
    collection.Count.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.Randomize{T}(IList{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IList_Randomize_Method()
  {
    //AssertionExtensions.Should(() => CollectionsExtensions.Randomize<object>(null!)).ThrowExactly<ArgumentNullException>();

    var collection = new List<object?>();
    collection.Randomize().Should().BeSameAs(collection).And.BeEmpty();

    collection = new List<object?> { string.Empty };
    collection.Randomize().Should().BeSameAs(collection).And.Equal(string.Empty);

    var sequence = new object?[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };
    collection = new List<object?>(sequence);
    collection.Randomize().Should().BeSameAs(collection).And.Contain(sequence);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CollectionsExtensions.Fill{T}(IList{T}, Func{int, T})"/></description></item>
  ///     <item><description><see cref="CollectionsExtensions.Fill{T}(IList{T}, Func{T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IList_Fill_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.Fill(null!, _ => new object())).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.Fill(null!, () => new object())).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.Swap{T}(IList{T}, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void IList_Swap_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.Swap<object>(null!, 1, 2)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CollectionsExtensions.Discard{T}(IList{T}, int, int, Predicate{T}?)"/></description></item>
  ///     <item><description><see cref="CollectionsExtensions.Discard{T}(IList{T}, Range, Predicate{T}?)"/></description></item>
  ///     <item><description><see cref="CollectionsExtensions.Discard{T}(IList{T}, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IList_Discard_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.Discard<object>(null!, 1, 2)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.Discard<object>(null!, 1..2)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.Discard<object>(null!, 0)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.DiscardLast{T}(IList{T}, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void IList_DiscardLast_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.DiscardLast<object>(null!, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ReadOnly{T}(IList{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IList_ReadOnly_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ReadOnly<object>(null!)).ThrowExactly<ArgumentNullException>();

    var list = new List<object?> { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };
    var readOnly = list.ReadOnly();

    readOnly.Should().NotBeNull().And.NotBeSameAs(list).And.Equal(list);
    readOnly.IsReadOnly.Should().BeTrue();
    readOnly.GetType().Should().Implement<IReadOnlyList<object?>>();

    AssertionExtensions.Should(readOnly.Clear).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Add(new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Insert(0, new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Remove(new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.RemoveAt(0)).ThrowExactly<NotSupportedException>();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ReadOnly{TKey, TValue}(IDictionary{TKey, TValue})"/> method.</para>
  /// </summary>
  [Fact]
  public void IDictionary_ReadOnly_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ReadOnly<int, object>(null!)).ThrowExactly<ArgumentNullException>();

    var dictionary = new Dictionary<int, object?> { {1 , 1 }, { 2, string.Empty }, { 3, "2" }, { 4, Guid.NewGuid() }, { 5, null }, { 6, 10.5} };
    var readOnly = dictionary.ReadOnly();

    readOnly.Should().NotBeNull().And.NotBeSameAs(dictionary).And.Equal(dictionary);
    readOnly.IsReadOnly.Should().BeTrue();
    readOnly.Values.IsReadOnly.Should().BeTrue();
    readOnly.GetType().Should().Implement<IReadOnlyDictionary<int, object?>>();

    AssertionExtensions.Should(readOnly.Clear).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Add(0, null)).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Remove(0)).ThrowExactly<NotSupportedException>();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.Sorted{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IDictionary_Sorted_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.Sorted<int, object>(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToSortedList{TKey, TValue}(IDictionary{TKey, TValue?}, IComparer{TKey}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IDictionary_ToSortedList_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ToSortedList<int, object>(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToTuple{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IDictionary_ToTuple_Method()
  {
    AssertionExtensions.Should(() => ((Dictionary<object, object>) null!).ToTuple()).ThrowExactly<ArgumentNullException>();
    
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToDictionary(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void NameValueCollection_ToDictionary_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ToDictionary(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToTuple(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void NameValueCollection_ToTuple_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ToTuple(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
}