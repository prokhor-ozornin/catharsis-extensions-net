namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for filesystem I/O related types.</para>
/// </summary>
/// <seealso cref="FileSystemInfo"/>
public static class FileSystemInfoExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="entry"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="entry"/> is <see langword="null"/>.</exception>
  public static Uri ToUri(this FileSystemInfo entry) => entry is not null ? new Uri(entry.FullName) : throw new ArgumentNullException(nameof(entry));
}