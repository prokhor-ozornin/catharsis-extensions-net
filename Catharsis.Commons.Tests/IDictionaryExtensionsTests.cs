using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IDictionaryExtensions"/>.</para>
  /// </summary>
  public sealed class IDictionaryExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="IDictionaryExtensions.AddNext{TKey,TValue}(IDictionary{TKey, TValue}, TKey, TValue)"/> method.</para>
    /// </summary>
    [Fact]
    public void AddNext_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IDictionaryExtensions.AddNext(null, new object(), new object()));
      Assert.Throws<ArgumentNullException>(() =>
      {
        new Dictionary<object, object>().AddNext(null, new object());
      });

      IDictionary<string, string> first = new Dictionary<string, string>();
      var second = first.AddNext("key", "value");
      Assert.Equal(1, first.Count);
      Assert.Equal("key", first.Single().Key);
      Assert.Equal("value", first.Single().Value);
      Assert.True(ReferenceEquals(first, second));
    }
  }
}