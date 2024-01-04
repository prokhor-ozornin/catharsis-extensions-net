using System.Text;
using Catharsis.Commons;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TextReaderExtensions"/>.</para>
/// </summary>
public sealed class TextReaderExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.IsEnd(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEnd_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).IsEnd()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().ToStreamReader().IsEnd()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToStreamReader());
      Validate(Attributes.EmptyStream().ToStreamReader());
      Validate(Attributes.RandomStream().ToStreamReader());
      Validate(Attributes.RandomReadOnlyStream().ToStreamReader());
    }

    return;

    static void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsEnd().Should().Be(reader.BaseStream.Length == 0);
        reader.ToBytesAsync().Await();
        reader.IsEnd().Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.Lines(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Method()
  {
    AssertionExtensions.Should(() => TextReaderExtensions.Lines(null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.LinesAsync(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void LinesAsync_Method()
  {
    AssertionExtensions.Should(() => TextReaderExtensions.LinesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.Skip{TReader}(TReader, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Skip_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).Skip(0)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
    AssertionExtensions.Should(() => Stream.Null.ToStreamReader().Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.AsSynchronized(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsSynchronized_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).AsSynchronized()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    using (var reader = Attributes.EmptyTextReader())
    {
      using (var synchronized = reader.AsSynchronized())
      {
        synchronized.Should().NotBeNull().And.NotBeSameAs(reader).And.NotBeSameAs(reader.AsSynchronized());
        synchronized.Peek().Should().Be(reader.Peek());
      }
    }

    var value = Attributes.RandomString();
    using (var reader = value.ToStringReader())
    {
      using (var synchronized = reader.AsSynchronized())
      {
        synchronized.Should().NotBeNull().And.NotBeSameAs(reader).And.NotBeSameAs(reader.AsSynchronized());
        synchronized.Peek().Should().Be(reader.Peek());
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToBytes(TextReader, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToBytesAsync(TextReader, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToBytesAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToText(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToTextAsync(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();

    using (var reader = Attributes.EmptyTextReader())
    {
      reader.ToTextAsync().Should().NotBeNull().And.NotBeSameAs(reader.ToTextAsync());
    }

    using (var reader = Attributes.EmptyTextReader())
    {
      reader.ToTextAsync().Await().Should().BeEmpty();
      reader.Read().Should().Be(-1);
    }

    var text = Attributes.RandomString();

    using (var reader = text.ToStringReader())
    {
      reader.ToTextAsync().Await().Should().Be(text);
      reader.Read().Should().Be(-1);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TextReaderExtensions.ToEnumerable(TextReader, bool)"/></description></item>
  ///     <item><description><see cref="TextReaderExtensions.ToEnumerable(TextReader, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).ToEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => Stream.Null.ToStreamReader().ToEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TextReaderExtensions.ToAsyncEnumerable(TextReader, bool)"/></description></item>
  ///     <item><description><see cref="TextReaderExtensions.ToAsyncEnumerable(TextReader, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).ToAsyncEnumerable().ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).ToAsyncEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => Stream.Null.ToStreamReader().ToAsyncEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToXmlReader(TextReader, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
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
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToXmlDictionaryReader(TextReader, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToXmlDocument(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
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
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToXDocument(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.ToXDocumentAsync(TextReader, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("reader").Await();
    AssertionExtensions.Should(() => Stream.Null.ToStreamReader().ToXDocumentAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

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
  ///   <para>Performs testing of <see cref="TextReaderExtensions.DeserializeAsDataContract{T}(TextReader, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextReaderExtensions.DeserializeAsXml{T}(TextReader, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
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
}