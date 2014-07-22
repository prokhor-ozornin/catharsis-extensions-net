using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="MemberInfo"/>.</para>
  /// </summary>
  /// <seealso cref="MemberInfo"/>
  public static class MemberInfoExtensions
  {
    /// <summary>
    ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified <see cref="Type"/>, that is applied to current <see cref="Type"/>'s member.</para>
    ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
    /// </summary>
    /// <param name="self">Member of <see cref="Type"/>, whose attribute is to be returned.</param>
    /// <param name="attributeType">Type of custom attribute.</param>
    /// <returns>Instance of attribute, whose type equals to <paramref name="attributeType"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.GetCustomAttributes(System.Type, bool)"/>
    public static object Attribute(this MemberInfo self, Type attributeType)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(attributeType);

      return self.Attributes(attributeType).FirstOrDefault();
    }

    /// <summary>
    ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified <see cref="Type"/>, that is applied to current <see cref="Type"/>'s member.</para>
    ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
    /// </summary>
    /// <typeparam name="T">Type of custom attribute.</typeparam>
    /// <param name="self">Member of <see cref="Type"/>, whose attribute is to be returned.</param>
    /// <returns>Instance of attribute, whose type equals to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.GetCustomAttributes(System.Type, bool)"/>
    public static T Attribute<T>(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.Attributes<T>().FirstOrDefault();
    }

    /// <summary>
    ///   <para>Returns a collection of all custom attributes, identified by specified <see cref="Type"/>, which are applied to current <see cref="Type"/>'s member.</para>
    /// </summary>
    /// <param name="self">Member of <see cref="Type"/>, whose attributes are to be returned.</param>
    /// <param name="attributeType">Type of custom attributes.</param>
    /// <returns>Collection of custom attributes, whose type equals to <paramref name="attributeType"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.GetCustomAttributes(System.Type, bool)"/>
    public static IEnumerable<object> Attributes(this MemberInfo self, Type attributeType)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(attributeType);

      return self.GetCustomAttributes(attributeType, true);
    }

    /// <summary>
    ///   <para>Returns a collection of all custom attributes, identified by specified <see cref="Type"/>, which are applied to current <see cref="Type"/>'s member.</para>
    /// </summary>
    /// <typeparam name="T">Type of custom attributes.</typeparam>
    /// <param name="self">Member of <see cref="Type"/>, whose attributes are to be returned.</param>
    /// <returns>Collection of custom attributes, whose type equals to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.GetCustomAttributes(System.Type, bool)"/>
    public static IEnumerable<T> Attributes<T>(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.Attributes(typeof(T)).Cast<T>();
    }

    /// <summary>
    ///   <para>Determines whether a targer <see cref="Type"/>'s member represents constructor of a class (a <see cref="ConstructorInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents class constructor, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Constructor"/>
    public static bool IsConstructor(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Constructor;
    }

    /// <summary>
    ///   <para>Determines whether a targer <see cref="Type"/>'s member represents an event (a <see cref="EventInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents an event, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Event"/>
    public static bool IsEvent(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Event;
    }

    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a field (a <see cref="FieldInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents a field, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Field"/>
    public static bool IsField(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Field;
    }
    
    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a method (a <see cref="MethodInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents a method, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Method"/>
    public static bool IsMethod(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Method;
    }

    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a property (a <see cref="PropertyInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents a property, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Property"/>
    public static bool IsProperty(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Property;
    }

    /// <summary>
    ///   <para>Returns the <see cref="Type"/> of the target <see cref="Type"/>'s property/field member.</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class that represents either a field (<see cref="FieldInfo"/> instance) or property (<see cref="PropertyInfo"/> instance).</param>
    /// <returns>Type of the field/property, represented by specified <paramref name="self"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.DeclaringType"/>
    public static Type Type(this MemberInfo self)
    {
      Assertion.NotNull(self);

      switch (self.MemberType)
      {
        case MemberTypes.Event:
          return self.To<EventInfo>().EventHandlerType;
  
        case MemberTypes.Field:
          return self.To<FieldInfo>().FieldType;
        
        case MemberTypes.Method:
          return self.To<MethodInfo>().ReturnType;
        
        case MemberTypes.Property:
          return self.To<PropertyInfo>().PropertyType;
        
        default:
          return self.DeclaringType;
      }
    }
  }
}