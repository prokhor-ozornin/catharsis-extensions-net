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
  public static bool IsEmpty(this SecureString secure) => secure is not null ? secure.Length <= 0 : throw new ArgumentNullException(nameof(secure));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static SecureString Empty(this SecureString secure)
  {
    if (secure is null) throw new ArgumentNullException(nameof(secure));

    secure.Clear();
   
    return secure;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static SecureString Min(this SecureString left, SecureString right)
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
  public static SecureString Max(this SecureString left, SecureString right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length >= right.Length ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static SecureString TryFinallyClear(this SecureString secure, Action<SecureString> action)
  {
    if (secure is null) throw new ArgumentNullException(nameof(secure));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return secure.TryFinally(action, secure => secure.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  public static SecureString AsReadOnly(this SecureString secure)
  {
    if (secure is null) throw new ArgumentNullException(nameof(secure));

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
    if (secure is null) throw new ArgumentNullException(nameof(secure));

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
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

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
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (destination is null) throw new ArgumentNullException(nameof(destination));

    destination.WriteText(text);

    return text;
  }
}