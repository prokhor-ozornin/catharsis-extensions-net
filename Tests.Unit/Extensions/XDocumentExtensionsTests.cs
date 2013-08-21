using System;
using System.Xml.Linq;
using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XDocumentExtensions"/>.</para>
  /// </summary>
  public sealed class XDocumentExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="XDocumentExtensions.Dictionary(XDocument)"/> method.</para>
    /// </summary>
    [Fact]
    public void Dictionary_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XDocumentExtensions.Dictionary(null));

      throw new NotImplementedException();
    }
  }
}