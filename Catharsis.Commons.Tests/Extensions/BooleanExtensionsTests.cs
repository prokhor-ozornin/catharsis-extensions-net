using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="BooleanExtensions"/>.</para>
  /// </summary>
  public sealed class BooleanExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="BooleanExtensions.And(bool, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void And_Method()
    {
      Assert.True(true.And(true));
      Assert.False(true.And(false));
      Assert.False(false.And(true));
      Assert.False(false.And(false));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="BooleanExtensions.Not(bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Not_Method()
    {
      Assert.False(true.Not());
      Assert.True(false.Not());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="BooleanExtensions.Or(bool, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Or_Method()
    {
      Assert.True(true.Or(true));
      Assert.True(true.Or(false));
      Assert.True(false.Or(true));
      Assert.False(false.Or(false));
    }
  }
}