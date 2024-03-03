using System.Text;
using System.Xml;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="XmlDocumentExtensions"/>.</para>
/// </summary>
public sealed class XmlDocumentExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.IsEmpty(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    var xml = new XmlDocument();
    xml.IsEmpty().Should().BeTrue();

    xml.RemoveAll();
    xml.AppendChild(xml.CreateComment(null));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveAll();
    xml.AppendChild(xml.CreateDocumentType("name", null, null, null));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveAll();
    xml.AppendChild(xml.CreateElement("element"));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveAll();
    xml.AppendChild(xml.CreateProcessingInstruction("target", "data"));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveAll();
    xml.AppendChild(xml.CreateSignificantWhitespace(null));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveAll();
    xml.AppendChild(xml.CreateWhitespace(null));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveAll();
    xml.AppendChild(xml.CreateXmlDeclaration("1.0", null, null));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveAll();
    xml.AppendChild(xml.CreateDocumentFragment());
    xml.IsEmpty().Should().BeTrue();

    xml.RemoveAll();
    xml.IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Empty(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

      Validate(new XmlDocument());

      var xml = new XmlDocument();
      xml.AppendChild(xml.CreateElement("root"));
      Validate(xml);
    }

    return;

    static void Validate(XmlDocument xml)
    {
      xml.Empty().Should().NotBeNull().And.BeSameAs(xml);
      xml.HasChildNodes.Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.TryFinallyClear(XmlDocument, Action{XmlDocument})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    var document = new XmlDocument();
    document.TryFinallyClear(xml => xml.AppendChild(xml.CreateElement("root"))).Should().NotBeNull().And.BeSameAs(document);
    document.ChildNodes.Count.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.ToEnumerable(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.ToBytes(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.ToText(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().Serialize((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_TextWriter_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().Serialize((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_Stream_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().Serialize((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Attributes.RandomFakeFile())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().Serialize((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }
}