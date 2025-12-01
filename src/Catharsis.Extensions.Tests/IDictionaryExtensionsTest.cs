using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IDictionaryExtensions"/>.</para>
/// </summary>
/// <seealso cref="IDictionaryExtensions"/>
public sealed class IDictionaryExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.Get{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/> method.</para>
  /// </summary>
  [Fact]
  public void Get_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<object, object>) null).Get(new object())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
    }

    throw new NotImplementedException();

    return;

    static void Test<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.Set{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/> method.</para>
  /// </summary>
  [Fact]
  public void Set_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<object, object>) null).Set(new object())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
    }

    throw new NotImplementedException();

    return;

    static void Test<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IDictionaryExtensions.GetOrSet{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/></description></item>
  ///     <item><description><see cref="IDictionaryExtensions.GetOrSet{TKey, TValue}(IDictionary{TKey, TValue}, TKey, Func{TValue})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void GetOrSet_Methods()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IDictionaryExtensions.With{TKey, TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/></description></item>
  ///     <item><description><see cref="IDictionaryExtensions.With{TKey, TValue}(IDictionary{TKey, TValue}, IEnumerable{ValueTuple{TKey, TValue}})"/></description></item>
  ///     <item><description><see cref="IDictionaryExtensions.With{TKey, TValue}(IDictionary{TKey, TValue}, ValueTuple{TKey, TValue}[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<string, object>) null).With(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");

      static void Test<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<string, object>) null).With(Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
      AssertionExtensions.Should(() => new Dictionary<string, object>().With((IEnumerable<(string Name, object Value)>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      static void Test<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IEnumerable<(TKey key, TValue value)> elements)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<string, object>) null).With()).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
      AssertionExtensions.Should(() => new Dictionary<string, object>().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      static void Test<TKey, TValue>(IDictionary<TKey, TValue> dictionary, params (TKey key, TValue value)[] elements)
      {
      }
    }

    throw new NotImplementedException();
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

      static void Test<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IEnumerable<(TKey key, TValue value)> elements)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<string, object>) null).Without(Array.Empty<string>())).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
      AssertionExtensions.Should(() => new Dictionary<string, object>().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      static void Test<TKey, TValue>(IDictionary<TKey, TValue> dictionary, params (TKey key, TValue value)[] elements)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToSortedList{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedList_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<int, object>) null).ToSortedList()).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToSortedDictionary{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedDictionary_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<int, object>) null).ToSortedDictionary()).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToValueTuple{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToValueTuple_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<object, object>) null).ToValueTuple()).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToTuple{TKey, TValue}(IDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTuple_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<object, object>) null).ToTuple()).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToFrozenDictionary{TKey, TValue}(IDictionary{TKey, TValue}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToFrozenDictionary_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IDictionary<int, object>) null).ToFrozenDictionary()).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }
}