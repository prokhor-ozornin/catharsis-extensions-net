namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for binary readers.</para>
/// </summary>
/// <seealso cref="BinaryReader"/>
public static class BinaryReaderExtensions
{
  /// <param name="reader">Binary reader instance for evaluation.</param>
  extension(BinaryReader reader)
  {
    /// <summary>
    ///   <para>Determines whether the specified <seealso cref="BinaryReader"/> is currently at the starting position, meaning the position within it's underlying <seealso cref="Stream"/> is zero.</para>
    /// </summary>
    /// <value>If the specified <paramref name="reader"/> is at the starting position, return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEnd(BinaryReader)"/>
    public bool IsStart => reader?.BaseStream.IsStart() ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para>Determines whether the specified <seealso cref="BinaryReader"/> is currently at the final position, meaning it's currently at the end of its underlying <seealso cref="Stream"/>.</para>
    /// </summary>
    /// <value>If the specified <paramref name="reader"/> is at the final position, return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsStart(BinaryReader)"/>
    public bool IsEnd => reader?.BaseStream.IsEnd() ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public BinaryReader Rewind()
    {
      if (reader is null)
        throw new ArgumentNullException(nameof(reader));

      reader.BaseStream.MoveToStart();

      return reader;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public BinaryReader Skip(int count)
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
    ///   <para>Determines whether the specified <see cref="BinaryReader"/> instance is either <see langword="null"/> or "empty".</para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEmpty(BinaryReader)"/>
    public bool IsUnset => reader is null || reader.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="BinaryReader"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <value>If the specified <paramref name="reader"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset(BinaryReader)"/>
    public bool IsEmpty => reader?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="BinaryReader"/>, which will read data from the same underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public BinaryReader Clone() => reader is not null ? new BinaryReader(reader.BaseStream) : throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para>"Empties" a specified <seealso cref="BinaryReader"/> by setting the length of its underlying <seealso cref="Stream"/> to zero.</para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public BinaryReader Empty()
    {
      if (reader is null)
        throw new ArgumentNullException(nameof(reader));

      reader.BaseStream.Empty();

      return reader;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="reader"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public BinaryReader TryFinallyClear(Action<BinaryReader> action)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return reader.TryFinally(action, x => x.Empty());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(BinaryReader, int)"/>
    /// <seealso cref="ToAsyncEnumerable(BinaryReader, int)"/>
    public IEnumerable<byte> ToEnumerable() => reader?.BaseStream.ToEnumerable() ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(BinaryReader)"/>
    /// <seealso cref="ToAsyncEnumerable(BinaryReader)"/>
    public IEnumerable<byte[]> ToEnumerable(int count) => reader?.BaseStream.ToEnumerable(count) ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToAsyncEnumerable(BinaryReader, int)"/>
    /// <seealso cref="ToEnumerable(BinaryReader, int)"/>
    public IAsyncEnumerable<byte> ToAsyncEnumerable() => reader?.BaseStream.ToAsyncEnumerable() ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToAsyncEnumerable(BinaryReader)"/>
    /// <seealso cref="ToEnumerable(BinaryReader)"/>
    public IAsyncEnumerable<byte[]> ToAsyncEnumerable(int count) => reader?.BaseStream.ToAsyncEnumerable(count) ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(BinaryReader)"/>
    public IEnumerable<byte> ToBytes() => reader?.BaseStream.ToBytes() ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(BinaryReader)"/>
    public IAsyncEnumerable<byte> ToBytesAsync() => reader?.BaseStream.ToBytesAsync() ?? throw new ArgumentNullException(nameof(reader));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public byte[] Bytes => reader.ToBytes().ToArray();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public string ToText()
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
    ///   <para>[NEW]</para>
    /// </summary>
    public string Text => reader.ToText();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => reader is not null && reader.PeekChar() >= 0;
  }
}