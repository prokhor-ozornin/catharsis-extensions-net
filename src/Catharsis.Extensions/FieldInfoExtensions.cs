using System.Reflection;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="FieldInfo"/>
public static class FieldInfoExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="field"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="field"/> is <see langword="null"/>.</exception>
  public static bool IsOfType<T>(this FieldInfo field) => field is not null ? field.FieldType == typeof(T) : throw new ArgumentNullException(nameof(field));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="field"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="field"/> is <see langword="null"/>.</exception>
  public static bool IsProtected(this FieldInfo field) => field?.IsFamily ?? throw new ArgumentNullException(nameof(field));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="field"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="field"/> is <see langword="null"/>.</exception>
  public static bool IsInternal(this FieldInfo field) => field?.IsAssembly ?? throw new ArgumentNullException(nameof(field));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="field"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="field"/> is <see langword="null"/>.</exception>
  public static bool IsProtectedInternal(this FieldInfo field) => field?.IsFamilyOrAssembly ?? throw new ArgumentNullException(nameof(field));
}