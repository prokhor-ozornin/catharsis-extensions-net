using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="TextReader"/>
/// <seealso cref="TextWriter"/>
/// <seealso cref="StreamReader"/>
/// <seealso cref="StreamWriter"/>
/// <seealso cref="StringBuilder"/>
public static class TextExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsStart(this StreamReader reader) => reader is not null ? reader.BaseStream.IsStart() : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsEnd(this TextReader reader) => reader is not null ? reader.Peek() < 0 : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsEmpty(this StreamReader reader) => reader is not null ? reader.BaseStream.IsEmpty() : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static bool IsEmpty(this StreamWriter writer) => writer is not null ? writer.BaseStream.IsEmpty() : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static bool IsEmpty(this StringBuilder builder) => builder is not null ? builder.Length <= 0 : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static StreamReader Empty(this StreamReader reader)
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
  public static StreamWriter Empty(this StreamWriter writer)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));

    writer.BaseStream.Empty();

    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static StringBuilder Empty(this StringBuilder builder) => builder is not null ? builder.Clear() : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static StringBuilder Min(this StringBuilder left, StringBuilder right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length <= right.Length ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static StringBuilder Max(this StringBuilder left, StringBuilder right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length >= right.Length ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static StreamReader Rewind(this StreamReader reader)
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
  public static StreamWriter Rewind(this StreamWriter writer)
  {
    if (writer is null) throw new ArgumentNullException(nameof(writer));

    writer.BaseStream.MoveToStart();

    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IEnumerable<string> Lines(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    for (string line; (line = reader.ReadLine()) != null;)
    {
      yield return line;
    }
  }

  /// <summary>
  ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a list of strings, using default system-dependent string separator.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
  /// <returns>List of strings which have been read from a <paramref name="reader"/>.</returns>
  public static async IAsyncEnumerable<string> LinesAsync(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    for (string line; (line = await reader.ReadLineAsync()) != null;)
    {
      yield return line;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TReader"></typeparam>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static TReader Skip<TReader>(this TReader reader, int count) where TReader : TextReader
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    count.Times(() => reader.Read());

    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="character"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static string Repeat(this char character, int count) => count >= 0 ? new string(character, count) : throw new ArgumentOutOfRangeException(nameof(count));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static T Print<T>(this T instance, TextWriter destination)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.Write(instance.ToStateString());

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> PrintAsync<T>(this T instance, TextWriter destination, CancellationToken cancellation = default)
  {
    if (instance is null) throw new ArgumentNullException(nameof(instance));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteAsync(instance.ToStateString().ToReadOnlyMemory(), cancellation).ConfigureAwait(false);

    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static StringBuilder TryFinallyClear(this StringBuilder builder, Action<StringBuilder> action)
  {
    if (builder is null) throw new ArgumentNullException(nameof(builder));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return builder.TryFinally(action, builder => builder.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static TextReader AsSynchronized(this TextReader reader) => reader is not null ? TextReader.Synchronized(reader) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static TextWriter AsSynchronized(this TextWriter writer) => writer is not null ? TextWriter.Synchronized(writer) : throw new ArgumentNullException(nameof(writer));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this TextReader reader, Encoding encoding = null) => reader.ToText().ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<byte[]> ToBytesAsync(this TextReader reader, Encoding encoding = null) => (await reader.ToTextAsync().ConfigureAwait(false)).ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static string ToText(this TextReader reader) => reader is not null ? reader.ReadToEnd() : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a string.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
  /// <returns>Text content which have been read from a <paramref name="reader"/>.</returns>
  public static async Task<string> ToTextAsync(this TextReader reader) => reader is not null ? await reader.ReadToEndAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IEnumerable<char> ToEnumerable(this TextReader reader) => reader.ToEnumerable(4096).SelectMany(chars => chars);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<char[]> ToEnumerable(this TextReader reader, int count)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

    return new TextReaderEnumerable(reader, count);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<char> ToAsyncEnumerable(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    await foreach (var elements in reader.ToAsyncEnumerable(4096).ConfigureAwait(false))
    {
      foreach (var element in elements)
      {
        yield return element;
      }
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<char[]> ToAsyncEnumerable(this TextReader reader, int count)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

    return new TextReaderAsyncEnumerable(reader, count);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static StringWriter ToStringWriter(this StringBuilder builder, IFormatProvider format = null) => builder is not null ? new StringWriter(builder, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static XmlWriter ToXmlWriter(this StringBuilder builder) => builder is not null ? XmlWriter.Create(builder, new XmlWriterSettings { Indent = true }) : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static TWriter WriteBytes<TWriter>(this TWriter destination, IEnumerable<byte> bytes, Encoding encoding = null) where TWriter : TextWriter
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return destination.WriteText(bytes.AsArray().ToText(encoding));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TWriter> WriteBytesAsync<TWriter>(this TWriter destination, IEnumerable<byte> bytes, Encoding encoding = null, CancellationToken cancellation = default) where TWriter : TextWriter
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    return await destination.WriteTextAsync(bytes.AsArray().ToText(encoding), cancellation).ConfigureAwait(false);
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static TWriter WriteText<TWriter>(this TWriter destination, string text) where TWriter : TextWriter
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    destination.Write(text);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TWriter> WriteTextAsync<TWriter>(this TWriter destination, string text, CancellationToken cancellation = default) where TWriter : TextWriter
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteAsync(text.ToReadOnlyMemory(), cancellation).ConfigureAwait(false);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, TextWriter destination, Encoding encoding = null)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    bytes.AsArray().ToText(encoding).WriteTo(destination);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, TextWriter destination, Encoding encoding = null)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await bytes.AsArray().ToText(encoding).WriteToAsync(destination).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, TextWriter destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteToAsync(this string text, TextWriter destination, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteTextAsync(text, cancellation).ConfigureAwait(false);

    return text;
  }

  private sealed class TextReaderEnumerable : IEnumerable<char[]>
  {
    private readonly TextReader reader;
    private readonly int count;

    public TextReaderEnumerable(TextReader reader, int count)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
      this.count = count;
    }

    public IEnumerator<char[]> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<char[]>
    {
      private readonly TextReaderEnumerable parent;
      private readonly char[] buffer;

      public Enumerator(TextReaderEnumerable parent)
      {
        this.parent = parent ?? throw new ArgumentOutOfRangeException(nameof(parent));
        buffer = new char[parent.count];
      }

      public char[] Current { get; private set; } = Array.Empty<char>();

      public bool MoveNext()
      {
        var count = parent.reader.Read(buffer, 0, parent.count);

        if (count > 0)
        {
          Current = buffer.Range(0, count);
        }

        return count > 0;
      }

      public void Reset() { throw new InvalidOperationException(); }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }

  private sealed class TextReaderAsyncEnumerable : IAsyncEnumerable<char[]>
  {
    private readonly TextReader reader;
    private readonly int count;

    public TextReaderAsyncEnumerable(TextReader reader, int count)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
      this.count = count;
    }

    public IAsyncEnumerator<char[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new Enumerator(this);

    private sealed class Enumerator : IAsyncEnumerator<char[]>
    {
      private readonly TextReaderAsyncEnumerable parent;
      private readonly char[] buffer;

      public Enumerator(TextReaderAsyncEnumerable parent)
      {
        this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        buffer = new char[parent.count];
      }

      public ValueTask DisposeAsync() => default;

      public char[] Current { get; private set; } = Array.Empty<char>();

      public async ValueTask<bool> MoveNextAsync()
      {
        var count = await parent.reader.ReadAsync(buffer, 0, parent.count).ConfigureAwait(false);

        if (count > 0)
        {
          Current = buffer.Range(0, count);
        }

        return count > 0;
      }
    }
  }
}