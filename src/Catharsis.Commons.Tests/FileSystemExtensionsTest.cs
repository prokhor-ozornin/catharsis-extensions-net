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
    AssertionExtensions.Should(() => ((DriveInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

    DriveInfo.GetDrives().Should().Contain(drive => !drive.IsEmpty());
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.IsEmpty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => ((FileInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>();

    var file = RandomFakeFile;
    file.Exists.Should().BeFalse();
    file.IsEmpty().Should().BeTrue();

    var bytes = Randomizer.ByteSequence(1).ToArray();
    RandomEmptyFile.TryFinallyDelete(file =>
    {
      file.Exists.Should().BeTrue();
      file.Length.Should().Be(0);
      file.IsEmpty().Should().BeTrue();
      bytes.WriteTo(file).Await();
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
    AssertionExtensions.Should(() => ((FileInfo) null).Empty()).ThrowExactly<ArgumentNullException>();

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
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Empty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Empty_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Empty()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.CreateWithPath(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_CreateWithPath_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.CreateWithPath(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Lines(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_Lines_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Lines(null)).ThrowExactly<ArgumentNullException>();

    void ValidateFile(Encoding encoding)
    {
      RandomEmptyFile.TryFinallyDelete(file =>
      {
        var lines = file.Lines(encoding);
        lines.Should().NotBeNull().And.NotBeSameAs(file.Lines(encoding));
        lines.ToArray().Await().Should().BeEmpty();
      });

      RandomEmptyFile.TryFinallyDelete(file =>
      {
        var lines = Randomizer.LettersSequence(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteTo(file, encoding).Await();
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
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Print{T}(T, FileInfo, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.Print<object>(null, RandomFakeFile)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => FileSystemExtensions.Print(new object(), null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.TryFinallyClear(FileInfo, Action{FileInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => RandomFakeFile.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.TryFinallyClear(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => RandomFakeDirectory.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.TryFinallyDelete(FileInfo, Action{FileInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_TryFinallyDelete_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).TryFinallyDelete(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => RandomFakeFile.TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>();

    var bytes = RandomBytes;

    var file = RandomFakeFile;
    file.Exists.Should().BeFalse();
    file.TryFinallyDelete(file => bytes.WriteTo(file).Await()).Should().NotBeNull().And.BeSameAs(file);
    file.Exists.Should().BeFalse();

    file = RandomEmptyFile;
    file.Exists.Should().BeTrue();
    file.TryFinallyDelete(file => bytes.WriteTo(file).Await()).Should().NotBeNull().And.BeSameAs(file);
    file.Exists.Should().BeTrue();
    file.Length.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.TryFinallyDelete(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_TryFinallyDelete_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).TryFinallyDelete(_ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => RandomFakeDirectory.TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => FileSystemExtensions.Files(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Directories(DriveInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DriveInfo_Directories_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).Directories()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Directories(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Directories_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Directories()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Size(DriveInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DriveInfo_Size_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).Size()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.Size(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryInfo_Size_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Size()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileSystemInfo_ToUri_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToUri(null)).ThrowExactly<ArgumentNullException>();

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
    AssertionExtensions.Should(() => FileSystemExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToStream_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToStream(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToReadOnlyStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToReadOnlyStream_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToReadOnlyStream(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToWriteOnlyStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToWriteOnlyStream_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToWriteOnlyStream(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStreamReader(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToStreamReader_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToStreamReader(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToStreamWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToStreamWriter_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.ToStreamWriter(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToBytes(FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileSystemExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>();

      var bytes = RandomBytes;

      RandomEmptyFile.TryFinallyDelete(file =>
      {
        bytes.WriteTo(file).Await();
        file.ToBytes().ToArray().Await().Should().Equal(bytes);
      });

      // Cancellation & offset
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.ToText(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_ToText_Method()
  {
    using (new AssertionScope())
    {
      void ValidateFile(Encoding encoding)
      {
        var text = RandomString;

        RandomEmptyFile.TryFinallyDelete(file =>
        {
          text.WriteTo(file, encoding).Await();
          file.ToText(encoding).Should().Be(text);
        });
      }

      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => FileSystemExtensions.ToText(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

        ValidateFile(null);
        Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateFile);

        // Cancellation & offset
      }
    }

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteBytes(FileInfo, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => RandomFakeFile.WriteBytes(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteText(FileInfo, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileInfo_WriteText_Method()
  {
    AssertionExtensions.Should(() => FileSystemExtensions.WriteText(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => RandomFakeFile.WriteText(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteTo(IEnumerable{byte}, FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(RandomFakeFile)).ThrowExactlyAsync<ArgumentNullException>();
    AssertionExtensions.Should(() => FileSystemExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemExtensions.WriteTo(string, FileInfo, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(RandomFakeFile)).ThrowExactlyAsync<ArgumentNullException>();
    AssertionExtensions.Should(() => FileSystemExtensions.WriteTo(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>();

    throw new NotImplementedException();
  }
}