using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="MethodInfo"/>.</para>
  /// </summary>
  public static class MethodInfoExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="method"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="method"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static object Attribute(this MethodInfo method, Type attributeType)
    {
      Assertion.NotNull(method);
      Assertion.NotNull(attributeType);

      return method.Attributes(attributeType).FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="method"/> is a <c>null</c> reference.</exception>
    public static T Attribute<T>(this MethodInfo method)
    {
      Assertion.NotNull(method);

      return method.Attributes<T>().FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="method"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="method"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<object> Attributes(this MethodInfo method, Type attributeType)
    {
      Assertion.NotNull(method);
      Assertion.NotNull(attributeType);

      return method.GetCustomAttributes(attributeType, true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="method"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Attributes<T>(this MethodInfo method)
    {
      Assertion.NotNull(method);

      return method.Attributes(typeof(T)).Cast<T>();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="method"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="method"/> or <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static Delegate Delegate(this MethodInfo method, Type type)
    {
      Assertion.NotNull(method);
      Assertion.NotNull(type);

      return System.Delegate.CreateDelegate(type, method);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="method"/> is a <c>null</c> reference.</exception>
    public static Delegate Delegate<T>(this MethodInfo method)
    {
      Assertion.NotNull(method);

      return method.Delegate(typeof(T));
    }
  }
}