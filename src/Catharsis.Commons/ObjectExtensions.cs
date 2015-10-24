using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for basic <see cref="object"/> type.</para>
  /// </summary>
  /// <seealso cref="object"/>
  public static class ObjectExtensions
  {
    /// <summary>
    ///   <para>Tries to convert given object to specified type and returns a <c>null</c> reference on failure.</para>
    /// </summary>
    /// <typeparam name="T">Type to convert object to.</typeparam>
    /// <param name="self">Object to convert.</param>
    /// <returns>Object, converted to the specified type, or a <c>null</c> reference if the conversion cannot be performed.</returns>
    /// <remarks>If specified object instance is a <c>null</c> reference, a <c>null</c> reference will be returned as a result.</remarks>
    /// <seealso cref="To{T}"/>
    public static T As<T>(this object self)
    {
      return self is T ? (T) self : default(T);
    }

    /// <summary>
    ///   <para>Calls specified delegate action in a context of target object and returns the result of the call. If target object implements <see cref="IDisposable"/> interface, invokes delegate method inside a <c>using</c> code block.</para>
    /// </summary>
    /// <typeparam name="SUBJECT">Type of target object.</typeparam>
    /// <typeparam name="OUTPUT">Type of output result for the <paramref name="action"/> delegate.</typeparam>
    /// <param name="self">Target object, in context of which <paramref name="action"/> method is to be called.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <returns>Value, returned by calling of <paramref name="action"/> delegate's method in a context of <paramref name="self"/> object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Do{T}"/>
    public static OUTPUT Do<SUBJECT, OUTPUT>(this SUBJECT self, Func<SUBJECT, OUTPUT> action)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(action);

      var result = default(OUTPUT);
      self.Do<SUBJECT>(it => result = action(it));
      return result;
    }

    /// <summary>
    ///   <para>Calls specified delegate action in a context of target object. If target object implements <see cref="IDisposable"/> interface, invokes delegate method inside a <c>using</c> code block.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="self">Target object, in context of which <paramref name="action"/> method is to be called.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <returns>Back reference to the current target object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Do{SUBJECT,OUTPUT}"/>
    /// <remarks>If <paramref name="self"/> does not implement <see cref="IDisposable"/> interface, this method simply calls <paramref name="action"/>'s method.</remarks>
    public static T Do<T>(this T self, Action<T> action)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(action);

      if (self is IDisposable)
      {
        using (self as IDisposable)
        {
          action(self);
        }
      }
      else
      {
        action(self);
      }

      return self;
    }

    /// <summary>
    ///   <para>Returns state (names and values of all public properties) for the given object.</para>
    /// </summary>
    /// <param name="self">Target object whose public properties names and values are to be returned.</param>
    /// <returns>State of <paramref name="self"/> as a string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>Property name is separated from property value by a colon, each name-value pairs are separated by comma characters.</remarks>
    /// <seealso cref="object.ToString()"/>
    public static string Dump(this object self)
    {
      Assertion.NotNull(self);

      return self.ToString(self.GetType().GetProperties().Select(property => property.Name).ToArray());
    }

    /// <summary>
    ///   <para>Returns the value of object's field with a specified name.</para>
    /// </summary>
    /// <param name="self">Object whose field's value is to be returned.</param>
    /// <param name="name">Name of field of <paramref name="self"/>'s type.</param>
    /// <returns>Value of <paramref name="self"/>'s field with a given <paramref name="name"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static object Field(this object self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.GetType().AnyField(name)?.GetValue(self);
    }

    /// <summary>
    ///   <para>Determines if the object is compatible with the given type, as specified by the <c>is</c> operator.</para>
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <param name="self">Object whose type compatibility with <typeparamref name="T"/> is to be determined.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is type-compatible with <typeparamref name="T"/>, <c>false</c> if not.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static bool Is<T>(this object self)
    {
      Assertion.NotNull(self);

      return self is T;
    }

    /// <summary>
    ///   <para>Returns the value of a member on a target object, using expression tree to specify type's member.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <typeparam name="MEMBER">Type of <paramref name="self"/>'s member.</typeparam>
    /// <param name="self">Target object, whose member's value is to be returned.</param>
    /// <param name="expression">Lambda expression that represents a member of <typeparamref name="T"/> type, whose value for <paramref name="self"/> instance is to be returned. Generally it should represents either a public property/field or no-arguments method.</param>
    /// <returns>Value of member of <typeparamref name="T"/> type on a <paramref name="self"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="expression"/> is a <c>null</c> reference.</exception>
    public static MEMBER Member<T, MEMBER>(this T self, Expression<Func<T, MEMBER>> expression)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(expression);

      return expression.Compile()(self);
    }

    /// <summary>
    ///   <para>Calls/invokes instance method on a target object, passing specified parameters.</para>
    /// </summary>
    /// <param name="self">The object on which to invoke the method.</param>
    /// <param name="name">Name of the method to be invoked.</param>
    /// <param name="parameters">Optional set of parameters to be passed to invoked method, if it requires some.</param>
    /// <returns>An object containing the return value of the invoked method.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="MethodInfo.Invoke(object, object[])"/>
    public static object Method(this object self, string name, params object[] parameters)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.GetType().AnyMethod(name)?.Invoke(self, parameters);
    }

    /// <summary>
    ///   <para>Sets values of several properties on specified target object.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="self">Target object whose properties are to be changed.</param>
    /// <param name="properties">Set of properties (as a collection of name - value pairs) to be set on <paramref name="self"/>.</param>
    /// <returns>Back reference to the current target object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Property(object, string)"/>
    /// <seealso cref="Property{T}(T, string, object)"/>
    /// <seealso cref="Properties{T}(T, object)"/>
    public static T Properties<T>(this T self, IDictionary<string, object> properties)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(properties);

      properties.Each(property => self.Property(property.Key, property.Value));
      return self;
    }

    /// <summary>
    ///   <para>Sets values of several properties on specified target object.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="self">Target object whose properties are to be changed.</param>
    /// <param name="properties">Object whose public properties are to be used for setting matched ones on target object.</param>
    /// <returns>Back reference to the current target object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Property(object, string)"/>
    /// <seealso cref="Property{T}(T, string, object)"/>
    /// <seealso cref="Properties{T}(T, IDictionary{string, object})"/>
    public static T Properties<T>(this T self, object properties)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(properties);

      properties.GetType().GetProperties().Each(property => self.Property(property.Name, properties.Property(property.Name)));
      return self;
    }

    /// <summary>
    ///   <para>Creates and returns a dictionary from the values of public properties of target object.</para>
    /// </summary>
    /// <param name="self">Target object whose properties values are returned.</param>
    /// <returns>Dictionary of name - value pairs for public properties of <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static IDictionary<string, object> PropertiesMap(this object self)
    {
      Assertion.NotNull(self);

      return self.GetType().GetProperties().ToDictionary(property => property.Name, property => property.GetValue(self, null));
    }

    /// <summary>
    ///   <para>Returns the value of given property for specified target object.</para>
    /// </summary>
    /// <param name="self">Target object, whose property's value is to be returned.</param>
    /// <param name="name">Name of property to inspect.</param>
    /// <returns>Value of property <paramref name="name"/> for <paramref name="self"/> instance, or a <c>null</c> reference in case this property does not exists for <paramref name="self"/>'s type.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="Property{T}(T, string, object)"/>
    /// <seealso cref="Properties{T}(T, IDictionary{string, object})"/>
    /// <seealso cref="Properties{T}(T, object)"/>
    public static object Property(this object self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.GetType().AnyProperty(name)?.GetValue(self, null);
    }

    /// <summary>
    ///   <para>Sets the value of given property on specified target object.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="self">Target object whose property is to be changed.</param>
    /// <param name="property">Name of property to change.</param>
    /// <param name="value">New value of object's property.</param>
    /// <returns>Back reference to the current target object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="property"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="property"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="Property(object, string)"/>
    /// <seealso cref="Properties{T}(T, IDictionary{string, object})"/>
    /// <seealso cref="Properties{T}(T, object)"/>
    public static T Property<T>(this T self, string property, object value)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(property);

      self.GetType().AnyProperty(property)?.SetValue(self, value, null);
      return self;
    }

    /// <summary>
    ///   <para>Tries to convert given object to specified type and throws exception on failure.</para>
    /// </summary>
    /// <typeparam name="T">Type to convert object to.</typeparam>
    /// <param name="self">Object to convert.</param>
    /// <returns>Object, converted to the specified type.</returns>
    /// <exception cref="InvalidCastException">If conversion to specified type cannot be performed.</exception>
    /// <remarks>If specified object instance is a <c>null</c> reference, a <c>null</c> reference will be returned as a result.</remarks>
    /// <seealso cref="As{T}"/>
    public static T To<T>(this object self)
    {
      return (T) self;
    }

    /// <summary>
    ///   <para>Returns a generic string representation of object, using values of specified properties.</para>
    /// </summary>
    /// <param name="self">Object to be converted to string representation.</param>
    /// <param name="properties">Set of properties, whose values are used for string representation of <paramref name="self"/>.</param>
    /// <returns>String representation of <paramref name="self"/>. Property name is separated from value by colon character, name-value pairs are separated by comma and immediately following space characters, and all content is placed in square brackets afterwards.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="object.ToString()"/>
    /// <seealso cref="ToString{T}(T, Expression{Func{T, object}}[])"/>
    public static string ToString(this object self, IEnumerable<string> properties)
    {
      Assertion.NotNull(self);

      const string Separator = ", ";
      var sb = new StringBuilder();
      properties?.Where(property => self.GetType().HasProperty(property)).Each(property => sb.Append($"{property}:\"{self.Property(property)}\"{Separator}"));
      if (sb.Length > 0)
      {
        sb.Remove(sb.Length - Separator.Length, Separator.Length);
      }
      return $"[{sb}]";
    }

    /// <summary>
    ///   <para>Returns a generic string representation of object, using values of specified properties in a form of lambda expressions.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="self">Object to be converted to string representation.</param>
    /// <param name="properties">Set of properties, whose values are used for string representation of <paramref name="self"/>. Each property is represented as a lambda expression.</param>
    /// <returns>String representation of <paramref name="self"/>. Property name is separated from value by colon character, name-value pairs are separated by comma and immediately following space characters, and all content is placed in square brackets afterwards.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/>If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="object.ToString()"/>
    /// <seealso cref="ToString(object, IEnumerable{string})"/>
    public static string ToString<T>(this T self, params Expression<Func<T, object>>[] properties)
    {
      Assertion.NotNull(self);

      const string Separator = ", ";
      var sb = new StringBuilder();
      properties?.Each(property => sb.Append($"{property.Body.To<UnaryExpression>().Operand.To<MemberExpression>().Member.Name}:\"{property.Compile()(self)}\"{Separator}"));
      if (sb.Length > 0)
      {
        sb.Remove(sb.Length - Separator.Length, Separator.Length);
      }
      return $"[{sb}]";
    }

    /// <summary>
    ///   <para>Returns a string representation of object, formatted according to <see cref="CultureInfo.InvariantCulture"/>.</para>
    /// </summary>
    /// <param name="self">Object to be converted to string representation.</param>
    /// <returns>String representation of <seealso cref="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="object.ToString()"/>
    public static string ToStringInvariant(this object self)
    {
      Assertion.NotNull(self);

      return string.Format(CultureInfo.InvariantCulture, "{0}", self);
    }

