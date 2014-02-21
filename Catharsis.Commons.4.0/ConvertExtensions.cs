using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Convert"/>.</para>
  ///   <seealso cref="Convert"/>
  /// </summary>
  public static class ConvertExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static bool Boolean(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      if (subject == null)
      {
        return false;
      }

      switch (Type.GetTypeCode(subject.GetType()))
      {
        case TypeCode.Boolean :
          return ((bool) subject);

        case TypeCode.Byte :
          return ((byte) subject) > 0;

        case TypeCode.Char :
          return ((char) subject) != Char.MinValue;

        case TypeCode.Decimal :
          return ((decimal) subject) > 0;

        case TypeCode.Double :
          return ((double) subject) > 0;

        case TypeCode.Int16 :
          return ((short) subject) > 0;

        case TypeCode.Int32 :
          return ((int) subject) > 0;

        case TypeCode.Int64 :
          return ((long) subject) > 0;

        case TypeCode.SByte :
          return ((sbyte) subject) > 0;

        case TypeCode.Single :
          return ((Single) subject) > 0;

        case TypeCode.String :
          return ((string) subject).Length > 0;

        case TypeCode.UInt16 :
          return ((ushort) subject) > 0;

        case TypeCode.UInt32 :
          return ((uint) subject) > 0;

        case TypeCode.UInt64 :
          return ((ulong) subject) > 0;
      }

      if (subject is string)
      {
        return subject.To<string>().Length > 0;
      }

      if (subject is IEnumerable)
      {
        return subject.To<IEnumerable>().Cast<object>().Any();
      }

      if (subject is FileInfo)
      {
        return subject.To<FileInfo>().Length > 0;
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

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static byte? Byte(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      if (subject == null)
      {
        return null;
      }

      if (subject.IsNumeric())
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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static DateTime? DateTime(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static decimal? Decimal(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      if (subject == null)
      {
        return null;
      }

      if (subject.IsNumeric())
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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static double? Double(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      if (subject == null)
      {
        return null;
      }

      if (subject.IsNumeric())
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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static Guid? Guid(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static short? Int16(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      if (subject == null)
      {
        return null;
      }

      if (subject.IsNumeric())
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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static int? Int32(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static long? Int64(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      if (subject == null)
      {
        return null;
      }

      if (subject.IsNumeric())
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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static IPAddress IPAddress(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      if (subject == null)
      {
        return null;
      }

      if (subject is IPAddress)
      {
        return subject as IPAddress;
      }

      IPAddress result;
      if (System.Net.IPAddress.TryParse(subject.ToString(), out result))
      {
        return result;
      }

      return null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Regex Regex(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static Single? Single(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      if (subject == null)
      {
        return null;
      }

      if (subject.IsNumeric())
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
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static string String(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

      return subject != null ? subject.ToString() : null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="convert"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="convert"/> is a <c>null</c> reference.</exception>
    public static Uri Uri(this Convert convert, object subject)
    {
      Assertion.NotNull(convert);

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
  }
}