using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StreamExtensions"/>.</para>
/// </summary>
public sealed class StreamExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsReadOnly(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsReadOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsReadOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(false, Stream.Null);
      Validate(false, Attributes.EmptyStream());
      Validate(false, Attributes.RandomStream());
      Validate(true, Attributes.RandomReadOnlyStream());
      Validate(true, Attributes.RandomReadOnlyForwardStream());
      Validate(false, Attributes.WriteOnlyStream());
      Validate(false, Attributes.WriteOnlyForwardStream());
    }

    return;

    static void Validate(bool result, Stream stream)
    {
      using (stream)
      {
        stream.IsReadOnly().Should().Be(stream.CanRead && !stream.CanWrite).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsWriteOnly(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsWriteOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsWriteOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(false, Stream.Null);
      Validate(false, Attributes.EmptyStream());
      Validate(false, Attributes.RandomStream());
      Validate(false, Attributes.RandomReadOnlyStream());
      Validate(false, Attributes.RandomReadOnlyForwardStream());
      Validate(true, Attributes.WriteOnlyStream());
      Validate(true, Attributes.WriteOnlyForwardStream());
    }

    return;

    static void Validate(bool result, Stream stream)
    {
      using (stream)
      {
        stream.IsWriteOnly().Should().Be(stream.CanWrite && !stream.CanRead).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsOperable(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsOperable_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsOperable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(true, Stream.Null);
      Validate(true, Attributes.EmptyStream());
      Validate(true, Attributes.RandomStream());
      Validate(true, Attributes.RandomReadOnlyStream());
      Validate(true, Attributes.RandomReadOnlyForwardStream());
      Validate(true, Attributes.WriteOnlyStream());
      Validate(true, Attributes.WriteOnlyForwardStream());
    }

    return;

    static void Validate(bool result, Stream stream)
    {
      using (stream)
      {
        stream.IsOperable().Should().Be(stream.CanRead || stream.CanWrite).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsStart(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStart_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsStart(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().IsStart()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => Attributes.WriteOnlyForwardStream().IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null);
      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.RandomReadOnlyStream());
      Validate(Attributes.WriteOnlyStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        stream.MoveToStart().IsStart().Should().BeTrue();
        stream.MoveToEnd().IsStart().Should().Be(stream.IsEmpty());
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsEnd(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEnd_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsEnd(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Attributes.WriteOnlyForwardStream().IsEnd()).ThrowExactly<ArgumentException>();

      Validate(Stream.Null);
      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.RandomReadOnlyStream());
      Validate(Attributes.WriteOnlyStream());
    }

    return;

    static void Validate(Stream stream) {
      using (stream)
      {
        stream.MoveToStart().IsEnd().Should().Be(stream.IsEmpty());
        stream.MoveToEnd().IsEnd().Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsUnset(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Attributes.WriteOnlyForwardStream().IsEmpty()).ThrowExactly<ArgumentException>();

      Validate(true, null);
      Validate(true, Stream.Null);
      Validate(true, Attributes.EmptyStream());
      Validate(false, Attributes.RandomStream());
      Validate(false, Attributes.RandomReadOnlyStream());
      Validate(false, Attributes.RandomReadOnlyForwardStream());
      Validate(true, Attributes.WriteOnlyStream());
    }

    return;

    static void Validate(bool result, Stream stream)
    {
      using (stream)
      {
        stream.IsUnset().Should().Be(stream is null || stream.IsEmpty()).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsEmpty(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsEmpty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Attributes.WriteOnlyForwardStream().IsEmpty()).ThrowExactly<ArgumentException>();

      Validate(true, Stream.Null);
      Validate(true, Attributes.EmptyStream());
      Validate(false, Attributes.RandomStream());
      Validate(false, Attributes.RandomReadOnlyStream());
      Validate(false, Attributes.RandomReadOnlyForwardStream());
      Validate(true, Attributes.WriteOnlyStream());
    }

    return;

    static void Validate(bool result, Stream stream)
    {
      using (stream)
      {
        stream.IsEmpty().Should().Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Empty{TStream}(TStream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Validate(Attributes.RandomReadOnlyForwardStream())).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => Validate(Attributes.WriteOnlyForwardStream())).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null);
      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.RandomReadOnlyStream());
      Validate(Attributes.WriteOnlyStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        stream.Empty().Should().BeAssignableTo<Stream>().And.BeSameAs(stream).And.HavePosition(0).And.HaveLength(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Min(Stream, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.Min(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("min");
      AssertionExtensions.Should(() => Stream.Null.Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("max");

      Validate(Stream.Null, Stream.Null);
      Validate(Stream.Null, Attributes.EmptyStream());
      Validate(Stream.Null, Attributes.RandomStream());
      
      Validate(Attributes.EmptyStream(), Stream.Null);
      Validate(Attributes.EmptyStream(), Attributes.EmptyStream());
      Validate(Attributes.EmptyStream(), Attributes.RandomStream());
    }

    return;

    static void Validate(Stream min, Stream max)
    {
      using (min)
      {
        using (max)
        {
          min.Min(max).Should().BeAssignableTo<Stream>().And.BeSameAs(min);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Max(Stream, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.Max(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => Stream.Null.Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(Stream.Null, Stream.Null);
      Validate(Stream.Null, Attributes.EmptyStream());
      Validate(Stream.Null, Attributes.RandomStream());

      Validate(Attributes.EmptyStream(), Stream.Null);
      Validate(Attributes.EmptyStream(), Attributes.EmptyStream());
      Validate(Attributes.EmptyStream(), Attributes.RandomStream());
    }

    return;

    static void Validate(Stream min, Stream max)
    {
      using (min)
      {
        using (max)
        {
          min.Max(max).Should().BeOfType<Stream>().And.BeSameAs(max);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MinMax(Stream, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.MinMax(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("min");
      AssertionExtensions.Should(() => Stream.Null.MinMax(null)).ThrowExactly<ArgumentNullException>().WithParameterName("max");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream min, Stream max) => min.MinMax(max).Should().Be((min, max));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveBy{TStream}(TStream, long)"/> method.</para>
  /// </summary>
  [Fact]
  public void MoveBy_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.MoveBy<Stream>(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, long offset)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveTo{TStream}(TStream, long)"/> method.</para>
  /// </summary>
  [Fact]
  public void MoveTo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.MoveTo<Stream>(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, long position)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveToStart{TStream}(TStream)"/> method.</para>
  /// </summary>
  [Fact]
  public void MoveToStart_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.MoveToStart<Stream>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Stream.Null);
      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        stream.MoveToEnd().MoveToStart().Should().BeOfType<Stream>().And.BeSameAs(stream).And.HavePosition(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveToEnd{TStream}(TStream)"/> method.</para>
  /// </summary>
  [Fact]
  public void MoveToEnd_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.MoveToStart<Stream>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Stream.Null);
      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        stream.MoveToStart().MoveToEnd().Should().BeOfType<Stream>().And.BeSameAs(stream).And.HavePosition(stream.Length);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Lines(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(IEnumerable<string> result, Stream stream, Encoding encoding = null)
    {
      using (stream)
      {
        stream.Lines(encoding).Should().BeOfType<string[]>().And.Equal(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.LinesAsync(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void LinesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.LinesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(IEnumerable<string> result, Stream stream, Encoding encoding = null)
    {
      using (stream)
      {
        stream.LinesAsync(encoding).ToArray().Should().BeOfType<string[]>().And.Equal(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Skip{TStream}(TStream, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Skip_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).Skip(0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Stream.Null.Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, int count)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.TryFinallyClear{TStream}(TStream, Action{TStream})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.TryFinallyClear<Stream>(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Stream.Null.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, IEnumerable<byte> bytes)
    {
      using (stream)
      {
        stream.TryFinallyClear(stream => stream.WriteBytes(bytes)).Should().BeOfType<Stream>().And.BeSameAs(stream);
        stream.Should().BeOfType<Stream>().And.HavePosition(0).And.HaveLength(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsSynchronized(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsSynchronized_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsSynchronized(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.RandomReadOnlyStream());
      Validate(Attributes.WriteOnlyStream());
      Validate(Attributes.RandomReadOnlyForwardStream());
      Validate(Attributes.WriteOnlyForwardStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (var synchronized = stream.AsSynchronized())
      {
        AssertionExtensions.Should(() => synchronized.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => synchronized.WriteTimeout).ThrowExactly<InvalidOperationException>();

        synchronized.Should().BeOfType<Stream>().And.NotBeSameAs(stream);

        synchronized.CanRead.Should().Be(stream.CanRead);
        synchronized.CanWrite.Should().Be(stream.CanWrite);
        synchronized.CanSeek.Should().Be(stream.CanSeek);
        synchronized.CanTimeout.Should().Be(stream.CanTimeout);

        if (stream.CanSeek)
        {
          synchronized.Length.Should().Be(stream.Length);

          synchronized.Position.Should().Be(stream.Position);

          synchronized.MoveToEnd();
          stream.Position.Should().Be(synchronized.Position);
        }
      }

      if (stream.CanSeek)
      {
        AssertionExtensions.Should(() => stream.Length).ThrowExactly<ObjectDisposedException>();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsReadOnly(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsReadOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Attributes.WriteOnlyStream().AsReadOnly()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => Attributes.WriteOnlyForwardStream().AsReadOnly()).ThrowExactly<NotSupportedException>();

      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.RandomReadOnlyStream());
      Validate(Attributes.RandomReadOnlyForwardStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (var readOnly = stream.AsReadOnly())
      {
        AssertionExtensions.Should(() => readOnly.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => readOnly.WriteTimeout).ThrowExactly<InvalidOperationException>();

        readOnly.Should().BeOfType<Stream>().And.NotBeSameAs(stream);

        readOnly.CanRead.Should().BeTrue();
        readOnly.CanWrite.Should().BeFalse();
        readOnly.CanSeek.Should().Be(stream.CanSeek);
        readOnly.CanTimeout.Should().Be(stream.CanTimeout);

        if (stream.CanSeek)
        {
          readOnly.Length.Should().Be(stream.Length);

          readOnly.Position.Should().Be(stream.Position);

          readOnly.MoveToEnd();
          stream.Position.Should().Be(readOnly.Position);
        }
      }

      if (stream.CanSeek)
      {
        AssertionExtensions.Should(() => stream.Length).ThrowExactly<ObjectDisposedException>();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsReadOnlyForward(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnlyForward_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsReadOnlyForward(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Attributes.WriteOnlyStream().AsReadOnlyForward()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => Attributes.WriteOnlyForwardStream().AsReadOnlyForward()).ThrowExactly<NotSupportedException>();

      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.RandomReadOnlyStream());
      Validate(Attributes.RandomReadOnlyForwardStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (var readOnly = stream.AsReadOnlyForward())
      {
        AssertionExtensions.Should(() => readOnly.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => readOnly.WriteTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => readOnly.Length).ThrowExactly<NotSupportedException>();
        AssertionExtensions.Should(() => readOnly.Position).ThrowExactly<NotSupportedException>();

        readOnly.Should().BeOfType<Stream>().And.NotBeSameAs(stream);

        readOnly.CanRead.Should().BeTrue();
        readOnly.CanWrite.Should().BeFalse();
        readOnly.CanSeek.Should().BeFalse();
        readOnly.CanTimeout.Should().Be(stream.CanTimeout);
      }

      if (stream.CanSeek)
      {
        AssertionExtensions.Should(() => stream.Length).ThrowExactly<ObjectDisposedException>();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsWriteOnly(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsWriteOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsWriteOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyStream().AsWriteOnly()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().AsWriteOnly()).ThrowExactly<NotSupportedException>();

      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.WriteOnlyStream());
      Validate(Attributes.WriteOnlyForwardStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (var writeOnly = stream.AsWriteOnly())
      {
        AssertionExtensions.Should(() => writeOnly.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => writeOnly.WriteTimeout).ThrowExactly<InvalidOperationException>();

        writeOnly.Should().BeOfType<Stream>().And.NotBeSameAs(stream);

        writeOnly.CanRead.Should().BeFalse();
        writeOnly.CanWrite.Should().BeTrue();
        writeOnly.CanSeek.Should().Be(stream.CanSeek);
        writeOnly.CanTimeout.Should().Be(stream.CanTimeout);

        if (stream.CanSeek)
        {
          writeOnly.Length.Should().Be(stream.Length);

          writeOnly.Position.Should().Be(stream.Position);

          writeOnly.MoveToEnd();
          stream.Position.Should().Be(writeOnly.Position);
        }
      }

      if (stream.CanSeek)
      {
        AssertionExtensions.Should(() => stream.Length).ThrowExactly<ObjectDisposedException>();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsWriteOnlyForward(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsWriteOnlyForward_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsWriteOnlyForward(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyStream().AsWriteOnlyForward()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => Attributes.RandomReadOnlyForwardStream().AsWriteOnlyForward()).ThrowExactly<NotSupportedException>();

      Validate(Attributes.EmptyStream());
      Validate(Attributes.RandomStream());
      Validate(Attributes.WriteOnlyStream());
      Validate(Attributes.WriteOnlyForwardStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (var writeOnly = stream.AsWriteOnlyForward())
      {
        AssertionExtensions.Should(() => writeOnly.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => writeOnly.WriteTimeout).ThrowExactly<InvalidOperationException>();

        writeOnly.Should().BeOfType<Stream>().And.NotBeSameAs(stream);

        writeOnly.CanRead.Should().BeFalse();
        writeOnly.CanWrite.Should().BeTrue();
        writeOnly.CanSeek.Should().BeFalse();
        writeOnly.CanTimeout.Should().Be(stream.CanTimeout);
      }

      if (stream.CanSeek)
      {
        AssertionExtensions.Should(() => stream.Length).ThrowExactly<ObjectDisposedException>();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBytes(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBytes(null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Attributes.WriteOnlyStream().ToBytes().ToArray()).ThrowExactly<NotSupportedException>();

      Validate([], Stream.Null);
      Validate([], Attributes.EmptyStream(), true);

      Attributes.RandomBytes().With(bytes => Validate(bytes, bytes.ToMemoryStream()));
      Attributes.RandomBytes().With(bytes => Validate(bytes, bytes.ToMemoryStream(), true));
    }

    return;

    static void Validate(IEnumerable<byte> result, Stream stream, bool close = false)
    {
      using (stream)
      {
        stream.ToBytes(close).Should().BeAssignableTo<IEnumerable<byte>>().And.Equal(result);

        if (close)
        {
          stream.Should().NotBeReadable().And.NotBeWritable().And.NotBeSeekable();
        }
        else
        {
          stream.IsEnd().Should().BeTrue();
        }
      }
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBytesAsync(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBytesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
      AssertionExtensions.Should(() => Attributes.WriteOnlyStream().ToBytesAsync().ToArray()).ThrowExactly<AggregateException>().WithInnerException<NotSupportedException>();

      Validate([], Stream.Null);
      Validate([], Attributes.EmptyStream(), true);

      Attributes.RandomBytes().With(bytes => Validate(bytes, bytes.ToMemoryStream()));
      Attributes.RandomBytes().With(bytes => Validate(bytes, bytes.ToMemoryStream(), true));
    }

    return;

    static void Validate(IEnumerable<byte> result, Stream stream, bool close = false)
    {
      using (stream)
      {
        stream.ToBytesAsync(close).ToArray().Should().BeOfType<byte[]>().And.Equal(result);

        if (close)
        {
          stream.Should().NotBeReadable().And.NotBeWritable().And.NotBeSeekable();
        }
        else
        {
          stream.IsEnd().Should().BeTrue();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToText(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Encoding.GetEncodings().Select(encoding => encoding.GetEncoding()).ForEach(encoding =>
      {
        Validate(string.Empty, Stream.Null, encoding);
        //Validate(string.Empty, Attributes.EmptyStream(), encoding, true);

        Attributes.RandomString().With(text => Validate(text, new MemoryStream().WriteText(text, encoding), encoding));
        //Attributes.RandomString().With(text => Validate(text, new MemoryStream().WriteText(text, encoding), encoding));
      });
    }

    return;

    static void Validate(string result, Stream stream, Encoding encoding = null, bool close = false)
    {
      using (stream)
      {
        stream.ToText(encoding).Should().BeOfType<string>().And.Be(result);

        if (close)
        {
          stream.IsOperable().Should().BeFalse();
        }
        else
        {
          stream.IsEnd().Should().BeTrue();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToTextAsync(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

      //Validate(null);
      //Encoding.GetEncodings().ForEach(Validate);

      //var bytes = Attributes.RandomBytes();

      //var text = stream.Text(encoding);
      //stream.Rewind().ToTextReader(encoding).Text().Should().Be(text);
      //bytes.Text(encoding).Should().Be(text);
      //stream.ReadByte().Should().Be(-1);
      //stream.Rewind().Text(encoding).Should().Be(text);
      //AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, Stream stream, Encoding encoding = null)
    {
      using (stream)
      {
        var task = stream.ToTextAsync(encoding);
        task.Should().BeAssignableTo<Task<string>>();
        task.Await().Should().BeOfType<string>().And.Be(result);
        stream.IsEnd().Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteBytes{TStream}(TStream, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.WriteBytes<Stream>(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Stream.Null.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, byte[] bytes)
    {
      using (stream)
      {
        var position = stream.Position;
        var length = stream.Length;

        stream.MoveToEnd().WriteBytes(bytes).Should().BeOfType<Stream>().And.BeSameAs(stream);
        stream.Position.Should().Be(position + bytes.Length);
        stream.Length.Should().Be(length + bytes.Length);
        //stream.MoveBy(-bytes.Length)
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteBytesAsync{TStream}(TStream, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.WriteBytesAsync<Stream>(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Stream.Null.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Stream.Null.WriteBytesAsync(Enumerable.Empty<byte>(), Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, byte[] bytes)
    {
      using (stream)
      {
        var position = stream.Position;
        var length = stream.Length;

        var task = stream.WriteBytesAsync(bytes);
        task.Should().BeAssignableTo<Task<Stream>>();
        task.Await().Should().BeOfType<Stream>().And.BeSameAs(stream);
        stream.Position.Should().Be(position + bytes.Length);
        stream.Length.Should().Be(length + bytes.Length);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteText{TStream}(TStream, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.WriteText<Stream>(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Stream.Null.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, string text, Encoding encoding = null)
    {
      using (stream)
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteTextAsync{TStream}(TStream, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.WriteTextAsync<Stream>(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Stream.Null.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => Stream.Null.WriteTextAsync(string.Empty, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, string text, Encoding encoding = null)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteTo(IEnumerable{byte}, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
      AssertionExtensions.Should(() => StreamExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, byte[] bytes)
    {
      using (stream)
      {
        bytes.WriteTo(stream).Should().BeOfType<byte[]>().And.BeSameAs(bytes);
        //
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteToAsync(IEnumerable{byte}, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => StreamExtensions.WriteToAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Stream.Null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, byte[] bytes)
    {
      using (stream)
      {
        var task = bytes.WriteToAsync(stream);
        task.Should().BeAssignableTo<Task<IEnumerable<byte>>>();
        //
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteTo(string, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => StreamExtensions.WriteTo(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, string text, Encoding encoding = null)
    {
      using (stream)
      {
        text.WriteTo(stream, encoding).Should().BeOfType<string>().And.BeSameAs(text);
        //
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteToAsync(string, Stream, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => StreamExtensions.WriteToAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(Stream.Null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, string text, Encoding encoding = null)
    {
      using (stream)
      {
        var task = text.WriteToAsync(stream, encoding);
        task.Should().BeAssignableTo<Task<string>>();
        task.Await().Should().BeOfType<string>();
        //
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsBrotli(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompressAsBrotli_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.CompressAsBrotli(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        var compressed = stream.CompressAsBrotli();
        compressed.Should().BeOfType<BrotliStream>().And.BeReadable().And.BeWritable().And.BeSeekable().And.HavePosition(0).And.HaveLength(stream.Length);
        compressed.BaseStream.Should().BeSameAs(stream);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsDeflate(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompressAsDeflate_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.CompressAsDeflate(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      /*var bytes = Attributes.RandomBytes();

      var stream = new MemoryStream();
      byte[] compressed = [];
      using (var deflate = stream.Deflate(CompressionMode.Compress))
      {
        deflate.BaseStream.Should().BeOfType<Stream>().And.BeSameAs(stream);
        AssertionExtensions.Should(() => deflate.ReadByte()).ThrowExactly<InvalidOperationException>();
        deflate.Write(bytes);
      }
      compressed = stream.ToArray();
      compressed.Should().NotEqual(bytes);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();

      stream = new MemoryStream(compressed);
      byte[] decompressed = [];
      using (var deflate = stream.Deflate(CompressionMode.Decompress))
      {
        deflate.BaseStream.Should().BeSameAs(stream);
        decompressed = deflate.Bytes();
      }
      decompressed.Should().Equal(bytes);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();

      using (var s = new MemoryStream())
      {
        s.Deflate(bytes).Should().BeSameAs(s);
        s.ToArray().Should().Equal(compressed);
        s.Bytes().Should().BeEmpty();
        s.CanRead.Should().BeTrue();
        s.CanWrite.Should().BeTrue();
      }

      using (var s = new MemoryStream(compressed))
      {
        s.Deflate().Should().Equal(bytes);
        s.Bytes().Should().BeEmpty();
        s.CanRead.Should().BeTrue();
        s.CanWrite.Should().BeTrue();
      }

      new MemoryStream().Deflate(bytes).Rewind().Deflate().Should().Equal(bytes);*/
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsGzip(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompressAsGzip_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.CompressAsGzip(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      /*var bytes = Attributes.RandomBytes();

      var stream = new MemoryStream();
      byte[] compressed = [];
      using (var gzip = stream.Gzip(CompressionMode.Compress))
      {
        gzip.BaseStream.Should().BeSameAs(stream);
        AssertionExtensions.Should(() => gzip.ReadByte()).ThrowExactly<InvalidOperationException>();
        gzip.Write(bytes);
      }
      compressed = stream.ToArray();
      compressed.Should().NotEqual(bytes);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();

      stream = new MemoryStream(compressed);
      byte[] decompressed = [];
      using (var gzip = stream.Gzip(CompressionMode.Decompress))
      {
        gzip.BaseStream.Should().BeSameAs(stream);
        decompressed = gzip.Bytes();
      }
      decompressed.Should().Equal(bytes);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();

      using (var s = new MemoryStream())
      {
        s.CompressAsGzip(bytes).Should().BeSameAs(s);
        s.ToArray().Should().Equal(compressed);
        s.Bytes().Should().BeEmpty();
        s.CanRead.Should().BeTrue();
        s.CanWrite.Should().BeTrue();
      }

      using (var s = new MemoryStream(compressed))
      {
        s.DecompressAsGzip().Should().Equal(bytes);
        s.Bytes().Should().BeEmpty();
        s.CanRead.Should().BeTrue();
        s.CanWrite.Should().BeTrue();
      }

      new MemoryStream().CompressAsGzip(bytes).Rewind().CompressAsGzip().Should().Equal(bytes);*/
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsZlib(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompressAsZlib_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.CompressAsZlib(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsBrotli(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecompressAsBrotli_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.DecompressAsBrotli(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsDeflate(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecompressAsDeflate_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.DecompressAsDeflate(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsGzip(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecompressAsGzip_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.DecompressAsGzip(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsZlib(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecompressAsZlib_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.DecompressAsZlib(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StreamExtensions.ToEnumerable(Stream, bool)"/></description></item>
  ///     <item><description><see cref="StreamExtensions.ToEnumerable(Stream, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      static void Validate(Stream stream)
      {
        using (stream)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToEnumerable(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Stream.Null.ToEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate(Stream stream)
      {
        using (stream)
        {

        }
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StreamExtensions.ToAsyncEnumerable(Stream, bool)"/></description></item>
  ///     <item><description><see cref="StreamExtensions.ToAsyncEnumerable(Stream, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToAsyncEnumerable(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

      static void Validate(Stream stream)
      {
        using (stream)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToAsyncEnumerable(null, 1).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable(0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate(Stream stream)
      {
        using (stream)
        {

        }
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBufferedStream(Stream, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBufferedStream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBufferedStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Stream.Null.ToBufferedStream(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("bufferSize");

      new int?[] { null, 1, 4096, 8192 }.ForEach(buffer =>
      {
        Validate(Attributes.EmptyStream(), buffer);
        Validate(Attributes.RandomStream(), buffer);
        Validate(Attributes.RandomReadOnlyStream(), buffer);
        Validate(Attributes.WriteOnlyStream(), buffer);
        Validate(Attributes.RandomReadOnlyForwardStream(), buffer);
        Validate(Attributes.WriteOnlyForwardStream(), buffer);
      });
    }

    return;

    static void Validate(Stream stream, int? size)
    {
      using (var buffered = stream.ToBufferedStream(size))
      {
        AssertionExtensions.Should(() => buffered.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => buffered.WriteTimeout).ThrowExactly<InvalidOperationException>();

        buffered.Should().BeOfType<BufferedStream>();

        buffered.BufferSize.Should().Be(size ?? 4096);
        buffered.UnderlyingStream.Should().BeSameAs(stream);
        buffered.CanRead.Should().Be(stream.CanRead);
        buffered.CanWrite.Should().Be(stream.CanWrite);
        buffered.CanSeek.Should().Be(stream.CanSeek);
        buffered.CanTimeout.Should().Be(stream.CanTimeout);

        if (stream.CanSeek)
        {
          buffered.Length.Should().Be(stream.Length);

          buffered.Position.Should().Be(stream.Position);

          buffered.MoveToEnd();
          stream.Position.Should().Be(buffered.Position);
        }
      }

      if (stream.CanSeek)
      {
        AssertionExtensions.Should(() => stream.Length).ThrowExactly<ObjectDisposedException>();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBinaryReader(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBinaryReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBinaryReader(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Attributes.RandomStream(), null);
      Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomStream(), encoding.GetEncoding()));
    }
    
    return;

    static void Validate(MemoryStream stream, Encoding encoding)
    {
      using (stream)
      {
        var bytes = stream.ToArray();

        using (var reader = stream.ToBinaryReader(encoding))
        {
          reader.BaseStream.Should().BeOfType<Stream>().And.BeSameAs(stream);
          reader.ReadBytes(bytes.Length).Should().Equal(bytes);
        }

        AssertionExtensions.Should(stream.ReadByte).ThrowExactly<ObjectDisposedException>();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBinaryWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBinaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBinaryWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Attributes.RandomBytes());
      Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomBytes(), encoding.GetEncoding()));
    }

    return;

    static void Validate(byte[] bytes, Encoding encoding = null)
    {
      using var stream = new MemoryStream();

      using (var writer = stream.ToBinaryWriter(encoding))
      {
        writer.BaseStream.Should().BeOfType<Stream>().And.BeSameAs(stream);
        writer.Write(bytes);
      }

      stream.ToArray().Should().BeOfType<byte[]>().And.Equal(bytes);

      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToStreamReader(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToStreamReader(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Attributes.RandomStream());
      Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomStream(), encoding.GetEncoding()));
    }

    return;

    static void Validate(MemoryStream stream, Encoding encoding = null)
    {
      using (stream)
      {
        var bytes = stream.ToArray();
        using var reader = stream.ToStreamReader(encoding);

        reader.BaseStream.Should().BeOfType<Stream>().And.BeSameAs(stream);
        reader.ToText().Should().BeOfType<string>().And.Be(bytes.ToText(encoding));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToStreamWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToStreamWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Attributes.RandomString());
      Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomString(), encoding.GetEncoding()));
    }

    return;

    static void Validate(string text, Encoding encoding = null)
    {
      using var stream = new MemoryStream();

      using (var writer = stream.ToStreamWriter(encoding))
      {
        writer.BaseStream.Should().BeOfType<Stream>().And.BeSameAs(stream);
        writer.Write(text);
      }

      stream.ToArray().Should().BeOfType<byte[]>().And.Equal(text.ToBytes(encoding));
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlReader(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";

      using (var stream = new MemoryStream(Xml.Bytes()))
      {
        var reader = stream.ToXmlReader();
        reader.Settings.CloseInput.Should().BeFalse();
        reader.Settings.IgnoreComments.Should().BeTrue();
        reader.Settings.IgnoreWhitespace.Should().BeTrue();
        reader.ReadStartElement("article");
        reader.ReadString().Should().Be("text");
        reader.ReadEndElement();
        reader.Close();
        stream.Bytes().Should().BeEmpty();
        stream.ReadByte().Should().Be(-1);
      }

      using (var stream = new MemoryStream(Xml.Bytes()))
      {
        stream.ToXmlReader(true).Close();
        //True(reader.Settings.CloseInput);
        AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
      }*/

      // TODO Encoding support
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlDictionaryReader(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      /*var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><article>text</article>";
      using (var stream = new MemoryStream())
      {
        using (var writer = stream.ToXmlWriter())
        {
          writer.Settings.CloseOutput.Should().BeFalse();
          writer.Settings.Encoding.ToString().Should().Be(Encoding.UTF8.ToString());
          writer.Settings.Indent.Should().BeFalse();
          writer.WriteElementString("article", "text");
        }
        stream.ToArray().Should().Equal(xml.Bytes(Encoding.UTF8));
        stream.Bytes().Should().BeEmpty();
        stream.ReadByte().Should().Be(-1);

        using (var writer = stream.Rewind().ToXmlWriter(true))
        {
          writer.Settings.CloseOutput.Should().BeTrue();
          writer.Settings.Encoding.ToString().Should().Be(Encoding.UTF8.ToString());
        }
        AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
      }

      xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";
      using (var stream = new MemoryStream())
      {
        using (var writer = stream.ToXmlWriter(encoding: Encoding.Unicode))
        {
          writer.Settings.CloseOutput.Should().BeFalse();
          writer.Settings.Encoding.ToString().Should().Be(Encoding.Unicode.ToString());
          writer.WriteElementString("article", "text");
        }
        stream.ToArray().Should().Equal(xml.Bytes(Encoding.Unicode));
        stream.Bytes().Should().BeEmpty();
        stream.ReadByte().Should().Be(-1);

        using (var writer = stream.ToXmlWriter(true, Encoding.Unicode))
        {
          writer.Settings.CloseOutput.Should().BeTrue();
          writer.Settings.Encoding.ToString().Should().Be(Encoding.Unicode.ToString());
        }
        AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
      }*/
    }

    // TODO Encoding support

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, Encoding encoding = null)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlDictionaryWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, Encoding encoding = null)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlDocument(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      using (var stream = new MemoryStream(Xml.ToBytes(Encoding.UTF32)))
      {
        AssertionExtensions.Should(() => stream.ToXmlDocument()).ThrowExactly<XmlException>();
      }

      using (var stream = new MemoryStream(Xml.ToBytes(Encoding.Unicode)))
      {
        stream.ToXmlDocument().ToText().Should().Be(Xml);
        stream.ToArray().Should().BeEmpty();
        stream.ReadByte().Should().Be(-1);
      }

      using (var stream = new MemoryStream(Xml.ToBytes(Encoding.Unicode)))
      {
        stream.ToXmlDocument().ToText().Should().Be(Xml);
        AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
      }*/

      // TODO Encoding support
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXDocument(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXDocumentAsync(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
      AssertionExtensions.Should(() => Stream.Null.ToXDocumentAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

      /*const string Xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><article>text</article>";

      using (var stream = new MemoryStream(Xml.ToBytes(Encoding.UTF32)))
      {
        AssertionExtensions.Should(() => stream.ToXDocumentAsync()).ThrowExactlyAsync<XmlException>().Await();
      }

      using (var stream = new MemoryStream(Xml.ToBytes(Encoding.Unicode)))
      {
        stream.ToXDocumentAsync().ToString().Should().Be("<article>text</article>");
        stream.ToArray().Should().BeEmpty();
        stream.ReadByte().Should().Be(-1);
      }

      using (var stream = new MemoryStream(Xml.ToBytes(Encoding.Unicode)))
      {
        stream.ToXDocumentAsync().ToString().Should().Be("<article>text</article>");
        stream.ReadByte().Should().Be(-1);
      }*/

      // TODO Encoding support
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DeserializeAsDataContract{T}(Stream, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, params Type[] types)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DeserializeAsXml{T}(Stream, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      /*var subject = Guid.Empty;

      using (var stream = new MemoryStream())
      {
        subject.AsXml(stream, Encoding.Unicode);
        stream.Rewind().AsXml<Guid>().Should().Be(subject);
        stream.CanWrite.Should().BeTrue();
      }

      using (var stream = new MemoryStream())
      {
        subject.AsXml(stream, Encoding.Unicode);
        stream.Rewind().AsXml<Guid>(true).Should().Be(subject);
        stream.CanWrite.Should().BeFalse();
      }*/

      // TODO Encoding support
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, params Type[] types)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Hash(Stream, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Hash_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Stream.Null.Hash(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

      using var algorithm = MD5.Create();

      AssertionExtensions.Should(() => ((Stream) null).Hash(Attributes.HashAlgorithm())).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      
      algorithm.Should().BeOfType<MD5>();

      new[] { Stream.Null, Attributes.RandomStream() }.ForEach(stream =>
      {
        using (stream)
        {
          var hash = stream.Hash(Attributes.HashAlgorithm());
          hash.Should().BeOfType<byte[]>().And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
        }
      });
    }

    throw new NotImplementedException();

    return;

    static void Validate(Stream stream, HashAlgorithm algorithm)
    {
      using (stream)
      {
        using (algorithm)
        {

        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashAsync(Stream, HashAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Stream.Null.HashAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();

      using var algorithm = MD5.Create();

      AssertionExtensions.Should(() => ((Stream) null).HashAsync(Attributes.HashAlgorithm())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

      algorithm.Should().BeOfType<MD5>();

      new[] { Stream.Null, Attributes.RandomStream() }.ForEach(stream =>
      {
        using (stream)
        {
          AssertionExtensions.Should(() => stream.HashAsync(algorithm, Attributes.CancellationToken())).ThrowExactlyAsync<TaskCanceledException>().Await();

          var task = stream.HashAsync(Attributes.HashAlgorithm());
          task.Should().BeAssignableTo<Task<byte>[]>();
          task.Await().Should().BeOfType<byte[]>().And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
        }
      });

      throw new NotImplementedException();
    }

    return;

    static void Validate(Stream stream, HashAlgorithm algorithm)
    {
      using (stream)
      {
        using (algorithm)
        {

        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashMd5(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashMd5_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashMd5()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = MD5.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        stream.MoveToStart().HashMd5().Should().BeOfType<byte[]>().And.HaveCount(16).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashMd5Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashMd5Async_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashMd5Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
      AssertionExtensions.Should(() => Stream.Null.HashMd5Async(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>();

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = MD5.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        var task = stream.MoveToStart().HashMd5Async();
        task.Should().BeAssignableTo<Task<byte[]>>();
        task.Await().Should().BeOfType<byte[]>().And.HaveCount(16).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha1(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha1_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashSha1()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = SHA1.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        stream.MoveToStart().HashSha1().Should().BeOfType<byte[]>().And.HaveCount(20).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha1Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha1Async_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashSha1Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
      AssertionExtensions.Should(() => Stream.Null.HashSha1Async(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>();

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = SHA1.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        var task = stream.MoveToStart().HashSha1Async();
        task.Should().BeAssignableTo<Task<byte[]>>();
        task.Await().Should().BeOfType<byte[]>().And.HaveCount(20).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha256(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha256_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashSha256()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = SHA256.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        stream.MoveToStart().HashSha256().Should().BeOfType<byte[]>().And.HaveCount(32).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha256Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha256Async_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashSha256Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
      AssertionExtensions.Should(() => Stream.Null.HashSha256Async(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>();

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = SHA256.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        var task = stream.MoveToStart().HashSha256Async();
        task.Should().BeAssignableTo<Task<byte>>();
        task.Await().Should().BeOfType<byte[]>().And.HaveCount(32).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha384(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha384_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashSha384()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = SHA384.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        stream.MoveToStart().HashSha384().Should().BeOfType<byte[]>().And.HaveCount(48).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha384Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha384Async_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashSha384Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
      AssertionExtensions.Should(() => Stream.Null.HashSha384Async(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>();

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = SHA384.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        var task = stream.MoveToStart().HashSha384Async();
        task.Should().BeAssignableTo<Task<byte[]>>();
        task.Await().Should().BeOfType<byte[]>().And.HaveCount(48).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha512(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha512_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashSha512()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = SHA512.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        stream.MoveToStart().HashSha512().Should().BeOfType<byte[]>().And.HaveCount(64).And.Equal(bytes);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha512Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha512Async_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).HashSha512Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
      AssertionExtensions.Should(() => Stream.Null.HashSha512Async(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>();

      Validate(Stream.Null);
      Validate(Attributes.RandomStream());
    }

    return;

    static void Validate(Stream stream)
    {
      using (stream)
      {
        using var algorithm = SHA384.Create();
        var bytes = algorithm.ComputeHash(stream.MoveToStart());
        var task = stream.MoveToStart().HashSha512Async();
        task.Should().BeAssignableTo<Task<byte[]>>();
        task.Await().Should().BeOfType<byte[]>().And.HaveCount(64).And.Equal(bytes);
      }
    }
  }
}