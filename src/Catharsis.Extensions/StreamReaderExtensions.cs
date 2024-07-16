namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="StreamReader"/>
public static class StreamReaderExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="StreamReader"/>, which will read data from the same underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="reader">Stream reader instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
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
  /// <seealso cref="IsEmpty(StreamReader)"/>
  public static bool IsUnset(this StreamReader reader) => reader is null || reader.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="StreamReader"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="reader">Stream reader instance for evaluation.</param>
  /// <returns>If the specified <paramref name="reader"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(StreamReader)"/>
  public static bool IsEmpty(this StreamReader reader) => reader?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader">Stream reader to be cleared.</param>
  /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
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
  /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static StreamReader Rewind(this StreamReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    reader.BaseStream.MoveToStart();

    return reader;
  }
}