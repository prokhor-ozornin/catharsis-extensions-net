namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for binary writers.</para>
/// </summary>
/// <seealso cref="BinaryWriter"/>
public static class BinaryWriterExtensions
{
  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="BinaryWriter"/>, which will write data to the same underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="writer">Binary writer instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static BinaryWriter Clone(this BinaryWriter writer) => writer is not null ? new BinaryWriter(writer.BaseStream) : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsEnd(BinaryWriter)"/>
  public static bool IsStart(this BinaryWriter writer) => writer?.BaseStream.IsStart() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsStart(BinaryWriter)"/>
  public static bool IsEnd(this BinaryWriter writer) => writer?.BaseStream.IsEnd() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(BinaryWriter)"/>
  public static bool IsUnset(this BinaryWriter writer) => writer is null || writer.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="BinaryWriter"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
  /// </summary>
  /// <param name="writer">Binary writer instance for evaluation.</param>
  /// <returns>If the specified <paramref name="writer"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(BinaryWriter)"/>
  public static bool IsEmpty(this BinaryWriter writer) => writer?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para>"Empties" a specified <seealso cref="BinaryWriter"/> by setting the length of its underlying <seealso cref="Stream"/> to zero.</para>
  /// </summary>
  /// <param name="writer">Binary writer to be cleared.</param>
  /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static BinaryWriter Empty(this BinaryWriter writer)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));

    writer.BaseStream.Empty();
    
    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
  public static BinaryWriter Rewind(this BinaryWriter writer)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));

    writer.BaseStream.MoveToStart();
    
    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static BinaryWriter TryFinallyClear(this BinaryWriter writer, Action<BinaryWriter> action)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return writer.TryFinally(action, x => x.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns>Back self-reference to the given <paramref name="destination"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteText(BinaryWriter, string)"/>
  public static BinaryWriter WriteBytes(this BinaryWriter destination, IEnumerable<byte> bytes)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    destination.Write(bytes.AsArray());
   
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns>Back self-reference to the given <paramref name="destination"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytes(BinaryWriter, IEnumerable{byte})"/>
  public static BinaryWriter WriteText(this BinaryWriter destination, string text)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    destination.Write(text);

    return destination;
  }
}