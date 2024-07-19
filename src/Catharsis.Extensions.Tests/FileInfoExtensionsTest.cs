using System.Text;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="FileInfoExtensions"/>.</para>
/// </summary>
public sealed class FileInfoExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.IsUnset(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate(bool result, FileInfo file) => file.IsUnset().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.IsEmpty(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      var file = Attributes.RandomFakeFile();
      file.Exists.Should().BeFalse();
      file.IsEmpty().Should().BeTrue();

      var bytes = new Random().ByteSequence(1).ToArray();

      Attributes.RandomEmptyFile().TryFinallyDelete(info =>
      {
        info.Exists.Should().BeTrue();
        info.Length.Should().Be(0);
        info.IsEmpty().Should().BeTrue();
        bytes.WriteToAsync(info).Await();
        info.Length.Should().Be(bytes.Length);
        info.IsEmpty().Should().BeFalse();
      });
    }

    return;

    static void Validate(bool result, FileInfo file) => file.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.InDirectory(FileInfo, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void InDirectory_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((FileInfo) null).InDirectory(Attributes.RandomFakeDirectory())).ThrowExactly<ArgumentNullException>().WithParameterName("file");
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().InDirectory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    }

    throw new NotImplementedException();

    return;

    static void Validate(bool result, FileInfo file, DirectoryInfo directory) => file.InDirectory(directory).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.CreateWithPath(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void CreateWithPath_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileInfoExtensions.CreateWithPath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.Lines(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileInfoExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      Validate(Attributes.RandomEmptyFile());
      Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomEmptyFile(), encoding.GetEncoding()));
    }

    return;

    static void Validate(FileInfo file, Encoding encoding = null)
    {
      file.TryFinallyDelete(file =>
      {
        var lines = file.Lines(encoding);
        lines.Should().BeOfType<string[]>().And.BeSameAs(file.Lines(encoding)).And.BeEmpty();

        lines = new Random().LettersSequence(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteToAsync(file, encoding).Await();
        file.Lines(encoding).Should().BeOfType<string[]>().And.Equal(lines);
      });
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.LinesAsync(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void LinesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileInfoExtensions.LinesAsync(null).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();

      Validate(Attributes.RandomEmptyFile());
      Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomEmptyFile(), encoding.GetEncoding()));
    }

    return;

    static void Validate(FileInfo file, Encoding encoding = null)
    {
      file.TryFinallyDelete(file =>
      {
        var lines = new Random().LettersSequence(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteToAsync(file, encoding).Await();
        file.LinesAsync(encoding).ToArray().Should().BeOfType<IAsyncEnumerable<string>>().And.Equal(lines);

        var linesAsync = file.LinesAsync(encoding);
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
      AssertionExtensions.Should(() => FileInfoExtensions.AsReadOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file)
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
      AssertionExtensions.Should(() => FileInfoExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      Validate(Attributes.RandomEmptyFile());
      Validate(Attributes.RandomNonEmptyFile());
    }

    return;

    static void Validate(FileInfo original)
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

      var file = Attributes.RandomFakeFile();
      file.Exists.Should().BeFalse();
      file.Empty().Should().BeOfType<FileInfo>().And.BeSameAs(file);
      file.Exists.Should().BeTrue();
      file.Length.Should().Be(0);
      file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);

      Attributes.RandomNonEmptyFile().TryFinallyDelete(info =>
      {
        info.Length.Should().BePositive();
        info.Empty().Should().BeOfType<FileInfo>().And.BeSameAs(info);

        info.Exists.Should().BeTrue();
        info.Length.Should().Be(0);
        info.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
      });
    }

    return;

    static void Validate(FileInfo file)
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
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file)
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
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(Attributes.RandomFakeFile(), Attributes.RandomBytes());
      Validate(Attributes.RandomEmptyFile(), Attributes.RandomBytes());
      Validate(Attributes.RandomNonEmptyFile(), Attributes.RandomBytes());
    }

    return;

    static void Validate(FileInfo file, byte[] bytes)
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

    static void Validate(FileInfo file, params Type[] types)
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

    static void Validate(FileInfo file, params Type[] types)
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
      AssertionExtensions.Should(() => FileInfoExtensions.WriteBytes(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file, byte[] bytes)
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
      AssertionExtensions.Should(() => FileInfoExtensions.WriteBytesAsync(null, [])).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteBytesAsync([], Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file, byte[] bytes)
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
      AssertionExtensions.Should(() => FileInfoExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file, string text, Encoding encoding = null)
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
      AssertionExtensions.Should(() => FileInfoExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteTextAsync(string.Empty, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file, string text, Encoding encoding = null)
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
      AssertionExtensions.Should(() => FileInfoExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, FileInfo file) => file.ToBytes().Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToBytesAsync(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileInfoExtensions.ToBytesAsync(null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

      var bytes = Attributes.RandomBytes();

      Attributes.RandomEmptyFile().TryFinallyDelete(file =>
      {
        bytes.WriteToAsync(file).Await();
        file.ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(bytes);
      });

      // Attributes.CancellationToken() & offset
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, FileInfo file) => file.ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToText(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileInfoExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, FileInfo file, Encoding encoding = null) => file.ToText(encoding).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToTextAsync(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileInfoExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();

      ValidateFile(Attributes.RandomEmptyFile(), Attributes.RandomString(), null);
      Encoding.GetEncodings().ForEach(encoding => ValidateFile(Attributes.RandomEmptyFile(), Attributes.RandomString(), encoding.GetEncoding()));

      // Attributes.CancellationToken() & offset
    }

    return;

    static void ValidateFile(FileInfo file, string text, Encoding encoding)
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
      AssertionExtensions.Should(() => FileInfoExtensions.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file)
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
      AssertionExtensions.Should(() => FileInfoExtensions.ToReadOnlyStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file)
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
      AssertionExtensions.Should(() => FileInfoExtensions.ToWriteOnlyStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file)
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
      AssertionExtensions.Should(() => FileInfoExtensions.ToStreamReader(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file, Encoding encoding = null)
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
      AssertionExtensions.Should(() => FileInfoExtensions.ToStreamWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file, Encoding encoding = null)
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

    static void Validate(FileInfo file)
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

    static void Validate(FileInfo file)
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

    static void Validate(FileInfo file, Encoding encoding = null)
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

    static void Validate(FileInfo file, Encoding encoding = null)
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

    static void Validate(FileInfo file)
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

    static void Validate(FileInfo file)
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
      AssertionExtensions.Should(() => Attributes.RandomFakeFile().ToXDocumentAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(FileInfo file)
    {
    }
  }
}