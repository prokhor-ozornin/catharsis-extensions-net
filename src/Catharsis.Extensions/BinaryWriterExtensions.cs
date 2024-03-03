namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for binary writers.</para>
/// </summary>
/// <seealso cref="BinaryWriter"/>
public static class BinaryWriterExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static BinaryWriter Clone(this BinaryWriter writer) => writer is not null ? new BinaryWriter(writer.BaseStream) : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsStart(this BinaryWriter writer) => writer?.BaseStream.IsStart() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEnd(this BinaryWriter writer) => writer?.BaseStream.IsEnd() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this BinaryWriter writer) => writer?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static BinaryWriter WriteText(this BinaryWriter destination, string text)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    destination.Write(text);

    return destination;
  }
}