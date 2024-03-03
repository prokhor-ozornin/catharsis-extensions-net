using System.Reflection;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="MethodInfo"/>
public static class MethodInfoExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="method"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsOverridable(this MethodInfo method) => method is not null ? method.IsVirtual && !method.IsFinal : throw new ArgumentNullException(nameof(method));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="method"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsProtected(this MethodInfo method) => method?.IsFamily ?? throw new ArgumentNullException(nameof(method));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="method"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsInternal(this MethodInfo method) => method?.IsAssembly ?? throw new ArgumentNullException(nameof(method));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="method"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsProtectedInternal(this MethodInfo method) => method?.IsFamilyOrAssembly ?? throw new ArgumentNullException(nameof(method));

  /// <summary>
  ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
  /// </summary>
  /// <typeparam name="T">The type of delegate to create.</typeparam>
  /// <param name="method">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
  /// <returns>A delegate of the specified type to represent the specified static method.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Delegate ToDelegate<T>(this MethodInfo method) => method.ToDelegate(typeof(T));

  /// <summary>
  ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
  /// </summary>
  /// <param name="method">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
  /// <param name="type">The type of delegate to create.</param>
  /// <returns>A delegate of the specified type to represent the specified static method.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Delegate ToDelegate(this MethodInfo method, Type type)
  {
    if (method is null) throw new ArgumentNullException(nameof(method));
    if (type is null) throw new ArgumentNullException(nameof(type));

    return Delegate.CreateDelegate(type, method);
  }
}