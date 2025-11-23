namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for filesystem I/O related types.</para>
/// </summary>
/// <seealso cref="DirectoryInfo"/>
public static class DirectoryInfoExtensions
{
  /// <param name="directory"></param>
  extension(DirectoryInfo directory)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
    public long TotalSize(string pattern = null, bool recursive = true) => directory.ListFiles(pattern, recursive).Sum(file => file.Length);
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public long Size => directory.TotalSize();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="parent"/> is <see langword="null"/>.</exception>
    public bool InDirectory(DirectoryInfo parent)
    {
      if (directory is null) throw new ArgumentNullException(nameof(directory));
      if (parent is null) throw new ArgumentNullException(nameof(parent));

      return parent.ListDirectories(null, true).Contains(directory);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
    public IEnumerable<FileInfo> ListFiles(string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateFiles(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : [] : throw new ArgumentNullException(nameof(directory));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public IEnumerable<FileInfo> Files => ListFiles(directory);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
    public IEnumerable<DirectoryInfo> ListDirectories(string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateDirectories(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : [] : throw new ArgumentNullException(nameof(directory));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public IEnumerable<DirectoryInfo> Directories => ListDirectories(directory);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty(DirectoryInfo)"/>
    public bool IsUnset => directory is null || directory.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="DirectoryInfo"/> instance can be considered "empty", meaning it either doesn't exist or doesn't contain any other files or directories.</para>
    /// </summary>
    /// <value>If the specified <paramref name="directory"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset(DirectoryInfo)"/>
    public bool IsEmpty => directory is not null ? !directory.Exists || directory.ToEnumerable().IsEmpty() : throw new ArgumentNullException(nameof(directory));

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="DirectoryInfo"/> that will point to the same directory as the original.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
    public DirectoryInfo Clone() => directory is not null ? new DirectoryInfo(directory.ToString()) : throw new ArgumentNullException(nameof(directory));

    /// <summary>
    ///   <para>"Empties" a specified <seealso cref="BinaryWriter"/> by setting the length of its underlying <seealso cref="Stream"/> to zero.</para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="directory"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/>   is <see langword="null"/>.</exception>
    public DirectoryInfo Empty()
    {
      if (directory is null) throw new ArgumentNullException(nameof(directory));

      ListDirectories(directory).ForEach(info => info.Delete(true));
      ListFiles(directory).ForEach(file => file.Delete());

      return directory;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="directory"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryFinallyDelete(DirectoryInfo, Action{DirectoryInfo})"/>
    public DirectoryInfo TryFinallyClear(Action<DirectoryInfo> action)
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
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="directory"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso cref="TryFinallyClear(DirectoryInfo, Action{DirectoryInfo})"/>
    public DirectoryInfo TryFinallyDelete(Action<DirectoryInfo> action)
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
    /// <param name="entries"></param>
    /// <returns>Back self-reference to the given <paramref name="directory"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="entries"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With(DirectoryInfo, FileSystemInfo[])"/>
    public DirectoryInfo With(IEnumerable<FileSystemInfo> entries)
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
    /// <param name="entries"></param>
    /// <returns>Back self-reference to the given <paramref name="directory"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With(DirectoryInfo, IEnumerable{FileSystemInfo})"/>
    public DirectoryInfo With(params FileSystemInfo[] entries) => directory.With(entries as IEnumerable<FileSystemInfo>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="entries"></param>
    /// <returns>Back self-reference to the given <paramref name="directory"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="directory"/> or <paramref name="entries"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without(DirectoryInfo, FileSystemInfo[])"/>
    public DirectoryInfo Without(IEnumerable<FileSystemInfo> entries)
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
    /// <param name="entries"></param>
    /// <returns>Back self-reference to the given <paramref name="directory"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without(DirectoryInfo, IEnumerable{FileSystemInfo})"/>
    public DirectoryInfo Without(params FileSystemInfo[] entries) => directory.Without(entries as IEnumerable<FileSystemInfo>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is <see langword="null"/>.</exception>
    public IEnumerable<FileSystemInfo> ToEnumerable(string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateFileSystemInfos(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : [] : throw new ArgumentNullException(nameof(directory));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => directory is not null && directory.Exists;
  }
}