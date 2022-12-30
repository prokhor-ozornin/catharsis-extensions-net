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
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CollectionsExtensions.AddRange{T}(ICollection{T}, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="CollectionsExtensions.AddRange{T}(ICollection{T}, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ICollection_AddRange_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.AddRange<object>(null, Enumerable.Empty<object>)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Array.Empty<object>().AddRange((IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>();

      var collection = new List<object>();

      CollectionsExtensions.AddRange(collection, Enumerable.Empty<object>()).Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();

      IEnumerable<object> elements = new object[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };

      CollectionsExtensions.AddRange(collection, elements).Should().NotBeNull().And.BeSameAs(collection).And.Equal(elements);
      CollectionsExtensions.AddRange(collection, elements).Should().NotBeNull().And.BeSameAs(collection).And.Equal(elements.Concat(elements));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.AddRange<object>(null, Array.Empty<object>)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Array.Empty<object>().AddRange()).ThrowExactly<ArgumentNullException>();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CollectionsExtensions.AddRange(NameValueCollection, IEnumerable{(string Name, object Value)})"/></description></item>
  ///     <item><description><see cref="CollectionsExtensions.AddRange(NameValueCollection, (string Name, object Value)[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void NameValueCollection_AddRange_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((NameValueCollection) null).AddRange(Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new NameValueCollection().AddRange((IEnumerable<(string Name, object Value)>) null)).ThrowExactly<ArgumentNullException>();

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((NameValueCollection) null).AddRange(Array.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new NameValueCollection().AddRange(null)).ThrowExactly<ArgumentNullException>();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CollectionsExtensions.RemoveRange{T}(ICollection{T}, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="CollectionsExtensions.RemoveRange{T}(ICollection{T}, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ICollection_RemoveRange_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((ICollection<object>) null).RemoveRange(Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Array.Empty<object>().RemoveRange((IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>();

      var collection = new List<object>();
      collection.RemoveRange(Enumerable.Empty<object>()).Should().NotBeNull().And.BeSameAs(collection).And.Equal(collection);

      var elements = new object[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };

      collection = new List<object>(elements);
      collection.RemoveRange(Enumerable.Empty<object>()).Should().NotBeNull().And.BeSameAs(collection).And.Equal(elements);

      collection = new List<object>(elements);
      collection.RemoveRange(elements).Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((ICollection<object>) null).RemoveRange(Array.Empty<object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Array.Empty<object>().RemoveRange(null)).ThrowExactly<ArgumentNullException>();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.RemoveRange{T}(IList{T}, int, int?, Predicate{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IList_RemoveRange_Method()
  {
    AssertionExtensions.Should(() => ((IList<object>) null).RemoveRange(0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CollectionsExtensions.InsertRange{T}(IList{T}, int, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="CollectionsExtensions.InsertRange{T}(IList{T}, int, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IList_InsertRange_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.InsertRange<object>(null, 0, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Array.Empty<object>().InsertRange(0, (IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>();

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.InsertRange(null, 0, Array.Empty<object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Array.Empty<object>().InsertRange(0, null)).ThrowExactly<ArgumentNullException>();

    }

    throw new NotImplementedException();
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
      AssertionExtensions.Should(() => CollectionsExtensions.Empty<object>(null)).ThrowExactly<ArgumentNullException>();
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
      AssertionExtensions.Should(() => CollectionsExtensions.Empty<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(new NameValueCollection());
      Validate(new NameValueCollection().AddRange(RandomObjects.Select(element => (element.GetType().FullName, element))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="CollectionsExtensions.Fill{T}(IList{T}, Func{T}, int?, int?)"/></description></item>
  ///     <item><description><see cref="CollectionsExtensions.Fill{T}(IList{T}, Func{int, T}, int?, int?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IList_Fill_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.Fill(null, () => new object())).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => CollectionsExtensions.Fill(null, _ => new object())).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.Swap{T}(IList{T}, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void IList_Swap_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.Swap<object>(null, 1, 2)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.Randomize{T}(IList{T}, Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void IList_Randomize_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.Randomize<object>(null)).ThrowExactly<ArgumentNullException>();

    var collection = new List<object>();
    collection.Randomize().Should().BeSameAs(collection).And.BeEmpty();

    collection = new List<object> { string.Empty };
    collection.Randomize().Should().BeSameAs(collection).And.Equal(string.Empty);

    var sequence = new object[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };
    collection = new List<object>(sequence);
    collection.Randomize().Should().BeSameAs(collection).And.Contain(sequence);
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.TryFinallyClear{T}(ICollection{T}, Action{ICollection{T}})"/> method.</para>
  /// </summary>
  [Fact]
  public void ICollection_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((ICollection<object>) null).TryFinallyClear(_ => {})).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Array.Empty<object>().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Array.Empty<object>().TryFinallyClear(_ => { })).ThrowExactly<NotSupportedException>();

    var collection = new List<object>();
    collection.TryFinallyClear(collection => collection.Add(new object())).Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.TryFinallyClear(NameValueCollection, Action{NameValueCollection})"/> method.</para>
  /// </summary>
  [Fact]
  public void NameValueCollection_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((NameValueCollection) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new NameValueCollection().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>();

    var collection = new NameValueCollection();
    collection.TryFinallyClear(collection => collection.Add("key", "value")).Should().NotBeNull().And.BeSameAs(collection);
    collection.Count.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.AsReadOnly{T}(IList{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IList_AsReadOnly_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.AsReadOnly<object>(null)).ThrowExactly<ArgumentNullException>();

    var list = new List<object> { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };
    var readOnly = CollectionsExtensions.AsReadOnly(list);

    /*readOnly.Should().NotBeNull().And.NotBeSameAs(list).And.Equal(list);
    readOnly.IsReadOnly.Should().BeTrue();
    readOnly.GetType().Should().Implement<IReadOnlyList<object>>();

    AssertionExtensions.Should(readOnly.Clear).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Add(new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Insert(0, new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Remove(new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.RemoveAt(0)).ThrowExactly<NotSupportedException>();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.AsReadOnly{T}(IList{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IDictionary_AsReadOnly_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.AsReadOnly<int, object>(null)).ThrowExactly<ArgumentNullException>();

    /*var dictionary = new Dictionary<int, object> { {1 , 1 }, { 2, string.Empty }, { 3, "2" }, { 4, Guid.NewGuid() }, { 5, null }, { 6, 10.5} };
    var readOnly = dictionary.AsReadOnly();

    readOnly.Should().NotBeNull().And.NotBeSameAs(dictionary).And.Equal(dictionary);
    readOnly.IsReadOnly.Should().BeTrue();
    readOnly.Values.IsReadOnly.Should().BeTrue();
    readOnly.GetType().Should().Implement<IReadOnlyDictionary<int, object>>();

    AssertionExtensions.Should(readOnly.Clear).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Add(0, null)).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Remove(0)).ThrowExactly<NotSupportedException>();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToSortedList{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void IDictionary_ToSortedList_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ToSortedList<int, object>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToDictionary(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void NameValueCollection_ToDictionary_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ToDictionary(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToSortedDictionary{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void IDictionary_ToSortedDictionary_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ToSortedDictionary<int, object>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToValueTuple{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void IDictionary_ToValueTuple_Method()
  {
    AssertionExtensions.Should(() => ((Dictionary<object, object>) null).ToValueTuple()).ThrowExactly<ArgumentNullException>();
    
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="CollectionsExtensions.ToValueTuple(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void NameValueCollection_ToValueTuple_Method()
  {
    AssertionExtensions.Should(() => CollectionsExtensions.ToValueTuple(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
}