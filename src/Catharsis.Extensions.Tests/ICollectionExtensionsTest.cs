using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ICollectionExtensions"/>.</para>
/// </summary>
public sealed class ICollectionExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ICollectionExtensions.AddRange{T}(ICollection{T}, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="ICollectionExtensions.AddRange{T}(ICollection{T}, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void AddRange_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ICollectionExtensions.AddRange<object>(null, Enumerable.Empty<object>)).ThrowExactly<ArgumentNullException>().WithParameterName("to");
      AssertionExtensions.Should(() => Array.Empty<object>().AddRange((IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("from");

      var collection = new List<object>();

      ICollectionExtensions.AddRange(collection, Enumerable.Empty<object>()).Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();

      IEnumerable<object> elements = new object[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };

      ICollectionExtensions.AddRange(collection, elements).Should().NotBeNull().And.BeSameAs(collection).And.Equal(elements);
      ICollectionExtensions.AddRange(collection, elements).Should().NotBeNull().And.BeSameAs(collection).And.Equal(elements.Concat(elements));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ICollectionExtensions.AddRange<object>(null, Array.Empty<object>)).ThrowExactly<ArgumentNullException>().WithParameterName("to");
      AssertionExtensions.Should(() => Array.Empty<object>().AddRange(null)).ThrowExactly<ArgumentNullException>().WithParameterName("from");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ICollectionExtensions.RemoveRange{T}(ICollection{T}, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="ICollectionExtensions.RemoveRange{T}(ICollection{T}, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void RemoveRange_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((ICollection<object>) null).RemoveRange(Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("from");
      AssertionExtensions.Should(() => Array.Empty<object>().RemoveRange((IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

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
      AssertionExtensions.Should(() => ((ICollection<object>) null).RemoveRange(Array.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("from");
      AssertionExtensions.Should(() => Array.Empty<object>().RemoveRange(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ICollectionExtensions.Empty{T}(ICollection{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    void Validate<T>(ICollection<T> collection)
    {
      collection.Empty().Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ICollectionExtensions.Empty<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
      AssertionExtensions.Should(() => Array.Empty<object>().Empty()).ThrowExactly<NotSupportedException>();

      Validate(Array.Empty<object>().ToList());
      Validate(RandomObjects.ToList());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ICollectionExtensions.TryFinallyClear{T}(ICollection{T}, Action{ICollection{T}})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((ICollection<object>) null).TryFinallyClear(_ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
    AssertionExtensions.Should(() => Array.Empty<object>().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    AssertionExtensions.Should(() => Array.Empty<object>().TryFinallyClear(_ => { })).ThrowExactly<NotSupportedException>();

    var collection = new List<object>();
    collection.TryFinallyClear(collection => collection.Add(new object())).Should().NotBeNull().And.BeSameAs(collection).And.BeEmpty();
  }
}