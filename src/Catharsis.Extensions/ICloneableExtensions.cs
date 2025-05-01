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
  public static T Clone<T>(this ICloneable cloneable) => cloneable.Clone().To<T>();
}