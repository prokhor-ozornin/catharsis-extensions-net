using System.Linq.Expressions;
using System.Text;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for basic object type.</para>
/// </summary>
/// <seealso cref="object"/>
public static class ObjectExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static T? As<T>(this object instance) => instance is T s ? s : default;

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
  ///   <para>Determines if the object is compatible with the given type, as specified by the <c>is</c> operator.</para>
  /// </summary>
  /// <typeparam name="T">Type of object.</typeparam>
  /// <param name="instance">Object whose type compatibility with <typeparamref name="T"/> is to be determined.</param>
  /// <returns><c>true</c> if <paramref name="instance"/> is type-compatible with <typeparamref name="T"/>, <c>false</c> if not.</returns>
  public static bool Is<T>(this object instance) => instance is T;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static bool IsNull(this object? instance)
  {
    return instance switch
    {
      WeakReference reference => !reference.IsAlive || reference.Target == null,
     _ => instance == null
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
  public static bool IsEmpty<T>(this Lazy<T> instance) => !instance.IsValueCreated || instance.Value == null || instance.Value.ToString().IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TSubject"></typeparam>
  /// <typeparam name="TResult"></typeparam>
  /// <param name="instance"></param>
  /// <param name="function"></param>
  /// <param name="finalizer"></param>
  /// <param name="dispose"></param>
  /// <returns></returns>
  public static TResult? UseFinally<TSubject, TResult>(this TSubject instance, Func<TSubject, TResult?> function, Action<TSubject>? finalizer = null, bool dispose = false)
  {
    try
    {
      return function(instance);
    }
    finally
    {
      finalizer?.Invoke(instance);

      if (dispose && instance is IDisposable disposable)
      {
        disposable.Dispose();
      }
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="action"></param>
  /// <param name="finalizer"></param>
  /// <param name="dispose"></param>
  /// <returns></returns>
  public static T UseFinally<T>(this T instance, Action<T> action, Action<T>? finalizer = null, bool dispose = false) => instance.UseFinally(instance => { action(instance); return instance; }, finalizer, dispose)!;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TSubject"></typeparam>
  /// <typeparam name="TResult"></typeparam>
  /// <param name="instance"></param>
  /// <param name="function"></param>
  /// <returns></returns>
  public static TResult? Use<TSubject, TResult>(this TSubject instance, Func<TSubject, TResult?> function) => function(instance);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="action"></param>
  /// <param name="condition"></param>
  /// <returns></returns>
  public static T Use<T>(this T instance, Action<T> action, Predicate<T>? condition = null)
  {
    if (condition != null)
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
  public static T? While<T>(this T? instance, Predicate<T?> condition, Action<T?> action)
  {
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
  ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for equality comparison.</description></item>
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
  public static bool Equality<T>(this T? left, T? right, params string[] properties)
  {
    if (left == null && right == null)
    {
      return true;
    }

    if (left == null || right == null)
    {
      return false;
    }

    if (ReferenceEquals(left, right))
    {
      return true;
    }

    if (!properties.Any())
    {
      return left.Equals(right);
    }

    var type = left.GetType();
    var typeProperties = properties.Select(property => type.Property(property)).Where(property => property != null);
    var typeFields = properties.Select(field => type.Field(field)).Where(field => field != null);

    if (!typeProperties.Any() && !typeFields.Any())
    {
      return Equals(left, right);
    }

    return
      typeProperties.All(property =>
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
      })
      && typeFields.All(field =>
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
  ///   <para>Determines whether specified objects are considered equal by comparing values of the given set of properties, represented as expression trees, on each of them.</para>
  ///   <para>The following algorithm is used in equality determination:
  ///     <list type="bullet">
  ///       <item><description>If both <paramref name="left"/> and <paramref name="right"/> are <c>null</c> references, method returns <c>true</c>.</description></item>
  ///       <item><description>If one of compared objects is <c>null</c> and another is not, method returns <c>false</c>.</description></item>
  ///       <item><description>If both objects references are equal (they represent the same object instance), method returns <c>true</c>.</description></item>
  ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for equality comparison.</description></item>
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
  public static bool Equality<T>(this T? left, T? right, params Expression<Func<T, object?>>[] properties)
  {
    if (left == null && right == null)
    {
      return true;
    }

    if (left == null || right == null)
    {
      return false;
    }

    if (ReferenceEquals(left, right))
    {
      return true;
    }

    if (!properties.Any())
    {
      return left.Equals(right);
    }

    return properties.Select(expression => expression.Compile()).All(func => Equals(func(left), func(right)));
  }

  /// <summary>
  ///   <para>Returns a hash value of a given object, using specified set of properties in its calculation.</para>
  ///   <para>The following algorithm is used in hash code calculation:
  ///     <list type="bullet">
  ///       <item><description>If <paramref name="instance"/> is a <c>null</c> reference, methods returns 0.</description></item>
  ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for hash code calculation.</description></item>
  ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="instance"/>'s properties hash codes is returned in that case.</description></item>
  ///     </list>
  ///   </para>
  /// </summary>
  /// <typeparam name="T">Type of object.</typeparam>
  /// <param name="instance">Target object, whose hash code is to be returned.</param>
  /// <param name="properties">Collection of properties names, whose values are to be used in hash code's calculation.</param>
  /// <returns>Hash code for <paramref name="instance"/>.</returns>
  public static int HashCode<T>(this T? instance, params string[] properties)
  {
    if (instance == null)
    {
      return 0;
    }

    if (!properties.Any())
    {
      return instance.GetHashCode();
    }

    var hash = 0;
    
    properties.Select(name => instance.GetType().Property(name))
              .Where(property => property != null)
              .Select(property => property.GetValue(instance, null))
              .Where(property => property != null)
              .ForEach(value => hash += value.GetHashCode());
    
    return hash;
  }

  /// <summary>
  ///   <para>Returns a hash value of a given object, using specified set of properties, represented as experssion trees, in its calculation.</para>
  ///   <para>The following algorithm is used in hash code calculation:
  ///     <list type="bullet">
  ///       <item><description>If <paramref name="instance"/> is a <c>null</c> reference, methods returns 0.</description></item>
  ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for hash code calculation.</description></item>
  ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="instance"/>'s properties hash codes is returned in that case.</description></item>
  ///     </list>
  ///   </para>
  /// </summary>
  /// <typeparam name="T">Type of object.</typeparam>
  /// <param name="instance">Target object, whose hash code is to be returned.</param>
  /// <param name="properties">Collection of properties in a form of expression trees, whose values are to be used in hash code's calculation.</param>
  /// <returns>Hash code for <paramref name="instance"/>.</returns>
  public static int HashCode<T>(this T? instance, params Expression<Func<T, object?>>[] properties)
  {
    if (instance == null)
    {
      return 0;
    }

    if (!properties.Any())
    {
      return instance.GetHashCode();
    }

    var hash = 0;
    
    properties.Select(expression => expression.Compile()(instance)).NonNullable().ForEach(value => hash += value.GetHashCode());
    
    return hash;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> Print<T>(this T instance, CancellationToken cancellation = default)
  {
    if (instance != null)
    {
      await Console.Out.Print(instance, cancellation);
    }

    return instance;
  }

  /// <summary>
  ///   <para>Returns the value of a member on a target object, using expression tree to specify type's member.</para>
  /// </summary>
  /// <typeparam name="T">Type of target object.</typeparam>
  /// <typeparam name="TResult">Type of <paramref name="instance"/>'s member.</typeparam>
  /// <param name="instance">Target object, whose member's value is to be returned.</param>
  /// <param name="expression">Lambda expression that represents a member of <typeparamref name="T"/> type, whose value for <paramref name="instance"/> instance is to be returned. Generally it should represents either a public property/field or no-arguments method.</param>
  /// <returns>Value of member of <typeparamref name="T"/> type on a <paramref name="instance"/> instance.</returns>
  public static TResult Member<T, TResult>(this T instance, Expression<Func<T, TResult>> expression) => expression.Compile()(instance);

  /// <summary>
  ///   <para>Returns the value of object's field with a specified name.</para>
  /// </summary>
  /// <param name="instance">Object whose field's value is to be returned.</param>
  /// <param name="name">Name of field of <paramref name="instance"/>'s type.</param>
  /// <returns>Value of <paramref name="instance"/>'s field with a given <paramref name="name"/>.</returns>
  public static object? Field(this object instance, string name) => instance.GetType().Field(name)?.GetValue(instance);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="name"></param>
  /// <param name="parameters"></param>
  /// <returns></returns>
  public static object? Method(this object instance, string name, IEnumerable<object?>? parameters) => instance.GetType().Method(name)?.Invoke(instance, parameters?.AsArray());

  /// <summary>
  ///   <para>Calls/invokes instance method on a target object, passing specified parameters.</para>
  /// </summary>
  /// <param name="instance">The object on which to invoke the method.</param>
  /// <param name="name">Name of the method to be invoked.</param>
  /// <param name="parameters">Optional set of parameters to be passed to invoked method, if it requires some.</param>
  /// <returns>An object containing the return value of the invoked method.</returns>
  public static object? Method(this object instance, string name, params object?[] parameters) => instance.Method(name, parameters as IEnumerable<object>);

  /// <summary>
  ///   <para>Returns the value of given property for specified target object.</para>
  /// </summary>
  /// <param name="instance">Target object, whose property's value is to be returned.</param>
  /// <param name="name">Name of property to inspect.</param>
  /// <returns>Value of property <paramref name="name"/> for <paramref name="instance"/> instance, or a <c>null</c> reference in case this property does not exists for <paramref name="instance"/>'s type.</returns>
  public static object? Property(this object instance, string name)
  {
    var property = instance.GetType().Property(name);

    return property != null && property.CanRead ? property.GetValue(instance, null) : null;
  }

  /// <summary>
  ///   <para>Sets the value of given property on specified target object.</para>
  /// </summary>
  /// <typeparam name="T">Type of target object.</typeparam>
  /// <param name="instance">Target object whose property is to be changed.</param>
  /// <param name="name">Name of property to change.</param>
  /// <param name="value">New value of object's property.</param>
  /// <returns>Back reference to the current target object.</returns>
  public static T Property<T>(this T instance, string name, object? value)
  {
    var propertyInfo = instance?.GetType().Property(name);

    if (propertyInfo != null && propertyInfo.CanWrite)
    {
      propertyInfo.SetValue(instance, value, null);
    }

    return instance;
  }

  /// <summary>
  ///   <para>Creates and returns a dictionary from the values of public properties of target object.</para>
  /// </summary>
  /// <param name="instance">Target object whose properties values are returned.</param>
  /// <returns>Dictionary of name - value pairs for public properties of <paramref name="instance"/>.</returns>
  public static IEnumerable<(string Name, object? Value)> Properties(this object instance) => instance.GetType().GetProperties().Select(property => (property.Name, property.GetValue(instance, null)));

  /// <summary>
  ///   <para>Sets values of several properties on specified target object.</para>
  /// </summary>
  /// <typeparam name="T">Type of target object.</typeparam>
  /// <param name="instance">Target object whose properties are to be changed.</param>
  /// <param name="properties">Object whose public properties are to be used for setting matched ones on target object.</param>
  /// <returns>Back reference to the current target object.</returns>
  public static T Properties<T>(this T instance, object properties)
  {
    properties.GetType().Properties().ForEach(property => instance.Property(property.Name, properties.Property(property.Name)));
    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="provider"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static string ToStringFormatted(this object instance, IFormatProvider? provider = null, string? format = null) => provider == null ? FormattableString.Invariant($"{instance}") : string.Format(provider, format == null ? "{0}" : $"{{0:{format}}}", instance);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static string ToStringInvariant(this object instance, string? format = null) => instance.ToStringFormatted(null, format);

  /// <summary>
  ///   <para>Returns state (names and values of all public properties) for the given object.</para>
  /// </summary>
  /// <param name="instance">Target object whose public properties names and values are to be returned.</param>
  /// <returns>State of <paramref name="instance"/> as a string.</returns>
  /// <remarks>Property name is separated from property value by a colon, each name-value pairs are separated by comma characters.</remarks>
  public static string ToStringState(this object instance) => instance.ToStringState(instance.GetType().GetProperties().Select(property => property.Name).ToArray());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="properties"></param>
  /// <returns></returns>
  public static string ToStringState(this object instance, params string[] properties)
  {
    const string separator = ", ";

    var result = new StringBuilder();

    properties.Where(name => instance.GetType().HasProperty(name)).ForEach(name => result.Append($"{name}:\"{instance.Property(name)?.ToStringInvariant()}\"{separator}"));

    if (result.Length > 0)
    {
      result.Remove(result.Length - separator.Length, separator.Length);
    }

    return $"[{result}]";
  }

  /// <summary>
  ///   <para>Returns a generic string representation of object, using values of specified properties in a form of lambda expressions.</para>
  /// </summary>
  /// <typeparam name="T">Type of target object.</typeparam>
  /// <param name="instance">Object to be converted to string representation.</param>
  /// <param name="properties">Set of properties, whose values are used for string representation of <paramref name="instance"/>. Each property is represented as a lambda expression.</param>
  /// <returns>String representation of <paramref name="instance"/>. Property name is separated from value by colon character, name-value pairs are separated by comma and immediately following space characters, and all content is placed in square brackets afterwards.</returns>
  public static string ToStringState<T>(this T instance, params Expression<Func<T, object?>>[] properties)
  {
    const string separator = ", ";

    var result = new StringBuilder();

    properties.ForEach(property => result.Append($"{property.Body.To<UnaryExpression>().Operand.To<MemberExpression>().Member.Name}:\"{property.Compile()(instance)?.ToStringInvariant()}\"{separator}"));

    if (result.Length > 0)
    {
      result.Remove(result.Length - separator.Length, separator.Length);
    }

    return $"[{result}]";
  }
}