namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for binary readers.</para>
/// </summary>
/// <seealso cref="BinaryReader"/>
public static class BinaryReaderExtensions
{
  /// <summary>
  ///   <para>Determines whether the specified <seealso cref="BinaryReader"/> is currently at the starting position, meaning the position within it's underlying <seealso cref="Stream"/> is zero.</para>
  /// </summary>
  /// <param name="reader">Binary reader instance for evaluation.</param>
  /// <returns>If the specified <paramref name="reader"/> is at the starting position, return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsEnd(BinaryReader)"/>
  public static bool IsStart(this BinaryReader reader) => reader?.BaseStream.IsStart() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para>Determines whether the specified <seealso cref="BinaryReader"/> is currently at the final position, meaning it's currently at the end of its underlying <seealso cref="Stream"/>.</para>
  /// </summary>
  /// <param name="reader">Binary reader instance for evaluation.</param>
  /// <returns>If the specified <paramref name="reader"/> is at the final position, return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsStart(BinaryReader)"/>
  public static bool IsEnd(this BinaryReader reader) => reader?.BaseStream.IsEnd() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static BinaryReader Rewind(this BinaryReader reader)
  {
    if (reader is null)
      throw new ArgumentNullException(nameof(reader));

    reader.BaseStream.MoveToStart();

    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static BinaryReader Skip(this BinaryReader reader, int count)
  {
    if (reader is null)
      throw new ArgumentNullException(nameof(reader));
    if (count < 0)
      throw new ArgumentOutOfRangeException(nameof(count));

    try
    {
      count.Times(() => reader.ReadByte());
    }
    catch (EndOfStreamException)
    {
    }

    return reader;
  }

  /// <summary>
  ///   <para>Determines whether the specified <see cref="BinaryReader"/> instance is either <see langword="null"/> or "empty".</para>
  /// </summary>
  /// <param name="reader"></param>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsEmpty(BinaryReader)"/>
  public static bool IsUnset(this BinaryReader reader) => reader is null || reader.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="BinaryReader"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="reader">Binary reader instance for evaluation.</param>
  /// <returns>If the specified <paramref name="reader"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(BinaryReader)"/>
  public static bool IsEmpty(this BinaryReader reader) => reader?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="BinaryReader"/>, which will read data from the same underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="reader">Binary reader instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static BinaryReader Clone(this BinaryReader reader) => reader is not null ? new BinaryReader(reader.BaseStream) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para>"Empties" a specified <seealso cref="BinaryReader"/> by setting the length of its underlying <seealso cref="Stream"/> to zero.</para>
  /// </summary>
  /// <param name="reader">Binary reader to be cleared.</param>
  /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static BinaryReader Empty(this BinaryReader reader)
  {
    if (reader is null)
      throw new ArgumentNullException(nameof(reader));

    reader.BaseStream.Empty();

    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="reader"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static BinaryReader TryFinallyClear(this BinaryReader reader, Action<BinaryReader> action)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return reader.TryFinally(action, x => x.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToEnumerable(BinaryReader, int)"/>
  /// <seealso cref="ToAsyncEnumerable(BinaryReader, int)"/>
  public static IEnumerable<byte> ToEnumerable(this BinaryReader reader) => reader?.BaseStream.ToEnumerable() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToEnumerable(BinaryReader)"/>
  /// <seealso cref="ToAsyncEnumerable(BinaryReader)"/>
  public static IEnumerable<byte[]> ToEnumerable(this BinaryReader reader, int count) => reader?.BaseStream.ToEnumerable(count) ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToAsyncEnumerable(BinaryReader, int)"/>
  /// <seealso cref="ToEnumerable(BinaryReader, int)"/>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this BinaryReader reader) => reader?.BaseStream.ToAsyncEnumerable() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToAsyncEnumerable(BinaryReader)"/>
  /// <seealso cref="ToEnumerable(BinaryReader)"/>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this BinaryReader reader, int count) => reader?.BaseStream.ToAsyncEnumerable(count) ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytesAsync(BinaryReader)"/>
  public static IEnumerable<byte> ToBytes(this BinaryReader reader) => reader?.BaseStream.ToBytes() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytes(BinaryReader)"/>
  public static IAsyncEnumerable<byte> ToBytesAsync(this BinaryReader reader) => reader?.BaseStream.ToBytesAsync() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static string ToText(this BinaryReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    try
    {
      return reader.ReadString();
    }
    catch (EndOfStreamException)
    {
      return string.Empty;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool ToBoolean(this BinaryReader reader) => reader is not null && reader.PeekChar() >= 0;
}