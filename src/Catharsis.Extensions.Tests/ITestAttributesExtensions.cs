using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using Catharsis.Commons;

namespace Catharsis.Extensions;

/// <summary>
///   <para></para>
/// </summary>
public static class ITestAttributesExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static string ShellCommand(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(ShellCommand), "cmd.exe") : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static IAsyncEnumerable<object> EmptyAsyncEnumerable(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(EmptyAsyncEnumerable), Enumerable.Empty<object>().ToAsyncEnumerable()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static byte[] RandomBytes(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomBytes), new Random().ByteSequence(short.MaxValue).AsArray()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static char[] RandomChars(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomChars), new Random().Letters(short.MaxValue).AsArray()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static string RandomString(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomString), new Random().Letters(short.MaxValue)) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static SecureString EmptySecureString(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(EmptySecureString), new SecureString()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static TextReader EmptyTextReader(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(EmptyTextReader), string.Empty.ToStringReader()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static SecureString RandomSecureString(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomSecureString), new Random().SecureStringInRange(short.MaxValue, 'a'..'z', 'A'..'Z')) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static object[] RandomObjects(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomObjects), new Random().ObjectSequence(short.MaxValue).AsArray()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static Stream EmptyStream(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(EmptyStream), new MemoryStream()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static MemoryStream RandomStream(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomStream), new Random().MemoryStreamAsync(short.MaxValue).Await()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static Stream RandomReadOnlyStream(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomReadOnlyStream), new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnly()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static Stream RandomReadOnlyForwardStream(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomReadOnlyForwardStream), new Random().MemoryStreamAsync(short.MaxValue).Await().AsReadOnlyForward()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static Stream WriteOnlyStream(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(WriteOnlyStream), new MemoryStream().AsWriteOnly()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static Stream WriteOnlyForwardStream(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(WriteOnlyForwardStream), new MemoryStream().AsWriteOnlyForward()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static string RandomName(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomName), new Random().Letters(25)) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static FileInfo RandomFakeFile(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomFakeFile), new Random().FilePath().ToFile()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static FileInfo RandomEmptyFile(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomEmptyFile), new Random().File()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static FileInfo RandomNonEmptyFile(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomNonEmptyFile), new Random().TextFileAsync(short.MaxValue).Await()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static DirectoryInfo RandomDirectory(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomDirectory), new Random().Directory()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static DirectoryInfo RandomFakeDirectory(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(RandomFakeDirectory), new Random().DirectoryPath().ToDirectory()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static HttpClient Http(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(Http), new HttpClient()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static TcpClient Tcp(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(Tcp), new TcpClient()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static UdpClient Udp(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(Udp), new UdpClient()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static SymmetricAlgorithm SymmetricAlgorithm(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(SymmetricAlgorithm), Aes.Create()) : throw new ArgumentNullException(nameof(attributes));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="attributes"/> is <see langword="null"/>.</exception>
  public static HashAlgorithm HashAlgorithm(this ITestAttributes attributes) => attributes is not null ? attributes.Retrieve(nameof(HashAlgorithm), SHA512.Create()) : throw new ArgumentNullException(nameof(attributes));
}