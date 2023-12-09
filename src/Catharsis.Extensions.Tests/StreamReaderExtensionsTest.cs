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
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.IsStart(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStart_Method()
  {
    void Validate(StreamReader reader)
    {
      using (reader)
      {
        reader.BaseStream.MoveToStart();
        reader.IsStart().Should().BeTrue();
        reader.BaseStream.MoveToEnd();
        reader.IsStart().Should().Be(reader.BaseStream.Length == 0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamReader) null).IsStart()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.ToStreamReader().IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToStreamReader());
      Validate(EmptyStream.ToStreamReader());
      Validate(RandomStream.ToStreamReader());
      Validate(RandomReadOnlyStream.ToStreamReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.IsEmpty(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
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
      AssertionExtensions.Should(() => ((StreamReader) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Stream.Null.ToStreamReader(), true);
      Validate(EmptyStream.ToStreamReader(), true);
      Validate(RandomStream.ToStreamReader(), false);
      Validate(RandomReadOnlyStream.ToStreamReader(), false);
      Validate(RandomReadOnlyForwardStream.ToStreamReader(), false);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.Empty(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
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
      AssertionExtensions.Should(() => ((StreamReader) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Stream.Null.ToStreamReader());
      Validate(RandomStream.ToStreamReader());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamReaderExtensions.Rewind(StreamReader)"/> method.</para>
  /// </summary>
  [Fact]
  public void Rewind_Method()
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
      AssertionExtensions.Should(() => ((StreamReader) null).Rewind()).ThrowExactly<ArgumentNullException>().WithParameterName("reader");

      Validate(Stream.Null.ToStreamReader());
      Validate(RandomStream.ToStreamReader());
    }
  }
}