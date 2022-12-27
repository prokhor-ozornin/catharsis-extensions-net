﻿using System.Text;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="BinaryExtensions"/>.</para>
/// </summary>
public sealed class BinaryExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.IsStart(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_IsStart_Method()
  {
    void Validate(BinaryReader reader)
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
      //AssertionExtensions.Should(() => ((BinaryReader) null!).IsStart()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.ToBinaryReader().IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToBinaryReader());
      Validate(EmptyStream.ToBinaryReader());
      Validate(RandomStream.ToBinaryReader());
      Validate(RandomReadOnlyStream.ToBinaryReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.IsStart(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_IsStart_Method()
  {
    void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        writer.BaseStream.MoveToStart();
        writer.IsStart().Should().BeTrue();
        writer.BaseStream.MoveToEnd();
        writer.IsStart().Should().Be(writer.BaseStream.Length <= 0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryWriter) null!).IsStart()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToBinaryWriter());
      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
      Validate(WriteOnlyStream.ToBinaryWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.IsEnd(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_IsEnd_Method()
  {
    void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsEnd().Should().Be(reader.BaseStream.Length <= 0);
        reader.BaseStream.MoveToEnd();
        reader.IsEnd().Should().BeTrue();
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryReader) null!).IsEnd()).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToBinaryReader());
      Validate(EmptyStream.ToBinaryReader());
      Validate(RandomStream.ToBinaryReader());
      Validate(RandomReadOnlyStream.ToBinaryReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.IsEnd(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_IsEnd_Method()
  {
    void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        writer.BaseStream.MoveToStart();
        writer.IsEnd().Should().Be(writer.BaseStream.Length <= 0);
        writer.BaseStream.MoveToEnd();
        writer.IsEnd().Should().BeTrue();
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryWriter) null!).IsEnd()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().IsEnd()).ThrowExactly<ArgumentException>();

      Validate(Stream.Null.ToBinaryWriter());
      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
      Validate(WriteOnlyStream.ToBinaryWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.IsEmpty(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_IsEmpty_Method()
  {
    void Validate(BinaryReader reader, bool empty)
    {
      using (reader)
      {
        reader.IsEmpty().Should().Be(empty);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryReader) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToBinaryReader(), true);
      Validate(EmptyStream.ToBinaryReader(), true);
      Validate(RandomStream.ToBinaryReader(), false);
      Validate(RandomReadOnlyStream.ToBinaryReader(), false);
      Validate(RandomReadOnlyForwardStream.ToBinaryReader(), false);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.IsEmpty(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_IsEmpty_Method()
  {
    void Validate(BinaryWriter writer, bool empty)
    {
      using (writer)
      {
        writer.IsEmpty().Should().Be(empty);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryWriter) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().IsEmpty()).ThrowExactly<ArgumentException>();

      Validate(Stream.Null.ToBinaryWriter(), true);
      Validate(EmptyStream.ToBinaryWriter(), true);
      Validate(RandomStream.ToBinaryWriter(), false);
      Validate(WriteOnlyStream.ToBinaryWriter(), true);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Empty(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_Empty_Method()
  {
    void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.Empty().Should().NotBeNull().And.BeSameAs(reader);
        reader.BaseStream.Should().HaveLength(0).And.HavePosition(0);
        reader.PeekChar().Should().Be(-1);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryReader) null!).Empty()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.ToBinaryReader().Empty()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream.ToBinaryReader());
      Validate(RandomStream.ToBinaryReader());
      Validate(RandomReadOnlyStream.ToBinaryReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Empty(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_Empty_Method()
  {
    void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        writer.Empty().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HaveLength(0).And.HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryWriter) null!).Empty()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().Empty()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
      Validate(WriteOnlyStream.ToBinaryWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Rewind(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_Rewind_Method()
  {
    void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToEnd();
        reader.Rewind().Should().NotBeNull().And.BeSameAs(reader);
        reader.BaseStream.Should().HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryReader) null!).Rewind()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.ToBinaryReader().Rewind()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream.ToBinaryReader());
      Validate(RandomStream.ToBinaryReader());
      Validate(RandomReadOnlyStream.ToBinaryReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Rewind(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_Rewind_Method()
  {
    void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        writer.BaseStream.MoveToEnd();
        writer.Rewind().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryWriter) null!).Rewind()).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().Rewind()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
      Validate(WriteOnlyStream.ToBinaryWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Bytes(BinaryReader, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_Bytes_Method()
  {
    void Validate(BinaryReader reader)
    {
      using (reader)
      {
        AssertionExtensions.Should(() => reader.Bytes(Cancellation).ToArray()).ThrowExactlyAsync<TaskCanceledException>().Await();

        var bytes = reader.BaseStream.Bytes().ToArray().Await();
        reader.BaseStream.MoveToStart();

        var sequence = reader.Bytes();
        sequence.Should().NotBeNull().And.NotBeSameAs(reader.Bytes());

        sequence.ToArray().Await().Should().Equal(bytes);
        sequence.ToArray().Await().Should().BeEmpty();
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => BinaryExtensions.Bytes(null!)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyStream.ToBinaryReader());
      Validate(RandomStream.ToBinaryReader());
      Validate(RandomReadOnlyStream.ToBinaryReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Bytes(BinaryWriter, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_Bytes_Method()
  {
    void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        AssertionExtensions.Should(() => writer.Bytes(RandomBytes, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var bytes = RandomBytes;

        writer.BaseStream.MoveToEnd();

        var count = bytes.Length;
        var length = writer.BaseStream.Length;
        var position = writer.BaseStream.Position;

        using var task = writer.Bytes(bytes);

        task.Await().Should().NotBeNull().And.BeSameAs(writer);

        writer.BaseStream.Length.Should().Be(length + count);
        writer.BaseStream.Position.Should().Be(position + count);
        writer.BaseStream.MoveBy(-count).Bytes().ToArray().Await().Should().Equal(bytes);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => BinaryExtensions.Bytes(null!, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      //AssertionExtensions.Should(() => Stream.Null.ToBinaryWriter().Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Text(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_Text_Method()
  {
    void Validate(BinaryReader reader, string value)
    {
      using (reader)
      {
        reader.Text().Should().NotBeNull().And.Be(value);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => BinaryExtensions.Text(null!)).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToBinaryReader(), string.Empty);
      Validate(EmptyStream.ToBinaryReader(), string.Empty);

      var text = RandomString;
      foreach (var encoding in Encoding.GetEncodings().Select(info => info.GetEncoding()))
      {
        using var stream = new MemoryStream();

        stream.ToBinaryWriter(encoding).Text(text);
        Validate(stream.MoveToStart().ToBinaryReader(encoding), text);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Text(BinaryWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_Text_Method()
  {
    void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        var text = RandomString;

        writer.Text(text).Should().NotBeNull().And.BeSameAs(writer);
        
        using var reader = writer.BaseStream.MoveToStart().ToBinaryReader();
        
        reader.Text().Should().Be(text);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => BinaryExtensions.Text(null!, string.Empty)).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => Stream.Null.ToBinaryWriter().Text(null!)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyStream.ToBinaryWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.WriteTo(IEnumerable{byte}, BinaryWriter, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        AssertionExtensions.Should(() => RandomBytes.WriteTo(writer, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var bytes = RandomBytes;

        writer.BaseStream.MoveToEnd();

        var count = bytes.Length;
        var length = writer.BaseStream.Length;
        var position = writer.BaseStream.Position;

        using var task = bytes.WriteTo(writer);

        task.Await().Should().NotBeNull().And.BeSameAs(bytes);

        writer.BaseStream.Length.Should().Be(length + count);
        writer.BaseStream.Position.Should().Be(position + count);
        writer.BaseStream.MoveBy(-count).Bytes().ToArray().Await().Should().Equal(bytes);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).WriteTo(Stream.Null.ToBinaryWriter())).ThrowExactlyAsync<ArgumentNullException>();
      //AssertionExtensions.Should(() => BinaryExtensions.WriteTo(Enumerable.Empty<byte>(), null!)).ThrowExactlyAsync<ArgumentNullException>();

      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.WriteTo(string, BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    void Validate(BinaryWriter writer, string text)
    {
      using (writer)
      {
        text.WriteTo(writer).Should().NotBeNull().And.BeSameAs(text);

        using (var reader = writer.BaseStream.MoveToStart().ToBinaryReader())
        {
          reader.Text().Should().Be(text);
        }
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((string) null!).WriteTo(Stream.Null.ToBinaryWriter())).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => BinaryExtensions.WriteTo(string.Empty, null!)).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToBinaryWriter(), string.Empty);
      Validate(EmptyStream.ToBinaryWriter(), RandomString);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Print(BinaryWriter, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_Print_Method()
  {
    void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        var instance = RandomString;

        writer.Print(instance).Should().NotBeNull().And.BeSameAs(writer);

        using var reader = writer.BaseStream.MoveToStart().ToBinaryReader();

        reader.Text().Should().Be(instance.ToStringState());
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => BinaryExtensions.Print(null!, new object())).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => Stream.Null.ToBinaryWriter().Print(null!)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyStream.ToBinaryWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.UseTemporarily(BinaryReader, Action{BinaryReader})"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_UseTemporarily_Method()
  {
    void Validate(Stream stream)
    {
      using (stream)
      {
        using (var reader = stream.ToBinaryReader())
        {
          reader.UseTemporarily(_ => {}).Should().NotBeNull().And.BeSameAs(reader);
          reader.BaseStream.Should().HavePosition(0).And.HaveLength(0);
        }
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryReader) null!).UseTemporarily(_ => {})).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => Stream.Null.ToBinaryReader().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.ToBinaryReader().UseTemporarily(_ => { })).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null);
      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.UseTemporarily(BinaryWriter, Action{BinaryWriter})"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryWriter_UseTemporarily_Method()
  {
    void Validate(Stream stream)
    {
      using (stream)
      {
        using (var writer = stream.ToBinaryWriter())
        {
          writer.UseTemporarily(_ => { }).Should().NotBeNull().And.BeSameAs(writer);
          writer.BaseStream.Should().HavePosition(0).And.HaveLength(0);
        }
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((BinaryWriter) null!).UseTemporarily(_ => { })).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => Stream.Null.ToBinaryWriter().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().UseTemporarily(_ => { })).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null);
      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(WriteOnlyStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryExtensions.Skip(BinaryReader, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryReader_Skip_Method()
  {
    void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.Skip(int.MinValue).Should().NotBeNull().And.BeSameAs(reader);
        reader.IsStart().Should().BeTrue();

        reader.Skip(0).Should().NotBeNull().And.BeSameAs(reader);
        reader.IsStart().Should().BeTrue();

        reader.Skip((int) reader.BaseStream.Length).Should().NotBeNull().And.BeSameAs(reader);
        reader.IsEnd().Should().BeTrue();

        reader.Skip(int.MaxValue).Should().NotBeNull().And.BeSameAs(reader);
        reader.IsEnd().Should().BeTrue();
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => BinaryExtensions.Skip(null!, 0)).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null.ToBinaryReader());
      Validate(EmptyStream.ToBinaryReader());
      Validate(RandomStream.ToBinaryReader());
      Validate(RandomReadOnlyStream.ToBinaryReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="BinaryExtensions.ToEnumerable(BinaryReader)"/></description></item>
  ///     <item><description><see cref="BinaryExtensions.ToEnumerable(BinaryReader, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryReader_ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      void Validate(BinaryReader reader, byte[] bytes)
      {
        using (reader)
        {
          var sequence = reader.ToEnumerable();
          sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToEnumerable());
          sequence.Should().Equal(bytes);
        }
      }

      using (new AssertionScope())
      {
        //AssertionExtensions.Should(() => BinaryExtensions.ToEnumerable(null!)).ThrowExactly<ArgumentNullException>();

        Validate(Stream.Null.ToBinaryReader(), Array.Empty<byte>());
        Validate(EmptyStream.ToBinaryReader(), Array.Empty<byte>());

        var bytes = RandomBytes;
        using (var stream = new MemoryStream(bytes))
        {
          Validate(stream.ToBinaryReader(), bytes);
        }
      }
    }

    using (new AssertionScope())
    {
      void Validate(BinaryReader reader, byte[] bytes)
      {
        using (reader)
        {
          AssertionExtensions.Should(() => reader.ToEnumerable(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
          AssertionExtensions.Should(() => reader.ToEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>();

          var sequence = reader.ToEnumerable(1);
          sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToEnumerable(1));
          sequence.SelectMany(bytes => bytes).Should().Equal(bytes);
        }
      }

      using (new AssertionScope())
      {
        //AssertionExtensions.Should(() => BinaryExtensions.ToEnumerable(null!, 1)).ThrowExactly<ArgumentNullException>();

        Validate(Stream.Null.ToBinaryReader(), Array.Empty<byte>());
        Validate(EmptyStream.ToBinaryReader(), Array.Empty<byte>());

        var bytes = RandomBytes;
        using (var stream = new MemoryStream(bytes))
        {
          Validate(stream.ToBinaryReader(), bytes);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="BinaryExtensions.ToAsyncEnumerable(BinaryReader)"/></description></item>
  ///     <item><description><see cref="BinaryExtensions.ToAsyncEnumerable(BinaryReader, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryReader_ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      void Validate(BinaryReader reader, byte[] bytes)
      {
        using (reader)
        {
          var sequence = reader.ToAsyncEnumerable();
          sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToAsyncEnumerable());
          sequence.ToArray().Await().Should().Equal(bytes);
        }
      }

      using (new AssertionScope())
      {
        //AssertionExtensions.Should(() => BinaryExtensions.ToAsyncEnumerable(null!)).ThrowExactly<ArgumentNullException>();

        Validate(Stream.Null.ToBinaryReader(), Array.Empty<byte>());
        Validate(EmptyStream.ToBinaryReader(), Array.Empty<byte>());

        var bytes = RandomBytes;
        using (var stream = new MemoryStream(bytes))
        {
          Validate(stream.ToBinaryReader(), bytes);
        }
      }
    }

    using (new AssertionScope())
    {
      void Validate(BinaryReader reader, byte[] bytes)
      {
        using (reader)
        {
          AssertionExtensions.Should(() => reader.ToAsyncEnumerable(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
          AssertionExtensions.Should(() => reader.ToAsyncEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>();

          var sequence = reader.ToAsyncEnumerable(5);
          sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToAsyncEnumerable(5));
          sequence.ToArray().Await().SelectMany(bytes => bytes).Should().Equal(bytes);
        }
      }

      using (new AssertionScope())
      {
        //AssertionExtensions.Should(() => BinaryExtensions.ToAsyncEnumerable(null!, 1)).ThrowExactly<ArgumentNullException>();

        Validate(Stream.Null.ToBinaryReader(), Array.Empty<byte>());
        Validate(EmptyStream.ToBinaryReader(), Array.Empty<byte>());

        var bytes = RandomBytes;
        using (var stream = new MemoryStream(bytes))
        {
          Validate(stream.ToBinaryReader(), bytes);
        }
      }
    }
  }
}