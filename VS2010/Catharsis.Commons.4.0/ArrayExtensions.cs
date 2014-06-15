using System;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for arrays.</para>
  /// </summary>
  public static class ArrayExtensions
  {
    /// <summary>
    ///   <para>Returns BASE64-encoded representation of a bytes sequence.</para>
    /// </summary>
    /// <param name="bytes">Bytes to convert to BASE64 encoding.</param>
    /// <returns>BASE64 string representation of <paramref name="bytes"/> array.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="System.Convert.ToBase64String(byte[])"/>
    public static string Base64(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      return System.Convert.ToBase64String(bytes);
    }

    /// <summary>
    ///   <para>Converts array of characters into array of bytes, using specified encoding.</para>
    /// </summary>
    /// <param name="chars">Source array of characters.</param>
    /// <param name="encoding">Encoding to be used for transforming between <see cref="char"/> at its <see cref="byte"/> equivalent. If not specified, uses <see cref="Encoding.UTF8"/> encoding.</param>
    /// <returns>Array of bytes which represents <paramref name="chars"/> array in <paramref name="encoding"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="chars"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Encoding.GetBytes(char[])"/>
    public static byte[] Bytes(this char[] chars, Encoding encoding = null)
    {
      Assertion.NotNull(chars);

      return (encoding ?? Encoding.UTF8).GetBytes(chars);
    }

    /// <summary>
    ///   <para>Converts array of bytes into HEX-encoded string.</para>
    /// </summary>
    /// <param name="bytes">Bytes to convert to HEX string.</param>
    /// <returns>HEX string representation of <paramref name="bytes"/> array.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BitConverter.ToString(byte[])"/>
    /// <seealso cref="StringExtensions.Hex(string)"/>
    public static string Hex(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      return BitConverter.ToString(bytes).Replace("-", "");
    }

    /// <summary>
    ///   <para>Concatenates contents of two arrays, producing a new one.</para>
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="first">First (left-side) array.</param>
    /// <param name="second">Second (right-side) array.</param>
    /// <returns>Results array which contains all elements from <paramref name="first"/> array, immediately followed by all elements from <paramref name="second"/> array.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="first"/> or <paramref name="second"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Array.Copy(Array, Array, int)"/>
    public static T[] Join<T>(this T[] first, T[] second)
    {
      Assertion.NotNull(first);
      Assertion.NotNull(second);

      var result = new T[first.Length + second.Length];
      Array.Copy(first, result, first.Length);
      Array.Copy(second, 0, result, first.Length, second.Length);
      return result;
    }

    /// <summary>
    ///   <para>Returns string representation of specified array of characters.</para>
    /// </summary>
    /// <param name="chars">Source array of characters.</param>
    /// <returns>String which is formed from contents of <paramref name="chars"/> array.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="chars"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="String(byte[], Encoding)"/>
    public static string String(this char[] chars)
    {
      Assertion.NotNull(chars);

      return new string(chars);
    }

    /// <summary>
    ///   <para>Converts array of bytes into a string, using specified encoding.</para>
    /// </summary>
    /// <param name="bytes">Source array of bytes.</param>
    /// <param name="encoding">Encoding to be used for transforming between <see cref="byte"/> at its <see cref="char"/> equivalent. If not specified, uses <see cref="Encoding.UTF8"/> encoding.</param>
    /// <returns>Array of characters as a string which represents <paramref name="bytes"/> array in <paramref name="encoding"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="String(char[])"/>
    /// <seealso cref="Encoding.GetString(byte[], int, int)"/>
    public static string String(this byte[] bytes, Encoding encoding = null)
    {
      Assertion.NotNull(bytes);

      return (encoding ?? Encoding.UTF8).GetString(bytes, 0, bytes.Length);
    }
  }
}