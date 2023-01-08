using System.IO;
using System.Runtime.InteropServices;
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
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsBinary{T}(T, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsBinary_Stream_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsBinary<object>(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsBinary(null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    /*var subject = new object();
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
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsBinary{T}(T, FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsBinary_FileInfo_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsBinary<object>(null, RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsBinary(new object(), (FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsBinary(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsBinary_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsBinary(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsBinary{T}(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_DeserializeAsBinary_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).DeserializeAsBinary<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsBinaryAsync{T}(IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_DeserializeAsBinaryAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).DeserializeAsBinaryAsync<object>()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().DeserializeAsBinaryAsync<object>(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsBinary{T}(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DeserializeAsBinary_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).DeserializeAsBinary<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsBinary{T}(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_DeserializeAsBinary_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).DeserializeAsBinary<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsBinary{T}(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_DeserializeAsBinary_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsBinary<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsBinaryAsync{T}(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_DeserializeAsBinaryAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsBinaryAsync<object>()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsDataContract{T}(T, XmlWriter, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsDataContract_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsDataContract<object>(null, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsDataContract((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsDataContract{T}(T, TextWriter, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsDataContract_TextWriter_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsDataContract<object>(null, Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsDataContract((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsDataContract{T}(T, Stream, Encoding, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsDataContract_Stream_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsDataContract<object>(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsDataContract((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsDataContract{T}(T, FileInfo, Encoding, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsDataContract_FileInfo_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsDataContract<object>(null, RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsDataContract((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsDataContract(object, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsDataContract(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsDataContract{T}(XmlReader, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsDataContract{T}(TextReader, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsDataContract{T}(Stream, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsDataContract{T}(FileInfo, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsDataContract{T}(string, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void String_DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((string) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsDataContract{T}(Uri, TimeSpan?, IEnumerable{(string Name, object Value)}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsDataContractAsync{T}(Uri, TimeSpan?, IEnumerable{(string Name, object Value)}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_DeserializeAsDataContractAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsDataContractAsync<object>()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsXml{T}(T, XmlWriter, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsXml_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsXml<object>(null, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsXml((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsXml{T}(T, TextWriter, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsXml_TextWriter_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsXml<object>(null, Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsXml((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsXml{T}(T, Stream, Encoding, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsXml_Stream_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsXml<object>(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsXml((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    
    /*var subject = RandomString;

    var xml = subject.AsXml();
    var stringWriter = new StringWriter();
    stringWriter.ToXmlWriter().Write(writer =>
    {
      new XmlSerializer(subject.GetType()).Serialize(writer, subject);
      stringWriter.ToString().Should().Be(xml);
    });
    subject.AsXml((Type[]) null).Should().Be(xml);
    subject.AsXml(Array.Empty<Type>()).Should().Be(xml);
    subject.AsXml((Type[]) null).Should().Be(xml);

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
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsXml{T}(T, FileInfo, Encoding, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsXml_FileInfo_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsXml<object>(null, RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => new object().SerializeAsXml((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.SerializeAsXml(object, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_SerializeAsXml_Method()
  {
    AssertionExtensions.Should(() => SerializationExtensions.SerializeAsXml(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsXml{T}(XmlReader, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    /*var serialized = RandomString;

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
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsXml{T}(TextReader, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

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
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsXml{T}(Stream, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    /*var subject = Guid.Empty;

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
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsXml{T}(FileInfo, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsXml{T}(string, IEnumerable{Type})"/> method.</para>
  /// </summary>
  [Fact]
  public void String_DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((string) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    /*var subject = Guid.Empty;
    subject.AsXml().AsXml<Guid>().Should().Be(subject);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsXml{T}(Uri, TimeSpan?, IEnumerable{(string Name, object Value)}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.DeserializeAsXmlAsync{T}(Uri, TimeSpan?, IEnumerable{(string Name, object Value)}, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_DeserializeAsXmlAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).DeserializeAsXmlAsync<object>()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().Serialize((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_TextWriter_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().Serialize((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_Stream_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().Serialize((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize(RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XmlDocument().Serialize((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XmlDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlDocument_Serialize_Method()
  {
    AssertionExtensions.Should(() => ((XmlDocument) null).Serialize()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXmlDocument(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXmlDocument(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

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
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXmlDocument(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

    using (var stream = new MemoryStream(Xml.ToBytes(Encoding.UTF32)))
    {
      AssertionExtensions.Should(() => stream.ToXmlDocument()).ThrowExactly<XmlException>();
    }

    using (var stream = new MemoryStream(Xml.ToBytes(Encoding.Unicode)))
    {
      stream.ToXmlDocument().ToText().Should().Be(Xml);
      stream.ToArray().Should().BeEmpty();
      stream.ReadByte().Should().Be(-1);
    }

    using (var stream = new MemoryStream(Xml.ToBytes(Encoding.Unicode)))
    {
      stream.ToXmlDocument().ToText().Should().Be(Xml);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }*/

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXmlDocument(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXmlDocument(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    /*new XmlDocument().Text().Should().BeEmpty();

    var document = new XmlDocument();
    var element = document.CreateElement("article");
    element.SetAttribute("id", "1");
    element.InnerText = "Text";
    document.AppendChild(element);
    document.Text().Should().Be("<?xml version=\"1.0\" encoding=\"utf-16\"?><article id=\"1\">Text</article>");*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXmlDocument(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXmlDocumentAsync(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXmlDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXmlDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XDocument().Serialize((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_TextWriter_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XDocument().Serialize((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_Stream_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XDocument().Serialize((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize(RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("xml");
    AssertionExtensions.Should(() => new XDocument().Serialize((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.Serialize(XDocument)"/> method.</para>
  /// </summary>
  [Fact]
  public void XDocument_Serialize_Method()
  {
    AssertionExtensions.Should(() => ((XDocument) null).Serialize()).ThrowExactly<ArgumentNullException>().WithParameterName("xml");

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocument(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocumentAsync(XmlReader, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void XmlReader_ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();
    AssertionExtensions.Should(() => Stream.Null.ToXmlReader().ToXDocumentAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocument(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocumentAsync(TextReader, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();
    AssertionExtensions.Should(() => Stream.Null.ToStreamReader().ToXDocumentAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    /*const string Xml = "<?xml version=\"1.0\"?><article>text</article>";

    using (var reader = Xml.ToStringReader())
    {
      reader.ToXDocumentAsync().ToString().Should().Be("<article>text</article>");
      reader.Read().Should().Be(-1);
    }

    using (var reader = Xml.ToStringReader())
    {
      reader.ToXDocumentAsync().ToString().Should().Be("<article>text</article>");
      reader.Read().Should().Be(-1);
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocument(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocumentAsync(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
    AssertionExtensions.Should(() => Stream.Null.ToXDocumentAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

    using (var stream = new MemoryStream(Xml.ToBytes(Encoding.UTF32)))
    {
      AssertionExtensions.Should(() => stream.ToXDocumentAsync()).ThrowExactlyAsync<XmlException>().Await();
    }

    using (var stream = new MemoryStream(Xml.ToBytes(Encoding.Unicode)))
    {
      stream.ToXDocumentAsync().ToString().Should().Be("<article>text</article>");
      stream.ToArray().Should().BeEmpty();
      stream.ReadByte().Should().Be(-1);
    }

    using (var stream = new MemoryStream(Xml.ToBytes(Encoding.Unicode)))
    {
      stream.ToXDocumentAsync().ToString().Should().Be("<article>text</article>");
      stream.ReadByte().Should().Be(-1);
    }*/

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocument(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocumentAsync(FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();
    AssertionExtensions.Should(() => RandomFakeFile.ToXDocumentAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocument(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocumentAsync(string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.ToXDocumentAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocument(Uri, TimeSpan?, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SerializationExtensions.ToXDocumentAsync(Uri, TimeSpan?, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Uri_ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((Uri) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => LocalHost.ToXDocumentAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }
}