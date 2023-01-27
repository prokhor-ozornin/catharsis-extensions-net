using System.Text;
using System.Xml;
using System.Xml.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

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
  ///   <para>Performs testing of <see cref="XmlExtensions.IsEmpty(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

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
      AssertionExtensions.Should(() => ((XmlDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

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
      AssertionExtensions.Should(() => ((XDocument) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

      Validate(new XDocument());
      Validate(new XDocument(new XElement("root")));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.Skip(XmlReader, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_Skip_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.Skip(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
    AssertionExtensions.Should(() => Stream.Null.ToXmlReader().Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.Print{T}(T, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.Print<object>(null, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => XmlExtensions.Print(new object(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.PrintAsync{T}(T, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_PrintAsync_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.PrintAsync<object>(null, Stream.Null.ToXmlWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
    AssertionExtensions.Should(() => new object().PrintAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.TryFinallyClear(XmlDocument, Action{XmlDocument})"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    var xml = new XmlDocument();
    xml.TryFinallyClear(xml => xml.AppendChild(xml.CreateElement("root"))).Should().NotBeNull().And.BeSameAs(xml);
    xml.ChildNodes.Count.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.TryFinallyClear(XDocument, Action{XDocument})"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XDocument().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    var xml = new XDocument();
    xml.TryFinallyClear(xml => xml.Add(new XElement("root"))).Should().NotBeNull().And.BeSameAs(xml);
    xml.Nodes().Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToEnumerable(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToEnumerable(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(TextReader, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

    var textReader = Xml.ToStringReader();
    textReader.ToXmlReader().With(reader =>
    {
      reader.Settings.CloseInput.Should().BeFalse();
      reader.Settings.IgnoreComments.Should().BeTrue();
      reader.Settings.IgnoreWhitespace.Should().BeTrue();
      reader.ReadStartElement("article");
      return reader.ReadString();
    }).Should().Be("text");
    textReader.Read().Should().Be(-1);

    textReader = Xml.ToStringReader();
    textReader.ToXmlReader().With(reader =>
    {
      //reader.Settings.CloseInput.Should().BeTrue();
      reader.ReadStartElement("article");
      return reader.ReadString();
    }).Should().Be("text");
    AssertionExtensions.Should(() => textReader.Read()).ThrowExactly<ObjectDisposedException>();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";

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
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReader(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlReaderAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXmlReaderAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlReaderAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlWriter(TextWriter, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((TextWriter) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

    /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

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
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    /*var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";
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
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlWriter(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(TextReader, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(FileInfo, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReader(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryReaderAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXmlDictionaryReaderAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlDictionaryReaderAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryWriter(XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlWriter_ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((XmlWriter) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryWriter(TextWriter, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((TextWriter) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToXmlDictionaryWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToBytes(XmlReader, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToBytesAsync(XmlReader, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToBytesAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToBytes(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToBytes(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToBytesAsync(XDocument, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToBytesAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("xml").Await();
    AssertionExtensions.Should(() => new XDocument().ToBytesAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToText(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToText_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToTextAsync(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToText(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_ToText_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToText(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToText_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.ToTextAsync(XDocument, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("xml").Await();
    AssertionExtensions.Should(() => new XDocument().ToTextAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.WriteBytes(XmlWriter, IEnumerable{byte}, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlWriter_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.WriteBytesAsync(XmlWriter, IEnumerable{byte}, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlWriter_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.WriteText(XmlWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlWriter_WriteText_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.WriteTextAsync(XmlWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlWriter_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.WriteTo(IEnumerable{byte}, XmlWriter, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => XmlExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.WriteToAsync(IEnumerable{byte}, XmlWriter, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Stream.Null.ToXmlWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => XmlExtensions.WriteToAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.WriteTo(string, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.WriteTo(null, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlExtensions.WriteToAsync(string, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => XmlExtensions.WriteToAsync(null, Stream.Null.ToXmlWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();

    throw new NotImplementedException();
  }
}