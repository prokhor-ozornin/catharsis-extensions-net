namespace Catharsis.Extensions;

/// <summary>
///   <para>A set of extension methods for the <see cref="ICloneable"/> interface.</para>
/// </summary>
public static class ICloneableExtensions
{
  /// <param name="cloneable"></param>
  extension(ICloneable cloneable)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cloneable"/> is <see langword="null"/>.</exception>
    public T Clone<T>() => cloneable is not null ? cloneable.Clone().To<T>() : throw new ArgumentNullException(nameof(cloneable));
  }
}