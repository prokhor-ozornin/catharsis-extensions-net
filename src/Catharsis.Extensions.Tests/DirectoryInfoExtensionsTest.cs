using System.Collections.Specialized;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DirectoryInfoExtensions"/>.</para>
/// </summary>
public sealed class DirectoryInfoExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Clone(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Clone()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    Validate(Directory.GetCurrentDirectory().ToDirectory());
    Validate(Attributes.RandomDirectory());

    return;

    static void Validate(DirectoryInfo original)
    {
      var clone = original.Clone();

      clone.Should().NotBeSameAs(original).And.NotBe(original);
      clone.ToString().Should().Be(original.ToString());
      clone.FullName.Should().Be(original.FullName);
      clone.Name.Should().Be(original.Name);
      clone.Extension.Should().Be(original.Extension);
      clone.Parent?.ToString().Should().Be(original.Parent?.ToString());
      clone.Root.ToString().Should().Be(original.Root.ToString());
      clone.Exists.Should().Be(original.Exists);
      clone.Attributes.Should().Be(original.Attributes);
      clone.LinkTarget.Should().Be(original.LinkTarget);
      clone.UnixFileMode.Should().Be(original.UnixFileMode);
      clone.CreationTimeUtc.Should().Be(original.CreationTimeUtc);
      clone.LastAccessTimeUtc.Should().Be(original.LastAccessTimeUtc);
      clone.LastWriteTimeUtc.Should().Be(original.LastWriteTimeUtc);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="DirectoryInfoExtensions.With(DirectoryInfo, IEnumerable{FileSystemInfo})"/></description></item>
  ///     <item><description><see cref="DirectoryInfoExtensions.With(DirectoryInfo, FileSystemInfo[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => DirectoryInfoExtensions.With(null, Enumerable.Empty<FileSystemInfo>())).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
      AssertionExtensions.Should(() => Attributes.RandomDirectory().With((IEnumerable<FileSystemInfo>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("entries");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => DirectoryInfoExtensions.With(null, Array.Empty<FileSystemInfo>())).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
      AssertionExtensions.Should(() => Attributes.RandomDirectory().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("entries");

    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="DirectoryInfoExtensions.Without(DirectoryInfo, IEnumerable{FileSystemInfo})"/></description></item>
  ///     <item><description><see cref="DirectoryInfoExtensions.Without(DirectoryInfo, FileSystemInfo[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => DirectoryInfoExtensions.Without(null, Enumerable.Empty<FileSystemInfo>())).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
      AssertionExtensions.Should(() => Attributes.RandomDirectory().Without((IEnumerable<FileSystemInfo>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("entries");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => DirectoryInfoExtensions.Without(null, Array.Empty<FileSystemInfo>())).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
      AssertionExtensions.Should(() => Attributes.RandomDirectory().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("entries");

    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.IsEmpty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    var directory = Attributes.RandomFakeDirectory();
    directory.Exists.Should().BeFalse();
    directory.IsEmpty().Should().BeTrue();

    directory = Environment.SystemDirectory.ToDirectory();
    directory.Exists.Should().BeTrue();
    directory.IsEmpty().Should().BeFalse();

    Attributes.RandomDirectory().TryFinallyDelete(info =>
    {
      info.Exists.Should().BeTrue();
      info.IsEmpty().Should().BeTrue();
      new Random().File(new Random().Directory(info));
      new Random().File(info);
      info.IsEmpty().Should().BeFalse();
    });

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Empty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();

    return;

    static void Validate(DirectoryInfo directory)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.TryFinallyClear(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    AssertionExtensions.Should(() => Attributes.RandomFakeDirectory().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.TryFinallyDelete(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDelete_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).TryFinallyDelete(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    AssertionExtensions.Should(() => Attributes.RandomFakeDirectory().TryFinallyDelete(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    var directory = Attributes.RandomFakeDirectory();
    directory.Exists.Should().BeFalse();
    directory.TryFinallyDelete(info =>
    {
      new Random().File(info);
      new Random().File(new Random().Directory(info));
    });
    directory.Exists.Should().BeFalse();

    directory = Attributes.RandomDirectory();
    directory.Exists.Should().BeTrue();
    directory.TryFinallyDelete(info =>
    {
      new Random().File(info);
      new Random().File(new Random().Directory(info));
    });
    directory.Exists.Should().BeTrue();
    directory.IsEmpty().Should().BeTrue();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Files(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Files_Method()
  {
    AssertionExtensions.Should(() => DirectoryInfoExtensions.Files(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Directories(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Directories_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Directories()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.InDirectory(DirectoryInfo, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void InDirectory_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).InDirectory(Attributes.RandomFakeDirectory())).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    AssertionExtensions.Should(() => Attributes.RandomFakeDirectory().InDirectory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("parent");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Size(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Size_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Size()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.ToEnumerable(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => DirectoryInfoExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }
}