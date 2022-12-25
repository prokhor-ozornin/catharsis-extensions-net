using System.Text;
using System.Xml;
using System.Xml.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="XmlExtensions"/>.</para>
/// </summary>
public sealed class XmlExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.IsEmpty(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_IsEmpty_Method()
  {
    //AssertionExtensions.Should(() => ((XmlDocument) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
  ///   <para>Performs testing of <see cref="XmlExtensions.IsEmpty(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_IsEmpty_Method()
  {
    //AssertionExtensions.Should(() => ((XDocument) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
  ///   <para>Performs testing of <see cref="XmlExtensions.Empty(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Empty_Method()
  {
    void Validate(XmlDocument xml)
    {
      xml.Empty().Should().NotBeNull().And.BeSameAs(xml);
      xml.HasChildNodes.Should().BeFalse();
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((XmlDocument) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

      Validate(new XmlDocument());

      var xml = new XmlDocument();
      xml.AppendChild(xml.CreateElement("root"));
      Validate(xml);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.Empty(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Empty_Method()
  {
    void Validate(XDocument xml)
    {
      xml.Empty().Should().NotBeNull().And.BeSameAs(xml);
      xml.Nodes().Should().BeEmpty();
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((XDocument) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

      Validate(new XDocument());
      Validate(new XDocument(new XElement("root")));
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="XmlExtensions.Bytes(XmlDocument)"/></description></item>
  ///     <item><description><see cref="XmlExtensions.Bytes(XmlDocument, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void XmlDocument_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null!).Bytes()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null!).Bytes(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new XmlDocument().Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="XmlExtensions.Bytes(XDocument, CancellationToken)"/></description></item>
  ///     <item><description><see cref="XmlExtensions.Bytes(XDocument, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void XDocument_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XDocument) null!).Bytes()).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XDocument) null!).Bytes(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new XDocument().Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="XmlExtensions.Text(XmlDocument)"/></description></item>
  ///     <item><description><see cref="XmlExtensions.Text(XmlDocument, string)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void XmlDocument_Text_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null!).Text()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlDocument) null!).Text(string.Empty)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new XmlDocument().Text(null!)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="XmlExtensions.Text(XDocument, CancellationToken)"/></description></item>
  ///     <item><description><see cref="XmlExtensions.Text(XDocument, string, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void XDocument_Text_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XDocument) null!).Text()).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XDocument) null!).Text(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => new XDocument().Text(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.Text(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_Text_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null!).Text()).ThrowExactlyAsync<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.Text(XmlWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlWriter_Text_Method()
  {
    AssertionExtensions.Should(() => ((XmlWriter) null!).Text(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().Text(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.UseTemporarily(XmlDocument, Action{XmlDocument})"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => ((XmlDocument) null!).UseTemporarily(_ => {})).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => new XmlDocument().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    var xml = new XmlDocument();
    xml.UseTemporarily(xml => xml.AppendChild(xml.CreateElement("root"))).Should().NotBeNull().And.BeSameAs(xml);
    xml.ChildNodes.Count.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.UseTemporarily(XDocument, Action{XDocument})"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => ((XDocument) null!).UseTemporarily(_ => { })).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => new XDocument().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    var xml = new XDocument();
    xml.UseTemporarily(xml => xml.Add(new XElement("root"))).Should().NotBeNull().And.BeSameAs(xml);
    xml.Nodes().Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.Print(XmlWriter, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlWriter_Print_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.Print(null!, new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().Print(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.Skip(XmlReader, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_Skip_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.Skip(null!, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToEnumerable(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null!).ToEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToEnumerable(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null!).ToEnumerable()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToXmlReader_Method()
  {
    /*AssertionExtensions.Should(() => ((TextReader) null!).ToXmlReader()).ThrowExactly<ArgumentNullException>();

    const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

    var textReader = Xml.ToStringReader();
    textReader.ToXmlReader().Use(reader =>
    {
      reader.Settings.CloseInput.Should().BeFalse();
      reader.Settings.IgnoreComments.Should().BeTrue();
      reader.Settings.IgnoreWhitespace.Should().BeTrue();
      reader.ReadStartElement("article");
      return reader.ReadString();
    }).Should().Be("text");
    textReader.Read().Should().Be(-1);

    textReader = Xml.ToStringReader();
    textReader.ToXmlReader().Use(reader =>
    {
      //reader.Settings.CloseInput.Should().BeTrue();
      reader.ReadStartElement("article");
      return reader.ReadString();
    }).Should().Be("text");
    AssertionExtensions.Should(() => textReader.Read()).ThrowExactly<ObjectDisposedException>();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlReader_Method()
  {
    /*AssertionExtensions.Should(() => ((Stream) null!).ToXmlReader()).ThrowExactly<ArgumentNullException>();

    const string Xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";

    using (var stream = new MemoryStream(Xml.Bytes()))
    {
      var reader = stream.ToXmlReader();
      reader.Settings.CloseInput.Should().BeFalse();
      reader.Settings.IgnoreComments.Should().BeTrue();
      reader.Settings.IgnoreWhitespace.Should().BeTrue();
      reader.ReadStartElement("article");
      reader.ReadString().Should().Be("text");
      reader.ReadEndElement();
      reader.Close();
      stream.Bytes().Should().BeEmpty();
      stream.ReadByte().Should().Be(-1);
    }

    using (var stream = new MemoryStream(Xml.Bytes()))
    {
      stream.ToXmlReader(true).Close();
      //True(reader.Settings.CloseInput);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }*/

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).ToXmlReader()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(Uri, Encoding?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null!).ToXmlReader()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((string) null!).ToXmlReader()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null!).ToXmlReader()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlWriter(TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_ToXmlWriter_Method()
  {
    /*AssertionExtensions.Should(() => ((TextWriter) null!).ToXmlWriter()).ThrowExactly<ArgumentNullException>();

    const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

    var textWriter = new StringWriter();
    textWriter.ToXmlWriter().Write(writer =>
    {
      writer.Settings.CloseOutput.Should().BeFalse();
      writer.Settings.Encoding.ToString().Should().Be(Encoding.Unicode.ToString());
      writer.Settings.Indent.Should().BeFalse();
      writer.WriteElementString("article", "text");
    });
    textWriter.ToString().Should().Be(Xml);
    textWriter.Write(string.Empty);
    textWriter.Close();

    textWriter = new StringWriter();
    textWriter.ToXmlWriter(true).Write(writer =>
    {
      writer.Settings.CloseOutput.Should().BeTrue();
      writer.Settings.Encoding.ToString().Should().Be(Encoding.Unicode.ToString());
      writer.Settings.Indent.Should().BeFalse();
    });
    textWriter.Write(string.Empty);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlWriter(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlWriter_Method()
  {
    /*AssertionExtensions.Should(() => ((Stream) null!).ToXmlWriter()).ThrowExactly<ArgumentNullException>();

    var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";
    using (var stream = new MemoryStream())
    {
      using (var writer = stream.ToXmlWriter())
      {
        writer.Settings.CloseOutput.Should().BeFalse();
        writer.Settings.Encoding.ToString().Should().Be(Encoding.UTF8.ToString());
        writer.Settings.Indent.Should().BeFalse();
        writer.WriteElementString("article", "text");
      }
      stream.ToArray().Should().Equal(xml.Bytes(Encoding.UTF8));
      stream.Bytes().Should().BeEmpty();
      stream.ReadByte().Should().Be(-1);

      using (var writer = stream.Rewind().ToXmlWriter(true))
      {
        writer.Settings.CloseOutput.Should().BeTrue();
        writer.Settings.Encoding.ToString().Should().Be(Encoding.UTF8.ToString());
      }
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
    using (var stream = new MemoryStream())
    {
      using (var writer = stream.ToXmlWriter(encoding: Encoding.Unicode))
      {
        writer.Settings.CloseOutput.Should().BeFalse();
        writer.Settings.Encoding.ToString().Should().Be(Encoding.Unicode.ToString());
        writer.WriteElementString("article", "text");
      }
      stream.ToArray().Should().Equal(xml.Bytes(Encoding.Unicode));
      stream.Bytes().Should().BeEmpty();
      stream.ReadByte().Should().Be(-1);

      using (var writer = stream.ToXmlWriter(true, Encoding.Unicode))
      {
        writer.Settings.CloseOutput.Should().BeTrue();
        writer.Settings.Encoding.ToString().Should().Be(Encoding.Unicode.ToString());
      }
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }*/

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlWriter(FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).ToXmlWriter()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlWriter(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null!).ToXmlWriter()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null!).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null!).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(Uri, Encoding?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null!).ToXmlDictionaryReader()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((string) null!).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryWriter(XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlWriter_ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((XmlWriter) null!).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryWriter(TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((TextWriter) null!).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryWriter(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryWriter(FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
}