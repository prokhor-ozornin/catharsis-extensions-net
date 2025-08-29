namespace Catharsis.Extensions;

/// <summary>
///   <para>A set of extension methods for the <see cref="ICloneable"/> interface.</para>
/// </summary>
public static class ICloneableExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="cloneable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="cloneable"/> is <see langword="null"/>.</exception>
  public static T Clone<T>(this ICloneable cloneable) => cloneable is not null ? cloneable.Clone().To<T>() : throw new ArgumentNullException(nameof(cloneable));
}