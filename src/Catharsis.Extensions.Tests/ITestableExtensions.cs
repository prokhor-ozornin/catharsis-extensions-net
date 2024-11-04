using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;

namespace Catharsis.Extensions;

/// <summary>
///   <para></para>
/// </summary>
public static class ITestableExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static string ShellCommand(this ITestable testable) => testable is not null ? "cmd.exe" : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static IAsyncEnumerable<object> EmptyAsyncEnumerable(this ITestable testable) => testable is not null ? Enumerable.Empty<object>().ToAsyncEnumerable() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static byte[] RandomBytes(this ITestable testable) => testable is not null ? new Random().ByteSequence(short.MaxValue).AsArray() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static char[] RandomChars(this ITestable testable) => testable is not null ? new Random().Letters(short.MaxValue).AsArray() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static string RandomString(this ITestable testable) => testable is not null ? new Random().Letters(short.MaxValue) : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static SecureString EmptySecureString(this ITestable testable) => testable is not null ? new SecureString() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static TextReader EmptyTextReader(this ITestable testable) => testable is not null ? string.Empty.ToStringReader() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static SecureString RandomSecureString(this ITestable testable) => testable is not null ? new Random().SecureString(short.MaxValue, new [] {'a'..'z', 'A'..'Z'}) : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static object[] RandomObjects(this ITestable testable) => testable is not null ? new Random().ObjectSequence(short.MaxValue).AsArray() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static Stream EmptyStream(this ITestable testable) => testable is not null ? new MemoryStream() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static MemoryStream RandomStream(this ITestable testable) => testable is not null ? new Random().MemoryStreamAsync(short.MaxValue).Await() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static Stream RandomReadOnlyStream(this ITestable testable) => testable is not null ? new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnly() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static Stream RandomReadOnlyForwardStream(this ITestable testable) => testable is not null ? new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnlyForward() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static Stream WriteOnlyStream(this ITestable testable) => testable is not null ? new MemoryStream().AsWriteOnly() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static Stream WriteOnlyForwardStream(this ITestable testable) => testable is not null ? new MemoryStream().AsWriteOnlyForward() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static string RandomName(this ITestable testable) => testable is not null ? new Random().Letters(25) : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static FileInfo RandomFakeFile(this ITestable testable) => testable is not null ? new Random().FilePath().ToFile() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static FileInfo RandomEmptyFile(this ITestable testable) => testable is not null ? new Random().File() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static FileInfo RandomNonEmptyFile(this ITestable testable) => testable is not null ? new Random().TextFileAsync(short.MaxValue).Await() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static DirectoryInfo RandomDirectory(this ITestable testable) => testable is not null ? new Random().Directory() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static DirectoryInfo RandomFakeDirectory(this ITestable testable) => testable is not null ? new Random().DirectoryPath().ToDirectory() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static HttpClient Http(this ITestable testable) => testable is not null ? new HttpClient() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static TcpClient Tcp(this ITestable testable) => testable is not null ? new TcpClient() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static UdpClient Udp(this ITestable testable) => testable is not null ? new UdpClient() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static SymmetricAlgorithm SymmetricAlgorithm(this ITestable testable) => testable is not null ? Aes.Create() : throw new ArgumentNullException(nameof(testable));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="testable"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="testable"/> is <see langword="null"/>.</exception>
  public static HashAlgorithm HashAlgorithm(this ITestable testable) => testable is not null ? SHA512.Create() : throw new ArgumentNullException(nameof(testable));
}