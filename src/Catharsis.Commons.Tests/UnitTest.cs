using System.Security;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para></para>
/// </summary>
public abstract class UnitTest
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static CancellationToken Cancellation { get; } = new(true);

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static IAsyncEnumerable<object> EmptyAsyncEnumerable { get; } = Enumerable.Empty<object>().ToAsyncEnumerable();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static Random Randomizer { get; } = new();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static byte[] RandomBytes => new Random().ByteSequence(short.MaxValue).AsArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static char[] RandomChars => new Random().Letters(short.MaxValue).AsArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static string RandomString => new Random().Letters(short.MaxValue);

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static SecureString EmptySecureString => new();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <returns></returns>
  protected static TextReader EmptyTextReader => string.Empty.ToStringReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static SecureString RandomSecureString => new Random().SecureString(short.MaxValue);

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static object[] RandomObjects => new Random().ObjectSequence(short.MaxValue).AsArray();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static Stream EmptyStream => new MemoryStream();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static MemoryStream RandomStream => new Random().MemoryStreamAsync(short.MaxValue).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static Stream RandomReadOnlyStream => new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnly();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static Stream RandomReadOnlyForwardStream => new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnlyForward();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static Stream WriteOnlyStream => new MemoryStream().AsWriteOnly();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static Stream WriteOnlyForwardStream => new MemoryStream().AsWriteOnlyForward();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static string RandomName => new Random().Letters(25);

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static FileInfo RandomFakeFile => new Random().FilePath().ToFile();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static FileInfo RandomEmptyFile => new Random().File();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static FileInfo RandomNonEmptyFile => new Random().TextFileAsync(short.MaxValue).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static DirectoryInfo RandomDirectory => new Random().Directory();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected static DirectoryInfo RandomFakeDirectory => new Random().DirectoryPath().ToDirectory();
}