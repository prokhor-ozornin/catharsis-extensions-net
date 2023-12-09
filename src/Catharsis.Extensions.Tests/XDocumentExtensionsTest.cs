using System.Text;
using System.Xml;
using System.Xml.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="XDocumentExtensions"/>.</para>
/// </summary>
public sealed class XDocumentExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Clone(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => XDocumentExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.IsEmpty(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    var xml = new XDocument();
    xml.IsEmpty().Should().BeTrue();

    xml.RemoveNodes();
    xml.Add(new XComment("comment"));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveNodes();
    xml.Add(new XProcessingInstruction("target", "data"));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveNodes();
    xml.Add(new XElement("element"));
    xml.IsEmpty().Should().BeFalse();

    xml.RemoveNodes();
    xml.IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Empty(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    void Validate(XDocument xml)
    {
      xml.Empty().Should().NotBeNull().And.BeSameAs(xml);
      xml.Nodes().Should().BeEmpty();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

      Validate(new XDocument());
      Validate(new XDocument(new XElement("root")));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.TryFinallyClear(XDocument, Action{XDocument})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("document");
    AssertionExtensions.Should(() => new XDocument().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    var xml = new XDocument();
    xml.TryFinallyClear(xml => xml.Add(new XElement("root"))).Should().NotBeNull().And.BeSameAs(xml);
    xml.Nodes().Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToEnumerable(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToXmlReader(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToXmlWriter(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToBytes(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToBytesAsync(XDocument, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToBytesAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("document").Await();
    AssertionExtensions.Should(() => new XDocument().ToBytesAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToText(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToTextAsync(XDocument, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("document").Await();
    AssertionExtensions.Should(() => new XDocument().ToTextAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Serialize(XDocument, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
    AssertionExtensions.Should(() => new XDocument().Serialize((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Serialize(XDocument, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_TextWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
    AssertionExtensions.Should(() => new XDocument().Serialize((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Serialize(XDocument, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_Stream_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("document");
    AssertionExtensions.Should(() => new XDocument().Serialize((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Serialize(XDocument, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("document");
    AssertionExtensions.Should(() => new XDocument().Serialize((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Serialize(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();
  }
}