using System;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="StringHttpExtensions"/>.</para>
  /// </summary>
  public sealed class StringHttpExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="StringHttpExtensions.UrlDecode(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void UrlDecode_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringHttpExtensions.UrlDecode(null));

      Assert.Equal(string.Empty, string.Empty.UrlDecode());
      Assert.Equal("#value?", "%23value%3F".UrlDecode());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringHttpExtensions.UrlEncode(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void UrlEncode_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringHttpExtensions.UrlEncode(null));

      Assert.Equal(string.Empty, string.Empty.UrlEncode());
      Assert.Equal("%23value%3F", "#value?".UrlEncode());
    }
  }
}