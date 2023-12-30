using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TypeExtensions"/>.</para>
/// </summary>
public sealed class TypeExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.IsSealed(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsSealed_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.IsSealed(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.IsStatic(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsStatic_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.IsStatic(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.IsAssignableFrom{T}(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAssignableFrom_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.IsAssignableFrom<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.IsAssignableTo{T}(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsAssignableTo_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.IsAssignableTo<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    /*typeof(object).IsAssignableTo<object>().Should().BeTrue();
    typeof(string).IsAssignableTo<object>().Should().BeTrue();
    typeof(object).IsAssignableTo<string>().Should().BeFalse();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.IsArray{T}(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsArray_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.IsArray<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TypeExtensions.HasMethod(Type, string, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="TypeExtensions.HasMethod(Type, string, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IsDerivedFrom_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.IsDerivedFrom(null, typeof(object))).ThrowExactly<ArgumentNullException>().WithParameterName("type");
      AssertionExtensions.Should(() => typeof(object).IsDerivedFrom(null)).ThrowExactly<ArgumentNullException>().WithParameterName("baseType");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.IsDerivedFrom<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TypeExtensions.Implements{T}(Type)"/></description></item>
  ///     <item><description><see cref="TypeExtensions.Implements(Type, Type)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Implements_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.Implements(null, typeof(object))).ThrowExactly<ArgumentNullException>().WithParameterName("type");
      AssertionExtensions.Should(() => typeof(object).Implements(null)).ThrowExactly<ArgumentNullException>().WithParameterName("interfaceType");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.Implements<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.Implementations(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void Implementations_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.Implementations(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.Implementors(Type, Assembly)"/> method.</para>
  /// </summary>
  [Fact]
  public void Implementors_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.Implementors(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.HasField(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HasField_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.HasField(null, "name")).ThrowExactly<ArgumentNullException>().WithParameterName("type");
    AssertionExtensions.Should(() => typeof(object).HasField(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

    /*typeof(object).HasField("field").Should().BeFalse();

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
  ///   <para>Performs testing of <see cref="TypeExtensions.HasProperty(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HasProperty_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.HasProperty(null, "name")).ThrowExactly<ArgumentNullException>().WithParameterName("type");
    AssertionExtensions.Should(() => typeof(object).HasProperty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

    /*typeof(object).HasProperty("property").Should().BeFalse();

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
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TypeExtensions.HasMethod(Type, string, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="TypeExtensions.HasMethod(Type, string, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void HasMethod_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.HasMethod(null, "name")).ThrowExactly<ArgumentNullException>().WithParameterName("type");
      AssertionExtensions.Should(() => typeof(object).HasMethod(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

      /*typeof(object).HasMethod("method").Should().BeFalse();

      var subject = typeof(TestObject);
      subject.HasMethod("PublicStaticMethod").Should().BeTrue();
      subject.HasMethod("ProtectedStaticMethod").Should().BeTrue();
      subject.HasMethod("PrivateStaticMethod").Should().BeTrue();
      subject.HasMethod("PublicMethod").Should().BeTrue();
      subject.HasMethod("ProtectedMethod").Should().BeTrue();
      subject.HasMethod("PrivateMethod").Should().BeTrue();*/

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.HasMethod(null, "name", [])).ThrowExactly<ArgumentNullException>().WithParameterName("type");
      AssertionExtensions.Should(() => typeof(object).HasMethod(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("name");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.AnyEvent(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void AnyEvent_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.AnyEvent(null, "name")).ThrowExactly<ArgumentNullException>().WithParameterName("type");
    AssertionExtensions.Should(() => typeof(object).AnyEvent(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

    /*var type = typeof(TestObject);
    type.Event("PublicEvent").Should().NotBeNull();
    type.Event("ProtectedEvent").Should().NotBeNull();
    type.Event("PrivateEvent").Should().NotBeNull();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.AnyField(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void AnyField_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.AnyField(null, "name")).ThrowExactly<ArgumentNullException>().WithParameterName("type");
    AssertionExtensions.Should(() => typeof(object).AnyField(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

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
  ///   <para>Performs testing of <see cref="TypeExtensions.AnyProperty(Type, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void AnyProperty_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.AnyProperty(null, "name")).ThrowExactly<ArgumentNullException>().WithParameterName("type");
    AssertionExtensions.Should(() => typeof(object).AnyProperty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

    //var type = typeof(TestObject);

    //ReflectionExtensions.AnyProperty(type, "PublicStaticProperty").With(property => property.IsPublic().Should().BeTrue());
    //ReflectionExtensions.AnyProperty(type, "ProtectedStaticProperty").With(property => property.IsPublic().Should().BeFalse());
    //ReflectionExtensions.AnyProperty(type, "PrivateStaticProperty").With(property => property.IsPublic().Should().BeFalse());
    //ReflectionExtensions.AnyProperty(type, "PublicProperty").With(property => property.IsPublic().Should().BeTrue());
    //ReflectionExtensions.AnyProperty(type, "ProtectedProperty").With(property => property.IsPublic().Should().BeFalse());
    //ReflectionExtensions.AnyProperty(type, "PrivateProperty").With(property => property.IsPublic().Should().BeFalse());

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TypeExtensions.AnyMethod(Type, string, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="TypeExtensions.AnyMethod(Type, string, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void AnyMethod_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.AnyMethod(null, "name")).ThrowExactly<ArgumentNullException>().WithParameterName("type");
      AssertionExtensions.Should(() => typeof(object).AnyMethod(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.AnyMethod(null, "name", [])).ThrowExactly<ArgumentNullException>().WithParameterName("type");
      AssertionExtensions.Should(() => typeof(object).AnyMethod(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("name");
    }
    
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TypeExtensions.HasConstructor(Type, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="TypeExtensions.HasConstructor(Type, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void HasConstructor_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.HasConstructor(null, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("type");
      AssertionExtensions.Should(() => typeof(object).HasConstructor((IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("arguments");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.HasConstructor(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("type");
      AssertionExtensions.Should(() => typeof(object).HasConstructor(null)).ThrowExactly<ArgumentNullException>().WithParameterName("arguments");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TypeExtensions.HasDefaultConstructor(Type)"/> method.</para>
  /// </summary>
  [Fact]
  public void HasDefaultConstructor_Method()
  {
    AssertionExtensions.Should(() => TypeExtensions.HasDefaultConstructor(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    /*typeof(TestObject).Constructor().Should().NotBeNull();
    typeof(string).Constructor().Should().BeNull();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TypeExtensions.Instance{T}(Type, IEnumerable{object})"/></description></item>
  ///     <item><description><see cref="TypeExtensions.Instance{T}(Type, object[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Instance_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.Instance<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("type");

      /*typeof(TestObject).Instance().Should().NotBeNull();
      typeof(TestObject).Instance(Enumerable.Empty<KeyValuePair<string, object>>().Should().NotBeNull());
      AssertionExtensions.Should(() => typeof(TestObject).Instance(new object(), new object())).ThrowExactly<MissingMethodException>();

      typeof(TestObject).Instance("value").To<TestObject>().PublicProperty.Should().Be("value");
      typeof(TestObject).Instance(new Dictionary<string, object> { { "PublicProperty", "value" } }).To<TestObject>().PublicProperty.ToString().Should().Be("value");

      typeof(TestObject).Instance(new
      {
        PublicProperty = "value"
      }).As<TestObject>().PublicProperty.Should().Be("value");*/
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TypeExtensions.Instance<object>(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("type");

    }

    throw new NotImplementedException();
  }
}