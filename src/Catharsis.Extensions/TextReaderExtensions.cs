using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="TextReader"/>
public static class TextReaderExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static bool IsEnd(this TextReader reader) => reader is not null ? reader.Peek() < 0 : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="LinesAsync(TextReader)"/>
  public static IEnumerable<string> Lines(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    for (string line; (line = reader.ReadLine()) is not null;)
    {
      yield return line;
    }
  }

  /// <summary>
  ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a list of strings, using default system-dependent string separator.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
  /// <returns>List of strings which have been read from a <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Lines(TextReader)"/>
  public static async IAsyncEnumerable<string> LinesAsync(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    for (string line; (line = await reader.ReadLineAsync()) is not null;)
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
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
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
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static TextReader AsSynchronized(this TextReader reader) => reader is not null ? TextReader.Synchronized(reader) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytesAsync(TextReader, Encoding)"/>
  public static byte[] ToBytes(this TextReader reader, Encoding encoding = null) => reader.ToText().ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytes(TextReader, Encoding)"/>
  public static async Task<byte[]> ToBytesAsync(this TextReader reader, Encoding encoding = null) => (await reader.ToTextAsync().ConfigureAwait(false)).ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTextAsync(TextReader)"/>
  public static string ToText(this TextReader reader) => reader?.ReadToEnd() ?? throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a string.</para>
  /// </summary>
  /// <param name="reader"><see cref="TextReader"/> which is used to read text from its underlying source.</param>
  /// <returns>Text content which have been read from a <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(TextReader)"/>
  public static async Task<string> ToTextAsync(this TextReader reader) => reader is not null ? await reader.ReadToEndAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToEnumerable(TextReader, int, bool)"/>
  public static IEnumerable<char> ToEnumerable(this TextReader reader, bool close = false) => reader.ToEnumerable(4096, close).SelectMany(chars => chars);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="ToEnumerable(TextReader, bool)"/>
  public static IEnumerable<char[]> ToEnumerable(this TextReader reader, int count, bool close = false)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

    return new TextReaderEnumerable(reader, count, close);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToAsyncEnumerable(TextReader, int, bool)"/>
  public static async IAsyncEnumerable<char> ToAsyncEnumerable(this TextReader reader, bool close = false)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    await foreach (var elements in reader.ToAsyncEnumerable(4096, close).ConfigureAwait(false))
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
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="ToAsyncEnumerable(TextReader, bool)"/>
  public static IAsyncEnumerable<char[]> ToAsyncEnumerable(this TextReader reader, int count, bool close = false)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

    return new TextReaderAsyncEnumerable(reader, count, close);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static XmlReader ToXmlReader(this TextReader reader, bool close = true) => reader is not null ? XmlReader.Create(reader, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true }) : throw new ArgumentNullException(nameof(reader));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="close"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static XmlDictionaryReader ToXmlDictionaryReader(this TextReader reader, bool close = true) => reader.ToXmlReader(close).ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static XmlDocument ToXmlDocument(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    using var xmlReader = reader.ToXmlReader(false);

    return xmlReader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToXDocumentAsync(TextReader, CancellationToken)"/>
  public static XDocument ToXDocument(this TextReader reader)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    using var xmlReader = reader.ToXmlReader(false);

    return xmlReader.ToXDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToXDocument(TextReader)"/>
  public static async Task<XDocument> ToXDocumentAsync(this TextReader reader, CancellationToken cancellation = default)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    cancellation.ThrowIfCancellationRequested();

    using var xmlReader = reader.ToXmlReader(false);

    return await xmlReader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="reader"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static T DeserializeAsDataContract<T>(this TextReader reader, params Type[] types)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    using var xmlReader = reader.ToXmlReader(false);

    return xmlReader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="reader"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  public static T DeserializeAsXml<T>(this TextReader reader, params Type[] types)
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));

    using var xmlReader = reader.ToXmlReader(false);

    return xmlReader.DeserializeAsXml<T>(types);
  }

  private sealed class TextReaderEnumerable : IEnumerable<char[]>
  {
    private readonly TextReader reader;
    private readonly int count;
    private readonly bool close;

    public TextReaderEnumerable(TextReader reader, int count, bool close)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
      this.count = count;
      this.close = close;
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

      public char[] Current { get; private set; } = [];

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

      public void Dispose()
      {
        if (parent.close)
        {
          parent.reader.Dispose();
        }
      }

      object IEnumerator.Current => Current;
    }
  }

  private sealed class TextReaderAsyncEnumerable : IAsyncEnumerable<char[]>
  {
    private readonly TextReader reader;
    private readonly int count;
    private readonly bool close;

    public TextReaderAsyncEnumerable(TextReader reader, int count, bool close)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
      this.count = count;
      this.close = close;
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

      public async ValueTask DisposeAsync()
      {
        if (parent.close)
        {
          parent.reader.Dispose();
        }

        await Task.Yield();
      }

      public char[] Current { get; private set; } = [];

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