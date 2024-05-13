namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="StreamWriter"/>
public static class StreamWriterExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static StreamWriter Clone(this StreamWriter writer) => writer is not null ? new StreamWriter(writer.BaseStream, writer.Encoding) { AutoFlush = writer.AutoFlush, NewLine = writer.NewLine } : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this StreamWriter writer) => writer?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  public static StreamWriter Rewind(this StreamWriter writer)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));

    writer.BaseStream.MoveToStart();

    return writer;
  }
}