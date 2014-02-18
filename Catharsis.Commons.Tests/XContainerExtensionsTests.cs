using System;
using System.Xml.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XContainerExtensions"/>.</para>
  /// </summary>
  public sealed class XContainerExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="XContainerExtensions.AddContent{T}(T, object[])"/> method.</para>
    /// </summary>
    [Fact]
    public void AddContent_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XContainerExtensions.AddContent<XContainer>(null));

      var document = new XDocument();
      Assert.True(ReferenceEquals(document.AddContent(new XElement("root")), document));
      Assert.Equal("root", document.Root.Name);
    }
  }
}