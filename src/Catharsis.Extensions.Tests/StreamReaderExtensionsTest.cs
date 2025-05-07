using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StreamReaderExtensions"/>.</para>
/// </summary>
public sealed class StreamReaderExtensionsTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.IsStart(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStart_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).IsStart()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => this.RandomReadOnlyForwardStream().ToStreamReader().IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToStreamReader());
      Validate(this.EmptyStream().ToStreamReader());
      Validate(this.RandomStream().ToStreamReader());
      Validate(this.RandomReadOnlyStream().ToStreamReader());
    }

    return;

    static void Validate(StreamReader reader)
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
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.Rewind(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Rewind_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).Rewind()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Stream.Null.ToStreamReader());
      Validate(this.RandomStream().ToStreamReader());
    }

    return;

    static void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.ToBytesAsync().Await();
        reader.Rewind().Should().BeOfType<StreamReader>().And.BeSameAs(reader);
        reader.BaseStream.Should().HavePosition(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.IsUnset(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      Validate(true, null);
      Validate(true, Stream.Null.ToStreamReader());
      Validate(true, this.EmptyStream().ToStreamReader());
      Validate(false, this.RandomStream().ToStreamReader());
      Validate(false, this.RandomReadOnlyStream().ToStreamReader());
      Validate(false, this.RandomReadOnlyForwardStream().ToStreamReader());
    }

    return;

    static void Validate(bool result, StreamReader reader)
    {
      using (reader)
      {
        reader.IsUnset().Should().Be(reader is null || reader.IsEmpty()).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.IsEmpty(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(true, Stream.Null.ToStreamReader());
      Validate(true, this.EmptyStream().ToStreamReader());
      Validate(false, this.RandomStream().ToStreamReader());
      Validate(false, this.RandomReadOnlyStream().ToStreamReader());
      Validate(false, this.RandomReadOnlyForwardStream().ToStreamReader());
    }

    return;

    static void Validate(bool result, StreamReader reader)
    {
      using (reader)
      {
        reader.IsEmpty().Should().Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.Empty(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Stream.Null.ToStreamReader());
      Validate(this.RandomStream().ToStreamReader());
    }

    return;

    static void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.Empty().Should().BeOfType<StreamReader>().And.BeSameAs(reader);
        reader.BaseStream.Should().BeOfType<Stream>().And.HaveLength(0).And.HavePosition(0);
        reader.Peek().Should().Be(-1);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.Clone(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamReaderExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Stream.Null.ToStreamReader());
      Validate(this.EmptyStream().ToStreamReader());
      Validate(this.RandomStream().ToStreamReader());
    }

    return;

    static void Validate(StreamReader original)
    {
      using (original)
      {
        var clone = original.Clone();

        using (clone)
        {
          clone.Should().BeOfType<StreamReader>().And.NotBeSameAs(original);
          clone.BaseStream.Should().BeSameAs(original.BaseStream);
          clone.CurrentEncoding.Should().BeSameAs(original.CurrentEncoding);
        }
      }
    }
  }
}