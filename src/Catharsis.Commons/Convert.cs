using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Represents a converter between different source and destination <see cref="Type"/>s.</para>
  /// </summary>
  public sealed class Convert
  {
    private static readonly Convert converter = new Convert();

    /// <summary>
    ///   <para>Current converter instance.</para>
    /// </summary>
    public static Convert To
    {
      get { return converter; }
    }
  }


  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Convert"/>.</para>
  /// </summary>
  /// <seealso cref="Convert"/>
  public static class ConvertExtensions
  {
    /// <summary>
    ///   <para>Converts target object to the <see cref="byte"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="byte"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static byte? Byte(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is byte)
      {
        return subject as byte?;
      }

      byte result;
      if (byte.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="DateTime"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="DateTime"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static DateTime? DateTime(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is DateTime)
      {
        return subject as DateTime?;
      }

      DateTime result;
      if (System.DateTime.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="decimal"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="decimal"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static decimal? Decimal(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is decimal)
      {
        return subject as decimal?;
      }

      decimal result;
      if (decimal.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="double"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="double"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static double? Double(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is double)
      {
        return subject as double?;
      }

      double result;
      if (double.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="Guid"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="Guid"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static Guid? Guid(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is Guid)
      {
        return subject as Guid?;
      }

      Guid result;
      if (subject.ToString().ToGuid(out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="short"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="short"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static short? Int16(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is short)
      {
        return subject as short?;
      }

      short result;
      if (short.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="int"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="int"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static int? Int32(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is int)
      {
        return subject as int?;
      }

      int result;
      if (int.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="long"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="long"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static long? Int64(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is long)
      {
        return subject as long?;
      }

      long result;
      if (long.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="Regex"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="Regex"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Regex Regex(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is Regex)
      {
        return subject as Regex;
      }

      return new Regex(subject.ToString());
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="Single"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="Single"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static Single? Single(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is Single)
      {
        return subject as Single?;
      }

      Single result;
      if (System.Single.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="string"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="string"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static string String(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      return subject?.ToString();
    }

    /// <summary>
    ///   <para>Converts target object to the <see cref="Uri"/> value. Returns <c>null</c> if object is a <c>null</c> reference or conversion is not possible.</para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="Uri"/>, or a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static Uri Uri(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return null;
      }

      if (subject is Uri)
      {
        return subject as Uri;
      }

      return new Uri(subject.ToString());
    }

#if NET_35
    /// <summary>
    ///   <para>Converts target object to the <see cref="bool"/> value, using non-strict approach.</para>
    ///   <para>The following algorithm is used to determine how to perform such conversion:
    ///     <list type="bullet">
    ///       <item><description>If <paramref name="subject"/> is a <c>null</c> reference, the result is <c>false</c>.</description></item>
    ///       <item><description>If <paramref name="subject"/> is a <see cref="bool"/> value, it's returned as it is.</description></item>
    ///       <item><description>If <paramref name="subject"/> is a positive numeric value, the result is <c>true</c>, if it's negative or zero - the result is <c>false</c>.</description></item>
    ///       <item><description>If <paramref name="subject"/> is a <see cref="string"/>, <c>true</c> is returned if it contains at least one character, <c>false</c> otherwise.</description></item>
    ///       <item><description>If <paramref name="subject"/> implements <see cref="IEnumerable"/>, <c>true</c> is returned if it contains at least one element, <c>false</c> otherwise.</description></item>
    ///       <item><description>If <paramref name="subject"/> is a <see cref="Stream"/> value, <c>true</c> is returned if it's not empty, <c>false</c> otherwise.</description></item>
    ///       <item><description>If <paramref name="subject"/> is a <see cref="Match"/> value, <c>true</c> is returned if it's successful, <c>false</c> otherwise.</description></item>
    ///     </list>
    ///   </para>
    /// </summary>
    /// <param name="self">Extended converter instance.</param>
    /// <param name="subject">Target object for conversion.</param>
    /// <returns><paramref name="subject"/> instance that was converted to <see cref="bool"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static bool Boolean(this Convert self, object subject)
    {
      Assertion.NotNull(self);

      if (subject == null)
      {
        return false;
      }

      switch (Type.GetTypeCode(subject.GetType()))
      {
        case TypeCode.Boolean:
          return ((bool)subject);

        case TypeCode.Byte:
          return ((byte)subject) > 0;

        case TypeCode.Char:
          return ((char)subject) != char.MinValue;

        case TypeCode.Decimal:
          return ((decimal)subject) > 0;

        case TypeCode.Double:
          return ((double)subject) > 0;

        case TypeCode.Int16:
          return ((short)subject) > 0;

        case TypeCode.Int32:
          return ((int)subject) > 0;

        case TypeCode.Int64:
          return ((long)subject) > 0;

        case TypeCode.SByte:
          return ((sbyte)subject) > 0;

        case TypeCode.Single:
          return ((Single)subject) > 0;

        case TypeCode.String:
          return ((string)subject).Length > 0;

        case TypeCode.UInt16:
          return ((ushort)subject) > 0;

        case TypeCode.UInt32:
          return ((uint)subject) > 0;

        case TypeCode.UInt64:
          return ((ulong)subject) > 0;
      }

      if (subject is IEnumerable)
      {
        return subject.To<IEnumerable>().Cast<object>().Any();
      }

      if (subject is Stream)
      {
        return subject.To<Stream>().Length > 0;
      }

      if (subject is Match)
      {
        return subject.To<Match>().Success;
      }

      return true;
    }
#endif
  }
}