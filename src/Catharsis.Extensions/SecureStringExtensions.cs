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
  /// <param name="text"></param>
  extension(SecureString text)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
    public SecureString AsReadOnly()
    {
      if (text is null) throw new ArgumentNullException(nameof(text));

      text.MakeReadOnly();

      return text;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <seealso cref="IsEmpty"/>
    public bool IsUnset => text is null || text.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="SecureString"/> instance can be considered "empty", meaning its length is zero.</para>
    /// </summary>
    /// <value>If the specified <paramref name="text"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset"/>
    public bool IsEmpty => text is not null ? text.Length == 0 : throw new ArgumentNullException(nameof(text));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
    public SecureString Empty()
    {
      if (text is null) throw new ArgumentNullException(nameof(text));

      text.Clear();

      return text;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public SecureString TryFinallyClear(Action<SecureString> action)
    {
      if (text is null) throw new ArgumentNullException(nameof(text));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return text.TryFinally(action, x => x.Empty());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="characters"></param>
    /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="characters"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With(SecureString, char[])"/>
    public SecureString With(IEnumerable<char> characters)
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
    /// <param name="characters"></param>
    /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With(SecureString, IEnumerable{char})"/>
    public SecureString With(params char[] characters) => text.With(characters as IEnumerable<char>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="positions"></param>
    /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="positions"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without(SecureString, int[])"/>
    public SecureString Without(IEnumerable<int> positions)
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
    /// <param name="positions"></param>
    /// <returns>Back self-reference to the given <paramref name="text"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without(SecureString, IEnumerable{int})"/>
    public SecureString Without(params int[] positions) => text.Without(positions as IEnumerable<int>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Max(SecureString, SecureString)"/>
    /// <seealso cref="MinMax(SecureString, SecureString)"/>
    public SecureString Min(SecureString other)
    {
      if (text is null) throw new ArgumentNullException(nameof(text));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return text.Length <= other.Length ? text : other;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min(SecureString, SecureString)"/>
    /// <seealso cref="MinMax(SecureString, SecureString)"/>
    public SecureString Max(SecureString other)
    {
      if (text is null) throw new ArgumentNullException(nameof(text));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return text.Length > other.Length ? text : other;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="text"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min(SecureString, SecureString)"/>
    /// <seealso cref="Max(SecureString, SecureString)"/>
    public (SecureString Min, SecureString Max) MinMax(SecureString other)
    {
      if (text is null) throw new ArgumentNullException(nameof(text));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return text.Length <= other.Length ? (text, other) : (other, text);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
    public byte[] ToBytes(Encoding encoding = null) => text.ToText().ToBytes(encoding);
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public byte[] Bytes => text.ToBytes();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is <see langword="null"/>.</exception>
    public string ToText()
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
    ///   <para>[NEW]</para>
    /// </summary>
    public string Text => text.ToText();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public bool ToBoolean() => text is not null && text.Length > 0;
  }
}