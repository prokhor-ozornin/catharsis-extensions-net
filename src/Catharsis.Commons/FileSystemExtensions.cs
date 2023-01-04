﻿using System.Runtime.CompilerServices;
using System.Text;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for filesystem I/O related types.</para>
/// </summary>
/// <seealso cref="DriveInfo"/>
/// <seealso cref="DirectoryInfo"/>
/// <seealso cref="FileInfo"/>
public static class FileSystemExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <returns></returns>
  public static bool IsEmpty(this DriveInfo drive) => drive is not null ? drive.RootDirectory.IsEmpty() : throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <returns></returns>
  public static bool IsEmpty(this DirectoryInfo directory) => directory.ToEnumerable().IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static bool IsEmpty(this FileInfo file) => file is not null ? !file.Exists || file.Length <= 0 : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para>Erases all content from a file, making it a zero-length one.</para>
  /// </summary>
  /// <param name="file">File to truncate.</param>
  /// <returns>Back reference to the current file.</returns>
  public static FileInfo Empty(this FileInfo file)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    file.CreateWithPath().Refresh();

    return file;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <returns></returns>
  public static DirectoryInfo Empty(this DirectoryInfo directory)
  {
    if (directory is null) throw new ArgumentNullException(nameof(directory));

    directory.Directories().ForEach(directory => directory.Delete(true));
    directory.Files().ForEach(file => file.Delete());

    return directory;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static FileInfo CreateWithPath(this FileInfo file)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    if (file.DirectoryName != null)
    {
      Directory.CreateDirectory(file.DirectoryName);
    }

    using var stream = file.Create();

    return file;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static IEnumerable<string> Lines(this FileInfo file, Encoding encoding = null)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToStreamReader(encoding);

    return reader.Lines();
  }

  /// <summary>
  ///   <para>Reads text content of a file and returns it as a list of strings, using default system-dependent string separator.</para>
  /// </summary>
  /// <param name="file">File to read text from.</param>
  /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
  /// <returns>List of strings which have been read from a <paramref name="file"/>.</returns>
  public static async IAsyncEnumerable<string> LinesAsync(this FileInfo file, Encoding encoding = null)
  {
    if (file is null) throw new ArgumentNullException(nameof(file));

    using var reader = file.ToStreamReader(encoding);

    await foreach (var line in reader.LinesAsync().ConfigureAwait(false))
    {
      yield return line;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static T Print<T>(this T instance, FileInfo destination, Encoding encoding = null)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    using var writer = destination.ToStreamWriter(encoding);

    return instance.Print(writer);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> PrintAsync<T>(this T instance, FileInfo destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await using var writer = destination.ToStreamWriter(encoding);

    return await instance.PrintAsync(writer, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static FileInfo TryFinallyClear(this FileInfo file, Action<FileInfo> action) => file.TryFinally(file =>
    {
      file.CreateWithPath();
      action(file);
    }, file => file.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static DirectoryInfo TryFinallyClear(this DirectoryInfo directory, Action<DirectoryInfo> action) => directory.TryFinally(directory =>
    {
      directory.Create();
      action(directory);
    }, directory => directory.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static FileInfo TryFinallyDelete(this FileInfo file, Action<FileInfo> action) => file.TryFinally(file =>
    {
      file.CreateWithPath();
      action(file);
    }, file => file.Delete());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static DirectoryInfo TryFinallyDelete(this DirectoryInfo directory, Action<DirectoryInfo> action) => directory.TryFinally(directory =>
    {
      directory.Create();
      action(directory);
    }, directory => directory.Delete(true));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<FileInfo> Files(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateFiles(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<FileInfo>() : throw new ArgumentNullException(nameof(directory));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<DirectoryInfo> Directories(this DriveInfo drive, string pattern = null, bool recursive = false) => drive is not null ? drive.RootDirectory.Directories(pattern, recursive) : throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<DirectoryInfo> Directories(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateDirectories(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<DirectoryInfo>() : throw new ArgumentNullException(nameof(directory));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static long Size(this DriveInfo drive, string pattern = null, bool recursive = true) => drive is not null ? drive.RootDirectory.Size(pattern, recursive) : throw new ArgumentNullException(nameof(drive));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static long Size(this DirectoryInfo directory, string pattern = null, bool recursive = true) => directory.Files(pattern, recursive).Sum(file => file.Length);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="entry"></param>
  /// <returns></returns>
  public static Uri ToUri(this FileSystemInfo entry) => entry is not null ? new Uri(entry.FullName) : throw new ArgumentNullException(nameof(entry));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<FileSystemInfo> ToEnumerable(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory is not null ? directory.Exists ? directory.EnumerateFileSystemInfos(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<FileSystemInfo>() : throw new ArgumentNullException(nameof(directory));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static FileStream ToStream(this FileInfo file) => file is not null ? file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static FileStream ToReadOnlyStream(this FileInfo file) => file is not null ? file.Open(FileMode.Open, FileAccess.Read, FileShare.Read) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static FileStream ToWriteOnlyStream(this FileInfo file) => file is not null ? file.Open(FileMode.Append, FileAccess.Write, FileShare.None) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static StreamReader ToStreamReader(this FileInfo file, Encoding encoding = null) => file is not null ? new StreamReader(file.FullName, encoding ?? Encoding.Default) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static StreamWriter ToStreamWriter(this FileInfo file, Encoding encoding = null) => file is not null ? new StreamWriter(file.FullName, true, encoding ?? Encoding.Default, -1) : throw new ArgumentNullException(nameof(file));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this FileInfo source)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var stream = source.ToReadOnlyStream();

    return stream.ToBytes();
  }

  /// <summary>
  ///  <para>Reads entire contents of file and returns it as a byte array.</para>
  /// </summary>
  /// <param name="source">File to read data from.</param>
  /// <param name="cancellation"></param>
  /// <returns>Byte content of specified <paramref name="source"/>.</returns>
  public static async IAsyncEnumerable<byte> ToBytesAsync(this FileInfo source, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    await using var stream = source.ToReadOnlyStream();

    await foreach (var element in stream.ToBytesAsync(cancellation).ConfigureAwait(false))
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string ToText(this FileInfo source, Encoding encoding = null)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToStreamReader(encoding);

    return reader.ToText();
  }

  /// <summary>
  ///   <para>Reads text content of a file and returns it as a string.</para>
  /// </summary>
  /// <param name="source">File to read text from.</param>
  /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
  /// <returns>Text contents of a <paramref name="source"/>.</returns>
  public static async Task<string> ToTextAsync(this FileInfo source, Encoding encoding = null)
  {
    if (source is null) throw new ArgumentNullException(nameof(source));

    using var reader = source.ToStreamReader(encoding);

    return await reader.ToTextAsync().ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  public static FileInfo WriteBytes(this FileInfo destination, IEnumerable<byte> bytes)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    try
    {
      using var stream = destination.ToWriteOnlyStream();

      stream.WriteBytes(bytes);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<FileInfo> WriteBytesAsync(this FileInfo destination, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    try
    {
      await using var stream = destination.ToWriteOnlyStream();

      await stream.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<FileInfo> WriteTextAsync(this FileInfo destination, string text, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    try
    {
      await using var writer = destination.ToStreamWriter(encoding);

      await writer.WriteTextAsync(text, cancellation).ConfigureAwait(false);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static FileInfo WriteText(this FileInfo destination, string text, Encoding encoding = null)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    try
    {
      using var writer = destination.ToStreamWriter(encoding);
      writer.WriteText(text);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, FileInfo destination)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, FileInfo destination, CancellationToken cancellation = default)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, FileInfo destination, Encoding encoding = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text, encoding);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteToAsync(this string text, FileInfo destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);

    return text;
  }
}