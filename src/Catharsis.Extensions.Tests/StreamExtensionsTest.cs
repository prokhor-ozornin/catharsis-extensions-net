using System.Security.Cryptography;
using System.Text;
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
  ///   <para>Performs testing of <see cref="StreamExtensions.IsStart(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStart_Method()
  {
    void Validate(Stream stream)
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
  public void IsEnd_Method()
  {
    void Validate(Stream stream)
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
  public void IsEmpty_Method()
  {
    void Validate(Stream stream, bool empty)
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
  public void Empty_Method()
  {
    void Validate(Stream stream)
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
  public void Min_Method()
  {
    void Validate(Stream min, Stream max)
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
  public void Max_Method()
  {
    void Validate(Stream min, Stream max)
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
  public void MoveBy_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveBy<Stream>(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveTo{TStream}(TStream, long)"/> method.</para>
  /// </summary>
  [Fact]
  public void MoveTo_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveTo<Stream>(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveToStart{TStream}(TStream)"/> method.</para>
  /// </summary>
  [Fact]
  public void MoveToStart_Method()
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
  public void MoveToEnd_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveToStart<Stream>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Lines(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.LinesAsync(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void LinesAsync_Method()
  {
    void Validate(Encoding encoding)
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
  public void Skip_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).Skip(0)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    AssertionExtensions.Should(() => Stream.Null.Skip(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

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
  public void AsSynchronized_Method()
  {
    void Validate(Stream stream)
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
  public void AsReadOnly_Method()
  {
    void Validate(Stream stream)
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
  public void AsReadOnlyForward_Method()
  {
    void Validate(Stream stream)
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
  public void AsWriteOnly_Method()
  {
    void Validate(Stream stream)
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
  public void AsWriteOnlyForward_Method()
  {
    void Validate(Stream stream)
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
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.ToBytes(null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
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
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToTextAsync(Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      void Validate(Encoding encoding)
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
  public void WriteBytes_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.WriteBytes<Stream>(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Stream.Null.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteBytesAsync{TStream}(TStream, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
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
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.WriteText<Stream>(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Stream.Null.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteTextAsync{TStream}(TStream, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
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
  public void CompressAsBrotli_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsBrotli(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsDeflate(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompressAsDeflate_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsDeflate(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    /*var bytes = RandomBytes;

    var stream = new MemoryStream();
    byte[] compressed = [];
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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsGzip(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompressAsGzip_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsGzip(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    /*var bytes = RandomBytes;

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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsZlib(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompressAsZlib_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsZlib(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsBrotli(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecompressAsBrotli_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsBrotli(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsDeflate(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecompressAsDeflate_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsDeflate(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsGzip(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecompressAsGzip_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsGzip(null)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsZlib(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void DecompressAsZlib_Method()
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
  public void ToEnumerable_Methods()
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
  public void ToAsyncEnumerable_Methods()
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
  public void ToBufferedStream_Method()
  {
    void Validate(Stream stream, int? size)
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
  public void ToBinaryReader_Method()
  {
    void Validate(Encoding encoding)
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
  public void ToBinaryWriter_Method()
  {
    void Validate(Encoding encoding)
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
  public void ToStreamReader_Method()
  {
    void Validate(Encoding encoding)
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
  public void ToStreamWriter_Method()
  {
    void Validate(Encoding encoding)
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

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlReader(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlDictionaryReader(Stream, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
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

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlDictionaryWriter(Stream, Encoding, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXmlDocument(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXDocument(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToXDocumentAsync(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();
    AssertionExtensions.Should(() => Stream.Null.ToXDocumentAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DeserializeAsDataContract{T}(Stream, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DeserializeAsXml{T}(Stream, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Hash(Stream, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Hash_Method()
  {
    AssertionExtensions.Should(() => Stream.Null.Hash(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    using var algorithm = MD5.Create();

    AssertionExtensions.Should(() => ((Stream) null).Hash(algorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("stream");
    
    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.Hash(algorithm);
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().Hash(algorithm)).And.HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashAsync(Stream, HashAlgorithm, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashAsync_Method()
  {
    AssertionExtensions.Should(() => Stream.Null.HashAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("algorithm").Await();

    using var algorithm = MD5.Create();

    AssertionExtensions.Should(() => ((Stream) null).HashAsync(algorithm)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashAsync(algorithm, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        var hash = stream.HashAsync(algorithm);
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashAsync(algorithm));
        hash.Await().Should().HaveCount(algorithm.HashSize / 8).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashMd5(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashMd5_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashMd5()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = MD5.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashMd5();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashMd5()).And.HaveCount(16).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashMd5Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashMd5Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashMd5Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = MD5.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashMd5Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashMd5Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashMd5Async());
        hash.Await().Should().HaveCount(16).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha1(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha1_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha1()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = SHA1.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashSha1();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha1()).And.HaveCount(20).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha1Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha1Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha1Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = SHA1.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha1Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashSha1Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha1Async());
        hash.Await().Should().HaveCount(20).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha256(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha256_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha256()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = SHA256.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashSha256();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha256()).And.HaveCount(32).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha256Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha256Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha256Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = SHA256.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha256Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashSha256Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha256Async());
        hash.Await().Should().HaveCount(32).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha384(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha384_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha384()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = SHA384.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashSha384();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha384()).And.HaveCount(48).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha384Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha384Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha384Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = SHA384.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha384Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashSha384Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha384Async());
        hash.Await().Should().HaveCount(48).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha512(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha512_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha512()).ThrowExactly<ArgumentNullException>().WithParameterName("stream");

    using var algorithm = SHA512.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        var hash = stream.HashSha512();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha512()).And.HaveCount(64).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.HashSha512Async(Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_HashSha512Async_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null).HashSha512Async()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("stream").Await();

    using var algorithm = SHA512.Create();

    algorithm.Should().NotBeNull();

    foreach (var stream in new[] { Stream.Null, RandomStream })
    {
      using (stream)
      {
        AssertionExtensions.Should(() => stream.HashSha512Async(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

        var hash = stream.HashSha512Async();
        hash.Should().NotBeNull().And.NotBeSameAs(stream.MoveToStart().HashSha512Async());
        hash.Await().Should().HaveCount(64).And.Equal(algorithm.ComputeHash(stream.MoveToStart()));
      }
    }
  }
}