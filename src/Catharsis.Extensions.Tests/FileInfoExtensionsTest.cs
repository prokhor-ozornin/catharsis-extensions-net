﻿using System.Text;
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
  ///   <para>Performs testing of <see cref="FileInfoExtensions.Clone(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.IsEmpty(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    var file = Attributes.RandomFakeFile();
    file.Exists.Should().BeFalse();
    file.IsEmpty().Should().BeTrue();

    var bytes = new Random().ByteSequence(1).ToArray();

    Attributes.RandomEmptyFile().TryFinallyDelete(file =>
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
  ///   <para>Performs testing of <see cref="FileInfoExtensions.Empty(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    var file = Attributes.RandomFakeFile();
    file.Exists.Should().BeFalse();
    file.Empty().Should().NotBeNull().And.BeSameAs(file);
    file.Exists.Should().BeTrue();
    file.Length.Should().Be(0);
    file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);

    Attributes.RandomNonEmptyFile().TryFinallyDelete(file =>
    {
      file.Length.Should().BePositive();
      file.Empty().Should().NotBeNull().And.BeSameAs(file);

      file.Exists.Should().BeTrue();
      file.Length.Should().Be(0);
      file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.CreateWithPath(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void CreateWithPath_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.CreateWithPath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
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

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    return;

    void Validate(Encoding encoding)
    {
      Attributes.RandomEmptyFile().TryFinallyDelete(file =>
      {
        var lines = file.Lines(encoding);
        lines.Should().NotBeNull().And.BeSameAs(file.Lines(encoding)).And.BeEmpty();
      });

      Attributes.RandomEmptyFile().TryFinallyDelete(file =>
      {
        var lines = new Random().LettersSequence(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteToAsync(file, encoding).Await();
        file.Lines(encoding).Should().NotBeNull().And.NotBeSameAs(file.Lines(encoding)).And.Equal(lines);
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

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    return;

    void Validate(Encoding encoding)
    {
      Attributes.RandomEmptyFile().TryFinallyDelete(file =>
      {
        var lines = file.LinesAsync(encoding);
        lines.Should().NotBeNull().And.NotBeSameAs(file.LinesAsync(encoding));
        lines.ToArray().Should().BeEmpty();
      });

      Attributes.RandomEmptyFile().TryFinallyDelete(file =>
      {
        var lines = new Random().LettersSequence(80, 1000).ToArray();
        lines.Join(Environment.NewLine).WriteToAsync(file, encoding).Await();
        file.LinesAsync(encoding).ToArray().Should().NotBeNull().And.Equal(lines);
      });
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.TryFinallyClear(FileInfo, Action{FileInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).TryFinallyClear(_ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
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

      Validate(Attributes.RandomFakeFile());
      Validate(Attributes.RandomEmptyFile());
      Validate(Attributes.RandomNonEmptyFile());
    }

    return;

    void Validate(FileInfo file)
    {
      var bytes = Attributes.RandomBytes();

      file.Exists.Should().BeFalse();
      file.TryFinallyDelete(file => bytes.WriteToAsync(file).Await()).Should().NotBeNull().And.BeSameAs(file);
      file.Exists.Should().BeFalse();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.AsReadOnly(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnly_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.AsReadOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.InDirectory(FileInfo, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void InDirectory_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).InDirectory(Attributes.RandomFakeDirectory())).ThrowExactly<ArgumentNullException>().WithParameterName("file");
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().InDirectory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStream_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToReadOnlyStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyStream_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.ToReadOnlyStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToWriteOnlyStream(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToWriteOnlyStream_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.ToWriteOnlyStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToStreamReader(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamReader_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.ToStreamReader(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToStreamWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStreamWriter_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.ToStreamWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToBytes(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToBytesAsync(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.ToBytesAsync(null).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    var bytes = Attributes.RandomBytes();

    Attributes.RandomEmptyFile().TryFinallyDelete(file =>
    {
      bytes.WriteToAsync(file).Await();
      file.ToBytesAsync().ToArray().Should().Equal(bytes);
    });

    // Attributes.CancellationToken() & offset

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToText(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToTextAsync(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    void ValidateFile(Encoding encoding)
    {
      var text = Attributes.RandomString();

      Attributes.RandomEmptyFile().TryFinallyDelete(file =>
      {
        text.WriteToAsync(file, encoding).Await();
        file.ToTextAsync(encoding).Await().Should().Be(text);
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileInfoExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();

      ValidateFile(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateFile);

      // Attributes.CancellationToken() & offset
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlReader(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlDictionaryReader(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlDictionaryWriter(FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryWriter_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXmlDocument(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXDocument(FileInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.ToXDocumentAsync(FileInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("file").Await();
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().ToXDocumentAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.WriteBytes(FileInfo, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.WriteBytesAsync(FileInfo, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteBytesAsync(Enumerable.Empty<byte>(), Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.WriteText(FileInfo, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.WriteTextAsync(FileInfo, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => FileInfoExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => Attributes.RandomFakeFile().WriteTextAsync(string.Empty, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.DeserializeAsDataContract{T}(FileInfo, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FileInfoExtensions.DeserializeAsXml{T}(FileInfo, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((FileInfo) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("file");

    throw new NotImplementedException();
  }
}