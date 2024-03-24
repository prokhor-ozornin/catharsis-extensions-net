using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StreamReaderExtensions"/>.</para>
/// </summary>
public sealed class StreamReaderExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.Clone(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => StreamReaderExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

    throw new NotImplementedException();

    return;

    static void Validate(StreamReader reader)
    {
      using (reader)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.IsStart(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStart_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).IsStart()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().ToStreamReader().IsStart()).ThrowExactly<NotSupportedException>();

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
        reader.IsStart().Should().BeTrue();
        reader.BaseStream.MoveToEnd();
        reader.IsStart().Should().Be(reader.BaseStream.Length == 0);
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
      Validate(true, Attributes.EmptyStream().ToStreamReader());
      Validate(false, Attributes.RandomStream().ToStreamReader());
      Validate(false, Attributes.RandomReadOnlyStream().ToStreamReader());
      Validate(false, Attributes.RandomReadOnlyForwardStream().ToStreamReader());
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
      Validate(Attributes.RandomStream().ToStreamReader());
    }

    return;

    static void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.Empty().Should().BeSameAs(reader);
        reader.BaseStream.Should().HaveLength(0).And.HavePosition(0);
        reader.Peek().Should().Be(-1);
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
      Validate(Attributes.RandomStream().ToStreamReader());
    }

    return;

    static void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.ToBytesAsync().Await();
        reader.Rewind().Should().BeSameAs(reader);
        reader.BaseStream.Should().HavePosition(0);
      }
    }
  }
}