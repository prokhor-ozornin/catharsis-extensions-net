namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for filesystem I/O related types.</para>
/// </summary>
/// <seealso cref="DirectoryInfo"/>
public static class DirectoryInfoExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="DirectoryInfo"/> that will point to the same directory as the original.</para>
  /// </summary>
  /// <param name="directory">Directory instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
  public static DirectoryInfo Clone(this DirectoryInfo directory) => directory is not null ? new DirectoryInfo(directory.ToString()) : throw new ArgumentNullException(nameof(directory));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="entries"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="entries"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(DirectoryInfo, FileSystemInfo[])"/>
  public static DirectoryInfo With(this DirectoryInfo directory, IEnumerable<FileSystemInfo> entries)
  {
    if (directory is null) throw new ArgumentNullException(nameof(directory));
    if (entries is null) throw new ArgumentNullException(nameof(entries));

    foreach (var entry in entries)
    {
      File.Create(Path.Combine(directory.FullName, entry.Name));
    }

    return directory;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="entries"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(DirectoryInfo, IEnumerable{FileSystemInfo})"/>
  public static DirectoryInfo With(this DirectoryInfo directory, params FileSystemInfo[] entries) => directory.With(entries as IEnumerable<FileSystemInfo>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="entries"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="entries"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(DirectoryInfo, FileSystemInfo[])"/>
  public static DirectoryInfo Without(this DirectoryInfo directory, IEnumerable<FileSystemInfo> entries)
  {
    if (directory is null) throw new ArgumentNullException(nameof(directory));
    if (entries is null) throw new ArgumentNullException(nameof(entries));

    foreach (var entry in entries)
    {
      File.Delete(Path.Combine(directory.FullName, entry.Name));
    }

    return directory;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="entries"></param>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(DirectoryInfo, IEnumerable{FileSystemInfo})"/>
  public static DirectoryInfo Without(this DirectoryInfo directory, params FileSystemInfo[] entries) => directory.Without(entries as IEnumerable<FileSystemInfo>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(DirectoryInfo)"/>
  public static bool IsUnset(this DirectoryInfo directory) => directory is null || directory.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="DirectoryInfo"/> instance can be considered "empty", meaning it either doesn't exist or doesn't contain any other files or directories.</para>
  /// </summary>
  /// <param name="directory">Directory instance for evaluation.</param>
  /// <returns>If the specified <paramref name="directory"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(DirectoryInfo)"/>
  public static bool IsEmpty(this DirectoryInfo directory) => directory is not null ? !directory.Exists || directory.ToEnumerable().IsEmpty() : throw new ArgumentNullException(nameof(directory));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/>   is <see langword="null"/>.</exception>
  public static DirectoryInfo Empty(this DirectoryInfo directory)
  {
    if (directory is null) throw new ArgumentNullException(nameof(directory));

    directory.Directories().ForEach(info => info.Delete(true));
    directory.Files().ForEach(file => file.Delete());

    return directory;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="TryFinallyDelete(DirectoryInfo, Action{DirectoryInfo})"/>
  public static DirectoryInfo TryFinallyClear(this DirectoryInfo directory, Action<DirectoryInfo> action)
  {
    if (directory is null) throw new ArgumentNullException(nameof(directory));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return directory.TryFinally(info =>
    {
      info.Create();
      action(info);
    }, info => info.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  /// <seealso cref="TryFinallyClear(DirectoryInfo, Action{DirectoryInfo})"/>
  public static DirectoryInfo TryFinallyDelete(this DirectoryInfo directory, Action<DirectoryInfo> action)
  {
    if (directory is null) throw new ArgumentNullException(nameof(directory));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return directory.TryFinally(info =>
    {
      info.Create();
      action(info);
    }, info =>
    {
      if (info.Exists)
      {
        info.Delete(true);
      }
    });
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
  public static IEnumerable<FileInfo> Files(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateFiles(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : [] : throw new ArgumentNullException(nameof(directory));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
  public static IEnumerable<DirectoryInfo> Directories(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateDirectories(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : [] : throw new ArgumentNullException(nameof(directory));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="parent"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="parent"/> is <see langword="null"/>.</exception>
  public static bool InDirectory(this DirectoryInfo directory, DirectoryInfo parent)
  {
    if (directory is null) throw new ArgumentNullException(nameof(directory));
    if (parent is null) throw new ArgumentNullException(nameof(parent));

    return parent.Directories(null, true).Contains(directory);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
  public static long Size(this DirectoryInfo directory, string pattern = null, bool recursive = true) => directory.Files(pattern, recursive).Sum(file => file.Length);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
  public static IEnumerable<FileSystemInfo> ToEnumerable(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateFileSystemInfos(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : [] : throw new ArgumentNullException(nameof(directory));
}