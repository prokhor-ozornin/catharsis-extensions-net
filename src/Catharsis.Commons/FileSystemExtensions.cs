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
  public static bool IsEmpty(this DriveInfo drive) => drive.RootDirectory.IsEmpty();

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
  public static bool IsEmpty(this FileInfo file) => !file.Exists || file.Length <= 0;

  /// <summary>
  ///   <para>Erases all content from a file, making it a zero-length one.</para>
  /// </summary>
  /// <param name="file">File to truncate.</param>
  /// <returns>Back reference to the current file.</returns>
  public static FileInfo Empty(this FileInfo file)
  {
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
    await using var writer = destination.ToStreamWriter(encoding);

    return await instance.PrintAsync(writer, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static FileInfo TryFinallyClear(this FileInfo file, Action<FileInfo> action)
  {
    return file.TryFinally(file =>
    {
      file.CreateWithPath();
      action(file);
    }, file => file.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static DirectoryInfo TryFinallyClear(this DirectoryInfo directory, Action<DirectoryInfo> action)
  {
    return directory.TryFinally(directory =>
    {
      directory.Create();
      action(directory);
    }, directory => directory.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static FileInfo TryFinallyDelete(this FileInfo file, Action<FileInfo> action)
  {
    return file.TryFinally(file =>
    {
      file.CreateWithPath();
      action(file);
    }, file => file.Delete());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static DirectoryInfo TryFinallyDelete(this DirectoryInfo directory, Action<DirectoryInfo> action)
  {
    return directory.TryFinally(directory =>
    {
      directory.Create();
      action(directory);
    }, directory => directory.Delete(true));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<FileInfo> Files(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory.Exists ? directory.EnumerateFiles(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<FileInfo>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<DirectoryInfo> Directories(this DriveInfo drive, string pattern = null, bool recursive = false) => drive.RootDirectory.Directories(pattern, recursive);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<DirectoryInfo> Directories(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory.Exists ? directory.EnumerateDirectories(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<DirectoryInfo>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static long Size(this DriveInfo drive, string pattern = null, bool recursive = true) => drive.RootDirectory.Size(pattern, recursive);

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
  public static Uri ToUri(this FileSystemInfo entry) => new(entry.FullName);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<FileSystemInfo> ToEnumerable(this DirectoryInfo directory, string pattern = null, bool recursive = false) => directory.Exists ? directory.EnumerateFileSystemInfos(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<FileSystemInfo>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static FileStream ToStream(this FileInfo file) => file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static FileStream ToReadOnlyStream(this FileInfo file) => file.Open(FileMode.Open, FileAccess.Read, FileShare.Read);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static FileStream ToWriteOnlyStream(this FileInfo file) => file.Open(FileMode.Append, FileAccess.Write, FileShare.None);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static StreamReader ToStreamReader(this FileInfo file, Encoding encoding = null) => new(file.FullName, encoding ?? Encoding.Default);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static StreamWriter ToStreamWriter(this FileInfo file, Encoding encoding = null) => new(file.FullName, true, encoding ?? Encoding.Default, -1);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this FileInfo source)
  {
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
    await destination.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);
    return text;
  }
}