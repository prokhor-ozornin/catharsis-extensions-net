using System.Text;
using System.Xml;
using System.Xml.Linq;
using Catharsis.Commons;
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
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="XDocumentExtensions.With(XDocument, IEnumerable{object})"/></description></item>
  ///     <item><description><see cref="XDocumentExtensions.With(XDocument, object[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XDocumentExtensions.With(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => new XDocument().With((IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("nodes");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XDocumentExtensions.With(null, Array.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => new XDocument().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("nodes");

      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Clone(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => XDocumentExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();

    return;

    static void Validate(XDocument original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<XDocument>().And.NotBeSameAs(original).And.NotBe(original);
      clone.ToString().Should().Be(original.ToString());
      clone.Root.Should().Be(original.Root);
      clone.Declaration.Should().Be(original.Declaration);
      clone.DocumentType.Should().Be(original.DocumentType);
      clone.NodeType.Should().Be(original.NodeType);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.IsUnset(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    Validate(true, null);
    Validate(true, new XDocument());
    Validate(false, new XDocument(new XComment("comment")));
    Validate(false, new XDocument(new XProcessingInstruction("target", "data")));
    Validate(false, new XDocument(new XElement("element")));

    return;

    static void Validate(bool result, XDocument document) => document.IsUnset().Should().Be(document is null || document.IsEmpty()).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.IsEmpty(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    Validate(true, new XDocument());
    Validate(false, new XDocument(new XComment("comment")));
    Validate(false, new XDocument(new XProcessingInstruction("target", "data")));
    Validate(false, new XDocument(new XElement("element")));

    return;

    static void Validate(bool result, XDocument document) => document.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Empty(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    Validate(new XDocument());
    Validate(new XDocument(new XElement("root")));

    return;

    static void Validate(XDocument document)
    {
      document.Empty().Should().BeOfType<XDocument>().And.BeSameAs(document);
      document.Nodes().Should().BeEmpty();
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

    Validate(new XDocument(), new XElement("root"));

    return;

    static void Validate(XDocument document, params object[] nodes)
    {
      document.TryFinallyClear(document => document.With(nodes)).Should().BeOfType<XDocument>().And.BeSameAs(document);
      document.Nodes().Should().BeEmpty();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToEnumerable(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();

    return;

    static void Validate(XDocument document)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToXmlReader(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();

    return;

    static void Validate(XDocument document)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToXmlWriter(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();

    return;

    static void Validate(XDocument document)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToBytes(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, XDocument document) => document.ToBytes().Should().BeOfType<byte[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToBytesAsync(XDocument, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToBytesAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("document").Await();
    AssertionExtensions.Should(() => new XDocument().ToBytesAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, XDocument document)
    {
      var task = document.ToBytesAsync();
      task.Should().BeAssignableTo<Task<byte[]>>();
      task.Await().Should().BeOfType<byte[]>().And.Equal(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToText(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();

    return;

    static void Validate(string result, XDocument document) => document.ToText().Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.ToTextAsync(XDocument, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("document").Await();
    AssertionExtensions.Should(() => new XDocument().ToTextAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();

    return;

    static void Validate(string result, XDocument document)
    {
      var task = document.ToTextAsync();
      task.Should().BeAssignableTo<Task<string>>();
      task.Await().Should().BeOfType<string>().And.Be(result);
    }
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

    return;

    static void Validate(XDocument document)
    {
    }
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

    return;

    static void Validate(XDocument document)
    {
    }
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

    return;

    static void Validate(XDocument document, Stream stream, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Serialize(XDocument, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(Attributes.RandomFakeFile())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
    AssertionExtensions.Should(() => new XDocument().Serialize((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();

    return;

    static void Validate(XDocument document, FileInfo file, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XDocumentExtensions.Serialize(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize()).ThrowExactly<ArgumentNullException>().WithParameterName("document");

    throw new NotImplementedException();

    return;

    static void Validate(XDocument document)
    {
    }
  }
}