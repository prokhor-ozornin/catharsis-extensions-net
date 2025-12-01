namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="StreamWriter"/>
public static class StreamWriterExtensions
{
  /// <param name="writer"></param>
  extension(StreamWriter writer)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => writer is null || writer.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="StreamWriter"/> instance can be considered "empty", meaning it has an "empty" underlying <see cref="Stream"/>.</para>
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
    public StreamWriter Rewind()
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));

      writer.BaseStream.MoveToStart();

      return writer;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="writer"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    public StreamWriter Empty()
    {
      if (writer is null) throw new ArgumentNullException(nameof(writer));

      writer.BaseStream.Empty();

      return writer;
    }

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="StreamWriter"/>, which will write data to the same underlying <see cref="Stream"/>.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <see langword="null"/>.</exception>
    public StreamWriter Clone() => writer is not null ? new StreamWriter(writer.BaseStream, writer.Encoding) { AutoFlush = writer.AutoFlush, NewLine = writer.NewLine } : throw new ArgumentNullException(nameof(writer));
  }
}