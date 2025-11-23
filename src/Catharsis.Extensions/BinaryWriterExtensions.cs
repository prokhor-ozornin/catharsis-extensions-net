namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for binary writers.</para>
/// </summary>
/// <seealso cref="BinaryWriter"/>
public static class BinaryWriterExtensions
{
  /// <param name="writer"></param>
  extension(BinaryWriter writer)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEnd"/>
    public bool IsStart => writer?.BaseStream.IsStart ?? throw new ArgumentNullException(nameof(writer));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsStart"/>
    public bool IsEnd => writer?.BaseStream.IsEnd ?? throw new ArgumentNullException(nameof(writer));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => writer is null || writer.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="BinaryWriter"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <value>If the specified <paramref name="writer"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset"/>
    public bool IsEmpty => writer?.BaseStream.IsEmpty ?? throw new ArgumentNullException(nameof(writer));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    public BinaryWriter Rewind()
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));

      writer.BaseStream.MoveToStart();

      return writer;
    }

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="BinaryWriter"/>, which will write data to the same underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    public BinaryWriter Clone() => writer is not null ? new BinaryWriter(writer.BaseStream) : throw new ArgumentNullException(nameof(writer));

    /// <summary>
    ///   <para>"Empties" a specified <seealso cref="BinaryWriter"/> by setting the length of its underlying <seealso cref="Stream"/> to zero.</para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    public BinaryWriter Empty()
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));

      writer.BaseStream.Empty();

      return writer;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public BinaryWriter TryFinallyClear(Action<BinaryWriter> action)
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return writer.TryFinally(action, x => x.Empty());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText(BinaryWriter, string)"/>
    public BinaryWriter WriteBytes(IEnumerable<byte> bytes)
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      writer.Write(bytes.AsArray());
   
      return writer;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="writer"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes(BinaryWriter, IEnumerable{byte})"/>
    public BinaryWriter WriteText(string text)
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));
      if (text is null) throw new ArgumentNullException(nameof(text));

      writer.Write(text);

      return writer;
    }
  }
}