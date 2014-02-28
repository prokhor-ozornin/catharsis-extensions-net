using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TypeExtensions"/>.</para>
  /// </summary>
  public sealed class TypeExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.AllProperties(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void AllProperties_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.AllProperties(null));

      var type = typeof(TestObject);
      var properties = type.AllProperties();
      Assert.True(properties.Contains(type.GetProperty("PublicProperty")));
      Assert.True(properties.Contains(type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance)));
      Assert.True(properties.Contains(type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance)));
      Assert.True(properties.Contains(type.GetProperty("PublicStaticProperty", BindingFlags.Public | BindingFlags.Static)));
      Assert.True(properties.Contains(type.GetProperty("ProtectedStaticProperty", BindingFlags.NonPublic | BindingFlags.Static)));
      Assert.True(properties.Contains(type.GetProperty("PrivateStaticProperty", BindingFlags.NonPublic | BindingFlags.Static)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.AnyEvent(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyEvent_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.AnyEvent(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyEvent(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyEvent(string.Empty));

      var type = typeof(TestObject);
      Assert.NotNull(type.AnyEvent("PublicEvent"));
      Assert.NotNull(type.AnyEvent("ProtectedEvent"));
      Assert.NotNull(type.AnyEvent("PrivateEvent"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.AnyField(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyField_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.AnyField(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyField(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyField(string.Empty));

      var type = typeof(TestObject);

      type.AnyField("PublicStaticField").With(field =>
      {
        Assert.True(field.IsPublic);
        Assert.True(field.IsStatic);
      });

      type.AnyField("ProtectedStaticField").With(field =>
      {
        Assert.False(field.IsPrivate);
        Assert.True(field.IsStatic);
      });

      type.AnyField("PrivateStaticField").With(field =>
      {
        Assert.True(field.IsPrivate);
        Assert.True(field.IsStatic);
      });

      type.AnyField("PublicField").With(field =>
      {
        Assert.True(field.IsPublic);
        Assert.False(field.IsStatic);
      });

      type.AnyField("ProtectedField").With(field =>
      {
        Assert.False(field.IsPrivate);
        Assert.False(field.IsStatic);
      });

      type.AnyField("PrivateField").With(field =>
      {
        Assert.True(field.IsPrivate);
        Assert.False(field.IsStatic);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.AnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyMethod_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.AnyMethod(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyMethod(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyMethod(string.Empty));

      var type = typeof(TestObject);

      type.AnyMethod("PublicStaticMethod").With(method =>
      {
        Assert.True(method.IsPublic);
        Assert.True(method.IsStatic);
      });

      type.AnyMethod("ProtectedStaticMethod").With(method =>
      {
        Assert.False(method.IsPrivate);
        Assert.True(method.IsStatic);
      });

      type.AnyMethod("PrivateStaticMethod").With(method =>
      {
        Assert.True(method.IsPrivate);
        Assert.True(method.IsStatic);
      });

      type.AnyMethod("PublicMethod").With(method =>
      {
        Assert.True(method.IsPublic);
        Assert.False(method.IsStatic);
      });

      type.AnyMethod("ProtectedMethod").With(method =>
      {
        Assert.False(method.IsPrivate);
        Assert.False(method.IsStatic);
      });

      type.AnyMethod("PrivateMethod").With(method =>
      {
        Assert.True(method.IsPrivate);
        Assert.False(method.IsStatic);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.AnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyProperty_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.AnyProperty(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyProperty(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyProperty(string.Empty));

      var type = typeof(TestObject);

      type.AnyProperty("PublicStaticProperty").With(property => Assert.True(property.IsPublic()));
      type.AnyProperty("ProtectedStaticProperty").With(property => Assert.False(property.IsPublic()));
      type.AnyProperty("PrivateStaticProperty").With(property => Assert.False(property.IsPublic()));
      type.AnyProperty("PublicProperty").With(property => Assert.True(property.IsPublic()));
      type.AnyProperty("ProtectedProperty").With(property => Assert.False(property.IsPublic()));
      type.AnyProperty("PrivateProperty").With(property => Assert.False(property.IsPublic()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.DefaultConstructor(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void DefaultConstructor()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.DefaultConstructor(null));

      Assert.NotNull(typeof(TestObject).DefaultConstructor());
      Assert.Null(typeof(string).DefaultConstructor());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="TypeExtensions.Implements(Type, Type)"/></description></item>
    ///     <item><description><see cref="TypeExtensions.Implements{INTERFACE}(Type)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Implements_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.Implements(null, typeof(ICloneable)));
      Assert.Throws<ArgumentNullException>(() => typeof(object).Implements(null));
      Assert.Throws<ArgumentException>(() => typeof(object).Implements(typeof(object)));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.Implements<ICloneable>(null));
      Assert.Throws<ArgumentException>(() => typeof(object).Implements<object>());

      Assert.False(typeof(TestObject).Implements(typeof(ICloneable)));
      Assert.False(typeof(TestObject).Implements<ICloneable>());

      Assert.True(typeof(TestObject).Implements(typeof(IDisposable)));
      Assert.True(typeof(TestObject).Implements<IDisposable>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.Inherits(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void Inherits_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.Inherits(null));

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
    ///   <para>Performs testing of <see cref="TypeExtensions.IsAssignableTo{T}(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsAssignableTo_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsAssignableTo<object>(null));

      Assert.True(typeof(object).IsAssignableTo<object>());
      Assert.True(typeof(string).IsAssignableTo<object>());
      Assert.False(typeof(object).IsAssignableTo<string>());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="TypeExtensions.NewInstance(Type, object[])"/></description></item>
    ///     <item><description><see cref="TypeExtensions.NewInstance(Type, IEnumerable{KeyValuePair{string, object}})"/></description></item>
    ///     <item><description><see cref="TypeExtensions.NewInstance(Type, object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void NewInstance_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.NewInstance(null));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.NewInstance(null, Enumerable.Empty<KeyValuePair<string, object>>()));
      Assert.Throws<ArgumentNullException>(() => typeof(object).NewInstance((IEnumerable<KeyValuePair<string, object>>) null));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.NewInstance(null, new object()));
      Assert.Throws<ArgumentNullException>(() => typeof(object).NewInstance((object) null));
      
      Assert.NotNull(typeof(TestObject).NewInstance());
      Assert.NotNull(typeof(TestObject).NewInstance(Enumerable.Empty<KeyValuePair<string, object>>()));
      Assert.Throws<MissingMethodException>(() => typeof(TestObject).NewInstance(new object(), new object()));

      Assert.Equal("value", typeof(TestObject).NewInstance(new object[] { "value" }).To<TestObject>().PublicProperty);
      Assert.Equal("value", typeof(TestObject).NewInstance(new Dictionary<string, object>().AddNext("PublicProperty", "value")).To<TestObject>().PublicProperty.ToString());
      Assert.Equal("value", typeof(TestObject).NewInstance(new { PublicProperty = "value" }).To<TestObject>().PublicProperty);
    }
  }
}