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
  /// <typeparam name="TReader"></typeparam>
  /// <param name="reader"></param>
  /// <param name="count"></param>
  /// <returns>Back self-reference to the given <paramref name="reader"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static TReader Skip<TReader>(this TReader reader, int count) where TReader : TextReader
  {
    if (reader is null) throw new ArgumentNullException(nameof(reader));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    count.Times(() => reader.Read());

    return reader;
  }

  /// <param name="reader"></param>
  extension(TextReader reader)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public bool IsEnd => reader is not null ? reader.Peek() < 0 : throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="LinesAsync(TextReader)"/>
    public IEnumerable<string> Lines()
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
    /// <returns>List of strings which have been read from a <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Lines(TextReader)"/>
    public async IAsyncEnumerable<string> LinesAsync()
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public TextReader AsSynchronized() => reader is not null ? TextReader.Synchronized(reader) : throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public T DeserializeAsDataContract<T>(params Type[] types)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      using var xmlReader = reader.ToXmlReader(false);

      return xmlReader.DeserializeAsDataContract<T>(types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public T DeserializeAsXml<T>(params Type[] types)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      using var xmlReader = reader.ToXmlReader(false);

      return xmlReader.DeserializeAsXml<T>(types);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToEnumerable(TextReader, int, bool)"/>
    public IEnumerable<char> ToEnumerable(bool close = false) => reader.ToEnumerable(4096, close).SelectMany(chars => chars);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="ToEnumerable(TextReader, bool)"/>
    public IEnumerable<char[]> ToEnumerable(int count, bool close = false)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      return new TextReaderEnumerable(reader, count, close);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToAsyncEnumerable(TextReader, int, bool)"/>
    public async IAsyncEnumerable<char> ToAsyncEnumerable(bool close = false)
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
    /// <param name="count"></param>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="ToAsyncEnumerable(TextReader, bool)"/>
    public IAsyncEnumerable<char[]> ToAsyncEnumerable(int count, bool close = false)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      return new TextReaderAsyncEnumerable(reader, count, close);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(TextReader, Encoding)"/>
    public byte[] ToBytes(Encoding encoding = null) => reader.ToText().ToBytes(encoding);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(TextReader, Encoding)"/>
    public async Task<byte[]> ToBytesAsync(Encoding encoding = null) => (await reader.ToTextAsync().ConfigureAwait(false)).ToBytes(encoding);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(TextReader)"/>
    public string ToText() => reader?.ReadToEnd() ?? throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para>Reads text using specified <see cref="TextReader"/> and returns it as a string.</para>
    /// </summary>
    /// <returns>Text content which have been read from a <paramref name="reader"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(TextReader)"/>
    public async Task<string> ToTextAsync() => reader is not null ? await reader.ReadToEndAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public XmlReader ToXmlReader(bool close = true) => reader is not null ? XmlReader.Create(reader, new XmlReaderSettings { CloseInput = close, IgnoreComments = true, IgnoreWhitespace = true }) : throw new ArgumentNullException(nameof(reader));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="close"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public XmlDictionaryReader ToXmlDictionaryReader(bool close = true) => reader.ToXmlReader(close).ToXmlDictionaryReader();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    public XmlDocument ToXmlDocument()
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      using var xmlReader = reader.ToXmlReader(false);

      return xmlReader.ToXmlDocument();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocumentAsync(TextReader, CancellationToken)"/>
    public XDocument ToXDocument()
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      using var xmlReader = reader.ToXmlReader(false);

      return xmlReader.ToXDocument();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="reader"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToXDocument(TextReader)"/>
    public async Task<XDocument> ToXDocumentAsync(CancellationToken cancellation = default)
    {
      if (reader is null) throw new ArgumentNullException(nameof(reader));

      cancellation.ThrowIfCancellationRequested();

      using var xmlReader = reader.ToXmlReader(false);

      return await xmlReader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => reader is not null && reader.Peek() >= 0;
  }

  private sealed class TextReaderEnumerable : IEnumerable<char[]>
  {
    private TextReader Reader { get; }
    private int Count { get; }
    private bool Close { get; }

    public TextReaderEnumerable(TextReader reader, int count, bool close)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      Reader = reader ?? throw new ArgumentNullException(nameof(reader));
      Count = count;
      Close = close;
    }

    public IEnumerator<char[]> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Enumerator : IEnumerator<char[]>
    {
      private TextReaderEnumerable Parent { get; }
      private char[] Buffer { get; }

      public Enumerator(TextReaderEnumerable parent)
      {
        Parent = parent ?? throw new ArgumentOutOfRangeException(nameof(parent));
        Buffer = new char[parent.Count];
      }

      public char[] Current { get; private set; } = [];

      public bool MoveNext()
      {
        var count = Parent.Reader.Read(Buffer, 0, Parent.Count);

        if (count > 0)
        {
          Current = Buffer.Range(0, count);
        }

        return count > 0;
      }

      public void Reset() { throw new InvalidOperationException(); }

      public void Dispose()
      {
        if (Parent.Close)
        {
          Parent.Reader.Dispose();
        }
      }

      object IEnumerator.Current => Current;
    }
  }

  private sealed class TextReaderAsyncEnumerable : IAsyncEnumerable<char[]>
  {
    private TextReader Reader { get; }
    private int Count { get; }
    private bool Close { get;}

    public TextReaderAsyncEnumerable(TextReader reader, int count, bool close)
    {
      if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));

      Reader = reader ?? throw new ArgumentNullException(nameof(reader));
      Count = count;
      Close = close;
    }

    public IAsyncEnumerator<char[]> GetAsyncEnumerator(CancellationToken cancellation = default) => new Enumerator(this);

    private sealed class Enumerator : IAsyncEnumerator<char[]>
    {
      private TextReaderAsyncEnumerable Parent { get; }
      private char[] Buffer { get; }

      public Enumerator(TextReaderAsyncEnumerable parent)
      {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        Buffer = new char[parent.Count];
      }

      public async ValueTask DisposeAsync()
      {
        if (Parent.Close)
        {
          Parent.Reader.Dispose();
        }

        await Task.Yield();
      }

      public char[] Current { get; private set; } = [];

      public async ValueTask<bool> MoveNextAsync()
      {
        var count = await Parent.Reader.ReadAsync(Buffer, 0, Parent.Count).ConfigureAwait(false);

        if (count > 0)
        {
          Current = Buffer.Range(0, count);
        }

        return count > 0;
      }
    }
  }
}