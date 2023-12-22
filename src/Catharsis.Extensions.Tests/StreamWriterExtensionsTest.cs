﻿using FluentAssertions;
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
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.IsEmpty(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    void Validate(StreamWriter writer)
    {
      using (writer)
      {
        writer.Empty().IsEmpty().Should().BeTrue();
        writer.BaseStream.WriteByte(byte.MinValue);
        writer.IsEmpty().Should().BeFalse();
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ToStreamWriter().IsEmpty()).ThrowExactly<ArgumentException>();

      Validate(EmptyStream.ToStreamWriter());
      Validate(RandomStream.ToStreamWriter());
      Validate(WriteOnlyStream.ToStreamWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.Empty(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    void Validate(StreamWriter writer)
    {
      using (writer)
      {
        writer.Empty().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HaveLength(0).And.HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

      Validate(Stream.Null.ToStreamWriter());
      Validate(RandomStream.ToStreamWriter());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamWriterExtensions.Rewind(StreamWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Rewind_Method()
  {
    void Validate(StreamWriter writer)
    {
      using (writer)
      {
        RandomBytes.WriteToAsync(writer).Await();
        writer.Flush();
        writer.Rewind().Should().NotBeNull().And.BeSameAs(writer);
        writer.BaseStream.Should().HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StreamWriter) null).Rewind()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");

      Validate(Stream.Null.ToStreamWriter());
      Validate(RandomStream.ToStreamWriter());
    }
  }
}