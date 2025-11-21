using System.Reflection;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="MethodInfo"/>
public static class MethodInfoExtensions
{
  /// <param name="method"></param>
  extension(MethodInfo method)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="method"/> is <see langword="null"/>.</exception>
    public bool IsOverridable => method is not null ? method.IsVirtual && !method.IsFinal : throw new ArgumentNullException(nameof(method));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="method"/> is <see langword="null"/>.</exception>
    public bool IsProtected => method?.IsFamily ?? throw new ArgumentNullException(nameof(method));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="method"/> is <see langword="null"/>.</exception>
    public bool IsInternal => method?.IsAssembly ?? throw new ArgumentNullException(nameof(method));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="method"/> is <see langword="null"/>.</exception>
    public bool IsProtectedInternal => method?.IsFamilyOrAssembly ?? throw new ArgumentNullException(nameof(method));

    /// <summary>
    ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
    /// </summary>
    /// <typeparam name="T">The type of delegate to create.</typeparam>
    /// <returns>A delegate of the specified type to represent the specified static method.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="method"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToDelegate(MethodInfo, Type)"/>
    public Delegate ToDelegate<T>() => method.ToDelegate(typeof(T));

    /// <summary>
    ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
    /// </summary>
    /// <param name="type">The type of delegate to create.</param>
    /// <returns>A delegate of the specified type to represent the specified static method.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="method"/> or <paramref name="type"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToDelegate{T}(MethodInfo)"/>
    public Delegate ToDelegate(Type type)
    {
      if (method is null) throw new ArgumentNullException(nameof(method));
      if (type is null) throw new ArgumentNullException(nameof(type));

      return Delegate.CreateDelegate(type, method);
    }
  }
}