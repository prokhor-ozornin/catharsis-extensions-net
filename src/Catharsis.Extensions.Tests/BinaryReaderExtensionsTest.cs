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
    AssertionExtensions.Should(() => BinaryReaderExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();

    return;

    static void Validate(BinaryReader reader)
    {
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
        reader.IsStart().Should().Be(reader.BaseStream.Length == 0);
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
        reader.IsEnd().Should().Be(reader.BaseStream.Length == 0);
        reader.BaseStream.MoveToEnd();
        reader.IsEnd().Should().BeTrue();
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

      Validate(Stream.Null.ToBinaryReader(), true);
      Validate(Attributes.EmptyStream().ToBinaryReader(), true);
      Validate(Attributes.RandomStream().ToBinaryReader(), false);
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader(), false);
      Validate(Attributes.RandomReadOnlyForwardStream().ToBinaryReader(), false);
    }

    return;

    static void Validate(BinaryReader reader, bool isEmpty)
    {
      using (reader)
      {
        reader.IsEmpty().Should().Be(isEmpty);
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

      Validate(Attributes.EmptyStream().ToBinaryReader());
      Validate(Attributes.RandomStream().ToBinaryReader());
      Validate(Attributes.RandomReadOnlyStream().ToBinaryReader());
    }

    return;

    static void Validate(BinaryReader reader)
    {
      using (reader)
      {
        reader.Empty().Should().NotBeNull().And.BeSameAs(reader);
        reader.BaseStream.Should().HaveLength(0).And.HavePosition(0);
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
        reader.Rewind().Should().NotBeNull().And.BeSameAs(reader);
        reader.BaseStream.Should().HavePosition(0);
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
        AssertionExtensions.Should(() => reader.Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

        reader.Skip(0).Should().NotBeNull().And.BeSameAs(reader);
        reader.IsStart().Should().BeTrue();

        reader.Skip((int) reader.BaseStream.Length).Should().NotBeNull().And.BeSameAs(reader);
        reader.IsEnd().Should().BeTrue();

        reader.Skip(int.MaxValue).Should().NotBeNull().And.BeSameAs(reader);
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

      Validate(Stream.Null);
      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.RandomReadOnlyStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using var reader = stream.ToBinaryReader();

      reader.TryFinallyClear(_ => { }).Should().NotBeNull().And.BeSameAs(reader);
      reader.BaseStream.Should().HavePosition(0).And.HaveLength(0);
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => BinaryReaderExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

        Validate(Stream.Null.ToBinaryReader(), []);
        Validate(Attributes.EmptyStream().ToBinaryReader(), []);

        var bytes = Attributes.RandomBytes();
        using (var stream = new MemoryStream(bytes))
        {
          Validate(stream.ToBinaryReader(), bytes);
        }
      }

      static void Validate(BinaryReader reader, byte[] bytes)
      {
        using (reader)
        {
          var sequence = reader.ToEnumerable();
          sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToEnumerable());
          sequence.Should().Equal(bytes);
        }
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => BinaryReaderExtensions.ToEnumerable(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

        Validate(Stream.Null.ToBinaryReader(), []);
        Validate(Attributes.EmptyStream().ToBinaryReader(), []);

        var bytes = Attributes.RandomBytes();
        using (var stream = new MemoryStream(bytes))
        {
          Validate(stream.ToBinaryReader(), bytes);
        }
      }

      static void Validate(BinaryReader reader, byte[] bytes)
      {
        using (reader)
        {
          AssertionExtensions.Should(() => reader.ToEnumerable(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
          AssertionExtensions.Should(() => reader.ToEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

          var sequence = reader.ToEnumerable(1);
          sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToEnumerable(1));
          sequence.SelectMany(bytes => bytes).Should().Equal(bytes);
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => BinaryReaderExtensions.ToAsyncEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

        Validate(Stream.Null.ToBinaryReader(), []);
        Validate(Attributes.EmptyStream().ToBinaryReader(), []);

        var bytes = Attributes.RandomBytes();
        using (var stream = new MemoryStream(bytes))
        {
          Validate(stream.ToBinaryReader(), bytes);
        }
      }

      static void Validate(BinaryReader reader, byte[] bytes)
      {
        using (reader)
        {
          var sequence = reader.ToAsyncEnumerable();
          sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToAsyncEnumerable());
          sequence.ToArray().Should().Equal(bytes);
        }
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => BinaryReaderExtensions.ToAsyncEnumerable(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

        Validate(Stream.Null.ToBinaryReader(), []);
        Validate(Attributes.EmptyStream().ToBinaryReader(), []);

        var bytes = Attributes.RandomBytes();
        using (var stream = new MemoryStream(bytes))
        {
          Validate(stream.ToBinaryReader(), bytes);
        }
      }

      static void Validate(BinaryReader reader, byte[] bytes)
      {
        using (reader)
        {
          AssertionExtensions.Should(() => reader.ToAsyncEnumerable(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
          AssertionExtensions.Should(() => reader.ToAsyncEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

          var sequence = reader.ToAsyncEnumerable(1);
          sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToAsyncEnumerable(5));
          sequence.ToArray().SelectMany(bytes => bytes).Should().Equal(bytes);
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
        sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToBytes());

        sequence.ToArray().Should().Equal(bytes);
        sequence.ToArray().Should().BeEmpty();
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
        sequence.Should().NotBeNull().And.NotBeSameAs(reader.ToBytesAsync());

        sequence.ToArray().Should().Equal(bytes);
        sequence.ToArray().Should().BeEmpty();
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

      Validate(Stream.Null.ToBinaryReader(), string.Empty);
      Validate(Attributes.EmptyStream().ToBinaryReader(), string.Empty);

      var text = Attributes.RandomString();
      foreach (var encoding in Encoding.GetEncodings().Select(encoding => encoding.GetEncoding()))
      {
        using var stream = new MemoryStream();

        text.WriteTo(stream.ToBinaryWriter(encoding));
        Validate(stream.MoveToStart().ToBinaryReader(encoding), text);
      }
    }

    return;

    static void Validate(BinaryReader reader, string value)
    {
      using (reader)
      {
        reader.ToText().Should().NotBeNull().And.Be(value);
      }
    }
  }
}