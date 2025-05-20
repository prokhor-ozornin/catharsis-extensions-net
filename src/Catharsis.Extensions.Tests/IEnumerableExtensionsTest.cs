using AutoFixture;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IEnumerableExtensions"/>.</para>
/// </summary>
public sealed class IEnumerableExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IEnumerableExtensions.ForEach{T}(IEnumerable{T}, Action{T})"/></description></item>
  ///     <item><description><see cref="IEnumerableExtensions.ForEach{T}(IEnumerable{T}, Action{int, T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ForEach_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ForEach<object>(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ForEach((Action<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      static void Test<T>(IEnumerable<T> enumerable)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ForEach<object>(null, (_, _) => { })).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ForEach((Action<int, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      static void Test<T>(IEnumerable<T> enumerable)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Join{T}(IEnumerable{T}, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Join_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.Join<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().Join().Should().BeOfType<string>().And.BeEmpty();
      Enumerable.Empty<object>().Join(",").Should().BeOfType<string>().And.BeEmpty();

      new object[] { null, string.Empty, "*", null }.Join().Should().BeOfType<string>().And.Be("*");
      new object[] { null, string.Empty, "*", null }.Join(",").Should().BeOfType<string>().And.Be("*");
      new object[] { null, string.Empty, "*", 100, null, "#" }.Join(",").Should().BeOfType<string>().And.Be("*,100,#");
    }

    return;

    static void Test<T>(string result, IEnumerable<T> enumerable, string separator = null) => enumerable.Join(separator).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Repeat{T}(IEnumerable{T}, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Repeat_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.Repeat<object>(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().Repeat(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      Enumerable.Empty<object>().Repeat(0).Should().BeOfType<IEnumerable<object>>().And.BeEmpty();
      Enumerable.Empty<object>().Repeat(1).Should().BeOfType<IEnumerable<object>>().And.BeEmpty();

      var enumerable = new object[] { null, 1, 55.5, string.Empty, Guid.Empty, null };

      enumerable.Repeat(0).Should().BeOfType<IEnumerable<object>>().And.BeEmpty();
      enumerable.Repeat(1).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(enumerable).And.Equal(enumerable);
      enumerable.Repeat(2).Should().BeOfType<IEnumerable<object>>().And.Equal(enumerable.Concat(enumerable));
      enumerable.Repeat(3).Should().BeOfType<IEnumerable<object>>().And.Equal(enumerable.Concat(enumerable).Concat(enumerable));
    }

    return;

    static void Test<T>(IEnumerable<T> result, IEnumerable<T> enumerable, int count) => enumerable.Repeat(count).Should().BeOfType<IEnumerable<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Range{T}(IEnumerable{T}, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.Range<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().Range(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().Range(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Random{T}(IEnumerable{T}, Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.Random<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().Random().Should().BeNull();

      var element = new object();
      new[] { element }.Random().Should().BeOfType<object>().And.BeSameAs(element);

      string[] elements = ["1", "2"];
      elements.Should().Contain(elements.Random());
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(IEnumerable<T> enumerable)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Randomize{T}(IEnumerable{T}, Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Randomize_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.Randomize<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      IEnumerable<object> collection = [];
      collection.Randomize().Should().BeOfType<IEnumerable<object>>().And.NotBeSameAs(collection).And.BeEmpty();

      collection = new object[] { string.Empty };
      collection.Randomize().Should().BeOfType<IEnumerable<object>>().And.NotBeSameAs(collection).And.Equal(string.Empty);

      var enumerable = new object[] { 1, 2, 3, 4, 5 };
      collection = new List<object>(enumerable);
      collection.Randomize().Should().BeOfType<IEnumerable<object>>().And.NotBeSameAs(collection).And.Contain(enumerable);
    }

    return;

    static void Test<T>(IEnumerable<T> enumerable)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.StartsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void StartsWith_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.StartsWith(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().StartsWith(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable, IEnumerable<T> other, IEqualityComparer<T> comparer = null) => enumerable.StartsWith(other, comparer).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.EndsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void EndsWith_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.EndsWith(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().EndsWith(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable, IEnumerable<T> other, IEqualityComparer<T> comparer = null) => enumerable.EndsWith(other, comparer).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Contains{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Contains_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.Contains(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().Contains(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

      Enumerable.Empty<object>().Contains([]).Should().BeTrue();

      Enumerable.Empty<object>().Contains(new object[] { null }).Should().BeFalse();
      new object[] { null }.Contains([]).Should().BeTrue();
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable, IEnumerable<T> other, IEqualityComparer<T> comparer = null) => enumerable.Contains(other, comparer).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ContainsUnique{T}(IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ContainsUnique_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ContainsUnique<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => enumerable.ContainsUnique(comparer).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ContainsNull{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ContainsNull_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ContainsNull<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable) => enumerable.ContainsNull().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ContainsDefault{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ContainsDefault_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ContainsDefault<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable) => enumerable.ContainsDefault().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.IsSubset{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IsSubset_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.IsSubset(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().IsSubset(null)).ThrowExactly<ArgumentNullException>().WithParameterName("superset");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable, IEnumerable<T> superset, IEqualityComparer<T> comparer = null) => enumerable.IsSubset(superset, comparer).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.IsSuperset{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IsSuperset_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.IsSuperset(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().IsSuperset(null)).ThrowExactly<ArgumentNullException>().WithParameterName("subset");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable, IEnumerable<T> subset, IEqualityComparer<T> comparer = null) => enumerable.IsSuperset(subset, comparer).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.IsReversed{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IsReversed_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.IsReversed(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().IsReversed(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reversed");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable, IEnumerable<T> reversed, IEqualityComparer<T> comparer = null) => enumerable.IsReversed(reversed, comparer).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.IsOrdered{T}(IEnumerable{T}, IComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IsOrdered_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.IsOrdered<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable, IComparer<T> comparer = null) => enumerable.IsOrdered(comparer).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WithCancellation{T}(IEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithCancellation_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.WithCancellation<object>(null, CancellationToken.None)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => IEnumerableExtensions.WithCancellation<object>(null, CancellationToken.None)).ThrowExactly<OperationCanceledException>();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.IsUnset{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable) => enumerable.IsUnset().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.IsEmpty{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.IsEmpty<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().IsEmpty().Should().BeTrue();
      Array.Empty<object>().IsEmpty().Should().BeTrue();

      new object[] { null }.IsEmpty().Should().BeFalse();

      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable) => enumerable.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Min{T}(IEnumerable{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.Min(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("min");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("max");

      var first = Enumerable.Empty<object>();
      var second = Enumerable.Empty<object>();
      first.Min(second).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(first);

      first = [];
      second = [null];
      first.Min(second).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(first);

      first = [string.Empty];
      second = [null];
      first.Min(second).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(first);

      first = [string.Empty];
      second = [null, string.Empty];
      first.Min(second).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(first);
    }

    return;

    static void Test<T>(IEnumerable<T> min, IEnumerable<T> max) => min.Min(max).Should().BeOfType<IEnumerable<T>>().And.BeSameAs(min);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Max{T}(IEnumerable{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.Max(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("min");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("max");

      var first = Enumerable.Empty<object>();
      var second = Enumerable.Empty<object>();
      first.Max(second).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(first);

      first = [];
      second = [null];
      first.Max(second).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(second);

      first = [string.Empty];
      second = [null];
      first.Max(second).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(first);

      first = [string.Empty];
      second = [null, string.Empty];
      first.Max(second).Should().BeOfType<IEnumerable<object>>().And.BeSameAs(second);
    }

    return;

    static void Test<T>(IEnumerable<T> min, IEnumerable<T> max) => min.Max(max).Should().BeOfType<IEnumerable<T>>().And.BeSameAs(max);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.MinMax{T}(IEnumerable{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.MinMax(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(IEnumerable<T> min, IEnumerable<T> max) => min.MinMax(max).Should().Be((min, max));
  }























  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.AsArray{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void AsArray_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.AsArray<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().AsArray().Should().BeOfType<object[]>().And.BeEmpty().And.BeSameAs(Enumerable.Empty<object>().AsArray());

      object[] array = [];
      array.AsArray().Should().BeOfType<object[]>().And.BeSameAs(array);

      var list = new List<object> {null, 1, 55.5, string.Empty, Guid.Empty, null};
      list.AsArray().Should().BeOfType<object[]>().And.NotBeSameAs(list).And.Equal(list);
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.AsNotNullable{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void AsNotNullable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.AsNotNullable<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().AsNotNullable().Should().BeOfType<IEnumerable<object>>().And.BeEmpty();

      var enumerable = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
      enumerable.AsNotNullable().Should().BeOfType<IEnumerable<object>>().And.Equal(enumerable.Where(element => element is not null));
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToBase64(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBase64_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).ToBase64()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToHex(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToHex_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToHex(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

      var bytes = Bytes;

      Enumerable.Empty<byte>().ToHex().Should().BeEmpty();
      bytes.ToHex().Should().HaveLength(bytes.Length * 2);
      bytes.ToHex().IsMatch("[0-9A-Z]").Should().BeTrue();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Encrypt(IEnumerable{byte}, SymmetricAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Encrypt_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).Encrypt(SymmetricAlgorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().Encrypt(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    }

    throw new NotImplementedException();

    return;

    static void Test(IEnumerable<byte> result, IEnumerable<byte> bytes, SymmetricAlgorithm algorithm)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.EncryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void EncryptAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).EncryptAsync(SymmetricAlgorithm)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().EncryptAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().EncryptAsync(SymmetricAlgorithm)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(IEnumerable<byte> result, IEnumerable<byte> bytes, SymmetricAlgorithm algorithm)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Decrypt(IEnumerable{byte}, SymmetricAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Decrypt_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).Decrypt(SymmetricAlgorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().Decrypt(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");
    }

    throw new NotImplementedException();

    return;

    static void Test(IEnumerable<byte> result, IEnumerable<byte> bytes, SymmetricAlgorithm algorithm)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.DecryptAsync(IEnumerable{byte}, SymmetricAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecryptAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).DecryptAsync(SymmetricAlgorithm)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => System.IO.Stream.Null.DecryptAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();
      AssertionExtensions.Should(() => System.IO.Stream.Null.DecryptAsync(SymmetricAlgorithm)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(IEnumerable<byte> result, IEnumerable<byte> bytes, SymmetricAlgorithm algorithm)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.Hash(IEnumerable{byte}, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Hash_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().Hash(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

      using var algorithm = MD5.Create();

      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).Hash(HashAlgorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

      algorithm.Should().BeOfType<MD5>();

      Enumerable.Empty<byte>().Hash(HashAlgorithm).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash([]));

      var bytes = Bytes;
      bytes.Hash(HashAlgorithm).Should().BeOfType<byte[]>().And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(bytes));
    }

    return;

    static void Test(byte[] bytes, HashAlgorithm algorithm)
    {
      using (algorithm)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.HashMd5(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void HashMd5_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashMd5()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

      Test(Enumerable.Empty<byte>().ToArray());
      Test(Bytes);
    }

    return;

    static void Test(byte[] bytes)
    {
      using var algorithm = MD5.Create();
      bytes.HashMd5().Should().BeOfType<byte[]>().And.HaveCount(16).And.Equal(algorithm.ComputeHash(bytes));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.HashSha1(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha1_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashSha1()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

      Test(Enumerable.Empty<byte>().ToArray());
      Test(Bytes);
    }

    return;

    static void Test(byte[] bytes)
    {
      using var algorithm = SHA1.Create();
      bytes.HashSha1().Should().BeOfType<byte[]>().And.HaveCount(20).And.Equal(algorithm.ComputeHash(bytes));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.HashSha256(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha256_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashSha256()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

      Test(Enumerable.Empty<byte>().ToArray());
      Test(Bytes);
    }

    return;

    static void Test(byte[] bytes)
    {
      using var algorithm = SHA256.Create();
      bytes.HashSha256().Should().BeOfType<byte[]>().And.HaveCount(32).And.Equal(algorithm.ComputeHash(bytes));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.HashSha384(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha384_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashSha384()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

      Test(Enumerable.Empty<byte>().ToArray());
      Test(Bytes);
    }

    return;

    static void Test(byte[] bytes)
    {
      using var algorithm = SHA384.Create();
      bytes.HashSha384().Should().BeOfType<byte[]>().And.HaveCount(48).And.Equal(algorithm.ComputeHash(bytes));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.HashSha512(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha512_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).HashSha512()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

      Test(Enumerable.Empty<byte>().ToArray());
      Test(Bytes);
    }

    return;

    static void Test(byte[] bytes)
    {
      using var algorithm = SHA512.Create();
      bytes.HashSha512().Should().BeOfType<byte[]>().And.HaveCount(64).And.Equal(algorithm.ComputeHash(bytes));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_IEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(System.IO.Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => IEnumerableExtensions.WriteTo([], (Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test(Stream stream, byte[] bytes)
    {
      using (stream)
      {
        bytes.WriteTo(stream).Should().BeOfType<byte[]>().And.BeSameAs(bytes);
        //
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_IEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(System.IO.Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => IEnumerableExtensions.WriteToAsync([], (Stream) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(System.IO.Stream.Null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(Stream stream, byte[] bytes)
    {
      using (stream)
      {
        var task = bytes.WriteToAsync(stream);
        task.Should().BeAssignableTo<Task<IEnumerable<byte>>>();
        //
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, TextWriter, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(System.IO.Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => string.Empty.WriteTo((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, TextWriter, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(System.IO.Stream.Null.ToStreamWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync((TextWriter) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(System.IO.Stream.Null.ToStreamWriter())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_BinaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(System.IO.Stream.Null.ToBinaryWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo((BinaryWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

      Test(EmptyStream.ToBinaryWriter(), Bytes);
      Test(Stream.ToBinaryWriter(), Bytes);
    }

    return;

    static void Test(BinaryWriter writer, byte[] bytes)
    {
      using (writer)
      {
        writer.BaseStream.MoveToEnd();

        var count = bytes.Length;
        var length = writer.BaseStream.Length;
        var position = writer.BaseStream.Position;

        bytes.WriteTo(writer).Should().BeOfType<byte[]>().And.BeSameAs(bytes);

        writer.BaseStream.Length.Should().Be(length + count);
        writer.BaseStream.Position.Should().Be(position + count);
        writer.BaseStream.MoveBy(-count).ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, XmlWriter, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(System.IO.Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, XmlWriter, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(System.IO.Stream.Null.ToXmlWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync((XmlWriter) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(FakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(FakeFile)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync((FileInfo) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(FakeFile)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, Uri, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_Uri_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo("localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo((Uri) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, Uri, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_Uri_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync("localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync((Uri) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync("localhost".ToUri(), null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_Process_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo((Process) null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_Process_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync((Process) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
      //AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(ShellProcess)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_HttpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Fixture.Create<HttpClient>(), "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(Fixture.Create<HttpClient>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_HttpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Fixture.Create<HttpClient>(), "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Fixture.Create<HttpClient>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Fixture.Create<HttpClient>(), "localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_TcpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Fixture.Create<TcpClient>())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(Fixture.Create<TcpClient>())).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, TcpClient, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_TcpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Fixture.Create<TcpClient>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Fixture.Create<TcpClient>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Fixture.Create<TcpClient>())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{byte}, UdpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_UdpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Fixture.Create<UdpClient>())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteTo(Fixture.Create<UdpClient>())).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteToAsync(IEnumerable{byte}, UdpClient, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_UdpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Fixture.Create<UdpClient>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Fixture.Create<UdpClient>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Fixture.Create<UdpClient>())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.WriteTo(IEnumerable{char}, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.WriteTo(null, EmptySecureString)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => IEnumerableExtensions.WriteTo(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test(IEnumerable<char> text, SecureString destination)
    {
      using (destination)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToAsyncEnumerable{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToAsyncEnumerable<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Test(Enumerable.Empty<object>());
      Test(Array.Empty<object>());
      Test(new Random().ObjectSequence(1000).ToArray());
    }

    return;

    static void Test<T>(IEnumerable<T> enumerable)
    {
      var result = enumerable.ToAsyncEnumerable();
      result.Should().BeOfType<IAsyncEnumerable<T>>();
      result.ToArray().Should().BeOfType<T[]>().And.Equal(enumerable.ToArray());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToLinkedList{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToLinkedList_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToLinkedList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().ToLinkedList().Should().BeOfType<LinkedList<object>>().And.BeEmpty();

      IEnumerable<int?> enumerable = [1, null, 2, null, 3];
      enumerable.ToLinkedList().Should().BeOfType<LinkedList<int?>>().And.Equal(enumerable);
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToReadOnlyList{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyList_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToReadOnlyList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      throw new NotImplementedException();
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToSortedSet{T}(IEnumerable{T}, IComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedSet_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToSortedSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().ToSortedSet().Should().BeOfType<SortedSet<object>>().And.BeEmpty();

      IEnumerable<int?> enumerable = [1, null, 2, null, 3, null, 3, 2, 1];
      enumerable.ToSortedSet().Should().BeOfType<SortedSet<int?>>().And.Equal(null, 1, 2, 3);
      enumerable.ToSortedSet(Comparer<int?>.Create((x, y) => x.GetValueOrDefault() < y.GetValueOrDefault() ? 1 : x.GetValueOrDefault() > y.GetValueOrDefault() ? -1 : 0)).Should().BeOfType<SortedSet<int?>>().And.Equal(3, 2, 1, null);
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToReadOnlySet{T}(IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlySet_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToReadOnlySet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(IEnumerable<T> result, IEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => enumerable.ToReadOnlySet(comparer).Should().BeOfType<HashSet<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToFrozenSet{T}(IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToFrozenSet_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToFrozenSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(IEnumerable<T> result, IEnumerable<T> enumerable, IEqualityComparer<T> comparer = null) => enumerable.ToFrozenSet(comparer).Should().BeOfType<FrozenSet<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToStack{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStack_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToStack<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().ToStack().Should().BeOfType<Stack<object>>().And.BeEmpty();

      IEnumerable<int?> enumerable = [null, 1, null, 2, null, 3, null];
      enumerable.ToStack().Should().BeOfType<Stack<int?>>().And.Equal(enumerable.Reverse());
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToQueue{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToQueue_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToQueue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToPriorityQueue{TElement, TPriority}(IEnumerable{ValueTuple{TElement, TPriority}, IComparer{TPriority})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToPriorityQueue_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToPriorityQueue<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test<TElement, TPriority>(IEnumerable<(TElement Element, TPriority Priority)> enumerable, IComparer<TPriority> comparer = null)
    {
      var array = enumerable.ToArray();
      var queue = array.ToPriorityQueue(comparer);
      queue.Should().BeOfType<PriorityQueue<TElement, TPriority>>();
      queue.UnorderedItems.Order().Should().Equal(array);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToImmutableQueue{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableQueue_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToImmutableQueue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(IEnumerable<T> result, IEnumerable<T> enumerable) => enumerable.ToImmutableQueue().Should().BeOfType<ImmutableQueue<T>>().And.Equal(result);
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToArraySegment{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToArraySegment_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToArraySegment<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().ToArraySegment().Should().BeOfType<ArraySegment<object>>().And.BeEmpty();

      var enumerable = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
      var result = enumerable.ToArraySegment();
      result.Should().BeOfType<ArraySegment<object>>().And.Equal(enumerable);
      result.Array.Should().BeSameAs(enumerable);
      result.Offset.Should().Be(0);
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToMemory{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToMemory_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToMemory<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      var memory = Enumerable.Empty<object>().ToMemory();
      memory.Should().BeOfType<Memory<object>>();
      memory.IsEmpty.Should().BeTrue();

      var enumerable = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
      var result = enumerable.ToMemory();
      result.Should().BeOfType<Memory<object>>();
      result.Length.Should().Be(enumerable.Length);
      result.ToArray().Should().BeOfType<object[]>().And.Equal(enumerable);
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToReadOnlyMemory{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyMemory_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToReadOnlyMemory<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      var memory = Enumerable.Empty<object>().ToReadOnlyMemory();
      memory.IsEmpty.Should().BeTrue();
      memory.Should().BeOfType<ReadOnlyMemory<object>>();

      var enumerable = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
      var result = enumerable.ToReadOnlyMemory();
      result.Should().BeOfType<ReadOnlyMemory<object>>();
      result.Length.Should().Be(enumerable.Length);
      result.ToArray().Should().BeOfType<object[]>().And.Equal(enumerable);
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToSpan{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSpan_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToSpan<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().ToSpan().IsEmpty.Should().BeTrue();

      var enumerable = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
      var result = enumerable.ToSpan();
      result.Length.Should().Be(enumerable.Length);
      result.ToArray().Should().BeOfType<object[]>().And.Equal(enumerable);
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToReadOnlySpan{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlySpan_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToReadOnlySpan<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      Enumerable.Empty<object>().ToReadOnlySpan().IsEmpty.Should().BeTrue();

      var enumerable = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
      var result = enumerable.ToReadOnlySpan();
      result.Length.Should().Be(enumerable.Length);
      result.ToArray().Should().BeOfType<object[]>().And.Equal(enumerable);
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToRange(IEnumerable{Range})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToRange_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToRange(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IEnumerableExtensions.ToValueTuple{T}(IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="IEnumerableExtensions.ToValueTuple{TKey, TValue}(IEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToValueTuple_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToValueTuple<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToValueTuple<object, object>(null, element => element)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToValueTuple<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

      static void Test()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IEnumerableExtensions.ToTuple{T}(IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="IEnumerableExtensions.ToTuple{TKey, TValue}(IEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToTuple_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToTuple<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToTuple<object, object>(null, element => element)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToTuple<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

      static void Test()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToDictionary{TKey, TValue}(IEnumerable{ValueTuple{TKey, TValue}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDictionary_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToDictionary<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToReadOnlyDictionary{TKey, TValue}(IEnumerable{TKey, TValue, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyDictionary_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToReadOnlyDictionary<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToText(IEnumerable{char})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IEnumerableExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");
    }

    throw new NotImplementedException();

    return;

    static void Test(IEnumerable<char> characters)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IEnumerableExtensions.ToMemoryStream(IEnumerable{byte})"/></description></item>
  ///     <item><description><see cref="IEnumerableExtensions.ToMemoryStream(IEnumerable{byte[]})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToMemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte[]>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>().WithParameterName("enumerable");

      static void Test()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IEnumerableExtensions.ToMemoryStreamAsync(IEnumerable{byte}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="IEnumerableExtensions.ToMemoryStreamAsync(IEnumerable{byte[]}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToMemoryStreamAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("enumerable").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().ToMemoryStreamAsync(CancellationToken.None)).ThrowExactlyAsync<OperationCanceledException>().Await();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte[]>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("enumerable").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().ToMemoryStreamAsync(CancellationToken.None)).ThrowExactlyAsync<OperationCanceledException>().Await();

      static void Test()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IEnumerableExtensions.ToBoolean{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBoolean_Method()
  {
    using (new AssertionScope())
    {
      Test<object>(false, null);
      Test(false, Enumerable.Empty<object>());
      Test(true, new object[] { new() });
    }

    return;

    static void Test<T>(bool result, IEnumerable<T> enumerable) => enumerable.ToBoolean().Should().Be(result);
  }
}