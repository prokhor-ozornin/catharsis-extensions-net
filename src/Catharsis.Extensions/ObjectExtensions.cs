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
  /// <param name="instance"></param>
  extension(object instance)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T As<T>() where T : class => instance as T;

    /// <summary>
    ///   <para>Tries to convert given object to specified type and throws exception on failure.</para>
    /// </summary>
    /// <typeparam name="T">Type to convert object to.</typeparam>
    /// <returns>Object, converted to the specified type.</returns>
    /// <exception cref="InvalidCastException">If conversion to specified type cannot be performed.</exception>
    /// <remarks>If specified object instance is a <c>null</c> reference, a <c>null</c> reference will be returned as a result.</remarks>
    public T To<T>() => (T) instance;

    /// <summary>
    ///   <para>Determines if the object is compatible with the given type, as specified by the <c>is</c> operator.</para>
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <returns><c>true</c> if <paramref name="instance"/> is type-compatible with <typeparamref name="T"/>, <c>false</c> if not.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is <see langword="null"/>.</exception>
    public bool Is<T>() => instance is not null ? instance is T : throw new ArgumentNullException(nameof(instance));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    public bool IsNull
    {
      get
      {
        return instance switch
        {
          WeakReference reference => !reference.IsAlive || reference.Target is null, _ => instance is null
        };
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsSameAs(object other) => ReferenceEquals(instance, other);

    /// <summary>
    ///   <para>Returns the value of object's field with a specified name.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Name of field of <paramref name="instance"/>'s type.</param>
    /// <returns>Value of <paramref name="instance"/>'s field with a given <paramref name="name"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    public T GetFieldValue<T>(string name)
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
    /// <param name="name">Name of property to inspect.</param>
    /// <returns>Value of property <paramref name="name"/> for <paramref name="instance"/> instance, or a <c>null</c> reference in case this property does not exists for <paramref name="instance"/>'s type.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <seealso cref="SetPropertyValue{T}(T, string, object)"/>
    public T GetPropertyValue<T>(string name)
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
    ///   <para></para>
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <seealso cref="CallMethod{T}(object, string, object[])"/>
    public T CallMethod<T>(string name, IEnumerable<object> parameters = null)
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
    /// <param name="name">Name of the method to be invoked.</param>
    /// <param name="parameters">Optional set of parameters to be passed to invoked method, if it requires some.</param>
    /// <returns>An object containing the return value of the invoked method.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <seealso cref="CallMethod{T}(object, string, IEnumerable{object})"/>
    public T CallMethod<T>(string name, params object[] parameters) => instance.CallMethod<T>(name, parameters as IEnumerable<object>);

    /// <summary>
    ///   <para>Creates and returns a dictionary from the values of public properties of target object.</para>
    /// </summary>
    /// <param name="properties"></param>
    /// <returns>Dictionary of name - value pairs for public properties of <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is <see langword="null"/>.</exception>
    /// <seealso cref="GetState{T}(T, IEnumerable{Expression{Func{T, object}}})"/>
    public IEnumerable<(string Name, object Value)> GetState(IEnumerable<string> properties = null)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));

      var type = instance.GetType();
      var typeProperties = properties?.Select(property => type.AnyProperty(property)) ?? instance.GetType().GetProperties();

      return typeProperties.Select(property => (property.Name, instance.GetPropertyValue<object>(property.Name)));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is <see langword="null"/>.</exception>
    public string SerializeAsDataContract(params Type[] types)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));

      using var destination = new StringWriter();

      instance.SerializeAsDataContract(destination, types);

      return destination.ToString();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is <see langword="null"/>.</exception>
    public string SerializeAsXml(params Type[] types)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));

      using var destination = new StringWriter();

      instance.SerializeAsXml(destination, types);

      return destination.ToString();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ToFormattedString(IFormatProvider provider = null, string format = null)
    {
      if (instance is null)
      {
        return string.Empty;
      }

      if (provider is null)
      {
        return FormattableString.Invariant($"{instance}");
      }

      return string.Format(provider, format is null ? "{0}" : $"{{0:{format}}}", instance);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ToInvariantString(string format = null) => instance.ToFormattedString(null, format);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <seealso cref="ToStateString(object, string[])"/>
    public string ToStateString(IEnumerable<string> properties = null)
    {
      if (instance is null)
      {
        return string.Empty;
      }

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
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <seealso cref="ToStateString(object, IEnumerable{string})"/>
    public string ToStateString(params string[] properties) => instance.ToStateString(properties as IEnumerable<string>);
  }

  /// <param name="instance"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(T instance) where T : IDisposable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparamref name="T"/>
    /// <param name="action"></param>
    /// <param name="finalizer"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryFinallyDispose{TSubject, TResult}(TSubject, Func{TSubject, TResult}, Action{TSubject})"/>
    public T TryFinallyDispose(Action<T> action, Action<T> finalizer = null)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return instance.TryFinallyDispose(_ =>
      {
        action(instance);

        return instance;
      }, finalizer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="function"></param>
    /// <param name="finalizer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="function"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryFinallyDispose{T}(T, Action{T}, Action{T})"/>
    public TResult TryFinallyDispose<TResult>(Func<T, TResult> function, Action<T> finalizer = null)
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
  }

  /// <param name="instance"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(T instance)
  {
    /// <summary>
    ///   <para>Returns the value of a member on a target object, using expression tree to specify type's member.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <typeparam name="TResult">Type of <paramref name="instance"/>'s member.</typeparam>
    /// <param name="expression">Lambda expression that represents a member of <typeparamref name="T"/> type, whose value for <paramref name="instance"/> instance is to be returned. Generally it should represents either a public property/field or no-arguments method.</param>
    /// <returns>Value of member of <typeparamref name="T"/> type on a <paramref name="instance"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="expression"/> is <see langword="null"/>.</exception>
    public TResult GetMember<TResult>(Expression<Func<T, TResult>> expression)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (expression is null) throw new ArgumentNullException(nameof(expression));

      return expression.Compile()(instance);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="condition"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public T With(Action<T> action, Predicate<T> condition = null)
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
    /// <param name="condition"></param>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/>, <paramref name="condition"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public T While(Predicate<T> condition, Action<T> action)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (condition is null) throw new ArgumentNullException(nameof(condition));
      if (action is null) throw new ArgumentNullException(nameof(action));

      action.Execute(condition, instance);

      return instance;
    }

    /// <summary>
    ///   <para>Sets the value of given field on specified target object.</para>
    /// </summary>
    /// <param name="name">Name of field to change.</param>
    /// <param name="value">New value of object's field.</param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <seealso cref="GetFieldValue{T}(object, string)"/>
    public T SetFieldValue(string name, object value)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (name is null) throw new ArgumentNullException(nameof(name));

      var field = instance.GetType().AnyField(name);

      if (field is null)
      {
        throw new InvalidOperationException($"Instance of type {instance.GetType()} has no field named \"{name}\"");
      }

      field.SetValue(instance, value);

      return instance;
    }

    /// <summary>
    ///   <para>Sets the value of given property on specified target object.</para>
    /// </summary>
    /// <param name="name">Name of property to change.</param>
    /// <param name="value">New value of object's property.</param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="name"/> is <see langword="null"/>.</exception>
    /// <seealso cref="GetPropertyValue{T}(object, string)"/>
    public T SetPropertyValue(string name, object value)
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
    /// <param name="member"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="member"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="member"/> is invalid string.</exception>
    public T Nullify(string member)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (member is null) throw new ArgumentNullException(nameof(member));
      if (member.IsEmpty) throw new ArgumentException(nameof(member));

      var type = instance.GetType();

      if (type.HasProperty(member))
      {
        instance.SetPropertyValue(member, null);
      }

      if (type.HasField(member))
      {
        instance.SetFieldValue(member, null);
      }

      return instance;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is <see langword="null"/>.</exception>
    /// <seealso cref="GetState(object, IEnumerable{string})"/>
    public IEnumerable<(string Name, object Value)> GetState(IEnumerable<Expression<Func<T, object>>> properties = null)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));

      return properties is null ? instance.GetState((IEnumerable<string>) null) : properties.Select(property => (property.Body.To<UnaryExpression>().Operand.To<MemberExpression>().Member.Name, property.Compile()(instance)));
    }

    /// <summary>
    ///   <para>Sets values of several properties on specified target object.</para>
    /// </summary>
    /// <param name="properties">Object whose public properties are to be used for setting matched ones on target object.</param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="properties"/> is <see langword="null"/>.</exception>
    /// <seealso cref="SetState{T}(T, object)"/>
    public T SetState(IEnumerable<(string Name, object Value)> properties)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (properties is null) throw new ArgumentNullException(nameof(properties));

      properties.ForEach(property => instance.SetPropertyValue(property.Name, property.Value));

      return instance;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="properties"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="properties"/> is <see langword="null"/>.</exception>
    /// <seealso cref="SetState{T}(T, IEnumerable{ValueTuple{string, object}})"/>
    public T SetState(object properties)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (properties is null) throw new ArgumentNullException(nameof(properties));

      return instance.SetState(properties.GetState());
    }

    /// <summary>
    ///   <para>Determines whether specified objects are considered equal by comparing values of the given set of properties/fields on each of them.</para>
    ///   <para>The following algorithm is used in equality determination:
    ///     <list type="bullet">
    ///       <item><description>If both <paramref name="instance"/> and <paramref name="other"/> are <c>null</c> references, method returns <c>true</c>.</description></item>
    ///       <item><description>If one of compared objects is <c>null</c> and another is not, method returns <c>false</c>.</description></item>
    ///       <item><description>If both objects references are equal (they represent the same object instance), method returns <c>true</c>.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type does not contain any properties/fields in <paramref name="properties"/> set, <see cref="object.Equals(object, object)"/> method is used for equality comparison.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties/fields in <paramref name="properties"/> set, their values are used for equality comparison according to <see cref="object.Equals(object)"/> method of both <paramref name="instance"/> and <paramref name="other"/> instances.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <param name="other">Second object to compare with the current one.</param>
    /// <param name="properties">Set of properties/fields whose values are used in equality comparison.</param>
    /// <returns><c>true</c> if <paramref name="instance"/> and <paramref name="other"/> are considered equal, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Equality{T}(T, T, string[])"/>
    public bool Equality(T other, IEnumerable<string> properties)
    {
      if (properties is null) throw new ArgumentNullException(nameof(properties));

      if (instance is null && other is null)
      {
        return true;
      }

      if (instance is null || other is null)
      {
        return false;
      }

      if (instance.IsSameAs(other))
      {
        return true;
      }

      var propertiesArray = properties.AsArray();

      if (propertiesArray.Length == 0)
      {
        return instance.Equals(other);
      }

      var type = instance.GetType();
      var typeProperties = propertiesArray.Select(property => type.AnyProperty(property)).Where(property => property is not null).AsArray();
      var typeFields = propertiesArray.Select(field => type.AnyField(field)).Where(field => field is not null).AsArray();

      if (typeProperties.Length == 0 && typeFields.Length == 0)
      {
        return Equals(instance, other);
      }

      return typeProperties.All(property =>
      {
        var firstProperty = property.GetValue(instance, null);
        object secondProperty = null;

        try
        {
          secondProperty = property.GetValue(other, null);
        }
        catch
        {
          // Ignored
        }

        return Equals(firstProperty, secondProperty);
      }) && typeFields.All(field =>
      {
        var first = field.GetValue(instance);
        object second = null;

        try
        {
          second = field.GetValue(other);
        }
        catch
        {
          // Ignored
        }

        return Equals(first, second);
      });
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <seealso cref="Equality{T}(T, T, IEnumerable{string})"/>
    public bool Equality(T other, params string[] properties) => Equality(instance, other, properties as IEnumerable<string>);

    /// <summary>
    ///   <para>Determines whether specified objects are considered equal by comparing values of the given set of properties, represented as expression trees, on each of them.</para>
    ///   <para>The following algorithm is used in equality determination:
    ///     <list type="bullet">
    ///       <item><description>If both <paramref name="instance"/> and <paramref name="other"/> are <c>null</c> references, method returns <c>true</c>.</description></item>
    ///       <item><description>If one of compared objects is <c>null</c> and another is not, method returns <c>false</c>.</description></item>
    ///       <item><description>If both objects references are equal (they represent the same object instance), method returns <c>true</c>.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type does not contain any properties in <paramref name="properties"/> set, <see cref="object.Equals(object, object)"/> method is used for equality comparison.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for equality comparison according to <see cref="object.Equals(object)"/> method of both <paramref name="instance"/> and <paramref name="other"/> instances.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <param name="other">Second object to compare with the current one.</param>
    /// <param name="properties">Set of properties in a form of expression trees, whose values are used in equality comparison.</param>
    /// <returns><c>true</c> if <paramref name="instance"/> and <paramref name="other"/> are considered equal, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Equality{T}(T, T, Expression{Func{T, object}}[])"/>
    public bool Equality(T other, IEnumerable<Expression<Func<T, object>>> properties)
    {
      if (properties is null) throw new ArgumentNullException(nameof(properties));

      if (instance is null && other is null)
      {
        return true;
      }

      if (instance is null || other is null)
      {
        return false;
      }

      if (instance.IsSameAs(other))
      {
        return true;
      }

      var propertiesArray = properties.AsArray();

      if (propertiesArray.Length == 0)
      {
        return instance.Equals(other);
      }

      return properties.Select(expression => expression.Compile()).All(func => Equals(func(instance), func(other)));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <seealso cref="Equality{T}(T, T, IEnumerable{Expression{Func{T, object}}})"/>
    public bool Equality(T other, params Expression<Func<T, object>>[] properties) => instance.Equality(other, properties as IEnumerable<Expression<Func<T, object>>>);

    /// <summary>
    ///   <para>Returns a hash value of a given object, using specified set of properties in its calculation.</para>
    ///   <para>The following algorithm is used in hash code calculation:
    ///     <list type="bullet">
    ///       <item><description>If <paramref name="instance"/> is a <c>null</c> reference, methods returns 0.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="instance"/>'s properties hash codes is returned in that case.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <param name="properties">Collection of properties names, whose values are to be used in hash code's calculation.</param>
    /// <returns>Hash code for <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is <see langword="null"/>.</exception>
    /// <seealso cref="HashCode{T}(T, string[])"/>
    public int HashCode(IEnumerable<string> properties)
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
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <seealso cref="HashCode{T}(T, IEnumerable{string})"/>
    public int HashCode(params string[] properties) => instance.HashCode(properties as IEnumerable<string>);

    /// <summary>
    ///   <para>Returns a hash value of a given object, using specified set of properties, represented as expression trees, in its calculation.</para>
    ///   <para>The following algorithm is used in hash code calculation:
    ///     <list type="bullet">
    ///       <item><description>If <paramref name="instance"/> is a <c>null</c> reference, methods returns 0.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="instance"/>'s properties hash codes is returned in that case.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <param name="properties">Collection of properties in a form of expression trees, whose values are to be used in hash code's calculation.</param>
    /// <returns>Hash code for <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is <see langword="null"/>.</exception>
    public int HashCode(IEnumerable<Expression<Func<T, object>>> properties)
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
    /// <param name="properties"></param>
    /// <returns></returns>
    public int HashCode(params Expression<Func<T, object>>[] properties) => instance.HashCode(properties as IEnumerable<Expression<Func<T, object>>>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <param name="finalizer"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryFinally{TSubject, TResult}(TSubject, Func{TSubject, TResult}, Action{TSubject})"/>
    public T TryFinally(Action<T> action, Action<T> finalizer = null)
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
    /// <param name="action"></param>
    /// <param name="exception"></param>
    /// <param name="finalizer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryCatchFinally{T, TException}(T, Action{T}, Action{T}, Action{T})"/>
    public Exception TryCatchFinally(Action<T> action, Action<T> exception = null, Action<T> finalizer = null)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return instance.TryCatchFinally<T, Exception>(action, exception, finalizer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <param name="function"></param>
    /// <param name="exception"></param>
    /// <param name="finalizer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="function"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryCatchFinally{T}(T, Action{T}, Action{T}, Action{T})"/>
    public TException TryCatchFinally<TException>(Action<T> function, Action<T> exception = null, Action<T> finalizer = null) where TException : Exception
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
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is <see langword="null"/>.</exception>
    /// <seealso cref="PrintAsync{T}(T, CancellationToken)"/>
    public T Print()
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));

      instance.Print(Console.Out);

      return instance;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Print{T}(T)"/>
    public async Task<T> PrintAsync(CancellationToken cancellation = default)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));

      cancellation.ThrowIfCancellationRequested();

      await instance.PrintAsync(Console.Out, cancellation).ConfigureAwait(false);

      return instance;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="PrintAsync{T}(T, Stream, Encoding, CancellationToken)"/>
    public T Print(Stream destination, Encoding encoding = null)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToStreamWriter(encoding, false);

      return instance.Print(writer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Print{T}(T, Stream, Encoding)"/>
    public async Task<T> PrintAsync(Stream destination, Encoding encoding = null, CancellationToken cancellation = default)
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
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="PrintAsync{T}(T, TextWriter, CancellationToken)"/>
    public T Print(TextWriter destination)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      destination.Write(instance.ToStateString());

      return instance;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Print{T}(T, TextWriter)"/>
    public async Task<T> PrintAsync(TextWriter destination, CancellationToken cancellation = default)
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
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="PrintAsync{T}(T, XmlWriter)"/>
    public T Print(XmlWriter destination)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Print{T}(T, XmlWriter)"/>
    public async Task<T> PrintAsync(XmlWriter destination)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      await destination.WriteTextAsync(instance.ToStateString()).ConfigureAwait(false);

      return instance;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T Print(BinaryWriter destination)
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
    /// <param name="encoding"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="PrintAsync{T}(T, FileInfo, Encoding, CancellationToken)"/>
    public T Print(FileInfo destination, Encoding encoding = null)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToStreamWriter(encoding);

      return instance.Print(writer);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Print{T}(T, FileInfo, Encoding)"/>
    public async Task<T> PrintAsync(FileInfo destination, Encoding encoding = null, CancellationToken cancellation = default)
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
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="timeout"></param>
    /// <param name="headers"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="PrintAsync{T}(T, Uri, Encoding, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/>
    public T Print(Uri destination, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var stream = destination.ToStream(timeout, headers);

      return instance.Print(stream, encoding);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="timeout"></param>
    /// <param name="cancellation"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Print{T}(T, Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/>
    public async Task<T> PrintAsync(Uri destination, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
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
    /// <param name="destination"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="PrintAsync{T}(T, Process, CancellationToken)"/>
    public T Print(Process destination)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      return instance.Print(destination.StandardInput);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Print{T}(T, Process)"/>
    public async Task<T> PrintAsync(Process destination, CancellationToken cancellation = default) => await instance.PrintAsync(destination.StandardInput, cancellation).ConfigureAwait(false);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="types"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T SerializeAsDataContract(XmlWriter destination, params Type[] types)
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
    /// <param name="destination"></param>
    /// <param name="types"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T SerializeAsDataContract(TextWriter destination, params Type[] types)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(false);

      return instance.SerializeAsDataContract(writer, types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="types"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T SerializeAsDataContract(Stream destination, Encoding encoding = null, params Type[] types)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(encoding, false);

      return instance.SerializeAsDataContract(writer, types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="types"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T SerializeAsDataContract(FileInfo destination, Encoding encoding = null, params Type[] types)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(encoding);

      return instance.SerializeAsDataContract(writer, types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="types"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T SerializeAsXml(XmlWriter destination, params Type[] types)
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
    /// <param name="destination"></param>
    /// <param name="types"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T SerializeAsXml(TextWriter destination, params Type[] types)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(false);

      return instance.SerializeAsXml(writer, types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="types"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T SerializeAsXml(Stream destination, Encoding encoding = null, params Type[] types)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(encoding, false);

      return instance.SerializeAsXml(writer, types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="encoding"></param>
    /// <param name="types"></param>
    /// <returns>Back self-reference to the given <paramref name="instance"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
    public T SerializeAsXml(FileInfo destination, Encoding encoding = null, params Type[] types)
    {
      if (instance is null) throw new ArgumentNullException(nameof(instance));
      if (destination is null) throw new ArgumentNullException(nameof(destination));

      using var writer = destination.ToXmlWriter(encoding);

      return instance.SerializeAsXml(writer, types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns></returns>
    public IEnumerable<T> ToEnumerable(params T[] elements)
    {
      var collection = new List<T>(elements.Length + 1) {instance};

      collection.AddRange(elements);

      return collection;
    }

    /// <summary>
    ///   <para>Returns a generic string representation of object, using values of specified properties in a form of lambda expressions.</para>
    /// </summary>
    /// <param name="properties">Set of properties, whose values are used for string representation of <paramref name="instance"/>. Each property is represented as a lambda expression.</param>
    /// <returns>String representation of <paramref name="instance"/>. Property name is separated from value by colon character, name-value pairs are separated by comma and immediately following space characters, and all content is placed in square brackets afterwards.</returns>
    /// <seealso cref="ToStateString{T}(T, Expression{Func{T, object}}[])"/>
    public string ToStateString(IEnumerable<Expression<Func<T, object>>> properties = null)
    {
      if (instance is null)
      {
        return string.Empty;
      }

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
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <seealso cref="ToStateString{T}(T, IEnumerable{Expression{Func{T, object}}})"/>
    public string ToStateString(params Expression<Func<T, object>>[] properties) => instance.ToStateString(properties as IEnumerable<Expression<Func<T, object>>>);
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="function"></param>
    /// <param name="finalizer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="instance"/> or <paramref name="function"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryFinally{T}(T, Action{T}, Action{T})"/>
    public TResult TryFinally<TResult>(Func<T, TResult> function, Action<T> finalizer = null)
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
  }

  /// <param name="instance"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(T? instance) where T : struct
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    public bool IsUnset => instance is null || instance.IsEmpty;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    public bool IsEmpty => !instance.HasValue || instance.Value.ToString().IsUnset;
  }

  /// <param name="instance"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(Lazy<T> instance)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    public bool IsUnset => instance is null || instance.IsEmpty;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="instance"/> is <see langword="null"/>.</exception>
    public bool IsEmpty => instance is not null ? !instance.IsValueCreated || instance.Value is null || instance.Value.ToString().IsUnset : throw new ArgumentNullException(nameof(instance));
  }
}