using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IComparableExtensions"/>.</para>
/// </summary>
/// <seealso cref="IComparableExtensions"/>
public sealed class IComparableExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.Min{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IComparable) null).Min(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("comparable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(T min, T max) where T : IComparable => min.Min(max).Should().BeSameAs(min);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.Max{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IComparable) null).Max(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("comparable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(T min, T max) where T : IComparable => min.Max(max).Should().BeSameAs(max);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.MinMax{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IComparable) null).MinMax(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("comparable");
    }

    throw new NotImplementedException();

    return;

    static void Test<T>(T min, T max) where T : IComparable => min.MinMax(max).Should().Be((min, max));
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.get_IsDefault{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDefault_Property()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, T instance) where T : struct, IComparable<T> => instance.IsDefault.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.get_IsPositive{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsPositive_Property()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, T instance) where T : struct, IComparable<T> => instance.IsPositive.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.get_IsNegative{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsNegative_Property()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, T instance) where T : struct, IComparable<T> => instance.IsNegative.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsLesser{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsLesser_Method()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, T left, T right) where T : struct, IComparable<T> => left.IsLesser(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsLesserOrEqual{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsLesserOrEqual_Method()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, T left, T right) where T : struct, IComparable<T> => left.IsLesserOrEqual(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsGreater{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsGreater_Method()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, T left, T right) where T : struct, IComparable<T> => left.IsGreater(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IComparableExtensions.IsGreaterOrEqual{T}(T, T)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsGreaterOrEqual_Method()
  {
    using (new AssertionScope())
    {
      throw new NotImplementedException();
    }

    return;

    static void Test<T>(bool result, T left, T right) where T : struct, IComparable<T> => left.IsGreaterOrEqual(right).Should().Be(result);
  }
}