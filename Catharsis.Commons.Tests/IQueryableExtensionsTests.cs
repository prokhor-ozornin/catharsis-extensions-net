using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IQueryableExtensions"/>.</para>
  /// </summary>
  public sealed class IQueryableExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="IQueryableExtensions.Random{T}(IQueryable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Random_Method()
    {
      Assert.Null(Enumerable.Empty<object>().AsQueryable().Random());

      var element = new object();
      Assert.True(ReferenceEquals(new[] { element }.AsQueryable().Random(), element));

      var elements = new[] { "first", "second" }.AsQueryable();
      Assert.True(elements.Contains(elements.Random()));
    }
  }
}