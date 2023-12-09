using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DriveInfoExtensions"/>.</para>
/// </summary>
public sealed class DriveInfoExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DriveInfoExtensions.Clone(DriveInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => DriveInfoExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DriveInfoExtensions.IsEmpty(DriveInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    DriveInfo.GetDrives().Should().Contain(drive => !drive.IsEmpty());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DriveInfoExtensions.Directories(DriveInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Directories_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).Directories()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DriveInfoExtensions.Size(DriveInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Size_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).Size()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    throw new NotImplementedException();
  }
}