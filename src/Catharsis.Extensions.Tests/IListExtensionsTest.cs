using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IListExtensions"/>.</para>
/// </summary>
public sealed class IListExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IListExtensions.With{T}(IList{T}, int, T)"/></description></item>
  ///     <item><description><see cref="IListExtensions.With{T}(IList{T}, int, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="IListExtensions.With{T}(IList{T}, int, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      static void Validate<T>(IList<T> list)
      {

      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IListExtensions.With<object>(null, 0, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("to");
      AssertionExtensions.Should(() => Array.Empty<object>().With(0, (IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("from");
      AssertionExtensions.Should(() => Array.Empty<object>().With(-1, Enumerable.Empty<object>())).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");

      static void Validate<T>(IList<T> list)
      {

      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IListExtensions.With<object>(null, default, null)).ThrowExactly<ArgumentNullException>().WithParameterName("list");
      AssertionExtensions.Should(() => Array.Empty<object>().With(-1, null)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("position");

      static void Validate<T>(IList<T> list)
      {

      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IListExtensions.Without{T}(IList{T}, IEnumerable{int})"/></description></item>
  ///     <item><description><see cref="IListExtensions.Without{T}(IList{T}, int[])"/></description></item>
  ///     <item><description><see cref="IListExtensions.Without{T}(IList{T}, int, int?, Predicate{T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      static void Validate<T>(IList<T> list)
      {

      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IListExtensions.Without<object>(null, default)).ThrowExactly<ArgumentNullException>().WithParameterName("list");
      AssertionExtensions.Should(() => Array.Empty<object>().Without(new[] { -1 })).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("position");

      static void Validate<T>(IList<T> list)
      {

      }
    }

    using (new AssertionScope())
    {
      static void Validate<T>(IList<T> list)
      {

      }
    }
    
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IListExtensions.Fill{T}(IList{T}, Func{T}, int?, int?)"/></description></item>
  ///     <item><description><see cref="IListExtensions.Fill{T}(IList{T}, Func{int, T}, int?, int?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Fill_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IListExtensions.Fill(null, () => new object())).ThrowExactly<ArgumentNullException>().WithParameterName("list");
      AssertionExtensions.Should(() => Array.Empty<object>().Fill((Func<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("filler");
      AssertionExtensions.Should(() => Array.Empty<object>().Fill(_ => null, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
      AssertionExtensions.Should(() => Array.Empty<object>().Fill(_ => null, 1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
      AssertionExtensions.Should(() => Array.Empty<object>().Fill(_ => null, 0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
      AssertionExtensions.Should(() => Array.Empty<object>().Fill(_ => null, 0, 1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");

      static void Validate<T>(IList<T> list)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IListExtensions.Fill(null, _ => new object())).ThrowExactly<ArgumentNullException>().WithParameterName("list");
      AssertionExtensions.Should(() => Array.Empty<object>().Fill((Func<int, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("filler");

      static void Validate<T>(IList<T> list)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IListExtensions.Swap{T}(IList{T}, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Swap_Method()
  {
    AssertionExtensions.Should(() => IListExtensions.Swap<object>(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("list");
    AssertionExtensions.Should(() => Array.Empty<object>().Swap(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("firstIndex");
    AssertionExtensions.Should(() => Array.Empty<object>().Swap(1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("firstIndex");
    AssertionExtensions.Should(() => Array.Empty<object>().Swap(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("secondIndex");
    AssertionExtensions.Should(() => Array.Empty<object>().Swap(0, 1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("secondIndex");

    throw new NotImplementedException();

    return;

    static void Validate<T>(IList<T> list)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IListExtensions.Randomize{T}(IList{T}, Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Randomize_Method()
  {
    AssertionExtensions.Should(() => IListExtensions.Randomize<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("list");

    var collection = new List<object>();
    collection.Randomize().Should().BeOfType<IList<object>>().And.BeSameAs(collection).And.BeEmpty();

    collection = [string.Empty];
    collection.Randomize().Should().BeOfType<IList<object>>().And.BeSameAs(collection).And.Equal(string.Empty);

    var sequence = new object[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };
    collection = [..sequence];
    collection.Randomize().Should().BeOfType<IList<object>>().And.BeSameAs(collection).And.Contain(sequence);

    return;

    static void Validate<T>(IList<T> list)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IListExtensions.AsReadOnly{T}(IList{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnly_Method()
  {
    AssertionExtensions.Should(() => IListExtensions.AsReadOnly<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("list");

    /*var list = new List<object> { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };
    var readOnly = CollectionsExtensions.AsReadOnly(list);

    readOnly.Should().NotBeNull().And.Equal(list);
    readOnly.IsReadOnly.Should().BeTrue();
    readOnly.GetType().Should().Implement<IReadOnlyList<object>>();

    AssertionExtensions.Should(readOnly.Clear).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Add(new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Insert(0, new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.Remove(new object())).ThrowExactly<NotSupportedException>();
    AssertionExtensions.Should(() => readOnly.RemoveAt(0)).ThrowExactly<NotSupportedException>();*/

    throw new NotImplementedException();

    return;

    static void Validate<T>(IList<T> list)
    {
    }
  }
}