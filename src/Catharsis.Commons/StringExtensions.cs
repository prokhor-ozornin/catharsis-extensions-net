using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for basic string type.</para>
/// </summary>
/// <seealso cref="string"/>
public static class StringExtensions
{
  /// <summary>
  ///   <para>Determines where a string is either <c>null</c> reference or is <see cref="string.Empty"/>.</para>
  /// </summary>
  /// <param name="text">String to evaluate.</param>
  /// <returns><c>true</c> if <paramref name="text"/> is either a <c>null</c> reference or an empty string.</returns>
  /// <seealso cref="string.IsNullOrEmpty(string)"/>
  public static bool IsEmpty(this string text) => string.IsNullOrWhiteSpace(text);

  /// <summary>
  ///   <para>Determines whether a source string represents a valid <see cref="bool"/> value.</para>
  /// </summary>
  /// <param name="text">Source string to evaluate.</param>
  /// <returns><c>true</c> if <paramref name="text"/> represents a valid value of <see cref="bool"/> type, <c>false</c> otherwise.</returns>
  public static bool IsBoolean(this string text) => text.ToBoolean(out _);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsSbyte(this string text, IFormatProvider format = null) => text.ToSbyte(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsByte(this string text, IFormatProvider format = null) => text.ToByte(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsShort(this string text, IFormatProvider format = null) => text.ToShort(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsUshort(this string text, IFormatProvider format = null) => text.ToUshort(out _, format);

  /// <summary>
  ///   <para>Determines whether a source string represents a valid integer value.</para>
  /// </summary>
  /// <param name="text">Source string to evaluate.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> represents a valid value of integer type, <c>false</c> otherwise.</returns>
  public static bool IsInt(this string text, IFormatProvider format = null) => text.ToInt(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsUint(this string text, IFormatProvider format = null) => text.ToUint(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsLong(this string text, IFormatProvider format = null) => text.ToLong(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsUlong(this string text, IFormatProvider format = null) => text.ToUlong(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsFloat(this string text, IFormatProvider format = null) => text.ToFloat(out _, format);

  /// <summary>
  ///   <para>Determines whether a source string represents a valid <see cref="double"/> value.</para>
  /// </summary>
  /// <param name="text">Source string to evaluate.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> represents a valid value of <see cref="double"/> type, <c>false</c> otherwise.</returns>
  public static bool IsDouble(this string text, IFormatProvider format = null) => text.ToDouble(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsDecimal(this string text, IFormatProvider format = null) => text.ToDecimal(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="text"></param>
  /// <returns></returns>
  public static bool IsEnum<T>(this string text) where T : struct => text.ToEnum<T>(out _);

  /// <summary>
  ///   <para>Determines whether a source string represents a valid <see cref="Guid"/> value.</para>
  /// </summary>
  /// <param name="text">Source string to evaluate.</param>
  /// <returns><c>true</c> if <paramref name="text"/> represents a valid value of <see cref="Guid"/> type, <c>false</c> otherwise.</returns>
  public static bool IsGuid(this string text) => text.ToGuid(out _);

  /// <summary>
  ///   <para>Determines whether a source string represents a valid <see cref="Uri"/> value.</para>
  /// </summary>
  /// <param name="text">Source string to evaluate.</param>
  /// <returns><c>true</c> if <paramref name="text"/> represents a valid value of <see cref="Uri"/> type, <c>false</c> otherwise.</returns>
  public static bool IsUri(this string text) => text.ToUri(out _);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static bool IsType(this string text) => text.ToType(out _);

  /// <summary>
  ///   <para>Determines whether a source string represents a valid <see cref="DateTime"/> value.</para>
  /// </summary>
  /// <param name="text">Source string to evaluate.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> represents a valid value of <see cref="DateTime"/> type, <c>false</c> otherwise.</returns>
  public static bool IsDateTime(this string text, IFormatProvider format = null) => text.ToDateTime(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsDateTimeOffset(this string text, IFormatProvider format = null) => text.ToDateTimeOffset(out _, format);

#if NET6_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsDateOnly(this string text, IFormatProvider format = null) => text.ToDateOnly(out _, format);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool IsTimeOnly(this string text, IFormatProvider format = null) => text.ToTimeOnly(out _, format);
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static bool IsFile(this string text) => text.ToFile(out _);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static bool IsDirectory(this string text) => text.ToDirectory(out _);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static bool IsIpAddress(this string text) => text.ToIpAddress(out _);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static string Min(this string left, string right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length <= right.Length ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static string Max(this string left, string right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length >= right.Length ? left : right;
  }
  

  /// <summary>
  ///   <para>Compares two specified strings and returns an integer that indicates their relative position in the sort order.</para>
  /// </summary>
  /// <param name="left">The current string to compare with the second.</param>
  /// <param name="right">The second string to compare with the current.</param>
  /// <param name="culture"></param>
  /// <returns>Integer value that indicates the lexical relationship between the two comparands.</returns>
  /// <seealso cref="string.Compare(string, string, StringComparison)"/>
  public static int Compare(this string left, string right, CultureInfo culture = null) => string.Compare(left, right, true, culture ?? CultureInfo.InvariantCulture);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static int CompareAsNumber(this string left, string right, IFormatProvider format = null) => left != null && right != null ? left.ToDouble(format ?? CultureInfo.InvariantCulture).CompareTo(right.ToDouble(format ?? CultureInfo.InvariantCulture)) : left.Compare(right);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static int CompareAsDate(this string left, string right, IFormatProvider format = null) => left != null && right != null ? left.ToDateTimeOffset(format ?? CultureInfo.InvariantCulture).CompareTo(right.ToDateTimeOffset(format ?? CultureInfo.InvariantCulture)) : left.Compare(right);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static string Append(this string left, string right) => left + right;

  /// <summary>
  ///   <para>Prepends specified string to the target one, concatenating both of them.</para>
  /// </summary>
  /// <param name="left">Source string.</param>
  /// <param name="right">String to prepend.</param>
  /// <returns>Concatenated result of <paramref name="right"/> and <paramref name="left"/> string.</returns>
  public static string Prepend(this string left, string right) => right + left;

  /// <summary>
  ///   <para>Removes specified number of characters from the beginning of a string.</para>
  /// </summary>
  /// <param name="text">String to be altered.</param>
  /// <param name="offset"></param>
  /// <param name="count">Number of characters to drop.</param>
  /// <param name="condition"></param>
  /// <returns>Resulting string with removed characters.</returns>
  public static string RemoveRange(this string text, int offset, int? count = null, Predicate<char> condition = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
    if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return condition != null ? text.Skip(offset).Where(character => condition(character)).AsArray().ToText() : count != null ? text.Remove(offset, count.Value) : text.Remove(offset);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string Reverse(this string text) => text is not null ? text.Length > 0 ? text.Reverse<char>().ToArray().ToText() : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="replacements"></param>
  /// <returns></returns>
  public static string Replace(this string text, IEnumerable<(string Name, object Value)> replacements)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (replacements is null) throw new ArgumentNullException(nameof(replacements));

    if (text.Length == 0)
    {
      return text;
    }

    var result = new StringBuilder(text);

    replacements.ForEach(replacement => result.Replace(replacement.Name, replacement.Value?.ToInvariantString() ?? string.Empty));

    return result.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="replacements"></param>
  /// <returns></returns>
  public static string Replace(this string text, params (string Name, object Value)[] replacements) => text.Replace(replacements as IEnumerable<(string Name, object Value)>);

  /// <summary>
  ///   <para>Alters case of all characters inside a string, using provided culture.</para>
  ///   <para>Upper-case characters are converted to lower-case and vice versa.</para>
  /// </summary>
  /// <param name="text">Source string to be converted.</param>
  /// <param name="culture"></param>
  /// <returns>Result string with swapped case of characters from <paramref name="text"/> string.</returns>
  public static string SwapCase(this string text, CultureInfo culture = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    if (text.Length == 0)
    {
      return text;
    }

    var result = new StringBuilder(text.Length);

    foreach (var character in text)
    {
      result.Append(char.IsUpper(character) ? culture != null ? char.ToLower(character, culture) : char.ToLower(character) : culture != null ? char.ToUpper(character, culture) : char.ToUpper(character));
    }

    return result.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public static string Capitalize(this string text, CultureInfo culture = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    if (text.Length == 0 || char.IsUpper(text, 0))
    {
      return text;
    }

    var chars = text.ToCharArray();

    chars[0] = culture != null ? char.ToUpper(chars[0], culture) : char.ToUpper(chars[0]);

    return chars.ToText();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string CapitalizeAll(this string text) => text is not null ? text.Length > 0 ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Multiplies/repeats value of source string given number of times, returning resulting string.</para>
  /// </summary>
  /// <param name="text">String to repeat.</param>
  /// <param name="count">Number of repeats.</param>
  /// <returns>Resulting string, consisting of <paramref name="text"/>'s data that has been repeated <paramref name="count"/> times.</returns>
  public static string Repeat(this string text, int count)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (text.Length == 0 || count == 0)
    {
      return string.Empty;
    }

    var result = new StringBuilder(text.Length * count);

    count.Times(() => result.Append(text));

    return result.ToString();
  }

  /// <summary>
  ///   <para>Splits given string on a newline (<see cref="Environment.NewLine"/>) character into array of strings.</para>
  ///   <para>If source string is <see cref="string.Empty"/>, empty array is returned.</para>
  /// </summary>
  /// <param name="text">Source string to be split.</param>
  /// <param name="separator"></param>
  /// <returns>Target array of strings, which are part of <paramref name="text"/> string.</returns>
  public static string[] Lines(this string text, string separator = null) => text is not null ? text.Length > 0 ? text.Split(separator ?? Environment.NewLine) : Array.Empty<string>() : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts a BASE64-encoded string to an equivalent 8-bit unsigned integer array.</para>
  /// </summary>
  /// <param name="text">BASE64-encoded string to be converted to binary form.</param>
  /// <returns>An array of 8-bit unsigned integers that is equivalent to <paramref name="text"/>.</returns>
  /// <seealso cref="System.Convert.FromBase64String(string)"/>
  public static byte[] FromBase64(this string text) => text is not null ? text.Length > 0 ? System.Convert.FromBase64String(text) : Array.Empty<byte>() : throw new ArgumentNullException(nameof(text));

  #if NET6_0
  /// <summary>
  ///   <para>Converts HEX-encoded string into a sequence of bytes.</para>
  /// </summary>
  /// <param name="text">HEX-encoded string to be converted to byte sequence.</param>
  /// <returns>Decoded data from HEX-encoded <paramref name="text"/> string.</returns>
  public static byte[] FromHex(this string text) => text is not null ? text.Length > 0 ? System.Convert.FromHexString(text) : Array.Empty<byte>() : throw new ArgumentNullException(nameof(text));
#endif

  /// <summary>
  ///   <para>URL-encodes string.</para>
  /// </summary>
  /// <param name="text">String to encode.</param>
  /// <returns>Encoded string.</returns>
  public static string UrlEncode(this string text) => text is not null ? text.Length > 0 ? Uri.EscapeDataString(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Decodes URL-encoded string.</para>
  /// </summary>
  /// <param name="text">String to decode.</param>
  /// <returns>Decoded string.</returns>
  public static string UrlDecode(this string text) => text is not null ? text.Length > 0 ? Uri.UnescapeDataString(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts a string to an HTML-encoded string.</para>
  /// </summary>
  /// <param name="text">String to convert to HTML-encoded version.</param>
  /// <returns>HTML-encoded version of <paramref name="text"/>.</returns>
  /// <seealso cref="WebUtility.HtmlEncode(string)"/>
  public static string HtmlEncode(this string text) => text is not null ? text.Length > 0 ? WebUtility.HtmlEncode(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.</para>
  /// </summary>
  /// <param name="text">HTML-encoded version of string.</param>
  /// <returns>HTML-decoded version of <paramref name="text"/>.</returns>
  /// <seealso cref="WebUtility.HtmlDecode(string)"/>
  public static string HtmlDecode(this string text) => text is not null ? text.Length > 0 ? WebUtility.HtmlDecode(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="value"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static string Indent(this string text, char value, int count = 1)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return text;
    }

    var indent = value.Repeat(count);

    if (text.Length == 0)
    {
      return indent;
    }

    var result = new StringBuilder();

    var lines = text.Lines().AsArray();

    lines.ForEach((index, line) =>
    {
      result.Append(indent);
      result.Append(line.Trim());

      if (index < lines.Length - 1)
      {
        result.AppendLine();
      }
    });

    return result.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public static string Unindent(this string text, char value)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    if (text.Length == 0)
    {
      return text;
    }

    var result = new StringBuilder(text);

    foreach (var line in text.Lines())
    {
      result.AppendLine(line.SkipWhile(character => character == value).ToArray().ToText());
    }

    return result.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static string Spacify(this string text, int count = 1) => text.Indent(' ', count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string Unspacify(this string text) => text.Unindent(' ');

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static string Tabify(this string text, int count = 1) => text.Indent('\t', count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string Untabify(this string text) => text.Unindent('\t');

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="command"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  public static Process Execute(this string command, IEnumerable<string> arguments = null)
  {
    if (command is null) throw new ArgumentNullException(nameof(command));

    var process = command.ToProcess();

    if (arguments != null)
    {
      process.StartInfo.ArgumentList.AddRange(arguments);
    }
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.RedirectStandardError = true;
    process.StartInfo.RedirectStandardInput = true;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.UseShellExecute = false;

    process.Start();

    return process;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="command"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  public static Process Execute(this string command, params string[] arguments) => command.Execute(arguments as IEnumerable<string>);

  /// <summary>
  ///   <para>Converts string to a sequence of bytes, using specified <see cref="Encoding"/>.</para>
  /// </summary>
  /// <param name="text">String that consists of characters that are to be transformed to bytes.</param>
  /// <param name="encoding">Encoding to be used for transformation between characters of <paramref name="text"/> and their bytes equivalents. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
  /// <returns>Sequence of bytes that form <paramref name="text"/> string in given <paramref name="encoding"/>.</returns>
  /// <seealso cref="Encoding.GetBytes(string)"/>
  public static byte[] ToBytes(this string text, Encoding encoding = null) => text is not null ? text.Length > 0 ? (encoding ?? Encoding.Default).GetBytes(text) : Array.Empty<byte>() : throw new ArgumentNullException(nameof(text));

    /// <summary>
  ///   <para>Converts specified string into <see cref="bool"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <returns><c>true</c> if <paramref name="text"/> is equivalent to <see cref="bool.TrueString"/>, <c>false otherwise</c>.</returns>
  /// <seealso cref="bool.Parse(string)"/>
  public static bool ToBoolean(this string text) => text is not null ? bool.Parse(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///  <para>Converts specified string into <see cref="bool"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result"><c>true</c> if <paramref name="text"/> is equivalent to <see cref="bool.TrueString"/>, <c>false otherwise</c>.</param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="bool.TryParse(string, out bool)"/>
  public static bool ToBoolean(this string text, out bool? result) => (result = bool.TryParse(text, out var value) ? value : null) != null;
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static sbyte ToSbyte(this string text, IFormatProvider format = null) => text is not null ? sbyte.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool ToSbyte(this string text, out sbyte? result, IFormatProvider format = null) => (result = sbyte.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="byte"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="byte"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="byte.Parse(string)"/>
  public static byte ToByte(this string text, IFormatProvider format = null) => text is not null ? byte.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="bool"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="byte"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="byte.TryParse(string, out byte)"/>
  public static bool ToByte(this string text, out byte? result, IFormatProvider format = null) => (result = byte.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="short"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="short"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="short.Parse(string)"/>
  public static short ToShort(this string text, IFormatProvider format = null) => text is not null ? short.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="short"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="short"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="short.TryParse(string, out short)"/>
  public static bool ToShort(this string text, out short? result, IFormatProvider format = null) => (result = short.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static ushort ToUshort(this string text, IFormatProvider format = null) => text is not null ? ushort.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool ToUshort(this string text, out ushort? result, IFormatProvider format = null) => (result = ushort.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="int"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="int"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="int.Parse(string)"/>
  public static int ToInt(this string text, IFormatProvider format = null) => text is not null ? int.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="int"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="int"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="int.TryParse(string, out int)"/>
  public static bool ToInt(this string text, out int? result, IFormatProvider format = null) => (result = int.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static uint ToUint(this string text, IFormatProvider format = null) => text is not null ? uint.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool ToUint(this string text, out uint? result, IFormatProvider format = null) => (result = uint.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="long"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="long"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="long.Parse(string)"/>
  public static long ToLong(this string text, IFormatProvider format = null) => text is not null ? long.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="long"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="long"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="long.TryParse(string, out long)"/>
  public static bool ToLong(this string text, out long? result, IFormatProvider format = null) => (result = long.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static ulong ToUlong(this string text, IFormatProvider format = null) => text is not null ? ulong.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool ToUlong(this string text, out ulong? result, IFormatProvider format = null) => (result = ulong.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="float"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="float"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="float.Parse(string)"/>
  public static float ToFloat(this string text, IFormatProvider format = null) => text is not null ? float.Parse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="float"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="float"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="float.TryParse(string, out float)"/>
  public static bool ToFloat(this string text, out float? result, IFormatProvider format = null) => (result = float.TryParse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="double"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="double"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="double.Parse(string)"/>
  public static double ToDouble(this string text, IFormatProvider format = null) => text is not null ? double.Parse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="double"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="double"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="double.TryParse(string, out double)"/>
  public static bool ToDouble(this string text, out double? result, IFormatProvider format = null) => (result = double.TryParse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="decimal"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="decimal"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="decimal.Parse(string)"/>
  public static decimal ToDecimal(this string text, IFormatProvider format = null) => text is not null ? decimal.Parse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="decimal"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="decimal"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="decimal.TryParse(string, out decimal)"/>
  public static bool ToDecimal(this string text, out decimal? result, IFormatProvider format = null) => (result = decimal.TryParse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into enumeration of specified type.</para>
  /// </summary>
  /// <typeparam name="T">Type of enumeration.</typeparam>
  /// <param name="text">String to be converted.</param>
  /// <returns>Element of enumeration of <typeparamref name="T"/> type, to which string <paramref name="text"/> was converted.</returns>
  public static T ToEnum<T>(this string text) where T : struct => text is not null ? Enum.Parse<T>(text, true) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  public static bool ToEnum<T>(this string text, out T? result) where T : struct
  {
    try
    {
      result = text.ToEnum<T>();
      return result is not null;
    }
    catch
    {
      result = null;
      return false;
    }
  }

  /// <summary>
  ///   <para>Converts specified string into <see cref="Guid"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <returns>The <see cref="Guid"/> value to which string <paramref name="text"/> was converted.</returns>
  public static Guid ToGuid(this string text) => text is not null ? Guid.Parse(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="Guid"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="Guid"/> value to which string <paramref name="text"/> was converted.</param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  public static bool ToGuid(this string text, out Guid? result) => (result = Guid.TryParse(text, out var value) ? value : null) != null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="Uri"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <returns>The <see cref="Uri"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="ToUri(string, out Uri)"/>
  public static Uri ToUri(this string text) => text is not null ? new Uri(text, UriKind.RelativeOrAbsolute) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="Uri"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="Uri"/> value to which string <paramref name="text"/> was converted.</param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="ToUri(string)"/>
  public static bool ToUri(this string text, out Uri result) => Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out result);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static Type ToType(this string text) => text is not null ? Type.GetType(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  public static bool ToType(this string text, out Type result)
  {
    try
    {
      result = text.ToType();
      return result is not null;
    }
    catch
    {
      result = null;
      return false;
    }
  }

  /// <summary>
  ///   <para>Converts specified string into <see cref="DateTime"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="DateTime"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="DateTime.Parse(string)"/>
  public static DateTime ToDateTime(this string text, IFormatProvider format = null) => text is not null ? DateTime.Parse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="DateTime"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="DateTime"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="DateTime.TryParse(string, out DateTime)"/>
  public static bool ToDateTime(this string text, out DateTime? result, IFormatProvider format = null) => (result = DateTime.TryParse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out var value) ? value : null) != null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static DateTimeOffset ToDateTimeOffset(this string text, IFormatProvider format = null) => text is not null ? DateTimeOffset.Parse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool ToDateTimeOffset(this string text, out DateTimeOffset? result, IFormatProvider format = null) => (result = DateTimeOffset.TryParse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out var value) ? value : null) != null;

#if NET6_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static DateOnly ToDateOnly(this string text, IFormatProvider format = null) => text is not null ? DateOnly.Parse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool ToDateOnly(this string text, out DateOnly? result, IFormatProvider format = null) => (result = DateOnly.TryParse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out var value) ? value : null) != null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static TimeOnly ToTimeOnly(this string text, IFormatProvider format = null) => text is not null ? TimeOnly.Parse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static bool ToTimeOnly(this string text, out TimeOnly? result, IFormatProvider format = null) => (result = TimeOnly.TryParse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out var value) ? value : null) != null;
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static FileInfo ToFile(this string text) => text is not null ? new FileInfo(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  public static bool ToFile(this string text, out FileInfo result)
  {
    try
    {
      result = text.ToFile();
      return result.Exists;
    }
    catch
    {
      result = null;
      return false;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static DirectoryInfo ToDirectory(this string text) => text is not null ? new DirectoryInfo(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  public static bool ToDirectory(this string text, out DirectoryInfo result)
  {
    try
    {
      result = text.ToDirectory();
      return result.Exists;
    }
    catch
    {
      result = null;
      return false;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string ToPath(this string text) => text is not null ? Path.GetFullPath(text.ToUri().LocalPath).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static IPAddress ToIpAddress(this string text) => text is not null ? IPAddress.Parse(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  public static bool ToIpAddress(this string text, out IPAddress result) => IPAddress.TryParse(text, out result);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static IPHostEntry ToIpHost(this string text)
  {
    text.ToIpAddress(out var ip);

    return ip != null ? new IPHostEntry { AddressList = new[] { ip } } : new IPHostEntry { HostName = text };
  }

  /// <summary>
  ///   <para>Converts specified string into <see cref="Regex"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="options"></param>
  /// <returns>The <see cref="Regex"/> value to which string <paramref name="text"/> was converted.</returns>
  public static Regex ToRegex(this string text, RegexOptions options = RegexOptions.None) => text is not null ? new Regex(text, options) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static StringBuilder ToStringBuilder(this string text) => text is not null ? new StringBuilder(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  public static StringReader ToStringReader(this string text) => text is not null ? new StringReader(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="info"></param>
  /// <returns></returns>
  public static Process ToProcess(this string text, ProcessStartInfo info = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    var process = new Process();

    if (info != null)
    {
      process.StartInfo = info;
    }

    process.StartInfo.FileName = text;

    return process;
  }
}