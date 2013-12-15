using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Convert"/>.</para>
  /// </summary>
  public sealed class ConvertTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Convert.To()"/> method.</para>
    /// </summary>
    [Fact]
    public void To_Method()
    {
      Assert.True(Convert.To != null);
      Assert.True(ReferenceEquals(Convert.To, Convert.To));
    }
  }
}