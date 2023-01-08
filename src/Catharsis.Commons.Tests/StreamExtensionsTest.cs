using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StreamExtensions"/>.</para>
/// </summary>
public sealed class StreamExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsStart(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_IsStart_Method()
  {
    static void Validate(Stream stream)
    {
      using (stream)
      {
        stream.MoveToStart();
        stream.IsStart().Should().BeTrue();
        stream.MoveToEnd();
        stream.IsStart().Should().Be(stream.Length == 0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsStart(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.IsStart()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.IsStart()).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null);
      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(WriteOnlyStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsEnd(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_IsEnd_Method()
  {
    static void Validate(Stream stream)
    {
      using (stream)
      {
        stream.MoveToStart();
        stream.IsEnd().Should().Be(stream.Length == 0);
        stream.MoveToEnd();
        stream.IsEnd().Should().BeTrue();
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsEnd(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.IsEnd()).ThrowExactly<ArgumentException>();

      Validate(Stream.Null);
      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(WriteOnlyStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.IsEmpty(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_IsEmpty_Method()
  {
    static void Validate(Stream stream, bool empty)
    {
      using (stream)
      {
        stream.IsEmpty().Should().Be(empty);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.IsEmpty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => WriteOnlyForwardStream.IsEmpty()).ThrowExactly<ArgumentException>();

      Validate(Stream.Null, true);
      Validate(EmptyStream, true);
      Validate(RandomStream, false);
      Validate(RandomReadOnlyStream, false);
      Validate(RandomReadOnlyForwardStream, false);
      Validate(WriteOnlyStream, true);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Empty{TStream}(TStream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Empty_Method()
  {
    static void Validate(Stream stream)
    {
      using (stream)
      {
        stream.Empty().Should().NotBeNull().And.BeSameAs(stream).And.HaveLength(0).And.HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Validate(RandomReadOnlyForwardStream)).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => Validate(WriteOnlyForwardStream)).ThrowExactly<NotSupportedException>();

      Validate(Stream.Null);
      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(WriteOnlyStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Min(Stream, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Min_Method()
  {
    static void Validate(Stream min, Stream max)
    {
      using (min)
      {
        using (max)
        {
          min.Min(min).Should().NotBeNull().And.BeSameAs(min);
          max.Min(max).Should().NotBeNull().And.BeSameAs(max);
          min.Min(max).Should().NotBeNull().And.BeSameAs(min);
        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.Min(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => Stream.Null.Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(Stream.Null, Stream.Null);
      Validate(Stream.Null, EmptyStream);
      Validate(Stream.Null, RandomStream);
      
      Validate(EmptyStream, Stream.Null);
      Validate(EmptyStream, EmptyStream);
      Validate(EmptyStream, RandomStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Max(Stream, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Max_Method()
  {
    static void Validate(Stream min, Stream max)
    {
      using (min)
      {
        using (max)
        {
          min.Max(min).Should().NotBeNull().And.BeSameAs(min);
          max.Max(max).Should().NotBeNull().And.BeSameAs(max);
          max.Max(min).Should().NotBeNull().And.BeSameAs(max);
        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.Max(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => Stream.Null.Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(Stream.Null, Stream.Null);
      Validate(Stream.Null, EmptyStream);
      Validate(Stream.Null, RandomStream);

      Validate(EmptyStream, Stream.Null);
      Validate(EmptyStream, EmptyStream);
      Validate(EmptyStream, RandomStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveBy{TStream}(TStream, long)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_MoveBy_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveBy<Stream>(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveTo{TStream}(TStream, long)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_MoveTo_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveTo<Stream>(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveToStart{TStream}(TStream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_MoveToStart_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveToStart<Stream>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    Stream.Null.MoveToStart().Should().BeSameAs(Stream.Null);

    var bytes = RandomBytes;
    var stream = new MemoryStream(bytes);
    stream.Position.Should().Be(0);
    stream.MoveToEnd();
    stream.Position.Should().Be(stream.Length);
    stream.MoveToStart();
    stream.Position.Should().Be(0);
    stream.MoveToStart();
    stream.Position.Should().Be(0);

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveToEnd{TStream}(TStream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_MoveToEnd_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveToStart<Stream>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Lines(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Lines_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.LinesAsync(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_LinesAsync_Method()
  {
    static void Validate(Encoding encoding)
    {

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.LinesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Skip{TStream}(TStream, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Skip_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).Skip(0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    AssertionExtensions.Should(() => Stream.Null.Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Print{T}(T, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.Print<object>(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => StreamExtensions.Print(new object(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.PrintAsync{T}(T, Stream, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_PrintAsync_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.PrintAsync<object>(null, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
    AssertionExtensions.Should(() => StreamExtensions.PrintAsync(new object(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => new object().PrintAsync(Stream.Null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.TryFinallyClear{TStream}(TStream, Action{TStream})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.TryFinallyClear<Stream>(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    AssertionExtensions.Should(() => Stream.Null.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsSynchronized(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsSynchronized_Method()
  {
    static void Validate(Stream stream)
    {
      using (var synchronized = stream.AsSynchronized())
      {
        AssertionExtensions.Should(() => synchronized.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => synchronized.WriteTimeout).ThrowExactly<InvalidOperationException>();

        synchronized.Should().NotBeNull().And.NotBeSameAs(stream);

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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsSynchronized(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(WriteOnlyStream);
      Validate(RandomReadOnlyForwardStream);
      Validate(WriteOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsReadOnly(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsReadOnly_Method()
  {
    static void Validate(Stream stream)
    {
      using (var readOnly = stream.AsReadOnly())
      {
        AssertionExtensions.Should(() => readOnly.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => readOnly.WriteTimeout).ThrowExactly<InvalidOperationException>();

        readOnly.Should().NotBeNull().And.NotBeSameAs(stream);

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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsReadOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => WriteOnlyStream.AsReadOnly()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.AsReadOnly()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(RandomReadOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsReadOnlyForward(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsReadOnlyForward_Method()
  {
    static void Validate(Stream stream)
    {
      using (var readOnly = stream.AsReadOnlyForward())
      {
        AssertionExtensions.Should(() => readOnly.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => readOnly.WriteTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => readOnly.Length).ThrowExactly<NotSupportedException>();
        AssertionExtensions.Should(() => readOnly.Position).ThrowExactly<NotSupportedException>();

        readOnly.Should().NotBeNull().And.NotBeSameAs(stream);

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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsReadOnlyForward(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => WriteOnlyStream.AsReadOnlyForward()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.AsReadOnlyForward()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(RandomReadOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsWriteOnly(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsWriteOnly_Method()
  {
    static void Validate(Stream stream)
    {
      using (var writeOnly = stream.AsWriteOnly())
      {
        AssertionExtensions.Should(() => writeOnly.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => writeOnly.WriteTimeout).ThrowExactly<InvalidOperationException>();

        writeOnly.Should().NotBeNull().And.NotBeSameAs(stream);

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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsWriteOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => RandomReadOnlyStream.AsWriteOnly()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.AsWriteOnly()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(WriteOnlyStream);
      Validate(WriteOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.AsWriteOnlyForward(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_AsWriteOnlyForward_Method()
  {
    static void Validate(Stream stream)
    {
      using (var writeOnly = stream.AsWriteOnlyForward())
      {
        AssertionExtensions.Should(() => writeOnly.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => writeOnly.WriteTimeout).ThrowExactly<InvalidOperationException>();

        writeOnly.Should().NotBeNull().And.NotBeSameAs(stream);

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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.AsWriteOnlyForward(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => RandomReadOnlyStream.AsWriteOnlyForward()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.AsWriteOnlyForward()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(WriteOnlyStream);
      Validate(WriteOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBytes(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToBytes_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.ToBytes(null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBytesAsync(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBytesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

      Stream.Null.ToBytesAsync().ToArray().Should().BeEmpty();

      var bytes = RandomBytes;

      var stream = new MemoryStream(bytes);
      stream.ToBytesAsync().ToArray().Should().Equal(bytes);
      stream.ReadByte().Should().Be(-1);
      stream.Close();
      //AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();

      stream = new MemoryStream(bytes);
      stream.ToBytesAsync().ToArray().Should().Equal(bytes);
      //AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToText(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToText_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToTextAsync(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      static void Validate(Encoding encoding)
      {
        //var bytes = RandomBytes;

        //var text = stream.Text(encoding);
        //stream.Rewind().ToTextReader(encoding).Text().Should().Be(text);
        //bytes.Text(encoding).Should().Be(text);
        //stream.ReadByte().Should().Be(-1);
        //stream.Rewind().Text(encoding).Should().Be(text);
        //AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
      }

      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => ((Stream) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

        Validate(null);
        Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteBytes{TStream}(TStream, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.WriteBytes<Stream>(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Stream.Null.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteBytesAsync{TStream}(TStream, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.WriteBytesAsync<Stream>(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Stream.Null.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Stream.Null.WriteBytesAsync(Enumerable.Empty<byte>(), Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteText{TStream}(TStream, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_WriteText_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.WriteText<Stream>(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Stream.Null.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteTextAsync{TStream}(TStream, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.WriteTextAsync<Stream>(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Stream.Null.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => Stream.Null.WriteTextAsync(string.Empty, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteTo(IEnumerable{byte}, Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => StreamExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteToAsync(IEnumerable{byte}, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => StreamExtensions.WriteToAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(Stream.Null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteTo(string, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => StreamExtensions.WriteTo(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteToAsync(string, Stream, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => StreamExtensions.WriteToAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Stream.Null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsBrotli(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_CompressAsBrotli_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsBrotli(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsDeflate(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_CompressAsDeflate_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsDeflate(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    /*var bytes = RandomBytes;

    var stream = new MemoryStream();
    var compressed = Array.Empty<byte>();
    using (var deflate = stream.Deflate(CompressionMode.Compress))
    {
      deflate.BaseStream.Should().BeSameAs(stream);
      AssertionExtensions.Should(() => deflate.ReadByte()).ThrowExactly<InvalidOperationException>();
      deflate.Write(bytes);
    }
    compressed = stream.ToArray();
    compressed.Should().NotEqual(bytes);
    AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();

    stream = new MemoryStream(compressed);
    var decompressed = Array.Empty<byte>();
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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsGzip(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_CompressAsGzip_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsGzip(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    /*var bytes = RandomBytes;

    var stream = new MemoryStream();
    var compressed = Array.Empty<byte>();
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
    var decompressed = Array.Empty<byte>();
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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsZlib(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_CompressAsZlib_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsZlib(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsBrotli(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecompressAsBrotli_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsBrotli(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsDeflate(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecompressAsDeflate_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsDeflate(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsGzip(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecompressAsGzip_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsGzip(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsZlib(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecompressAsZlib_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsZlib(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StreamExtensions.ToEnumerable(Stream, bool)"/></description></item>
  ///     <item><description><see cref="StreamExtensions.ToEnumerable(Stream, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Stream_ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToEnumerable(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Stream.Null.ToEnumerable(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
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
  public void Stream_ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToAsyncEnumerable(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToAsyncEnumerable(null, 1).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable(0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBufferedStream(Stream, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToBufferedStream_Method()
  {
    static void Validate(Stream stream, int? size)
    {
      using (var buffered = stream.ToBufferedStream(size))
      {
        AssertionExtensions.Should(() => buffered.ReadTimeout).ThrowExactly<InvalidOperationException>();
        AssertionExtensions.Should(() => buffered.WriteTimeout).ThrowExactly<InvalidOperationException>();

        buffered.Should().NotBeNull();

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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBufferedStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
      AssertionExtensions.Should(() => Stream.Null.ToBufferedStream(0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("bufferSize");

      foreach (var buffer in new int?[] {null, 1, 4096, 8192})
      {
        Validate(EmptyStream, buffer);
        Validate(RandomStream, buffer);
        Validate(RandomReadOnlyStream, buffer);
        Validate(WriteOnlyStream, buffer);
        Validate(RandomReadOnlyForwardStream, buffer);
        Validate(WriteOnlyForwardStream, buffer);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBinaryReader(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToBinaryReader_Method()
  {
    static void Validate(Encoding encoding)
    {
      using var stream = RandomStream;

      var bytes = stream.ToArray();

      using (var reader = stream.ToBinaryReader(encoding))
      {
        reader.BaseStream.Should().NotBeNull().And.BeSameAs(stream);
        reader.ReadBytes(bytes.Length).Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBinaryReader(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBinaryWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToBinaryWriter_Method()
  {
    static void Validate(Encoding encoding)
    {
      var bytes = RandomBytes;

      using var stream = new MemoryStream();

      using (var writer = stream.ToBinaryWriter(encoding))
      {
        writer.BaseStream.Should().BeSameAs(stream);
        writer.Write(bytes);
      }

      stream.ToArray().Should().Equal(bytes);

      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToBinaryWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToStreamReader(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToStreamReader_Method()
  {
    static void Validate(Encoding encoding)
    {
      using var stream = RandomStream;
      var bytes = stream.ToArray();
      using var reader = stream.ToStreamReader(encoding);

      reader.BaseStream.Should().BeSameAs(stream);
      reader.ToText().Should().Be(bytes.ToText(encoding));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToStreamReader(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToStreamWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToStreamWriter_Method()
  {
    static void Validate(Encoding encoding)
    {
      var text = RandomString;

      using var stream = new MemoryStream();

      using (var writer = stream.ToStreamWriter(encoding))
      {
        writer.BaseStream.Should().BeSameAs(stream);
        writer.Write(text);
      }

      stream.ToArray().Should().Equal(text.ToBytes(encoding));
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToStreamWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }
}