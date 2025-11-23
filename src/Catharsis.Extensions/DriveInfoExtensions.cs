namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for filesystem I/O related types.</para>
/// </summary>
/// <seealso cref="DriveInfo"/>
public static class DriveInfoExtensions
{
  /// <param name="drive"></param>
  extension(DriveInfo drive)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is <see langword="null"/>.</exception>
    public long Size(string pattern = null, bool recursive = true) => drive?.RootDirectory.TotalSize(pattern, recursive) ?? throw new ArgumentNullException(nameof(drive));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public long Size => drive.Size();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is <see langword="null"/>.</exception>
    public IEnumerable<DirectoryInfo> Directories(string pattern = null, bool recursive = false) => drive?.RootDirectory.ListDirectories(pattern, recursive) ?? throw new ArgumentNullException(nameof(drive));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public IEnumerable<DirectoryInfo> Directories => drive.Directories();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty(DriveInfo)"/>
    public bool IsUnset => drive is null || drive.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="DriveInfo"/> instance can be considered "empty", meaning it contains no files or subdirectories.</para>
    /// </summary>
    /// <value>If the specified <paramref name="drive"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset(DriveInfo)"/>
    public bool IsEmpty => drive?.RootDirectory.IsEmpty ?? throw new ArgumentNullException(nameof(drive));

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="DriveInfo"/> that represents the same drive as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="drive"/> is <see langword="null"/>.</exception>
    public DriveInfo Clone() => drive is not null ? new DriveInfo(drive.Name) : throw new ArgumentNullException(nameof(drive));
  }
}