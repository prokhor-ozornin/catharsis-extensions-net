using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for basic string type.</para>
/// </summary>
/// <seealso cref="string"/>
public static class StringExtensions
{
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
  /// <seealso cref="CompareAsDate(string, string, IFormatProvider)"/>
  public static int CompareAsNumber(this string left, string right, IFormatProvider format = null) => left is not null && right is not null ? left.ToDouble(format ?? CultureInfo.InvariantCulture).CompareTo(right.ToDouble(format ?? CultureInfo.InvariantCulture)) : left.Compare(right);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <seealso cref="CompareAsNumber(string, string, IFormatProvider)"/>
  public static int CompareAsDate(this string left, string right, IFormatProvider format = null) => left is not null && right is not null ? left.ToDateTimeOffset(format ?? CultureInfo.InvariantCulture).CompareTo(right.ToDateTimeOffset(format ?? CultureInfo.InvariantCulture)) : left.Compare(right);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <seealso cref="Prepend(string, string)"/>
  public static string Append(this string left, string right) => left + right;

  /// <summary>
  ///   <para>Prepends specified string to the target one, concatenating both of them.</para>
  /// </summary>
  /// <param name="left">Source string.</param>
  /// <param name="right">String to prepend.</param>
  /// <returns>Concatenated result of <paramref name="right"/> and <paramref name="left"/> string.</returns>
  /// <seealso cref="Append(string, string)"/>
  public static string Prepend(this string left, string right) => right + left;
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="replacements"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="replacements"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Replace(string, ValueTuple{string, object}[])"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Replace(string, IEnumerable{ValueTuple{string, object}})"/>
  public static string Replace(this string text, params (string Name, object Value)[] replacements) => text.Replace(replacements as IEnumerable<(string Name, object Value)>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static string Reverse(this string text) => text is not null ? text.Length > 0 ? text.Reverse<char>().ToArray().ToText() : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Multiplies/repeats value of source string given number of times, returning resulting string.</para>
  /// </summary>
  /// <param name="text">String to repeat.</param>
  /// <param name="count">Number of repeats.</param>
  /// <returns>Resulting string, consisting of <paramref name="text"/>'s data that has been repeated <paramref name="count"/> times.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static string[] Lines(this string text, string separator = null) => text is not null ? text.Length > 0 ? text.Split(separator ?? Environment.NewLine) : [] : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Alters case of all characters inside a string, using provided culture.</para>
  ///   <para>Upper-case characters are converted to lower-case and vice versa.</para>
  /// </summary>
  /// <param name="text">Source string to be converted.</param>
  /// <param name="culture"></param>
  /// <returns>Result string with swapped case of characters from <paramref name="text"/> string.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
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
      result.Append(char.IsUpper(character) ? culture is not null ? char.ToLower(character, culture) : char.ToLower(character) : culture is not null ? char.ToUpper(character, culture) : char.ToUpper(character));
    }

