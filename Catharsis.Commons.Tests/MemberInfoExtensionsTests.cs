using System;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="MemberInfoExtensions"/>.</para>
  /// </summary>
  public sealed class MemberInfoExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsConstructor(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsConstructor_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsConstructor(null));

      Assert.True(typeof(TestObject).DefaultConstructor().To<MemberInfo>().IsConstructor());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsEvent(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsEvent_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsEvent(null));

      Assert.True(typeof(TestObject).AnyEvent("PublicEvent").To<MemberInfo>().IsEvent());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsField(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsField_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsField(null));

      Assert.True(typeof(TestObject).AnyField("PublicField").To<MemberInfo>().IsField());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsMethod(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsMethod_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsMethod(null));

      Assert.True(typeof(TestObject).AnyMethod("PublicMethod").To<MemberInfo>().IsMethod());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsProperty(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsProperty_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsProperty(null));

      Assert.True(typeof(TestObject).AnyProperty("PublicProperty").To<MemberInfo>().IsProperty());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.MemberType(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void MemberType_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.MemberType(null));

      var type = typeof(TestObject);

      var eventMember = type.AnyEvent("PublicEvent");
      Assert.Equal(eventMember.EventHandlerType, eventMember.MemberType());

      var fieldMember = type.AnyField("PublicField");
      Assert.Equal(fieldMember.FieldType, fieldMember.MemberType());

      var methodMember = type.AnyMethod("PublicMethod");
      Assert.Equal(methodMember.ReturnType, methodMember.MemberType());
      
      var propertyMember = type.AnyProperty("PublicProperty");
      Assert.Equal(propertyMember.PropertyType, propertyMember.MemberType());
    }
  }
}