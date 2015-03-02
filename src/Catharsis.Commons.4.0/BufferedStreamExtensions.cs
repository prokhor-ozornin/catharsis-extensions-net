using System;
using System.IO;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Stream"/>.</para>
  /// </summary>
  /// <seealso cref="Stream"/>
  public static class BufferedStreamExtensions
  {
    /// <summary>
    ///   <para>Creates a buffered version of <see cref="Stream"/> from specified one.</para>
    /// </summary>
    /// <param name="self">Original stream that should be buffered.</param>
    /// <param name="bufferSize">Size of buffer in bytes. If not specified, default buffer size will be used.</param>
    /// <returns>Buffer version of stream that wraps original <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BufferedStream"/>
    public static BufferedStream Buffered(this Stream self, int? bufferSize = null)
    {
      Assertion.NotNull(self);

      return bufferSize != null ? new BufferedStream(self, bufferSize.Value) : new BufferedStream(self);
    }
  }
}