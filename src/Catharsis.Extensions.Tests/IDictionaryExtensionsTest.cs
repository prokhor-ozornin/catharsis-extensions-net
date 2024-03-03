using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IDictionaryExtensions"/>.</para>
/// </summary>
public sealed class IDictionaryExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.GetValueOrDefault{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/> method.</para>
  /// </summary>
  [Fact]
  public void GetValueOrDefault_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.GetValueOrDefault<object, object>(null, new object())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.SetValueOrDefault{TKey,TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/> method.</para>
  /// </summary>
  [Fact]
  public void SetValueOrDefault_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.SetValueOrDefault<object, object>(null, new object())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

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

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToFrozenDictionary{TKey, TValue}(IDictionary{TKey, TValue}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToFrozenDictionary_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.ToFrozenDictionary<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();
  }
}