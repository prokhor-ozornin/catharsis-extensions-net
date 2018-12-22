using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class ObjectExtensionsTest
  {
    private sealed class DumpTestObject
    {
      public object Property { get; set; }
    }

    [Fact]
    public void as_type()
    {
      object subject = null;
      Assert.Null(subject.As<object>());

      Assert.Null(subject.As<string>());

      subject = new object();
      Assert.True(ReferenceEquals(subject.As<object>(), subject));

      var date = DateTime.UtcNow;
      Assert.Equal(default(string), date.As<string>());

      Assert.Equal(date, date.As<DateTime>());
    }

    [Fact]
    public void binary()
    {
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Binary(null));
      Assert.Throws<ArgumentNullException>(() => StreamExtensions.Binary(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => new object().Binary(null));

      var subject = new object();
      var serialized = new object().Binary();
      Assert.True(serialized.Length > 0);
      Assert.False(new object().Binary().Binary().SequenceEqual(new object().Binary()));
      Assert.False(new object().Binary().SequenceEqual(string.Empty.Binary()));
      Assert.True(subject.Binary().SequenceEqual(subject.Binary()));
      Assert.True(Guid.Empty.ToString().Binary().SequenceEqual(Guid.Empty.ToString().Binary()));

      using (var stream = new MemoryStream())
      {
        Assert.True(ReferenceEquals(subject.Binary(stream), subject));
        Assert.True(stream.ToArray().SequenceEqual(serialized));
        Assert.True(stream.CanWrite);
      }
      using (var stream = new MemoryStream())
      {
        Assert.True(ReferenceEquals(subject.Binary(stream, true), subject));
        Assert.True(stream.ToArray().SequenceEqual(serialized));
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      };
    }

    [Fact]
    public void dump()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Dump(null));

      Assert.Equal("[]", new object().Dump());

      var subject = new DumpTestObject();
      Assert.Equal(@"[Property:""""]", subject.Dump());
      subject.Property = Guid.Empty;
      Assert.Equal($"[Property:\"{Guid.Empty}\"]", subject.Dump());
    }

    [Fact]
    public void equality()
    {
      Assert.True(ObjectExtensions.Equality<object>(null, null, (string[]) null));
      Assert.True(ObjectExtensions.Equality(null, null, (Expression<Func<object, object>>[]) null));
      Assert.False(ObjectExtensions.Equality(null, new object(), (string[])null));
      Assert.False(ObjectExtensions.Equality(null, new object(), (Expression<Func<object, object>>[])null));
      Assert.False(new object().Equality(null, (string[])null));
      Assert.False(new object().Equality(null, (Expression<Func<object, object>>[]) null));
      Assert.False(new object().Equality(string.Empty, (string[]) null));
      Assert.False(new object().Equality(string.Empty, (Expression<Func<object, object>>[])null));
      Assert.False(new object().Equality(new object(), (string[])null));
      Assert.False(new object().Equality(new object(), (Expression<Func<object, object>>[])null));

      var subject = new object();
      Assert.True(subject.Equality(subject, (string[])null));
      Assert.True(subject.Equality(subject, (Expression<Func<object, object>>[]) null));
      Assert.True(Guid.Empty.ToString().Equality(Guid.Empty.ToString(), (string[]) null));
      Assert.True(Guid.Empty.ToString().Equality(Guid.Empty.ToString(), (Expression<Func<object, object>>[]) null));
      Assert.True(Guid.NewGuid().ToString().Equality(Guid.NewGuid().ToString(), new[] { "Length" }));
      Assert.True(Guid.NewGuid().ToString().Equality(Guid.NewGuid().ToString(), it => it.Length));
      Assert.False("first".Equality("second", "Length"));
      Assert.False("first".Equality("second", it => it.Length));
      Assert.True("text".Equality("text", "property"));
      Assert.False("first".Equality("second", "property"));
      
      var testSubject = new TestObject();
      Assert.True(testSubject.Equality(testSubject, (string[]) null));
      Assert.True(testSubject.Equality(testSubject, (Expression<Func<TestObject, object>>[]) null));
      Assert.True(new TestObject().Equality(new TestObject(), (string[])null));
      Assert.True(new TestObject().Equality(new TestObject(), (Expression<Func<TestObject, object>>[])null));
      Assert.True(new TestObject { PublicProperty = "property" }.Equality(new TestObject { PublicProperty = "property" }, (string[]) null));
      Assert.True(new TestObject { PublicProperty = "property" }.Equality(new TestObject { PublicProperty = "property" }, (Expression<Func<TestObject, object>>[]) null));
      Assert.False(new TestObject { PublicProperty = "property" }.Equality(new TestObject(), (string[])null));
      Assert.False(new TestObject { PublicProperty = "property" }.Equality(new TestObject(), (Expression<Func<TestObject, object>>[])null));
      Assert.False(new TestObject { PublicProperty = "first" }.Equality(new TestObject { PublicProperty = "second" }, (string[])null));
      Assert.False(new TestObject { PublicProperty = "first" }.Equality(new TestObject { PublicProperty = "second" }, (Expression<Func<TestObject, object>>[])null));
    }

    [Fact]
    public void field()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Field(null, "field"));
      Assert.Throws<ArgumentNullException>(() => new object().Field(null));
      Assert.Throws<ArgumentException>(() => new object().Field(string.Empty));

      Assert.Null(new object().Field("field"));

      var subject = new TestObject { PublicField = "value" };
      Assert.Equal("value", subject.Field("PublicField").To<string>());
    }

    [Fact]
    public void get_hash_code()
    {
      Assert.Equal(0, ObjectExtensions.GetHashCode<object>(null, (string[]) null));
      Assert.Equal(0, ObjectExtensions.GetHashCode<object>(null, Enumerable.Empty<string>().ToArray()));
      Assert.Equal(0, ObjectExtensions.GetHashCode(null, (Expression<Func<object, object>>[]) null));
      Assert.Equal(0, ObjectExtensions.GetHashCode(null, Enumerable.Empty<Expression<Func<object, object>>>().ToArray()));

      Assert.NotEqual(0, new object().GetHashCode((string[])null));
      Assert.NotEqual(0, new object().GetHashCode((Expression<Func<object, object>>[]) null));

      Assert.NotEqual(new object().GetHashCode((string[])null), new object().GetHashCode((string[])null));
      Assert.NotEqual(new object().GetHashCode((Expression<Func<object, object>>[]) null), new object().GetHashCode((Expression<Func<object, object>>[]) null));

      var subject = new object();
      Assert.Equal(subject.GetHashCode((string[])null), subject.GetHashCode((string[])null));
      Assert.Equal(subject.GetHashCode((Expression<Func<object, object>>[]) null), subject.GetHashCode((Expression<Func<object, object>>[]) null));
      Assert.Equal(subject.GetHashCode(), subject.GetHashCode((string[])null));
      Assert.Equal(subject.GetHashCode(), subject.GetHashCode((Expression<Func<object, object>>[])null));
      Assert.NotEqual(new object().GetHashCode((string[]) null), string.Empty.GetHashCode((string[]) null));
      Assert.Equal( string.Empty.GetHashCode((string[]) null), string.Empty.GetHashCode((string[]) null));

      Assert.Equal(Guid.NewGuid().ToString().GetHashCode(new[] { "Length" }), Guid.NewGuid().ToString().GetHashCode(new[] { "Length" }));
      Assert.Equal(Guid.NewGuid().ToString().GetHashCode(it => it.Length), Guid.NewGuid().ToString().GetHashCode(new[] { "Length" }));
      Assert.Equal(Guid.NewGuid().ToString().GetHashCode(it => it.Length), Guid.NewGuid().ToString().GetHashCode(it => it.Length));

      var testObject = new TestObject();
      Assert.Equal(testObject.GetHashCode((string[])null), testObject.GetHashCode((string[])null));
      Assert.Equal(testObject.GetHashCode(new[] { "PublicProperty" }), testObject.GetHashCode(new[] { "PublicProperty" }));
      Assert.Equal(testObject.GetHashCode(new[] { "PublicProperty" }), testObject.GetHashCode(it => it.PublicProperty));
      Assert.Equal(testObject.GetHashCode(it => it.PublicProperty), testObject.GetHashCode(it => it.PublicProperty));
      Assert.Equal(testObject.GetHashCode(new[] { "ProtectedProperty" }), testObject.GetHashCode(new[] { "PublicProperty" }));
      Assert.Equal(testObject.GetHashCode(new[] { "ProtectedProperty" }), testObject.GetHashCode(it => it.PublicProperty));
      Assert.Equal(testObject.GetHashCode(new[] { "invalid" }), testObject.GetHashCode(new[] { "invalid" }));
      Assert.Equal(testObject.GetHashCode(new[] { "invalid_2" }), testObject.GetHashCode(new[] { "invalid_1" }));
      testObject.PublicProperty = "property";
      Assert.Equal(new TestObject { PublicProperty = "property" }.GetHashCode((string[]) null), new TestObject { PublicProperty = "property" }.GetHashCode((string[])null));
      Assert.NotEqual(new TestObject { PublicProperty = "second" }.GetHashCode((string[])null), new TestObject { PublicProperty = "first" }.GetHashCode((string[])null));
      Assert.Equal(testObject.GetHashCode(new[] { "PublicProperty" }), testObject.GetHashCode(new[] { "PublicProperty" }));
      Assert.Equal(testObject.GetHashCode(new[] { "PublicProperty" }), testObject.GetHashCode(it => it.PublicProperty));
      Assert.Equal(testObject.GetHashCode(it => it.PublicProperty), testObject.GetHashCode(it => it.PublicProperty));
      Assert.NotEqual(testObject.GetHashCode(new[] { "ProtectedProperty" }), testObject.GetHashCode(new[] { "PublicProperty" }));
      Assert.NotEqual(testObject.GetHashCode(new[] { "ProtectedProperty" }), testObject.GetHashCode(it => it.PublicProperty));
    }

    [Fact]
    public void is_instance_of()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Is<object>(null));

      Assert.True(new object().Is<object>());
      Assert.False(new object().Is<string>());
      Assert.True(string.Empty.Is<IEnumerable<char>>());
    }

    [Fact]
    public void member()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Member(null, Enumerable.Empty<Expression<Func<object, object>>>().FirstOrDefault()));
      Assert.Throws<ArgumentNullException>(() => new object().Member<object, object>(null));

      var text = Guid.NewGuid().ToString();
      Assert.Equal(text.Length, text.Member(it => it.Length));
      Assert.Equal(text, text.Member(it => it.ToString(CultureInfo.InvariantCulture)));
      Assert.True(DateTime.UtcNow.Member(it => it.Ticks <= DateTime.UtcNow.Ticks));
    }

    [Fact]
    public void method()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Method(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => new object().Method(null));
      Assert.Throws<ArgumentException>(() => new object().Method(string.Empty));
      Assert.Throws<TargetParameterCountException>(() => new object().Method("ToString", new object()));
      Assert.Throws<AmbiguousMatchException>(() => string.Empty.Method("ToString").To<string>() == string.Empty);

      Assert.Null(new object().Method("method"));
      Assert.True((bool)string.Empty.Method("Contains", string.Empty));
    }

    [Fact]
    public void property()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Property(null, "property"));
      Assert.Throws<ArgumentNullException>(() => new object().Property(null));
      Assert.Throws<ArgumentException>(() => new object().Property(string.Empty));

      Assert.Null(new object().Property("property"));

      var subject = new TestObject { PublicProperty = "value" };
      Assert.Equal("value", subject.Property("PublicProperty").To<string>());


      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Property<object>(null, "property", new object()));
      Assert.Throws<ArgumentNullException>(() => new object().Property(null, new object()));
      Assert.Throws<ArgumentException>(() => new object().Property(string.Empty, new object()));

      subject = new TestObject();
      var property = Guid.NewGuid().ToString();
      Assert.True(ReferenceEquals(subject.Property("PublicProperty", null), subject));
      Assert.Throws<ArgumentException>(() => subject.Property("ReadOnlyProperty", property));

      subject.Property("PublicStaticProperty", property);
      Assert.Equal(property, subject.Property("PublicStaticProperty"));

      subject.Property("ProtectedStaticProperty", property);
      Assert.Equal(property, subject.Property("ProtectedStaticProperty"));

      subject.Property("PrivateStaticProperty", property);
      Assert.Equal(property, subject.Property("PrivateStaticProperty"));

      subject.Property("PublicProperty", property);
      Assert.Equal(property, subject.Property("PublicProperty"));

      subject.Property("ProtectedProperty", property);
      Assert.Equal(property, subject.Property("ProtectedProperty"));

      subject.Property("PrivateProperty", property);
      Assert.Equal(property, subject.Property("PrivateProperty"));
    }

    [Fact]
    public void properties()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Properties<object>(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => new object().Properties((IDictionary<string, object>)null));
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Properties<object>(null, (object)null));
      Assert.Throws<ArgumentNullException>(() => new object().Properties((object)null));

      var subject = new TestObject();
      var property = Guid.NewGuid().ToString();

      Assert.Throws<ArgumentException>(() => subject.Properties(new Dictionary<string, object>{ { "ReadOnlyProperty", property } }));
      Assert.True(ReferenceEquals(subject.Properties(new Dictionary<string, object> { { "PublicProperty", property }, { "property", new object() } }), subject));
      Assert.Equal(property, subject.Property("PublicProperty"));

      Assert.Throws<ArgumentException>(() => subject.Properties(new { ReadOnlyProperty = property }));
      Assert.True(ReferenceEquals(subject.Properties(new { PublicProperty = property, property = new object() }), subject));
      Assert.Equal(property, subject.Property("PublicProperty"));
    }

    [Fact]
    public void properties_map()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.PropertiesMap(null));

      Assert.False(new { }.PropertiesMap().Any());
      var map = new { name = "value" }.PropertiesMap();
      Assert.Equal(1, map.Count);
      Assert.Equal("value", map["name"]);
    }

    [Fact]
    public void to()
    {
      object subject = null;
      Assert.Null(subject.To<object>());
      Assert.Null(subject.To<string>());
      
      subject = new object();
      Assert.True(ReferenceEquals(subject.To<object>(), subject));
    }

    [Fact]
    public void to_string()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.ToString(null, Enumerable.Empty<string>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.ToString(null, Enumerable.Empty<Expression<Func<object, object>>>().ToArray()));
      
      Assert.Equal("[]", new object().ToString(new [] {"property"}));
      Assert.Equal("[]", new object().ToString((string[]) null));
      Assert.Equal("[]", new object().ToString(Enumerable.Empty<string>().ToArray()));
      Assert.Equal("[]", new object().ToString((Expression<Func<object, object>>[]) null));
      Assert.Equal("[]", new object().ToString(Enumerable.Empty<Expression<Func<object, object>>>().ToArray()));

      var date = DateTime.UtcNow;
      Assert.Equal($"[Ticks:\"{date.Ticks}\"]", date.ToString(new [] {"Ticks"}));
      Assert.Equal($"[Ticks:\"{date.Ticks}\"]", date.ToString(it => it.Ticks));
      Assert.Equal($"[Day:\"{date.Day}\", Month:\"{date.Month}\", Year:\"{date.Year}\"]", date.ToString(new [] {"Day", "Month", "Year"}));
      Assert.Equal($"[Day:\"{date.Day}\", Month:\"{date.Month}\", Year:\"{date.Year}\"]", date.ToString(it => it.Day, it => it.Month, it => it.Year));
      Assert.Equal($"[Today:\"{DateTime.Today}\"]", date.ToString(new [] {"Today"}));
    }

    [Fact]
    public void to_string_invariant()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.ToStringInvariant(null));

      var subject = new object();
      Assert.Equal(subject.ToString(), subject.ToStringInvariant());

      subject = "subject";
      Assert.Equal(subject.ToString(), subject.ToStringInvariant());

      subject = DateTime.UtcNow;
      Assert.Equal(string.Format(CultureInfo.InvariantCulture, "{0}", subject), subject.ToStringInvariant());
      Assert.NotEqual(string.Format(CultureInfo.GetCultureInfo("ru"), "{0}", subject), subject.ToStringInvariant());

      subject = 1.5;
      Assert.Equal(string.Format(CultureInfo.InvariantCulture, "{0}", subject), subject.ToStringInvariant());
      Assert.NotEqual(string.Format(CultureInfo.GetCultureInfo("ru"), "{0}", subject), subject.ToStringInvariant());
    }

    [Fact]
    public void do_action()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Do<object>(null, subject => {}));
      Assert.Throws<ArgumentNullException>(() => new object().Do(null));
      
      var text = Guid.NewGuid().ToString();
      Assert.Throws<ObjectDisposedException>(() => new StringReader(text).Do(disposable =>
      {
        disposable.Do(it => Assert.True(it.ReadToEnd() == text));
      }).Read());

      var list = new List<string>().Do(it => it.Add(text));
      Assert.Single(list);
      Assert.Equal(text, list[0]);

      Assert.Equal(text, new object().Do(it => text));
    }

    [Fact]
    public void to_xml()
    {
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.ToXml<object>(null));
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.ToXml<object>(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => new object().ToXml((Stream) null));
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.ToXml<object>(null, TextWriter.Null));
      Assert.Throws<ArgumentNullException>(() => new object().ToXml((TextWriter) null));
      Assert.Throws<ArgumentNullException>(() => XmlExtensions.ToXml<object>(null, XmlWriter.Create(Stream.Null)));
      Assert.Throws<ArgumentNullException>(() => new object().ToXml((XmlWriter) null));

      var subject = Guid.NewGuid().ToString();

      var xml = subject.ToXml();
      var stringWriter = new StringWriter();
      stringWriter.XmlWriter().Write(writer =>
      {
        new XmlSerializer(subject.GetType()).Serialize(writer, subject);
        Assert.Equal(xml, stringWriter.ToString());
      });
      Assert.Equal(xml, subject.ToXml((Type[]) null));
      Assert.Equal(xml, subject.ToXml(Enumerable.Empty<Type>().ToArray()));
      Assert.Equal(xml, subject.ToXml((Type[]) null));

      using (var stream = new MemoryStream())
      {
        Assert.True(ReferenceEquals(subject.ToXml(stream, Encoding.Unicode), subject));
        Assert.Equal(xml, stream.Rewind().Text());
        Assert.True(stream.CanWrite);
      }

      using (var writer = new StringWriter())
      {
        Assert.True(ReferenceEquals(subject.ToXml(writer), subject));
        Assert.Equal(xml, writer.ToString());
        writer.WriteLine();
      }

      stringWriter = new StringWriter();
      using (var writer = stringWriter.XmlWriter())
      {
        Assert.True(ReferenceEquals(subject.ToXml(writer), subject));
        Assert.Equal(xml, stringWriter.ToString());
        stringWriter.WriteLine();
      }
    }
  }
}