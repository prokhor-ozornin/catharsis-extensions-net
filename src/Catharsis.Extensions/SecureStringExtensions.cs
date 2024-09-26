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
  /// <param name="text"></param>
  /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static SecureString AsReadOnly(this SecureString text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    text.MakeReadOnly();

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <seealso cref="IsEmpty(SecureString)"/>
  public static bool IsUnset(this SecureString text) => text is null || text.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="SecureString"/> instance can be considered "empty", meaning its length is zero.</para>
  /// </summary>
  /// <param name="text">Secure string instance for evaluation.</param>
  /// <returns>If the specified <paramref name="text"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(SecureString)"/>
  public static bool IsEmpty(this SecureString text) => text is not null ? text.Length == 0 : throw new ArgumentNullException(nameof(text));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text">Secure string to be cleared.</param>
  /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static SecureString Empty(this SecureString text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    text.Clear();

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="action"></param>
  /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static SecureString TryFinallyClear(this SecureString text, Action<SecureString> action)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return text.TryFinally(action, x => x.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="characters"></param>
  /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="characters"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(SecureString, char[])"/>
  public static SecureString With(this SecureString text, IEnumerable<char> characters)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (characters is null) throw new ArgumentNullException(nameof(characters));

    foreach (var character in characters)
    {
      text.AppendChar(character);
    }

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="characters"></param>
  /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(SecureString, IEnumerable{char})"/>
  public static SecureString With(this SecureString text, params char[] characters) => text.With(characters as IEnumerable<char>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="positions"></param>
  /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="positions"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(SecureString, int[])"/>
  public static SecureString Without(this SecureString text, IEnumerable<int> positions)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (positions is null) throw new ArgumentNullException(nameof(positions));

    foreach (var position in positions)
    {
      text.RemoveAt(position);
    }

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="positions"></param>
  /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(SecureString, IEnumerable{int})"/>
  public static SecureString Without(this SecureString text, params int[] positions) => text.Without(positions as IEnumerable<int>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Max(SecureString, SecureString)"/>
  /// <seealso cref="MinMax(SecureString, SecureString)"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Min(SecureString, SecureString)"/>
  /// <seealso cref="MinMax(SecureString, SecureString)"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Min(SecureString, SecureString)"/>
  /// <seealso cref="Max(SecureString, SecureString)"/>
  public static (SecureString Min, SecureString Max) MinMax(this SecureString left, SecureString right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length <= right.Length ? (left, right) : (right, left);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="encoding"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static byte[] ToBytes(this SecureString text, Encoding encoding = null) => text.ToText().ToBytes(encoding);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
  public static string ToText(this SecureString text)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));

    if (text.Length == 0)
    {
      return string.Empty;
    }

    var pointer = IntPtr.Zero;

    try
    {
      pointer = Marshal.SecureStringToGlobalAllocUnicode(text);

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
  /// <param name="text"></param>
  public static bool ToBoolean(this SecureString text) => text is not null && text.Length > 0;
}