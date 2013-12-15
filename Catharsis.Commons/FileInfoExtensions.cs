using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="FileInfo"/>.</para>
  ///   <seealso cref="FileInfo"/>
  /// </summary>
  public static class FileInfoExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static FileInfo Append(this FileInfo file, byte[] bytes)
    {
      Assertion.NotNull(file);
      Assertion.NotNull(bytes);

      if (bytes.Length > 0)
      {
        file.OpenWrite().Write(bytes).Close();
        file.Refresh();
      }

      return file;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    public static FileInfo Append(this FileInfo file, string text, Encoding encoding = null)
    {
      Assertion.NotNull(file);
      Assertion.NotNull(text);

      if (text.Length > 0)
      {
        file.OpenWrite().TextWriter(encoding).WriteObject(text).Close();
        file.Refresh();
      }

      return file;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="file"/> or <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static FileInfo Append(this FileInfo file, Stream stream)
    {
      Assertion.NotNull(file);
      Assertion.NotNull(stream);

      file.OpenWrite().With(stream.CopyTo);
      file.Refresh();

      return file;
    }

    /// <summary>
    ///  <para></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is a <c>null</c> reference.</exception>
    public static byte[] Bytes(this FileInfo file)
    {
      Assertion.NotNull(file);

      return file.OpenRead().Bytes(true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is a <c>null</c> reference.</exception>
    public static FileInfo Clear(this FileInfo file)
    {
      Assertion.NotNull(file);

      File.Create(file.FullName).Close();
      file.Refresh();

      return file;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is a <c>null</c> reference.</exception>
    public static IList<string> Lines(this FileInfo file, Encoding encoding = null)
    {
      Assertion.NotNull(file);

      return file.OpenRead().TextReader(encoding).With(reader => reader.Lines());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is a <c>null</c> reference.</exception>
    public static string Text(this FileInfo file, Encoding encoding = null)
    {
      Assertion.NotNull(file);

      return file.OpenRead().TextReader(encoding).With(reader => reader.Text());
    }
  }
}