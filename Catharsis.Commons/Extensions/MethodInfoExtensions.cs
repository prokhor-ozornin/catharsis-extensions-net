using System;
using System.Reflection;

namespace Catharsis.Commons.Extensions
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