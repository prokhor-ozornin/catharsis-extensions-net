using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StreamWriterExtensions"/>.</para>
/// </summary>
/// <seealso cref="StreamWriterExtensions"/>
public sealed class StreamWriterExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.get_IsUnset(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Property()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test(bool result, StreamWriter writer) => writer.IsUnset.Should().Be(writer is null || writer.IsEmpty).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.get_IsEmpty(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).IsEmpty).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToStreamWriter().IsEmpty).ThrowExactly<ArgumentException>();

      Test(EmptyStream.ToStreamWriter());
      Test(Stream.ToStreamWriter());
      Test(WriteOnlyStream.ToStreamWriter());
    }

    return;

    static void Test(StreamWriter writer)
    {
      using (writer)
      {
        writer.Empty().IsEmpty.Should().BeTrue();
        writer.BaseStream.WriteByte(byte.MinValue);
        writer.IsEmpty.Should().BeFalse();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.Rewind(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Rewind_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).Rewind()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

      Test(System.IO.Stream.Null.ToStreamWriter(), Bytes);
      Test(Stream.ToStreamWriter(), Bytes);
    }

    return;

    static void Test(StreamWriter writer, IEnumerable<byte> bytes)
    {
      using (writer)
      {
        bytes.WriteToAsync(writer).Await();
        writer.Flush();
        writer.Rewind().Should().BeOfType<StreamWriter>().And.BeSameAs(writer);
        writer.BaseStream.Should().BeOfType<Stream>().And.HavePosition(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.Empty(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

      Test(System.IO.Stream.Null.ToStreamWriter());
      Test(Stream.ToStreamWriter());
    }

    return;

    static void Test(StreamWriter writer)
    {
      using (writer)
      {
        writer.Empty().Should().BeOfType<StreamWriter>().And.BeSameAs(writer);
        writer.BaseStream.Should().BeOfType<Stream>().And.HaveLength(0).And.HavePosition(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.Clone(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).Clone()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

      Test(System.IO.Stream.Null.ToStreamWriter());
      Test(EmptyStream.ToStreamWriter());
      Test(Stream.ToStreamWriter());
    }

    return;

    static void Test(StreamWriter original)
    {
      using (original)
      {
        var clone = original.Clone();

        using (clone)
        {
          clone.Should().BeOfType<StreamWriter>().And.NotBeSameAs(original);
          clone.AutoFlush.Should().Be(original.AutoFlush);
          clone.BaseStream.Should().BeSameAs(original.BaseStream);
          clone.Encoding.Should().BeSameAs(original.Encoding);
          clone.NewLine.Should().Be(original.NewLine);
          clone.FormatProvider.Should().BeSameAs(original.FormatProvider);
        }
      }
    }
  }
}