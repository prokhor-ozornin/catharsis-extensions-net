using System;
using System.ComponentModel;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="MethodInfoExtensions"/>.</para>
  /// </summary>
  public sealed class MethodInfoExtensionsTests
  {
    private delegate string AsString(object subject);

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="MethodInfoExtensions.Attribute(MethodInfo, Type)"/></description></item>
    ///     <item><description><see cref="MethodInfoExtensions.Attribute{T}(MethodInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Attribute_Methods()
    {
      var method = typeof(object).GetMethod("ToString");
      Assert.Null(method.Attribute(typeof(DescriptionAttribute)));
      Assert.Null(method.Attribute<DescriptionAttribute>());

      method = typeof(TestObject).GetMethod("PublicMethod");
      Assert.NotNull(method.Attribute(typeof(DescriptionAttribute)));
      Assert.NotNull(method.Attribute<DescriptionAttribute>());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="MethodInfoExtensions.Delegate(MethodInfo, Type)"/></description></item>
    ///     <item><description><see cref="MethodInfoExtensions.Delegate{T}(MethodInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Delegate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MethodInfoExtensions.Delegate(null, typeof(object)));
      Assert.Throws<ArgumentNullException>(() => typeof(object).GetMethod("ToString").Delegate(null));
      Assert.Throws<ArgumentNullException>(() => MethodInfoExtensions.Delegate<object>(null));

      var method = typeof(object).GetMethod("ToString");
      Assert.Throws<ArgumentException>(() => method.Delegate(typeof(object)));
      var methodDelegate = method.Delegate(typeof(AsString));
      Assert.True(ReferenceEquals(methodDelegate.Method, method));
      Assert.Null(methodDelegate.Target);
      Assert.Throws<TargetParameterCountException>(() => methodDelegate.DynamicInvoke());
      Assert.Throws<TargetParameterCountException>(() => methodDelegate.DynamicInvoke(new object(), new object()));
      Assert.Equal("test", methodDelegate.DynamicInvoke("test").To<string>());
      Assert.Equal(method.Delegate(typeof(AsString)), method.Delegate<AsString>());
    }
  }
}