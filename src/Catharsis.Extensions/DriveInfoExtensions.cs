namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for filesystem I/O related types.</para>
/// </summary>
/// <seealso cref="DriveInfo"/>
public static class DriveInfoExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="DriveInfo"/> that represents the same drive as the original.</para>
  /// </summary>
  /// <param name="drive">Drive instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is <see langword="null"/>.</exception>
  public static DriveInfo Clone(this DriveInfo drive) => drive is not null ? new DriveInfo(drive.Name) : throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(DriveInfo)"/>
  public static bool IsUnset(this DriveInfo drive) => drive is null || drive.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="DriveInfo"/> instance can be considered "empty", meaning it contains no files or subdirectories.</para>
  /// </summary>
  /// <param name="drive">Drive instance for evaluation.</param>
  /// <returns>If the specified <paramref name="drive"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(DriveInfo)"/>
  public static bool IsEmpty(this DriveInfo drive) => drive?.RootDirectory.IsEmpty() ?? throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is <see langword="null"/>.</exception>
  public static IEnumerable<DirectoryInfo> Directories(this DriveInfo drive, string pattern = null, bool recursive = false) => drive?.RootDirectory.Directories(pattern, recursive) ?? throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is <see langword="null"/>.</exception>
  public static long Size(this DriveInfo drive, string pattern = null, bool recursive = true) => drive?.RootDirectory.Size(pattern, recursive) ?? throw new ArgumentNullException(nameof(drive));
}