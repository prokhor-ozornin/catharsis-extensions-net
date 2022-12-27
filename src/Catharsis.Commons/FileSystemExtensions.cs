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
  /// <param name="action"></param>
  /// <returns></returns>
  public static FileInfo UseTemporarily(this FileInfo file, Action<FileInfo> action)
  {
    return file.UseFinally(file =>
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
  public static DirectoryInfo UseTemporarily(this DirectoryInfo directory, Action<DirectoryInfo> action)
  {
    return directory.UseFinally(directory =>
    {
      directory.Create();
      action(directory);
    }, directory => directory.Delete(true));
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
  ///  <para>Reads entire contents of file and returns it as a byte array.</para>
  /// </summary>
  /// <param name="source">File to read data from.</param>
  /// <param name="cancellation"></param>
  /// <returns>Byte content of specified <paramref name="source"/>.</returns>
  public static async IAsyncEnumerable<byte> Bytes(this FileInfo source, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    await using var stream = source.OpenRead();

    await foreach (var element in stream.Bytes(cancellation))
    {
      yield return element;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<FileInfo> Bytes(this FileInfo destination, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    try
    {
      await using var stream = destination.OpenWrite();
      await stream.Bytes(bytes, cancellation);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para>Reads text content of a file and returns it as a string.</para>
  /// </summary>
  /// <param name="source">File to read text from.</param>
  /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
  /// <returns>Text contents of a <paramref name="source"/>.</returns>
  public static async Task<string> Text(this FileInfo source, Encoding? encoding = null)
  {
    await using var stream = source.OpenRead();
    using var reader = stream.ToStreamReader(encoding);

    return await reader.Text();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<FileInfo> Text(this FileInfo destination, string text, Encoding? encoding = null, CancellationToken cancellation = default)
  {
    try
    {
      await using var stream = destination.OpenWrite();
      await stream.Text(text, encoding, cancellation);

      return destination;
    }
    finally
    {
      destination.Refresh();
    }
  }

  /// <summary>
  ///   <para>Reads text content of a file and returns it as a list of strings, using default system-dependent string separator.</para>
  /// </summary>
  /// <param name="file">File to read text from.</param>
  /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
  /// <returns>List of strings which have been read from a <paramref name="file"/>.</returns>
  public static async IAsyncEnumerable<string> Lines(this FileInfo file, Encoding? encoding = null)
  {
    await using var stream = file.OpenRead();
    using var reader = stream.ToStreamReader(encoding);

    await foreach (var line in reader.Lines())
    {
      yield return line;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<FileInfo> Print(this FileInfo destination, object instance, CancellationToken cancellation = default)
  {
    await destination.ToStream().Print(instance, cancellation: cancellation);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteTo(this IEnumerable<byte> bytes, FileInfo destination, CancellationToken cancellation = default)
  {
    await destination.Bytes(bytes, cancellation);
    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteTo(this string text, FileInfo destination, Encoding? encoding = null, CancellationToken cancellation = default)
  {
    await destination.Text(text, encoding, cancellation);
    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static long Size(this DriveInfo drive, string? pattern = null, bool recursive = true) => drive.RootDirectory.Size(pattern, recursive);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static long Size(this DirectoryInfo directory, string? pattern = null, bool recursive = true) => directory.Files(pattern, recursive).Sum(file => file.Length);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="drive"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<DirectoryInfo> Directories(this DriveInfo drive, string? pattern = null, bool recursive = false) => drive.RootDirectory.Directories(pattern, recursive);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<DirectoryInfo> Directories(this DirectoryInfo directory, string? pattern = null, bool recursive = false) => directory.Exists ? directory.EnumerateDirectories(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<DirectoryInfo>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<FileInfo> Files(this DirectoryInfo directory, string? pattern = null, bool recursive = false) => directory.Exists ? directory.EnumerateFiles(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<FileInfo>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="directory"></param>
  /// <param name="pattern"></param>
  /// <param name="recursive"></param>
  /// <returns></returns>
  public static IEnumerable<FileSystemInfo> ToEnumerable(this DirectoryInfo directory, string? pattern = null, bool recursive = false) => directory.Exists ? directory.EnumerateFileSystemInfos(pattern ?? "*", new EnumerationOptions { RecurseSubdirectories = recursive }) : Enumerable.Empty<FileSystemInfo>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static FileStream ToStream(this FileInfo file) => new(file.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="file"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static StreamReader ToStreamReader(this FileInfo file, Encoding? encoding = null) => new(file.FullName, encoding ?? Encoding.Default);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="entry"></param>
  /// <returns></returns>
  public static Uri ToUri(this FileSystemInfo entry) => new(entry.FullName);
}