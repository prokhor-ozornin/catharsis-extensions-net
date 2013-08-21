using System;
using System.Security.Cryptography;
using System.Text;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public static class ArrayExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="chars"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="chars"/> is a <c>null</c> reference.</exception>
    public static byte[] Bytes(this char[] chars, Encoding encoding = null)
    {
      Assertion.NotNull(chars);

      return (encoding ?? Encoding.Default).GetBytes(chars);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static string EncodeBase64(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      return Convert.ToBase64String(bytes);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static string EncodeHex(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      return BitConverter.ToString(bytes).Replace("-", "");
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="array"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeMD5(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("MD5"))
      {
        return hash.ComputeHash(bytes);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeSHA1(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("SHA1"))
      {
        return hash.ComputeHash(bytes);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeSHA256(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("SHA256"))
      {
        return hash.ComputeHash(bytes);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static byte[] EncodeSHA512(this byte[] bytes)
    {
      Assertion.NotNull(bytes);

      using (var hash = HashAlgorithm.Create("SHA512"))
      {
        return hash.ComputeHash(bytes);
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="first"/> or <paramref name="second"/> is a <c>null</c> reference.</exception>
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
    ///   <para></para>
    /// </summary>
    /// <param name="chars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="chars"/> is a <c>null</c> reference.</exception>
    public static string String(this char[] chars)
    {
      Assertion.NotNull(chars);

      return new string(chars);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes"/> is a <c>null</c> reference.</exception>
    public static string String(this byte[] bytes, Encoding encoding = null)
    {
      Assertion.NotNull(bytes);

      return (encoding ?? Encoding.Default).GetString(bytes);
    }
  }
}