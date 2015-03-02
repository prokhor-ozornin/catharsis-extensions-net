using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="FileSystemExtensions"/>.</para>
  /// </summary>
  public sealed class FileSystemExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="FileSystemExtensions.Append(FileInfo, byte[])"/></description></item>
    ///     <item><description><see cref="FileSystemExtensions.Append(FileInfo, string, Encoding)"/></description></item>
    ///     <item><description><see cref="FileSystemExtensions.Append(FileInfo, Stream)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Append_Methods()
    {
      var fileInfo = new FileInfo(Path.GetTempFileName());
      Assert.Throws<ArgumentNullException>(() => FileSystemExtensions.Append(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => fileInfo.Append((byte[]) null));
      Assert.Throws<ArgumentNullException>(() => FileSystemExtensions.Append(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => fileInfo.Append((string)null));
      Assert.Throws<ArgumentNullException>(() => FileSystemExtensions.Append(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => fileInfo.Append((Stream)null));

      var bytes = Guid.NewGuid().ToByteArray();
      WithFile(file =>
      {
        Assert.True(ReferenceEquals(file.Append(Enumerable.Empty<byte>().ToArray()), file));
        Assert.False(file.Exists);
      });
      WithFile(file =>
      {
        Assert.True(ReferenceEquals(file.Append(bytes), file));
        Assert.True(file.Exists);
        Assert.True(file.Bytes().SequenceEqual(bytes));
      });
      
      var text = Guid.NewGuid().ToString();
      WithFile(file =>
      {
        Assert.True(ReferenceEquals(file.Append(string.Empty), file));
        Assert.False(file.Exists);
      });
      WithFile(file =>
      {
        Assert.True(ReferenceEquals(file.Append(text), file));
        Assert.True(file.Exists);
        Assert.Equal(text, file.Text());
      });
      WithFile(file =>
      {
        Assert.True(ReferenceEquals(file.Append(text, Encoding.Unicode), file));
        Assert.True(file.Exists);
        Assert.Equal(text, file.Text(Encoding.Unicode));
      });

      WithFile(file =>
      {
        Assert.True(ReferenceEquals(file.Append(Stream.Null), file));
        Assert.True(file.Exists);
        Assert.Equal(0, file.Length);
      });
      WithFile(file => new MemoryStream(bytes).Do(stream =>
      {
        Assert.True(ReferenceEquals(file.Append(stream), file));
        Assert.True(file.Bytes().SequenceEqual(bytes));
      }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileSystemExtensions.Bytes(FileInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileSystemExtensions.Bytes(null));

      WithFile(file =>
      {
        var bytes = Guid.NewGuid().ToByteArray();
        Assert.True(file.Append(bytes).Bytes().SequenceEqual(bytes));
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileSystemExtensions.Clear(FileInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void Clear_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileSystemExtensions.Clear(null));

      WithFile(file =>
      {
        Assert.False(file.Exists);
        Assert.True(ReferenceEquals(file.Clear(), file));
        Assert.True(file.Exists);
        Assert.Equal(0, file.Length);
      });
      WithFile(file =>
      {
        file.Append(Guid.NewGuid().ToByteArray());
        Assert.True(file.Length > 0);
        Assert.Equal(0, file.Clear().Length);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileSystemExtensions.Lines(FileInfo, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void Lines_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileSystemExtensions.Lines(null));

      WithFile(file => Assert.False(file.Clear().Lines().Any()));

      var text = "First{0}Second{0}Third{0}".FormatSelf(Environment.NewLine);
      WithFile(file =>
      {
        var lines = file.Append(text).Lines();
        Assert.Equal(4, lines.Count);
        Assert.Equal("First", lines[0]);
        Assert.Equal("Second", lines[1]);
        Assert.Equal("Third", lines[2]);
        Assert.Equal(string.Empty, lines[3]);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileSystemExtensions.Text(FileInfo, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void Text_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileSystemExtensions.Text(null));

      var text = Guid.NewGuid().ToString();
      WithFile(file =>
      {
        file.Append(text);
        Assert.Equal(text, file.Text());
      });
      WithFile(file =>
      {
        file.Append(text, Encoding.Unicode);
        Assert.Equal(text, file.Text(Encoding.Unicode));
      });
    }

    private static void WithFile(Action<FileInfo> action)
    {
      Assertion.NotNull(action);

      var file = new FileInfo(Path.GetRandomFileName());
      try
      {
        action(file);
      }
      finally
      {
        file.Delete();
      }
    }
  }
}