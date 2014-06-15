using System;
using System.IO;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  /// </summary>
  /// <seealso cref="Stream"/>
  public static class StreamExtendedExtensions
  {
    /// <summary>
    ///   <para>Creates a buffered version of <see cref="Stream"/> from specified one.</para>
    /// </summary>
    /// <param name="stream">Original stream that should be buffered.</param>
    /// <param name="bufferSize">Size of buffer in bytes. If not specified, default buffer size will be used.</param>
    /// <returns>Buffer version of stream that wraps original <paramref name="stream"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BufferedStream"/>
    public static BufferedStream Buffered(this Stream stream, int? bufferSize = null)
    {
      Assertion.NotNull(stream);

      return bufferSize != null ? new BufferedStream(stream, bufferSize.Value) : new BufferedStream(stream);
    }
  }
}