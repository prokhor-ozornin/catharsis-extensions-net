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
  /// <param name="character"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static string Repeat(this char character, int count) => new(character, count);

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
  public static TextReader Synchronized(this TextReader reader) => TextReader.Synchronized(reader);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="writer"></param>
  /// <returns></returns>
  public static TextWriter Synchronized(this TextWriter writer) => TextWriter.Synchronized(writer);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static async Task<byte[]> Bytes(this TextReader reader, Encoding? encoding = null) => (await reader.Text()).Bytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="to"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TWriter> Bytes<TWriter>(this TWriter to, IEnumerable<byte> bytes, Encoding? encoding = null, CancellationToken cancellation = default) where TWriter : TextWriter => await to.Text(bytes.AsArray().Text(encoding), cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static byte[] Bytes(this StringBuilder builder, Encoding? encoding = null) => builder.ToString().Bytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="bytes"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static StringBuilder Bytes(this StringBuilder builder, IEnumerable<byte> bytes, Encoding? encoding = null) => builder.Text(bytes.AsArray().Text(encoding));

  /// <summary>
  ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a string.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
  /// <returns>Text content which have been read from a <paramref name="reader"/>.</returns>
  public static async Task<string> Text(this TextReader reader) => await reader.ReadToEndAsync();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="writer"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TWriter> Text<TWriter>(this TWriter writer, string text, CancellationToken cancellation = default) where TWriter : TextWriter
  {
    await writer.WriteAsync(text.ToReadOnlyMemory(), cancellation);
    return writer;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static string Text(this StringBuilder builder) => builder.ToString();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static StringBuilder Text(this StringBuilder builder, string text) => builder.Clear().Append(text);

  /// <summary>
  ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a list of strings, using default system-dependent string separator.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
  /// <returns>List of strings which have been read from a <paramref name="reader"/>.</returns>
  public static async IAsyncEnumerable<string> Lines(this TextReader reader)
  {
    foreach (var line in (await reader.Text()).Lines())
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
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteTo(this string text, TextWriter destination, CancellationToken cancellation = default)
  {
    await destination.Text(text, cancellation);
    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, StringBuilder destination)
  {
    destination.Text(text);
    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static StringBuilder UseTemporarily(this StringBuilder builder, Action<StringBuilder> action) => builder.UseFinally(action, builder => builder.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="TWriter"></typeparam>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<TWriter> Print<TWriter>(this TWriter destination, object instance, CancellationToken cancellation = default) where TWriter : TextWriter
  {
    await destination.WriteAsync(instance.ToStringState().ToReadOnlyMemory(), cancellation);
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IEnumerable<char> ToEnumerable(this TextReader reader) => new TextReaderEnumerable(reader);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<char[]> ToEnumerable(this TextReader reader, int count) => new TextReaderChunkEnumerable(reader, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static IEnumerable<char> ToEnumerable(this StringBuilder builder) => new StringBuilderEnumerable(builder);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<char[]> ToEnumerable(this StringBuilder builder, int count) => new StringBuilderChunkEnumerable(builder, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<char> ToAsyncEnumerable(this TextReader reader) => new TextReaderAsyncEnumerable(reader);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<char[]> ToAsyncEnumerable(this TextReader reader, int count) => new TextReaderChunkAsyncEnumerable(reader, count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static StringWriter ToStringWriter(this StringBuilder builder, IFormatProvider? format = null) => new(builder, format ?? CultureInfo.InvariantCulture);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  public static XmlWriter ToXmlWriter(this StringBuilder builder) => XmlWriter.Create(builder);

  private sealed class TextReaderEnumerable : IEnumerable<char>
  {
    private readonly TextReader reader;

    public TextReaderEnumerable(TextReader reader) => this.reader = reader;

    public IEnumerator<char> GetEnumerator() => new TextReaderEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class TextReaderEnumerator : IEnumerator<char>
    {
      private readonly TextReaderEnumerable parent;

      public TextReaderEnumerator(TextReaderEnumerable parent) => this.parent = parent;

      public char Current { get; private set; }

      public bool MoveNext()
      {
        if (parent.reader.Peek() < 0)
        {
          return false;
        }

        var result = parent.reader.Read();

        if (result >= 0)
        {
          Current = (char) result;
        }

        return result >= 0;
      }

      public void Reset() { throw new InvalidOperationException(); }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }

  private sealed class TextReaderChunkEnumerable : IEnumerable<char[]>
  {
    private readonly TextReader reader;
    private readonly int count;

    public TextReaderChunkEnumerable(TextReader reader, int count)
    {
      this.reader = reader;
      this.count = count;
    }

    public IEnumerator<char[]> GetEnumerator() => new TextReaderChunkEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class TextReaderChunkEnumerator : IEnumerator<char[]>
    {
      private readonly TextReaderChunkEnumerable parent;

      public TextReaderChunkEnumerator(TextReaderChunkEnumerable parent)
      {
        this.parent = parent;
        Current = new char[parent.count];
      }

      public char[] Current { get; }

      public bool MoveNext()
      {
        if (parent.reader.Peek() < 0)
        {
          return false;
        }

        return parent.reader.Read(Current, 0, parent.count) > 0;
      }

      public void Reset() { throw new InvalidOperationException(); }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }

  private sealed class TextReaderAsyncEnumerable : IAsyncEnumerable<char>
  {
    private readonly TextReader reader;

    public TextReaderAsyncEnumerable(TextReader reader) => this.reader = reader;

    public IAsyncEnumerator<char> GetAsyncEnumerator(CancellationToken cancellation = default) => new TextReaderAsyncEnumerator(this);

    private sealed class TextReaderAsyncEnumerator : IAsyncEnumerator<char>
    {
      private readonly TextReaderAsyncEnumerable parent;
      private readonly char[] buffer = new char[1];

      public TextReaderAsyncEnumerator(TextReaderAsyncEnumerable parent) => this.parent = parent;

      public ValueTask DisposeAsync() => default;

      public char Current { get; private set; }

      public async ValueTask<bool> MoveNextAsync()
      {
        if (parent.reader.Peek() < 0)
        {
          return false;
        }

        var result = await parent.reader.ReadAsync(buffer, 0, 1);

        if (result <= 0)
        {
          return false;
        }

        Current = buffer[0];

        return true;
      }
    }
  }

  private sealed class TextReaderChunkAsyncEnumerable : IAsyncEnumerable<char[]>
  {
    private readonly TextReader reader;
    private readonly int count;

    public TextReaderChunkAsyncEnumerable(TextReader reader, int count)
    {
      this.reader = reader;
      this.count = count;
    }

    public IAsyncEnumerator<char[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new TextReaderChunkAsyncEnumerator(this);

    private sealed class TextReaderChunkAsyncEnumerator : IAsyncEnumerator<char[]>
    {
      private readonly TextReaderChunkAsyncEnumerable parent;

      public TextReaderChunkAsyncEnumerator(TextReaderChunkAsyncEnumerable parent)
      {
        this.parent = parent;
        Current = new char[parent.count];
      }

      public ValueTask DisposeAsync() => default;

      public char[] Current { get; }

      public async ValueTask<bool> MoveNextAsync()
      {
        if (parent.reader.Peek() < 0)
        {
          return false;
        }

        var result = await parent.reader.ReadAsync(Current, 0, parent.count);

        return result > 0;
      }
    }
  }

  private sealed class StringBuilderEnumerable : IEnumerable<char>
  {
    private readonly StringBuilder builder;

    public StringBuilderEnumerable(StringBuilder builder) => this.builder = builder;

    public IEnumerator<char> GetEnumerator() => new StringBuilderEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class StringBuilderEnumerator : IEnumerator<char>
    {
      private readonly StringBuilderEnumerable parent;
      private int index = -1;

      public StringBuilderEnumerator(StringBuilderEnumerable parent) => this.parent = parent;

      public char Current => parent.builder[index];

      public bool MoveNext()
      {
        index++;
        return index < parent.builder.Length;
      }

      public void Reset() { index = -1; }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }

  private sealed class StringBuilderChunkEnumerable : IEnumerable<char[]>
  {
    private readonly StringBuilder builder;
    private readonly int count;

    public StringBuilderChunkEnumerable(StringBuilder builder, int count)
    {
      this.builder = builder;
      this.count = count;
    }

    public IEnumerator<char[]> GetEnumerator() => new StringBuilderChunkEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class StringBuilderChunkEnumerator : IEnumerator<char[]>
    {
      private readonly StringBuilderChunkEnumerable parent;
      private int index = -1;

      public StringBuilderChunkEnumerator(StringBuilderChunkEnumerable parent)
      {
        this.parent = parent;

        Current = new char[parent.count];
      }

      public char[] Current { get; }

      public bool MoveNext()
      {
        index++;

        if (index * parent.count >= parent.builder.Length)
        {
          return false;
        }

        parent.builder.CopyTo(index * parent.count, Current, 0, parent.count);

        return true;
      }

      public void Reset() { index = -1; }

      public void Dispose() {}

      object IEnumerator.Current => Current;
    }
  }
}