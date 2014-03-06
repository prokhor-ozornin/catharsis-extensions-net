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
    ///   <para></para>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="bufferSize"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is a <c>null</c> reference.</exception>
    public static BufferedStream Buffered(this Stream stream, int? bufferSize = null)
    {
      Assertion.NotNull(stream);

      return bufferSize != null ? new BufferedStream(stream, bufferSize.Value) : new BufferedStream(stream);
    }
  }
}