#if NET_35
    /// <summary>
    ///   <para>Determines whether specified objects are considered equal by comparing values of the given set of properties/fields on each of them.</para>
    ///   <para>The following algorithm is used in equality determination:
    ///     <list type="bullet">
    ///       <item><description>If both <paramref name="self"/> and <paramref name="other"/> are <c>null</c> references, method returns <c>true</c>.</description></item>
    ///       <item><description>If one of compared objects is <c>null</c> and another is not, method returns <c>false</c>.</description></item>
    ///       <item><description>If both objects references are equal (they represent the same object instance), method returns <c>true</c>.</description></item>
    ///       <item><description>If <paramref name="propertiesOrFields"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for equality comparison.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type does not contain any properties/fields in <paramref name="propertiesOrFields"/> set, <see cref="object.Equals(object, object)"/> method is used for equality comparison.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties/fields in <paramref name="propertiesOrFields"/> set, their values are used for equality comparison according to <see cref="object.Equals(object)"/> method of both <paramref name="self"/> and <paramref name="other"/> instances.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <typeparam name="T">Type of objects to compare.</typeparam>
    /// <param name="self">Current object to compare with the second.</param>
    /// <param name="other">Second object to compare with the current one.</param>
    /// <param name="propertiesOrFields">Set of properties/fields whose values are used in equality comparison.</param>
    /// <returns><c>true</c> if <paramref name="self"/> and <paramref name="other"/> are considered equal, <c>false</c> otherwise.</returns>
    /// <seealso cref="object.Equals(object)"/>
    /// <seealso cref="Equality{T}(T, T, Expression{Func{T, object}}[])"/>
    public static bool Equality<T>(this T self, T other, params string[] propertiesOrFields)
    {
      if (self == null && other == null)
      {
        return true;
      }

      if (self == null || other == null)
      {
        return false;
      }

      if (ReferenceEquals(self, other))
      {
        return true;
      }

      if (propertiesOrFields == null || !propertiesOrFields.Any())
      {
        var metaPropertiesOrFields = self.GetType().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaPropertiesOrFields.Length == 0 ? self.Equals(other) : self.Equality(other, metaPropertiesOrFields);
      }

      var type = self.GetType();
      var typeProperties = propertiesOrFields.Select(property => type.AnyProperty(property)).Where(property => property != null);
      var typeFields = propertiesOrFields.Select(field => type.AnyField(field)).Where(field => field != null);

      if (!typeProperties.Any() && !typeFields.Any())
      {
        return Equals(self, other);
      }

      return
        typeProperties.All(property =>
        {
          var first = property.GetValue(self, null);
          object second = null;
          try
          {
            second = property.GetValue(other, null);
          }
          catch
          {
          }

          return Equals(first, second);
        })
        && typeFields.All(field =>
        {
          var first = field.GetValue(self);
          object second = null;
          try
          {
            second = field.GetValue(other);
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
    ///       <item><description>If both <paramref name="self"/> and <paramref name="other"/> are <c>null</c> references, method returns <c>true</c>.</description></item>
    ///       <item><description>If one of compared objects is <c>null</c> and another is not, method returns <c>false</c>.</description></item>
    ///       <item><description>If both objects references are equal (they represent the same object instance), method returns <c>true</c>.</description></item>
    ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for equality comparison.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type does not contain any properties in <paramref name="properties"/> set, <see cref="object.Equals(object, object)"/> method is used for equality comparison.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for equality comparison according to <see cref="object.Equals(object)"/> method of both <paramref name="self"/> and <paramref name="other"/> instances.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <typeparam name="T">Type of objects to compare.</typeparam>
    /// <param name="self">Current object to compare with the second.</param>
    /// <param name="other">Second object to compare with the current one.</param>
    /// <param name="properties">Set of properties in a form of expression trees, whose values are used in equality comparison.</param>
    /// <returns><c>true</c> if <paramref name="self"/> and <paramref name="other"/> are considered equal, <c>false</c> otherwise.</returns>
    /// <seealso cref="object.Equals(object)"/>
    /// <seealso cref="Equality{T}(T, T, string[])"/>
    public static bool Equality<T>(this T self, T other, params Expression<Func<T, object>>[] properties)
    {
      if (self == null && other == null)
      {
        return true;
      }

      if (self == null || other == null)
      {
        return false;
      }

      if (ReferenceEquals(self, other))
      {
        return true;
      }

      if (properties == null || properties.Length == 0)
      {
        var metaProperties = self.GetType().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? self.Equals(other) : self.Equality(other, metaProperties);
      }

      return properties.Select(property => property.Compile()).All(property => Equals(property(self), property(other)));
    }

        /// <summary>
    ///   <para>Returns a hash value of a given object, using specified set of properties in its calculation.</para>
    ///   <para>The following algorithm is used in hash code calculation:
    ///     <list type="bullet">
    ///       <item><description>If <paramref name="self"/> is a <c>null</c> reference, methods returns 0.</description></item>
    ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for hash code calculation.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="self"/>'s properties hash codes is returned in that case.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <param name="self">Target object, whose hash code is to be returned.</param>
    /// <param name="properties">Collection of properties names, whose values are to be used in hash code's calculation.</param>
    /// <returns>Hash code for <paramref name="self"/>.</returns>
    /// <seealso cref="object.GetHashCode()"/>
    /// <seealso cref="GetHashCode{T}(T, Expression{Func{T, object}}[])"/>
    public static int GetHashCode<T>(this T self, IEnumerable<string> properties)
    {
      if (self == null)
      {
        return 0;
      }

      if (properties == null || !properties.Any())
      {
        var metaProperties = self.GetType().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? self.GetHashCode() : self.GetHashCode(metaProperties);
      }

      var hash = 0;
      properties.Select(property => self.GetType().AnyProperty(property)).Where(property => property != null).Select(property => property.GetValue(self, null)).Where(value => value != null).Each(value => hash += value.GetHashCode());
      return hash;
    }

    /// <summary>
    ///   <para>Returns a hash value of a given object, using specified set of properties, represented as experssion trees, in its calculation.</para>
    ///   <para>The following algorithm is used in hash code calculation:
    ///     <list type="bullet">
    ///       <item><description>If <paramref name="self"/> is a <c>null</c> reference, methods returns 0.</description></item>
    ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for hash code calculation.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="self"/>'s properties hash codes is returned in that case.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <param name="self">Target object, whose hash code is to be returned.</param>
    /// <param name="properties">Collection of properties in a form of expression trees, whose values are to be used in hash code's calculation.</param>
    /// <returns>Hash code for <paramref name="self"/>.</returns>
    /// <seealso cref="object.GetHashCode()"/>
    /// <seealso cref="GetHashCode{T}(T, IEnumerable{string})"/>
    public static int GetHashCode<T>(this T self, params Expression<Func<T, object>>[] properties)
    {
      if (self == null)
      {
        return 0;
      }

      if (properties == null || properties.Length == 0)
      {
        var metaProperties = typeof(T).Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? self.GetHashCode() : self.GetHashCode(metaProperties);
      }

      var hash = 0;
      properties.Select(property => property.Compile()(self)).Where(value => value != null).Each(value => hash += value.GetHashCode());
      return hash;
    }
#endif
  }
}