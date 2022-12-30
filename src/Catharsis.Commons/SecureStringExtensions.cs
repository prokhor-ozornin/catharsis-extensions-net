using System.Security;
using System.Text;
using System.Runtime.InteropServices;

namespace Catharsis.Commons;

/// <summary>
///   <para></para>
/// </summary>
/// <seealso cref="SecureString"/>
public static class SecureStringExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static bool IsEmpty(this SecureString secure) => secure.Length <= 0;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static SecureString Empty(this SecureString secure)
  {
    secure.Clear();
    return secure;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static SecureString Min(this SecureString left, SecureString right) => left.Length <= right.Length ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static SecureString Max(this SecureString left, SecureString right) => left.Length >= right.Length ? left : right;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static SecureString TryFinallyClear(this SecureString secure, Action<SecureString> action) => secure.TryFinally(action, secure => secure.Empty());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static SecureString AsReadOnly(this SecureString secure)
  {
    secure.MakeReadOnly();
    return secure;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  public static byte[] ToBytes(this SecureString secure, Encoding encoding = null) => secure.ToText().ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static string ToText(this SecureString secure)
  {
    if (secure.Length <= 0)
    {
      return string.Empty;
    }

    var pointer = IntPtr.Zero;

    try
    {
      pointer = Marshal.SecureStringToGlobalAllocUnicode(secure);

      return Marshal.PtrToStringAuto(pointer) ?? string.Empty;
    }
    finally
    {
      if (pointer != IntPtr.Zero)
      {
        Marshal.ZeroFreeGlobalAllocUnicode(pointer);
      }
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static SecureString WriteText(this SecureString destination, IEnumerable<char> text)
  {
    text.ForEach(destination.AppendChar);
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static IEnumerable<char> WriteTo(this IEnumerable<char> text, SecureString destination)
  {
    destination.WriteText(text);
    return text;
  }
}