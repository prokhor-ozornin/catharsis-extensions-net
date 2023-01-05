using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="FileSystemExtensions"/>.</para>
/// </summary>
public sealed class FileSystemExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.IsEmpty(DriveInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DriveInfo_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    DriveInfo.GetDrives().Should().Contain(drive => !drive.IsEmpty());
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.IsEmpty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    var directory = RandomFakeDirectory;
    directory.Exists.Should().BeFalse();
    directory.IsEmpty().Should().BeTrue();

    directory = Environment.SystemDirectory.ToDirectory();
    directory.Exists.Should().BeTrue();
    directory.IsEmpty().Should().BeFalse();

    RandomDirectory.TryFinallyDelete(directory =>
    {
      directory.Exists.Should().BeTrue();
      directory.IsEmpty().Should().BeTrue();
      Randomizer.File(Randomizer.Directory(directory));
      Randomizer.File(directory);
      directory.IsEmpty().Should().BeFalse();
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.IsEmpty(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    var file = RandomFakeFile;
    file.Exists.Should().BeFalse();
    file.IsEmpty().Should().BeTrue();

    var bytes = Randomizer.ByteSequence(1).ToArray();
    RandomEmptyFile.TryFinallyDelete(file =>
    {
      file.Exists.Should().BeTrue();
      file.Length.Should().Be(0);
      file.IsEmpty().Should().BeTrue();
      bytes.WriteToAsync(file).Await();
      file.Length.Should().Be(bytes.Length);
      file.IsEmpty().Should().BeFalse();
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Empty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Empty_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Empty(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Empty_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    var file = RandomFakeFile;
    file.Exists.Should().BeFalse();
    file.Empty().Should().NotBeNull().And.BeSameAs(file);
    file.Exists.Should().BeTrue();
    file.Length.Should().Be(0);
    file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);

    RandomNonEmptyFile.TryFinallyDelete(file =>
    {
      file.Length.Should().BePositive();
      file.Empty().Should().NotBeNull().And.BeSameAs(file);

      file.Exists.Should().BeTrue();
      file.Length.Should().Be(0);
      file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.CreateWithPath(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_CreateWithPath_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.CreateWithPath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Lines(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Lines_Method()
  {
    static void Validate(Encoding encoding)
    {
      RandomEmptyFile.TryFinallyDelete(file =>
      {
        var lines = file.Lines(encoding);
        lines.Should().NotBeNull().And.BeSameAs(file.Lines(encoding)).And.BeEmpty();
      });

      RandomEmptyFile.TryFinallyDelete(file =>
      {
        var lines = Randomizer.LettersSequence(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteToAsync(file, encoding).Await();
        file.Lines(encoding).Should().NotBeNull().And.NotBeSameAs(file.Lines(encoding)).And.Equal(lines);
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileSystemExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.LinesAsync(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_LinesAsync_Method()
  {
    static void Validate(Encoding encoding)
    {
      RandomEmptyFile.TryFinallyDelete(file =>
      {
        var lines = file.LinesAsync(encoding);
        lines.Should().NotBeNull().And.NotBeSameAs(file.LinesAsync(encoding));
        lines.ToArray().Should().BeEmpty();
      });

      RandomEmptyFile.TryFinallyDelete(file =>
      {
        var lines = Randomizer.LettersSequence(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteToAsync(file, encoding).Await();
        file.LinesAsync(encoding).ToArray().Should().NotBeNull().And.Equal(lines);
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileSystemExtensions.LinesAsync(null).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentNullException>().WithParameterName("file");

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Print{T}(T, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Print<object>(null, RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => FileSystemExtensions.Print(new object(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.PrintAsync{T}(T, FileInfo, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_PrintAsync_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.PrintAsync<object>(null, RandomFakeFile)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
    AssertionExtensions.Should(() => FileSystemExtensions.PrintAsync(new object(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => new object().PrintAsync(RandomFakeFile, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.TryFinallyClear(FileInfo, Action{FileInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    AssertionExtensions.Should(() => RandomFakeFile.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.TryFinallyClear(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    AssertionExtensions.Should(() => RandomFakeDirectory.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.TryFinallyDelete(FileInfo, Action{FileInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_TryFinallyDelete_Method()
  {
    static void Validate(FileInfo file)
    {
      var bytes = RandomBytes;

      file.Exists.Should().BeFalse();
      file.TryFinallyDelete(file => bytes.WriteToAsync(file).Await()).Should().NotBeNull().And.BeSameAs(file);
      file.Exists.Should().BeFalse();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).TryFinallyDelete(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("file");
      AssertionExtensions.Should(() => RandomFakeFile.TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(RandomFakeFile);
      Validate(RandomEmptyFile);
      Validate(RandomNonEmptyFile);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.TryFinallyDelete(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_TryFinallyDelete_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).TryFinallyDelete(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    AssertionExtensions.Should(() => RandomFakeDirectory.TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    var directory = RandomFakeDirectory;
    directory.Exists.Should().BeFalse();
    directory.TryFinallyDelete(directory =>
    {
      Randomizer.File(directory);
      Randomizer.File(Randomizer.Directory(directory));
    });
    directory.Exists.Should().BeFalse();

    directory = RandomDirectory;
    directory.Exists.Should().BeTrue();
    directory.TryFinallyDelete(directory =>
    {
      Randomizer.File(directory);
      Randomizer.File(Randomizer.Directory(directory));
    });
    directory.Exists.Should().BeTrue();
    directory.IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Files(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Files_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Files(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Directories(DriveInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DriveInfo_Directories_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).Directories()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Directories(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Directories_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Directories()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Size(DriveInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DriveInfo_Size_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).Size()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Size(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Size_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Size()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToUri(FileSystemInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileSystemInfo_ToUri_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToUri(null)).ThrowExactly<ArgumentNullException>().WithParameterName("entry");

    foreach (var info in new FileSystemInfo[] { RandomFakeFile, RandomFakeDirectory })
    {
      var uri = info.ToUri();
      uri.Should().NotBeNull().And.NotBeSameAs(info.ToUri());
      uri.IsAbsoluteUri.Should().BeTrue();
      uri.OriginalString.Should().Be(info.FullName);
      uri.AbsolutePath.ToPath().Should().Be(info.FullName);
      uri.AbsoluteUri.ToPath().Should().Be(info.FullName);
      uri.Authority.Should().BeEmpty();
      uri.Fragment.Should().BeEmpty();
      uri.Host.Should().BeEmpty();
      uri.IsDefaultPort.Should().BeTrue();
      uri.IsFile.Should().BeTrue();
      uri.IsLoopback.Should().BeTrue();
      uri.IsUnc.Should().BeFalse();
      uri.LocalPath.Should().Be(info.FullName);
      uri.PathAndQuery.ToPath().Should().Be(info.FullName);
      uri.Port.Should().Be(-1);
      uri.Query.Should().BeEmpty();
      uri.Scheme.Should().Be(Uri.UriSchemeFile);
      uri.UserEscaped.Should().BeFalse();
      uri.UserInfo.Should().BeEmpty();
      uri.ToString().ToPath().Should().Be(info.FullName);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToEnumerable(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToStream_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToReadOnlyStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToReadOnlyStream_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToReadOnlyStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToWriteOnlyStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToWriteOnlyStream_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToWriteOnlyStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStreamReader(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToStreamReader_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToStreamReader(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStreamWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToStreamWriter_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToStreamWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToBytes(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToBytes_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    //throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToBytesAsync(FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToBytesAsync(null).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentNullException>().WithParameterName("file");
    AssertionExtensions.Should(() => RandomEmptyFile.ToBytesAsync(Cancellation).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<TaskCanceledException>();

    var bytes = RandomBytes;

    RandomEmptyFile.TryFinallyDelete(file =>
    {
      bytes.WriteToAsync(file).Await();
      file.ToBytesAsync().ToArray().Should().Equal(bytes);
    });

    // Cancellation & offset

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToText(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToText_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToTextAsync(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToTextAsync_Method()
  {
    void ValidateFile(Encoding encoding)
    {
      var text = RandomString;

      RandomEmptyFile.TryFinallyDelete(file =>
      {
        text.WriteToAsync(file, encoding).Await();
        file.ToTextAsync(encoding).Await().Should().Be(text);
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileSystemExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();

      ValidateFile(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateFile);

      // Cancellation & offset
    }

    //throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteBytes(FileInfo, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => RandomFakeFile.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteBytesAsync(FileInfo, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => RandomFakeFile.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => RandomFakeFile.WriteBytesAsync(Enumerable.Empty<byte>(), Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteText(FileInfo, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_WriteText_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => RandomFakeFile.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteTextAsync(FileInfo, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => RandomFakeFile.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => RandomFakeFile.WriteTextAsync(string.Empty, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteTo(IEnumerable{byte}, FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => FileSystemExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteToAsync(IEnumerable{byte}, FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(RandomFakeFile)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => FileSystemExtensions.WriteToAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(RandomFakeFile, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteTo(string, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(RandomFakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => FileSystemExtensions.WriteTo(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteToAsync(string, FileInfo, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(RandomFakeFile)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => FileSystemExtensions.WriteToAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(RandomFakeFile, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }
}