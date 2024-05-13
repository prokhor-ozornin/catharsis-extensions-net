using System.Collections.Specialized;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="NameValueCollectionExtensions"/>.</para>
/// </summary>
public sealed class NameValueCollectionExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="NameValueCollectionExtensions.Clone(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NameValueCollectionExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");

      Validate([]);
      Validate(new NameValueCollection().With(("id", Guid.NewGuid())));
    }

    return;

    static void Validate(NameValueCollection original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<NameValueCollection>().And.NotBeSameAs(original);
      clone.AllKeys.Should().Equal(original.AllKeys);
      clone.Count.Should().Be(original.Count);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NameValueCollectionExtensions.With(NameValueCollection, IEnumerable{ValueTuple{string, object}})"/></description></item>
  ///     <item><description><see cref="NameValueCollectionExtensions.With(NameValueCollection, ValueTuple{string, object}[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NameValueCollectionExtensions.With(null, Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
      AssertionExtensions.Should(() => new NameValueCollection().With((IEnumerable<(string Name, object Value)>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      static void Validate(NameValueCollection collection)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NameValueCollectionExtensions.With(null, Array.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
      AssertionExtensions.Should(() => new NameValueCollection().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      static void Validate(NameValueCollection collection)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NameValueCollectionExtensions.Without(NameValueCollection, IEnumerable{string})"/></description></item>
  ///     <item><description><see cref="NameValueCollectionExtensions.Without(NameValueCollection, string[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NameValueCollectionExtensions.Without(null, Enumerable.Empty<string>())).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
      AssertionExtensions.Should(() => new NameValueCollection().Without((IEnumerable<string>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      static void Validate(NameValueCollection collection, IEnumerable<string> elements)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NameValueCollectionExtensions.Without(null, Array.Empty<string>())).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
      AssertionExtensions.Should(() => new NameValueCollection().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      static void Validate(NameValueCollection collection, params string[] elements)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NameValueCollectionExtensions.Empty(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NameValueCollectionExtensions.Empty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");

      Validate([]);
      Validate(new NameValueCollection().With(Attributes.RandomObjects().Select(element => (element.GetType().FullName, element))));
    }

    return;

    static void Validate(NameValueCollection collection)
    {
      collection.Empty().Should().BeOfType<NameValueCollection>().And.BeSameAs(collection);
      collection.Count.Should().Be(0);
      collection.AllKeys.Should().BeEmpty();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NameValueCollectionExtensions.TryFinallyClear(NameValueCollection, Action{NameValueCollection})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((NameValueCollection) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
      AssertionExtensions.Should(() => new NameValueCollection().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      var collection = new NameValueCollection();
      collection.TryFinallyClear(collection => collection.Add("key", "value")).Should().BeOfType<NameValueCollection>().And.BeSameAs(collection);
      collection.Count.Should().Be(0);
    }

    return;

    static void Validate(NameValueCollection collection)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NameValueCollectionExtensions.ToDictionary(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDictionary_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NameValueCollectionExtensions.ToDictionary(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
    }

    throw new NotImplementedException();

    return;

    static void Validate(NameValueCollection collection)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NameValueCollectionExtensions.ToValueTuple(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToValueTuple_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => NameValueCollectionExtensions.ToValueTuple(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
    }

    throw new NotImplementedException();

    return;

    static void Validate(NameValueCollection collection)
    {
    }
  }
}