using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of reflection-related extensions methods, based on <see cref="TypeInfo"/> type.</para>
  /// </summary>
  public static class ReflectionTypeInfoExtensions
  {
    /// <summary>
    ///   <para>Determines whether a <see cref="Type"/> implements specified interface.</para>
    /// </summary>
    /// <param name="self">The type to evaluate.</param>
    /// <param name="interfaceType">Interface that must be implemented by <paramref name="self"/>.</param>
    /// <returns><c>true</c> if <paramref name="self"/> implements interface of type <paramref name="interfaceType"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="interfaceType"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="interfaceType"/> does not represent interface.</exception>
    /// <seealso cref="Type.GetInterface(string, bool)"/>
    /// <seealso cref="Implements{INTERFACE}(Type)"/>
    public static bool Implements(this Type self, Type interfaceType)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(interfaceType);

      if (!interfaceType.GetTypeInfo().IsInterface)
      {
        throw new ArgumentException($"Type {interfaceType} does not represent interface");
      }

      return self.GetInterfaces().Contains(interfaceType);
    }

    /// <summary>
    ///   <para>Determines whether a <see cref="Type"/> implements specified interface.</para>
    /// </summary>
    /// <typeparam name="T">Interface that must be implemented by <paramref name="self"/>.</typeparam>
    /// <param name="self">The type to evaluate.</param>
    /// <returns><c>true</c> if <paramref name="self"/> implements interface of type <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <typeparamref name="T"/> type does not represent interface.</exception>
    /// <seealso cref="Type.GetInterface(string, bool)"/>
    /// <seealso cref="Implements(Type, Type)"/>
    public static bool Implements<T>(this Type self)
    {
      Assertion.NotNull(self);

      return self.Implements(typeof(T));
    }

    /// <summary>
    ///   <para>Returns enumerator to iterate over the set of specified <see cref="Type"/>'s base types and implemented interfaces.</para>
    /// </summary>
    /// <param name="self">Type, whose ancestors (base types up the inheritance hierarchy) and implemented interfaces are returned.</param>
    /// <returns>Enumerator to iterate through <paramref name="self"/>'s base types and interfaces, which it implements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>The order of the base types and interfaces returned is undetermined.</remarks>
    /// <seealso cref="Type.BaseType"/>
    /// <seealso cref="Type.GetInterfaces()"/>
    public static IEnumerable<Type> Inherits(this Type self)
    {
      Assertion.NotNull(self);

      var types = new List<Type>();

      var currentType = self;
      var currentTypeInfo = currentType.GetTypeInfo();
      while (currentTypeInfo.BaseType != null)
      {
        types.Add(currentType);
        currentType = currentTypeInfo.BaseType;
      }
      types.Add(self.GetInterfaces());

      return types;
    }

    /// <summary>
    ///   <para>Determines whether target <see cref="Type"/> is anonymous one.</para>
    /// </summary>
    /// <param name="self">Subject type.</param>
    /// <returns><c>true</c> if <paramref name="self"/> represents an anonymous <see cref="Type"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static bool IsAnonymous(this Type self)
    {
      Assertion.NotNull(self);

      TypeInfo type = self.GetTypeInfo();
      return type.IsClass
             && type.Attribute<CompilerGeneratedAttribute>() != null
             && self.Name.ToLower().Contains("anonymous")
             && type.IsSealed
             && type.BaseType == typeof(object)
             && self.Properties().All(property => property.IsPublic());
    }

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
        var metaPropertiesOrFields = self.GetType().GetTypeInfo().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
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
        var metaProperties = self.GetType().GetTypeInfo().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
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
        var metaProperties = self.GetType().GetTypeInfo().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
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
        var metaProperties = typeof(T).GetTypeInfo().Attributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? self.GetHashCode() : self.GetHashCode(metaProperties);
      }

      var hash = 0;
      properties.Select(property => property.Compile()(self)).Where(value => value != null).Each(value => hash += value.GetHashCode());
      return hash;
    }
  }
}