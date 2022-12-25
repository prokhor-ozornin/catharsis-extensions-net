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
    //AssertionExtensions.Should(() => ((DriveInfo) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

    DriveInfo.GetDrives().Should().Contain(drive => !drive.IsEmpty());
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.IsEmpty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_IsEmpty_Method()
  {
    //AssertionExtensions.Should(() => ((DirectoryInfo) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

    var directory = RandomFakeDirectory;
    directory.Exists.Should().BeFalse();
    directory.IsEmpty().Should().BeTrue();

    directory = Environment.SystemDirectory.ToDirectory();
    directory.Exists.Should().BeTrue();
    directory.IsEmpty().Should().BeFalse();

    RandomDirectory.UseTemporarily(directory =>
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
    //AssertionExtensions.Should(() => ((FileInfo) null!).IsEmpty()).ThrowExactly<ArgumentNullException>();

    var file = RandomFakeFile;
    file.Exists.Should().BeFalse();
    file.IsEmpty().Should().BeTrue();

    var bytes = Randomizer.ByteSequence(1).ToArray();
    RandomEmptyFile.UseTemporarily(file =>
    {
      file.Exists.Should().BeTrue();
      file.Length.Should().Be(0);
      file.IsEmpty().Should().BeTrue();
      file.Write(bytes).Await();
      file.Length.Should().Be(bytes.Length);
      file.IsEmpty().Should().BeFalse();
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Empty(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Empty_Method()
  {
    //AssertionExtensions.Should(() => ((FileInfo) null!).Clear()).ThrowExactly<ArgumentNullException>();

    var file = RandomFakeFile;
    file.Exists.Should().BeFalse();
    file.Empty().Should().NotBeNull().And.BeSameAs(file);
    file.Exists.Should().BeTrue();
    file.Length.Should().Be(0);
    file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);

    RandomNonEmptyFile.UseTemporarily(file =>
    {
      file.Length.Should().BePositive();
      file.Empty().Should().NotBeNull().And.BeSameAs(file);

      file.Exists.Should().BeTrue();
      file.Length.Should().Be(0);
      file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Empty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Empty_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null!).Empty()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.UseTemporarily(FileInfo, Action{FileInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => ((FileInfo) null!).UseTemporarily(_ => {})).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => RandomNonExistingFile.UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    var bytes = RandomBytes;

    var file = RandomFakeFile;
    file.Exists.Should().BeFalse();
    file.UseTemporarily(file => file.Write(bytes).Await()).Should().NotBeNull().And.BeSameAs(file);
    file.Exists.Should().BeFalse();

    file = RandomEmptyFile;
    file.Exists.Should().BeTrue();
    file.UseTemporarily(file => file.Write(bytes).Await()).Should().NotBeNull().And.BeSameAs(file);
    file.Exists.Should().BeTrue();
    file.Length.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.UseTemporarily(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => ((DirectoryInfo) null!).UseTemporarily(_ => { })).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => RandomNonExistingDirectory.UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    var directory = RandomFakeDirectory;
    directory.Exists.Should().BeFalse();
    directory.UseTemporarily(directory => 
    {
      Randomizer.File(directory);
      Randomizer.File(Randomizer.Directory(directory));
    });
    directory.Exists.Should().BeFalse();

    directory = RandomDirectory;
    directory.Exists.Should().BeTrue();
    directory.UseTemporarily(directory =>
    {
      Randomizer.File(directory);
      Randomizer.File(Randomizer.Directory(directory));
    });
    directory.Exists.Should().BeTrue();
    directory.IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.CreateWithPath(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_CreateWithPath_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.CreateWithPath(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="FileSystemExtensions.Bytes(FileInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="FileSystemExtensions.Bytes(FileInfo, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void FileInfo_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileSystemExtensions.Bytes(null!)).ThrowExactly<ArgumentNullException>();

      var bytes = RandomBytes;

      RandomEmptyFile.UseTemporarily(file =>
      {
        file.Write(bytes).Await();
        file.Bytes().ToArray().Await().Should().Equal(bytes);
      });

      // Cancellation & offset
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileSystemExtensions.Bytes(null!, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => RandomFakeFile.Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="FileSystemExtensions.Text(FileInfo, Encoding?)"/></description></item>
  ///     <item><description><see cref="FileSystemExtensions.Text(FileInfo, string, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void FileInfo_Text_Methods()
  {
    using (new AssertionScope())
    {
      void ValidateFile(Encoding? encoding)
      {
        var text = RandomString;

        RandomEmptyFile.UseTemporarily(file =>
        {
          file.Write(text, encoding).Await();
          file.Text(encoding).Should().Be(text);
        });
      }

      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => FileSystemExtensions.Text(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

        ValidateFile(null);
        Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateFile);

        // Cancellation & offset
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileSystemExtensions.Text(null!, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => RandomFakeFile.Text((string) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Lines(FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Lines_Method()
  {
    //AssertionExtensions.Should(() => FileSystemExtensions.Lines(null!)).ThrowExactly<ArgumentNullException>();

    void ValidateFile(Encoding? encoding)
    {
      RandomEmptyFile.UseTemporarily(file =>
      {
        var lines = file.Lines(encoding);
        lines.Should().NotBeNull().And.NotBeSameAs(file.Lines(encoding));
        lines.ToArray().Await().Should().BeEmpty();
      });

      RandomEmptyFile.UseTemporarily(file =>
      {
        var lines = Randomizer.LettersSequence(80, 1000).ToArray();
        file.Write(lines.Join(Environment.NewLine), encoding).Await();
        file.Lines(encoding).ToList().Await().Should().Equal(lines);
      });
    }

    using (new AssertionScope())
    {
      ValidateFile(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateFile);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Write(FileInfo, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Write_Enumerable_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Write(null!, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => RandomFakeFile.Write((IEnumerable<byte>) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Write(FileInfo, Stream, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Write_Stream_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Write(null!, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => RandomFakeFile.Write((Stream) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Write(FileInfo, string, Encoding?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Write_String_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Write(null!, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => RandomFakeFile.Write((string) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Write(FileInfo, Uri, CancellationToken, (string Name, object Value)[])"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Write_Uri_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Write(null!, "https://localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => RandomFakeFile.Write((Uri) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Print(FileInfo, object, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Print_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Print(null!, new object())).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Size(DriveInfo, string?, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DriveInfo_Size_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null!).Size()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Size(DirectoryInfo, string?, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Size_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null!).Size()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Directories(DriveInfo, string?, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DriveInfo_Directories_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null!).Directories()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Directories(DirectoryInfo, string?, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Directories_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null!).Directories()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Files(DirectoryInfo, string?, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Files_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Files(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToEnumerable(DirectoryInfo, string?, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToEnumerable(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToStream_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToStream(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStreamReader(FileInfo, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToStreamReader_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToStreamReader(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileSystemInfo_ToUri_Method()
  {
    //AssertionExtensions.Should(() => FileSystemExtensions.ToUri(null!)).ThrowExactly<ArgumentNullException>();

    foreach (var info in new FileSystemInfo[] {RandomFakeFile, RandomFakeDirectory})
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
}