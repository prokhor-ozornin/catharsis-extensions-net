using Catharsis.Commons;
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

    return;

    static void Validate<T>(bool isPositive, T instance) where T : struct, IComparable<T> => instance.IsPositive().Should().Be(isPositive);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsNegative{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsNegative_Method()
  {
    throw new NotImplementedException();

    return;

    static void Validate<T>(bool isNegative, T instance) where T : struct, IComparable<T> => instance.IsNegative().Should().Be(isNegative);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsDefault{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDefault_Method()
  {
    throw new NotImplementedException();

    return;

    static void Validate<T>(bool isDefault, T instance) where T : struct, IComparable<T> => instance.IsDefault().Should().Be(isDefault);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.Min{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    AssertionExtensions.Should(() => IComparableExtensions.Min(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();

    return;

    static void Validate<T>(T result, T left, T right) where T : IComparable => left.Min(right).Should().BeSameAs(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.Max{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    AssertionExtensions.Should(() => IComparableExtensions.Max(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();

    return;

    static void Validate<T>(T result, T left, T right) where T : IComparable => left.Max(right).Should().BeSameAs(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.MinMax{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    AssertionExtensions.Should(() => IComparableExtensions.MinMax(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");

    throw new NotImplementedException();

    return;

    static void Validate<T>(T left, T right) where T : IComparable => left.MinMax(right).Should().Be((left, right));
  }
}