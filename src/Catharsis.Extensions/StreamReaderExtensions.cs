namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="StreamReader"/>
public static class StreamReaderExtensions
{
  /// <param name="reader"></param>
  extension(StreamReader reader)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public bool IsStart => reader?.BaseStream.IsStart ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public StreamReader Rewind()
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      reader.BaseStream.MoveToStart();

      return reader;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => reader is null || reader.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="StreamReader"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <value>If the specified <paramref name="reader"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset"/>
    public bool IsEmpty => reader?.BaseStream.IsEmpty ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public StreamReader Empty()
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      reader.BaseStream.Empty();

      return reader;
    }

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="StreamReader"/>, which will read data from the same underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public StreamReader Clone() => reader is not null ? new StreamReader(reader.BaseStream, reader.CurrentEncoding) : throw new ArgumentNullException(nameof(reader));
  }
}