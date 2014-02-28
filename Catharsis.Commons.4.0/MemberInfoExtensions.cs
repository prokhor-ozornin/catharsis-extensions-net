using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="MemberInfo"/>.</para>
  ///   <seealso cref="MemberInfo"/>
  /// </summary>
  public static class MemberInfoExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="member"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="member"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static object Attribute(this MemberInfo member, Type attributeType)
    {
      Assertion.NotNull(member);
      Assertion.NotNull(attributeType);

      return member.Attributes(attributeType).FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="member"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static T Attribute<T>(this MemberInfo member)
    {
      Assertion.NotNull(member);

      return member.Attributes<T>().FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="member"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="member"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<object> Attributes(this MemberInfo member, Type attributeType)
    {
      Assertion.NotNull(member);
      Assertion.NotNull(attributeType);

      return member.GetCustomAttributes(attributeType, true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="member"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Attributes<T>(this MemberInfo member)
    {
      Assertion.NotNull(member);

      return member.Attributes(typeof(T)).Cast<T>();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static bool IsConstructor(this MemberInfo member)
    {
      Assertion.NotNull(member);

      return member.MemberType == MemberTypes.Constructor;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static bool IsEvent(this MemberInfo member)
    {
      Assertion.NotNull(member);

      return member.MemberType == MemberTypes.Event;
    }

    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a field (a <see cref="FieldInfo"/> instance).</para>
    /// </summary>
    /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="member"/> represents a field, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static bool IsField(this MemberInfo member)
    {
      Assertion.NotNull(member);

      return member.MemberType == MemberTypes.Field;
    }
    
    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a method (a <see cref="MethodInfo"/> instance).</para>
    /// </summary>
    /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="member"/> represents a method, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static bool IsMethod(this MemberInfo member)
    {
      Assertion.NotNull(member);

      return member.MemberType == MemberTypes.Method;
    }

    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a property (a <see cref="PropertyInfo"/> instance).</para>
    /// </summary>
    /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="member"/> represents a property, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static bool IsProperty(this MemberInfo member)
    {
      Assertion.NotNull(member);

      return member.MemberType == MemberTypes.Property;
    }

    /// <summary>
    ///   <para>Returns the <see cref="Type"/> of the target <see cref="Type"/>'s property/field member.</para>
    /// </summary>
    /// <param name="member">Instance of extended <see cref="MemberInfo"/> class that represents either a field (<see cref="FieldInfo"/> instance) or property (<see cref="PropertyInfo"/> instance).</param>
    /// <returns>Type of the field/property, represented by specified <paramref name="member"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static Type MemberType(this MemberInfo member)
    {
      Assertion.NotNull(member);

      switch (member.MemberType)
      {
        case MemberTypes.Event:
        return member.To<EventInfo>().EventHandlerType;
        case MemberTypes.Field:
        return member.To<FieldInfo>().FieldType;
        case MemberTypes.Method:
        return member.To<MethodInfo>().ReturnType;
        case MemberTypes.Property:
        return member.To<PropertyInfo>().PropertyType;
        default:
        return member.DeclaringType;
      }
    }
  }
}