using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ReflectionExtensions"/>.</para>
  /// </summary>
  public sealed class ReflectionExtensionsTests
  {
    private delegate string AsString(object subject);

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ReflectionExtensions.Delegate(MethodInfo, Type)"/></description></item>
    ///     <item><description><see cref="ReflectionExtensions.Delegate{T}(MethodInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Delegate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Delegate(null, typeof(object)));
      Assert.Throws<ArgumentNullException>(() => typeof(object).GetMethod("ToString").Delegate(null));
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Delegate<object>(null));

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

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionAttributesExtensions.Description(Enum)"/> method.</para>
    /// </summary>
    [Fact]
    public void Description()
    {
      Assert.Equal("FirstOption", MockEnumeration.First.Description());
      Assert.Null(MockEnumeration.Second.Description());
      Assert.Equal("ThirdOption", MockEnumeration.Third.Description());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionAttributesExtensions.Descriptions{ENUM}()"/> method.</para>
    /// </summary>
    [Fact]
    public void Descriptions_Method()
    {
      Assert.Throws<ArgumentException>(() => ReflectionAttributesExtensions.Descriptions<DateTime>());
      var descriptions = ReflectionAttributesExtensions.Descriptions<MockEnumeration>().ToArray();
      Assert.Equal(3, descriptions.Count());
      Assert.Equal("FirstOption", descriptions[0]);
      Assert.Equal("Second", descriptions[1]);
      Assert.Equal("ThirdOption", descriptions[2]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.IsProtected(FieldInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsProtected_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsProtected(null));
 
      Assert.True(typeof(TestObject).GetField("ProtectedField", BindingFlags.Instance | BindingFlags.NonPublic).IsProtected());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.Resource(Assembly, string, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void Resource_Method()
    {
      Assert.Throws<ArgumentNullException>(() => Assembly.GetExecutingAssembly().Resource(null));
      Assert.Throws<ArgumentException>(() => Assembly.GetExecutingAssembly().Resource(string.Empty));

      Assert.Null(Assembly.GetExecutingAssembly().Resource("invalid"));
      Assert.Equal("resource", Assembly.GetExecutingAssembly().Resource("Catharsis.Commons.Resource.txt"));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ReflectionExtensions.Attribute(MemberInfo, Type)"/></description></item>
    ///     <item><description><see cref="ReflectionExtensions.Attribute{T}(MemberInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Attribute_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Attribute(null, typeof(object)));
      Assert.Throws<ArgumentNullException>(() => typeof(object).Attribute(null));
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Attribute<object>(null));

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
    ///     <item><description><see cref="ReflectionExtensions.Attributes(MemberInfo, Type)"/></description></item>
    ///     <item><description><see cref="ReflectionExtensions.Attributes{T}(MemberInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Attributes_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Attribute(null, typeof(object)));
      Assert.Throws<ArgumentNullException>(() => typeof(object).Attribute(null));
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Attributes<object>(null));

      Assert.False(typeof(TestObject).Attributes(typeof(NonSerializedAttribute)).Any());
      Assert.False(typeof(TestObject).Attributes<NonSerializedAttribute>().Any());

      Assert.True(typeof(TestObject).Attributes(typeof(SerializableAttribute)).SequenceEqual(new[] { new SerializableAttribute() }));
      Assert.True(typeof(TestObject).Attributes<SerializableAttribute>().SequenceEqual(new[] { new SerializableAttribute() }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionAttributesExtensions.Description(MemberInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void Description_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionAttributesExtensions.Description((MemberInfo) null));
      
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
    ///   <para>Performs testing of <see cref="ReflectionExtensions.IsPublic(PropertyInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsPublic_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsPublic(null));

      var type = typeof(TestObject);
      Assert.True(type.GetProperty("PublicProperty", BindingFlags.Public | BindingFlags.Instance).IsPublic());
      Assert.False(type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance).IsPublic());
      Assert.False(type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance).IsPublic());
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

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.AnyEvent(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyEvent_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.AnyEvent(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyEvent(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyEvent(string.Empty));

      var type = typeof(TestObject);
      Assert.NotNull(type.AnyEvent("PublicEvent"));
      Assert.NotNull(type.AnyEvent("ProtectedEvent"));
      Assert.NotNull(type.AnyEvent("PrivateEvent"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.AnyField(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyField_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.AnyField(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyField(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyField(string.Empty));

      var type = typeof(TestObject);

      type.AnyField("PublicStaticField").Do(field =>
      {
        Assert.True(field.IsPublic);
        Assert.True(field.IsStatic);
      });

      type.AnyField("ProtectedStaticField").Do(field =>
      {
        Assert.False(field.IsPrivate);
        Assert.True(field.IsStatic);
      });

      type.AnyField("PrivateStaticField").Do(field =>
      {
        Assert.True(field.IsPrivate);
        Assert.True(field.IsStatic);
      });

      type.AnyField("PublicField").Do(field =>
      {
        Assert.True(field.IsPublic);
        Assert.False(field.IsStatic);
      });

      type.AnyField("ProtectedField").Do(field =>
      {
        Assert.False(field.IsPrivate);
        Assert.False(field.IsStatic);
      });

      type.AnyField("PrivateField").Do(field =>
      {
        Assert.True(field.IsPrivate);
        Assert.False(field.IsStatic);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.AnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyMethod_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.AnyMethod(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyMethod(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyMethod(string.Empty));

      var type = typeof(TestObject);

      type.AnyMethod("PublicStaticMethod").Do(method =>
      {
        Assert.True(method.IsPublic);
        Assert.True(method.IsStatic);
      });

      type.AnyMethod("ProtectedStaticMethod").Do(method =>
      {
        Assert.False(method.IsPrivate);
        Assert.True(method.IsStatic);
      });

      type.AnyMethod("PrivateStaticMethod").Do(method =>
      {
        Assert.True(method.IsPrivate);
        Assert.True(method.IsStatic);
      });

      type.AnyMethod("PublicMethod").Do(method =>
      {
        Assert.True(method.IsPublic);
        Assert.False(method.IsStatic);
      });

      type.AnyMethod("ProtectedMethod").Do(method =>
      {
        Assert.False(method.IsPrivate);
        Assert.False(method.IsStatic);
      });

      type.AnyMethod("PrivateMethod").Do(method =>
      {
        Assert.True(method.IsPrivate);
        Assert.False(method.IsStatic);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.AnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyProperty_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.AnyProperty(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyProperty(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyProperty(string.Empty));

      var type = typeof(TestObject);

      type.AnyProperty("PublicStaticProperty").Do(property => Assert.True(property.IsPublic()));
      type.AnyProperty("ProtectedStaticProperty").Do(property => Assert.False(property.IsPublic()));
      type.AnyProperty("PrivateStaticProperty").Do(property => Assert.False(property.IsPublic()));
      type.AnyProperty("PublicProperty").Do(property => Assert.True(property.IsPublic()));
      type.AnyProperty("ProtectedProperty").Do(property => Assert.False(property.IsPublic()));
      type.AnyProperty("PrivateProperty").Do(property => Assert.False(property.IsPublic()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.DefaultConstructor(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void DefaultConstructor()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.DefaultConstructor(null));

      Assert.NotNull(typeof(TestObject).DefaultConstructor());
      Assert.Null(typeof(string).DefaultConstructor());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.HasField(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void HasField_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.HasField(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).HasField(null));
      Assert.Throws<ArgumentException>(() => typeof(object).HasField(string.Empty));

      Assert.False(typeof(object).HasField("field"));

      var subject = typeof(TestObject);
      Assert.True(subject.HasField("PublicStaticField"));
      Assert.True(subject.HasField("ProtectedStaticField"));
      Assert.True(subject.HasField("PrivateStaticField"));
      Assert.True(subject.HasField("PublicField"));
      Assert.True(subject.HasField("ProtectedField"));
      Assert.True(subject.HasField("PrivateField"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.HasMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void HasMethod_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.HasMethod(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).HasMethod(null));
      Assert.Throws<ArgumentException>(() => typeof(object).HasMethod(string.Empty));

      Assert.False(typeof(object).HasMethod("method"));

      var subject = typeof(TestObject);
      Assert.True(subject.HasMethod("PublicStaticMethod"));
      Assert.True(subject.HasMethod("ProtectedStaticMethod"));
      Assert.True(subject.HasMethod("PrivateStaticMethod"));
      Assert.True(subject.HasMethod("PublicMethod"));
      Assert.True(subject.HasMethod("ProtectedMethod"));
      Assert.True(subject.HasMethod("PrivateMethod"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.HasProperty(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void HasProperty_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.HasProperty(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).HasProperty(null));
      Assert.Throws<ArgumentException>(() => typeof(object).HasProperty(string.Empty));

      Assert.False(typeof(object).HasProperty("property"));

      var subject = typeof(TestObject);
      Assert.True(subject.HasProperty("PublicStaticProperty"));
      Assert.True(subject.HasProperty("ProtectedStaticProperty"));
      Assert.True(subject.HasProperty("PrivateStaticProperty"));
      Assert.True(subject.HasProperty("PublicProperty"));
      Assert.True(subject.HasProperty("ProtectedProperty"));
      Assert.True(subject.HasProperty("PrivateProperty"));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ReflectionExtensions.Implements(Type, Type)"/></description></item>
    ///     <item><description><see cref="ReflectionExtensions.Implements{INTERFACE}(Type)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Implements_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Implements(null, typeof(ICloneable)));
      Assert.Throws<ArgumentNullException>(() => typeof(object).Implements(null));
      Assert.Throws<ArgumentException>(() => typeof(object).Implements(typeof(object)));
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Implements<ICloneable>(null));
      Assert.Throws<ArgumentException>(() => typeof(object).Implements<object>());

      Assert.False(typeof(TestObject).Implements(typeof(ICloneable)));
      Assert.False(typeof(TestObject).Implements<ICloneable>());

      Assert.True(typeof(TestObject).Implements(typeof(IDisposable)));
      Assert.True(typeof(TestObject).Implements<IDisposable>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.Inherits(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void Inherits_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Inherits(null));

      Assert.False(typeof(object).Inherits().Any());

      var types = typeof(string).Inherits().ToArray();
      Assert.True(types.Contains(typeof(IComparable)));
      Assert.True(types.Contains(typeof(ICloneable)));
      Assert.True(types.Contains(typeof(IConvertible)));
      Assert.True(types.Contains(typeof(IComparable<string>)));
      Assert.True(types.Contains(typeof(IEnumerable<char>)));
      Assert.True(types.Contains(typeof(IEnumerable)));
      Assert.True(types.Contains(typeof(IEquatable<string>)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.IsAnonymous(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsAnonymous_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsAnonymous(null));

      Assert.False(typeof(object).IsAnonymous());
      Assert.True(new { }.GetType().IsAnonymous());
      Assert.True(new { property = "value" }.GetType().IsAnonymous());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.IsAssignableTo{T}(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsAssignableTo_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsAssignableTo<object>(null));

      Assert.True(typeof(object).IsAssignableTo<object>());
      Assert.True(typeof(string).IsAssignableTo<object>());
      Assert.False(typeof(object).IsAssignableTo<string>());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ReflectionExtensions.NewInstance(Type, object[])"/></description></item>
    ///     <item><description><see cref="ReflectionExtensions.NewInstance(Type, IEnumerable{KeyValuePair{string, object}})"/></description></item>
    ///     <item><description><see cref="ReflectionExtensions.NewInstance(Type, object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void NewInstance_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.NewInstance(null));
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.NewInstance(null, Enumerable.Empty<KeyValuePair<string, object>>()));
      Assert.Throws<ArgumentNullException>(() => typeof(object).NewInstance((IDictionary<string, object>) null));
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.NewInstance(null, new object()));
      Assert.Throws<ArgumentNullException>(() => typeof(object).NewInstance((object) null));
      
      Assert.NotNull(typeof(TestObject).NewInstance());
      Assert.NotNull(typeof(TestObject).NewInstance(Enumerable.Empty<KeyValuePair<string, object>>()));
      Assert.Throws<MissingMethodException>(() => typeof(TestObject).NewInstance(new object(), new object()));

      Assert.Equal("value", typeof(TestObject).NewInstance(new object[] { "value" }).To<TestObject>().PublicProperty);
      Assert.Equal("value", typeof(TestObject).NewInstance(new Dictionary<string, object> { { "PublicProperty", "value" } }).To<TestObject>().PublicProperty.ToString());
      Assert.Equal("value", typeof(TestObject).NewInstance(new { PublicProperty = "value" }).To<TestObject>().PublicProperty);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ReflectionExtensions.Properties(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void Properties_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Properties(null));

      var type = typeof(TestObject);
      var properties = type.Properties();
      Assert.True(properties.Contains(type.GetProperty("PublicProperty")));
      Assert.True(properties.Contains(type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance)));
      Assert.True(properties.Contains(type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance)));
      Assert.True(properties.Contains(type.GetProperty("PublicStaticProperty", BindingFlags.Public | BindingFlags.Static)));
      Assert.True(properties.Contains(type.GetProperty("ProtectedStaticProperty", BindingFlags.NonPublic | BindingFlags.Static)));
      Assert.True(properties.Contains(type.GetProperty("PrivateStaticProperty", BindingFlags.NonPublic | BindingFlags.Static)));
    }

    [Description("Enumeration")]
    private enum MockEnumeration
    {
      [Description("FirstOption")]
      First,

      Second,

      [Description("ThirdOption")]
      Third
    }
  }
}