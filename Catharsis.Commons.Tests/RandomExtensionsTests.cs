using System;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="RandomExtensions"/>.</para>
  /// </summary>
  public sealed class RandomExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="RandomExtensions.Bytes(Random, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => RandomExtensions.Bytes(null, 1));
      Assert.Throws<ArgumentException>(() => new Random().Bytes(-1));
      Assert.Throws<ArgumentException>(() => new Random().Bytes(0));

      const int Count = 100;
      Assert.True(new Random().Bytes(Count).Length == Count);
    }
  }
}