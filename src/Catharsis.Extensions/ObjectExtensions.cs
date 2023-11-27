using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for basic object type.</para>
/// </summary>
/// <seealso cref="object"/>
public static class ObjectExtensions
{
  /// <summary>
  ///   <para>Determines if the object is compatible with the given type, as specified by the <c>is</c> operator.</para>
  /// </summary>
  /// <typeparam name="T">Type of object.</typeparam>
  /// <param name="instance">Object whose type compatibility with <typeparamref name="T"/> is to be determined.</param>
  /// <returns><c>true</c> if <paramref name="instance"/> is type-compatible with <typeparamref name="T"/>, <c>false</c> if not.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool Is<T>(this object instance) => instance is not null ? instance is T : throw new ArgumentNullException(nameof(instance));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static bool IsSameAs(this object left, object right) => ReferenceEquals(left, right);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static bool IsNull(this object instance)
  {
    return instance switch
    {
      WeakReference reference => !reference.IsAlive || reference.Target is null,
      _ => instance is null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static bool IsEmpty<T>(this T? instance) where T : struct => !instance.HasValue || instance.Value.ToString().IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static bool IsEmpty<T>(this Lazy<T> instance) => instance is not null ? !instance.IsValueCreated || instance.Value is null || instance.Value.ToString().IsEmpty() : throw new ArgumentNullException(nameof(instance));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static T As<T>(this object instance) where T : class => instance as T;

  /// <summary>
  ///   <para>Tries to convert given object to specified type and throws exception on failure.</para>
  /// </summary>
  /// <typeparam name="T">Type to convert object to.</typeparam>
  /// <param name="instance">Object to convert.</param>
  /// <returns>Object, converted to the specified type.</returns>
  /// <exception cref="InvalidCastException">If conversion to specified type cannot be performed.</exception>
  /// <remarks>If specified object instance is a <c>null</c> reference, a <c>null</c> reference will be returned as a result.</remarks>
  public static T To<T>(this object instance) => (T) instance;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="action"></param>
  /// <param name="condition"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T With<T>(this T instance, Action<T> action, Predicate<T> condition = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (action is null) throw new ArgumentNullException(nameof(action));

    if (condition is not null)
    {
      action.Execute(condition, instance);
    }
    else
    {
      action(instance);
    }

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="condition"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T While<T>(this T instance, Predicate<T> condition, Action<T> action)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (condition is null) throw new ArgumentNullException(nameof(condition));
    if (action is null) throw new ArgumentNullException(nameof(action));

    action.Execute(condition, instance);

    return instance;
  }

  /// <summary>
  ///   <para>Determines whether specified objects are considered equal by comparing values of the given set of properties/fields on each of them.</para>
  ///   <para>The following algorithm is used in equality determination:
  ///     <list type="bullet">
  ///       <item><description>If both <paramref name="left"/> and <paramref name="right"/> are <c>null</c> references, method returns <c>true</c>.</description></item>
  ///       <item><description>If one of compared objects is <c>null</c> and another is not, method returns <c>false</c>.</description></item>
  ///       <item><description>If both objects references are equal (they represent the same object instance), method returns <c>true</c>.</description></item>
  ///       <item><description>If <typeparamref name="T"/> type does not contain any properties/fields in <paramref name="properties"/> set, <see cref="object.Equals(object, object)"/> method is used for equality comparison.</description></item>
  ///       <item><description>If <typeparamref name="T"/> type contains any of the properties/fields in <paramref name="properties"/> set, their values are used for equality comparison according to <see cref="object.Equals(object)"/> method of both <paramref name="left"/> and <paramref name="right"/> instances.</description></item>
  ///     </list>
  ///   </para>
  /// </summary>
  /// <typeparam name="T">Type of objects to compare.</typeparam>
  /// <param name="left">Current object to compare with the second.</param>
  /// <param name="right">Second object to compare with the current one.</param>
  /// <param name="properties">Set of properties/fields whose values are used in equality comparison.</param>
  /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are considered equal, <c>false</c> otherwise.</returns>
  public static bool Equality<T>(this T left, T right, IEnumerable<string> properties)
  {
    if (left is null && right is null)
    {
      return true;
    }

    if (left is null || right is null)
    {
      return false;
    }

    if (left.IsSameAs(right))
    {
      return true;
    }

    var propertiesArray = properties.AsArray();

    if (propertiesArray.Length == 0)
    {
      return left.Equals(right);
    }

    var type = left.GetType();
    var typeProperties = propertiesArray.Select(property => type.AnyProperty(property)).Where(property => property is not null).AsArray();
    var typeFields = propertiesArray.Select(field => type.AnyField(field)).Where(field => field is not null).AsArray();

    if (typeProperties.Length == 0 && typeFields.Length == 0)
    {
      return Equals(left, right);
    }

    return typeProperties.All(property =>
    {
      var firstProperty = property.GetValue(left, null);
      object secondProperty = null;
      try
      {
        secondProperty = property.GetValue(right, null);
      }
      catch
      {
      }

      return Equals(firstProperty, secondProperty);
    }) && typeFields.All(field =>
    {
      var first = field.GetValue(left);
      object second = null;
      try
      {
        second = field.GetValue(right);
      }
      catch
      {
      }

      return Equals(first, second);
    });
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  public static bool Equality<T>(this T left, T right, params string[] properties) => left.Equality(right, properties as IEnumerable<string>);

  /// <summary>
  ///   <para>Determines whether specified objects are considered equal by comparing values of the given set of properties, represented as expression trees, on each of them.</para>
  ///   <para>The following algorithm is used in equality determination:
  ///     <list type="bullet">
  ///       <item><description>If both <paramref name="left"/> and <paramref name="right"/> are <c>null</c> references, method returns <c>true</c>.</description></item>
  ///       <item><description>If one of compared objects is <c>null</c> and another is not, method returns <c>false</c>.</description></item>
  ///       <item><description>If both objects references are equal (they represent the same object instance), method returns <c>true</c>.</description></item>
  ///       <item><description>If <typeparamref name="T"/> type does not contain any properties in <paramref name="properties"/> set, <see cref="object.Equals(object, object)"/> method is used for equality comparison.</description></item>
  ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for equality comparison according to <see cref="object.Equals(object)"/> method of both <paramref name="left"/> and <paramref name="right"/> instances.</description></item>
  ///     </list>
  ///   </para>
  /// </summary>
  /// <typeparam name="T">Type of objects to compare.</typeparam>
  /// <param name="left">Current object to compare with the second.</param>
  /// <param name="right">Second object to compare with the current one.</param>
  /// <param name="properties">Set of properties in a form of expression trees, whose values are used in equality comparison.</param>
  /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are considered equal, <c>false</c> otherwise.</returns>
  public static bool Equality<T>(this T left, T right, IEnumerable<Expression<Func<T, object>>> properties)
  {
    if (left is null && right is null)
    {
      return true;
    }

    if (left is null || right is null)
    {
      return false;
    }

    if (left.IsSameAs(right))
    {
      return true;
    }

    var propertiesArray = properties.AsArray();

    if (propertiesArray.Length == 0)
    {
      return left.Equals(right);
    }

    return properties.Select(expression => expression.Compile()).All(func => Equals(func(left), func(right)));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  public static bool Equality<T>(this T left, T right, params Expression<Func<T, object>>[] properties) => left.Equality(right, properties as IEnumerable<Expression<Func<T, object>>>);

  /// <summary>
  ///   <para>Returns a hash value of a given object, using specified set of properties in its calculation.</para>
  ///   <para>The following algorithm is used in hash code calculation:
  ///     <list type="bullet">
  ///       <item><description>If <paramref name="instance"/> is a <c>null</c> reference, methods returns 0.</description></item>
  ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="instance"/>'s properties hash codes is returned in that case.</description></item>
  ///     </list>
  ///   </para>
  /// </summary>
  /// <typeparam name="T">Type of object.</typeparam>
  /// <param name="instance">Target object, whose hash code is to be returned.</param>
  /// <param name="properties">Collection of properties names, whose values are to be used in hash code's calculation.</param>
  /// <returns>Hash code for <paramref name="instance"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static int HashCode<T>(this T instance, IEnumerable<string> properties)
  {
    if (properties is null) throw new ArgumentNullException(nameof(properties));

    if (instance is null)
    {
      return 0;
    }

    var propertiesArray = properties.AsArray();

    if (propertiesArray.Length == 0)
    {
      return instance.GetHashCode();
    }

    var hash = 0;

    properties.Select(name => instance.GetType().AnyProperty(name))
              .Where(property => property is not null)
              .Select(property => property.GetValue(instance, null))
              .Where(property => property is not null)
              .ForEach(value => hash += value.GetHashCode());

    return hash;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  public static int HashCode<T>(this T instance, params string[] properties) => instance.HashCode(properties as IEnumerable<string>);

  /// <summary>
  ///   <para>Returns a hash value of a given object, using specified set of properties, represented as expression trees, in its calculation.</para>
  ///   <para>The following algorithm is used in hash code calculation:
  ///     <list type="bullet">
  ///       <item><description>If <paramref name="instance"/> is a <c>null</c> reference, methods returns 0.</description></item>
  ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="instance"/>'s properties hash codes is returned in that case.</description></item>
  ///     </list>
  ///   </para>
  /// </summary>
  /// <typeparam name="T">Type of object.</typeparam>
  /// <param name="instance">Target object, whose hash code is to be returned.</param>
  /// <param name="properties">Collection of properties in a form of expression trees, whose values are to be used in hash code's calculation.</param>
  /// <returns>Hash code for <paramref name="instance"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static int HashCode<T>(this T instance, IEnumerable<Expression<Func<T, object>>> properties)
  {
    if (properties is null) throw new ArgumentNullException(nameof(properties));

    if (instance is null)
    {
      return 0;
    }

    var propertiesArray = properties.AsArray();

    if (propertiesArray.Length == 0)
    {
      return instance.GetHashCode();
    }

    var hash = 0;

    properties.Select(expression => expression.Compile()(instance)).AsNotNullable().ForEach(value => hash += value.GetHashCode());

    return hash;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  public static int HashCode<T>(this T instance, params Expression<Func<T, object>>[] properties) => instance.HashCode(properties as IEnumerable<Expression<Func<T, object>>>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="action"></param>
  /// <param name="finalizer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T TryFinally<T>(this T instance, Action<T> action, Action<T> finalizer = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return instance.TryFinally(_ =>
    {
      action(instance);
      return instance;
    }, finalizer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TSubject"></typeparam>
  /// <typeparam name="TResult"></typeparam>
  /// <param name="instance"></param>
  /// <param name="function"></param>
  /// <param name="finalizer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TResult TryFinally<TSubject, TResult>(this TSubject instance, Func<TSubject, TResult> function, Action<TSubject> finalizer = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (function is null) throw new ArgumentNullException(nameof(function));

    try
    {
      return function(instance);
    }
    finally
    {
      finalizer?.Invoke(instance);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="action"></param>
  /// <param name="exception"></param>
  /// <param name="finalizer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Exception TryCatchFinally<T>(this T instance, Action<T> action, Action<T> exception = null, Action<T> finalizer = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return instance.TryCatchFinally<T, Exception>(action, exception, finalizer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="TException"></typeparam>
  /// <param name="instance"></param>
  /// <param name="function"></param>
  /// <param name="exception"></param>
  /// <param name="finalizer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TException TryCatchFinally<T, TException>(this T instance, Action<T> function, Action<T> exception = null, Action<T> finalizer = null) where TException : Exception
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (function is null) throw new ArgumentNullException(nameof(function));

    try
    {
      function(instance);

      return null;
    }
    catch (TException e)
    {
      exception?.Invoke(instance);
      return e;
    }
    finally
    {
      finalizer?.Invoke(instance);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="action"></param>
  /// <param name="finalizer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T TryFinallyDispose<T>(this T instance, Action<T> action, Action<T> finalizer = null) where T : IDisposable
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (action is null)  throw new ArgumentNullException(nameof(action));

    return instance.TryFinallyDispose(_ =>
    {
      action(instance);
      return instance;
    }, finalizer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TSubject"></typeparam>
  /// <typeparam name="TResult"></typeparam>
  /// <param name="instance"></param>
  /// <param name="function"></param>
  /// <param name="finalizer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TResult TryFinallyDispose<TSubject, TResult>(this TSubject instance, Func<TSubject, TResult> function, Action<TSubject> finalizer = null) where TSubject : IDisposable
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (function is null) throw new ArgumentNullException(nameof(function));

    try
    {
      return function(instance);
    }
    finally
    {
      finalizer?.Invoke(instance);
      instance.Dispose();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Print<T>(this T instance)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    instance.Print(Console.Out);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> PrintAsync<T>(this T instance, CancellationToken cancellation = default)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    cancellation.ThrowIfCancellationRequested();

    await instance.PrintAsync(Console.Out, cancellation).ConfigureAwait(false);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Print<T>(this T instance, Stream destination, Encoding encoding = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToStreamWriter(encoding, false);

    return instance.Print(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> PrintAsync<T>(this T instance, Stream destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await using var writer = destination.ToStreamWriter(encoding, false);

    return await instance.PrintAsync(writer, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Print<T>(this T instance, TextWriter destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.Write(instance.ToStateString());

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> PrintAsync<T>(this T instance, TextWriter destination, CancellationToken cancellation = default)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteAsync(instance.ToStateString().ToReadOnlyMemory(), cancellation).ConfigureAwait(false);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Print<T>(this T instance, XmlWriter destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(instance.ToStateString());

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> PrintAsync<T>(this T instance, XmlWriter destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteTextAsync(instance.ToStateString()).ConfigureAwait(false);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Print<T>(this T instance, BinaryWriter destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(instance.ToStateString());

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Print<T>(this T instance, FileInfo destination, Encoding encoding = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToStreamWriter(encoding);

    return instance.Print(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> PrintAsync<T>(this T instance, FileInfo destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await using var writer = destination.ToStreamWriter(encoding);

    return await instance.PrintAsync(writer, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Print<T>(this T instance, Uri destination, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var stream = destination.ToStream(timeout, headers);

    return instance.Print(stream, encoding);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> PrintAsync<T>(this T instance, Uri destination, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await using var stream = await destination.ToStreamAsync(timeout, headers).ConfigureAwait(false);

    return await instance.PrintAsync(stream, encoding, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Print<T>(this T instance, Process destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    return instance.Print(destination.StandardInput);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<T> PrintAsync<T>(this T instance, Process destination, CancellationToken cancellation = default) => await instance.PrintAsync(destination.StandardInput, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para>Creates and returns a dictionary from the values of public properties of target object.</para>
  /// </summary>
  /// <param name="instance">Target object whose properties values are returned.</param>
  /// <param name="properties"></param>
  /// <returns>Dictionary of name - value pairs for public properties of <paramref name="instance"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<(string Name, object Value)> GetState(this object instance, IEnumerable<string> properties = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    var type = instance.GetType();
    var typeProperties = properties?.Select(property => type.AnyProperty(property)) ?? instance.GetType().GetProperties();

    return typeProperties.Select(property => (property.Name, instance.GetPropertyValue<object>(property.Name)));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<(string Name, object Value)> GetState<T>(this T instance, IEnumerable<Expression<Func<T, object>>> properties = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    return properties is null ? instance.GetState((IEnumerable<string>) null) : properties.Select(property => (property.Body.To<UnaryExpression>().Operand.To<MemberExpression>().Member.Name, property.Compile()(instance)));
  }

  /// <summary>
  ///   <para>Sets values of several properties on specified target object.</para>
  /// </summary>
  /// <typeparam name="T">Type of target object.</typeparam>
  /// <param name="instance">Target object whose properties are to be changed.</param>
  /// <param name="properties">Object whose public properties are to be used for setting matched ones on target object.</param>
  /// <returns>Back reference to the current target object.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SetState<T>(this T instance, IEnumerable<(string Name, object Value)> properties)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (properties is null) throw new ArgumentNullException(nameof(properties));

    properties.ForEach(property => instance.SetPropertyValue(property.Name, property.Value));

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SetState<T>(this T instance, object properties)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (properties is null) throw new ArgumentNullException(nameof(properties));

    return instance.SetState(properties.GetState());
  }


  /// <summary>
  ///   <para>Returns the value of a member on a target object, using expression tree to specify type's member.</para>
  /// </summary>
  /// <typeparam name="T">Type of target object.</typeparam>
  /// <typeparam name="TResult">Type of <paramref name="instance"/>'s member.</typeparam>
  /// <param name="instance">Target object, whose member's value is to be returned.</param>
  /// <param name="expression">Lambda expression that represents a member of <typeparamref name="T"/> type, whose value for <paramref name="instance"/> instance is to be returned. Generally it should represents either a public property/field or no-arguments method.</param>
  /// <returns>Value of member of <typeparamref name="T"/> type on a <paramref name="instance"/> instance.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static TResult GetMember<T, TResult>(this T instance, Expression<Func<T, TResult>> expression)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (expression is null) throw new ArgumentNullException(nameof(expression));

    return expression.Compile()(instance);
  }

  /// <summary>
  ///   <para>Returns the value of object's field with a specified name.</para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance">Object whose field's value is to be returned.</param>
  /// <param name="name">Name of field of <paramref name="instance"/>'s type.</param>
  /// <returns>Value of <paramref name="instance"/>'s field with a given <paramref name="name"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static T GetFieldValue<T>(this object instance, string name)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (name is null) throw new ArgumentNullException(nameof(name));

    var field = instance.GetType().AnyField(name);
    
    if (field is null)
    {
      throw new InvalidOperationException($"Instance of type {instance.GetType()} has no field named \"{name}\"");
    }

    return (T) field.GetValue(instance);
  }

  /// <summary>
  ///   <para>Returns the value of given property for specified target object.</para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance">Target object, whose property's value is to be returned.</param>
  /// <param name="name">Name of property to inspect.</param>
  /// <returns>Value of property <paramref name="name"/> for <paramref name="instance"/> instance, or a <c>null</c> reference in case this property does not exists for <paramref name="instance"/>'s type.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static T GetPropertyValue<T>(this object instance, string name)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (name is null) throw new ArgumentNullException(nameof(name));

    var property = instance.GetType().AnyProperty(name);

    if (property is null)
    {
      throw new InvalidOperationException($"Instance of type {instance.GetType()} has no property named \"{name}\"");
    }

    return property.CanRead ? (T) property.GetValue(instance, null) : default;
  }

  /// <summary>
  ///   <para>Sets the value of given property on specified target object.</para>
  /// </summary>
  /// <typeparam name="T">Type of target object.</typeparam>
  /// <param name="instance">Target object whose property is to be changed.</param>
  /// <param name="name">Name of property to change.</param>
  /// <param name="value">New value of object's property.</param>
  /// <returns>Back reference to the current target object.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SetPropertyValue<T>(this T instance, string name, object value)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (name is null) throw new ArgumentNullException(nameof(name));

    var property = instance.GetType().AnyProperty(name);

    if (property is null)
    {
      throw new InvalidOperationException($"Instance of type {instance.GetType()} has no property named \"{name}\"");
    }

    if (property.CanWrite)
    {
      property.SetValue(instance, value, null);
    }

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="name"></param>
  /// <param name="parameters"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T CallMethod<T>(this object instance, string name, IEnumerable<object> parameters = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (name is null) throw new ArgumentNullException(nameof(name));

    var method = instance.GetType().AnyMethod(name);

    if (method is null)
    {
      throw new InvalidOperationException($"Instance of type {instance.GetType()} has no method named \"{name}\"");
    }

    return (T) method.Invoke(instance, parameters?.AsArray());
  }

  /// <summary>
  ///   <para>Calls/invokes instance method on a target object, passing specified parameters.</para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance">The object on which to invoke the method.</param>
  /// <param name="name">Name of the method to be invoked.</param>
  /// <param name="parameters">Optional set of parameters to be passed to invoked method, if it requires some.</param>
  /// <returns>An object containing the return value of the invoked method.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T CallMethod<T>(this object instance, string name, params object[] parameters) => instance.CallMethod<T>(name, parameters as IEnumerable<object>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="elements"></param>
  /// <returns></returns>
  public static IEnumerable<T> ToSequence<T>(this T instance, params T[] elements)
  {
    var collection = new List<T>(elements.Length + 1) {instance};

    collection.AddRange(elements);

    return collection;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="provider"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToFormattedString(this object instance, IFormatProvider provider = null, string format = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    return provider is null ? FormattableString.Invariant($"{instance}") : string.Format(provider, format is null ? "{0}" : $"{{0:{format}}}", instance);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToInvariantString(this object instance, string format = null) => instance.ToFormattedString(null, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToStateString(this object instance, IEnumerable<string> properties = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    if (instance is string text)
    {
      return text;
    }

    var state = instance.GetState(properties);

    return $"[{state.Select(property => $"{property.Name}:\"{property.Value?.ToInvariantString()}\"").Join(", ")}]";
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToStateString(this object instance, params string[] properties) => instance.ToStateString(properties as IEnumerable<string>);

  /// <summary>
  ///   <para>Returns a generic string representation of object, using values of specified properties in a form of lambda expressions.</para>
  /// </summary>
  /// <typeparam name="T">Type of target object.</typeparam>
  /// <param name="instance">Object to be converted to string representation.</param>
  /// <param name="properties">Set of properties, whose values are used for string representation of <paramref name="instance"/>. Each property is represented as a lambda expression.</param>
  /// <returns>String representation of <paramref name="instance"/>. Property name is separated from value by colon character, name-value pairs are separated by comma and immediately following space characters, and all content is placed in square brackets afterwards.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToStateString<T>(this T instance, IEnumerable<Expression<Func<T, object>>> properties = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    if (instance is string text)
    {
      return text;
    }

    var state = instance.GetState(properties);

    return $"[{state.Select(property => $"{property.Name}:\"{property.Value?.ToInvariantString()}\"").Join(", ")}]";
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToStateString<T>(this T instance, params Expression<Func<T, object>>[] properties) => instance.ToStateString(properties as IEnumerable<Expression<Func<T, object>>>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SerializeAsDataContract<T>(this T instance, XmlWriter destination, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    var serializer = new DataContractSerializer(typeof(T), types);

    serializer.WriteObject(destination, instance);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SerializeAsDataContract<T>(this T instance, TextWriter destination, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(false);

    return instance.SerializeAsDataContract(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SerializeAsDataContract<T>(this T instance, Stream destination, Encoding encoding = null, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding, false);

    return instance.SerializeAsDataContract(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SerializeAsDataContract<T>(this T instance, FileInfo destination, Encoding encoding = null, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding);

    return instance.SerializeAsDataContract(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string SerializeAsDataContract(this object instance, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    using var destination = new StringWriter();

    instance.SerializeAsDataContract(destination, types);

    return destination.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SerializeAsXml<T>(this T instance, XmlWriter destination, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    var serializer = new XmlSerializer(typeof(T), types);

    serializer.Serialize(destination, instance);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SerializeAsXml<T>(this T instance, TextWriter destination, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(false);

    return instance.SerializeAsXml(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SerializeAsXml<T>(this T instance, Stream destination, Encoding encoding = null, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding, false);

    return instance.SerializeAsXml(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T SerializeAsXml<T>(this T instance, FileInfo destination, Encoding encoding = null, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToXmlWriter(encoding);

    return instance.SerializeAsXml(writer, types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string SerializeAsXml(this object instance, params Type[] types)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));

    using var destination = new StringWriter();

    instance.SerializeAsXml(destination, types);

    return destination.ToString();
  }
}