    return result.ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="CapitalizeAll(string, CultureInfo)"/>
  public static string Capitalize(this string text, CultureInfo culture = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    if (text.Length == 0 || char.IsUpper(text, 0))
    {
      return text;
    }

    var chars = text.ToCharArray();

    chars[0] = culture is not null ? char.ToUpper(chars[0], culture) : char.ToUpper(chars[0]);

    return chars.ToText();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Capitalize(string, CultureInfo)"/>
  public static string CapitalizeAll(this string text, CultureInfo culture = null) => text is not null ? text.Length > 0 ? (culture ?? CultureInfo.CurrentCulture).TextInfo.ToTitleCase(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="value"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Unindent(string, char)"/>
  public static string Indent(this string text, char value, int count = 1)
  {
    if (text is null)
      throw new ArgumentNullException(nameof(text));
    if (count < 0)
      throw new ArgumentOutOfRangeException(nameof(count));

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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Indent(string, char, int)"/>
  public static string Unindent(this string text, char value)
  {
    if (text is null)
      throw new ArgumentNullException(nameof(text));

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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Unspacify(string)"/>
  public static string Spacify(this string text, int count = 1) => text.Indent(' ', count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Spacify(string, int)"/>
  public static string Unspacify(this string text) => text.Unindent(' ');

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Untabify(string)"/>
  public static string Tabify(this string text, int count = 1) => text.Indent('\t', count);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Tabify(string, int)"/>
  public static string Untabify(this string text) => text.Unindent('\t');

  /// <summary>
  ///   <para>Determines whether a string matches specified regular expression.</para>
  /// </summary>
  /// <param name="text">The string to search for a match.</param>
  /// <param name="pattern">The regular expression pattern to match.</param>
  /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
  /// <returns><c>true</c> if <paramref name="text"/> matches <paramref name="pattern"/> regular expression, <c>false</c> if not.</returns>
  /// <seealso cref="Regex.IsMatch(string, string)"/>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="pattern"/> is <see langword="null"/>.</exception>
  public static bool IsMatch(this string text, string pattern, RegexOptions? options = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (pattern is null) throw new ArgumentNullException(nameof(pattern));

    return options is not null ? Regex.IsMatch(text, pattern, options.Value) : Regex.IsMatch(text, pattern);
  }

  /// <summary>
  ///   <para>Searches source input string for all occurrences of a specified regular expression, using the specified matching options.</para>
  /// </summary>
  /// <param name="text">The string to search for a match.</param>
  /// <param name="pattern">The regular expression pattern to match.</param>
  /// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
  /// <returns>A collection of the <see cref="Match"/> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="pattern"/> is <see langword="null"/>.</exception>
  public static IEnumerable<Match> Matches(this string text, string pattern, RegexOptions? options = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (pattern is null) throw new ArgumentNullException(nameof(pattern));

    return options is not null ? Regex.Matches(text, pattern, options.Value) : Regex.Matches(text, pattern);
  }

  /// <summary>
  ///   <para>Determines where a string is either <c>null</c> reference or is <see cref="string.Empty"/>.</para>
  /// </summary>
  /// <param name="text">String to evaluate.</param>
  /// <returns><c>true</c> if <paramref name="text"/> is either a <c>null</c> reference or an empty string.</returns>
  /// <seealso cref="string.IsNullOrWhiteSpace(string)"/>
  public static bool IsUnset(this string text) => string.IsNullOrWhiteSpace(text);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsLowerCased(string)"/>
  public static bool IsUpperCased(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    return text.All(char.IsUpper);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUpperCased(string)"/>
  public static bool IsLowerCased(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    return text.All(char.IsLower);
  }

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

#if NET8_0
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
  /// <param name="command"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="command"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Execute(string, string[])"/>
  public static Process Execute(this string command, IEnumerable<string> arguments = null)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    var process = command.ToProcess();

    if (arguments is not null)
    {
      process.StartInfo.ArgumentList.With(arguments);
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
  /// <exception cref="ArgumentNullException">If <paramref name="command"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Execute(string, IEnumerable{string})"/>
  public static Process Execute(this string command, params string[] arguments) => command.Execute(arguments as IEnumerable<string>);

  /// <summary>
  ///   <para>Converts a BASE64-encoded string to an equivalent 8-bit unsigned integer array.</para>
  /// </summary>
  /// <param name="text">BASE64-encoded string to be converted to binary form.</param>
  /// <returns>An array of 8-bit unsigned integers that is equivalent to <paramref name="text"/>.</returns>
  /// <seealso cref="System.Convert.FromBase64String(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static byte[] FromBase64(this string text) => text is not null ? text.Length > 0 ? Convert.FromBase64String(text) : [] : throw new ArgumentNullException(nameof(text));

#if NET8_0
  /// <summary>
  ///   <para>Converts HEX-encoded string into a sequence of bytes.</para>
  /// </summary>
  /// <param name="text">HEX-encoded string to be converted to byte sequence.</param>
  /// <returns>Decoded data from HEX-encoded <paramref name="text"/> string.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static byte[] FromHex(this string text) => text is not null ? text.Length > 0 ? Convert.FromHexString(text) : [] : throw new ArgumentNullException(nameof(text));
#endif

  /// <summary>
  ///   <para>URL-encodes string.</para>
  /// </summary>
  /// <param name="text">String to encode.</param>
  /// <returns>Encoded string.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="UrlDecode(string)"/>
  public static string UrlEncode(this string text) => text is not null ? text.Length > 0 ? Uri.EscapeDataString(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Decodes URL-encoded string.</para>
  /// </summary>
  /// <param name="text">String to decode.</param>
  /// <returns>Decoded string.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="UrlEncode(string)"/>
  public static string UrlDecode(this string text) => text is not null ? text.Length > 0 ? Uri.UnescapeDataString(text) : string.Empty : throw new ArgumentNullException(nameof(text));
  
  /// <summary>
  ///   <para>Converts a string to an HTML-encoded string.</para>
  /// </summary>
  /// <param name="text">String to convert to HTML-encoded version.</param>
  /// <returns>HTML-encoded version of <paramref name="text"/>.</returns>
  /// <seealso cref="WebUtility.HtmlEncode(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="HtmlDecode(string)"/>
  public static string HtmlEncode(this string text) => text is not null ? text.Length > 0 ? WebUtility.HtmlEncode(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.</para>
  /// </summary>
  /// <param name="text">HTML-encoded version of string.</param>
  /// <returns>HTML-decoded version of <paramref name="text"/>.</returns>
  /// <seealso cref="WebUtility.HtmlDecode(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="HtmlEncode(string)"/>
  public static string HtmlDecode(this string text) => text is not null ? text.Length > 0 ? WebUtility.HtmlDecode(text) : string.Empty : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="algorithm"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="algorithm"/> is <see langword="null"/>.</exception>
  public static string Hash(this string text, HashAlgorithm algorithm) => text.ToBytes(Encoding.UTF8).Hash(algorithm).ToHex();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashMd5(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = MD5.Create();

    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashSha1(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = SHA1.Create();

    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashSha256(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = SHA256.Create();

    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashSha384(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = SHA384.Create();

    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static string HashSha512(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var algorithm = SHA512.Create();

    return text.Hash(algorithm);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Max(string, string)"/>
  /// <seealso cref="MinMax(string, string)"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Min(string, string)"/>
  /// <seealso cref="MinMax(string, string)"/>
  public static string Max(this string left, string right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length >= right.Length ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Min(string, string)"/>
  /// <seealso cref="Max(string, string)"/>
  public static (string Min, string Max) MinMax(this string left, string right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length <= right.Length ? (left, right) : (right, left);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="characters"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="characters"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(string, char[])"/>
  public static string With(this string text, IEnumerable<char> characters)
  {
    if (text is null)
      throw new ArgumentNullException(nameof(text));
    if (characters is null)
      throw new ArgumentNullException(nameof(characters));

    return text + characters.ToText();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="characters"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(string, IEnumerable{char})"/>
  public static string With(this string text, params char[] characters) => text.With(characters as IEnumerable<char>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="positions"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="positions"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(string, int[])"/>
  /// <seealso cref="Without(string, int, int?, Predicate{char})"/>
  public static string Without(this string text, IEnumerable<int> positions)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (positions is null) throw new ArgumentNullException(nameof(positions));

    return text.ToStringBuilder().Without(positions).ToString();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="positions"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(string, IEnumerable{int})"/>
  /// <seealso cref="Without(string, int, int?, Predicate{char})"/>
  public static string Without(this string text, params int[] positions) => text.Without(positions as IEnumerable<int>);

  /// <summary>
  ///   <para>Removes specified number of characters from the beginning of a string.</para>
  /// </summary>
  /// <param name="text">String to be altered.</param>
  /// <param name="offset"></param>
  /// <param name="count">Number of characters to drop.</param>
  /// <param name="condition"></param>
  /// <returns>Resulting string with removed characters.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Without(string, IEnumerable{int})"/>
  /// <seealso cref="Without(string, int[])"/>
  public static string Without(this string text, int offset, int? count = null, Predicate<char> condition = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
    if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return condition is not null ? text.Skip(offset).Where(character => condition(character)).AsArray().ToText() : count is not null ? text.Remove(offset, count.Value) : text.Remove(offset);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="text"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static T DeserializeAsDataContract<T>(this string text, params Type[] types)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var reader = text.ToXmlReader();

    return reader.DeserializeAsDataContract<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="text"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static T DeserializeAsXml<T>(this string text, params Type[] types)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var reader = text.ToXmlReader();

    return reader.DeserializeAsXml<T>(types);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, Stream, Encoding, CancellationToken)"/>
  public static string WriteTo(this string text, Stream destination, Encoding encoding = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text, encoding);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, Stream, Encoding)"/>
  public static async Task<string> WriteToAsync(this string text, Stream destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, TextWriter, CancellationToken)"/>
  public static string WriteTo(this string text, TextWriter destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, TextWriter)"/>
  public static async Task<string> WriteToAsync(this string text, TextWriter destination, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteTextAsync(text, cancellation).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  public static string WriteTo(this string text, BinaryWriter destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, XmlWriter)"/>
  public static string WriteTo(this string text, XmlWriter destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, XmlWriter)"/>
  public static async Task<string> WriteToAsync(this string text, XmlWriter destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteTextAsync(text).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, FileInfo, Encoding, CancellationToken)"/>
  public static string WriteTo(this string text, FileInfo destination, Encoding encoding = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text, encoding);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, FileInfo, Encoding)"/>
  public static async Task<string> WriteToAsync(this string text, FileInfo destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, Process, CancellationToken)"/>
  public static string WriteTo(this string text, Process destination)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, Process)"/>
  public static async Task<string> WriteToAsync(this string text, Process destination, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    await destination.WriteTextAsync(text, cancellation).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, Uri, Encoding, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/>
  public static string WriteTo(this string text, Uri destination, Encoding encoding = null, TimeSpan? timeout = null, params (string Name, object Value)[] headers)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteBytes(text.ToBytes(encoding), timeout, headers);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="timeout"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/>
  public static async Task<string> WriteToAsync(this string text, Uri destination, Encoding encoding = null, TimeSpan? timeout = null, CancellationToken cancellation = default, params (string Name, object Value)[] headers)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteBytesAsync(text.ToBytes(encoding), timeout, cancellation, headers).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="client"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/>, <paramref name="client"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, HttpClient, Uri, CancellationToken)"/>
  public static HttpContent WriteTo(this string text, HttpClient client, Uri destination) => client.WriteText(text, destination);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="client"></param>
  /// <param name="destination"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/>, <paramref name="client"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, HttpClient, Uri)"/>
  public static async Task<HttpContent> WriteToAsync(this string text, HttpClient client, Uri destination, CancellationToken cancellation = default) => await client.WriteTextAsync(text, destination, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, TcpClient, Encoding, CancellationToken)"/>
  public static string WriteTo(this string text, TcpClient destination, Encoding encoding = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text, encoding);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, TcpClient, Encoding)"/>
  public static async Task<string> WriteToAsync(this string text, TcpClient destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteToAsync(string, UdpClient, Encoding, CancellationToken)"/>
  public static string WriteTo(this string text, UdpClient destination, Encoding encoding = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text, encoding);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <param name="encoding"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="destination"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTo(string, UdpClient, Encoding)"/>
  public static async Task<string> WriteToAsync(this string text, UdpClient destination, Encoding encoding = null, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    cancellation.ThrowIfCancellationRequested();

    await destination.WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);

    return text;
  }

  /// <summary>
  ///   <para>Converts string to a sequence of bytes, using specified <see cref="Encoding"/>.</para>
  /// </summary>
  /// <param name="text">String that consists of characters that are to be transformed to bytes.</param>
  /// <param name="encoding">Encoding to be used for transformation between characters of <paramref name="text"/> and their bytes equivalents. If not specified, default <see cref="Encoding.UTF8"/> is used.</param>
  /// <returns>Sequence of bytes that form <paramref name="text"/> string in given <paramref name="encoding"/>.</returns>
  /// <seealso cref="Encoding.GetBytes(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static byte[] ToBytes(this string text, Encoding encoding = null) => text is not null ? text.Length > 0 ? (encoding ?? Encoding.Default).GetBytes(text) : [] : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="bool"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <returns><c>true</c> if <paramref name="text"/> is equivalent to <see cref="bool.TrueString"/>, <c>false otherwise</c>.</returns>
  /// <seealso cref="bool.Parse(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBoolean(string, out bool?)"/>
  public static bool ToBoolean(this string text) => text is not null ? bool.Parse(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///  <para>Converts specified string into <see cref="bool"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result"><c>true</c> if <paramref name="text"/> is equivalent to <see cref="bool.TrueString"/>, <c>false otherwise</c>.</param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="bool.TryParse(string, out bool)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBoolean(string)"/>
  public static bool ToBoolean(this string text, out bool? result) => (result = bool.TryParse(text, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToSbyte(string, out sbyte?, IFormatProvider)"/>
  public static sbyte ToSbyte(this string text, IFormatProvider format = null) => text is not null ? sbyte.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <seealso cref="ToSbyte(string, IFormatProvider)"/>
  public static bool ToSbyte(this string text, out sbyte? result, IFormatProvider format = null) => (result = sbyte.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="byte"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="byte"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="byte.Parse(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToByte(string, out byte?, IFormatProvider)"/>
  public static byte ToByte(this string text, IFormatProvider format = null) => text is not null ? byte.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="bool"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="byte"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="byte.TryParse(string, out byte)"/>
  /// <seealso cref="ToByte(string, IFormatProvider)"/>
  public static bool ToByte(this string text, out byte? result, IFormatProvider format = null) => (result = byte.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="short"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="short"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="short.Parse(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToShort(string, out short?, IFormatProvider)"/>
  public static short ToShort(this string text, IFormatProvider format = null) => text is not null ? short.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="short"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="short"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="short.TryParse(string, out short)"/>
  /// <seealso cref="ToShort(string, IFormatProvider)"/>
  public static bool ToShort(this string text, out short? result, IFormatProvider format = null) => (result = short.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToUshort(string, out ushort?, IFormatProvider)"/>
  public static ushort ToUshort(this string text, IFormatProvider format = null) => text is not null ? ushort.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <seealso cref="ToUshort(string, IFormatProvider)"/>
  public static bool ToUshort(this string text, out ushort? result, IFormatProvider format = null) => (result = ushort.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="int"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="int"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="int.Parse(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToInt(string, out int?, IFormatProvider)"/>
  public static int ToInt(this string text, IFormatProvider format = null) => text is not null ? int.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="int"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="int"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="int.TryParse(string, out int)"/>
  /// <seealso cref="ToInt(string, IFormatProvider)"/>
  public static bool ToInt(this string text, out int? result, IFormatProvider format = null) => (result = int.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToUint(string, out uint?, IFormatProvider)"/>
  public static uint ToUint(this string text, IFormatProvider format = null) => text is not null ? uint.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <seealso cref="ToUint(string, IFormatProvider)"/>
  public static bool ToUint(this string text, out uint? result, IFormatProvider format = null) => (result = uint.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="long"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="long"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="long.Parse(string)"/>
  /// <seealso cref="ToLong(string, out long?, IFormatProvider)"/>
  public static long ToLong(this string text, IFormatProvider format = null) => text is not null ? long.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="long"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="long"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="long.TryParse(string, out long)"/>
  /// <seealso cref="ToLong(string, IFormatProvider)"/>
  public static bool ToLong(this string text, out long? result, IFormatProvider format = null) => (result = long.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToUlong(string, out ulong?, IFormatProvider)"/>
  public static ulong ToUlong(this string text, IFormatProvider format = null) => text is not null ? ulong.Parse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <seealso cref="ToUlong(string, IFormatProvider)"/>
  public static bool ToUlong(this string text, out ulong? result, IFormatProvider format = null) => (result = ulong.TryParse(text, NumberStyles.Integer, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="float"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="float"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="float.Parse(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToFloat(string, out float?, IFormatProvider)"/>
  public static float ToFloat(this string text, IFormatProvider format = null) => text is not null ? float.Parse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="float"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="float"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="float.TryParse(string, out float)"/>
  /// <seealso cref="ToFloat(string, IFormatProvider)"/>
  public static bool ToFloat(this string text, out float? result, IFormatProvider format = null) => (result = float.TryParse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="double"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="double"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="double.Parse(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToDouble(string, out double?, IFormatProvider)"/>
  public static double ToDouble(this string text, IFormatProvider format = null) => text is not null ? double.Parse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="double"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="double"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="double.TryParse(string, out double)"/>
  /// <seealso cref="ToDouble(string, IFormatProvider)"/>
  public static bool ToDouble(this string text, out double? result, IFormatProvider format = null) => (result = double.TryParse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="decimal"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="format"></param>
  /// <returns>The <see cref="decimal"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="decimal.Parse(string)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToDecimal(string, out decimal?, IFormatProvider)"/>
  public static decimal ToDecimal(this string text, IFormatProvider format = null) => text is not null ? decimal.Parse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="decimal"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="decimal"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="decimal.TryParse(string, out decimal)"/>
  /// <seealso cref="ToDecimal(string, IFormatProvider)"/>
  public static bool ToDecimal(this string text, out decimal? result, IFormatProvider format = null) => (result = decimal.TryParse(text, NumberStyles.Float, format ?? CultureInfo.InvariantCulture, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into enumeration of specified type.</para>
  /// </summary>
  /// <typeparam name="T">Type of enumeration.</typeparam>
  /// <param name="text">String to be converted.</param>
  /// <returns>Element of enumeration of <typeparamref name="T"/> type, to which string <paramref name="text"/> was converted.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToEnum{T}(string, out T?)"/>
  public static T ToEnum<T>(this string text) where T : struct => text is not null ? Enum.Parse<T>(text, true) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  /// <seealso cref="ToEnum{T}(string)"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToGuid(string, out Guid?)"/>
  public static Guid ToGuid(this string text) => text is not null ? Guid.Parse(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="Guid"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="Guid"/> value to which string <paramref name="text"/> was converted.</param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="ToGuid(string)"/>
  public static bool ToGuid(this string text, out Guid? result) => (result = Guid.TryParse(text, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para>Converts specified string into <see cref="Uri"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <returns>The <see cref="Uri"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <seealso cref="ToUri(string, out Uri)"/>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToType(string, out Type)"/>
  public static Type ToType(this string text) => text is not null ? Type.GetType(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  /// <seealso cref="ToType(string)"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToDateTime(string, out DateTime?, IFormatProvider)"/>
  public static DateTime ToDateTime(this string text, IFormatProvider format = null) => text is not null ? DateTime.Parse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para>Converts specified string into <see cref="DateTime"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="result">The <see cref="DateTime"/> value to which string <paramref name="text"/> was converted.</param>
  /// <param name="format"></param>
  /// <returns><c>true</c> if <paramref name="text"/> was successfully converted, <c>false</c> otherwise.</returns>
  /// <seealso cref="DateTime.TryParse(string, out DateTime)"/>
  public static bool ToDateTime(this string text, out DateTime? result, IFormatProvider format = null) => (result = DateTime.TryParse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToDateTimeOffset(string, out DateTimeOffset?, IFormatProvider)"/>
  public static DateTimeOffset ToDateTimeOffset(this string text, IFormatProvider format = null) => text is not null ? DateTimeOffset.Parse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <seealso cref="ToDateTimeOffset(string, IFormatProvider)"/>
  public static bool ToDateTimeOffset(this string text, out DateTimeOffset? result, IFormatProvider format = null) => (result = DateTimeOffset.TryParse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out var value) ? value : null) is not null;

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToDateOnly(string, out DateOnly?, IFormatProvider)"/>
  public static DateOnly ToDateOnly(this string text, IFormatProvider format = null) => text is not null ? DateOnly.Parse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <seealso cref="ToDateOnly(string, IFormatProvider)"/>
  public static bool ToDateOnly(this string text, out DateOnly? result, IFormatProvider format = null) => (result = DateOnly.TryParse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out var value) ? value : null) is not null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTimeOnly(string, out TimeOnly?, IFormatProvider)"/>
  public static TimeOnly ToTimeOnly(this string text, IFormatProvider format = null) => text is not null ? TimeOnly.Parse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <seealso cref="ToTimeOnly(string, IFormatProvider)"/>
  public static bool ToTimeOnly(this string text, out TimeOnly? result, IFormatProvider format = null) => (result = TimeOnly.TryParse(text, format ?? CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out var value) ? value : null) is not null;
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToFile(string, out FileInfo)"/>
  public static FileInfo ToFile(this string text) => text is not null ? new FileInfo(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  /// <seealso cref="ToFile(string)"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToDirectory(string, out DirectoryInfo)"/>
  public static DirectoryInfo ToDirectory(this string text) => text is not null ? new DirectoryInfo(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  /// <seealso cref="ToDirectory(string)"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static string ToPath(this string text) => text is not null ? Path.GetFullPath(text.ToUri().LocalPath).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToIpAddress(string, out IPAddress)"/>
  public static IPAddress ToIpAddress(this string text) => text is not null ? IPAddress.Parse(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  /// <seealso cref="ToIpAddress(string)"/>
  public static bool ToIpAddress(this string text, out IPAddress result) => IPAddress.TryParse(text, out result);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static IPHostEntry ToIpHost(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    text.ToIpAddress(out var ip);

    return ip is not null ? new IPHostEntry { AddressList = [ip]} : new IPHostEntry { HostName = text };
  }

  /// <summary>
  ///   <para>Converts specified string into <see cref="Regex"/> value.</para>
  /// </summary>
  /// <param name="text">String to be converted.</param>
  /// <param name="options"></param>
  /// <returns>The <see cref="Regex"/> value to which string <paramref name="text"/> was converted.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static Regex ToRegex(this string text, RegexOptions options = RegexOptions.None) => text is not null ? new Regex(text, options) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static StringBuilder ToStringBuilder(this string text) => text is not null ? new StringBuilder(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static StringReader ToStringReader(this string text) => text is not null ? new StringReader(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <param name="contentType"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static StringContent ToStringContent(this string text, Encoding encoding = null, string contentType = null) => text is not null ? new StringContent(text, encoding, contentType) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static XmlReader ToXmlReader(this string text) => text.ToStringReader().ToXmlReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static XmlDictionaryReader ToXmlDictionaryReader(this string text) => text.ToStringReader().ToXmlDictionaryReader();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static XmlDocument ToXmlDocument(this string text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    using var reader = text.ToXmlReader();

    return reader.ToXmlDocument();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToXDocumentAsync(string, CancellationToken)"/>
  public static XDocument ToXDocument(this string text) => text is not null ? XDocument.Parse(text) : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToXDocument(string)"/>
  public static async Task<XDocument> ToXDocumentAsync(this string text, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    cancellation.ThrowIfCancellationRequested();

    using var reader = text.ToXmlReader();

    return await reader.ToXDocumentAsync(cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="info"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static Process ToProcess(this string text, ProcessStartInfo info = null)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    var process = new Process();

    if (info is not null)
    {
      process.StartInfo = info;
    }

    process.StartInfo.FileName = text;

    return process;
  }
}