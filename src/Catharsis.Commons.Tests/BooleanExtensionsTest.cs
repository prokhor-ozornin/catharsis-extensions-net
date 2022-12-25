using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="BooleanExtensions"/>.</para>
/// </summary>
public sealed class BooleanExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="BooleanExtensions.And(bool, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Boolean_And_Method()
  {
    true.And(true).Should().BeTrue();
    true.And(false).Should().BeFalse();
    false.And(true).Should().BeFalse();
    false.And(false).Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BooleanExtensions.Or(bool, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Boolean_Or_Method()
  {
    true.Or(true).Should().BeTrue();
    true.Or(false).Should().BeTrue();
    false.Or(true).Should().BeTrue();
    false.Or(false).Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BooleanExtensions.Not(bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Boolean_Not_Method()
  {
    true.Not().Should().BeFalse();
    false.Not().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="BooleanExtensions.Xor(bool, bool)"/> method.</para>
  /// </summary>
  [Fact]
  public void Boolean_Xor_Method()
  {
    true.Xor(true).Should().BeFalse();
    true.Xor(false).Should().BeTrue();
    false.Xor(true).Should().BeTrue();
    false.Xor(false).Should().BeFalse();
  }
}