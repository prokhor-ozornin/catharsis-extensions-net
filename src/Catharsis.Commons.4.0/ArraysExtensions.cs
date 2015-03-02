using System;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for arrays.</para>
  /// </summary>
  public static class ArraysExtensions
  {
    /// <summary>
    ///   <para>Returns BASE64-encoded representation of a bytes sequence.</para>
    /// </summary>
    /// <param name="self">Bytes to convert to BASE64 encoding.</param>
    /// <returns>BASE64 string representation of <paramref name="self"/> array.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="System.Convert.ToBase64String(byte[])"/>
    public static string Base64(this byte[] self)
    {
      Assertion.NotNull(self);

      return System.Convert.ToBase64String(self);
    }

    /// <summary>
    ///   <para>Converts array of characters into array of bytes, using specified encoding.</para>
    /// </summary>
    /// <param name="self">Source array of characters.</param>
    /// <param name="encoding">Encoding to be used for transforming between <see cref="char"/> at its <see cref="byte"/> equivalent. If not specified, uses <see cref="Encoding.UTF8"/> encoding.</param>
    /// <returns>Array of bytes which represents <paramref name="self"/> array in <paramref name="encoding"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Encoding.GetBytes(char[])"/>
    public static byte[] Bytes(this char[] self, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      return (encoding ?? Encoding.UTF8).GetBytes(self);
    }

    /// <summary>
    ///   <para>Converts array of bytes into HEX-encoded string.</para>
    /// </summary>
    /// <param name="self">Bytes to convert to HEX string.</param>
    /// <returns>HEX string representation of <paramref name="self"/> array.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="BitConverter.ToString(byte[])"/>
    /// <seealso cref="StringExtensions.Hex(string)"/>
    public static string Hex(this byte[] self)
    {
      Assertion.NotNull(self);

      return BitConverter.ToString(self).Replace("-", "");
    }

    /// <summary>
    ///   <para>Concatenates contents of two arrays, producing a new one.</para>
    /// </summary>
    /// <typeparam name="T">Type of array elements.</typeparam>
    /// <param name="self">First (left-side) array.</param>
    /// <param name="other">Second (right-side) array.</param>
    /// <returns>Results array which contains all elements from <paramref name="self"/> array, immediately followed by all elements from <paramref name="second"/> array.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="other"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Array.Copy(Array, Array, int)"/>
    public static T[] Join<T>(this T[] self, T[] other)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(other);

      var result = new T[self.Length + other.Length];
      Array.Copy(self, result, self.Length);
      Array.Copy(other, 0, result, self.Length, other.Length);
      return result;
    }

    /// <summary>
    ///   <para>Returns string representation of specified array of characters.</para>
    /// </summary>
    /// <param name="self">Source array of characters.</param>
    /// <returns>String which is formed from contents of <paramref name="self"/> array.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="String(byte[], Encoding)"/>
    public static string String(this char[] self)
    {
      Assertion.NotNull(self);

      return new string(self);
    }

    /// <summary>
    ///   <para>Converts array of bytes into a string, using specified encoding.</para>
    /// </summary>
    /// <param name="self">Source array of bytes.</param>
    /// <param name="encoding">Encoding to be used for transforming between <see cref="byte"/> at its <see cref="char"/> equivalent. If not specified, uses <see cref="Encoding.UTF8"/> encoding.</param>
    /// <returns>Array of characters as a string which represents <paramref name="self"/> array in <paramref name="encoding"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="String(char[])"/>
    /// <seealso cref="Encoding.GetString(byte[], int, int)"/>
    public static string String(this byte[] self, Encoding encoding = null)
    {
      Assertion.NotNull(self);

      return (encoding ?? Encoding.UTF8).GetString(self, 0, self.Length);
    }
  }
}