using System.Collections.Specialized;
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
    AssertionExtensions.Should(() => NameValueCollectionExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="NameValueCollectionExtensions.AddRange(NameValueCollection, IEnumerable{(string Name, object Value)})"/></description></item>
  ///     <item><description><see cref="NameValueCollectionExtensions.AddRange(NameValueCollection, (string Name, object Value)[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void AddRange_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((NameValueCollection) null).AddRange(Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("to");
      AssertionExtensions.Should(() => new NameValueCollection().AddRange((IEnumerable<(string Name, object Value)>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("from");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((NameValueCollection) null).AddRange([])).ThrowExactly<ArgumentNullException>().WithParameterName("to");
      AssertionExtensions.Should(() => new NameValueCollection().AddRange(null)).ThrowExactly<ArgumentNullException>().WithParameterName("from");

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

      Validate(new NameValueCollection());
      Validate(new NameValueCollection().AddRange(RandomObjects.Select(element => (element.GetType().FullName, element))));
    }

    return;

    static void Validate(NameValueCollection collection)
    {
      collection.Empty().Should().NotBeNull().And.BeSameAs(collection);
      collection.Count.Should().Be(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NameValueCollectionExtensions.TryFinallyClear(NameValueCollection, Action{NameValueCollection})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((NameValueCollection) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("collection");
    AssertionExtensions.Should(() => new NameValueCollection().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    var collection = new NameValueCollection();
    collection.TryFinallyClear(collection => collection.Add("key", "value")).Should().NotBeNull().And.BeSameAs(collection);
    collection.Count.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NameValueCollectionExtensions.ToDictionary(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDictionary_Method()
  {
    AssertionExtensions.Should(() => NameValueCollectionExtensions.ToDictionary(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="NameValueCollectionExtensions.ToValueTuple(NameValueCollection)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToValueTuple_Method()
  {
    AssertionExtensions.Should(() => NameValueCollectionExtensions.ToValueTuple(null)).ThrowExactly<ArgumentNullException>().WithParameterName("collection");

    throw new NotImplementedException();
  }
}