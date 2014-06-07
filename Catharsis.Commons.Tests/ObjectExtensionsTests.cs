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
  /// <summary>
  ///   <para>Tests set for class <see cref="ObjectExtensions"/>.</para>
  /// </summary>
  public sealed class ObjectExtensionsTests
  {
    private sealed class DumpTestObject
    {
      public object Property { get; set; }
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.As{T}(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void As_Method()
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

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ObjectBinaryExtensions.Binary(object)"/></description></item>
    ///     <item><description><see cref="ObjectBinaryExtensions.Binary(object, Stream, bool)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Binary_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectBinaryExtensions.Binary(null));
      Assert.Throws<ArgumentNullException>(() => ObjectBinaryExtensions.Binary(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => new object().Binary(null));

      var subject = new object();
      var serialized = new object().Binary();
      Assert.True(serialized.Length > 0);
      Assert.False(new object().Binary().Binary().SequenceEqual(new object().Binary()));
      Assert.False(new object().Binary().SequenceEqual(string.Empty.Binary()));
      Assert.True(subject.Binary().SequenceEqual(subject.Binary()));
      Assert.True(Guid.Empty.ToString().Binary().SequenceEqual(Guid.Empty.ToString().Binary()));

      new MemoryStream().With(stream =>
      {
        Assert.True(ReferenceEquals(subject.Binary(stream), subject));
        Assert.True(stream.ToArray().SequenceEqual(serialized));
        Assert.True(stream.CanWrite);
      });
      new MemoryStream().With(stream =>
      {
        Assert.True(ReferenceEquals(subject.Binary(stream, true), subject));
        Assert.True(stream.ToArray().SequenceEqual(serialized));
        Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
      });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.Dump(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Dump_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Dump(null));

      Assert.Equal("[]", new object().Dump());

      var subject = new DumpTestObject();
      Assert.Equal(@"[Property:""""]", subject.Dump());
      subject.Property = Guid.Empty;
      Assert.Equal("[Property:\"{0}\"]".FormatSelf(Guid.Empty), subject.Dump());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ObjectExtensions.Equality{T}(T, T, string[])"/></description></item>
    ///     <item><description><see cref="ObjectExtensions.Equality{T}(T, T, Expression{Func{T, object}}[])"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equality_Methods()
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
      Assert.True(Guid.NewGuid().ToString().Equality(Guid.NewGuid().ToString(), x => x.Length));
      Assert.False("first".Equality("second", "Length"));
      Assert.False("first".Equality("second", x => x.Length));
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

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.Field(object, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Field_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Field(null, "field"));
      Assert.Throws<ArgumentNullException>(() => new object().Field(null));
      Assert.Throws<ArgumentException>(() => new object().Field(string.Empty));

      Assert.Null(new object().Field("field"));

      var subject = new TestObject { PublicField = "value" };
      Assert.Equal("value", subject.Field("PublicField").To<string>());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ObjectExtensions.GetHashCode{T}(T, IEnumerable{string})"/></description></item>
    ///     <item><description><see cref="ObjectExtensions.GetHashCode{T}(T, Expression{Func{T, object}}[])"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void GetHashCode_Methods()
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
      Assert.Equal(Guid.NewGuid().ToString().GetHashCode(x => x.Length), Guid.NewGuid().ToString().GetHashCode(new[] { "Length" }));
      Assert.Equal(Guid.NewGuid().ToString().GetHashCode(x => x.Length), Guid.NewGuid().ToString().GetHashCode(x => x.Length));

      var testObject = new TestObject();
      Assert.Equal(testObject.GetHashCode((string[])null), testObject.GetHashCode((string[])null));
      Assert.Equal(testObject.GetHashCode(new[] { "PublicProperty" }), testObject.GetHashCode(new[] { "PublicProperty" }));
      Assert.Equal(testObject.GetHashCode(new[] { "PublicProperty" }), testObject.GetHashCode(x => x.PublicProperty));
      Assert.Equal(testObject.GetHashCode(x => x.PublicProperty), testObject.GetHashCode(x => x.PublicProperty));
      Assert.Equal(testObject.GetHashCode(new[] { "ProtectedProperty" }), testObject.GetHashCode(new[] { "PublicProperty" }));
      Assert.Equal(testObject.GetHashCode(new[] { "ProtectedProperty" }), testObject.GetHashCode(x => x.PublicProperty));
      Assert.Equal(testObject.GetHashCode(new[] { "invalid" }), testObject.GetHashCode(new[] { "invalid" }));
      Assert.Equal(testObject.GetHashCode(new[] { "invalid_2" }), testObject.GetHashCode(new[] { "invalid_1" }));
      testObject.PublicProperty = "property";
      Assert.Equal(new TestObject { PublicProperty = "property" }.GetHashCode((string[]) null), new TestObject { PublicProperty = "property" }.GetHashCode((string[])null));
      Assert.NotEqual(new TestObject { PublicProperty = "second" }.GetHashCode((string[])null), new TestObject { PublicProperty = "first" }.GetHashCode((string[])null));
      Assert.Equal(testObject.GetHashCode(new[] { "PublicProperty" }), testObject.GetHashCode(new[] { "PublicProperty" }));
      Assert.Equal(testObject.GetHashCode(new[] { "PublicProperty" }), testObject.GetHashCode(x => x.PublicProperty));
      Assert.Equal(testObject.GetHashCode(x => x.PublicProperty), testObject.GetHashCode(x => x.PublicProperty));
      Assert.NotEqual(testObject.GetHashCode(new[] { "ProtectedProperty" }), testObject.GetHashCode(new[] { "PublicProperty" }));
      Assert.NotEqual(testObject.GetHashCode(new[] { "ProtectedProperty" }), testObject.GetHashCode(x => x.PublicProperty));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.Is{T}(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Is_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Is<object>(null));

      Assert.True(new object().Is<object>());
      Assert.False(new object().Is<string>());
      Assert.True(string.Empty.Is<IEnumerable<char>>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.IsFalse(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsFalse_Method()
    {
      Assert.True(ObjectExtensions.IsFalse(null));
      Assert.True(false.IsFalse());
      Assert.True(byte.MinValue.IsFalse());
      Assert.True(char.MinValue.IsFalse());
      Assert.True(decimal.Zero.IsFalse());
      Assert.True(((double)0).IsFalse());
      Assert.True(((short)0).IsFalse());
      Assert.True(((int)0).IsFalse());
      Assert.True(((long)0).IsFalse());
      Assert.True(((sbyte)0).IsFalse());
      Assert.True(((Single)0).IsFalse());
      Assert.True(string.Empty.IsFalse());
      Assert.True(((ushort)0).IsFalse());
      Assert.True(((uint)0).IsFalse());
      Assert.True(((ulong)0).IsFalse());
      Assert.True(Enumerable.Empty<object>().IsFalse());
      Assert.True(new object[] { }.IsFalse());
      Assert.True(Regex.Match("first", "second").IsFalse());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.IsTrue(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsTrue_Method()
    {
      Assert.True(true.IsTrue());
      Assert.True(((byte)1).IsTrue());
      Assert.True(char.MaxValue.IsTrue());
      Assert.True(decimal.One.IsTrue());
      Assert.True(double.Epsilon.IsTrue());
      Assert.True(((short)1).IsTrue());
      Assert.True(((int)1).IsTrue());
      Assert.True(((long)1).IsTrue());
      Assert.True(((sbyte)1).IsTrue());
      Assert.True(Single.Epsilon.IsTrue());
      Assert.True("string".IsTrue());
      Assert.True(((ushort)1).IsTrue());
      Assert.True(((uint)1).IsTrue());
      Assert.True(((ulong)1).IsTrue());
      Assert.True(new[] { new object() }.IsTrue());
      Assert.True(Regex.Match("value", "value").IsTrue());
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.IsNumeric(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsNumeric_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.IsNumeric(null));

      Assert.True(byte.MinValue.IsNumeric());
      Assert.True(decimal.MinValue.IsNumeric());
      Assert.True(double.MinValue.IsNumeric());
      Assert.True(short.MinValue.IsNumeric());
      Assert.True(int.MinValue.IsNumeric());
      Assert.True(sbyte.MinValue.IsNumeric());
      Assert.True(Single.MinValue.IsNumeric());
      Assert.True(ushort.MinValue.IsNumeric());
      Assert.True(uint.MinValue.IsNumeric());
      Assert.True(ulong.MinValue.IsNumeric());
      Assert.False(new object().IsNumeric());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.Member{T,R}(T, Expression{Func{T,R}})"/> method.</para>
    /// </summary>
    [Fact]
    public void Member_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Member(null, Enumerable.Empty<Expression<Func<object, object>>>().FirstOrDefault()));
      Assert.Throws<ArgumentNullException>(() => new object().Member<object, object>(null));

      var text = Guid.NewGuid().ToString();
      Assert.Equal(text.Length, text.Member(x => x.Length));
      Assert.Equal(text, text.Member(x => x.ToString(CultureInfo.InvariantCulture)));
      Assert.True(DateTime.UtcNow.Member(x => x.Ticks <= DateTime.UtcNow.Ticks));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.Method(object, string, object[])"/> method.</para>
    /// </summary>
    [Fact]
    public void Method_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.Method(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => new object().Method(null));
      Assert.Throws<ArgumentException>(() => new object().Method(string.Empty));
      Assert.Throws<TargetParameterCountException>(() => new object().Method("ToString", new object()));
      Assert.Throws<AmbiguousMatchException>(() => string.Empty.Method("ToString").To<string>() == string.Empty);

      Assert.Null(new object().Method("method"));
      Assert.True((bool)string.Empty.Method("Contains", string.Empty));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ObjectExtensions.Property(object, string)"/></description></item>
    ///     <item><description><see cref="ObjectExtensions.Property{T}(T, string, object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Property_Methods()
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

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ObjectExtensions.Properties{T}(T, IEnumerable{KeyValuePair{string, object}})"/></description></item>
    ///     <item><description><see cref="ObjectExtensions.Properties{T}(T, object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Properties_Methods()
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

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.PropertiesMap(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void PropertiesMap_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.PropertiesMap(null));

      Assert.False(new { }.PropertiesMap().Any());
      var map = new { name = "value" }.PropertiesMap();
      Assert.Equal(1, map.Count);
      Assert.Equal("value", map["name"]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ObjectExtensions.To{T}(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void To_Method()
    {
      object subject = null;
      Assert.Null(subject.To<object>());
      Assert.Null(subject.To<string>());
      
      subject = new object();
      Assert.True(ReferenceEquals(subject.To<object>(), subject));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ObjectExtensions.ToString(object, IEnumerable{string})"/></description></item>
    ///     <item><description><see cref="ObjectExtensions.ToString{T}(T, Expression{Func{T, object}}[])"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.ToString(null, Enumerable.Empty<string>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.ToString(null, Enumerable.Empty<Expression<Func<object, object>>>().ToArray()));
      
      Assert.Equal("[]", new object().ToString(new [] {"property"}));
      Assert.Equal("[]", new object().ToString((string[]) null));
      Assert.Equal("[]", new object().ToString(Enumerable.Empty<string>().ToArray()));
      Assert.Equal("[]", new object().ToString((Expression<Func<object, object>>[]) null));
      Assert.Equal("[]", new object().ToString(Enumerable.Empty<Expression<Func<object, object>>>().ToArray()));

      var date = DateTime.UtcNow;
      Assert.Equal("[Ticks:\"{0}\"]".FormatSelf(date.Ticks), date.ToString(new [] {"Ticks"}));
      Assert.Equal("[Ticks:\"{0}\"]".FormatSelf(date.Ticks), date.ToString(x => x.Ticks));
      Assert.Equal("[Day:\"{0}\", Month:\"{1}\", Year:\"{2}\"]".FormatSelf(date.Day, date.Month, date.Year), date.ToString(new [] {"Day", "Month", "Year"}));
      Assert.Equal("[Day:\"{0}\", Month:\"{1}\", Year:\"{2}\"]".FormatSelf(date.Day, date.Month, date.Year), date.ToString(x => x.Day, x => x.Month, x => x.Year));
      Assert.Equal("[Today:\"{0}\"]".FormatSelf(DateTime.Today), date.ToString(new [] {"Today"}));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ObjectExtensions.With{T}(T, Action{T})"/></description></item>
    ///     <item><description><see cref="ObjectExtensions.With{T, RESULT}(T, Func{T, RESULT})"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void With_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectExtensions.With<object>(null, subject => {}));
      Assert.Throws<ArgumentNullException>(() => new object().With(null));
      
      var text = Guid.NewGuid().ToString();
      Assert.Throws<ObjectDisposedException>(() => new StringReader(text).With(disposable =>
      {
        disposable.With(x => Assert.True(x.ReadToEnd() == text));
      }).Read());

      var list = new List<string>().With(x => x.Add(text));
      Assert.Equal(1, list.Count);
      Assert.Equal(text, list[0]);

      Assert.Equal(text, new object().With(x => text));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ObjectXmlExtensions.Xml{T}(T, Type[])"/></description></item>
    ///     <item><description><see cref="ObjectXmlExtensions.Xml{T}(T, Stream, Encoding, Type[])"/></description></item>
    ///     <item><description><see cref="ObjectXmlExtensions.Xml{T}(T, TextWriter, Type[])"/></description></item>
    ///     <item><description><see cref="ObjectXmlExtensions.Xml{T}(T, XmlWriter, Type[])"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ObjectXmlExtensions.Xml<object>(null));
      Assert.Throws<ArgumentNullException>(() => ObjectXmlExtensions.Xml<object>(null, Stream.Null));
      Assert.Throws<ArgumentNullException>(() => new object().Xml((Stream)null));
      Assert.Throws<ArgumentNullException>(() => ObjectXmlExtensions.Xml<object>(null, TextWriter.Null));
      Assert.Throws<ArgumentNullException>(() => new object().Xml((TextWriter)null));
      Assert.Throws<ArgumentNullException>(() => ObjectXmlExtensions.Xml<object>(null, XmlWriter.Create(Stream.Null)));
      Assert.Throws<ArgumentNullException>(() => new object().Xml((XmlWriter)null));

      var subject = Guid.NewGuid().ToString();
      
      var xml = subject.Xml();
      var stringWriter = new StringWriter();
      stringWriter.XmlWriter().Write(writer =>
      {
        new XmlSerializer(subject.GetType()).Serialize(writer, subject);
        Assert.Equal(xml, stringWriter.ToString());
      });
      Assert.Equal(xml, subject.Xml((Type[]) null));
      Assert.Equal(xml, subject.Xml(Enumerable.Empty<Type>().ToArray()));
      Assert.Equal(xml, subject.Xml((Type[]) null));

      new MemoryStream().With(stream =>
      {
        Assert.True(ReferenceEquals(subject.Xml(stream, Encoding.Unicode), subject));
        Assert.Equal(xml, stream.Rewind().Text());
        Assert.True(stream.CanWrite);
      });

      new StringWriter().With(writer =>
      {
        Assert.True(ReferenceEquals(subject.Xml(writer), subject));
        Assert.Equal(xml, writer.ToString());
        writer.WriteLine();
      });

      stringWriter = new StringWriter();
      stringWriter.XmlWriter().Write(writer =>
      {
        Assert.True(ReferenceEquals(subject.Xml(writer), subject));
        Assert.Equal(xml, stringWriter.ToString());
        stringWriter.WriteLine();
      });
    }
  }
}