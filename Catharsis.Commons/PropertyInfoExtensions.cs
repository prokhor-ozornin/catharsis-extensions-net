using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="PropertyInfo"/>.</para>
  ///   <seealso cref="PropertyInfo"/>
  /// </summary>
  public static class PropertyInfoExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="property"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static object Attribute(this PropertyInfo property, Type attributeType)
    {
      Assertion.NotNull(property);
      Assertion.NotNull(attributeType);

      return property.Attributes(attributeType).FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="property"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="property"/> is a <c>null</c> reference.</exception>
    public static T Attribute<T>(this PropertyInfo property)
    {
      Assertion.NotNull(property);

      return property.Attributes<T>().FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="property"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<object> Attributes(this PropertyInfo property, Type attributeType)
    {
      Assertion.NotNull(property);
      Assertion.NotNull(attributeType);

      return property.GetCustomAttributes(attributeType, true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="property"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="property"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Attributes<T>(this PropertyInfo property)
    {
      Assertion.NotNull(property);

      return property.Attributes(typeof(T)).Cast<T>();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="property"/> is a <c>null</c> reference.</exception>
    public static bool IsPublic(this PropertyInfo property)
    {
      Assertion.NotNull(property);

      if (property.CanRead)
      {
        return property.GetGetMethod() != null;
      }

      if (property.CanWrite)
      {
        return property.GetSetMethod() != null;
      }

      return false;
    }
  }
}