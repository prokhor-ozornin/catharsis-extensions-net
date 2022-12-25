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
    void Validate(Stream stream)
    {
      using (stream)
      {
        stream.MoveToStart();
        stream.IsStart().Should().BeTrue();
        stream.MoveToEnd();
        stream.IsStart().Should().Be(stream.Length <= 0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => StreamExtensions.IsStart(null!)).ThrowExactly<ArgumentNullException>();
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
    void Validate(Stream stream)
    {
      using (stream)
      {
        stream.MoveToStart();
        stream.IsEnd().Should().Be(stream.Length <= 0);
        stream.MoveToEnd();
        stream.IsEnd().Should().BeTrue();
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => StreamExtensions.IsEnd(null!)).ThrowExactly<ArgumentNullException>();
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
    void Validate(Stream stream, bool empty)
    {
      using (stream)
      {
        stream.IsEmpty().Should().Be(empty);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => StreamExtensions.IsEmpty(null!)).ThrowExactly<ArgumentNullException>();
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
    void Validate(Stream stream)
    {
      using (stream)
      {
        stream.Empty().Should().NotBeNull().And.BeSameAs(stream).And.HaveLength(0).And.HavePosition(0);
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((Stream) null!).Empty()).ThrowExactly<ArgumentNullException>();
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
      //AssertionExtensions.Should(() => StreamExtensions.Min(null!, Stream.Null)).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => Stream.Null.Min(null!)).ThrowExactly<ArgumentNullException>();

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
      //AssertionExtensions.Should(() => StreamExtensions.Max(null!, Stream.Null)).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => Stream.Null.Max(null!)).ThrowExactly<ArgumentNullException>();

      Validate(Stream.Null, Stream.Null);
      Validate(Stream.Null, EmptyStream);
      Validate(Stream.Null, RandomStream);

      Validate(EmptyStream, Stream.Null);
      Validate(EmptyStream, EmptyStream);
      Validate(EmptyStream, RandomStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Buffered(Stream, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Buffered_Method()
  {
    void Validate(Stream stream, int? size = null)
    {
      using (var buffered = stream.Buffered(size))
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
      AssertionExtensions.Should(() => StreamExtensions.Buffered(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Stream.Null.Buffered(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();
      AssertionExtensions.Should(() => Stream.Null.Buffered(0)).ThrowExactly<ArgumentOutOfRangeException>();

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
  ///   <para>Performs testing of <see cref="StreamExtensions.Synchronized(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Synchronized_Method()
  {
    void Validate(Stream stream)
    {
      using (var synchronized = stream.Synchronized())
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
      AssertionExtensions.Should(() => StreamExtensions.Synchronized(null!)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(WriteOnlyStream);
      Validate(RandomReadOnlyForwardStream);
      Validate(WriteOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ReadOnly(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ReadOnly_Method()
  {
    void Validate(Stream stream)
    {
      using (var readOnly = stream.ReadOnly())
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
      //AssertionExtensions.Should(() => StreamExtensions.ReadOnly(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyStream.ReadOnly()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ReadOnly()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(RandomReadOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ReadOnlyForward(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ReadOnlyForward_Method()
  {
    void Validate(Stream stream)
    {
      using (var readOnly = stream.ReadOnlyForward())
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
      //AssertionExtensions.Should(() => StreamExtensions.ReadOnlyForward(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => WriteOnlyStream.ReadOnlyForward()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => WriteOnlyForwardStream.ReadOnlyForward()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(RandomReadOnlyStream);
      Validate(RandomReadOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteOnly(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_WriteOnly_Method()
  {
    void Validate(Stream stream)
    {
      using (var writeOnly = stream.WriteOnly())
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
      //AssertionExtensions.Should(() => StreamExtensions.WriteOnly(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomReadOnlyStream.WriteOnly()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.WriteOnly()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(WriteOnlyStream);
      Validate(WriteOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.WriteOnlyForward(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_WriteOnlyForward_Method()
  {
    void Validate(Stream stream)
    {
      using (var writeOnly = stream.WriteOnlyForward())
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
      //AssertionExtensions.Should(() => StreamExtensions.WriteOnly(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomReadOnlyStream.WriteOnlyForward()).ThrowExactly<NotSupportedException>();
      AssertionExtensions.Should(() => RandomReadOnlyForwardStream.WriteOnlyForward()).ThrowExactly<NotSupportedException>();

      Validate(EmptyStream);
      Validate(RandomStream);
      Validate(WriteOnlyStream);
      Validate(WriteOnlyForwardStream);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StreamExtensions.Bytes(Stream, CancellationToken)"/></description></item>
  ///     <item><description><see cref="StreamExtensions.Bytes{TStream}(TStream, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Stream_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.Bytes(null!)).ThrowExactly<ArgumentNullException>();

      Stream.Null.Bytes().ToArray().Await().Should().BeEmpty();

      var bytes = RandomBytes;

      var stream = new MemoryStream(bytes);
      stream.Bytes().ToArray().Await().Should().Equal(bytes);
      stream.ReadByte().Should().Be(-1);
      stream.Close();
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();

      stream = new MemoryStream(bytes);
      stream.Bytes().ToArray().Await().Should().Equal(bytes);
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null!).Bytes(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StreamExtensions.Text(Stream, Encoding?)"/></description></item>
  ///     <item><description><see cref="StreamExtensions.Text{TStream}(TStream, string, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Stream_Text_Methods()
  {
    using (new AssertionScope())
    {
      void Validate(Encoding? encoding = null)
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
        AssertionExtensions.Should(() => ((Stream) null!).Text()).ThrowExactlyAsync<ArgumentNullException>().Await();

        Validate();
        Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Stream) null!).Text(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Text((string) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Lines(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Lines_Method()
  {
    void Validate(Encoding? encoding = null)
    {

    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => StreamExtensions.Lines(null!)).ThrowExactly<ArgumentNullException>();

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Print{TStream}(TStream, object, Encoding?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Print_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).Print(new object())).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveBy{TStream}(TStream, long)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_MoveBy_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveBy<Stream>(null!, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveTo{TStream}(TStream, long)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_MoveTo_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.MoveTo<Stream>(null!, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.MoveToStart{TStream}(TStream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_MoveToStart_Method()
  {



    AssertionExtensions.Should(() => StreamExtensions.MoveToStart<Stream>(null!)).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => StreamExtensions.MoveToStart<Stream>(null!)).ThrowExactly<ArgumentNullException>(); 

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Write{TStream}(TStream, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Write_Enumerable_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).Write(Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.Write((IEnumerable<byte>) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Write{TStream}(TStream, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Write_Stream_Method()
  {
    /*AssertionExtensions.Should(() => ((Stream) null!).Write(Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.Write((Stream) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    var bytes = RandomBytes;
    var text = bytes.ToString();

    var stream = new MemoryStream();
    stream.Write<MemoryStream>(bytes).Should().BeSameAs(stream);
    stream.Close();
    stream.ToArray().Should().Equal(bytes);

    var from = new MemoryStream(bytes);
    var to = new MemoryStream();
    to.Write(from).Should().BeSameAs(to);
    to.ToArray().Should().Equal(bytes);
    from.Bytes().ToArray().Await().Should().BeEmpty();
    from.CanRead.Should().BeTrue();
    to.Bytes().ToArray().Await().Should().BeEmpty();
    to.CanWrite.Should().BeTrue();
    from.Close();
    to.Close();

    using (var s = new MemoryStream())
    {
      s.Write(string.Empty).Should().BeSameAs(s);
      s.Text().Await().Should().BeEmpty();
    }

    using (var s = new MemoryStream())
    {
      s.Write(text).Should().BeSameAs(s);
      s.Rewind().Text().Should().Be(text);
    }

    using (var s = new MemoryStream())
    {
      s.Write(text, Encoding.Unicode).Should().BeSameAs(s);
      s.Rewind().Text(encoding: Encoding.Unicode).Should().Be(text);
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Write{TStream}(TStream, FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Write_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).Write(RandomFakeFile)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.Write((FileInfo) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Write{TStream}(TStream, Uri, IEnumerable{(string Name, object Value)}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Write_Uri_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).Write("https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.Write((Uri) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Write{TStream}(TStream, string, Encoding?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Write_Text_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).Write(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Stream.Null.Write((string) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.Skip{TStream}(TStream, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Skip_Method()
  {
    AssertionExtensions.Should(() => ((Stream) null!).Skip(0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsBrotli(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_CompressAsBrotli_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.CompressAsBrotli(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.CompressAsDeflate(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_CompressAsDeflate_Method()
  {
    /*AssertionExtensions.Should(() => StreamExtensions.CompressAsDeflate(null!)).ThrowExactly<ArgumentNullException>();

    var bytes = RandomBytes;

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
    /*AssertionExtensions.Should(() => StreamExtensions.CompressAsGzip(null!)).ThrowExactly<ArgumentNullException>();

    var bytes = RandomBytes;

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
    AssertionExtensions.Should(() => StreamExtensions.CompressAsZlib(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsBrotli(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecompressAsBrotli_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsBrotli(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsDeflate(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecompressAsDeflate_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsDeflate(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsGzip(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecompressAsGzip_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsGzip(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.DecompressAsZlib(Stream)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_DecompressAsZlib_Method()
  {
    AssertionExtensions.Should(() => StreamExtensions.DecompressAsZlib(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StreamExtensions.ToEnumerable(Stream)"/></description></item>
  ///     <item><description><see cref="StreamExtensions.ToEnumerable(Stream, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Stream_ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToEnumerable(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToEnumerable(null!, 1)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StreamExtensions.ToAsyncEnumerable(Stream)"/></description></item>
  ///     <item><description><see cref="StreamExtensions.ToAsyncEnumerable(Stream, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Stream_ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToEnumerable(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToEnumerable(null!, 1)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBinaryReader(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToBinaryReader_Method()
  {
    void Validate(Encoding? encoding = null)
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
      AssertionExtensions.Should(() => StreamExtensions.ToBinaryReader(null!)).ThrowExactly<ArgumentNullException>();

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToBinaryWriter(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToBinaryWriter_Method()
  {
    void Validate(Encoding? encoding = null)
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
      AssertionExtensions.Should(() => StreamExtensions.ToBinaryWriter(null!)).ThrowExactly<ArgumentNullException>();

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToStreamReader(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToStreamReader_Method()
  {
    void Validate(Encoding? encoding = null)
    {
      using var stream = RandomStream;
      var bytes = stream.ToArray();
      using (var reader = stream.ToStreamReader(encoding))
      {
        reader.BaseStream.Should().BeSameAs(stream);
        reader.Text().Should().Be(bytes.Text(encoding));
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToStreamReader(null!)).ThrowExactly<ArgumentNullException>();

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StreamExtensions.ToStreamWriter(Stream, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_ToStreamWriter_Method()
  {
    void Validate(Encoding? encoding = null)
    {
      var text = RandomString;

      using var stream = new MemoryStream();

      using (var writer = stream.ToStreamWriter(encoding))
      {
        writer.BaseStream.Should().BeSameAs(stream);
        writer.Write(text);
      }

      stream.ToArray().Should().Equal(text.Bytes(encoding));
      AssertionExtensions.Should(() => stream.ReadByte()).ThrowExactly<ObjectDisposedException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StreamExtensions.ToStreamWriter(null!)).ThrowExactly<ArgumentNullException>();

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    throw new NotImplementedException();
  }
}