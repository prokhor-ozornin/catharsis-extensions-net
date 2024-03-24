using System.Collections.Generic;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IDictionaryExtensions"/>.</para>
/// </summary>
public sealed class IDictionaryExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IDictionaryExtensions.With{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{ValueTuple{TKey, TValue}})"/></description></item>
  ///     <item><description><see cref="IDictionaryExtensions.With{TKey, TValue}(IDictionary{TKey, TValue}, ValueTuple{TKey, TValue}[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IDictionaryExtensions.With(null, Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
      AssertionExtensions.Should(() => new Dictionary<string, object>().With((IEnumerable<(string Name, object Value)>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IDictionaryExtensions.With(null, Array.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
      AssertionExtensions.Should(() => new Dictionary<string, object>().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

    }

    throw new NotImplementedException();

    return;

    static void Validate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IEnumerable<(TKey key, TValue value)> elements)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IDictionaryExtensions.Without{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{TKey})"/></description></item>
  ///     <item><description><see cref="IDictionaryExtensions.Without{TKey, TValue}(IDictionary{TKey, TValue}, TKey[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<string, object>) null).Without(Enumerable.Empty<string>())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
      AssertionExtensions.Should(() => new Dictionary<string, object>().Without((IEnumerable<string>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<string, object>) null).Without(Array.Empty<string>())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
      AssertionExtensions.Should(() => new Dictionary<string, object>().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

    }

    throw new NotImplementedException();

    return;

    static void Validate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IEnumerable<(TKey key, TValue value)> elements)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.GetValueOrDefault{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/> method.</para>
  /// </summary>
  [Fact]
  public void GetValueOrDefault_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.GetValueOrDefault<object, object>(null, new object())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();

    return;

    static void Validate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.SetValueOrDefault{TKey,TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/> method.</para>
  /// </summary>
  [Fact]
  public void SetValueOrDefault_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.SetValueOrDefault<object, object>(null, new object())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();

    return;

    static void Validate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToSortedList{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedList_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.ToSortedList<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToSortedDictionary{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedDictionary_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.ToSortedDictionary<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToFrozenDictionary{TKey, TValue}(IDictionary{TKey, TValue}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToFrozenDictionary_Method()
  {
    AssertionExtensions.Should(() => IDictionaryExtensions.ToFrozenDictionary<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }
}