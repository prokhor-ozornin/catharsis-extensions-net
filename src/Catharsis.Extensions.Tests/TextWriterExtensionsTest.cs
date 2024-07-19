using System.Text;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TextWriterExtensions"/>.</para>
/// </summary>
public sealed class TextWriterExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TextWriterExtensions.AsSynchronized(TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsSynchronized_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextWriter) null).AsSynchronized()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

      using var writer = new StringWriter();

      var value = Attributes.RandomString();

      var synchronized = writer.AsSynchronized();

      synchronized.Should().BeOfType<TextWriter>().And.NotBeSameAs(writer);

      synchronized.Encoding.Should().Be(writer.Encoding);
      synchronized.FormatProvider.Should().Be(writer.FormatProvider);
      synchronized.NewLine.Should().Be(writer.NewLine);

      synchronized.Write(value);
      writer.ToString().Should().Be(value);
    }

    throw new NotImplementedException();

    return;

    static void Validate(TextWriter writer)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextWriterExtensions.WriteBytes{TWriter}(TWriter, IEnumerable{byte}, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TextWriterExtensions.WriteBytes<TextWriter>(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteBytes<TextWriter>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Validate(TextWriter writer, byte[] bytes, Encoding encoding = null)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextWriterExtensions.WriteBytesAsync{TWriter}(TWriter, IEnumerable{byte}, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TextWriterExtensions.WriteBytesAsync<TextWriter>(null, [])).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteBytesAsync<TextWriter>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteBytesAsync<TextWriter>([], null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(TextWriter writer, byte[] bytes, Encoding encoding = null)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextWriterExtensions.WriteText{TWriter}(TWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TextWriterExtensions.WriteText<TextWriter>(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteText<TextWriter>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Validate(TextWriter writer, string text)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextWriterExtensions.WriteTextAsync{TWriter}(TWriter, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TextWriterExtensions.WriteTextAsync<TextWriter>(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteTextAsync<TextWriter>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteTextAsync<TextWriter>(string.Empty, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(TextWriter writer, string text)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextWriterExtensions.ToXmlWriter(TextWriter, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    using (new AssertionScope())
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
    }

    throw new NotImplementedException();

    return;

    static void Validate(TextWriter writer)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextWriterExtensions.ToXmlDictionaryWriter(TextWriter, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextWriter) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
    }

    throw new NotImplementedException();

    return;

    static void Validate(TextWriter writer)
    {
      using (writer)
      {

      }
    }
  }
}