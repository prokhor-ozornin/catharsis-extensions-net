using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IReadOnlyDictionaryExtensions"/>.</para>
/// </summary>
public sealed class IReadOnlyDictionaryExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IDictionaryExtensions.ToValueTuple{TKey, TValue}(IReadOnlyDictionary{TKey, TValue}, IComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToValueTuple_Method()
  {
    AssertionExtensions.Should(() => ((IReadOnlyDictionary<object, object>) null).ToValueTuple()).ThrowExactly<ArgumentNullException>().WithParameterName("dictionary");
    
    throw new NotImplementedException();
  }
}