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
  /// <exception cref="ArgumentNullException">If <paramref name="array"/> is <see langword="null"/>.</exception>
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
  /// <param name="array"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="array"/> is <see langword="null"/>.</exception>
  public static byte[] FromBase64(this char[] array) => array is not null ? Convert.FromBase64CharArray(array, 0, array.Length) : throw new ArgumentNullException(nameof(array));

  /// <summary>
  ///   <para>Converts array of characters into array of bytes, using specified encoding.</para>
  /// </summary>
  /// <param name="array">Source array of characters.</param>
  /// <param name="encoding">Encoding to be used for transforming between <see cref="char"/> at its <see cref="byte"/> equivalent. If not specified, uses <see cref="Encoding.Default"/> encoding.</param>
  /// <returns>Array of bytes which represents <paramref name="array"/> array in <paramref name="encoding"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="array"/> is <see langword="null"/>.</exception>
  public static byte[] ToBytes(this char[] array, Encoding encoding = null) => array is not null ? array.Length > 0 ? (encoding ?? Encoding.Default).GetBytes(array) : [] : throw new ArgumentNullException(nameof(array));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="array"></param>
  /// <param name="offset"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="array"/> is <see langword="null"/>.</exception>
  public static ByteArrayContent ToByteArrayContent(this byte[] array, int? offset = null, int? count = null) => array is not null ? new ByteArrayContent(array, offset.GetValueOrDefault(), count.GetValueOrDefault(array.Length)) : throw new ArgumentNullException(nameof(array));

  /// <summary>
  ///   <para>Returns string representation of specified array of characters.</para>
  /// </summary>
  /// <param name="array">Source array of characters.</param>
  /// <returns>String which is formed from contents of <paramref name="array"/> array.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="array"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(byte[], Encoding)"/>
  public static string ToText(this char[] array) => array is not null ? array.Length > 0 ? new string(array) : string.Empty : throw new ArgumentNullException(nameof(array));

  /// <summary>
  ///   <para>Converts array of bytes into a string, using specified encoding.</para>
  /// </summary>
  /// <param name="array">Source array of bytes.</param>
  /// <param name="encoding">Encoding to be used for transforming between <see cref="byte"/> at its <see cref="char"/> equivalent. If not specified, uses <see cref="Encoding.UTF8"/> encoding.</param>
  /// <returns>Array of characters as a string which represents <paramref name="array"/> array in <paramref name="encoding"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="array"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(char[])"/>
  public static string ToText(this byte[] array, Encoding encoding = null) => array is not null ? array.Length > 0 ? (encoding ?? Encoding.Default).GetString(array) : string.Empty : throw new ArgumentNullException(nameof(array));
}