using System.Reflection;
using System.Text;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="FileInfoExtensions"/>.</para>
/// </summary>
/// <seealso cref="FileInfoExtensions"/>
public sealed class FileInfoExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.get_IsUnset(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Property()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test(bool result, FileInfo file) => file.IsUnset.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.get_IsEmpty(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).IsEmpty).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      var file = FakeFile;
      file.Exists.Should().BeFalse();
      file.IsEmpty.Should().BeTrue();

      var bytes = new Random().ToByte(1).ToArray();

      EmptyFile.TryFinallyDelete(info =>
      {
        info.Exists.Should().BeTrue();
        info.Length.Should().Be(0);
        info.IsEmpty.Should().BeTrue();
        bytes.WriteToAsync(info).Await();
        info.Length.Should().Be(bytes.Length);
        info.IsEmpty.Should().BeFalse();
      });
    }

    return;

    static void Test(bool result, FileInfo file) => file.IsEmpty.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.get_Bytes(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Bytes_Property()
  {
    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.get_Text(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Text_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.get_Lines(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.get_Stream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.InDirectory(FileInfo, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void InDirectory_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).InDirectory(FakeDirectory)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
      AssertionExtensions.Should(() => FakeFile.InDirectory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    }

    throw new NotImplementedException();

    return;

    static void Test(bool result, FileInfo file, DirectoryInfo directory) => file.InDirectory(directory).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.CreateWithPath(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void CreateWithPath_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).CreateWithPath()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToLines(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToLines_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToLines()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      Test(EmptyFile);
      Encoding.GetEncodings().ForEach(encoding => Test(EmptyFile, encoding.GetEncoding()));
    }

    return;

    static void Test(FileInfo file, Encoding encoding = null)
    {
      file.TryFinallyDelete(file =>
      {
        var lines = file.ToLines(encoding);
        lines.Should().BeOfType<string[]>().And.BeSameAs(file.ToLines(encoding)).And.BeEmpty();

        lines = new Random().ToLetters(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteToAsync(file, encoding).Await();
        file.ToLines(encoding).Should().BeOfType<string[]>().And.Equal(lines);
      });
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToLinesAsync(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToLinesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToLinesAsync().ToArrayAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      Test(EmptyFile);
      Encoding.GetEncodings().ForEach(encoding => Test(EmptyFile, encoding.GetEncoding()));
    }

    return;

    static void Test(FileInfo file, Encoding encoding = null)
    {
      file.TryFinallyDelete(file =>
      {
        var lines = new Random().ToLetters(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteToAsync(file, encoding).Await();
        file.ToLinesAsync(encoding).ToArray().Should().BeOfType<IAsyncEnumerable<string>>().And.Equal(lines);

        var linesAsync = file.ToLinesAsync(encoding);
        linesAsync.Should().BeOfType<IAsyncEnumerable<string>>();
        linesAsync.ToArray().Should().BeOfType<string[]>().And.BeEmpty();
      });
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.AsReadOnly(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).AsReadOnly()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.Clone(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).Clone()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      Test(EmptyFile);
      Test(NonEmptyFile);
    }

    return;

    static void Test(FileInfo original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<FileInfo>().And.NotBeSameAs(original).And.NotBe(original);
      clone.ToString().Should().Be(original.ToString());
      clone.FullName.Should().Be(original.FullName);
      clone.Name.Should().Be(original.Name);
      clone.DirectoryName.Should().Be(original.DirectoryName);
      clone.Extension.Should().Be(original.Extension);
      clone.Length.Should().Be(original.Length);
      clone.Exists.Should().Be(original.Exists);
      clone.IsReadOnly.Should().Be(original.IsReadOnly);
      clone.Attributes.Should().Be(original.Attributes);
      clone.LinkTarget.Should().Be(original.LinkTarget);
      clone.UnixFileMode.Should().Be(original.UnixFileMode);
      clone.CreationTimeUtc.Should().Be(original.CreationTimeUtc);
      clone.LastAccessTimeUtc.Should().Be(original.LastAccessTimeUtc);
      clone.LastWriteTimeUtc.Should().Be(original.LastWriteTimeUtc);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.Empty(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      var file = FakeFile;
      file.Exists.Should().BeFalse();
      file.Empty().Should().BeOfType<FileInfo>().And.BeSameAs(file);
      file.Exists.Should().BeTrue();
      file.Length.Should().Be(0);
      file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);

      NonEmptyFile.TryFinallyDelete(info =>
      {
        info.Length.Should().BePositive();
        info.Empty().Should().BeOfType<FileInfo>().And.BeSameAs(info);

        info.Exists.Should().BeTrue();
        info.Length.Should().Be(0);
        info.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
      });
    }

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.TryFinallyClear(FileInfo, Action{FileInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).TryFinallyClear(_ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("file");
      AssertionExtensions.Should(() => FakeFile.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.TryFinallyDelete(FileInfo, Action{FileInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDelete_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).TryFinallyDelete(_ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("file");
      AssertionExtensions.Should(() => FakeFile.TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Test(FakeFile, Bytes);
      Test(EmptyFile, Bytes);
      Test(NonEmptyFile, Bytes);
    }

    return;

    static void Test(FileInfo file, byte[] bytes)
    {
      file.Exists.Should().BeFalse();
      file.TryFinallyDelete(info =>
      {
        var task = bytes.WriteToAsync(info);
        task.Should().BeAssignableTo<Task<IEnumerable<byte>>>();
        //task.Await().Should().BeOfType<IEnumerable<byte>>().And.BeSameAs(file);
      }).Should().BeOfType<FileInfo>().And.NotBeNull().And.BeSameAs(file);
      file.Exists.Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.DeserializeAsDataContract{T}(FileInfo, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, params Type[] types)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.DeserializeAsXml{T}(FileInfo, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, params Type[] types)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.WriteBytes(FileInfo, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).WriteBytes([])).ThrowExactly<ArgumentNullException>().WithParameterName("file");
      AssertionExtensions.Should(() => FakeFile.WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, byte[] bytes)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.WriteBytesAsync(FileInfo, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).WriteBytesAsync([])).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();
      AssertionExtensions.Should(() => FakeFile.WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => FakeFile.WriteBytesAsync([])).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, byte[] bytes)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.WriteText(FileInfo, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).WriteText(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
      AssertionExtensions.Should(() => FakeFile.WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, string text, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.WriteTextAsync(FileInfo, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).WriteTextAsync(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();
      AssertionExtensions.Should(() => FakeFile.WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => FakeFile.WriteTextAsync(string.Empty, null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, string text, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToBytes(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(byte[] result, FileInfo file) => file.ToBytes().Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToBytesAsync(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToBytesAsync().ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      var bytes = Bytes;

      EmptyFile.TryFinallyDelete(file =>
      {
        bytes.WriteToAsync(file).Await();
        file.ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(bytes);
      });

      // this.CancellationToken() & offset
    }

    throw new NotImplementedException();

    return;

    static void Test(byte[] result, FileInfo file) => file.ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToText(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, FileInfo file, Encoding encoding = null) => file.ToText(encoding).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToTextAsync(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();

      TestFile(EmptyFile, Fixture.Create<string>(), null);
      Encoding.GetEncodings().ForEach(encoding => TestFile(EmptyFile, Fixture.Create<string>(), encoding.GetEncoding()));

      // this.CancellationToken() & offset
    }

    return;

    static void TestFile(FileInfo file, string text, Encoding encoding)
    {
      file.TryFinallyDelete(file =>
      {
        text.WriteTo(file, encoding);
        
        var task = file.ToTextAsync(encoding);
        task.Should().BeAssignableTo<Task<string>>();
        task.Await().Should().BeOfType<string>().And.Be(text);
      });
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToStream()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToReadOnlyStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyStream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToReadOnlyStream()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToWriteOnlyStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToWriteOnlyStream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToWriteOnlyStream()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToStreamReader(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToStreamReader()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToStreamWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToStreamWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlReader(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlDictionaryReader(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlDictionaryWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlDocument(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXDocument(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXDocumentAsync(FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();
      AssertionExtensions.Should(() => FakeFile.ToXDocumentAsync(CancellationToken.None)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToBoolean(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBoolean_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, FakeFile);
      Test(true, Assembly.GetExecutingAssembly().Location.ToFile());
    }

    return;

    static void Test(bool result, FileInfo file) => file.ToBoolean().Should().Be(result);
  }
}