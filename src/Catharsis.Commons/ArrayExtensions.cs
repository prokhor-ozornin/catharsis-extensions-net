using System.Text;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for arrays.</para>
/// </summary>
/// <seealso cref="Array"/>
public static class ArrayExtensions
{
  /// <summary>
  ///   <para>Converts array of characters into array of bytes, using specified encoding.</para>
  /// </summary>
  /// <param name="chars">Source array of characters.</param>
  /// <param name="encoding">Encoding to be used for transforming between <see cref="char"/> at its <see cref="byte"/> equivalent. If not specified, uses <see cref="Encoding.Default"/> encoding.</param>
  /// <returns>Array of bytes which represents <paramref name="chars"/> array in <paramref name="encoding"/>.</returns>
  public static byte[] Bytes(this char[] chars, Encoding? encoding = null) => chars.Length > 0 ? (encoding ?? Encoding.Default).GetBytes(chars) : Array.Empty<byte>();

  /// <summary>
  ///   <para>Returns string representation of specified array of characters.</para>
  /// </summary>
  /// <param name="chars">Source array of characters.</param>
  /// <returns>String which is formed from contents of <paramref name="chars"/> array.</returns>
  public static string Text(this char[] chars) => chars.Length > 0 ? new string(chars) : string.Empty;

  /// <summary>
  ///   <para>Converts array of bytes into a string, using specified encoding.</para>
  /// </summary>
  /// <param name="bytes">Source array of bytes.</param>
  /// <param name="encoding">Encoding to be used for transforming between <see cref="byte"/> at its <see cref="char"/> equivalent. If not specified, uses <see cref="Encoding.UTF8"/> encoding.</param>
  /// <returns>Array of characters as a string which represents <paramref name="bytes"/> array in <paramref name="encoding"/>.</returns>
  public static string Text(this byte[] bytes, Encoding? encoding = null) => bytes.Length > 0 ? (encoding ?? Encoding.Default).GetString(bytes) : string.Empty;
}