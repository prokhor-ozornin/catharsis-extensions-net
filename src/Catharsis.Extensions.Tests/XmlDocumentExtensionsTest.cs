﻿using System.Text;
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
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.IsUnset(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      Validate(true, null);
      Validate(true, new XmlDocument());
      Validate(true, new XmlDocument().With(document => document.With(document.CreateComment(null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateDocumentType("name", null, null, null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateElement("element"))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateProcessingInstruction("target", "data"))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateSignificantWhitespace(null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateWhitespace(null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateXmlDeclaration("1.0", null, null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateDocumentFragment())));
    }

    return;

    static void Validate(bool result, XmlDocument document) => document.IsUnset().Should().Be(document is null || document.IsEmpty()).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.IsEmpty(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

      Validate(true, new XmlDocument());
      Validate(true, new XmlDocument().With(document => document.With(document.CreateComment(null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateDocumentType("name", null, null, null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateElement("element"))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateProcessingInstruction("target", "data"))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateSignificantWhitespace(null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateWhitespace(null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateXmlDeclaration("1.0", null, null))));
      Validate(true, new XmlDocument().With(document => document.With(document.CreateDocumentFragment())));
    }

    return;

    static void Validate(bool result, XmlDocument document) => document.IsEmpty().Should().Be(result);
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
      Validate(new XmlDocument().With(document => document.With(document.CreateElement("root"))));
    }

    return;

    static void Validate(XmlDocument document)
    {
      document.Empty().Should().BeOfType<XmlDocument>().And.BeSameAs(document);
      document.HasChildNodes.Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Clone(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlDocumentExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => new XmlDocument().Clone()).ThrowExactly<XmlException>();

      throw new NotImplementedException();
    }

    return;

    static void Validate(XmlDocument original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<XmlDocument>().And.NotBeSameAs(original);
      clone.ToString().Should().Be(original.ToString());
      clone.InnerXml.Should().Be(original.InnerXml);
      clone.BaseURI.Should().Be(original.BaseURI);
      clone.IsReadOnly.Should().Be(original.IsReadOnly);
      clone.NamespaceURI.Should().Be(original.NamespaceURI);
      clone.NodeType.Should().Be(original.NodeType);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.TryFinallyClear(XmlDocument, Action{XmlDocument})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
      AssertionExtensions.Should(() => new XmlDocument().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      new XmlDocument().With(document => Validate(document, document.CreateElement("root")));
    }

    return;

    static void Validate(XmlDocument document, params XmlNode[] nodes)
    {
      document.TryFinallyClear(xml => xml.With(nodes)).Should().BeOfType<XmlDocument>().And.BeSameAs(document);
      document.ChildNodes.Count.Should().Be(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="XmlDocumentExtensions.With(XmlDocument, IEnumerable{XmlNode})"/></description></item>
  ///     <item><description><see cref="XmlDocumentExtensions.With(XmlDocument, XmlNode[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlDocumentExtensions.With(null, Enumerable.Empty<XmlNode>())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => new XmlDocument().With((IEnumerable<XmlNode>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("nodes");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlDocumentExtensions.With(null, Array.Empty<XmlNode>())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => new XmlDocument().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("nodes");

      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="XmlDocumentExtensions.Without(XmlDocument, IEnumerable{XmlNode})"/></description></item>
  ///     <item><description><see cref="XmlDocumentExtensions.Without(XmlDocument, XmlNode[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlDocumentExtensions.Without(null, Enumerable.Empty<XmlNode>())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => new XmlDocument().Without((IEnumerable<XmlNode>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("document");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlDocumentExtensions.Without(null, Array.Empty<XmlNode>())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => new XmlDocument().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("document");

      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
      AssertionExtensions.Should(() => new XmlDocument().Serialize((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlDocument document, XmlWriter writer)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
      AssertionExtensions.Should(() => new XmlDocument().Serialize((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlDocument document, TextWriter writer)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
      AssertionExtensions.Should(() => new XmlDocument().Serialize((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlDocument document, Stream stream, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Attributes.RandomFakeFile())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
      AssertionExtensions.Should(() => new XmlDocument().Serialize((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlDocument document, FileInfo file, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.Serialize(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void Serialize_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).Serialize()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlDocument document)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.ToEnumerable(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlDocument document)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.ToBytes(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, XmlDocument document) => document.ToBytes().Should().BeOfType<byte[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlDocumentExtensions.ToText(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, XmlDocument document) => document.ToText().Should().BeOfType<string>().And.Be(result);
  }
}