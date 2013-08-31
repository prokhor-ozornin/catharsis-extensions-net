﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="FileInfoExtensions"/>.</para>
  /// </summary>
  public sealed class FileInfoExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="FileInfoExtensions.Append(FileInfo, byte[])"/></description></item>
    ///     <item><description><see cref="FileInfoExtensions.Append(FileInfo, string, Encoding)"/></description></item>
    ///     <item><description><see cref="FileInfoExtensions.Append(FileInfo, Stream)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Append_Methods()
    {
      var fileInfo = new FileInfo(Path.GetTempFileName());
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Append(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Append(fileInfo, (byte[]) null));
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Append(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Append(fileInfo, (string)null));
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Append(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Append(fileInfo, (Stream)null));

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
        Assert.True(file.Text() == text);
      });
      WithFile(file =>
      {
        Assert.True(ReferenceEquals(file.Append(text, Encoding.Unicode), file));
        Assert.True(file.Exists);
        Assert.True(file.Text(Encoding.Unicode) == text);
      });

      WithFile(file =>
      {
        Assert.True(ReferenceEquals(file.Append(Stream.Null), file));
        Assert.True(file.Exists);
        Assert.True(file.Length == 0);
      });
      WithFile(file => new MemoryStream(bytes).With(stream =>
      {
        Assert.True(ReferenceEquals(file.Append(stream), file));
        Assert.True(file.Bytes().SequenceEqual(bytes));
      }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileInfoExtensions.Bytes(FileInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Bytes(null));

      WithFile(file =>
      {
        var bytes = Guid.NewGuid().ToByteArray();
        Assert.True(file.Append(bytes).Bytes().SequenceEqual(bytes));
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileInfoExtensions.Clear(FileInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void Clear_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Clear(null));

      WithFile(file =>
      {
        Assert.False(file.Exists);
        Assert.True(ReferenceEquals(file.Clear(), file));
        Assert.True(file.Exists);
        Assert.True(file.Length == 0);
      });
      WithFile(file =>
      {
        file.Append(Guid.NewGuid().ToByteArray());
        Assert.True(file.Length > 0);
        Assert.True(file.Clear().Length == 0);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileInfoExtensions.Lines(FileInfo, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void Lines_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Lines(null));

      WithFile(file => Assert.True(file.Clear().Lines().Count == 0));

      var text = "First{0}Second{0}Third{0}".FormatValue(Environment.NewLine);
      WithFile(file =>
      {
        var lines = file.Append(text).Lines();
        Assert.True(lines.Count == 3);
        Assert.True(lines[0] == "First");
        Assert.True(lines[1] == "Second");
        Assert.True(lines[2] == "Third");
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileInfoExtensions.Text(FileInfo, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void Text_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileInfoExtensions.Text(null));

      var text = Guid.NewGuid().ToString();
      WithFile(file =>
      {
        file.Append(text);
        Assert.True(file.Text() == text);
      });
      WithFile(file =>
      {
        file.Append(text, Encoding.Unicode);
        Assert.True(file.Text(Encoding.Unicode) == text);
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