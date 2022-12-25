namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for binary readers/writers.</para>
/// </summary>
/// <seealso cref="BinaryReader"/>
/// <seealso cref="BinaryWriter"/>
public static class BinaryExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsStart(this BinaryReader reader) => reader.BaseStream.IsStart();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static bool IsStart(this BinaryWriter writer) => writer.BaseStream.IsStart();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsEnd(this BinaryReader reader) => reader.BaseStream.IsEnd();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static bool IsEnd(this BinaryWriter writer) => writer.BaseStream.IsEnd();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsEmpty(this BinaryReader reader) => reader.BaseStream.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static bool IsEmpty(this BinaryWriter writer) => writer.BaseStream.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static BinaryReader Empty(this BinaryReader reader)
  {
    reader.BaseStream.Empty();
    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static BinaryWriter Empty(this BinaryWriter writer)
  {
    writer.BaseStream.Empty();
    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static BinaryReader Skip(this BinaryReader reader, int count)
  {
    count.Times(() => reader.ReadByte());
    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static BinaryReader Rewind(this BinaryReader reader)
  {
    reader.BaseStream.MoveToStart();
    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static BinaryWriter Rewind(this BinaryWriter writer)
  {
    writer.BaseStream.MoveToStart();
    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> Bytes(this BinaryReader reader, CancellationToken cancellation = default) => reader.BaseStream.Bytes(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<BinaryWriter> Bytes(this BinaryWriter writer, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    await writer.BaseStream.Bytes(bytes, cancellation);
    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <seealso cref="Text(BinaryWriter, string)"/>
  public static string Text(this BinaryReader reader) => reader.ReadString();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <seealso cref="Text(BinaryReader)"/>
  public static BinaryWriter Text(this BinaryWriter writer, string text)
  {
    writer.Write(text);

    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static BinaryWriter Print(this BinaryWriter destination, object instance)
  {
    destination.Write(instance.ToStringState());

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static BinaryReader UseTemporarily(this BinaryReader reader, Action<BinaryReader> action) => reader.UseFinally(action, reader => reader.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static BinaryWriter UseTemporarily(this BinaryWriter writer, Action<BinaryWriter> action) => writer.UseFinally(action, writer => writer.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <seealso cref="ToEnumerable(BinaryReader,int)"/>
  public static IEnumerable<byte> ToEnumerable(this BinaryReader reader) => reader.BaseStream.ToEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<byte[]> ToEnumerable(this BinaryReader reader, int count) => reader.BaseStream.ToEnumerable(count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this BinaryReader reader) => reader.BaseStream.ToAsyncEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this BinaryReader reader, int count) => reader.BaseStream.ToAsyncEnumerable(count);
}