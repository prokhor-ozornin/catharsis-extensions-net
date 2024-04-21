using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ObjectExtensions"/>.</para>
/// </summary>
public sealed class ObjectExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Is{T}(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Is_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Is<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

      Validate<object>(true, new object());
      Validate<string>(false, new object());
      Validate<IEnumerable<char>>(true, string.Empty);
    }
    
    return;

    static void Validate<T>(bool result, object instance) => instance.Is<T>().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsSameAs(object, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsSameAs_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate(bool result, object left, object right) => left.IsSameAs(right).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsNull(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsNull_Method()
  {
    using (new AssertionScope())
    {
      ((object) null).IsNull().Should().BeTrue();
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

    return;

    static void Validate(bool result, object instance) => instance.IsNull().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsUnset{T}(T?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Nullable_IsUnset_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(bool result, T? nullable) where T : struct => nullable.IsUnset().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsUnset{T}(Lazy{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Lazy_IsUnset_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(bool result, Lazy<T> lazy) => lazy.IsUnset().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsEmpty{T}(T?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Nullable_IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      ((sbyte?)sbyte.MinValue).IsEmpty().Should().BeFalse();
      ((sbyte?)sbyte.MaxValue).IsEmpty().Should().BeFalse();
      ((sbyte?)null).IsEmpty().Should().BeTrue();

      ((byte?)byte.MinValue).IsEmpty().Should().BeFalse();
      ((byte?)byte.MaxValue).IsEmpty().Should().BeFalse();
      ((byte?)null).IsEmpty().Should().BeTrue();

      ((short?)short.MinValue).IsEmpty().Should().BeFalse();
      ((short?)short.MaxValue).IsEmpty().Should().BeFalse();
      ((short?)null).IsEmpty().Should().BeTrue();

      ((ushort?)ushort.MinValue).IsEmpty().Should().BeFalse();
      ((ushort?)ushort.MaxValue).IsEmpty().Should().BeFalse();
      ((ushort?)null).IsEmpty().Should().BeTrue();

      ((int?)int.MinValue).IsEmpty().Should().BeFalse();
      ((int?)int.MaxValue).IsEmpty().Should().BeFalse();
      ((int?)null).IsEmpty().Should().BeTrue();

      ((uint?)uint.MinValue).IsEmpty().Should().BeFalse();
      ((uint?)uint.MaxValue).IsEmpty().Should().BeFalse();
      ((uint?)null).IsEmpty().Should().BeTrue();

      ((long?)long.MinValue).IsEmpty().Should().BeFalse();
      ((long?)long.MaxValue).IsEmpty().Should().BeFalse();
      ((long?)null).IsEmpty().Should().BeTrue();

      ((ulong?)ulong.MinValue).IsEmpty().Should().BeFalse();
      ((ulong?)ulong.MaxValue).IsEmpty().Should().BeFalse();
      ((ulong?)null).IsEmpty().Should().BeTrue();

      ((char?)null).IsEmpty().Should().BeTrue();
      ((char?)char.MinValue).IsEmpty().Should().BeFalse();

      ((Guid?)Guid.Empty).IsEmpty().Should().BeFalse();
      ((Guid?)null).IsEmpty().Should().BeTrue();
    }

    return;

    static void Validate<T>(bool result, T? nullable) where T : struct => nullable.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.IsEmpty{T}(Lazy{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void Lazy_IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      new Lazy<object>().IsEmpty().Should().BeTrue();

      new Lazy<object>(new object()).IsEmpty().Should().BeFalse();
      var lazy = new Lazy<object>(() => new object());
      lazy.IsEmpty().Should().BeTrue();
      _ = lazy.Value;
      lazy.IsEmpty().Should().BeFalse();

      new Lazy<object>((object)null).IsEmpty().Should().BeTrue();
      lazy = new Lazy<object>(() => null);
      lazy.IsEmpty().Should().BeTrue();
      _ = lazy.Value;
      lazy.IsEmpty().Should().BeTrue();

      new Lazy<object>(string.Empty).IsEmpty().Should().BeTrue();
      lazy = new Lazy<object>(() => string.Empty);
      lazy.IsEmpty().Should().BeTrue();
      _ = lazy.Value;
      lazy.IsEmpty().Should().BeTrue();

      new Lazy<object>(" \t\r\n ").IsEmpty().Should().BeTrue();
      lazy = new Lazy<object>(() => " \t\r\n ");
      lazy.IsEmpty().Should().BeTrue();
      _ = lazy.Value;
      lazy.IsEmpty().Should().BeTrue();
    }

    return;

    static void Validate<T>(bool result, Lazy<T> lazy) => lazy.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.As{T}(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void As_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.As<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(object instance) where T : class
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.To{T}(object)"/> method.</para>
  /// </summary>
  [Fact]
  public void To_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.To<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

      /*object? subject = null;
      subject.To<object>().Should().BeNull();
      subject.To<string>().Should().BeNull();

      subject = new object();
      subject.To<object>().Should().BeSameAs(subject);*/
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(object instance)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.With{T}(T, Action{T}, Predicate{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void With_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.With<object>(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      throw new NotImplementedException();
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.While{T}(T, Predicate{T}, Action{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void While_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.While<object>(null, _ => true, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().While(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("condition");
      AssertionExtensions.Should(() => new object().While(_ => true, null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Equality{T}(T, T, string[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Equality_String_Enumerable_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Equality{T}(T, T, string[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Equality_String_Array_Method()
  {
    using (new AssertionScope())
    {
      /*var subject = new object();
        subject.Equality(subject, (string[]) null).Should().BeTrue();
        subject.Equality(subject, (Expression<Func<object, object>>[]) null).Should().BeTrue();
        Guid.Empty.ToString().Equality(Guid.Empty.ToString(), (string[]) null).Should().BeTrue();
        Guid.Empty.ToString().Equality(Guid.Empty.ToString(), (Expression<Func<object, object>>[]) null).Should().BeTrue();
        Guid.NewGuid().ToString().Equality(Guid.NewGuid().ToString(), "Length").Should().BeTrue();
        Guid.NewGuid().ToString().Equality(Guid.NewGuid().ToString(), text => text.Length).Should().BeTrue();
        "first".Equality("second", "Length").Should().BeFalse();
        "first".Equality("second", text => text.Length).Should().BeFalse();
        "text".Equality("text", "property").Should().BeTrue();
        "first".Equality("second", "property").Should().BeFalse();

        var testSubject = new TestObject();
        testSubject.Equality(testSubject, (string[]) null).Should().BeTrue();
        testSubject.Equality(testSubject, (Expression<Func<TestObject, object>>[]) null).Should().BeTrue();
        new TestObject().Equality(new TestObject(), (string[]) null).Should().BeTrue();
        new TestObject().Equality(new TestObject(), (Expression<Func<TestObject, object>>[]) null).Should().BeTrue();
        new TestObject {PublicProperty = "property"}.Equality(new TestObject {PublicProperty = "property"}, (string[]) null).Should().BeTrue();
        new TestObject {PublicProperty = "property"}.Equality(new TestObject {PublicProperty = "property"}, (Expression<Func<TestObject, object>>[]) null).Should().BeTrue();
        new TestObject {PublicProperty = "property"}.Equality(new TestObject(), (string[]) null).Should().BeFalse();
        new TestObject {PublicProperty = "property"}.Equality(new TestObject(), (Expression<Func<TestObject, object>>[]) null).Should().BeFalse();
        new TestObject {PublicProperty = "first"}.Equality(new TestObject {PublicProperty = "second"}, (string[]) null).Should().BeFalse();
        new TestObject {PublicProperty = "first"}.Equality(new TestObject {PublicProperty = "second"}, (Expression<Func<TestObject, object>>[]) null).Should().BeFalse();*/
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Equality{T}(T, T, IEnumerable{Expression{Func{T, object}}})"/> method.</para>
  /// </summary>
  [Fact]
  public void Equality_Expression_Enumerable_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Equality{T}(T, T, Expression{Func{T, object}}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Equality_Expression_Array_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.HashCode{T}(T, IEnumerable{string})"/> method.</para>
  /// </summary>
  [Fact]
  public void HashCode_String_Enumerable_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.HashCode{T}(T, string[])"/> method.</para>
  /// </summary>
  [Fact]
  public void HashCode_String_Array_Method()
  {
    using (new AssertionScope())
    {
      /*new object().GetHashCode((string[]) null).Should().NotBe(0);
        new object().GetHashCode((Expression<Func<object, object>>[]) null).Should().NotBe(0);

        new object().GetHashCode((string[]) null).Should().NotBe(new object().GetHashCode((string[]) null));
        new object().GetHashCode((Expression<Func<object, object>>[]) null).Should().NotBe(new object().GetHashCode((Expression<Func<object, object>>[]) null));

        var subject = new object();
        subject.GetHashCode((string[]) null).Should().Be(subject.GetHashCode((string[]) null));
        subject.GetHashCode((Expression<Func<object, object>>[]) null).Should().Be(subject.GetHashCode((Expression<Func<object, object>>[]) null));
        subject.GetHashCode((string[]) null).Should().Be(subject.GetHashCode());
        subject.GetHashCode((Expression<Func<object, object>>[]) null).Should().Be(subject.GetHashCode());
        string.Empty.GetHashCode((string[]) null).Should().NotBe(new object().GetHashCode((string[]) null));
        string.Empty.GetHashCode((string[]) null).Should().Be(string.Empty.GetHashCode((string[]) null));

        Guid.NewGuid().ToString().GetHashCode("Length").Should().Be(Guid.NewGuid().ToString().GetHashCode("Length"));
        Guid.NewGuid().ToString().GetHashCode("Length").Should().Be(Guid.NewGuid().ToString().GetHashCode(it => it.Length));
        Guid.NewGuid().ToString().GetHashCode(it => it.Length).Should().Be(Guid.NewGuid().ToString().GetHashCode(it => it.Length));

        var testObject = new TestObject();
        testObject.GetHashCode((string[]) null).Should().Be(testObject.GetHashCode((string[]) null));
        testObject.GetHashCode("PublicProperty").Should().Be(testObject.GetHashCode("PublicProperty"));
        testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode("PublicProperty"));
        testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode(it => it.PublicProperty));
        testObject.GetHashCode("PublicProperty").Should().Be(testObject.GetHashCode("ProtectedProperty"));
        testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode("ProtectedProperty"));
        testObject.GetHashCode("invalid").Should().Be(testObject.GetHashCode("invalid"));
        testObject.GetHashCode("invalid_1").Should().Be(testObject.GetHashCode("invalid_2"));

        testObject.PublicProperty = "property";
        new TestObject {PublicProperty = "property"}.GetHashCode((string[]) null).Should().Be(new TestObject {PublicProperty = "property"}.GetHashCode((string[]) null));
        new TestObject {PublicProperty = "first"}.GetHashCode((string[]) null).Should().NotBe(new TestObject {PublicProperty = "second"}.GetHashCode((string[]) null));
        testObject.GetHashCode("PublicProperty").Should().Be(testObject.GetHashCode("PublicProperty"));
        testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode("PublicProperty"));
        testObject.GetHashCode(it => it.PublicProperty).Should().Be(testObject.GetHashCode(it => it.PublicProperty));
        testObject.GetHashCode("PublicProperty").Should().NotBe(testObject.GetHashCode("ProtectedProperty"));
        testObject.GetHashCode(it => it.PublicProperty).Should().NotBe(testObject.GetHashCode("ProtectedProperty"));*/
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.HashCode{T}(T, IEnumerable{Expression{Func{T, object}}})"/> method.</para>
  /// </summary>
  [Fact]
  public void HashCode_Expression_Enumerable_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.HashCode{T}(T, Expression{Func{T, object}}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void HashCode_Expression_Array_Method()
  {
    using (new AssertionScope())
    {
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
  ///     <item><description><see cref="ObjectExtensions.TryFinally{T}(T, Action{T}, Action{T})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.TryFinally{TSubject, TResult}(TSubject, Func{TSubject, TResult}, Action{TSubject})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TryFinally_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.TryFinally<object>(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().TryFinally(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.TryFinally<object, bool>(null, _ => true)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().TryFinally((Func<object, bool>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("function");

      var text = Attributes.RandomString();
      //AssertionExtensions.Should(() => text.ToStringReader().TryFinally(reader => { reader.TryFinally(reader => reader.ReadToEnd().Should().Be(text)); }).Read()).ThrowExactly<ObjectDisposedException>();

      var list = new List<string>().TryFinally(list => list.Add(text));
      list.Should().BeOfType<List<string>>().And.ContainSingle().Which.Should().Be(text);

      new object().TryFinally(_ => text).Should().BeOfType<string>().And.Be(text);
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
  ///     <item><description><see cref="ObjectExtensions.TryCatchFinally{T}(T, Action{T}, Action{T}, Action{T})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.TryCatchFinally{T, TException}(T, Action{T}, Action{T}, Action{T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TryCatchFinally_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.TryCatchFinally<object>(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().TryCatchFinally(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.TryCatchFinally<object, Exception>(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().TryCatchFinally<object, Exception>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("function");

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
  ///     <item><description><see cref="ObjectExtensions.TryFinallyDispose{T}(T, Action{T}, Action{T})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.TryFinallyDispose{TSubject, TResult}(TSubject, Func{TSubject, TResult}, Action{TSubject})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TryFinallyDispose_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.TryFinallyDispose<Stream>(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => Stream.Null.TryFinallyDispose(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.TryFinallyDispose<IDisposable, bool>(null, _ => true)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => Stream.Null.TryFinallyDispose((Func<Stream, bool>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("function");
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T)"/> method.</para>
  /// </summary>
  [Fact]
  public void Print_Console_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.PrintAsync{T}(T, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void PrintAsync_Console_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.PrintAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Print_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().Print((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, Stream stream, Encoding encoding = null)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.PrintAsync{T}(T, Stream, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void PrintAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.PrintAsync<object>(null, Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(Stream.Null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, Stream stream, Encoding encoding = null)
    {
      using (stream)
      {

      }
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Print_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null, Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().Print((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, TextWriter writer)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.PrintAsync{T}(T, TextWriter, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void PrintAsync_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.PrintAsync<object>(null, Stream.Null.ToStreamWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(Stream.Null.ToStreamWriter(), Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, TextWriter writer)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Print_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().Print((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, XmlWriter writer)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.PrintAsync{T}(T, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void PrintAsync_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.PrintAsync<object>(null, Stream.Null.ToXmlWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, XmlWriter writer)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T, BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void Print_BinaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null, Stream.Null.ToBinaryWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().Print((BinaryWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

      Validate(Attributes.EmptyStream().ToBinaryWriter(), Attributes.RandomString());
    }

    return;

    static void Validate(BinaryWriter writer, string text)
    {
      using (writer)
      {
        text.Print(writer).Should().BeOfType<string>().And.BeSameAs(text);

        using var reader = writer.BaseStream.MoveToStart().ToBinaryReader();

        reader.ToText().Should().BeOfType<string>().And.Be(text.ToStateString());
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Print_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null, Attributes.RandomFakeFile())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().Print((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, FileInfo file, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.PrintAsync{T}(T, FileInfo, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void PrintAsync_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.PrintAsync<object>(null, Attributes.RandomFakeFile())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(Attributes.RandomFakeFile(), null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, FileInfo file, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T, Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Print_Uri_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null, Attributes.LocalHost())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().Print((Uri) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, Uri uri, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.PrintAsync{T}(T, Uri, Encoding, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void PrintAsync_Uri_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.PrintAsync<object>(null, Attributes.LocalHost())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(Attributes.LocalHost(), null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, Uri uri, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.Print{T}(T, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Print_Process_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.Print<object>(null, Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().Print((Process) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.PrintAsync{T}(T, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void PrintAsync_Process_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.PrintAsync<object>(null, Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => new object().PrintAsync(Process.GetCurrentProcess(), Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ObjectExtensions.GetState(object, IEnumerable{string})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.GetState{T}(T, IEnumerable{Expression{Func{T, object}}})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void GetState_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.GetState(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

      /*var subject = new TestObject();
      var property = Attributes.RandomString();

      subject.Properties(new Dictionary<string, object> { { "PublicProperty", property }, { "property", new object() } }).Should().BeSameAs(subject);
      subject.Property("PublicProperty").Should().Be(property);

      subject.Properties(new
      {
        PublicProperty = property,
        property = new object()
      }).Should().BeSameAs(subject);

      subject.Property("PublicProperty").Should().Be(property);*/
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.GetState(null, (IEnumerable<Expression<Func<object, object>>>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
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
  ///     <item><description><see cref="ObjectExtensions.SetState{T}(T, IEnumerable{ValueTuple{string, object}})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.SetState{T}(T, object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void SetState_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SetState<object>(null, Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SetState(null)).ThrowExactly<ArgumentNullException>().WithParameterName("properties");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SetState<object>(null, new object())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SetState((object) null)).ThrowExactly<ArgumentNullException>().WithParameterName("properties");
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.GetMember{T, TResult}(T, Expression{Func{T, TResult}})"/> method.</para>
  /// </summary>
  [Fact]
  public void GetMember_Method()
  {
    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ObjectExtensions.GetMember(null, Enumerable.Empty<Expression<Func<object, object>>>().First())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
        //AssertionExtensions.Should(() => new object().GetMember<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("expression");

        var text = Attributes.RandomString();
      text.GetMember(instance => instance.Length).Should().Be(text.Length);
      text.GetMember(instance => instance.ToString(CultureInfo.InvariantCulture)).Should().BeOfType<string>().And.Be(text);
      DateTime.UtcNow.GetMember(instance => instance.Ticks <= DateTime.UtcNow.Ticks).Should().BeTrue();
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.GetFieldValue{T}(object, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void GetFieldValue_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.GetFieldValue<object>(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().GetFieldValue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

      //var result = new object().GetFieldValue<Guid>("field");

      //var subject = new TestObject {PublicField = "value"};
      //subject.Field("PublicField").To<string>().Should().Be("value");
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(T result, object instance, string name) => instance.GetFieldValue<T>(name).Should().BeOfType<T>().And.BeSameAs(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.GetPropertyValue{T}(object, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void GetPropertyValue_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.GetPropertyValue<object>(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().GetPropertyValue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

      /*new object().Property("property").Should().BeNull();

      var subject = new TestObject {PublicProperty = "value"};
      subject.Property("PublicProperty").As<string>().Should().Be("value");


      AssertionExtensions.Should(() => ObjectExtensions.Property<object>(null, "property", new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new object().Property(null, new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new object().Property(string.Empty, new object())).ThrowExactly<ArgumentException>();

      subject = new TestObject();

      subject.Property("PublicProperty", null).Should().BeSameAs(subject);

      var property = Attributes.RandomString();

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
      subject.Property("PrivateProperty").Should().Be(property);*/
    }

    throw new NotImplementedException();

    return;

    static void Validate<T>(T result, object instance, string name) => instance.GetPropertyValue<T>(name).Should().BeOfType<T>().And.BeSameAs(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SetPropertyValue{T}(T, string, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void SetPropertyValue_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SetPropertyValue<object>(null, string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SetPropertyValue(null, null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");
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
  ///     <item><description><see cref="ObjectExtensions.CallMethod{T}(object, string, IEnumerable{object})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.CallMethod{T}(object, string, object[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void CallMethod_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.CallMethod<object>(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().CallMethod<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.CallMethod<object>(null, string.Empty, [])).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().CallMethod<object>(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("name");

      /*new object().Method("method").Should().BeNull();
      ((bool) string.Empty.Method("Contains", string.Empty)).Should().BeTrue();*/
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.ToSequence{T}(T, T[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSequence_Method()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.ToFormattedString(object, IFormatProvider, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToFormattedString_Method()
  {
    using (new AssertionScope())
    {
      Validate(string.Empty, null);
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, object instance, IFormatProvider provider = null, string format = null) => instance.ToFormattedString(provider, format).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.ToInvariantString(object, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToInvariantString_Method()
  {
    using (new AssertionScope())
    {
      Validate(string.Empty, null);
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, object instance, string format = null) => instance.ToInvariantString(format).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ObjectExtensions.ToStateString{T}(T, IEnumerable{Expression{Func{T, object}}})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.ToStateString(object, string[])"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.ToStateString{T}(T, IEnumerable{Expression{Func{T, object}}})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.ToStateString{T}(T, Expression{Func{T, object}}[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToStateString_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.ToStateString(null, Enumerable.Empty<string>())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.ToStateString(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

      /*new object().ToStringState("property").Should().Be("[]");
      new object().ToStringState((string[]) null).Should().Be("[]");
      new object().ToStringState().Should().Be("[]");
      new object().ToStringState((Expression<Func<object, object>>[]) null).Should().Be("[]");
      new object().ToStringState(Array.Empty<Expression<Func<object, object>>>()).Should().Be("[]");

      var date = DateTime.UtcNow;
      date.ToStringState("Ticks").Should().Be($"[Ticks:\"{date.Ticks}\"]");
      date.ToStringState(date => date.Ticks).Should().Be($"[Ticks:\"{date.Ticks}\"]");
      date.ToStringState("Day", "Month", "Year").Should().Be($"[Day:\"{date.Day}\", Month:\"{date.Month}\", Year:\"{date.Year}\"]");
      date.ToStringState(date => date.Day, date => date.Month, date => date.Year).Should().Be($"[Day:\"{date.Day}\", Month:\"{date.Month}\", Year:\"{date.Year}\"]");
      date.ToStringState("Today").Should().Be($"[Today:\"{DateTime.Today}\"]");*/

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.ToStateString(null, Enumerable.Empty<Expression<Func<object, object>>>())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.ToStateString(null, Array.Empty<Expression<Func<object, object>>>())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsDataContract{T}(T, XmlWriter, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsDataContract_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsDataContract<object>(null, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SerializeAsDataContract((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, XmlWriter writer, params Type[] types)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsDataContract{T}(T, TextWriter, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsDataContract_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsDataContract<object>(null, Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SerializeAsDataContract((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, TextWriter writer, params Type[] types)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsDataContract{T}(T, Stream, Encoding, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsDataContract_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsDataContract<object>(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SerializeAsDataContract((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, Stream stream, Encoding encoding = null, params Type[] types)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsDataContract{T}(T, FileInfo, Encoding, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsDataContract_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsDataContract<object>(null, Attributes.RandomFakeFile())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SerializeAsDataContract((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, FileInfo file, Encoding encoding = null, params Type[] types)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsDataContract(object, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsDataContract_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsDataContract(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, params Type[] types)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsXml{T}(T, XmlWriter, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsXml_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsXml<object>(null, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SerializeAsXml((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, XmlWriter writer, params Type[] types)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsXml{T}(T, TextWriter, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsXml_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsXml<object>(null, Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SerializeAsXml((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, TextWriter writer, params Type[] types)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsXml{T}(T, Stream, Encoding, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsXml_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsXml<object>(null, Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SerializeAsXml((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

      /*var subject = Attributes.RandomString();

      var xml = subject.AsXml();
      var stringWriter = new StringWriter();
      stringWriter.ToXmlWriter().Write(writer =>
      {
        new XmlSerializer(subject.GetType()).Serialize(writer, subject);
        stringWriter.ToString().Should().Be(xml);
      });
      subject.AsXml((Type[]) null).Should().Be(xml);
      subject.AsXml(Array.Empty<Type>()).Should().Be(xml);
      subject.AsXml((Type[]) null).Should().Be(xml);

      using (var stream = new MemoryStream())
      {
        subject.AsXml(stream, Encoding.Unicode).Should().BeSameAs(subject);
        stream.Rewind().Text().Should().Be(xml);
        stream.CanWrite.Should().BeTrue();
      }

      using (var writer = new StringWriter())
      {
        subject.AsXml(writer).Should().BeSameAs(subject);
        writer.ToString().Should().Be(xml);
        writer.WriteLine();
      }

      stringWriter = new StringWriter();
      using (var writer = stringWriter.ToXmlWriter())
      {
        subject.AsXml(writer).Should().BeSameAs(subject);
        stringWriter.ToString().Should().Be(xml);
        stringWriter.WriteLine();
      }*/

      // TODO Encoding support
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, Stream stream, Encoding encoding = null, params Type[] types)
    {
      using (stream)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsXml{T}(T, FileInfo, Encoding, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsXml_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsXml<object>(null, Attributes.RandomFakeFile())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
      AssertionExtensions.Should(() => new object().SerializeAsXml((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, FileInfo file, Encoding encoding = null, params Type[] types)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ObjectExtensions.SerializeAsXml(object, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsXml_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ObjectExtensions.SerializeAsXml(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    }

    throw new NotImplementedException();

    return;

    static void Validate(object instance, params Type[] types)
    {
    }
  }
}