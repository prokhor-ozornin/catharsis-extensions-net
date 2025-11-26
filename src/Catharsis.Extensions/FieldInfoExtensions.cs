using System.Reflection;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="FieldInfo"/>
public static class FieldInfoExtensions
{
  /// <param name="field"></param>
  extension(FieldInfo field)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is <see langword="null"/>.</exception>
    public bool IsProtected => @field?.IsFamily ?? throw new ArgumentNullException(nameof(@field));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is <see langword="null"/>.</exception>
    public bool IsInternal => @field?.IsAssembly ?? throw new ArgumentNullException(nameof(@field));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is <see langword="null"/>.</exception>
    public bool IsProtectedInternal => @field?.IsFamilyOrAssembly ?? throw new ArgumentNullException(nameof(@field));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is <see langword="null"/>.</exception>
    public bool IsOfType<T>() => field is not null ? field.FieldType == typeof(T) : throw new ArgumentNullException(nameof(field));
  }
}