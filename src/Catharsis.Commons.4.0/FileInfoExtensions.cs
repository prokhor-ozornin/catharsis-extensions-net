using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="FileInfo"/>.</para>
  /// </summary>
  /// <seealso cref="FileInfo"/>
  public static class FileInfoExtensions
  {
    /// <summary>
    ///   <para>Appends array of bytes to the end of specified file.</para>
    /// </summary>
    /// <param name="self">File to append data to.</param>
    /// <param name="bytes">Sequence of bytes to be added to the end of file.</param>
    /// <returns>Back reference to the current file.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Append(FileInfo, string, Encoding)"/>
    /// <seealso cref="Append(FileInfo, Stream)"/>
    public static FileInfo Append(this FileInfo self, byte[] bytes)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(bytes);

      if (bytes.Length > 0)
      {
        self.OpenWrite().Write(bytes).Close();
        self.Refresh();
      }

      return self;
    }

    /// <summary>
    ///   <para>Appends text content to the end of specified file.</para>
    /// </summary>
    /// <param name="self">File to append text to.</param>
    /// <param name="text">Text data to be added to the end of file.</param>
    /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
    /// <returns>Back reference to the current file.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Append(FileInfo, byte[])"/>
    /// <seealso cref="Append(FileInfo, Stream)"/>
    public static FileInfo Append(this FileInfo self, string text, Encoding encoding = null)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(text);

      if (text.Length > 0)
      {
        using (var writer = self.OpenWrite().TextWriter(encoding))
        {
          writer.Write(text);
        }
        self.Refresh();
      }

      return self;
    }

    /// <summary>
    ///   <para>Appends content of a stream to the end of specified file.</para>
    /// </summary>
    /// <param name="self">File to append stream data to.</param>
    /// <param name="stream">Stream to use as a source of data.</param>
    /// <returns>Back reference to the current file.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="stream"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Append(FileInfo, byte[])"/>
    /// <seealso cref="Append(FileInfo, string, Encoding)"/>
    public static FileInfo Append(this FileInfo self, Stream stream)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(stream);

      using (var destination = self.OpenWrite())
      {
        const int bufferSize = 4096;
        var buffer = new byte[bufferSize];
        int count;
        while ((count = stream.Read(buffer, 0, bufferSize)) > 0)
        {
          destination.Write(buffer, 0, count);
        }
      }

      self.Refresh();

      return self;
    }

    /// <summary>
    ///  <para>Reads entire contents of file and returns it as a byte array.</para>
    /// </summary>
    /// <param name="self">File to read data from.</param>
    /// <returns>Byte content of specified <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static byte[] Bytes(this FileInfo self)
    {
      Assertion.NotNull(self);

      return self.OpenRead().Bytes(true);
    }

    /// <summary>
    ///   <para>Erases all content from a file, making it a zero-length one.</para>
    /// </summary>
    /// <param name="self">File to truncate.</param>
    /// <returns>Back reference to the current file.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static FileInfo Clear(this FileInfo self)
    {
      Assertion.NotNull(self);

      File.Create(self.FullName).Close();
      self.Refresh();

      return self;
    }

    /// <summary>
    ///   <para>Reads text content of a file and returns it as a list of strings, using default system-dependent string separator.</para>
    /// </summary>
    /// <param name="self">File to read text from.</param>
    /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
    /// <returns>List of strings which have been read from a <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static IList<string> Lines(this FileInfo self, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      using (var reader = self.OpenRead().TextReader(encoding))
      {
        return reader.Lines();
      }
    }

    /// <summary>
    ///   <para>Reads text content of a file and returns it as a string.</para>
    /// </summary>
    /// <param name="self">File to read text from.</param>
    /// <param name="encoding">Text encoding to be used for transformation between text and bytes. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
    /// <returns>Text contents of a <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string Text(this FileInfo self, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      using (var reader = self.OpenRead().TextReader(encoding))
      {
        return reader.Text();
      }
    }
  }
}