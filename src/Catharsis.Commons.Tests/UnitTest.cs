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
  protected CancellationToken Cancellation { get; } = new(true);

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
  protected MemoryStream RandomStream => new Random().MemoryStream(short.MaxValue).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream RandomReadOnlyStream => new Random().MemoryStream(short.MaxValue).Await().ReadOnly();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream RandomReadOnlyForwardStream => new Random().MemoryStream(short.MaxValue).Await().ReadOnlyForward();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream WriteOnlyStream => new MemoryStream().WriteOnly();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected Stream WriteOnlyForwardStream => new MemoryStream().WriteOnlyForward();

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
  protected FileInfo RandomNonEmptyFile => new Random().TextFile(short.MaxValue).Await();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected DirectoryInfo RandomDirectory => new Random().Directory();

  /// <summary>
  ///   <para></para>
  /// </summary>
  protected DirectoryInfo RandomFakeDirectory => new Random().DirectoryPath().ToDirectory();
}