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
  /// <param name="count"></param>
  /// <returns></returns>
  public static BinaryReader Skip(this BinaryReader reader, int count)
  {
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
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static T Print<T>(this T instance, BinaryWriter destination)
  {
    destination.WriteText(instance.ToStateString());
    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static BinaryReader TryFinallyClear(this BinaryReader reader, Action<BinaryReader> action) => reader.TryFinally(action, reader => reader.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static BinaryWriter TryFinallyClear(this BinaryWriter writer, Action<BinaryWriter> action) => writer.TryFinally(action, writer => writer.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
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

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToBytes(this BinaryReader reader, CancellationToken cancellation = default) => reader.BaseStream.ToBytes(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static string ToText(this BinaryReader reader)
  {
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
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  public static BinaryWriter WriteBytes(this BinaryWriter destination, IEnumerable<byte> bytes)
  {
    destination.Write(bytes.AsArray());
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static BinaryWriter WriteText(this BinaryWriter destination, string text)
  {
    destination.Write(text);
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, BinaryWriter destination)
  {
    destination.WriteBytes(bytes);
    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, BinaryWriter destination)
  {
    destination.WriteText(text);
    return text;
  }

}