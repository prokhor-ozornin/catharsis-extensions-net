using System;
using System.ComponentModel;
using System.Linq;
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
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="MemberInfoExtensions.Attribute(MemberInfo, Type)"/></description></item>
    ///     <item><description><see cref="MemberInfoExtensions.Attribute{T}(MemberInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Attribute_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.Attribute(null, typeof(object)));
      Assert.Throws<ArgumentNullException>(() => typeof(object).Attribute(null));
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.Attribute<object>(null));

      Assert.Null(typeof(TestObject).Attribute(typeof(NonSerializedAttribute)));
      Assert.Null(typeof(TestObject).Attribute<NonSerializedAttribute>());

      Assert.True(typeof(TestObject).Attribute(typeof(SerializableAttribute)) is SerializableAttribute);
      Assert.NotNull(typeof(TestObject).Attribute<SerializableAttribute>());


      var property = typeof(string).GetProperty("Length");
      Assert.Null(property.Attribute(typeof(DescriptionAttribute)));
      Assert.Null(property.Attribute<DescriptionAttribute>());

      property = typeof(TestObject).GetProperty("PublicProperty");
      Assert.NotNull(property.Attribute(typeof(DescriptionAttribute)));
      Assert.NotNull(property.Attribute<DescriptionAttribute>());


      var method = typeof(object).GetMethod("ToString");
      Assert.Null(method.Attribute(typeof(DescriptionAttribute)));
      Assert.Null(method.Attribute<DescriptionAttribute>());

      method = typeof(TestObject).GetMethod("PublicMethod");
      Assert.NotNull(method.Attribute(typeof(DescriptionAttribute)));
      Assert.NotNull(method.Attribute<DescriptionAttribute>());

      
      var field = typeof(string).GetField("Empty");
      Assert.Null(field.Attribute(typeof(DescriptionAttribute)));
      Assert.Null(field.Attribute<DescriptionAttribute>());

      field = typeof(TestObject).GetField("PublicField");
      Assert.NotNull(field.Attribute(typeof(DescriptionAttribute)));
      Assert.NotNull(field.Attribute<DescriptionAttribute>());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="MemberInfoExtensions.Attributes(MemberInfo, Type)"/></description></item>
    ///     <item><description><see cref="MemberInfoExtensions.Attributes{T}(MemberInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Attributes_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.Attribute(null, typeof(object)));
      Assert.Throws<ArgumentNullException>(() => typeof(object).Attribute(null));
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.Attributes<object>(null));

      Assert.False(typeof(TestObject).Attributes(typeof(NonSerializedAttribute)).Any());
      Assert.False(typeof(TestObject).Attributes<NonSerializedAttribute>().Any());

      Assert.True(typeof(TestObject).Attributes(typeof(SerializableAttribute)).SequenceEqual(new[] { new SerializableAttribute() }));
      Assert.True(typeof(TestObject).Attributes<SerializableAttribute>().SequenceEqual(new[] { new SerializableAttribute() }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="MemberInfoAttributesExtensions.Description(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void Description_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoAttributesExtensions.Description(null));
      
      Assert.Null(typeof(TestObject).Description());
      Assert.True(typeof(TestObject).AnyProperty("ReadOnlyProperty").Description().IsEmpty());
      Assert.Equal("PublicProperty", typeof(TestObject).AnyProperty("PublicProperty").Description());
      Assert.Equal("ProtectedProperty", typeof(TestObject).AnyProperty("ProtectedProperty").Description());
      Assert.Equal("PublicStaticProperty Description", typeof(TestObject).AnyProperty("PublicStaticProperty").Description());
    }

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
    ///   <para>Performs testing of <see cref="MemberInfoExtensions.Type(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void Type_Method()
    {
      Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.Type(null));

      var type = typeof(TestObject);

      var eventMember = type.AnyEvent("PublicEvent");
      Assert.Equal(eventMember.EventHandlerType, eventMember.Type());

      var fieldMember = type.AnyField("PublicField");
      Assert.Equal(fieldMember.FieldType, fieldMember.Type());

      var methodMember = type.AnyMethod("PublicMethod");
      Assert.Equal(methodMember.ReturnType, methodMember.Type());
      
      var propertyMember = type.AnyProperty("PublicProperty");
      Assert.Equal(propertyMember.PropertyType, propertyMember.Type());
    }
  }
}