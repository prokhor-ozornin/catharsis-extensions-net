using System.Security;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para></para>
/// </summary>
public abstract class UnitTest : IDisposable
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected const string ShellCommand = "cmd.exe";

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected CancellationToken Cancellation { get; } = new(true);

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Uri LocalHost { get; } = "https://localhost".ToUri();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected IAsyncEnumerable<object> EmptyAsyncEnumerable { get; } = Enumerable.Empty<object>().ToAsyncEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Random Randomizer { get; } = new();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected byte[] RandomBytes => new Random().ByteSequence(short.MaxValue).AsArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected char[] RandomChars => new Random().Letters(short.MaxValue).AsArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected string RandomString => new Random().Letters(short.MaxValue);

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected SecureString EmptySecureString => new();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <returns></returns>
  protected TextReader EmptyTextReader => string.Empty.ToStringReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected SecureString RandomSecureString => new Random().SecureString(short.MaxValue);

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected object[] RandomObjects => new Random().ObjectSequence(short.MaxValue).AsArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream EmptyStream => new MemoryStream();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected MemoryStream RandomStream => new Random().MemoryStreamAsync(short.MaxValue).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream RandomReadOnlyStream => new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnly();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream RandomReadOnlyForwardStream => new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnlyForward();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream WriteOnlyStream => new MemoryStream().AsWriteOnly();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream WriteOnlyForwardStream => new MemoryStream().AsWriteOnlyForward();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected string RandomName => new Random().Letters(25);

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected FileInfo RandomFakeFile => new Random().FilePath().ToFile();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected FileInfo RandomEmptyFile => new Random().File();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected FileInfo RandomNonEmptyFile => new Random().TextFileAsync(short.MaxValue).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected DirectoryInfo RandomDirectory => new Random().Directory();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected DirectoryInfo RandomFakeDirectory => new Random().DirectoryPath().ToDirectory();

  /// <summary>
  ///   <para></para>
  /// </summary>
  public virtual void Dispose()
  {
  }
}