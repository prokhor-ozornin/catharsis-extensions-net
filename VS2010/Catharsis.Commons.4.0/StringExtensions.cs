using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  /// </summary>
  /// <seealso cref="string"/>
  public static class StringExtensions
  {
    /// <summary>
    ///   <para>Converts a BASE64-encoded string to an equivalent 8-bit unsigned integer array.</para>
    /// </summary>
    /// <param name="value">BASE64-encoded string to be converted to binary form.</param>
    /// <returns>An array of 8-bit unsigned integers that is equivalent to <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="System.Convert.FromBase64String(string)"/>
    public static byte[] Base64(this string value)
    {
      Assertion.NotNull(value);

      return System.Convert.FromBase64String(value);
    }

    /// <summary>
    ///   <para>Converts string to an array of bytes, using specified <see cref="Encoding"/>.</para>
    /// </summary>
    /// <param name="value">String that consists of characters that are to be transformed to bytes.</param>
    /// <param name="encoding">Encoding to be used for transformation between characters of <paramref name="value"/> and their bytes equivalents. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
    /// <param name="preamble">Whether to append byte preamble of <paramref name="encoding"/> to the beginning of the resulting byte array.</param>
    /// <returns>Array of bytes that form <paramref name="value"/> string in given <paramref name="encoding"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Encoding.GetBytes(string)"/>
    public static byte[] Bytes(this string value, Encoding encoding = null, bool preamble = true)
    {
      Assertion.NotNull(value);

      var textEncoding = encoding ?? Encoding.UTF8;
      return preamble ? textEncoding.GetPreamble().Join(textEncoding.GetBytes(value)) : textEncoding.GetBytes(value);
    }

    /// <summary>
    ///   <para>Compares two specified strings and returns an integer that indicates their relative position in the sort order.</para>
    /// </summary>
    /// <param name="self">The current string to compare with the second.</param>
    /// <param name="other">The second string to compare with the current.</param>
    /// <param name="options">One of the enumeration values that specifies the rules to use in the comparison.</param>
    /// <returns>Integer value that indicates the lexical relationship between the two comparands.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="string.Compare(string, string, StringComparison)"/>
    /// <seealso cref="CompareTo(string, string, CompareOptions, CultureInfo)"/>
    public static int CompareTo(this string self, string other, StringComparison options)
    {
      Assertion.NotNull(self);

      return string.Compare(self, other, options);
    }

    /// <summary>
    ///   <para>Compares two specified strings and returns an integer that indicates their relative position in the sort order.</para>
    /// </summary>
    /// <param name="self">The current string to compare with the second.</param>
    /// <param name="other">The second string to compare with the current.</param>
    /// <param name="options">Options to use when performing the comparison (such as ignoring case or symbols).</param>
    /// <param name="culture">The culture that supplies culture-specific comparison information.</param>
    /// <returns>Integer value that indicates the lexical relationship between the two comparands.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="string.Compare(string, string, CultureInfo, CompareOptions)"/>
    /// <seealso cref="CompareTo(string, string, StringComparison)"/>
    public static int CompareTo(this string self, string other, CompareOptions options, CultureInfo culture = null)
    {
      Assertion.NotNull(self);

      return string.Compare(self, other, culture ?? CultureInfo.InvariantCulture, options);
    }

    /// <summary>
    ///   <para>Replaces each format item in a string with the text equivalent of a corresponding object's value.</para>
    /// </summary>
    /// <param name="value">A composite format string.</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    /// <returns>A copy of <paramref name="value"/> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="string.Format(string, object[])"/>
    public static string FormatSelf(this string value, params object[] args)
    {
      Assertion.NotNull(value);

      return string.Format(value, args);
    }

    /// <summary>
    ///   <para>Replaces each format item in a string with the text equivalent of a corresponding object's value, using <see cref="CultureInfo.InvariantCulture"/> to perform string conversion of objects.</para>
    /// </summary>
    /// <param name="value">A composite format string.</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    /// <returns>A copy of <paramref name="value"/> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="string.Format(string, object[])"/>
    public static string FormatInvariant(this string value, params object[] args)
    {
      Assertion.NotNull(value);

      return string.Format(CultureInfo.InvariantCulture, value, args);
    }

    /// <summary>
    ///   <para>Converts HEX-encoded string into array of bytes.</para>
    /// </summary>
    /// <param name="value">HEX-encoded string to be converted to byte array.</param>
    /// <returns>Decoded data from HEX-encoded <paramref name="value"/> string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="ArrayExtensions.Hex(byte[])"/>
    public static byte[] Hex(this string value)
    {
      Assertion.NotNull(value);

      if (value.Length == 0)
      {
        return Enumerable.Empty<byte>().ToArray();
      }

      var result = new byte[value.Length / 2];
      for (var i = 0; i < value.Length; i += 2)
      {
        result[i / 2] = byte.Parse(value.Substring(i, 2), NumberStyles.HexNumber);
      }
      return result;
    }

    /// <summary>
    ///   <para>Determines where a string is either <c>null</c> reference or is <see cref="string.Empty"/>.</para>
    /// </summary>
    /// <param name="value">String to evaluate.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is either a <c>null</c> reference or an empty string.</returns>
    /// <seealso cref="string.IsNullOrEmpty(string)"/>
    public static bool IsEmpty(this string value)
    {
      return string.IsNullOrEmpty(value);
    }

    /// <summary>
    ///   <para>Determines whether a string matches specified regular expression.</para>
    /// </summary>
    /// <param name="value">The string to search for a match.</param>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <returns><c>true</c> if <paramref name="value"/> matches <paramref name="pattern"/> regular expression, <c>false</c> if not.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="value"/> or <paramref name="pattern"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Regex.IsMatch(string, string)"/>
    public static bool Matches(this string value, string pattern)
    {
      Assertion.NotNull(value);
      Assertion.NotNull(pattern);

      return Regex.IsMatch(value, pattern);
    }

    /// <summary>
    ///   <para>Replaces all occurrences of different substrings with specified new values.</para>
    /// </summary>
    /// <param name="where">Source string where replacements are to be made.</param>
    /// <param name="replacements">Collection of replacements, where key represents a substring to be replaced and value represents a new value for former substring.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="where"/> or <paramref name="replacements"/> is a <c>null</c> reference.</exception>
    /// <returns>New string with performed replacements.</returns>
    /// <seealso cref="Replace(string, IEnumerable{string}, IEnumerable{string})"/>
    /// <seealso cref="Replace(string, string[], string[])"/>
    public static string Replace(this string where, IEnumerable<KeyValuePair<string, string>> replacements)
    {
      Assertion.NotNull(where);
      Assertion.NotNull(replacements);

      var sb = new StringBuilder(where);
      replacements.Where(replacement => replacement.Value != null).Each(replacement => sb.Replace(replacement.Key, replacement.Value));
      return sb.ToString();
    }

    /// <summary>
    ///   <para>Replaces all occurrences of different substrings with specified new values.</para>
    /// </summary>
    /// <param name="where">Source string where replacements are to be made.</param>
    /// <param name="from">Collection of substrings that should be replaced in source string. It must have the same number of elements as in <paramref name="to"/> sequence.</param>
    /// <param name="to">Collection of new replacement values. It must have the same number of elements as in <paramref name="from"/> sequence.</param>
    /// <returns>New string with performed replacements.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="where"/>, <paramref name="from"/> or <paramref name="to"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException"></exception>
    /// <seealso cref="Replace(string, IEnumerable{KeyValuePair{string, string}})"/>
    /// <seealso cref="Replace(string, string[], string[])"/>
    public static string Replace(this string where, IEnumerable<string> from, IEnumerable<string> to)
    {
      Assertion.NotNull(where);
      Assertion.NotNull(from);
      Assertion.NotNull(to);
      Assertion.True(from.Count() == to.Count());

      var sb = new StringBuilder(where);
      for (var i = 0; i < from.Count(); i++)
      {
        sb.Replace(from.ElementAt(i), to.ElementAt(i));
      }
      return sb.ToString();
    }

    /// <summary>
    ///   <para>Replaces all occurrences of different substrings with specified new values.</para>
    /// </summary>
    /// <param name="where">Source string where replacements are to be made.</param>
    /// <param name="from">Array of substrings that should be replaced in source string. It must have the same number of elements as in <paramref name="to"/> array.</param>
    /// <param name="to">Array of new replacement values. It must have the same number of elements as in <paramref name="from"/> array.</param>
    /// <returns>New string with performed replacements.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="where"/>, <paramref name="from"/> or <paramref name="to"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException"></exception>
    /// <seealso cref="Replace(string, IEnumerable{KeyValuePair{string, string}})"/>
    /// <seealso cref="Replace(string, IEnumerable{string}, IEnumerable{string})"/>
    public static string Replace(this string where, string[] from, string[] to)
    {
      Assertion.NotNull(where);
      Assertion.NotNull(from);
      Assertion.NotNull(to);
      Assertion.True(from.Length == to.Length);

      var sb = new StringBuilder(where);
      for (var i = 0; i < from.Length; i++)
      {
        sb.Replace(from[i], to[i]);
      }
      return sb.ToString();
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="bool"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> is equivalent to <see cref="bool.TrueString"/>, <c>false otherwise</c>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="bool.Parse(string)"/>
    /// <seealso cref="ToBoolean(string, out bool)"/>
    public static bool ToBoolean(this string value)
    {
      Assertion.NotEmpty(value);

      return bool.Parse(value);
    }

    /// <summary>
    ///  <para>Converts specified string into <see cref="bool"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result"><c>true</c> if <paramref name="value"/> is equivalent to <see cref="bool.TrueString"/>, <c>false otherwise</c>.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="bool.TryParse(string, out bool)"/>
    /// <seealso cref="ToBoolean(string)"/>
    public static bool ToBoolean(this string value, out bool result)
    {
      return bool.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="byte"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="byte"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is empty string.</exception>
    /// <seealso cref="byte.Parse(string)"/>
    /// <seealso cref="ToByte(string, out byte)"/>
    public static byte ToByte(this string value)
    {
      Assertion.NotEmpty(value);

      return Byte.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="bool"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="byte"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="byte.TryParse(string, out byte)"/>
    /// <seealso cref="ToByte(string)"/>
    public static bool ToByte(this string value, out byte result)
    {
      return byte.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="DateTime"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="DateTime"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="DateTime.Parse(string)"/>
    /// <seealso cref="ToDateTime(string, out DateTime)"/>
    public static DateTime ToDateTime(this string value)
    {
      Assertion.NotEmpty(value);

      return DateTime.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="DateTime"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="DateTime"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="DateTime.TryParse(string, out DateTime)"/>
    /// <seealso cref="ToDateTime(string)"/>
    public static bool ToDateTime(this string value, out DateTime result)
    {
      return DateTime.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="decimal"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="decimal"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is empty string.</exception>
    /// <seealso cref="decimal.Parse(string)"/>
    /// <seealso cref="ToDecimal(string, out decimal)"/>
    public static decimal ToDecimal(this string value)
    {
      Assertion.NotEmpty(value);

      return Decimal.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="decimal"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="decimal"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="decimal.TryParse(string, out decimal)"/>
    /// <seealso cref="ToDecimal(string)"/>
    public static bool ToDecimal(this string value, out decimal result)
    {
      return decimal.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="double"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="double"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is empty string.</exception>
    /// <seealso cref="double.Parse(string)"/>
    /// <seealso cref="ToDouble(string, out double)"/>
    public static double ToDouble(this string value)
    {
      Assertion.NotEmpty(value);

      return double.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="double"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="double"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="double.TryParse(string, out double)"/>
    /// <seealso cref="ToDouble(string)"/>
    public static bool ToDouble(this string value, out double result)
    {
      return double.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into enumeration of specified type.</para>
    /// </summary>
    /// <typeparam name="T">Type of enumeration.</typeparam>
    /// <param name="value">String to be converted.</param>
    /// <returns>Element of enumeration of <typeparamref name="T"/> type, to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public static T ToEnum<T>(this string value) where T : struct
    {
      Assertion.True(typeof(T).IsEnum);

      return Enum.Parse(typeof(T), value, true).To<T>();
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="Guid"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="Guid"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is empty string.</exception>
    /// <seealso cref="ToGuid(string, out Guid)"/>
    public static Guid ToGuid(this string value)
    {
      Assertion.NotEmpty(value);

      return new Guid(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="Guid"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="Guid"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="ToGuid(string)"/>
    public static bool ToGuid(this string value, out Guid result)
    {
      try
      {
        result = new Guid(value);
        return true;
      }
      catch
      {
        result = default(Guid);
        return false;
      }
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="short"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="short"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is empty string.</exception>
    /// <seealso cref="short.Parse(string)"/>
    /// <seealso cref="ToInt16(string, out short)"/>
    public static short ToInt16(this string value)
    {
      Assertion.NotEmpty(value);

      return Int16.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="short"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="short"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="short.TryParse(string, out short)"/>
    /// <seealso cref="ToInt16(string)"/>
    public static bool ToInt16(this string value, out short result)
    {
      return short.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="int"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="int"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is empty string.</exception>
    /// <seealso cref="int.Parse(string)"/>
    /// <seealso cref="ToInt32(string, out int)"/>
    public static int ToInt32(this string value)
    {
      Assertion.NotEmpty(value);

      return Int32.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="int"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="int"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="int.TryParse(string, out int)"/>
    /// <seealso cref="ToInt32(string)"/>
    public static bool ToInt32(this string value, out int result)
    {
      return int.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="long"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="long"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is empty string.</exception>
    /// <seealso cref="long.Parse(string)"/>
    /// <seealso cref="ToInt64(string, out long)"/>
    public static long ToInt64(this string value)
    {
      Assertion.NotEmpty(value);

      return Int64.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="long"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="long"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="long.TryParse(string, out long)"/>
    /// <seealso cref="ToInt64(string)"/>
    public static bool ToInt64(this string value, out long result)
    {
      return long.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="IPAddress"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="IPAddress"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="IPAddress.Parse(string)"/>
    /// <seealso cref="ToIpAddress(string, out IPAddress)"/>
    public static IPAddress ToIpAddress(this string value)
    {
      Assertion.NotEmpty(value);

      return IPAddress.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="IPAddress"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="IPAddress"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="IPAddress.TryParse(string, out IPAddress)"/>
    /// <seealso cref="ToIpAddress(string)"/>
    public static bool ToIpAddress(this string value, out IPAddress result)
    {
      return IPAddress.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="Regex"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="Regex"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public static Regex ToRegex(this string value)
    {
      Assertion.NotNull(value);

      return new Regex(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="Single"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="Single"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is empty string.</exception>
    /// <seealso cref="Single.Parse(string)"/>
    /// <seealso cref="ToSingle(string, out Single)"/>
    public static Single ToSingle(this string value)
    {
      Assertion.NotEmpty(value);

      return Single.Parse(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="Single"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="Single"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="Single.TryParse(string, out Single)"/>
    /// <seealso cref="ToSingle(string)"/>
    public static bool ToSingle(this string value, out Single result)
    {
      return Single.TryParse(value, out result);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="Uri"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <returns>The <see cref="Uri"/> value to which string <paramref name="value"/> was converted.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="ToUri(string, out Uri)"/>
    public static Uri ToUri(this string value)
    {
      Assertion.NotEmpty(value);

      return new Uri(value);
    }

    /// <summary>
    ///   <para>Converts specified string into <see cref="Uri"/> value.</para>
    /// </summary>
    /// <param name="value">String to be converted.</param>
    /// <param name="result">The <see cref="Uri"/> value to which string <paramref name="value"/> was converted.</param>
    /// <returns><c>true</c> if <paramref name="value"/> was successfully converted, <c>false</c> otherwise.</returns>
    /// <seealso cref="ToUri(string)"/>
    public static bool ToUri(this string value, out Uri result)
    {
      return Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result);
    }

    /// <summary>
    ///   <para>Replaces in a string all occurrences of substrings that start with specified marker ("tokens") with specified values.</para>
    /// </summary>
    /// <param name="value">Source string where replacements are to be made.</param>
    /// <param name="marker">Marker string that is used to locate substrings that should be replaced inside a <paramref name="value"/> string.</param>
    /// <param name="tokens">Dictionary of replacement key-value pairs with key representing substring to be replaced (without a marker), and value representing a new string.</param>
    /// <returns>New string with all replacements made.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="value"/>, <paramref name="tokens"/> or <paramref name="marker"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="marker"/> is <see cref="string.Empty"/> string.</exception>
    public static string Tokenize(this string value, IDictionary<string, object> tokens, string marker = ":")
    {
      Assertion.NotNull(value);
      Assertion.NotNull(tokens);
      Assertion.NotEmpty(marker);

      return value.Replace(tokens.Keys.Select(token => "{0}{1}".FormatSelf(marker, token)).ToArray(), tokens.Values.Select(token => token.ToString()).ToArray());
    }

    /// <summary>
    ///   <para>Determines whether the specified string is either <see cref="string.Empty"/> or consists only of white-space characters.</para>
    /// </summary>
    /// <param name="value">A string reference.</param>
    /// <returns><c>true</c> if the <paramref name="value"/> parameter is either empty string or consists of white-space characters; otherwise, <c>false</c>.</returns>
    /// <seealso cref="string.IsNullOrEmpty(string)"/>
    public static bool Whitespace(this string value)
    {
      return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
    }

    /// <summary>
    ///   <para>Splits given string on a newline (<see cref="Environment.NewLine"/>) character into array of strings.</para>
    ///   <para>If source string is <see cref="string.Empty"/>, empty array is returned.</para>
    /// </summary>
    /// <param name="value">Source string to be splitted.</param>
    /// <returns>Target array of strings, which are part of <paramref name="value"/> string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public static string[] Lines(this string value)
    {
      Assertion.NotNull(value);

      return value.IsEmpty() ? Enumerable.Empty<string>().ToArray() : value.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
    }

    /// <summary>
    ///   <para>Prepends specified string to the target one, concatenating both of them.</para>
    /// </summary>
    /// <param name="self">Source string.</param>
    /// <param name="other">String to prepend.</param>
    /// <returns>Concatenated result of <paramref name="other"/> and <paramref name="self"/> string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string Prepend(this string self, string other)
    {
      Assertion.NotNull(self);

      return other + self;
    }

    /// <summary>
    ///   <para>Alters case of all characters inside a string, using provided culture.</para>
    ///   <para>Upper-case characters are converted to lower-case and vice versa.</para>
    /// </summary>
    /// <param name="value">Source string to be converted.</param>
    /// <param name="culture">Culture to use for case conversion, or a <c>null</c> reference to use <see cref="CultureInfo.InvariantCulture"/>.</param>
    /// <returns>Result string with swapped case of characters from <paramref name="value"/> string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public static string SwapCase(this string value, CultureInfo culture = null)
    {
      Assertion.NotNull(value);

      culture = culture ?? CultureInfo.InvariantCulture;

      var sb = new StringBuilder(value.Length);
      foreach (var character in value)
      {
        sb.Append(char.IsUpper(character) ? char.ToLower(character, culture) : char.ToUpper(character, culture));
      }

      return sb.ToString();
    }
  }
}