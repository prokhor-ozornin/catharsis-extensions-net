using System.ComponentModel;
using System.Reflection;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ReflectionExtensions"/>.</para>
/// </summary>
public sealed class ReflectionExtensionsTest : UnitTest
{
  private delegate int Increment(int value);
  private delegate int Decrement(int value);

  private Delegate IncrementDelegate { get; }
  private Delegate DecrementDelegate { get; }

  private delegate string AsString(object subject);

  public ReflectionExtensionsTest()
  {
    IncrementDelegate = Delegate.CreateDelegate(typeof(Increment), GetType().Method("IncrementValue"));
    DecrementDelegate = Delegate.CreateDelegate(typeof(Decrement), GetType().Method("DecrementValue"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.Resource(Assembly, string, Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Assembly_Resource_Method()
  {
    AssertionExtensions.Should(() => Assembly.GetExecutingAssembly().Resource(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    Assembly.GetExecutingAssembly().Resource("invalid").Should().BeNull();
    Assembly.GetExecutingAssembly().Resource("Catharsis.Commons.Resource.txt").Should().Be("resource");

    // TODO Encoding support

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.And(Delegate, Delegate)"/> method.</para>
  /// </summary>
  [Fact]
  public void Delegate_And_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.And(null!, IncrementDelegate)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => IncrementDelegate.And(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => IncrementDelegate.And(DecrementDelegate)).ThrowExactly<ArgumentException>();

    var andDelegate = IncrementDelegate.And(IncrementDelegate);
    andDelegate.Should().BeOfType<MulticastDelegate>();
    andDelegate.Method.Should().Equals(GetType().Method("IncrementValue")).Should().BeTrue();
    andDelegate.Target.Should().BeNull();
    andDelegate.GetInvocationList().Should().Equal(IncrementDelegate, IncrementDelegate);
    andDelegate.DynamicInvoke(0).As<int>().Should().Be(1);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.Not(Delegate, Delegate?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Delegate_Not_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.Not(null!, IncrementDelegate)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => IncrementDelegate.Not(DecrementDelegate)).ThrowExactly<ArgumentException>();

    IncrementDelegate.Not(null).Should().BeSameAs(IncrementDelegate);
    IncrementDelegate.Not(IncrementDelegate).Should().BeNull();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsArray{T}(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_IsArray_Method()
  {
    AssertionExtensions.Should(() => typeof(object[]).IsArray<object>()).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsAssignableTo{T}(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_IsAssignableTo_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.IsAssignableTo<object>(null!)).ThrowExactly<ArgumentNullException>();

    typeof(object).IsAssignableTo<object>().Should().BeTrue();
    typeof(string).IsAssignableTo<object>().Should().BeTrue();
    typeof(object).IsAssignableTo<string>().Should().BeFalse();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsAnonymous(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_IsAnonymous_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.IsAnonymous(null!)).ThrowExactly<ArgumentNullException>();

    typeof(object).IsAnonymous().Should().BeFalse();
    new
    {
    }.GetType().IsAnonymous().Should().BeTrue();
    new
    {
      property = "value"
    }.GetType().IsAnonymous().Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ReflectionExtensions.Implements(Type, Type)"/></description></item>
  ///     <item><description><see cref="ReflectionExtensions.Implements{T}(Type)"/></description></item>
  ///     <item><description><see cref="ReflectionExtensions.Implements(Type)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Type_Implements_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Implements(null!, typeof(object))).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => typeof(object).Implements(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Implements<object>(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Implements(null!)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ReflectionExtensions.Properties(Type)"/></description></item>
  ///     <item><description><see cref="ReflectionExtensions.Properties(Type, object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Type_Properties_Methods()
  {
    /*using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Properties(null!)).ThrowExactly<ArgumentNullException>();

      var type = typeof(TestObject);
      var properties = type.Properties();
      properties.Should().Contain(type.GetProperty("PublicProperty"));
      properties.Should().Contain(type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance));
      properties.Should().Contain(type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance));
      properties.Should().Contain(type.GetProperty("PublicStaticProperty", BindingFlags.Public | BindingFlags.Static));
      properties.Should().Contain(type.GetProperty("ProtectedStaticProperty", BindingFlags.NonPublic | BindingFlags.Static));
      properties.Should().Contain(type.GetProperty("PrivateStaticProperty", BindingFlags.NonPublic | BindingFlags.Static));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Properties(null!, new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => typeof(object).Properties(null!)).ThrowExactly<ArgumentNullException>();
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ReflectionExtensions.Instance(Type, object?[]?)"/></description></item>
  ///     <item><description><see cref="ReflectionExtensions.Instance(Type, object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Type_Instance_Methods()
  {
    /*using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Instance(null!)).ThrowExactly<ArgumentNullException>();

      typeof(TestObject).Instance().Should().NotBeNull();
      typeof(TestObject).Instance(Enumerable.Empty<KeyValuePair<string, object>>().Should().NotBeNull());
      AssertionExtensions.Should(() => typeof(TestObject).Instance(new object(), new object())).ThrowExactly<MissingMethodException>();

      typeof(TestObject).Instance(new object[] {"value"}).To<TestObject>().PublicProperty.Should().Be("value");
      typeof(TestObject).Instance(new Dictionary<string, object> {{"PublicProperty", "value"}}).To<TestObject>().PublicProperty.ToString().Should().Be("value");

      typeof(TestObject).Instance(new
      {
        PublicProperty = "value"
      }).As<TestObject>().PublicProperty.Should().Be("value");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Instance(null!, new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => typeof(object).Instance((object) null!)).ThrowExactly<ArgumentNullException>();

    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.Event(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_Event_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.Event(null!, "name")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).Event(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).Event(string.Empty)).ThrowExactly<ArgumentException>();

    var type = typeof(TestObject);
    type.Event("PublicEvent").Should().NotBeNull();
    type.Event("ProtectedEvent").Should().NotBeNull();
    type.Event("PrivateEvent").Should().NotBeNull();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.Field(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_Field_Method()
  {
    AssertionExtensions.Should(() => ReflectionExtensions.Field(null!, "name")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).Field(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).Field(string.Empty)).ThrowExactly<ArgumentException>();

    /*var type = typeof(TestObject);

    type.Field("PublicStaticField").Use(field =>
    {
      field.IsPublic.Should().BeTrue();
      field.IsStatic.Should().BeTrue();
    });

    type.Field("ProtectedStaticField").Use(field =>
    {
      field.IsPrivate.Should().BeFalse();
      field.IsStatic.Should().BeTrue();
    });

    type.Field("PrivateStaticField").Use(field =>
    {
      field.IsPrivate.Should().BeTrue();
      field.IsStatic.Should().BeTrue();
    });

    type.Field("PublicField").Use(field =>
    {
      field.IsPublic.Should().BeTrue();
      field.IsStatic.Should().BeFalse();
    });

    type.Field("ProtectedField").Use(field =>
    {
      field.IsPrivate.Should().BeFalse();
      field.IsStatic.Should().BeFalse();
    });

    type.Field("PrivateField").Use(field =>
    {
      field.IsPrivate.Should().BeTrue();
      field.IsStatic.Should().BeFalse();
    });*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.Method(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_Method_Method()
  {
    AssertionExtensions.Should(() => ReflectionExtensions.Method(null!, "name")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).Method(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).Method(string.Empty)).ThrowExactly<ArgumentException>();

    /*var type = typeof(TestObject);

    type.Method("PublicStaticMethod").Use(method =>
    {
      method.IsPublic.Should().BeTrue();
      method.IsStatic.Should().BeTrue();
    });

    type.Method("ProtectedStaticMethod").Use(method =>
    {
      method.IsPrivate.Should().BeFalse();
      method.IsStatic.Should().BeTrue();
    });

    type.Method("PrivateStaticMethod").Use(method =>
    {
      method.IsPrivate.Should().BeTrue();
      method.IsStatic.Should().BeTrue();
    });

    type.Method("PublicMethod").Use(method =>
    {
      method.IsPublic.Should().BeTrue();
      method.IsStatic.Should().BeFalse();
    });

    type.Method("ProtectedMethod").Use(method =>
    {
      method.IsPrivate.Should().BeFalse();
      method.IsStatic.Should().BeFalse();
    });

    type.Method("PrivateMethod").Use(method =>
    {
      method.IsPrivate.Should().BeTrue();
      method.IsStatic.Should().BeFalse();
    });*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.Property(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_Property_Method()
  {
    AssertionExtensions.Should(() => ReflectionExtensions.Property(null!, "name")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).Property(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).Property(string.Empty)).ThrowExactly<ArgumentException>();

    var type = typeof(TestObject);

    type.Property("PublicStaticProperty").Use(property => property.IsPublic().Should().BeTrue());
    type.Property("ProtectedStaticProperty").Use(property => property.IsPublic().Should().BeFalse());
    type.Property("PrivateStaticProperty").Use(property => property.IsPublic().Should().BeFalse());
    type.Property("PublicProperty").Use(property => property.IsPublic().Should().BeTrue());
    type.Property("ProtectedProperty").Use(property => property.IsPublic().Should().BeFalse());
    type.Property("PrivateProperty").Use(property => property.IsPublic().Should().BeFalse());

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.Constructor(Type, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_Constructor_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.Constructor(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => ReflectionExtensions.Constructor(null!, Array.Empty<Type>())).ThrowExactly<ArgumentNullException>();

    typeof(TestObject).Constructor().Should().NotBeNull();
    typeof(string).Constructor().Should().BeNull();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.HasField(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_HasField_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.HasField(null!, "name")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).HasField(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).HasField(string.Empty)).ThrowExactly<ArgumentException>();

    typeof(object).HasField("field").Should().BeFalse();

    var subject = typeof(TestObject);
    subject.HasField("PublicStaticField").Should().BeTrue();
    subject.HasField("ProtectedStaticField").Should().BeTrue();
    subject.HasField("PrivateStaticField").Should().BeTrue();
    subject.HasField("PublicField").Should().BeTrue();
    subject.HasField("ProtectedField").Should().BeTrue();
    subject.HasField("PrivateField").Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.HasMethod(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_HasMethod_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.HasMethod(null!, "name")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).HasMethod(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).HasMethod(string.Empty)).ThrowExactly<ArgumentException>();

    typeof(object).HasMethod("method").Should().BeFalse();

    var subject = typeof(TestObject);
    subject.HasMethod("PublicStaticMethod").Should().BeTrue();
    subject.HasMethod("ProtectedStaticMethod").Should().BeTrue();
    subject.HasMethod("PrivateStaticMethod").Should().BeTrue();
    subject.HasMethod("PublicMethod").Should().BeTrue();
    subject.HasMethod("ProtectedMethod").Should().BeTrue();
    subject.HasMethod("PrivateMethod").Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.HasProperty(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Type_HasProperty_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.HasProperty(null!, "name")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).HasProperty(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => typeof(object).HasProperty(string.Empty)).ThrowExactly<ArgumentException>();

    typeof(object).HasProperty("property").Should().BeFalse();

    var subject = typeof(TestObject);
    subject.HasProperty("PublicStaticProperty").Should().BeTrue();
    subject.HasProperty("ProtectedStaticProperty").Should().BeTrue();
    subject.HasProperty("PrivateStaticProperty").Should().BeTrue();
    subject.HasProperty("PublicProperty").Should().BeTrue();
    subject.HasProperty("ProtectedProperty").Should().BeTrue();
    subject.HasProperty("PrivateProperty").Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.Type(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void MemberInfo_Type_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.Type(null!)).ThrowExactly<ArgumentNullException>();

    var type = typeof(TestObject);

    var eventMember = type.Event("PublicEvent");
    eventMember.Type().Should().Be(eventMember.EventHandlerType);

    var fieldMember = type.Field("PublicField");
    fieldMember.Type().Should().Be(fieldMember.FieldType);

    var methodMember = type.Method("PublicMethod");
    methodMember.Type().Should().Be(methodMember.ReturnType);

    var propertyMember = type.Property("PublicProperty");
    propertyMember.Type().Should().Be(propertyMember.PropertyType);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsConstructor(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void MemberInfo_IsConstructor_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.IsConstructor(null!)).ThrowExactly<ArgumentNullException>();

    typeof(TestObject).Constructor().As<MemberInfo>().IsConstructor().Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsEvent(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void MemberInfo_IsEvent_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.IsEvent(null!)).ThrowExactly<ArgumentNullException>();

    typeof(TestObject).Event("PublicEvent").As<MemberInfo>().IsEvent().Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsField(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void MemberInfo_IsField_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.IsField(null!)).ThrowExactly<ArgumentNullException>();

    typeof(TestObject).Field("PublicField").As<MemberInfo>().IsField().Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsMethod(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void MemberInfo_IsMethod_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.IsMethod(null!)).ThrowExactly<ArgumentNullException>();

    typeof(TestObject).Method("PublicMethod").As<MemberInfo>().IsMethod().Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsProperty(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void MemberInfo_IsProperty_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.IsProperty(null!)).ThrowExactly<ArgumentNullException>();

    typeof(TestObject).Property("PublicProperty").As<MemberInfo>().IsProperty().Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ReflectionExtensions.Attribute(MemberInfo, Type)"/></description></item>
  ///     <item><description><see cref="ReflectionExtensions.Attribute{T}(MemberInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void MemberInfo_Attribute_Methods()
  {
    /*using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Attribute(null!, typeof(object))).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => typeof(object).Attribute(null!)).ThrowExactly<ArgumentNullException>();

      typeof(TestObject).Attribute(typeof(NonSerializedAttribute)).Should().BeNull();
      typeof(TestObject).Attribute<NonSerializedAttribute>().Should().BeNull();

      typeof(TestObject).Attribute(typeof(SerializableAttribute)).Should().BeOfType<SerializableAttribute>();
      typeof(TestObject).Attribute<SerializableAttribute>().Should().NotBeNull();


      var property = typeof(string).GetProperty("Length");
      property.Attribute(typeof(DescriptionAttribute)).Should().BeNull();
      property.Attribute<DescriptionAttribute>().Should().BeNull();

      property = typeof(TestObject).GetProperty("PublicProperty");
      property.Attribute(typeof(DescriptionAttribute)).Should().NotBeNull();
      property.Attribute<DescriptionAttribute>().Should().NotBeNull();


      var method = typeof(object).GetMethod("ToString");
      method.Attribute(typeof(DescriptionAttribute)).Should().BeNull();
      method.Attribute<DescriptionAttribute>().Should().BeNull();

      method = typeof(TestObject).GetMethod("PublicMethod");
      method.Attribute(typeof(DescriptionAttribute)).Should().NotBeNull();
      method.Attribute<DescriptionAttribute>().Should().NotBeNull();


      var field = typeof(string).GetField("Empty");
      field.Attribute(typeof(DescriptionAttribute)).Should().BeNull();
      field.Attribute<DescriptionAttribute>().Should().BeNull();

      field = typeof(TestObject).GetField("PublicField");
      field.Attribute(typeof(DescriptionAttribute)).Should().NotBeNull();
      field.Attribute<DescriptionAttribute>().Should().NotBeNull();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Attribute<object>(null!)).ThrowExactly<ArgumentNullException>();
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ReflectionExtensions.Attributes(MemberInfo, Type)"/></description></item>
  ///     <item><description><see cref="ReflectionExtensions.Attributes{T}(MemberInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void MemberInfo_Attributes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Attributes(null!, typeof(object))).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => typeof(object).Attributes(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Attributes<object>(null!)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ReflectionExtensions.IsPublic(PropertyInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void PropertyInfo_IsPublic_Method()
  {
    /*AssertionExtensions.Should(() => ReflectionExtensions.IsPublic(null!)).ThrowExactly<ArgumentNullException>();

    var type = typeof(TestObject);
    type.GetProperty("PublicProperty", BindingFlags.Public | BindingFlags.Instance).IsPublic().Should().BeTrue();
    type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance).IsPublic().Should().BeFalse();
    type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance).IsPublic().Should().BeFalse();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ReflectionExtensions.Delegate(MethodInfo, Type)"/></description></item>
  ///     <item><description><see cref="ReflectionExtensions.Delegate{T}(MethodInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void MethodInfo_Delegate_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Delegate(null!, typeof(object))).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => typeof(object).GetMethod("ToString").Delegate(null!)).ThrowExactly<ArgumentNullException>();

      var method = typeof(object).GetMethod("ToString");
      AssertionExtensions.Should(() => method.Delegate(typeof(object))).ThrowExactly<ArgumentException>();
      var methodDelegate = method.Delegate(typeof(AsString));
      methodDelegate.Method.Should().BeSameAs(method);
      methodDelegate.Target.Should().BeNull();
      AssertionExtensions.Should(() => methodDelegate.DynamicInvoke()).ThrowExactly<TargetParameterCountException>();
      AssertionExtensions.Should(() => methodDelegate.DynamicInvoke(new object(), new object())).ThrowExactly<TargetParameterCountException>();
      methodDelegate.DynamicInvoke("test").To<string>().Should().Be("test");
      method.Delegate(typeof(AsString)).Should().Be(method.Delegate<AsString>());
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ReflectionExtensions.Delegate<object>(null!)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
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

  private static int IncrementValue(int value) => value + 1;

  private static int DecrementValue(int value) => value - 1;
  
  [Serializable]
  private class TestObject : IDisposable, IEquatable<TestObject>
  {
    [Description]
    [DisplayName]
    private object ReadOnlyProperty => null;

    [Description("PublicStaticProperty Description")]
    [DisplayName("PublicStaticProperty")]
    public static object PublicStaticProperty
    {
      get; set;
    }

    protected static object ProtectedStaticProperty
    {
      get; set;
    }

    private static object PrivateStaticProperty
    {
      get; set;
    }

    [Description("PublicProperty")]
    public object PublicProperty
    {
      get; set;
    }

    [DisplayName("ProtectedProperty")]
    protected object ProtectedProperty
    {
      get; set;
    }

    private object PrivateProperty
    {
      get; set;
    }

    [Description]
    public static object PublicStaticField;

    protected static object ProtectedStaticField;

    private static object PrivateStaticField;

    [Description]
    public object PublicField;

    protected object ProtectedField;

    private object PrivateField;

    public delegate void PublicDelegate();

    protected delegate void ProtectedDelegate();

    private delegate void PrivateDelegate();

    public event PublicDelegate PublicEvent;

    protected event ProtectedDelegate ProtectedEvent;

    private event PrivateDelegate PrivateEvent;

    public TestObject()
    {
    }

    public TestObject(object publicProperty) => PublicProperty = publicProperty;

    public static void PublicStaticMethod()
    {
    }

    protected static void ProtectedStaticMethod()
    {
    }

    private static void PrivateStaticMethod()
    {
    }

    [Description]
    public void PublicMethod()
    {
    }

    protected void ProtectedMethod()
    {
    }

    private void PrivateMethod()
    {
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public virtual bool Equals(TestObject other) => /*this.Equality(instance => instance.PublicProperty, instance => instance.PublicProperty)*/throw new NotImplementedException();

    public override bool Equals(object other) => Equals(other as TestObject);

    public override int GetHashCode() => this.HashCode(nameof(PublicProperty), nameof(PublicProperty));
  }
}