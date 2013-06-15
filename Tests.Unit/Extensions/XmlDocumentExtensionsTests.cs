using System;
using System.Xml;
using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="XmlDocumentExtensions"/>.</para>
  /// </summary>
  public sealed class XmlDocumentExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="XmlDocumentExtensions.String(XmlDocument)"/> method.</para>
    /// </summary>
    [Fact]
    public void String_Method()
    {
      Assert.Throws<ArgumentNullException>(() => XmlDocumentExtensions.String(null));

      Assert.True(new XmlDocument().String() == "");
      
      var document = new XmlDocument();
      var element = document.CreateElement("article");
      element.SetAttribute("id", "1");
      element.InnerText = "Text";
      document.AppendChild(element);
      Assert.True(document.String() == "<?xml version=\"1.0\" encoding=\"utf-16\"?><article id=\"1\">Text</article>");
    }
  }
}