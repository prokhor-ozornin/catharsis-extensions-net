namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="StreamReader"/>
public static class StreamReaderExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static StreamReader Clone(this StreamReader reader) => reader is not null ? new StreamReader(reader.BaseStream, reader.CurrentEncoding) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static bool IsStart(this StreamReader reader) => reader?.BaseStream.IsStart() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsUnset(this StreamReader reader) => reader is null || reader.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static bool IsEmpty(this StreamReader reader) => reader?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static StreamReader Empty(this StreamReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    reader.BaseStream.Empty();

    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static StreamReader Rewind(this StreamReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    reader.BaseStream.MoveToStart();

    return reader;
  }
}