using System.Text;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for arrays.</para>
/// </summary>
/// <seealso cref="Array"/>
public static class ArrayExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="array"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static T[] Range<T>(this T[] array, int? offset = null, int? count = null)
  {
    if (array is null) throw new ArgumentNullException(nameof(array));
    if (offset is < 0) throw new ArgumentOutOfRangeException(nameof(offset));
    if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (offset is null && count is null)
    {
      return array;
    }

    var fromIndex = offset ?? 0;
    var toIndex = count ?? array.Length - fromIndex;

    return new ArraySegment<T>(array, fromIndex, toIndex).ToArray();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="chars"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] FromBase64(this char[] chars) => chars is not null ? Convert.FromBase64CharArray(chars, 0, chars.Length) : throw new ArgumentNullException(nameof(chars));

  /// <summary>
  ///   <para>Converts array of characters into array of bytes, using specified encoding.</para>
  /// </summary>
  /// <param name="chars">Source array of characters.</param>
  /// <param name="encoding">Encoding to be used for transforming between <see cref="char"/> at its <see cref="byte"/> equivalent. If not specified, uses <see cref="Encoding.Default"/> encoding.</param>
  /// <returns>Array of bytes which represents <paramref name="chars"/> array in <paramref name="encoding"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] ToBytes(this char[] chars, Encoding encoding = null) => chars is not null ? chars.Length > 0 ? (encoding ?? Encoding.Default).GetBytes(chars) : Array.Empty<byte>() : throw new ArgumentNullException(nameof(chars));

  /// <summary>
  ///   <para>Returns string representation of specified array of characters.</para>
  /// </summary>
  /// <param name="chars">Source array of characters.</param>
  /// <returns>String which is formed from contents of <paramref name="chars"/> array.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this char[] chars) => chars is not null ? chars.Length > 0 ? new string(chars) : string.Empty : throw new ArgumentNullException(nameof(chars));

  /// <summary>
  ///   <para>Converts array of bytes into a string, using specified encoding.</para>
  /// </summary>
  /// <param name="bytes">Source array of bytes.</param>
  /// <param name="encoding">Encoding to be used for transforming between <see cref="byte"/> at its <see cref="char"/> equivalent. If not specified, uses <see cref="Encoding.UTF8"/> encoding.</param>
  /// <returns>Array of characters as a string which represents <paramref name="bytes"/> array in <paramref name="encoding"/>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this byte[] bytes, Encoding encoding = null) => bytes is not null ? bytes.Length > 0 ? (encoding ?? Encoding.Default).GetString(bytes) : string.Empty : throw new ArgumentNullException(nameof(bytes));
}