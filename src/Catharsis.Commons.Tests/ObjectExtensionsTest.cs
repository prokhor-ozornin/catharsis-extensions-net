using System.Globalization;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ObjectExtensions"/>.</para>
/// </summary>
public sealed class ObjectExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.As{T}(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_As_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.To{T}(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_To_Method()
  {
    /*
    AssertionExtensions.Should(() => ObjectExtensions.To<object>(null!)).ThrowExactly<ArgumentNullException>();

    object? subject = null;
    subject.To<object>().Should().BeNull();
    subject.To<string>().Should().BeNull();

    subject = new object();
    subject.To<object>().Should().BeSameAs(subject);
    */

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Is{T}(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Is_Method()
  {
    /*
    AssertionExtensions.Should(() => ObjectExtensions.Is<object>(null!)).ThrowExactly<ArgumentNullException>();

    new object().Is<object>().Should().BeTrue();
    new object().Is<string>().Should().BeFalse();
    string.Empty.Is<IEnumerable<char>>().Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsNull(object?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_IsNull_Method()
  {
    ((object?) null).IsNull().Should().BeTrue();
    ((int?) null).IsNull().Should().BeTrue();

    new object().IsNull().Should().BeFalse();
    string.Empty.IsNull().Should().BeFalse();
    Array.Empty<object>().IsNull().Should().BeFalse();
    Enumerable.Empty<object>().IsNull().Should().BeFalse();
    Guid.Empty.IsNull().Should().BeFalse();

    new WeakReference(null).IsNull().Should().BeTrue();
    new WeakReference(new object()).IsNull().Should().BeFalse();
    new WeakReference(string.Empty).IsNull().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsEmpty{T}(T?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Nullable_IsEmpty_Method()
  {
    ((sbyte?) sbyte.MinValue).IsEmpty().Should().BeFalse();
    ((sbyte?) sbyte.MaxValue).IsEmpty().Should().BeFalse();
    ((sbyte?) null).IsEmpty().Should().BeTrue();

    ((byte?) byte.MinValue).IsEmpty().Should().BeFalse();
    ((byte?) byte.MaxValue).IsEmpty().Should().BeFalse();
    ((byte?) null).IsEmpty().Should().BeTrue();

    ((short?) short.MinValue).IsEmpty().Should().BeFalse();
    ((short?) short.MaxValue).IsEmpty().Should().BeFalse();
    ((short?) null).IsEmpty().Should().BeTrue();

    ((ushort?) ushort.MinValue).IsEmpty().Should().BeFalse();
    ((ushort?) ushort.MaxValue).IsEmpty().Should().BeFalse();
    ((ushort?) null).IsEmpty().Should().BeTrue();

    ((int?) int.MinValue).IsEmpty().Should().BeFalse();
    ((int?) int.MaxValue).IsEmpty().Should().BeFalse();
    ((int?) null).IsEmpty().Should().BeTrue();

    ((uint?) uint.MinValue).IsEmpty().Should().BeFalse();
    ((uint?) uint.MaxValue).IsEmpty().Should().BeFalse();
    ((uint?) null).IsEmpty().Should().BeTrue();

    ((long?) long.MinValue).IsEmpty().Should().BeFalse();
    ((long?) long.MaxValue).IsEmpty().Should().BeFalse();
    ((long?) null).IsEmpty().Should().BeTrue();

    ((ulong?) ulong.MinValue).IsEmpty().Should().BeFalse();
    ((ulong?) ulong.MaxValue).IsEmpty().Should().BeFalse();
    ((ulong?) null).IsEmpty().Should().BeTrue();

    ((char?) null).IsEmpty().Should().BeTrue();
    ((char?) char.MinValue).IsEmpty().Should().BeFalse();

    ((Guid?) Guid.Empty).IsEmpty().Should().BeFalse();
    ((Guid?) null).IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsEmpty{T}(Lazy{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Lazy_IsEmpty_Method()
  {
    new Lazy<object>().IsEmpty().Should().BeTrue();
    
    new Lazy<object>(new object()).IsEmpty().Should().BeFalse();
    var lazy = new Lazy<object?>(() => new object());
    lazy.IsEmpty().Should().BeTrue();
    _ = lazy.Value;
    lazy.IsEmpty().Should().BeFalse();

    new Lazy<object?>((object?) null).IsEmpty().Should().BeTrue();
    lazy = new Lazy<object?>(() => null);
    lazy.IsEmpty().Should().BeTrue();
    _ = lazy.Value;
    lazy.IsEmpty().Should().BeTrue();

    new Lazy<object>(string.Empty).IsEmpty().Should().BeTrue();
    lazy = new Lazy<object?>(() => string.Empty);
    lazy.IsEmpty().Should().BeTrue();
    _ = lazy.Value;
    lazy.IsEmpty().Should().BeTrue();

    new Lazy<object>(" \t\r\n ").IsEmpty().Should().BeTrue();
    lazy = new Lazy<object?>(() => " \t\r\n ");
    lazy.IsEmpty().Should().BeTrue();
    _ = lazy.Value;
    lazy.IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ObjectExtensions.UseFinally{TSubject, TResult}(TSubject, Func{TSubject, TResult?}, Action{TSubject}?, bool)"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.UseFinally{T}(T, Action{T}, Action{T}?, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Object_UseFinally_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.UseFinally<object, bool>(null!, _ => true)).ThrowExactly<ArgumentNullException>();

      var text = RandomString;
      AssertionExtensions.Should(() => text.ToStringReader().UseFinally(reader => { reader.UseFinally(reader => reader.ReadToEnd().Should().Be(text)); }).Read()).ThrowExactly<ObjectDisposedException>();

      var list = new List<string>().UseFinally(list => list.Add(text));
      list.Should().ContainSingle().Which.Should().Be(text);

      new object().UseFinally(_ => text).Should().Be(text);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => new object().UseFinally(null!)).ThrowExactly<ArgumentNullException>();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ObjectExtensions.Use{TSubject, TResult}(TSubject, Func{TSubject, TResult?})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.Use{T}(T, Action{T}, Predicate{T}?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Object_Use_Methods()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.While{T}(T, Predicate{T}, Action{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_While_Method()
  {
    AssertionExtensions.Should(() => ObjectExtensions.While<object>(null!, _ => true, _ => {})).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => ObjectExtensions.While<object>(null!, _ => true, _ => { })).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Equality{T}(T?, T?, string[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Equality_Array_Method()
  {
    /*var subject = new object();
    subject.Equality(subject, (string[]) null!).Should().BeTrue();
    subject.Equality(subject, (Expression<Func<object, object>>[]) null!).Should().BeTrue();
    Guid.Empty.ToString().Equality(Guid.Empty.ToString(), (string[]) null!).Should().BeTrue();
    Guid.Empty.ToString().Equality(Guid.Empty.ToString(), (Expression<Func<object, object>>[]) null!).Should().BeTrue();
    Guid.NewGuid().ToString().Equality(Guid.NewGuid().ToString(), "Length").Should().BeTrue();
    Guid.NewGuid().ToString().Equality(Guid.NewGuid().ToString(), text => text.Length).Should().BeTrue();
    "first".Equality("second", "Length").Should().BeFalse();
    "first".Equality("second", text => text.Length).Should().BeFalse();
    "text".Equality("text", "property").Should().BeTrue();
    "first".Equality("second", "property").Should().BeFalse();

    var testSubject = new TestObject();
    testSubject.Equality(testSubject, (string[]) null!).Should().BeTrue();
    testSubject.Equality(testSubject, (Expression<Func<TestObject, object>>[]) null!).Should().BeTrue();
    new TestObject().Equality(new TestObject(), (string[]) null!).Should().BeTrue();
    new TestObject().Equality(new TestObject(), (Expression<Func<TestObject, object>>[]) null!).Should().BeTrue();
    new TestObject {PublicProperty = "property"}.Equality(new TestObject {PublicProperty = "property"}, (string[]) null!).Should().BeTrue();
    new TestObject {PublicProperty = "property"}.Equality(new TestObject {PublicProperty = "property"}, (Expression<Func<TestObject, object>>[]) null!).Should().BeTrue();
    new TestObject {PublicProperty = "property"}.Equality(new TestObject(), (string[]) null!).Should().BeFalse();
    new TestObject {PublicProperty = "property"}.Equality(new TestObject(), (Expression<Func<TestObject, object>>[]) null!).Should().BeFalse();
    new TestObject {PublicProperty = "first"}.Equality(new TestObject {PublicProperty = "second"}, (string[]) null!).Should().BeFalse();
    new TestObject {PublicProperty = "first"}.Equality(new TestObject {PublicProperty = "second"}, (Expression<Func<TestObject, object>>[]) null!).Should().BeFalse();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Equality{T}(T?, T?, Expression{Func{T, object?}}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Equality_Expression_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.HashCode{T}(T?, string[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_HashCode_Array_Method()
  {
    /*new object().GetHashCode((string[]) null!).Should().NotBe(0);
    new object().GetHashCode((Expression<Func<object, object>>[]) null!).Should().NotBe(0);

    new object().GetHashCode((string[]) null!).Should().NotBe(new object().GetHashCode((string[]) null!));
    new object().GetHashCode((Expression<Func<object, object>>[]) null!).Should().NotBe(new object().GetHashCode((Expression<Func<object, object>>[]) null!));

    var subject = new object();
    subject.GetHashCode((string[]) null!).Should().Be(subject.GetHashCode((string[]) null!));
    subject.GetHashCode((Expression<Func<object, object>>[]) null!).Should().Be(subject.GetHashCode((Expression<Func<object, object>>[]) null!));
    subject.GetHashCode((string[]) null!).Should().Be(subject.GetHashCode());
    subject.GetHashCode((Expression<Func<object, object>>[]) null!).Should().Be(subject.GetHashCode());
    string.Empty.GetHashCode((string[]) null!).Should().NotBe(new object().GetHashCode((string[]) null!));
    string.Empty.GetHashCode((string[]) null!).Should().Be(string.Empty.GetHashCode((string[]) null!));

    Guid.NewGuid().ToString().GetHashCode("Length").Should().Be(Guid.NewGuid().ToString().GetHashCode("Length"));
    Guid.NewGuid().ToString().GetHashCode("Length").Should().Be(Guid.NewGuid().ToString().GetHashCode(it => it.Length));
    Guid.NewGuid().ToString().GetHashCode(it => it.Length).Should().Be(Guid.NewGuid().ToString().GetHashCode(it => it.Length));

    var testObject = new TestObject();
    testObject.GetHashCode((string[]) null!).Should().Be(testObject.GetHashCode((string[]) null!));
    testObject.GetHashCode("PublicProperty").Should().Be(testObject.GetHashCode("PublicProperty"));
    testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode("PublicProperty"));
    testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode(it => it.PublicProperty));
    testObject.GetHashCode("PublicProperty").Should().Be(testObject.GetHashCode("ProtectedProperty"));
    testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode("ProtectedProperty"));
    testObject.GetHashCode("invalid").Should().Be(testObject.GetHashCode("invalid"));
    testObject.GetHashCode("invalid_1").Should().Be(testObject.GetHashCode("invalid_2"));

    testObject.PublicProperty = "property";
    new TestObject {PublicProperty = "property"}.GetHashCode((string[]) null!).Should().Be(new TestObject {PublicProperty = "property"}.GetHashCode((string[]) null!));
    new TestObject {PublicProperty = "first"}.GetHashCode((string[]) null!).Should().NotBe(new TestObject {PublicProperty = "second"}.GetHashCode((string[]) null!));
    testObject.GetHashCode("PublicProperty").Should().Be(testObject.GetHashCode("PublicProperty"));
    testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode("PublicProperty"));
    testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode(it => it.PublicProperty));
    testObject.GetHashCode("PublicProperty").Should().NotBe(testObject.GetHashCode("ProtectedProperty"));
    testObject.GetHashCode(it => it.PublicProperty).Should().NotBe(testObject.GetHashCode("ProtectedProperty"));*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.HashCode{T}(T?, Expression{Func{T, object?}}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_HashCode_Expression_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Member{T, TResult}(T, Expression{Func{T, TResult}})"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Member_Method()
  {
    AssertionExtensions.Should(() => ObjectExtensions.Member(null, Enumerable.Empty<Expression<Func<object?, object?>>>().First())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().Member<object, object>(null!)).ThrowExactly<ArgumentNullException>();

    var text = RandomString;
    text.Member(instance => instance.Length).Should().Be(text.Length);
    text.Member(instance => instance.ToString(CultureInfo.InvariantCulture)).Should().Be(text);
    DateTime.UtcNow.Member(instance => instance.Ticks <= DateTime.UtcNow.Ticks).Should().BeTrue();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Field(object, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Field_Method()
  {
    /*AssertionExtensions.Should(() => ObjectExtensions.Field(null!, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().Field(null!)).ThrowExactly<ArgumentNullException>();

    new object().Field("field").Should().BeNull();

    var subject = new TestObject {PublicField = "value"};
    subject.Field("PublicField").To<string>().Should().Be("value");*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Method(object, string, IEnumerable{object?}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Method_Enumerable_Method()
  {
    AssertionExtensions.Should(() => ObjectExtensions.Method(null!, string.Empty, (IEnumerable<string>) null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().Method(null!, (IEnumerable<string>) null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Method(object, string, object?[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Method_Array_Method()
  {
    /*AssertionExtensions.Should(() => ObjectExtensions.Method(null!, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new object().Method(null!)).ThrowExactly<ArgumentNullException>();

    new object().Method("method").Should().BeNull();
    ((bool) string.Empty.Method("Contains", string.Empty)).Should().BeTrue();*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ObjectExtensions.Property(object, string)"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.Property{T}(T, string, object?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Object_Property_Methods()
  {
    /*using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Property(null!, string.Empty)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new object().Property(null!)).ThrowExactly<ArgumentNullException>();

      new object().Property("property").Should().BeNull();

      var subject = new TestObject { PublicProperty = "value" };
      subject.Property("PublicProperty").As<string>().Should().Be("value");


      AssertionExtensions.Should(() => ObjectExtensions.Property<object>(null!, "property", new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new object().Property(null!, new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new object().Property(string.Empty, new object())).ThrowExactly<ArgumentException>();

      subject = new TestObject();

      subject.Property("PublicProperty", null).Should().BeSameAs(subject);

      var property = RandomString;

      subject.Property("ReadOnlyProperty", property);
      subject.Property("ReadOnlyProperty").Should().BeNull();

      subject.Property("PublicStaticProperty", property);
      subject.Property("PublicStaticProperty").Should().Be(property);

      subject.Property("ProtectedStaticProperty", property);
      subject.Property("ProtectedStaticProperty").Should().Be(property);

      subject.Property("PrivateStaticProperty", property);
      subject.Property("PrivateStaticProperty").Should().Be(property);

      subject.Property("PublicProperty", property);
      subject.Property("PublicProperty").Should().Be(property);

      subject.Property("ProtectedProperty", property);
      subject.Property("ProtectedProperty").Should().Be(property);

      subject.Property("PrivateProperty", property);
      subject.Property("PrivateProperty").Should().Be(property);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Property<object>(null!, string.Empty, null)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new object().Property(null!, null)).ThrowExactly<ArgumentNullException>();
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ObjectExtensions.Properties(object)"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.Properties{T}(T, object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Object_Properties_Methods()
  {
    /*using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Properties(null!)).ThrowExactly<ArgumentNullException>();

      var subject = new TestObject();
      var property = RandomString;

      subject.Properties(new Dictionary<string, object> { { "PublicProperty", property }, { "property", new object() } }).Should().BeSameAs(subject);
      subject.Property("PublicProperty").Should().Be(property);

      subject.Properties(new
      {
        PublicProperty = property,
        property = new object()
      }).Should().BeSameAs(subject);

      subject.Property("PublicProperty").Should().Be(property);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Properties<object>(null!, new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new object().Properties(null!)).ThrowExactly<ArgumentNullException>();
    }*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.ToStringState(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_ToStringState_Method()
  {
    /*AssertionExtensions.Should(() => ObjectExtensions.ToStringState(null!)).ThrowExactly<ArgumentNullException>();

    new object().ToStringState().Should().Be("[]");

    var subject = new DumpTestObject();
    ((object) subject).ToStringState().Should().Be(@"[Property:""""]");

    subject.Property = Guid.Empty;
    ((object) subject).ToStringState().Should().Be($"[Property:\"{Guid.Empty}\"]");*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.ToStringState(object, string[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_ToStringState_Array_Method()
  {
    /*AssertionExtensions.Should(() => ObjectExtensions.ToStringState(null!)).ThrowExactly<ArgumentNullException>();

    new object().ToStringState("property").Should().Be("[]");
    new object().ToStringState((string[]) null!).Should().Be("[]");
    new object().ToStringState().Should().Be("[]");
    new object().ToStringState((Expression<Func<object, object>>[]) null!).Should().Be("[]");
    new object().ToStringState(Array.Empty<Expression<Func<object, object>>>()).Should().Be("[]");

    var date = DateTime.UtcNow;
    date.ToStringState("Ticks").Should().Be($"[Ticks:\"{date.Ticks}\"]");
    date.ToStringState(date => date.Ticks).Should().Be($"[Ticks:\"{date.Ticks}\"]");
    date.ToStringState("Day", "Month", "Year").Should().Be($"[Day:\"{date.Day}\", Month:\"{date.Month}\", Year:\"{date.Year}\"]");
    date.ToStringState(date => date.Day, date => date.Month, date => date.Year).Should().Be($"[Day:\"{date.Day}\", Month:\"{date.Month}\", Year:\"{date.Year}\"]");
    date.ToStringState("Today").Should().Be($"[Today:\"{DateTime.Today}\"]");*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.ToStringState{T}(T, Expression{Func{T, object?}}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_ToStringState_Expression_Method()
  {
    AssertionExtensions.Should(() => ObjectExtensions.ToStringState(null, Array.Empty<Expression<Func<object?, object?>>>())).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.ToStringFormatted(object, IFormatProvider?, string?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_ToStringFormatted_Method()
  {
    AssertionExtensions.Should(() => ObjectExtensions.ToStringFormatted(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.ToStringInvariant(object, string?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_ToStringInvariant_Method()
  {
    AssertionExtensions.Should(() => ObjectExtensions.ToStringInvariant(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
}