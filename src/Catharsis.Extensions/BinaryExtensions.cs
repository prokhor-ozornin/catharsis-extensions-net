namespace Catharsis.Extensions;

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
  public static bool IsStart(this BinaryReader reader) => reader?.BaseStream.IsStart() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static bool IsStart(this BinaryWriter writer) => writer?.BaseStream.IsStart() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsEnd(this BinaryReader reader) => reader?.BaseStream.IsEnd() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static bool IsEnd(this BinaryWriter writer) => writer?.BaseStream.IsEnd() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsEmpty(this BinaryReader reader) => reader?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static bool IsEmpty(this BinaryWriter writer) => writer?.BaseStream.IsEmpty() ?? throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static BinaryReader Empty(this BinaryReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

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
    if (writer is null) throw new ArgumentNullException(nameof(writer));

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
    if (reader is null) throw new ArgumentNullException(nameof(reader));

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
    if (writer is null) throw new ArgumentNullException(nameof(writer));

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
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static T Print<T>(this T instance, BinaryWriter destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(instance.ToStateString());
    
    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static BinaryReader TryFinallyClear(this BinaryReader reader, Action<BinaryReader> action)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return reader.TryFinally(action, reader => reader.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static BinaryWriter TryFinallyClear(this BinaryWriter writer, Action<BinaryWriter> action)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return writer.TryFinally(action, writer => writer.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToEnumerable(this BinaryReader reader) => reader?.BaseStream.ToEnumerable() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<byte[]> ToEnumerable(this BinaryReader reader, int count) => reader?.BaseStream.ToEnumerable(count) ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToAsyncEnumerable(this BinaryReader reader) => reader?.BaseStream.ToAsyncEnumerable() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte[]> ToAsyncEnumerable(this BinaryReader reader, int count) => reader?.BaseStream.ToAsyncEnumerable(count) ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this BinaryReader reader) => reader?.BaseStream.ToBytes() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToBytesAsync(this BinaryReader reader) => reader?.BaseStream.ToBytesAsync() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
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
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
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
  public static BinaryWriter WriteText(this BinaryWriter destination, string text)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

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
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

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
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);
    
    return text;
  }
}