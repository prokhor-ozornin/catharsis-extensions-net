using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DirectoryInfoExtensions"/>.</para>
/// </summary>
public sealed class DirectoryInfoExtensionsTest : UnitTest
{

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.IsEmpty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
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
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Empty(DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.TryFinallyClear(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    AssertionExtensions.Should(() => RandomFakeDirectory.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.TryFinallyDelete(DirectoryInfo, Action{DirectoryInfo})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDelete_Method()
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
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Files(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Files_Method()
  {
    AssertionExtensions.Should(() => DirectoryInfoExtensions.Files(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Directories(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Directories_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Directories()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.InDirectory(DirectoryInfo, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void InDirectory_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).InDirectory(RandomFakeDirectory)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");
    AssertionExtensions.Should(() => RandomFakeDirectory.InDirectory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("parent");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.Size(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Size_Method()
  {
    AssertionExtensions.Should(() => ((DirectoryInfo) null).Size()).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DirectoryInfoExtensions.ToEnumerable(DirectoryInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => DirectoryInfoExtensions.ToEnumerable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("directory");

    throw new NotImplementedException();
  }
}