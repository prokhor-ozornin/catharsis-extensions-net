using System.ComponentModel;
using System.Reflection;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="MemberInfoExtensions"/>.</para>
/// </summary>
public sealed class MemberInfoExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsEvent(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEvent_Method()
  {
    AssertionExtensions.Should(() => MemberInfoExtensions.IsEvent(null)).ThrowExactly<ArgumentNullException>().WithParameterName("member");

    //typeof(TestObject).Event("PublicEvent").As<MemberInfo>().IsEvent().Should().BeTrue();

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsField(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsField_Method()
  {
    AssertionExtensions.Should(() => MemberInfoExtensions.IsField(null)).ThrowExactly<ArgumentNullException>().WithParameterName("member");

    //typeof(TestObject).Field("PublicField").As<MemberInfo>().IsField().Should().BeTrue();

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsProperty(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsProperty_Method()
  {
    AssertionExtensions.Should(() => MemberInfoExtensions.IsProperty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("member");

    //typeof(TestObject).Property("PublicProperty").As<MemberInfo>().IsProperty().Should().BeTrue();

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsMethod(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsMethod_Method()
  {
    AssertionExtensions.Should(() => MemberInfoExtensions.IsMethod(null)).ThrowExactly<ArgumentNullException>().WithParameterName("member");

    //typeof(TestObject).Method("PublicMethod").As<MemberInfo>().IsMethod().Should().BeTrue();

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MemberInfoExtensions.IsConstructor(MemberInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsConstructor_Method()
  {
    AssertionExtensions.Should(() => MemberInfoExtensions.IsConstructor(null)).ThrowExactly<ArgumentNullException>().WithParameterName("member");
    
    //typeof(TestObject).Constructor().As<MemberInfo>().IsConstructor().Should().BeTrue();

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="MemberInfoExtensions.Attribute{T}(MemberInfo)"/></description></item>
  ///     <item><description><see cref="MemberInfoExtensions.Attribute(MemberInfo, Type)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Attribute_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => MemberInfoExtensions.Attribute<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("member");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => MemberInfoExtensions.Attribute(null, typeof(object))).ThrowExactly<ArgumentNullException>().WithParameterName("member");
      AssertionExtensions.Should(() => typeof(object).Attribute(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

      /*typeof(TestObject).Attribute(typeof(NonSerializedAttribute)).Should().BeNull();
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
      field.Attribute<DescriptionAttribute>().Should().NotBeNull();*/
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="MemberInfoExtensions.Attributes{T}(MemberInfo)"/></description></item>
  ///     <item><description><see cref="MemberInfoExtensions.Attributes(MemberInfo, Type)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Attributes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => MemberInfoExtensions.Attributes<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("member");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => MemberInfoExtensions.Attributes(null, typeof(object))).ThrowExactly<ArgumentNullException>().WithParameterName("member");
      AssertionExtensions.Should(() => typeof(object).Attributes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

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