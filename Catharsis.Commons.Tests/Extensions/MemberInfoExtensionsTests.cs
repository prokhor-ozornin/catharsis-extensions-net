using System;
using System.Reflection;
using Xunit;


namespace Catharsis.Commons.Extensions
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

      Assert.True(typeof(TestObject).GetDefaultConstructor().To<MemberInfo>().IsConstructor());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsEvent(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsEvent_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsEvent(null));

      Assert.True(typeof(TestObject).GetAnyEvent("PublicEvent").To<MemberInfo>().IsEvent());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsField(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsField_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsField(null));

      Assert.True(typeof(TestObject).GetAnyField("PublicField").To<MemberInfo>().IsField());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsMethod(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsMethod_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsMethod(null));

      Assert.True(typeof(TestObject).GetAnyMethod("PublicMethod").To<MemberInfo>().IsMethod());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsProperty(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsProperty_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.IsProperty(null));

      Assert.True(typeof(TestObject).GetAnyProperty("PublicProperty").To<MemberInfo>().IsProperty());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.MemberType(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void MemberType_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.MemberType(null));

      var type = typeof(TestObject);

      var eventMember = type.GetAnyEvent("PublicEvent");
      Assert.True(eventMember.MemberType() == eventMember.EventHandlerType);

      var fieldMember = type.GetAnyField("PublicField");
      Assert.True(fieldMember.MemberType() == fieldMember.FieldType);

      var methodMember = type.GetAnyMethod("PublicMethod");
      Assert.True(methodMember.MemberType() == methodMember.ReturnType);
      
      var propertyMember = type.GetAnyProperty("PublicProperty");
      Assert.True(propertyMember.MemberType() == propertyMember.PropertyType);
    }
  }
}