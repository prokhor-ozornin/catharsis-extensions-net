using System.Reflection;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="MethodInfoExtensions"/>.</para>
/// </summary>
public sealed class MethodInfoExtensionsTest : UnitTest
{
  private delegate string AsString(object subject);

  /// <summary>
  ///   <para>Performs testing of <see cref="MethodInfoExtensions.IsOverridable(MethodInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsOverridable_Method()
  {
    AssertionExtensions.Should(() => MethodInfoExtensions.IsOverridable(null)).ThrowExactly<ArgumentNullException>().WithParameterName("method");

    throw new NotImplementedException();

    return;

    static void Validate(bool result, MethodInfo method) => method.IsOverridable().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MethodInfoExtensions.IsProtected(MethodInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsProtected_Method()
  {
    AssertionExtensions.Should(() => MethodInfoExtensions.IsProtected(null)).ThrowExactly<ArgumentNullException>().WithParameterName("method");

    throw new NotImplementedException();

    return;

    static void Validate(bool result, MethodInfo method) => method.IsProtected().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MethodInfoExtensions.IsInternal(MethodInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsInternal_Method()
  {
    AssertionExtensions.Should(() => MethodInfoExtensions.IsInternal(null)).ThrowExactly<ArgumentNullException>().WithParameterName("method");

    throw new NotImplementedException();

    return;

    static void Validate(bool result, MethodInfo method) => method.IsInternal().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MethodInfoExtensions.IsProtectedInternal(MethodInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsProtectedInternal_Method()
  {
    AssertionExtensions.Should(() => MethodInfoExtensions.IsProtectedInternal(null)).ThrowExactly<ArgumentNullException>().WithParameterName("method");

    throw new NotImplementedException();

    return;

    static void Validate(bool result, MethodInfo method) => method.IsProtectedInternal().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="MethodInfoExtensions.ToDelegate{T}(MethodInfo)"/></description></item>
  ///     <item><description><see cref="MethodInfoExtensions.ToDelegate(MethodInfo, Type)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void MethodInfo_ToDelegate_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => MethodInfoExtensions.ToDelegate<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("method");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => MethodInfoExtensions.ToDelegate(null, typeof(object))).ThrowExactly<ArgumentNullException>().WithParameterName("method");
      AssertionExtensions.Should(() => typeof(object).GetMethod("ToString").ToDelegate(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

      var method = typeof(object).GetMethod("ToString");
      AssertionExtensions.Should(() => method.ToDelegate(typeof(object))).ThrowExactly<ArgumentException>();
      var methodDelegate = method.ToDelegate(typeof(AsString));
      methodDelegate.Method.Should().BeSameAs(method);
      methodDelegate.Target.Should().BeNull();
      AssertionExtensions.Should(() => methodDelegate.DynamicInvoke()).ThrowExactly<TargetParameterCountException>();
      AssertionExtensions.Should(() => methodDelegate.DynamicInvoke(new object(), new object())).ThrowExactly<TargetParameterCountException>();
      methodDelegate.DynamicInvoke("test").To<string>().Should().Be("test");
      method.ToDelegate(typeof(AsString)).Should().Be(method.ToDelegate<AsString>());
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }
}