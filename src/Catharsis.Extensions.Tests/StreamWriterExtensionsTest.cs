using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StreamWriterExtensions"/>.</para>
/// </summary>
public sealed class StreamWriterExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.Clone(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => StreamWriterExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

    throw new NotImplementedException();

    return;

    static void Validate(StreamWriter writer)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.IsEmpty(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((StreamWriter) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
    AssertionExtensions.Should(() => Attributes.WriteOnlyForwardStream().ToStreamWriter().IsEmpty()).ThrowExactly<ArgumentException>();

    Validate(Attributes.EmptyStream().ToStreamWriter());
    Validate(Attributes.RandomStream().ToStreamWriter());
    Validate(Attributes.WriteOnlyStream().ToStreamWriter());

    return;

    static void Validate(StreamWriter writer)
    {
      using (writer)
      {
        writer.Empty().IsEmpty().Should().BeTrue();
        writer.BaseStream.WriteByte(byte.MinValue);
        writer.IsEmpty().Should().BeFalse();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.Empty(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    AssertionExtensions.Should(() => ((StreamWriter) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

    Validate(Stream.Null.ToStreamWriter());
    Validate(Attributes.RandomStream().ToStreamWriter());

    return;

    static void Validate(StreamWriter writer)
    {
      using (writer)
      {
        writer.Empty().Should().BeSameAs(writer);
        writer.BaseStream.Should().HaveLength(0).And.HavePosition(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.Rewind(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Rewind_Method()
  {
    AssertionExtensions.Should(() => ((StreamWriter) null).Rewind()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

    Validate(Stream.Null.ToStreamWriter(), Attributes.RandomBytes());
    Validate(Attributes.RandomStream().ToStreamWriter(), Attributes.RandomBytes());

    return;

    static void Validate(StreamWriter writer, IEnumerable<byte> bytes)
    {
      using (writer)
      {
        bytes.WriteToAsync(writer).Await();
        writer.Flush();
        writer.Rewind().Should().BeSameAs(writer);
        writer.BaseStream.Should().HavePosition(0);
      }
    }
  }
}