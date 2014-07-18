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
    ///   <para>Performs testing of <see cref="TypeExtensions.AnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyMethod_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.AnyMethod(null, "name"));
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
    ///   <para>Performs testing of <see cref="TypeExtensions.AnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void AnyProperty_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.AnyProperty(null, "name"));
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
    ///   <para>Performs testing of <see cref="TypeExtensions.HasField(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void HasField_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.HasField(null, "name"));
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
    ///   <para>Performs testing of <see cref="TypeExtensions.HasMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void HasMethod_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.HasMethod(null, "name"));
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
    ///   <para>Performs testing of <see cref="TypeExtensions.HasProperty(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void HasProperty_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.HasProperty(null, "name"));
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
    ///   <para>Performs testing of <see cref="TypeExtensions.IsAnonymous(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsAnonymous_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsAnonymous(null));

      Assert.False(typeof(object).IsAnonymous());
      Assert.True(new { }.GetType().IsAnonymous());
      Assert.True(new { property = "value" }.GetType().IsAnonymous());
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
      Assert.Equal("value", typeof(TestObject).NewInstance(new Dictionary<string, object> { { "PublicProperty", "value" } }).To<TestObject>().PublicProperty.ToString());
      Assert.Equal("value", typeof(TestObject).NewInstance(new { PublicProperty = "value" }).To<TestObject>().PublicProperty);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.Properties(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void Properties_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.Properties(null));

      var type = typeof(TestObject);
      var properties = type.Properties();
      Assert.True(properties.Contains(type.GetProperty("PublicProperty")));
      Assert.True(properties.Contains(type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance)));
      Assert.True(properties.Contains(type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance)));
      Assert.True(properties.Contains(type.GetProperty("PublicStaticProperty", BindingFlags.Public | BindingFlags.Static)));
      Assert.True(properties.Contains(type.GetProperty("ProtectedStaticProperty", BindingFlags.NonPublic | BindingFlags.Static)));
      Assert.True(properties.Contains(type.GetProperty("PrivateStaticProperty", BindingFlags.NonPublic | BindingFlags.Static)));
    }
  }
}