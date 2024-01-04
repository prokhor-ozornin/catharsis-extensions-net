using System.Text;
using System.Xml;
using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="XmlReaderExtensions"/>.</para>
/// </summary>
public sealed class XmlReaderExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.Skip(XmlReader, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Skip_Method()
  {
    AssertionExtensions.Should(() => XmlReaderExtensions.Skip(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
    AssertionExtensions.Should(() => Stream.Null.ToXmlReader().Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.ToBytes(XmlReader, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.ToBytesAsync(XmlReader, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToBytesAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.ToText(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.ToTextAsync(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.ToXmlDictionaryReader(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.ToXmlDocument(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.ToXDocument(XmlReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.ToXDocumentAsync(XmlReader, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();
    AssertionExtensions.Should(() => Stream.Null.ToXmlReader().ToXDocumentAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.DeserializeAsDataContract{T}(XmlReader, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlReaderExtensions.DeserializeAsXml{T}(XmlReader, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((XmlReader) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    /*var serialized = Attributes.RandomString();

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
}