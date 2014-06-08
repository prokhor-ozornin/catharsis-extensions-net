using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="object"/>.</para>
  /// </summary>
  /// <seealso cref="object"/>
  public static class ObjectExtensions
  {
    /// <summary>
    ///   <para>Tries to convert given object to specified type and returns a <c>null</c> reference on failure.</para>
    /// </summary>
    /// <typeparam name="T">Type to convert object to.</typeparam>
    /// <param name="subject">Object to convert.</param>
    /// <returns>Object, converted to the specified type, or a <c>null</c> reference if the conversion cannot be performed.</returns>
    /// <remarks>If specified object instance is a <c>null</c> reference, a <c>null</c> reference will be returned as a result.</remarks>
    /// <seealso cref="To{T}"/>
    public static T As<T>(this object subject)
    {
      return subject is T ? (T) subject : default(T);
    }

    /// <summary>
    ///   <para>Returns state (names and values of all public properties) for the given object.</para>
    /// </summary>
    /// <param name="subject">Target object whose public properties names and values are to be returned.</param>
    /// <returns>State of <paramref name="subject"/> as a string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    /// <remarks>Property name is separated from property value by a colon, each name-value pairs are separated by comma characters.</remarks>
    /// <seealso cref="object.ToString()"/>
    public static string Dump(this object subject)
    {
      Assertion.NotNull(subject);

      return subject.ToString(subject.GetType().GetProperties().Select(property => property.Name).ToArray());
    }

    /// <summary>
    ///   <para>Determines whether specified objects are considered equal by comparing values of the given set of properties on each of them.</para>
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
    /// <param name="properties">Set of properties whose values are used in equality comparison.</param>
    /// <returns><c>true</c> if <paramref name="self"/> and <paramref name="other"/> are considered equal, <c>false</c> otherwise.</returns>
    /// <seealso cref="object.Equals(object)"/>
    /// <seealso cref="Equality{T}(T, T, Expression{Func{T, object}}[])"/>
    public static bool Equality<T>(this T self, T other, params string[] properties)
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

      if (properties == null || !properties.Any())
      {
        var metaProperties = self.GetType().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? self.Equals(other) : self.Equality(other, metaProperties);
      }

      var subjectProperties = properties.Select(property => self.GetType().AnyProperty(property)).Where(property => property != null);
      if (!subjectProperties.Any())
      {
        return Equals(self, other);
      }

      return subjectProperties.All(property =>
      {
        var first = property.GetValue(self, null);
        object second = null;
        try
        {
          second = property.GetValue(other, null);
        }
        catch (TargetException)
        {
        }
        
        if (first == null && second == null)
        {
          return true;
        }

        if (first == null || second == null)
        {
          return false;
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
    ///   <para>Returns the value of object's field with a specified name.</para>
    /// </summary>
    /// <param name="subject">Object whose field's value is to be returned.</param>
    /// <param name="name">Name of field of <paramref name="subject"/>'s type.</param>
    /// <returns>Value of <paramref name="subject"/>'s field with a given <paramref name="name"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static object Field(this object subject, string name)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      var subjectField = subject.GetType().AnyField(name);
      return subjectField != null ? subjectField.GetValue(subject) : null;
    }

    /// <summary>
    ///   <para>Returns a hash value of a given object, using specified set of properties in its calculation.</para>
    ///   <para>The following algorithm is used in hash code calculation:
    ///     <list type="bullet">
    ///       <item><description>If <paramref name="subject"/> is a <c>null</c> reference, methods returns 0.</description></item>
    ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for hash code calculation.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="subject"/>'s properties hash codes is returned in that case.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <param name="subject">Target object, whose hash code is to be returned.</param>
    /// <param name="properties">Collection of properties names, whose values are to be used in hash code's calculation.</param>
    /// <returns>Hash code for <paramref name="subject"/>.</returns>
    /// <seealso cref="object.GetHashCode()"/>
    /// <seealso cref="GetHashCode{T}(T, Expression{Func{T, object}}[])"/>
    public static int GetHashCode<T>(this T subject, IEnumerable<string> properties)
    {
      if (subject == null)
      {
        return 0;
      }

      if (properties == null || !properties.Any())
      {
        var metaProperties = subject.GetType().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? subject.GetHashCode() : subject.GetHashCode(metaProperties);
      }

      var hash = 0;
      properties.Select(property => subject.GetType().AnyProperty(property)).Where(property => property != null).Select(property => property.GetValue(subject, null)).Where(value => value != null).Each(value => hash += value.GetHashCode());
      return hash;
    }

    /// <summary>
    ///   <para>Returns a hash value of a given object, using specified set of properties, represented as experssion trees, in its calculation.</para>
    ///   <para>The following algorithm is used in hash code calculation:
    ///     <list type="bullet">
    ///       <item><description>If <paramref name="subject"/> is a <c>null</c> reference, methods returns 0.</description></item>
    ///       <item><description>If <paramref name="properties"/> set is either a <c>null</c> reference or contains zero elements, <see cref="EqualsAndHashCodeAttribute"/> attribute is used for hash code calculation.</description></item>
    ///       <item><description>If <typeparamref name="T"/> type contains any of the properties in <paramref name="properties"/> set, their values are used for hash code calculation according to <see cref="object.GetHashCode()"/> method. The sum of <paramref name="subject"/>'s properties hash codes is returned in that case.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <param name="subject">Target object, whose hash code is to be returned.</param>
    /// <param name="properties">Collection of properties in a form of expression trees, whose values are to be used in hash code's calculation.</param>
    /// <returns>Hash code for <paramref name="subject"/>.</returns>
    /// <seealso cref="object.GetHashCode()"/>
    /// <seealso cref="GetHashCode{T}(T, IEnumerable{string})"/>
    public static int GetHashCode<T>(this T subject, params Expression<Func<T, object>>[] properties)
    {
      if (subject == null)
      {
        return 0;
      }

      if (properties == null || properties.Length == 0)
      {
        var metaProperties = typeof(T).Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? subject.GetHashCode() : subject.GetHashCode(metaProperties);
      }

      var hash = 0;
      properties.Select(property => property.Compile()(subject)).Where(value => value != null).Each(value => hash += value.GetHashCode());
      return hash;
    }
    
    /// <summary>
    ///   <para>Determines if the object is compatible with the given type, as specified by the <c>is</c> operator.</para>
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    /// <param name="subject">Object whose type compatibility with <typeparamref name="T"/> is to be determined.</param>
    /// <returns><c>true</c> if <paramref name="subject"/> is type-compatible with <typeparamref name="T"/>, <c>false</c> if not.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static bool Is<T>(this object subject)
    {
      Assertion.NotNull(subject);

      return subject is T;
    }

    /// <summary>
    ///   <para>Determines whether the object can be considered as a non-strict <c>false</c> boolean value.</para>
    ///   <para>The following algorithm is used to determine whether an object can be considered non-strictly as a <c>false</c> value:
    ///     <list type="bullet">
    ///       <item><description>It represents a <c>null</c> reference.</description></item>
    ///       <item><description>It represents a <c>false</c> value of <c>bool</c> type.</description></item>
    ///       <item><description>It represents a non-positive value of numeric type.</description></item>
    ///       <item><description>It represents an <see cref="string.Empty"/> string.</description></item>
    ///       <item><description>It represents an empty <see cref="IEnumerable"/> collection.</description></item>
    ///       <item><description>It represents a zero-length <see cref="FileInfo"/> object.</description></item>
    ///       <item><description>It represents a regular expression <see cref="Match"/> object without matches.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <param name="subject">Target object for non-strict boolean evaluation.</param>
    /// <returns><c>true</c> if <paramref name="subject"/> can be considered as a non-strict <c>false</c> other, <c>false</c> otherwise.</returns>
    /// <seealso cref="IsTrue(object)"/>
    public static bool IsFalse(this object subject)
    {
      return !IsTrue(subject);
    }

    /// <summary>
    ///   <para>Determines whether the object can be considered as a non-strict <c>true</c> boolean value.</para>
    ///   <para>The following algorithm is used to determine whether an object can be considered non-strictly as a <c>true</c> value:
    ///     <list type="bullet">
    ///       <item><description>It represents a <c>true</c> value of <c>bool</c> type.</description></item>
    ///       <item><description>It represents a positive value of numeric type.</description></item>
    ///       <item><description>It represents a non-empty string.</description></item>
    ///       <item><description>It represents a non-empty <see cref="IEnumerable"/> collection.</description></item>
    ///       <item><description>It represents a <see cref="FileInfo"/> object which does not represent an empty file.</description></item>
    ///       <item><description>It represents a <see cref="Stream"/> object which is not empty.</description></item>
    ///       <item><description>It represents a regular expression <see cref="Match"/> object with at least one match.</description></item>
    ///       <item><description>It represents any other type of object which is not a <c>null</c> reference.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <param name="subject">Target object for non-strict boolean evaluation.</param>
    /// <returns><c>true</c> if <paramref name="subject"/> can be considered as a non-strict <c>true</c> other, <c>false</c> otherwise.</returns>
    /// <seealso cref="IsFalse(object)"/>
    public static bool IsTrue(this object subject)
    {
      if (subject == null)
      {
        return false;
      }

      switch (Type.GetTypeCode(subject.GetType()))
      {
        case TypeCode.Boolean:
          return ((bool)subject);

        case TypeCode.Byte:
          return ((byte)subject) > 0;

        case TypeCode.Char:
          return ((char)subject) != Char.MinValue;

        case TypeCode.Decimal:
          return ((decimal)subject) > 0;

        case TypeCode.Double:
          return ((double)subject) > 0;

        case TypeCode.Int16:
          return ((short)subject) > 0;

        case TypeCode.Int32:
          return ((int)subject) > 0;

        case TypeCode.Int64:
          return ((long)subject) > 0;

        case TypeCode.SByte:
          return ((sbyte)subject) > 0;

        case TypeCode.Single:
          return ((Single)subject) > 0;

        case TypeCode.String:
          return ((string)subject).Length > 0;

        case TypeCode.UInt16:
          return ((ushort)subject) > 0;

        case TypeCode.UInt32:
          return ((uint)subject) > 0;

        case TypeCode.UInt64:
          return ((ulong)subject) > 0;
      }

      if (subject is IEnumerable)
      {
        return subject.To<IEnumerable>().Cast<object>().Any();
      }

      if (subject is FileInfo)
      {
        return subject.To<FileInfo>().Length > 0;
      }

      if (subject is Stream)
      {
        return subject.To<Stream>().Length > 0;
      }

      if (subject is Match)
      {
        return subject.To<Match>().Success;
      }

      return true;
    }

    /// <summary>
    ///   <para>Determines whether specified object represents a primitive numeric value type.</para>
    /// </summary>
    /// <param name="subject">Object to be evaluated.</param>
    /// <returns><c>true</c> if <paramref name="subject"/> represents any numeric <see cref="ValueType"/> (<c>byte</c>, <c>decimal</c>, <c>double</c>, <c>integer</c>, <c>single</c>), <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Type.GetTypeCode(Type)"/>
    public static bool IsNumeric(this object subject)
    {
      Assertion.NotNull(subject);

      switch (Type.GetTypeCode(subject.GetType()))
      {
        case TypeCode.Byte:
        case TypeCode.Decimal:
        case TypeCode.Double:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.SByte:
        case TypeCode.Single:
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          return true;
      }

      return false;
    }

    /// <summary>
    ///   <para>Returns the value of a member on a target object, using expression tree to specify type's member.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <typeparam name="MEMBER">Type of <paramref name="subject"/>'s member.</typeparam>
    /// <param name="subject">Target object, whose member's value is to be returned.</param>
    /// <param name="expression">Lambda expression that represents a member of <typeparamref name="T"/> type, whose value for <paramref name="subject"/> instance is to be returned. Generally it should represents either a public property/field or no-arguments method.</param>
    /// <returns>Value of member of <typeparamref name="T"/> type on a <paramref name="subject"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="expression"/> is a <c>null</c> reference.</exception>
    public static MEMBER Member<T, MEMBER>(this T subject, Expression<Func<T, MEMBER>> expression)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(expression);

      return expression.Compile()(subject);
    }

    /// <summary>
    ///   <para>Calls/invokes instance method on a target object, passing specified parameters.</para>
    /// </summary>
    /// <param name="subject">The object on which to invoke the method.</param>
    /// <param name="name">Name of the method to be invoked.</param>
    /// <param name="parameters">Optional set of parameters to be passed to invoked method, if it requires some.</param>
    /// <returns>An object containing the return value of the invoked method.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="MethodInfo.Invoke(object, object[])"/>
    public static object Method(this object subject, string name, params object[] parameters)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      var subjectMethod = subject.GetType().AnyMethod(name);
      return subjectMethod != null ? subjectMethod.Invoke(subject, parameters) : null;
    }

    /// <summary>
    ///   <para>Returns the value of given property for specified target object.</para>
    /// </summary>
    /// <param name="subject">Target object, whose property's value is to be returned.</param>
    /// <param name="name">Name of property to inspect.</param>
    /// <returns>Value of property <paramref name="name"/> for <paramref name="subject"/> instance, or a <c>null</c> reference in case this property does not exists for <paramref name="subject"/>'s type.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="Property{T}(T, string, object)"/>
    /// <seealso cref="Properties{T}(T, IEnumerable{KeyValuePair{string, object}})"/>
    /// <seealso cref="Properties{T}(T, object)"/>
    public static object Property(this object subject, string name)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      var subjectProperty = subject.GetType().AnyProperty(name);
      return subjectProperty != null ? subjectProperty.GetValue(subject, null) : null;
    }

    /// <summary>
    ///   <para>Sets the value of given property on specified target object.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="subject">Target object whose property is to be changed.</param>
    /// <param name="property">Name of property to change.</param>
    /// <param name="value">New value of object's property.</param>
    /// <returns>Back reference to the current target object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="property"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="property"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="Property(object, string)"/>
    /// <seealso cref="Properties{T}(T, IEnumerable{KeyValuePair{string, object}})"/>
    /// <seealso cref="Properties{T}(T, object)"/>
    public static T Property<T>(this T subject, string property, object value)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(property);

      var subjectProperty = subject.GetType().AnyProperty(property);
      if (subjectProperty != null)
      {
        subjectProperty.SetValue(subject, value, null);
      }
      return subject;
    }

    /// <summary>
    ///   <para>Sets values of several properties on specified target object.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="subject">Target object whose properties are to be changed.</param>
    /// <param name="properties">Set of properties (as a collection of name - value pairs) to be set on <paramref name="subject"/>.</param>
    /// <returns>Back reference to the current target object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Property(object, string)"/>
    /// <seealso cref="Property{T}(T, string, object)"/>
    /// <seealso cref="Properties{T}(T, object)"/>
    public static T Properties<T>(this T subject, IEnumerable<KeyValuePair<string, object>> properties)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(properties);

      properties.Each(property => subject.Property(property.Key, property.Value));
      return subject;
    }

    /// <summary>
    ///   <para>Sets values of several properties on specified target object.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="subject">Target object whose properties are to be changed.</param>
    /// <param name="properties">Object whose public properties are to be used for setting matched ones on target object.</param>
    /// <returns>Back reference to the current target object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Property(object, string)"/>
    /// <seealso cref="Property{T}(T, string, object)"/>
    /// <seealso cref="Properties{T}(T, IEnumerable{KeyValuePair{string, object}})"/>
    public static T Properties<T>(this T subject, object properties)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(properties);

      properties.GetType().GetProperties().Each(property => subject.Property(property.Name, properties.Property(property.Name)));
      return subject;
    }

    /// <summary>
    ///   <para>Creates and returns a dictionary from the values of public properties of target object.</para>
    /// </summary>
    /// <param name="subject">Target object whose properties values are returned.</param>
    /// <returns>Dictionary of name - value pairs for public properties of <paramref name="subject"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static IDictionary<string, object> PropertiesMap(this object subject)
    {
      Assertion.NotNull(subject);

      return subject.GetType().GetProperties().ToDictionary(property => property.Name, property => property.GetValue(subject, null));
    }

    /// <summary>
    ///   <para>Tries to convert given object to specified type and throws exception on failure.</para>
    /// </summary>
    /// <typeparam name="T">Type to convert object to.</typeparam>
    /// <param name="subject">Object to convert.</param>
    /// <returns>Object, converted to the specified type.</returns>
    /// <exception cref="InvalidCastException">If conversion to specified type cannot be performed.</exception>
    /// <remarks>If specified object instance is a <c>null</c> reference, a <c>null</c> reference will be returned as a result.</remarks>
    /// <seealso cref="As{T}"/>
    public static T To<T>(this object subject)
    {
      return (T) subject;
    }

    /// <summary>
    ///   <para>Returns a generic string representation of object, using values of specified properties.</para>
    /// </summary>
    /// <param name="subject">Object to be converted to string representation.</param>
    /// <param name="properties">Set of properties, whose values are used for string representation of <paramref name="subject"/>.</param>
    /// <returns>String representation of <paramref name="subject"/>. Property name is separated from value by colon character, name-value pairs are separated by comma and immediately following space characters, and all content is placed in square brackets afterwards.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="object.ToString()"/>
    /// <seealso cref="ToString{T}(T, Expression{Func{T, object}}[])"/>
    public static string ToString(this object subject, IEnumerable<string> properties)
    {
      Assertion.NotNull(subject);

      const string Separator = ", ";
      var sb = new StringBuilder();
      if (properties != null)
      {
        properties.Where(property => subject.GetType().HasProperty(property)).Each(property => sb.AppendFormat("{0}:\"{1}\"{2}", property, subject.Property(property), Separator));
      }
      if (sb.Length > 0)
      {
        sb.Remove(sb.Length - Separator.Length, Separator.Length);
      }
      return "[{0}]".FormatSelf(sb.ToString());
    }

    /// <summary>
    ///   <para>Returns a generic string representation of object, using values of specified properties in a form of lambda expressions.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="subject">Object to be converted to string representation.</param>
    /// <param name="properties">Set of properties, whose values are used for string representation of <paramref name="subject"/>. Each property is represented as a lambda expression.</param>
    /// <returns>String representation of <paramref name="subject"/>. Property name is separated from value by colon character, name-value pairs are separated by comma and immediately following space characters, and all content is placed in square brackets afterwards.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/>If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="object.ToString()"/>
    /// <seealso cref="ToString(object, IEnumerable{string})"/>
    public static string ToString<T>(this T subject, params Expression<Func<T, object>>[] properties)
    {
      Assertion.NotNull(subject);

      const string Separator = ", ";
      var sb = new StringBuilder();
      if (properties != null)
      {
        properties.Each(property => sb.AppendFormat("{0}:\"{1}\"{2}", property.Body.To<UnaryExpression>().Operand.To<MemberExpression>().Member.Name, property.Compile()(subject), Separator));
      }
      if (sb.Length > 0)
      {
        sb.Remove(sb.Length - Separator.Length, Separator.Length);
      }
      return "[{0}]".FormatSelf(sb.ToString());
    }

    /// <summary>
    ///   <para>Returns a string representation of object, formatted according to <see cref="CultureInfo.InvariantCulture"/>.</para>
    /// </summary>
    /// <param name="subject">Object to be converted to string representation.</param>
    /// <returns>String representation of <seealso cref="subject"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="object.ToString()"/>
    public static string ToStringInvariant(this object subject)
    {
      Assertion.NotNull(subject);

      return string.Format(CultureInfo.InvariantCulture, "{0}", subject);
    }

    /// <summary>
    ///   <para>Calls specified delegate action in a context of target object. If target object implements <see cref="IDisposable"/> interface, invokes delegate method inside a <c>using</c> code block.</para>
    /// </summary>
    /// <typeparam name="T">Type of target object.</typeparam>
    /// <param name="subject">Target object, in context of which <paramref name="action"/> method is to be called.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <returns>Back reference to the current target object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="With{SUBJECT, OUTPUT}(SUBJECT, Func{SUBJECT, OUTPUT})"/>
    /// <remarks>If <paramref name="subject"/> does not implement <see cref="IDisposable"/> interface, this method simply calls <paramref name="action"/>'s method.</remarks>
    public static T With<T>(this T subject, Action<T> action)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(action);

      if (subject is IDisposable)
      {
        using (subject as IDisposable)
        {
          action(subject);
        }
      }
      else
      {
        action(subject);
      }

      return subject;
    }

    /// <summary>
    ///   <para>Calls specified delegate action in a context of target object and returns the result of the call. If target object implements <see cref="IDisposable"/> interface, invokes delegate method inside a <c>using</c> code block.</para>
    /// </summary>
    /// <typeparam name="SUBJECT">Type of target object.</typeparam>
    /// <typeparam name="OUTPUT">Type of output result for the <paramref name="action"/> delegate.</typeparam>
    /// <param name="subject">Target object, in context of which <paramref name="action"/> method is to be called.</param>
    /// <param name="action">Delegate that represents a method to be called.</param>
    /// <returns>Value, returned by calling of <paramref name="action"/> delegate's method in a context of <paramref name="subject"/> object.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="With{T}(T, Action{T})"/>
    public static OUTPUT With<SUBJECT, OUTPUT>(this SUBJECT subject, Func<SUBJECT, OUTPUT> action)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(action);

      var result = default(OUTPUT);
      subject.With<SUBJECT>(x => result = action(x));
      return result;
    }
  }
}