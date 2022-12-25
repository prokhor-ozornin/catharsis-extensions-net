using System.Text;
using System.Xml;
using System.Xml.Linq;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="SerializationExtensions"/>.</para>
/// </summary>
public sealed class SerializationExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsBinary{T}(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsBinary_Stream_Serialize_Method()
  {
    /*AssertionExtensions.Should(() => SerializationExtensions.AsBinary<object>(null!, Stream.Null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsBinary(null!)).ThrowExactly<ArgumentNullException>();

    var subject = new object();
    var serialized = new object().AsBinary();
    serialized.Should().NotBeNullOrEmpty();
    new object().AsBinary().AsBinary().Should().NotEqual(new object().AsBinary());
    new object().AsBinary().Should().NotEqual(string.Empty.AsBinary());
    subject.AsBinary().Should().Equal(subject.AsBinary());
    Guid.Empty.ToString().AsBinary().Should().Equal(Guid.Empty.ToString().AsBinary());

    using (var stream = new MemoryStream())
    {
      subject.AsBinary(stream).Should().BeSameAs(subject);
      stream.ToArray().Should().Equal(serialized);
      stream.CanWrite.Should().BeTrue();
    }
    using (var stream = new MemoryStream())
    {
      subject.AsBinary(stream, true).Should().BeSameAs(subject);
      stream.ToArray().Should().Equal(serialized);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsBinary{T}(T, FileInfo, long?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsBinary_FileInfo_Serialize_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsBinary<object>(null!, RandomFakeFile)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => SerializationExtensions.AsBinary(new object(), (FileInfo) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsBinary(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsBinary_Enumerable_Serialize_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsBinary(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsBinary{T}(IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_AsBinary_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).AsBinary<object>()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsBinary{T}(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsBinary_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).AsBinary<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsBinary{T}(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_AsBinary_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).AsBinary<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsBinary{T}(Uri, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_AsBinary_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null!).AsBinary<object>()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(T, XmlWriter, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsDataContract_Serialize_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsDataContract<object>(null!, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsDataContract((XmlWriter) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(T, TextWriter, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsDataContract_Serialize_TextWriter_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsDataContract<object>(null!, Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsDataContract((TextWriter) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(T, Stream, Encoding?, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsDataContract_Serialize_Stream_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsDataContract<object>(null!, Stream.Null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsDataContract((Stream) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(T, FileInfo, Encoding?, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsDataContract_Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsDataContract<object>(null!, RandomFakeFile)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsDataContract((FileInfo) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract(object, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsDataContract_Serialize_String_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsDataContract(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(XmlReader, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_AsDataContract_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null!).AsDataContract<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(TextReader, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_AsDataContract_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null!).AsDataContract<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(Stream, Encoding?, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsDataContract_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).AsDataContract<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(FileInfo, Encoding?, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_AsDataContract_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).AsDataContract<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(Uri, IEnumerable{Type}?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_AsDataContract_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null!).AsDataContract<object>()).ThrowExactlyAsync<ArgumentNullException>().Await();
    
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsDataContract{T}(string, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_AsDataContract_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((string) null!).AsDataContract<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(T, XmlWriter, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsXml_Serialize_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsXml<object>(null!, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsXml((XmlWriter) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(T, TextWriter, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsXml_Serialize_TextWriter_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsXml<object>(null!, Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsXml((TextWriter) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(T, Stream, Encoding?, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsXml_Serialize_Stream_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsXml<object>(null!, Stream.Null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsXml((Stream) null!)).ThrowExactly<ArgumentNullException>();
    
    /*var subject = RandomString;

    var xml = subject.AsXml();
    var stringWriter = new StringWriter();
    stringWriter.ToXmlWriter().Write(writer =>
    {
      new XmlSerializer(subject.GetType()).Serialize(writer, subject);
      stringWriter.ToString().Should().Be(xml);
    });
    subject.AsXml((Type[]) null!).Should().Be(xml);
    subject.AsXml(Array.Empty<Type>()).Should().Be(xml);
    subject.AsXml((Type[]) null!).Should().Be(xml);

    using (var stream = new MemoryStream())
    {
      subject.AsXml(stream, Encoding.Unicode).Should().BeSameAs(subject);
      stream.Rewind().Text().Should().Be(xml);
      stream.CanWrite.Should().BeTrue();
    }

    using (var writer = new StringWriter())
    {
      subject.AsXml(writer).Should().BeSameAs(subject);
      writer.ToString().Should().Be(xml);
      writer.WriteLine();
    }

    stringWriter = new StringWriter();
    using (var writer = stringWriter.ToXmlWriter())
    {
      subject.AsXml(writer).Should().BeSameAs(subject);
      stringWriter.ToString().Should().Be(xml);
      stringWriter.WriteLine();
    }*/

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(T, FileInfo, Encoding?, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsXml_Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsXml<object>(null!, RandomFakeFile)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().AsXml((FileInfo) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml(object, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_AsXml_Serialize_String_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.AsXml(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(XmlReader, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_AsXml_Deserialize_Method()
  {
    /*AssertionExtensions.Should(() => ((XmlReader) null!).AsXml<object>()).ThrowExactly<ArgumentNullException>();

    var serialized = RandomString;

    var xml = new StringWriter().Use(writer =>
    {
      new XmlSerializer(serialized.GetType()).Serialize(writer, serialized);

      return writer.ToString();
    });

    var xmlReader = xml.ToStringReader();

    using (var reader = xml.ToXmlReader())
    {
      var deserialized = reader.AsXml(new [] {  serialized.GetType() });
      deserialized.Should().NotBeSameAs(serialized);
      deserialized.Should().Be(serialized);
    }

    xmlReader.ReadToEnd();
    xmlReader.Close();

    xmlReader = xml.ToStringReader();

    using (var reader = xmlReader.ToXmlReader())
    {
      var deserialized = reader.AsXml(new { serialized.GetType() });
      deserialized.Should().NotBeSameAs(serialized);
      deserialized.Should().Be(serialized);
      reader.Read().Should().BeFalse();
      xmlReader.Read().Should().Be(-1);
    }

    xml.ToStringReader().ToXmlReader().AsXml<string>().Should().Be(serialized);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(TextReader, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_AsXml_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null!).AsXml<object>()).ThrowExactly<ArgumentNullException>();

    /*var subject = Guid.Empty;

    using (var reader = subject.AsXml().ToStringReader())
    {
      reader.AsXml<Guid>().Should().Be(subject);
      reader.ReadLine();
    }

    using (var reader = subject.AsXml().ToStringReader())
    {
      reader.AsXml<Guid>().Should().Be(subject);
      AssertionExtensions.Should(() => reader.ReadLine()).ThrowExactly<ObjectDisposedException>();
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(Stream, Encoding?, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsXml_Deserialize_Method()
  {
    /*AssertionExtensions.Should(() => ((Stream) null!).AsXml<object>()).ThrowExactly<ArgumentNullException>();

    var subject = Guid.Empty;

    using (var stream = new MemoryStream())
    {
      subject.AsXml(stream, Encoding.Unicode);
      stream.Rewind().AsXml<Guid>().Should().Be(subject);
      stream.CanWrite.Should().BeTrue();
    }

    using (var stream = new MemoryStream())
    {
      subject.AsXml(stream, Encoding.Unicode);
      stream.Rewind().AsXml<Guid>(true).Should().Be(subject);
      stream.CanWrite.Should().BeFalse();
    }*/

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(FileInfo, Encoding?, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_AsXml_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).AsXml<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(Uri, IEnumerable{Type}?, Encoding?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_AsXml_Deserialize_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null!).AsXml<object>()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXml{T}(string, IEnumerable{Type}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_AsXml_Deserialize_Method()
  {
    /*AssertionExtensions.Should(() => ((string) null!).AsXml<object>()).ThrowExactly<ArgumentNullException>();

    var subject = Guid.Empty;
    subject.AsXml().AsXml<Guid>().Should().Be(subject);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null!).Serialize(Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new XmlDocument().Serialize((XmlWriter) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_TextWriter_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null!).Serialize(Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new XmlDocument().Serialize((TextWriter) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument, Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_Stream_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null!).Serialize(Stream.Null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new XmlDocument().Serialize((Stream) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument, FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null!).Serialize(RandomFakeFile)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new XmlDocument().Serialize((FileInfo) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_String_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null!).Serialize()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXmlDocument(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_AsXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null!).AsXmlDocument()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXmlDocument(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_AsXmlDocument_Method()
  {
    /*AssertionExtensions.Should(() => ((TextReader) null!).AsXmlDocument()).ThrowExactly<ArgumentNullException>();

    const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

    using (var reader = Xml.ToStringReader())
    {
      reader.AsXmlDocument().Text().Should().Be(Xml);
      reader.Read().Should().Be(-1);
    }

    using (var reader = Xml.ToStringReader())
    {
      reader.AsXmlDocument().Text().Should().Be(Xml);
      AssertionExtensions.Should(() => reader.Read()).ThrowExactly<ObjectDisposedException>();
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXmlDocument(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).AsXmlDocument()).ThrowExactly<ArgumentNullException>();

    const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

    using (var stream = new MemoryStream(Xml.Bytes(Encoding.UTF32)))
    {
      AssertionExtensions.Should(() => stream.AsXmlDocument()).ThrowExactly<XmlException>();
    }

    using (var stream = new MemoryStream(Xml.Bytes(Encoding.Unicode)))
    {
      stream.AsXmlDocument().Text().Should().Be(Xml);
      stream.ToArray().Should().BeEmpty();
      stream.ReadByte().Should().Be(-1);
    }

    using (var stream = new MemoryStream(Xml.Bytes(Encoding.Unicode)))
    {
      stream.AsXmlDocument().Text().Should().Be(Xml);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXmlDocument(FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_AsXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).AsXmlDocument()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXmlDocument(Uri, Encoding?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_AsXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null!).AsXmlDocument()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXmlDocument(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_AsXmlDocument_Method()
  {
    /*AssertionExtensions.Should(() => ((string) null!).AsXmlDocument()).ThrowExactly<ArgumentNullException>();

    new XmlDocument().Text().Should().BeEmpty();

    var document = new XmlDocument();
    var element = document.CreateElement("article");
    element.SetAttribute("id", "1");
    element.InnerText = "Text";
    document.AppendChild(element);
    document.Text().Should().Be("<?xml version=\"1.0\" encoding=\"utf-16\"?><article id=\"1\">Text</article>");*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null!).Serialize(Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new XDocument().Serialize((XmlWriter) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_TextWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null!).Serialize(Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new XDocument().Serialize((TextWriter) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument, Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_Stream_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null!).Serialize(Stream.Null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new XDocument().Serialize((Stream) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument, FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null!).Serialize(RandomFakeFile)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new XDocument().Serialize((FileInfo) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_String_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null!).Serialize()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXDocument(XmlReader, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_AsXDocument_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null!).AsXDocument()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXDocument(TextReader, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_AsXDocument_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null!).AsXDocument()).ThrowExactlyAsync<ArgumentNullException>().Await();

    const string Xml = "<?xml version=\"1.0\"?><article>text</article>";

    using (var reader = Xml.ToStringReader())
    {
      reader.AsXDocument().ToString().Should().Be("<article>text</article>");
      reader.Read().Should().Be(-1);
    }

    using (var reader = Xml.ToStringReader())
    {
      reader.AsXDocument().ToString().Should().Be("<article>text</article>");
      reader.Read().Should().Be(-1);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXDocument(Stream, Encoding?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsXDocument_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).AsXDocument()).ThrowExactlyAsync<ArgumentNullException>().Await();

    const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

    using (var stream = new MemoryStream(Xml.Bytes(Encoding.UTF32)))
    {
      AssertionExtensions.Should(() => stream.AsXDocument()).ThrowExactlyAsync<XmlException>().Await();
    }

    using (var stream = new MemoryStream(Xml.Bytes(Encoding.Unicode)))
    {
      stream.AsXDocument().ToString().Should().Be("<article>text</article>");
      stream.ToArray().Should().BeEmpty();
      stream.ReadByte().Should().Be(-1);
    }

    using (var stream = new MemoryStream(Xml.Bytes(Encoding.Unicode)))
    {
      stream.AsXDocument().ToString().Should().Be("<article>text</article>");
      stream.ReadByte().Should().Be(-1);
    }

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXDocument(FileInfo, Encoding?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_AsXDocument_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null!).AsXDocument()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXDocument(Uri, Encoding?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_AsXDocument_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null!).AsXDocument()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.AsXDocument(string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_AsXDocument_Method()
  {
    AssertionExtensions.Should(() => ((string) null!).AsXDocument()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }
}