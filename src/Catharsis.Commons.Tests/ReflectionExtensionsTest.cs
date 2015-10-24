using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class ReflectionExtensionsTests
  {
    private delegate int Increment(int value);
    private delegate int Decrement(int value);

    private readonly Delegate incrementDelegate;
    private readonly Delegate decrementDelegate;

    private delegate string AsString(object subject);

    public ReflectionExtensionsTests()
    {
      this.incrementDelegate = Delegate.CreateDelegate(typeof(Increment), this.GetType().AnyMethod("IncrementValue"));
      this.decrementDelegate = Delegate.CreateDelegate(typeof(Decrement), this.GetType().AnyMethod("DecrementValue"));
    }

    [Fact]
    public void and()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.And(null, this.incrementDelegate));
      Assert.Throws<ArgumentNullException>(() => this.incrementDelegate.And(null));
      Assert.Throws<ArgumentException>(() => this.incrementDelegate.And(this.decrementDelegate));

      var andDelegate = this.incrementDelegate.And(this.incrementDelegate);
      Assert.True(andDelegate is MulticastDelegate);
      Assert.Equal(this.GetType().AnyMethod("IncrementValue"), andDelegate.Method);
      Assert.Null(andDelegate.Target);
      Assert.True(andDelegate.GetInvocationList().SequenceEqual(new[] { this.incrementDelegate, this.incrementDelegate }));
      Assert.Equal(1, andDelegate.DynamicInvoke(0).To<int>());
    }

    [Fact]
    public void not()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Not(null, this.incrementDelegate));
      Assert.Throws<ArgumentException>(() => this.incrementDelegate.Not(this.decrementDelegate));

      Assert.True(ReferenceEquals(this.incrementDelegate.Not(null), this.incrementDelegate));
      Assert.Null(this.incrementDelegate.Not(this.incrementDelegate));
    }

    [Fact]
    public void to_delegate()
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

    [Fact]
    public void descriptions()
    {
      Assert.Throws<ArgumentException>(() => ReflectionExtensions.Descriptions<DateTime>());
      var descriptions = ReflectionExtensions.Descriptions<MockEnumeration>().ToArray();
      Assert.Equal(3, descriptions.Count());
      Assert.Equal("FirstOption", descriptions[0]);
      Assert.Equal("Second", descriptions[1]);
      Assert.Equal("ThirdOption", descriptions[2]);
    }

    [Fact]
    public void is_protected()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsProtected(null));
 
      Assert.True(typeof(TestObject).GetField("ProtectedField", BindingFlags.Instance | BindingFlags.NonPublic).IsProtected());
    }

    [Fact]
    public void resource()
    {
      Assert.Throws<ArgumentNullException>(() => Assembly.GetExecutingAssembly().Resource(null));
      Assert.Throws<ArgumentException>(() => Assembly.GetExecutingAssembly().Resource(string.Empty));

      Assert.Null(Assembly.GetExecutingAssembly().Resource("invalid"));
      Assert.Equal("resource", Assembly.GetExecutingAssembly().Resource("Catharsis.Commons.Resource.txt"));
    }

    [Fact]
    public void attributes()
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

    [Fact]
    public void description()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Description((MemberInfo) null));
      
      Assert.Null(typeof(TestObject).Description());
      Assert.True(typeof(TestObject).AnyProperty("ReadOnlyProperty").Description().IsEmpty());
      Assert.Equal("PublicProperty", typeof(TestObject).AnyProperty("PublicProperty").Description());
      Assert.Equal("ProtectedProperty", typeof(TestObject).AnyProperty("ProtectedProperty").Description());
      Assert.Equal("PublicStaticProperty Description", typeof(TestObject).AnyProperty("PublicStaticProperty").Description());
    }

    [Fact]
    public void is_constructor()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsConstructor(null));

      Assert.True(typeof(TestObject).DefaultConstructor().To<MemberInfo>().IsConstructor());
    }

    [Fact]
    public void is_event()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsEvent(null));

      Assert.True(typeof(TestObject).AnyEvent("PublicEvent").To<MemberInfo>().IsEvent());
    }

    [Fact]
    public void is_field()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsField(null));

      Assert.True(typeof(TestObject).AnyField("PublicField").To<MemberInfo>().IsField());
    }

    [Fact]
    public void is_method()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsMethod(null));

      Assert.True(typeof(TestObject).AnyMethod("PublicMethod").To<MemberInfo>().IsMethod());
    }

    [Fact]
    public void is_property()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsProperty(null));

      Assert.True(typeof(TestObject).AnyProperty("PublicProperty").To<MemberInfo>().IsProperty());
    }

    [Fact]
    public void is_public()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsPublic(null));

      var type = typeof(TestObject);
      Assert.True(type.GetProperty("PublicProperty", BindingFlags.Public | BindingFlags.Instance).IsPublic());
      Assert.False(type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance).IsPublic());
      Assert.False(type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance).IsPublic());
    }

    [Fact]
    public void type()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.Type(null));

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

    [Fact]
    public void any_event()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.AnyEvent(null, "name"));
      Assert.Throws<ArgumentNullException>(() => typeof(object).AnyEvent(null));
      Assert.Throws<ArgumentException>(() => typeof(object).AnyEvent(string.Empty));

      var type = typeof(TestObject);
      Assert.NotNull(type.AnyEvent("PublicEvent"));
      Assert.NotNull(type.AnyEvent("ProtectedEvent"));
      Assert.NotNull(type.AnyEvent("PrivateEvent"));
    }

    [Fact]
    public void any_field()
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

    [Fact]
    public void any_method()
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

    [Fact]
    public void any_property()
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

    [Fact]
    public void default_constructor()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.DefaultConstructor(null));

      Assert.NotNull(typeof(TestObject).DefaultConstructor());
      Assert.Null(typeof(string).DefaultConstructor());
    }

    [Fact]
    public void has_field()
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

    [Fact]
    public void has_method()
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

    [Fact]
    public void has_property()
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

    [Fact]
    public void implements()
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

    [Fact]
    public void inherits()
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

    [Fact]
    public void is_anonymous()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsAnonymous(null));

      Assert.False(typeof(object).IsAnonymous());
      Assert.True(new { }.GetType().IsAnonymous());
      Assert.True(new { property = "value" }.GetType().IsAnonymous());
    }

    [Fact]
    public void is_assignable_to()
    {
      Assert.Throws<ArgumentNullException>(() => ReflectionExtensions.IsAssignableTo<object>(null));

      Assert.True(typeof(object).IsAssignableTo<object>());
      Assert.True(typeof(string).IsAssignableTo<object>());
      Assert.False(typeof(object).IsAssignableTo<string>());
    }

    [Fact]
    public void new_instance()
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

    [Fact]
    public void properties()
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

    private static int IncrementValue(int value)
    {
      return value + 1;
    }

    private static int DecrementValue(int value)
    {
      return value - 1;
    }
  }
}