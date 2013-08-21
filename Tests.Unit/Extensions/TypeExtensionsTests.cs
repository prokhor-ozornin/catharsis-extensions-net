using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;


namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TypeExtensions"/>.</para>
  /// </summary>
  public sealed class TypeExtensionsTests
  {
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
    ///   <para>Performs testing of <see cref="TypeExtensions.GetAllProperties(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void GetAllProperties_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAllProperties(null));

      var type = typeof(TestObject);
      var properties = type.GetAllProperties();
      Assert.True(properties.Contains(type.GetProperty("PublicProperty")));
      Assert.True(properties.Contains(type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance)));
      Assert.True(properties.Contains(type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance)));
      Assert.True(properties.Contains(type.GetProperty("PublicStaticProperty", BindingFlags.Public | BindingFlags.Static)));
      Assert.True(properties.Contains(type.GetProperty("ProtectedStaticProperty", BindingFlags.NonPublic | BindingFlags.Static)));
      Assert.True(properties.Contains(type.GetProperty("PrivateStaticProperty", BindingFlags.NonPublic | BindingFlags.Static)));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.GetAnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void GetAnyEvent_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAnyEvent(null, "name"));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAnyEvent(typeof(object), null));
      Assert.Throws<ArgumentException>(() => TypeExtensions.GetAnyEvent(typeof(object), string.Empty));

      var type = typeof(TestObject);
      Assert.True(type.GetAnyEvent("PublicEvent") != null);
      Assert.True(type.GetAnyEvent("ProtectedEvent") != null);
      Assert.True(type.GetAnyEvent("PrivateEvent") != null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.GetAnyField(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void GetAnyField_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAnyField(null, "name"));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAnyField(typeof(object), null));
      Assert.Throws<ArgumentException>(() => TypeExtensions.GetAnyField(typeof(object), string.Empty));

      var type = typeof(TestObject);
      
      type.GetAnyField("PublicStaticField").With(field =>
      {
        Assert.True(field.IsPublic);
        Assert.True(field.IsStatic);
      });

      type.GetAnyField("ProtectedStaticField").With(field =>
      {
        Assert.False(field.IsPrivate);
        Assert.True(field.IsStatic);
      });

      type.GetAnyField("PrivateStaticField").With(field =>
      {
        Assert.True(field.IsPrivate);
        Assert.True(field.IsStatic);
      });

      type.GetAnyField("PublicField").With(field =>
      {
        Assert.True(field.IsPublic);
        Assert.False(field.IsStatic);
      });

      type.GetAnyField("ProtectedField").With(field =>
      {
        Assert.False(field.IsPrivate);
        Assert.False(field.IsStatic);
      });

      type.GetAnyField("PrivateField").With(field =>
      {
        Assert.True(field.IsPrivate);
        Assert.False(field.IsStatic);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.GetAnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void GetAnyMethod_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAnyMethod(null, "name"));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAnyMethod(typeof(object), null));
      Assert.Throws<ArgumentException>(() => TypeExtensions.GetAnyMethod(typeof(object), string.Empty));

      var type = typeof(TestObject);

      type.GetAnyMethod("PublicStaticMethod").With(method =>
      {
        Assert.True(method.IsPublic);
        Assert.True(method.IsStatic);
      });

      type.GetAnyMethod("ProtectedStaticMethod").With(method =>
      {
        Assert.False(method.IsPrivate);
        Assert.True(method.IsStatic);
      });

      type.GetAnyMethod("PrivateStaticMethod").With(method =>
      {
        Assert.True(method.IsPrivate);
        Assert.True(method.IsStatic);
      });

      type.GetAnyMethod("PublicMethod").With(method =>
      {
        Assert.True(method.IsPublic);
        Assert.False(method.IsStatic);
      });

      type.GetAnyMethod("ProtectedMethod").With(method =>
      {
        Assert.False(method.IsPrivate);
        Assert.False(method.IsStatic);
      });

      type.GetAnyMethod("PrivateMethod").With(method =>
      {
        Assert.True(method.IsPrivate);
        Assert.False(method.IsStatic);
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.GetAnyMethod(Type, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void GetAnyProperty_Method()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAnyProperty(null, "name"));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAnyProperty(typeof(object), null));
      Assert.Throws<ArgumentException>(() => TypeExtensions.GetAnyProperty(typeof(object), string.Empty));

      var type = typeof(TestObject);

      type.GetAnyProperty("PublicStaticProperty").With(property => Assert.True(property.IsPublic()));
      type.GetAnyProperty("ProtectedStaticProperty").With(property => Assert.False(property.IsPublic()));
      type.GetAnyProperty("PrivateStaticProperty").With(property => Assert.False(property.IsPublic()));
      type.GetAnyProperty("PublicProperty").With(property => Assert.True(property.IsPublic()));
      type.GetAnyProperty("ProtectedProperty").With(property => Assert.False(property.IsPublic()));
      type.GetAnyProperty("PrivateProperty").With(property => Assert.False(property.IsPublic()));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="TypeExtensions.GetAttribute(Type, Type)"/></description></item>
    ///     <item><description><see cref="TypeExtensions.GetAttribute{T}(Type)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void GetAttribute_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAttribute(null, typeof(object)));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAttribute(typeof(object), null));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAttribute<object>(null));

      Assert.True(typeof(TestObject).GetAttribute(typeof(NonSerializedAttribute)) == null);
      Assert.True(typeof(TestObject).GetAttribute<NonSerializedAttribute>() == null);

      Assert.True(typeof(TestObject).GetAttribute(typeof(SerializableAttribute)) is SerializableAttribute);
      Assert.True(typeof(TestObject).GetAttribute<SerializableAttribute>() != null);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="TypeExtensions.GetAttributes(Type, Type)"/></description></item>
    ///     <item><description><see cref="TypeExtensions.GetAttributes{T}(Type)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void GetAttributes_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAttribute(null, typeof(object)));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAttribute(typeof(object), null));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetAttributes<object>(null));

      Assert.True(typeof(TestObject).GetAttributes(typeof(NonSerializedAttribute)).Length == 0);
      Assert.True(typeof(TestObject).GetAttributes<NonSerializedAttribute>().Length == 0);

      Assert.True(typeof(TestObject).GetAttributes(typeof(SerializableAttribute)).SequenceEqual(new [] { new SerializableAttribute() }));
      Assert.True(typeof(TestObject).GetAttributes<SerializableAttribute>().SequenceEqual(new [] { new SerializableAttribute() }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TypeExtensions.GetDefaultConstructor(Type)"/> method.</para>
    /// </summary>
    [Fact]
    public void GetDefaultConstructor()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetDefaultConstructor(null));

      Assert.True(typeof(TestObject).GetDefaultConstructor() != null);
      Assert.True(typeof(string).GetDefaultConstructor() == null);
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
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.Implements(typeof(object), null));
      Assert.Throws<ArgumentException>(() => TypeExtensions.Implements(typeof(object), typeof(object)));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.Implements<ICloneable>(null));
      Assert.Throws<ArgumentException>(() => TypeExtensions.Implements<object>(typeof(object)));

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

      Assert.True(!typeof(object).Inherits().Any());

      var types = typeof(string).Inherits().ToSet();
      Assert.True(types.Contains(typeof(IComparable)));
      Assert.True(types.Contains(typeof(ICloneable)));
      Assert.True(types.Contains(typeof(IConvertible)));
      Assert.True(types.Contains(typeof(IComparable<string>)));
      Assert.True(types.Contains(typeof(IEnumerable<char>)));
      Assert.True(types.Contains(typeof(IEnumerable)));
      Assert.True(types.Contains(typeof(IEquatable<string>)));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="TypeExtensions.NewInstance(Type, object[])"/></description></item>
    ///     <item><description><see cref="TypeExtensions.NewInstance(Type, IDictionary{string, object})"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void NewInstance_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.NewInstance(null));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.NewInstance(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => TypeExtensions.NewInstance(typeof(object), (IDictionary<string, object>) null));

      Assert.True(typeof(TestObject).NewInstance() != null);
      Assert.True(typeof(TestObject).NewInstance(new Dictionary<string, object>()) != null);
      Assert.Throws<MissingMethodException>(() => typeof(TestObject).NewInstance(new object(), new object()));

      Assert.True(typeof(TestObject).NewInstance("property").To<TestObject>().PublicProperty.Equals("property"));
      Assert.True(typeof(TestObject).NewInstance(new Dictionary<string, object>().AddNext("PublicProperty", "property")).To<TestObject>().PublicProperty.ToString() == "property");
    }
  }
}