using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using static System.Math;

namespace Catharsis.Commons;

/// <summary>
///   <para>Converter between various source and destination types of objects.</para>
/// </summary>
public sealed class Convert
{
  /// <summary>
  ///   <para>Current converter instance.</para>
  /// </summary>
  public static Convert To { get; } = new();
}


/// <summary>
///   <para>Extension methods for types converter.</para>
/// </summary>
/// <seealso cref="Convert"/>
public static class ConvertExtensions
{
  /// <summary>
  ///   <para>Converts target object to the <see cref="string"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="encoding"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="string"/>, or a <c>null</c> reference.</returns>
  public static string String(this Convert convert, object instance, Encoding encoding = null)
  {
    if (convert == null)
    {
      throw new ArgumentNullException();
    }

    return instance switch
    {
      null => null,
      string text => text,
      BinaryReader reader => reader.ToText(),
      SecureString secure => secure.ToText(),
      FileInfo file => file.ToText(encoding).Await(),
      HttpContent http => http.ToText().Await(),
      Process process => process.ToText().Await(),
      Stream stream => stream.ToText(encoding).Await(),
      TextReader reader => reader.ToText().Await(),
      Uri uri => uri.ToText(encoding).Await(),
      XmlDocument xml => xml.ToText(),
      XDocument xml => xml.ToText().Await(),
      XmlReader xml => xml.ToText().Await(),
      _ => instance.ToInvariantString()
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static byte[] Binary(this Convert convert, object instance, Encoding encoding = null)
  {
    return instance switch
    {
      null => null,
      IEnumerable<byte> bytes => bytes.AsArray(),
      string text => text.ToBytes(encoding),
      SecureString secure => secure.ToText().ToBytes(encoding),
      Guid guid => guid.ToByteArray(),
      FileInfo file => file.ToBytes().ToArray().Await(),
      IPAddress address => address.ToBytes(),
      PhysicalAddress address => address.ToBytes(),
      HttpContent http => http.ToBytes().ToArray().Await(),
      Process process => process.ToBytes().ToArray().Await(),
      Stream stream => stream.ToBytes().ToArray().Await(),
      Uri uri => uri.ToBytes().ToArray().Await(),
      XmlDocument xml => xml.ToBytes(),
      XDocument xml => xml.ToBytes().Await(),
      _ => instance.SerializeAsBinary()
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static T[] Array<T>(this Convert convert, object instance)
  {
    return instance switch
    {
      null => null,
      T[] array => array,
      IEnumerable<T> sequence => sequence.AsArray(),
      IAsyncEnumerable<T> sequence => sequence.ToArray().Await(),
      _ => new[] { (T) instance }
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static sbyte? Sbyte(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => value,
      byte value => (sbyte) value,
      short value => (sbyte) value,
      ushort value => (sbyte) value,
      int value => (sbyte) value,
      uint value => (sbyte) value,
      long value => (sbyte) value,
      ulong value => (sbyte) value,
      float value => (sbyte) Round(value),
      double value => (sbyte) Round(value),
      decimal value => (sbyte) Round(value),
      _ => instance.ToFormattedString(format).ToSbyte(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="byte"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="format"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="byte"/>, or a <c>null</c> reference.</returns>
  public static byte? Byte(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => (byte) value,
      byte value => value,
      short value => (byte) value,
      ushort value => (byte) value,
      int value => (byte) value,
      uint value => (byte) value,
      long value => (byte) value,
      ulong value => (byte) value,
      float value => (byte) Round(value),
      double value => (byte) Round(value),
      decimal value => (byte) Round(value),
      _ => instance.ToFormattedString(format).ToByte(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="short"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="format"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="short"/>, or a <c>null</c> reference.</returns>
  public static short? Short(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => value,
      byte value => value,
      short value => value,
      ushort value => (short) value,
      int value => (short) value,
      uint value => (short) value,
      long value => (short) value,
      ulong value => (short) value,
      float value => (short) Round(value),
      double value => (short) Round(value),
      decimal value => (short) Round(value),
      _ => instance.ToFormattedString(format).ToShort(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static ushort? Ushort(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => (ushort) value,
      byte value => value,
      short value => (ushort) value,
      ushort value => value,
      int value => (ushort) value,
      uint value => (ushort) value,
      long value => (ushort) value,
      ulong value => (ushort) value,
      float value => (ushort) Round(value),
      double value => (ushort) Round(value),
      decimal value => (ushort) Round(value),
      _ => instance.ToFormattedString(format).ToUshort(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="int"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="format"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="int"/>, or a <c>null</c> reference.</returns>
  public static int? Int(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => value,
      byte value => value,
      short value => value,
      ushort value => value,
      int value => value,
      uint value => (int) value,
      long value => (int) value,
      ulong value => (int) value,
      float value => (int) Round(value),
      double value => (int) Round(value),
      decimal value => (int) Round(value),
      _ => instance.ToFormattedString(format).ToInt(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static uint? Uint(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => (uint) value,
      byte value => value,
      short value => (uint) value,
      ushort value => value,
      int value => (uint) value,
      uint value => value,
      long value => (uint) value,
      ulong value => (uint) value,
      float value => (uint) Round(value),
      double value => (uint) Round(value),
      decimal value => (uint) Round(value),
      _ => instance.ToFormattedString(format).ToUint(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="long"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="format"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="long"/>, or a <c>null</c> reference.</returns>
  public static long? Long(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => value,
      byte value => value,
      short value => value,
      ushort value => value,
      int value => value,
      uint value => value,
      long value => value,
      ulong value => (long) value,
      float value => (long) Round(value),
      double value => (long) Round(value),
      decimal value => (long) Round(value),
      _ => instance.ToFormattedString(format).ToLong(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static ulong? Ulong(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => (ulong) value,
      byte value => value,
      short value => (ulong) value,
      ushort value => value,
      int value => (ulong) value,
      uint value => value,
      long value => (ulong) value,
      ulong value => value,
      float value => (ulong) Round(value),
      double value => (ulong) Round(value),
      decimal value => (ulong) Round(value),
      _ => instance.ToFormattedString(format).ToUlong(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="Float"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="format"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="Float"/>, or a <c>null</c> reference.</returns>
  public static float? Float(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => (float) value,
      byte value => (float) value,
      short value => (float) value,
      ushort value => (float) value,
      int value => (float) value,
      uint value => (float) value,
      long value => (float) value,
      ulong value => (float) value,
      float value => value,
      double value => (float) value,
      decimal value => (float) value,
      _ => instance.ToFormattedString(format).ToFloat(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="double"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="format"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="double"/>, or a <c>null</c> reference.</returns>
  public static double? Double(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => (double) value,
      byte value => (double) value,
      short value => (double) value,
      ushort value => (double) value,
      int value => (double) value,
      uint value => (double) value,
      long value => (double) value,
      ulong value => (double) value,
      float value => (double) value,
      double value => value,
      decimal value => (double) value,
      _ => instance.ToFormattedString(format).ToDouble(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="decimal"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="format"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="decimal"/>, or a <c>null</c> reference.</returns>
  public static decimal? Decimal(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      sbyte value => value,
      byte value => value,
      short value => value,
      ushort value => value,
      int value => value,
      uint value => value,
      long value => value,
      ulong value => value,
      float value => (decimal) value,
      double value => (decimal) value,
      decimal value => value,
      _ => instance.ToFormattedString(format).ToDecimal(out var result, format) ? result : null
    };
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static T? Enum<T>(this Convert convert, object instance) where T : struct
  {
    return instance switch
    {
      null => null,
      _ => (T?) (instance is T ? instance : instance.ToInvariantString().ToEnum<T>(out var result) ? result : null)
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="DateTime"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="format"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="DateTime"/>, or a <c>null</c> reference.</returns>
  public static DateTime? DateTime(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      DateTime dateTime => dateTime,
      DateTimeOffset dateTimeOffset => dateTimeOffset.ToDateTime(),
#if NET6_0
      DateOnly dateOnly => dateOnly.ToDateTime(),
      TimeOnly timeOnly => timeOnly.ToDateTime(),
#endif
      _ => instance.ToFormattedString(format).ToDateTime(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static DateTimeOffset? DateTimeOffset(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      DateTime dateTime => dateTime.ToDateTimeOffset(),
      DateTimeOffset dateTimeOffset => dateTimeOffset,
#if NET6_0
      DateOnly dateOnly => dateOnly.ToDateTimeOffset(),
      TimeOnly timeOnly => timeOnly.ToDateTimeOffset(),
#endif
      _ => instance.ToFormattedString(format).ToDateTimeOffset(out var result, format) ? result : null
    };
  }

#if NET6_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static DateOnly? DateOnly(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      DateTime dateTime => dateTime.ToDateOnly(),
      DateTimeOffset dateTimeOffset => dateTimeOffset.ToDateOnly(),
      DateOnly dateOnly => dateOnly,
      _ => instance.ToFormattedString(format).ToDateOnly(out var result, format) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  public static TimeOnly? TimeOnly(this Convert convert, object instance, IFormatProvider format = null)
  {
    return instance switch
    {
      null => null,
      DateTime dateTime => dateTime.ToTimeOnly(),
      DateTimeOffset dateTimeOffset => dateTimeOffset.ToTimeOnly(),
      TimeOnly timeOnly => timeOnly,
      _ => instance.ToFormattedString(format).ToTimeOnly(out var result, format) ? result : null
    };
  }
#endif

  /// <summary>
  ///   <para>Converts target object to the <see cref="Guid"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="Guid"/>, or a <c>null</c> reference.</returns>
  public static Guid? Guid(this Convert convert, object instance)
  {
    return instance switch
    {
      null => null,
      Guid guid => guid,
      byte[] bytes => bytes.Length == 16 ? new Guid(bytes) : null,
      _ => instance.ToInvariantString().ToGuid(out var result) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="Regex"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <param name="options"></param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="Regex"/>, or a <c>null</c> reference.</returns>
  public static Regex Regex(this Convert convert, object instance, RegexOptions? options = null)
  {
    return instance switch
    {
      null => null,
      Regex regex => regex,
      _ => instance.ToInvariantString().ToRegex(options)
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="Uri"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="Uri"/>, or a <c>null</c> reference.</returns>
  public static Uri Uri(this Convert convert, object instance)
  {
    return instance switch
    {
      null => null,
      Uri uri => uri,
      _ => instance.ToInvariantString().ToUri(out var result) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static StringBuilder StringBuilder(this Convert convert, object instance)
  {
    return instance switch
    {
      null => null,
      StringBuilder builder => builder,
      _ => instance.ToInvariantString().ToStringBuilder()
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static IPAddress IpAddress(this Convert convert, object instance)
  {
    return instance switch
    {
      null => null,
      IPAddress ipAddress => ipAddress,
      long address => new IPAddress(address),
      uint address => new IPAddress(address),
      _ => instance.ToInvariantString().ToIpAddress(out var result) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static DirectoryInfo Directory(this Convert convert, object instance)
  {
    return instance switch
    {
      null => null,
      DirectoryInfo directory => directory,
      _ => instance.ToInvariantString().ToDirectory(out var result) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static FileInfo File(this Convert convert, object instance)
  {
    return instance switch
    {
      null => null,
      FileInfo file => file,
      _ => instance.ToInvariantString().ToFile(out var result) ? result : null
    };
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="convert"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static Type Type(this Convert convert, object instance)
  {
    return instance switch
    {
      null => null,
      Type type => type,
      _ => instance.ToInvariantString().ToType(out var result) ? result : null
    };
  }

  /// <summary>
  ///   <para>Converts target object to the <see cref="bool"/> value, using non-strict approach.</para>
  ///   <para>The following algorithm is used to determine how to perform such conversion:
  ///     <list type="bullet">
  ///       <item><description>If <paramref name="instance"/> is a <c>null</c> reference, the result is <c>false</c>.</description></item>
  ///       <item><description>If <paramref name="instance"/> is a <see cref="bool"/> value, it's returned as it is.</description></item>
  ///       <item><description>If <paramref name="instance"/> is a positive numeric value, the result is <c>true</c>, if it's negative or zero - the result is <c>false</c>.</description></item>
  ///       <item><description>If <paramref name="instance"/> is a <see cref="string"/>, <c>true</c> is returned if it contains at least one character, <c>false</c> otherwise.</description></item>
  ///       <item><description>If <paramref name="instance"/> implements <see cref="IEnumerable"/>, <c>true</c> is returned if it contains at least one element, <c>false</c> otherwise.</description></item>
  ///       <item><description>If <paramref name="instance"/> is a <see cref="Stream"/> value, <c>true</c> is returned if it's not empty, <c>false</c> otherwise.</description></item>
  ///       <item><description>If <paramref name="instance"/> is a <see cref="Match"/> value, <c>true</c> is returned if it's successful, <c>false</c> otherwise.</description></item>
  ///     </list>
  ///   </para>
  /// </summary>
  /// <param name="convert">Extended converter instance.</param>
  /// <param name="instance">Target object for conversion.</param>
  /// <returns><paramref name="instance"/> instance that was converted to <see cref="bool"/>.</returns>
  public static bool Boolean(this Convert convert, object instance)
  {
    return instance switch
    {
      null => false,
      bool value => value,
      char value => value != char.MinValue,
      sbyte value => value > 0,
      byte value => value > 0,
      short value => value > 0,
      ushort value => value > 0,
      int value => value > 0,
      uint value => value > 0,
      long value => value > 0,
      ulong value => value > 0,
      float value => value > 0,
      double value => value > 0,
      decimal value => value > 0,
      string value => value.Trim().Length > 0,
      IEnumerable value => value.Cast<object>().Any(),
      FileInfo value => value.Exists,
      DirectoryInfo value => value.Exists,
      Stream value => value is {CanSeek: true, Length: > 0},
      BinaryReader value => value.PeekChar() >= 0,
      TextReader value => value.Peek() >= 0,
      Match value => value.Success,
      StringBuilder value => value.Length > 0,
      SecureString value => value.Length > 0,
      _ => true
    };
  }
}