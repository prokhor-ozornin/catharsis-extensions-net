using System.Globalization;
using System.Text;
using System.Xml;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TextExtensions"/>.</para>
/// </summary>
public sealed class TextExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.IsStart(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void StreamReader_IsStart_Method()
  {
    void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsStart().Should().BeTrue();
        reader.BaseStream.MoveToEnd();
        reader.IsStart().Should().Be(reader.BaseStream.Length <= 0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).IsStart()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.ToStreamReader().IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToStreamReader());
      Validate(EmptyStream.ToStreamReader());
      Validate(RandomStream.ToStreamReader());
      Validate(RandomReadOnlyStream.ToStreamReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.IsEnd(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_IsEnd_Method()
  {
    void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsEnd().Should().Be(reader.BaseStream.Length <= 0);
        reader.ToBytesAsync().Await();
        reader.IsEnd().Should().BeTrue();
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).IsEnd()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.ToStreamReader().IsEnd()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToStreamReader());
      Validate(EmptyStream.ToStreamReader());
      Validate(RandomStream.ToStreamReader());
      Validate(RandomReadOnlyStream.ToStreamReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.IsEmpty(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void StreamReader_IsEmpty_Method()
  {
    void Validate(StreamReader reader, bool empty)
    {
      using (reader)
      {
        reader.IsEmpty().Should().Be(empty);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToStreamReader(), true);
      Validate(EmptyStream.ToStreamReader(), true);
      Validate(RandomStream.ToStreamReader(), false);
      Validate(RandomReadOnlyStream.ToStreamReader(), false);
      Validate(RandomReadOnlyForwardStream.ToStreamReader(), false);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.IsEmpty(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void StreamWriter_IsEmpty_Method()
  {
    void Validate(StreamWriter writer)
    {
      using (writer)
      {
        writer.Empty().IsEmpty().Should().BeTrue();
        writer.BaseStream.WriteByte(byte.MinValue);
        writer.IsEmpty().Should().BeFalse();
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).IsEmpty()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToStreamWriter().IsEmpty()).ThrowExactly<ArgumentException>();

      Validate(EmptyStream.ToStreamWriter());
      Validate(RandomStream.ToStreamWriter());
      Validate(WriteOnlyStream.ToStreamWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.IsEmpty(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((StringBuilder) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

    var builder = new StringBuilder();
    builder.IsEmpty().Should().BeTrue();

    builder.Append(char.MinValue);
    builder.IsEmpty().Should().BeFalse();

    builder.Clear();
    builder.IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Empty(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void StreamReader_Empty_Method()
  {
    void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.Empty().Should().NotBeNull().And.BeSameAs(reader);
        reader.BaseStream.Should().HaveLength(0).And.HavePosition(0);
        reader.Peek().Should().Be(-1);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).Empty()).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToStreamReader());
      Validate(RandomStream.ToStreamReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Empty(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void StreamWriter_Empty_Method()
  {
    void Validate(StreamWriter writer)
    {
      using (writer)
      {
        writer.Empty().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HaveLength(0).And.HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).Empty()).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToStreamWriter());
      Validate(RandomStream.ToStreamWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Empty(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_Empty_Method()
  {
    void Validate(StringBuilder builder)
    {
      builder.Empty().Should().NotBeNull().And.BeSameAs(builder);
      builder.Length.Should().Be(0);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null).Empty()).ThrowExactly<ArgumentNullException>();

      Validate(new StringBuilder());
      Validate(RandomString.ToStringBuilder());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Min(StringBuilder, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_Min_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.Min(null, new StringBuilder())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new StringBuilder().Min(null)).ThrowExactly<ArgumentNullException>();

    var first = new StringBuilder();
    var second = new StringBuilder();
    first.Min(second).Should().BeSameAs(first);

    first = new StringBuilder();
    second = new StringBuilder(char.MinValue.ToString());
    first.Min(second).Should().BeSameAs(first);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.ToString());
    first.Min(second).Should().BeSameAs(first);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.Repeat(2));
    first.Min(second).Should().BeSameAs(first);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Max(StringBuilder, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_Max_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.Max(null, new StringBuilder())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new StringBuilder().Max(null)).ThrowExactly<ArgumentNullException>();

    var first = new StringBuilder();
    var second = new StringBuilder();
    first.Max(second).Should().BeSameAs(first);

    first = new StringBuilder();
    second = new StringBuilder(char.MinValue.ToString());
    first.Max(second).Should().BeSameAs(second);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.ToString());
    first.Max(second).Should().BeSameAs(first);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.Repeat(2));
    first.Max(second).Should().BeSameAs(second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Rewind(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void StreamReader_Rewind_Method()
  {
    void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.ToBytesAsync().Await();
        reader.Rewind().Should().NotBeNull().And.BeSameAs(reader);
        reader.BaseStream.Should().HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).Rewind()).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToStreamReader());
      Validate(RandomStream.ToStreamReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Rewind(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void StreamWriter_Rewind_Method()
  {
    void Validate(StreamWriter writer)
    {
      using (writer)
      {
        RandomBytes.WriteToAsync(writer).Await();
        writer.Flush();
        writer.Rewind().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).Rewind()).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToStreamWriter());
      Validate(RandomStream.ToStreamWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Lines(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_Lines_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.Lines(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.LinesAsync(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_LinesAsync_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.LinesAsync(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Skip{TReader}(TReader, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_Skip_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).Skip(0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Repeat(char, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Char_Repeat_Method()
  {
    AssertionExtensions.Should(() => char.MinValue.Repeat(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    foreach (var character in new[] { char.MinValue, char.MaxValue })
    {
      character.Repeat(0).Should().NotBeNull().And.BeSameAs(character.Repeat(0)).And.BeEmpty();

      var text = character.Repeat(count);
      text.Should().NotBeNull().And.NotBeSameAs(character.Repeat(count)).And.HaveLength(count);
      text.ToCharArray().Should().AllBeEquivalentTo(character);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Print{T}(T, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.Print<object>(null, Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => TextExtensions.Print(new object(), null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.PrintAsync{T}(T, TextWriter, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_PrintAsync_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.PrintAsync<object>(null, Stream.Null.ToStreamWriter())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => TextExtensions.PrintAsync(new object(), null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.TryFinallyClear(StringBuilder, Action{StringBuilder})"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.TryFinallyClear(null, _ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new StringBuilder().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>();

    var builder = new StringBuilder();
    builder.TryFinallyClear(builder => builder.Append(RandomString)).Should().NotBeNull().And.BeSameAs(builder);
    builder.Length.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.AsSynchronized(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_AsSynchronized_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).AsSynchronized()).ThrowExactly<ArgumentNullException>();

    using (var reader = EmptyTextReader)
    {
      using (var synchronized = reader.AsSynchronized())
      {
        synchronized.Should().NotBeNull().And.NotBeSameAs(reader).And.NotBeSameAs(reader.AsSynchronized());
        synchronized.Peek().Should().Be(reader.Peek());
      }
    }

    var value = RandomString;
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
  ///   <para>Performs testing of <see cref="TextExtensions.AsSynchronized(TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_AsSynchronized_Method()
  {
    AssertionExtensions.Should(() => ((TextWriter) null).AsSynchronized()).ThrowExactly<ArgumentNullException>();

    using var writer = new StringWriter();

    var value = RandomString;

    var synchronized = writer.AsSynchronized();

    synchronized.Should().NotBeNull().And.NotBeSameAs(writer).And.NotBeSameAs(writer.AsSynchronized());

    synchronized.Encoding.Should().Be(writer.Encoding);
    synchronized.FormatProvider.Should().Be(writer.FormatProvider);
    synchronized.NewLine.Should().Be(writer.NewLine);

    synchronized.Write(value);
    writer.ToString().Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.ToBytes(TextReader, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToBytes()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.ToBytesAsync(TextReader, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToBytesAsync()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.ToText(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToText_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToText()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.ToTextAsync(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().Await();

    using (var reader = EmptyTextReader)
    {
      reader.ToTextAsync().Should().NotBeNull().And.NotBeSameAs(reader.ToTextAsync());
    }

    using (var reader = EmptyTextReader)
    {
      reader.ToTextAsync().Await().Should().BeEmpty();
      reader.Read().Should().Be(-1);
    }

    var text = RandomString;

    using (var reader = text.ToStringReader())
    {
      reader.ToTextAsync().Await().Should().Be(text);
      reader.Read().Should().Be(-1);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TextExtensions.ToEnumerable(TextReader)"/></description></item>
  ///     <item><description><see cref="TextExtensions.ToEnumerable(TextReader, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextReader_ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).ToEnumerable()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).ToEnumerable(1)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TextExtensions.ToAsyncEnumerable(TextReader)"/></description></item>
  ///     <item><description><see cref="TextExtensions.ToAsyncEnumerable(TextReader, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextReader_ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null).ToAsyncEnumerable(1)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.ToStringWriter(StringBuilder, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_ToStringWriter_Method()
  {
    void Validate(IFormatProvider format = null)
    {
      var value = RandomString;
      var builder = new StringBuilder();

      using var writer = builder.ToStringWriter(format);

      writer.Should().NotBeNull().And.NotBeSameAs(builder.ToStringWriter());

      writer.FormatProvider.Should().Be(format);
      writer.Write(value);
      builder.ToString().Should().Be(writer.ToString()).And.Be(value);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TextExtensions.ToStringWriter(null)).ThrowExactly<ArgumentNullException>();

      Validate();
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.ToXmlWriter(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.ToXmlWriter(null)).ThrowExactly<ArgumentNullException>();

    var value = RandomName;

    var builder = new StringBuilder();

    using var writer = builder.ToXmlWriter();

    writer.Should().NotBeNull().And.NotBeSameAs(builder.ToXmlWriter());

    writer.Settings.Should().NotBeNull();
    writer.WriteState.Should().Be(WriteState.Start);
    writer.XmlLang.Should().BeNull();
    writer.XmlSpace.Should().Be(XmlSpace.None);

    writer.WriteRaw(value);
    writer.Flush();

    builder.ToString().Should().Be($"<?xml version=\"1.0\" encoding=\"utf-16\"?>{value}");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteBytes{TWriter}(TWriter, IEnumerable{byte}, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.WriteBytes<TextWriter>(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteBytes<TextWriter>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteBytesAsync{TWriter}(TWriter, IEnumerable{byte}, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.WriteBytesAsync<TextWriter>(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteBytesAsync<TextWriter>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteText{TWriter}(TWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_WriteText_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.WriteText<TextWriter>(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteText<TextWriter>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteTextAsync{TWriter}(TWriter, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.WriteTextAsync<TextWriter>(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().WriteTextAsync<TextWriter>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteTo(IEnumerable{byte}, TextWriter, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Stream.Null.ToStreamWriter()));
    AssertionExtensions.Should(() => string.Empty.WriteTo((TextWriter) null));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteToAsync(IEnumerable{byte}, TextWriter, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Stream.Null.ToStreamWriter()));
    AssertionExtensions.Should(() => string.Empty.WriteToAsync((TextWriter) null));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteTo(string, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Stream.Null.ToStreamWriter()));
    AssertionExtensions.Should(() => string.Empty.WriteTo((TextWriter) null));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteToAsync(string, TextWriter, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Stream.Null.ToStreamWriter()));
    AssertionExtensions.Should(() => string.Empty.WriteToAsync((TextWriter) null));

    throw new NotImplementedException();
  }
}