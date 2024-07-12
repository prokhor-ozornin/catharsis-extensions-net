namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="StreamWriter"/>
public static class StreamWriterExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="StreamWriter"/>, which will write data to the same underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="writer">Stream writer instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static StreamWriter Clone(this StreamWriter writer) => writer is not null ? new StreamWriter(writer.BaseStream, writer.Encoding) { AutoFlush = writer.AutoFlush, NewLine = writer.NewLine } : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsEmpty(StreamWriter)"/>
  public static bool IsUnset(this StreamWriter writer) => writer is null || writer.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="StreamWriter"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="writer">Stream writer instance for evaluation.</param>
  /// <returns>If the specified <paramref name="writer"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(StreamWriter)"/>
  public static bool IsEmpty(this StreamWriter writer) => writer?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static StreamWriter Empty(this StreamWriter writer)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));

    writer.BaseStream.Empty();

    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static StreamWriter Rewind(this StreamWriter writer)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));

    writer.BaseStream.MoveToStart();

    return writer;
  }
}