using AutoFixture;
using System.Text;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="BinaryReaderExtensions"/>.</para>
/// </summary>
/// <seealso cref="BinaryReaderExtensions"/>
public sealed class BinaryReaderExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.get_IsUnset(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryReader().IsUnset).ThrowExactly<ArgumentException>().WithParameterName("reader");

      Test(true, null);
      Test(true, System.IO.Stream.Null.ToBinaryReader());
      Test(true, EmptyStream.ToBinaryReader());
      Test(false, Stream.ToBinaryReader());
      Test(false, ReadOnlyStream.ToBinaryReader());
      Test(false, ReadOnlyForwardStream.ToBinaryReader());
    }

    return;

    static void Test(bool result, BinaryReader reader)
    {
      using (reader)
      {
        reader.IsUnset.Should().Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.get_IsEmpty(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).IsEmpty).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryReader().IsEmpty).ThrowExactly<ArgumentException>();

      Test(true, System.IO.Stream.Null.ToBinaryReader());
      Test(true, EmptyStream.ToBinaryReader());
      Test(false, Stream.ToBinaryReader());
      Test(false, ReadOnlyStream.ToBinaryReader());
      Test(false, ReadOnlyForwardStream.ToBinaryReader());
    }

    return;

    static void Test(bool result, BinaryReader reader)
    {
      using (reader)
      {
        reader.IsEmpty.Should().Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.get_Bytes(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Bytes_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.get_Text(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Text_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.get_IsStart(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStart_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).IsStart).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => ReadOnlyForwardStream.ToBinaryReader().IsStart).ThrowExactly<NotSupportedException>();

      Test(System.IO.Stream.Null.ToBinaryReader());
      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
      Test(ReadOnlyStream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsStart.Should().BeTrue();
        reader.BaseStream.MoveToEnd();
        reader.IsStart.Should().Be(reader.BaseStream.IsEmpty);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.get_IsEnd(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEnd_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).IsEnd).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test(System.IO.Stream.Null.ToBinaryReader());
      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
      Test(ReadOnlyStream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsEnd.Should().Be(reader.BaseStream.IsEnd).And.Be(reader.BaseStream.IsEmpty);
        reader.BaseStream.MoveToEnd();
        reader.IsEnd.Should().Be(reader.BaseStream.IsEnd).And.BeTrue();
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
      AssertionExtensions.Should(() => ReadOnlyForwardStream.ToBinaryReader().Rewind()).ThrowExactly<NotSupportedException>();

      Test(System.IO.Stream.Null.ToBinaryReader());
      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
      Test(ReadOnlyStream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToEnd();
        reader.Rewind().Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.BaseStream.IsStart.Should().BeTrue();
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
      AssertionExtensions.Should(() => ((BinaryReader) null).Skip(0)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => System.IO.Stream.Null.ToBinaryReader().Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      Test(System.IO.Stream.Null.ToBinaryReader());
      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
      Test(ReadOnlyStream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader reader)
    {
      using (reader)
      {
        reader.Rewind();

        reader.Skip(0).Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.IsStart.Should().BeTrue();

        reader.Skip((int) reader.BaseStream.Length).Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.IsEnd.Should().BeTrue();

        reader.Skip(int.MaxValue).Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.IsEnd.Should().BeTrue();
      }
    }
  }
  

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.Clone(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).Clone()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test(System.IO.Stream.Null.ToBinaryReader());
      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader original)
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
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.Empty(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => ReadOnlyForwardStream.ToBinaryReader().Empty()).ThrowExactly<NotSupportedException>();

      Test(System.IO.Stream.Null.ToBinaryReader());
      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
      Test(ReadOnlyStream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader reader)
    {
      using (reader)
      {
        reader.Empty().Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.BaseStream.IsEmpty.Should().BeTrue();
        reader.PeekChar().Should().Be(-1);
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
      AssertionExtensions.Should(() => System.IO.Stream.Null.ToBinaryReader().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      AssertionExtensions.Should(() => ReadOnlyForwardStream.ToBinaryReader().TryFinallyClear(_ => { })).ThrowExactly<NotSupportedException>();

      Test(System.IO.Stream.Null.ToBinaryReader());
      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader reader)
    {
      using (reader)
      {
        reader.TryFinallyClear(reader => reader.BaseStream.WriteByte(byte.MaxValue)).Should().BeOfType<BinaryReader>().And.BeSameAs(reader);
        reader.IsEmpty.Should().BeTrue();
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
      AssertionExtensions.Should(() => ((BinaryReader) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test([], System.IO.Stream.Null.ToBinaryReader());
      Test([], EmptyStream.ToBinaryReader());

      var bytes = Bytes;
      using (var stream = new MemoryStream(bytes))
      {
        Test(bytes, stream.ToBinaryReader());
      }

      static void Test(byte[] result, BinaryReader reader)
      {
        using (reader)
        {
          reader.ToEnumerable().Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).ToEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test([], System.IO.Stream.Null.ToBinaryReader());
      Test([], EmptyStream.ToBinaryReader());

      var bytes = Bytes;
      using (var stream = new MemoryStream(bytes))
      {
        Test(bytes, stream.ToBinaryReader());
      }

      static void Test(byte[] result, BinaryReader reader)
      {
        using (reader)
        {
          AssertionExtensions.Should(() => reader.ToEnumerable(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
          AssertionExtensions.Should(() => reader.ToEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

          var enumerable = reader.ToEnumerable(1);
          enumerable.Should().BeOfType<IEnumerable<byte[]>>();
          enumerable.SelectMany(bytes => bytes).Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
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
      AssertionExtensions.Should(() => ((BinaryReader) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test([], System.IO.Stream.Null.ToBinaryReader());
      Test([], EmptyStream.ToBinaryReader());

      var bytes = Bytes;
      using (var stream = new MemoryStream(bytes))
      {
        Test(bytes, stream.ToBinaryReader());
      }

      static void Test(byte[] result, BinaryReader reader)
      {
        using (reader)
        {
          var enumerable = reader.ToAsyncEnumerable();
          enumerable.Should().BeOfType<IAsyncEnumerable<byte>>();
          enumerable.ToArray().Should().BeOfType<byte[]>().And.Equal(result);
        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryReader) null).ToAsyncEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test([], System.IO.Stream.Null.ToBinaryReader());
      Test([], EmptyStream.ToBinaryReader());

      var bytes = Bytes;
      using (var stream = new MemoryStream(bytes))
      {
        Test(bytes, stream.ToBinaryReader());
      }

      static void Test(byte[] result, BinaryReader reader)
      {
        using (reader)
        {
          AssertionExtensions.Should(() => reader.ToAsyncEnumerable(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
          AssertionExtensions.Should(() => reader.ToAsyncEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

          var enumerable = reader.ToAsyncEnumerable(1);
          enumerable.Should().BeOfType<IAsyncEnumerable<byte[]>>();
          enumerable.ToArray().SelectMany(bytes => bytes).Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
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
      AssertionExtensions.Should(() => ((BinaryReader) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
      Test(ReadOnlyStream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader reader)
    {
      using (reader)
      {
        var bytes = reader.BaseStream.ToBytes().ToArray();
        reader.BaseStream.MoveToStart();

        var enumerable = reader.ToBytes();
        enumerable.Should().BeOfType<IEnumerable<byte>>();
        enumerable.ToArray().Should().BeOfType<byte[]>().And.Equal(bytes);
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
      AssertionExtensions.Should(() => ((BinaryReader) null).ToBytesAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test(EmptyStream.ToBinaryReader());
      Test(Stream.ToBinaryReader());
      Test(ReadOnlyStream.ToBinaryReader());
    }

    return;

    static void Test(BinaryReader reader)
    {
      using (reader)
      {
        var bytes = reader.BaseStream.ToBytesAsync().ToArray();
        reader.BaseStream.MoveToStart();

        var enumerable = reader.ToBytesAsync();
        enumerable.Should().BeOfType<IAsyncEnumerable<byte>>();

        enumerable.ToArray().Should().BeOfType<byte[]>().And.Equal(bytes);
        enumerable.ToArray().Should().BeOfType<byte[]>().And.BeEmpty();
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
      AssertionExtensions.Should(() => ((BinaryReader) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Test(string.Empty, System.IO.Stream.Null.ToBinaryReader());
      Test(string.Empty, EmptyStream.ToBinaryReader());

      var text = Fixture.Create<string>();
      Encoding.GetEncodings().Select(encoding => encoding.GetEncoding()).ForEach(encoding =>
      {
        using var stream = new MemoryStream();

        text.WriteTo(stream.ToBinaryWriter(encoding));
        Test(text, stream.MoveToStart().ToBinaryReader(encoding));
      });
    }

    return;

    static void Test(string result, BinaryReader reader)
    {
      using (reader)
      {
        reader.ToText().Should().BeOfType<string>().And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryReaderExtensions.ToBoolean(BinaryReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBoolean_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, System.IO.Stream.Null.ToBinaryReader());
      Test(false, EmptyStream.ToBinaryReader());
      Test(true, Stream.ToBinaryReader());
    }

    return;

    static void Test(bool result, BinaryReader reader)
    {
      using (reader)
      {
        reader.ToBoolean().Should().Be(result);
      }
    }
  }
}