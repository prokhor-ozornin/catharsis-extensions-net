using System;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="MethodInfo"/>.</para>
  /// </summary>
  public static class MethodInfoExtensions
  {
    /// <summary>
    ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
    /// </summary>
    /// <param name="self">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
    /// <param name="type">The <see cref="Type"/> of delegate to create.</param>
    /// <returns>A delegate of the specified type to represent the specified static method.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="System.Delegate.CreateDelegate(Type, MethodInfo)"/>
    /// <seealso cref="Delegate{T}(MethodInfo)"/>
    public static Delegate Delegate(this MethodInfo self, Type type)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(type);

      return System.Delegate.CreateDelegate(type, self);
    }

    /// <summary>
    ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of delegate to create.</typeparam>
    /// <param name="self">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
    /// <returns>A delegate of the specified type to represent the specified static method.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="System.Delegate.CreateDelegate(Type, MethodInfo)"/>
    /// <seealso cref="Delegate(MethodInfo, Type)"/>
    public static Delegate Delegate<T>(this MethodInfo self)
    {
      Assertion.NotNull(self);

      return self.Delegate(typeof(T));
    }
  }
}