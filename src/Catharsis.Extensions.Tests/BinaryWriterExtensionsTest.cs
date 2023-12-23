using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="BinaryWriterExtensions"/>.</para>
/// </summary>
public sealed class BinaryWriterExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.Clone(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => BinaryWriterExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.IsStart(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStart_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryWriter) null).IsStart()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null.ToBinaryWriter());
      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
      Validate(WriteOnlyStream.ToBinaryWriter());
    }

    return;

    static void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        writer.BaseStream.MoveToStart();
        writer.IsStart().Should().BeTrue();
        writer.BaseStream.MoveToEnd();
        writer.IsStart().Should().Be(writer.BaseStream.Length == 0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.IsEnd(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEnd_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryWriter) null).IsEnd()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().IsEnd()).ThrowExactly<ArgumentException>();

      Validate(Stream.Null.ToBinaryWriter());
      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
      Validate(WriteOnlyStream.ToBinaryWriter());
    }

    return;

    static void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        writer.BaseStream.MoveToStart();
        writer.IsEnd().Should().Be(writer.BaseStream.Length == 0);
        writer.BaseStream.MoveToEnd();
        writer.IsEnd().Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.IsEmpty(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryWriter) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().IsEmpty()).ThrowExactly<ArgumentException>();

      Validate(Stream.Null.ToBinaryWriter(), true);
      Validate(EmptyStream.ToBinaryWriter(), true);
      Validate(RandomStream.ToBinaryWriter(), false);
      Validate(WriteOnlyStream.ToBinaryWriter(), true);
    }

    return;

    static void Validate(BinaryWriter writer, bool empty)
    {
      using (writer)
      {
        writer.IsEmpty().Should().Be(empty);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.Empty(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryWriter) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().Empty()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
      Validate(WriteOnlyStream.ToBinaryWriter());
    }

    return;

    static void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        writer.Empty().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HaveLength(0).And.HavePosition(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.Rewind(BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Rewind_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryWriter) null).Rewind()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().Rewind()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream.ToBinaryWriter());
      Validate(RandomStream.ToBinaryWriter());
      Validate(WriteOnlyStream.ToBinaryWriter());
    }

    return;

    static void Validate(BinaryWriter writer)
    {
      using (writer)
      {
        writer.BaseStream.MoveToEnd();
        writer.Rewind().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HavePosition(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.TryFinallyClear(BinaryWriter, Action{BinaryWriter})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((BinaryWriter) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
      AssertionExtensions.Should(() => Stream.Null.ToBinaryWriter().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToBinaryWriter().TryFinallyClear(_ => { })).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null);
      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(WriteOnlyStream);
    }

    return;

    static void Validate(Stream stream)
    {
      using var writer = stream.ToBinaryWriter();

      writer.TryFinallyClear(_ => { }).Should().NotBeNull().And.BeSameAs(writer);
      writer.BaseStream.Should().HavePosition(0).And.HaveLength(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.WriteBytes(BinaryWriter, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    AssertionExtensions.Should(() => BinaryWriterExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Stream.Null.ToBinaryWriter().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BinaryWriterExtensions.WriteText(BinaryWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => BinaryWriterExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Stream.Null.ToBinaryWriter().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }
}