using System.Text;
using Catharsis.Commons;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="BinaryReaderExtensions"/>.</para>
/// </summary>
public sealed class BinaryReaderExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.Clone(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Stream.Null.ToBinaryReader());
      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader original)
    {
      using (original)
      {
        var clone = original.Clone();
        
        using (clone)
        {
          clone.Should().BeOfType<BinaryReader>().And.NotBeSameAs(original).And.NotBe(original);
          clone.BaseStream.Should().BeSameAs(original.BaseStream).And.HavePosition(original.BaseStream.Position);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.IsStart(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStart_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).IsStart()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().ToBinaryReader().IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToBinaryReader());
      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsStart().Should().BeTrue();
        reader.BaseStream.MoveToEnd();
        reader.IsStart().Should().Be(reader.BaseStream.IsEmpty());
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.IsEnd(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEnd_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).IsEnd()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Stream.Null.ToBinaryReader());
      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader());
    }
    
    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsEnd().Should().Be(reader.BaseStream.IsEnd()).And.Be(reader.BaseStream.IsEmpty());
        reader.BaseStream.MoveToEnd();
        reader.IsEnd().Should().Be(reader.BaseStream.IsEnd()).And.BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.IsUnset(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      Validate(true, null);
      Validate(true, Stream.Null.ToBinaryReader());
      Validate(true, Attributes.EmptyStream().ToBinaryReader());
      Validate(false, Attributes.RandomStream().ToBinaryReader());
      Validate(false, Attributes.RandomReadOnlyStream().ToBinaryReader());
      Validate(false, Attributes.RandomReadOnlyForwardStream().ToBinaryReader());
    }

    return;

    static void Validate(bool result, BinaryReader reader)
    {
      using (reader)
      {
        reader.IsUnset().Should().Be(reader is null || reader.IsEmpty()).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.IsEmpty(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(true, Stream.Null.ToBinaryReader());
      Validate(true, Attributes.EmptyStream().ToBinaryReader());
      Validate(false, Attributes.RandomStream().ToBinaryReader());
      Validate(false, Attributes.RandomReadOnlyStream().ToBinaryReader());
      Validate(false, Attributes.RandomReadOnlyForwardStream().ToBinaryReader());
    }
    
    return;

    static void Validate(bool result, BinaryReader reader)
    {
      using (reader)
      {
        reader.IsEmpty().Should().Be(reader.BaseStream.IsEmpty()).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.Empty(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().ToBinaryReader().Empty()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToBinaryReader());
      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.Empty().Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.BaseStream.IsEmpty().Should().BeTrue();
        reader.PeekChar().Should().Be(-1);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.Rewind(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Rewind_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).Rewind()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().ToBinaryReader().Rewind()).ThrowExactly<NotSupportedException>();
      
      Validate(Stream.Null.ToBinaryReader());
      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToEnd();
        reader.Rewind().Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.BaseStream.IsStart().Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.Skip(BinaryReader, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Skip_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.Skip(null, 0)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Stream.Null.ToBinaryReader().Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      Validate(Stream.Null.ToBinaryReader());
      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.Rewind();

        reader.Skip(0).Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.IsStart().Should().BeTrue();

        reader.Skip((int) reader.BaseStream.Length).Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.IsEnd().Should().BeTrue();

        reader.Skip(int.MaxValue).Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.IsEnd().Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.TryFinallyClear(BinaryReader, Action{BinaryReader})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => Stream.Null.ToBinaryReader().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().ToBinaryReader().TryFinallyClear(_ => { })).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToBinaryReader());
      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.TryFinallyClear(reader => reader.BaseStream.WriteByte(byte.MaxValue)).Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.IsEmpty().Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="BinaryReaderExtensions.ToEnumerable(BinaryReader)"/></description></item>
  ///     <item><description><see cref="BinaryReaderExtensions.ToEnumerable(BinaryReader, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate([], Stream.Null.ToBinaryReader());
      Validate([], Attributes.EmptyStream().ToBinaryReader());

      var bytes = Attributes.RandomBytes();
      using (var stream = new MemoryStream(bytes))
      {
        Validate(bytes, stream.ToBinaryReader());
      }

      static void Validate(byte[] result, BinaryReader reader)
      {
        using (reader)
        {
          reader.ToEnumerable().Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.ToEnumerable(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate([], Stream.Null.ToBinaryReader());
      Validate([], Attributes.EmptyStream().ToBinaryReader());

      var bytes = Attributes.RandomBytes();
      using (var stream = new MemoryStream(bytes))
      {
        Validate(bytes, stream.ToBinaryReader());
      }

      static void Validate(byte[] result, BinaryReader reader)
      {
        using (reader)
        {
          AssertionExtensions.Should(() => reader.ToEnumerable(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
          AssertionExtensions.Should(() => reader.ToEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

          var sequence = reader.ToEnumerable(1);
          sequence.Should().BeOfType<IEnumerable<byte[]>>();
          sequence.SelectMany(bytes => bytes).Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="BinaryReaderExtensions.ToAsyncEnumerable(BinaryReader)"/></description></item>
  ///     <item><description><see cref="BinaryReaderExtensions.ToAsyncEnumerable(BinaryReader, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.ToAsyncEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate([], Stream.Null.ToBinaryReader());
      Validate([], Attributes.EmptyStream().ToBinaryReader());

      var bytes = Attributes.RandomBytes();
      using (var stream = new MemoryStream(bytes))
      {
        Validate(bytes, stream.ToBinaryReader());
      }

      static void Validate(byte[] result, BinaryReader reader)
      {
        using (reader)
        {
          var sequence = reader.ToAsyncEnumerable();
          sequence.Should().BeOfType<IAsyncEnumerable<byte>>();
          sequence.ToArray().Should().BeOfType<byte[]>().And.Equal(result);
        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.ToAsyncEnumerable(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate([], Stream.Null.ToBinaryReader());
      Validate([], Attributes.EmptyStream().ToBinaryReader());

      var bytes = Attributes.RandomBytes();
      using (var stream = new MemoryStream(bytes))
      {
        Validate(bytes, stream.ToBinaryReader());
      }

      static void Validate(byte[] result, BinaryReader reader)
      {
        using (reader)
        {
          AssertionExtensions.Should(() => reader.ToAsyncEnumerable(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
          AssertionExtensions.Should(() => reader.ToAsyncEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

          var sequence = reader.ToAsyncEnumerable(1);
          sequence.Should().BeOfType<IAsyncEnumerable<byte[]>>();
          sequence.ToArray().SelectMany(bytes => bytes).Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.ToBytes(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        var bytes = reader.BaseStream.ToBytes().ToArray();
        reader.BaseStream.MoveToStart();

        var sequence = reader.ToBytes();
        sequence.Should().BeOfType<IEnumerable<byte>>();
        sequence.ToArray().Should().BeOfType<byte[]>().And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.ToBytesAsync(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.ToBytesAsync(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        var bytes = reader.BaseStream.ToBytesAsync().ToArray();
        reader.BaseStream.MoveToStart();

        var sequence = reader.ToBytesAsync();
        sequence.Should().BeOfType<IAsyncEnumerable<byte>>();

        sequence.ToArray().Should().BeOfType<byte[]>().And.Equal(bytes);
        sequence.ToArray().Should().BeOfType<byte[]>().And.BeEmpty();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.ToText(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => BinaryReaderExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(string.Empty, Stream.Null.ToBinaryReader());
      Validate(string.Empty, Attributes.EmptyStream().ToBinaryReader());

      var text = Attributes.RandomString();
      Encoding.GetEncodings().Select(encoding => encoding.GetEncoding()).ForEach(encoding =>
      {
        using var stream = new MemoryStream();

        text.WriteTo(stream.ToBinaryWriter(encoding));
        Validate(text, stream.MoveToStart().ToBinaryReader(encoding));
      });
    }

    return;

    static void Validate(string result, BinaryReader reader)
    {
      using (reader)
      {
        reader.ToText().Should().BeOfType<string>().And.Be(result);
      }
    }
  }
}