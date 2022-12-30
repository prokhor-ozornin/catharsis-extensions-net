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
  public static bool IsStart(this StreamReader reader) => reader.BaseStream.IsStart();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsEnd(this TextReader reader) => reader.Peek() < 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static bool IsEmpty(this StreamReader reader) => reader.BaseStream.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static bool IsEmpty(this StreamWriter writer) => writer.BaseStream.IsEmpty();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static bool IsEmpty(this StringBuilder builder) => builder.Length <= 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static StreamReader Empty(this StreamReader reader)
  {
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
    writer.BaseStream.Empty();
    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static StringBuilder Empty(this StringBuilder builder) => builder.Clear();
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static StringBuilder Min(this StringBuilder left, StringBuilder right) => left.Length <= right.Length ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static StringBuilder Max(this StringBuilder left, StringBuilder right) => left.Length >= right.Length ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static StreamReader Rewind(this StreamReader reader)
  {
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
    writer.BaseStream.MoveToStart();
    return writer;
  }

  /// <summary>
  ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a list of strings, using default system-dependent string separator.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
  /// <returns>List of strings which have been read from a <paramref name="reader"/>.</returns>
  public static async IAsyncEnumerable<string> Lines(this TextReader reader)
  {
    foreach (var line in (await reader.ToText().ConfigureAwait(false)).Lines())
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
    count.Times(() => reader.Read());
    return reader;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="character"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static string Repeat(this char character, int count) => new(character, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> Print<T>(this T instance, TextWriter destination, CancellationToken cancellation = default)
  {
    await destination.WriteAsync(instance.ToStateString().ToReadOnlyMemory(), cancellation).ConfigureAwait(false);
    return instance;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static StringBuilder TryFinallyClear(this StringBuilder builder, Action<StringBuilder> action) => builder.TryFinally(action, builder => builder.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static TextReader AsSynchronized(this TextReader reader) => TextReader.Synchronized(reader);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static TextWriter AsSynchronized(this TextWriter writer) => TextWriter.Synchronized(writer);
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<byte[]> ToBytes(this TextReader reader, Encoding encoding = null) => (await reader.ToText().ConfigureAwait(false)).ToBytes(encoding);

  /// <summary>
  ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a string.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
  /// <returns>Text content which have been read from a <paramref name="reader"/>.</returns>
  public static async Task<string> ToText(this TextReader reader) => await reader.ReadToEndAsync().ConfigureAwait(false);

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
  public static IEnumerable<char[]> ToEnumerable(this TextReader reader, int count) => new TextReaderEnumerable(reader, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static async IAsyncEnumerable<char> ToAsyncEnumerable(this TextReader reader)
  {
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
  public static IAsyncEnumerable<char[]> ToAsyncEnumerable(this TextReader reader, int count) => new TextReaderAsyncEnumerable(reader, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static StringWriter ToStringWriter(this StringBuilder builder, IFormatProvider format = null) => new(builder, format ?? CultureInfo.InvariantCulture);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static XmlWriter ToXmlWriter(this StringBuilder builder) => XmlWriter.Create(builder);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<TWriter> WriteBytes<TWriter>(this TWriter destination, IEnumerable<byte> bytes, Encoding encoding = null) where TWriter : TextWriter => await destination.WriteText(bytes.AsArray().ToText(encoding)).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TWriter> WriteText<TWriter>(this TWriter destination, string text, CancellationToken cancellation = default) where TWriter : TextWriter
  {
    await destination.WriteAsync(text.ToReadOnlyMemory(), cancellation).ConfigureAwait(false);

    return destination;
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="writer"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteTo(this IEnumerable<byte> bytes, TextWriter writer, Encoding encoding = null)
  {
    await bytes.AsArray().ToText(encoding).WriteTo(writer).ConfigureAwait(false);
    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteTo(this string text, TextWriter destination, CancellationToken cancellation = default)
  {
    await destination.WriteText(text, cancellation).ConfigureAwait(false);
    return text;
  }

  private sealed class TextReaderEnumerable : IEnumerable<char[]>
  {
    private readonly TextReader reader;
    private readonly int count;

    public TextReaderEnumerable(TextReader reader, int count)
    {
      this.reader = reader;
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
        this.parent = parent;
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
      this.reader = reader;
      this.count = count;
    }

    public IAsyncEnumerator<char[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new Enumerator(this);

    private sealed class Enumerator : IAsyncEnumerator<char[]>
    {
      private readonly TextReaderAsyncEnumerable parent;
      private readonly char[] buffer;

      public Enumerator(TextReaderAsyncEnumerable parent)
      {
        this.parent = parent;
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