using System;
using System.Globalization;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="string"/>.</para>
  /// </summary>
  /// <seealso cref="string"/>
  public static class StringExtendedExtensions
  {
    /// <summary>
    ///   <para>Converts target string to title case (each word starting with a capital letter), using provided culture.</para>
    /// </summary>
    /// <param name="value">Source string to convert.</param>
    /// <param name="culture">Culture to use during conversion, or a <c>null</c> reference to use <see cref="CultureInfo.InvariantCulture"/>.</param>
    /// <returns>Capitalized instance of <paramref name="value"/> string.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="TextInfo.ToTitleCase(string)"/>
    public static string Capitalize(this string value, CultureInfo culture = null)
    {
      Assertion.NotNull(value);

      culture = culture ?? CultureInfo.InvariantCulture;
      return culture.TextInfo.ToTitleCase(value);
    }
  }
}