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
      //AssertionExtensions.Should(() => ((StreamReader) null!).IsStart()).ThrowExactly<ArgumentNullException>();
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
        reader.Bytes().Await();
        reader.IsEnd().Should().BeTrue();
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((TextReader) null!).IsEnd()).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => RandomReadOnlyForwardStream.ToStreamReader().IsEnd()).ThrowExactly<NotSupportedException>();

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
      //AssertionExtensions.Should(() => ((StreamReader) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
      //AssertionExtensions.Should(() => ((StreamWriter) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();
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
    //AssertionExtensions.Should(() => ((StringBuilder) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
      //AssertionExtensions.Should(() => ((StreamReader) null!).Empty()).ThrowExactly<ArgumentNullException>();

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
      //AssertionExtensions.Should(() => ((StreamWriter) null!).Empty()).ThrowExactly<ArgumentNullException>();

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
      //AssertionExtensions.Should(() => ((StringBuilder) null!).Empty()).ThrowExactly<ArgumentNullException>();

      Validate(new StringBuilder());
      Validate(RandomString.ToStringBuilder());
    }
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
        reader.Bytes().Await();
        reader.Rewind().Should().NotBeNull().And.BeSameAs(reader);
        reader.BaseStream.Should().HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((StreamReader) null!).Rewind()).ThrowExactly<ArgumentNullException>();

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
        writer.Bytes(RandomBytes).Await();
        writer.Flush();
        writer.Rewind().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((StreamWriter) null!).Rewind()).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToStreamWriter());
      Validate(RandomStream.ToStreamWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Min(StringBuilder, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_Min_Method()
  {
    //AssertionExtensions.Should(() => TextExtensions.Min(null!, new StringBuilder())).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => new StringBuilder().Min(null!)).ThrowExactly<ArgumentNullException>();

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
    //AssertionExtensions.Should(() => TextExtensions.Max(null!, new StringBuilder())).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => new StringBuilder().Max(null!)).ThrowExactly<ArgumentNullException>();

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
  ///   <para>Performs testing of <see cref="TextExtensions.Synchronized(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_Synchronized_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null!).Synchronized()).ThrowExactly<ArgumentNullException>();

    using (var reader = EmptyTextReader)
    {
      using (var synchronized = reader.Synchronized())
      {
        synchronized.Should().NotBeNull().And.NotBeSameAs(reader).And.NotBeSameAs(reader.Synchronized());
        synchronized.Peek().Should().Be(reader.Peek());
      }
    }

    var value = RandomString;
    using (var reader = value.ToStringReader())
    {
      using (var synchronized = reader.Synchronized())
      {
        synchronized.Should().NotBeNull().And.NotBeSameAs(reader).And.NotBeSameAs(reader.Synchronized());
        synchronized.Peek().Should().Be(reader.Peek());
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Synchronized(TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_Synchronized_Method()
  {
    AssertionExtensions.Should(() => ((TextWriter) null!).Synchronized()).ThrowExactly<ArgumentNullException>();

    using var writer = new StringWriter();

    var value = RandomString;
    
    var synchronized = writer.Synchronized();

    synchronized.Should().NotBeNull().And.NotBeSameAs(writer).And.NotBeSameAs(writer.Synchronized());

    synchronized.Encoding.Should().Be(writer.Encoding);
    synchronized.FormatProvider.Should().Be(writer.FormatProvider);
    synchronized.NewLine.Should().Be(writer.NewLine);

    synchronized.Write(value);
    writer.ToString().Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Bytes(TextReader, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_Bytes_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null!).Bytes()).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Bytes{TWriter}(TWriter, IEnumerable{byte}, Encoding?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_Bytes_Method()
  {
    AssertionExtensions.Should(() => ((TextWriter) null!).Bytes(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TextExtensions.Bytes(StringBuilder, Encoding?)"/></description></item>
  ///     <item><description><see cref="TextExtensions.Bytes(StringBuilder, IEnumerable{byte}, Encoding?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void StringBuilder_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null!).Bytes()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TextExtensions.Bytes(null!, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new StringBuilder().Bytes((IEnumerable<byte>) null!)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Text(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_Text_Methods()
  {
    //AssertionExtensions.Should(() => ((TextReader) null!).Text()).ThrowExactlyAsync<ArgumentNullException>().Await();

    using (var reader = EmptyTextReader)
    {
      reader.Text().Should().NotBeNull().And.NotBeSameAs(reader.Text());
    }

    using (var reader = EmptyTextReader)
    {
      reader.Text().Await().Should().BeEmpty();
      reader.Read().Should().Be(-1);
    }

    var text = RandomString;

    using (var reader = text.ToStringReader())
    {
      reader.Text().Await().Should().Be(text);
      reader.Read().Should().Be(-1);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Text{TWriter}(TWriter, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_Text_Method()
  {
    AssertionExtensions.Should(() => ((TextWriter) null!).Text(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().Text(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TextExtensions.Text(StringBuilder)"/></description></item>
  ///     <item><description><see cref="TextExtensions.Text(StringBuilder, string)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void StringBuilder_Text_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null!).Text()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TextExtensions.Text(null!, string.Empty)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new StringBuilder().Text(null!)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Lines(TextReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_Lines_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.Lines(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Skip{TReader}(TReader, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextReader_Skip_Method()
  {
    AssertionExtensions.Should(() => ((TextReader) null!).Skip(0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteTo(string, TextWriter, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_TextWriter_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.WriteTo(null!, Stream.Null.ToStreamWriter()));
    AssertionExtensions.Should(() => string.Empty.WriteTo((TextWriter) null!));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.WriteTo(string, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_StringBuilder_Method()
  {
    AssertionExtensions.Should(() => TextExtensions.WriteTo(null!, string.Empty.ToStringBuilder()));
    AssertionExtensions.Should(() => TextExtensions.WriteTo(string.Empty, null!));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.UseTemporarily(StringBuilder, Action{StringBuilder})"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => TextExtensions.UseTemporarily(null!, _ => {})).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => new StringBuilder().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    var builder = new StringBuilder();
    builder.UseTemporarily(builder => builder.Append(RandomString)).Should().NotBeNull().And.BeSameAs(builder);
    builder.Length.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.Print{TWriter}(TWriter, object, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextWriter_Print_Method()
  {
    AssertionExtensions.Should(() => ((TextWriter) null!).Print(new object())).ThrowExactlyAsync<ArgumentNullException>();
    AssertionExtensions.Should(() => Stream.Null.ToStreamWriter().Print(null!)).ThrowExactlyAsync<ArgumentNullException>();

    throw new NotImplementedException();
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
      AssertionExtensions.Should(() => ((TextReader) null!).ToEnumerable()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null!).ToEnumerable(1)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TextExtensions.ToEnumerable(StringBuilder)"/></description></item>
  ///     <item><description><see cref="TextExtensions.ToEnumerable(StringBuilder, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void StringBuilder_ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null!).ToEnumerable()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null!).ToEnumerable(1)).ThrowExactly<ArgumentNullException>();
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
      AssertionExtensions.Should(() => ((TextReader) null!).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TextReader) null!).ToAsyncEnumerable(1)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TextExtensions.ToStringWriter(StringBuilder, IFormatProvider?)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringBuilder_ToStringWriter_Method()
  {
    void Validate(IFormatProvider? format = null)
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
      AssertionExtensions.Should(() => TextExtensions.ToStringWriter(null!)).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => TextExtensions.ToXmlWriter(null!)).ThrowExactly<ArgumentNullException>();

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
}