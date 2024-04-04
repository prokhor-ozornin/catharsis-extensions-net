using System.Security;
using System.Text;
using System.Runtime.InteropServices;

namespace Catharsis.Extensions;

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
  /// <param name="characters"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static SecureString With(this SecureString secure, IEnumerable<char> characters)
  {
    if (secure is null) throw new ArgumentNullException(nameof(secure));
    if (characters is null) throw new ArgumentNullException(nameof(characters));

    foreach (var character in characters)
    {
      secure.AppendChar(character);
    }

    return secure;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="characters"></param>
  /// <returns></returns>
  public static SecureString With(this SecureString secure, params char[] characters) => secure.With(characters as IEnumerable<char>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="positions"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static SecureString Without(this SecureString secure, IEnumerable<int> positions)
  {
    if (secure is null) throw new ArgumentNullException(nameof(secure));
    if (positions is null) throw new ArgumentNullException(nameof(positions));

    foreach (var position in positions)
    {
      secure.RemoveAt(position);
    }

    return secure;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="positions"></param>
  /// <returns></returns>
  public static SecureString Without(this SecureString secure, params int[] positions) => secure.Without(positions as IEnumerable<int>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this SecureString secure) => secure is not null ? secure.Length == 0 : throw new ArgumentNullException(nameof(secure));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  public static SecureString Max(this SecureString left, SecureString right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length > right.Length ? left : right;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static (SecureString Min, SecureString Max) MinMax(this SecureString left, SecureString right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length <= right.Length ? (left, right) : (right, left);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static SecureString TryFinallyClear(this SecureString secure, Action<SecureString> action)
  {
    if (secure is null) throw new ArgumentNullException(nameof(secure));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return secure.TryFinally(action, x => x.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] ToBytes(this SecureString secure, Encoding encoding = null) => secure.ToText().ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="secure"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this SecureString secure)
  {
    if (secure is null) throw new ArgumentNullException(nameof(secure));

    if (secure.Length == 0)
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
}