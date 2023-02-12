using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IComparableExtensions"/>.</para>
/// </summary>
public sealed class IComparableExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsPositive{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPositive_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsNegative{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsNegative_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsDefault{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDefault_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.Min{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    AssertionExtensions.Should(() => IComparableExtensions.Min(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.Max{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    AssertionExtensions.Should(() => IComparableExtensions.Max(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.MinMax{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    AssertionExtensions.Should(() => IComparableExtensions.MinMax(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();
  }
}