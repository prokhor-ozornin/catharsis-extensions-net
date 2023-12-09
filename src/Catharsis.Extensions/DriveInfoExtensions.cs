namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for filesystem I/O related types.</para>
/// </summary>
/// <seealso cref="DriveInfo"/>
public static class DriveInfoExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static DriveInfo Clone(this DriveInfo drive) => drive is not null ? new DriveInfo(drive.Name) : throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this DriveInfo drive) => drive?.RootDirectory.IsEmpty() ?? throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<DirectoryInfo> Directories(this DriveInfo drive, string pattern = null, bool recursive = false) => drive?.RootDirectory.Directories(pattern, recursive) ?? throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static long Size(this DriveInfo drive, string pattern = null, bool recursive = true) => drive?.RootDirectory.Size(pattern, recursive) ?? throw new ArgumentNullException(nameof(drive));
}