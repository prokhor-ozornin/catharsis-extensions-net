using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="FieldInfo"/>.</para>
  ///   <seealso cref="FieldInfo"/>
  /// </summary>
  public static class FieldInfoExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="field"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static object Attribute(this FieldInfo field, Type attributeType)
    {
      Assertion.NotNull(field);
      Assertion.NotNull(attributeType);

      return field.Attributes(attributeType).FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is a <c>null</c> reference.</exception>
    public static T Attribute<T>(this FieldInfo field)
    {
      Assertion.NotNull(field);

      return field.Attributes<T>().FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="field"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<object> Attributes(this FieldInfo field, Type attributeType)
    {
      Assertion.NotNull(field);
      Assertion.NotNull(attributeType);

      return field.GetCustomAttributes(attributeType, true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Attributes<T>(this FieldInfo field)
    {
      Assertion.NotNull(field);

      return field.Attributes(typeof(T)).Cast<T>();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is a <c>null</c> reference.</exception>
    public static bool IsProtected(this FieldInfo field)
    {
      Assertion.NotNull(field);

      return !field.IsPublic && !field.IsPrivate;
    }
  }
}