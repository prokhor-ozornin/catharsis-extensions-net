namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for binary readers.</para>
/// </summary>
/// <seealso cref="BinaryReader"/>
public static class BinaryReaderExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static BinaryReader Clone(this BinaryReader reader) => reader is not null ? new BinaryReader(reader.BaseStream) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static bool IsStart(this BinaryReader reader) => reader?.BaseStream.IsStart() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static bool IsEnd(this BinaryReader reader) => reader?.BaseStream.IsEnd() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static bool IsUnset(this BinaryReader reader) => reader is null || reader.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static bool IsEmpty(this BinaryReader reader) => reader?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static BinaryReader Empty(this BinaryReader reader)
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
  public static BinaryReader Rewind(this BinaryReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    reader.BaseStream.MoveToStart();
    
    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static BinaryReader Skip(this BinaryReader reader, int count)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

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
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="action"></param>
  /// <returns></returns>
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
  public static IEnumerable<byte> ToEnumerable(this BinaryReader reader) => reader?.BaseStream.ToEnumerable() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static IEnumerable<byte[]> ToEnumerable(this BinaryReader reader, int count) => reader?.BaseStream.ToEnumerable(count) ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this BinaryReader reader) => reader?.BaseStream.ToAsyncEnumerable() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this BinaryReader reader, int count) => reader?.BaseStream.ToAsyncEnumerable(count) ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static IEnumerable<byte> ToBytes(this BinaryReader reader) => reader?.BaseStream.ToBytes() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
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
}