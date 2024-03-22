﻿using Catharsis.Commons;
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

    DriveInfo.GetDrives().ForEach(Validate);

    return;

    static void Validate(DriveInfo original)
    {
      var clone = original.Clone();

      clone.Should().NotBeSameAs(original).And.NotBe(original);
      clone.ToString().Should().Be(original.ToString());
      clone.Name.Should().Be(original.Name);
      clone.IsReady.Should().Be(original.IsReady);
      clone.RootDirectory.ToString().Should().Be(original.RootDirectory.ToString());
      clone.TotalSize.Should().Be(original.TotalSize);
      clone.TotalFreeSpace.Should().Be(original.TotalFreeSpace);
      clone.AvailableFreeSpace.Should().Be(original.AvailableFreeSpace);
      clone.DriveType.Should().Be(original.DriveType);
      clone.DriveFormat.Should().Be(original.DriveFormat);
      clone.VolumeLabel.Should().Be(original.VolumeLabel);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DriveInfoExtensions.IsEmpty(DriveInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    DriveInfo.GetDrives().Should().Contain(drive => !drive.IsEmpty());

    return;

    static void Validate(DriveInfo drive, bool isEmpty)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DriveInfoExtensions.Directories(DriveInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Directories_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).Directories()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    throw new NotImplementedException();

    return;

    static void Validate(DriveInfo drive)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DriveInfoExtensions.Size(DriveInfo, string, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Size_Method()
  {
    AssertionExtensions.Should(() => ((DriveInfo) null).Size()).ThrowExactly<ArgumentNullException>().WithParameterName("drive");

    throw new NotImplementedException();

    return;

    static void Validate(DriveInfo drive)
    {
    }
  }
}