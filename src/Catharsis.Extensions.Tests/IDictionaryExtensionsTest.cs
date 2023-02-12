using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IDictionaryExtensions"/>.</para>
/// </summary>
public sealed class IDictionaryExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.AsReadOnly{T}(IList{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnly_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.AsReadOnly<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

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
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToSortedList{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedList_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.ToSortedList<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToSortedDictionary{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedDictionary_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.ToSortedDictionary<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();
  }
}