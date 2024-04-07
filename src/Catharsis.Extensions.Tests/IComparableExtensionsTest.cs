using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
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
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Validate<T>(bool result, T instance) where T : struct, IComparable<T> => instance.IsPositive().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsNegative{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsNegative_Method()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Validate<T>(bool result, T instance) where T : struct, IComparable<T> => instance.IsNegative().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsDefault{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDefault_Method()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Validate<T>(bool result, T instance) where T : struct, IComparable<T> => instance.IsDefault().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.Min{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IComparableExtensions.Min(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(T min, T max) where T : IComparable => min.Min(max).Should().BeSameAs(min);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.Max{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IComparableExtensions.Max(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(T min, T max) where T : IComparable => min.Max(max).Should().BeSameAs(max);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.MinMax{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IComparableExtensions.MinMax(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(T min, T max) where T : IComparable => min.MinMax(max).Should().Be((min, max));
  }
}