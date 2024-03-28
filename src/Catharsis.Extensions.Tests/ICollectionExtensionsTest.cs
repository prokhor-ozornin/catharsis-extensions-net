using Catharsis.Commons;
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
  ///     <item><description><see cref="ICollectionExtensions.With{T}(ICollection{T}, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="ICollectionExtensions.With{T}(ICollection{T}, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ICollectionExtensions.With<object>(null, Enumerable.Empty<object>)).ThrowExactly<ArgumentNullException>().WithParameterName("to");
      AssertionExtensions.Should(() => Array.Empty<object>().With((IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("from");

      var collection = new List<object>();

      collection.With(Enumerable.Empty<object>()).Should().BeOfType<List<object>>().And.BeSameAs(collection).And.BeEmpty();

      IEnumerable<object> elements = new object[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };

      collection.With(elements).Should().BeOfType<object[]>().And.BeSameAs(collection).And.Equal(elements);
      collection.With(elements).Should().BeOfType<object[]>().And.BeSameAs(collection).And.Equal(elements.Concat(elements));

      static void Validate<T>(ICollection<T> collection, IEnumerable<T> elements)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ICollectionExtensions.With<object>(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("to");
      AssertionExtensions.Should(() => ICollectionExtensions.With(Array.Empty<object>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("from");

      static void Validate<T>(ICollection<T> collection, params T[] elements)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ICollectionExtensions.Without{T}(ICollection{T}, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="ICollectionExtensions.Without{T}(ICollection{T}, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((ICollection<object>) null).Without(Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("from");
      AssertionExtensions.Should(() => Array.Empty<object>().Without((IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      var collection = new List<object>();
      collection.Without(Enumerable.Empty<object>()).Should().BeOfType<List<object>>().And.BeSameAs(collection).And.Equal(collection);

      var elements = new object[] { 1, string.Empty, "2", Guid.NewGuid(), null, 10.5 };

      collection = [..elements];
      collection.Without(Enumerable.Empty<object>()).Should().BeOfType<List<object>>().And.BeSameAs(collection).And.Equal(elements);

      collection = [..elements];
      collection.Without(elements).Should().BeOfType<List<object>>().And.BeSameAs(collection).And.BeEmpty();

      static void Validate<T>(ICollection<T> collection, IEnumerable<T> elements)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((ICollection<object>) null).Without([])).ThrowExactly<ArgumentNullException>().WithParameterName("from");
      AssertionExtensions.Should(() => Array.Empty<object>().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      static void Validate<T>(ICollection<T> collection, params T[] elements)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ICollectionExtensions.Empty{T}(ICollection{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    AssertionExtensions.Should(() => ICollectionExtensions.Empty<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
    AssertionExtensions.Should(() => Array.Empty<object>().Empty()).ThrowExactly<NotSupportedException>();

    Validate(Array.Empty<object>().ToList());
    Validate(Attributes.RandomObjects().ToList());

    return;

    static void Validate<T>(ICollection<T> collection) => collection.Empty().Should().BeSameAs(collection).And.BeEmpty();
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

    Validate(new List<object>(), new object());

    return;

    static void Validate<T>(ICollection<T> collection, params T[] elements) => collection.TryFinallyClear(collection => collection.With(elements)).Should().BeSameAs(collection).And.BeEmpty();
  }
}