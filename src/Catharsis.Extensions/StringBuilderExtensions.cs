using System.Globalization;
using System.Text;
using System.Xml;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for textual I/O types.</para>
/// </summary>
/// <seealso cref="StringBuilder"/>
public static class StringBuilderExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="elements"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(StringBuilder, object[])"/>
  public static StringBuilder With(this StringBuilder builder, IEnumerable<object> elements)
  {
    if (builder is null) throw new ArgumentNullException(nameof(builder));
    if (elements is null) throw new ArgumentNullException(nameof(elements));

    foreach (var element in elements)
    {
      builder.Append(element);
    }

    return builder;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="elements"></param>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  /// <seealso cref="With(StringBuilder, IEnumerable{object})"/>
  public static StringBuilder With(this StringBuilder builder, params object[] elements) => builder.With(elements as IEnumerable<object>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="positions"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="positions"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(StringBuilder, int[])"/>
  public static StringBuilder Without(this StringBuilder builder, IEnumerable<int> positions)
  {
    if (builder is null) throw new ArgumentNullException(nameof(builder));
    if (positions is null) throw new ArgumentNullException(nameof(positions));

    foreach (var position in positions)
    {
      builder.Remove(position, 1);
    }

    return builder;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="positions"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Without(StringBuilder, IEnumerable{int})"/>
  public static StringBuilder Without(this StringBuilder builder, params int[] positions) => builder.Without(positions as IEnumerable<int>);

  /// <summary>
  ///   <para>Creates a copy of the specified <see cref="StringBuilder"/> with the same text contents and capacity.</para>
  /// </summary>
  /// <param name="builder">String builder instance to be cloned.</param>
  /// <returns>Cloning result.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  public static StringBuilder Clone(this StringBuilder builder) => builder is not null ? new StringBuilder(builder.ToString(), builder.Capacity) : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsEmpty(StringBuilder)"/>
  public static bool IsUnset(this StringBuilder builder) => builder is null || builder.IsEmpty();

  /// <summary>
  ///   <para>Determines whether the specified <see cref="StringBuilder"/> instance can be considered "empty", meaning its length is zero.</para>
  /// </summary>
  /// <param name="builder">String builder instance for evaluation.</param>
  /// <returns>If the specified <paramref name="builder"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IsUnset(StringBuilder)"/>
  public static bool IsEmpty(this StringBuilder builder) => builder is not null ? builder.Length == 0 : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  public static StringBuilder Empty(this StringBuilder builder) => builder?.Clear() ?? throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Max(StringBuilder, StringBuilder)"/>
  /// <seealso cref="MinMax(StringBuilder, StringBuilder)"/>
  public static StringBuilder Min(this StringBuilder left, StringBuilder right)
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
  /// <seealso cref="Min(StringBuilder, StringBuilder)"/>
  /// <seealso cref="MinMax(StringBuilder, StringBuilder)"/>
  public static StringBuilder Max(this StringBuilder left, StringBuilder right)
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
  /// <seealso cref="Min(StringBuilder, StringBuilder)"/>
  /// <seealso cref="Max(StringBuilder, StringBuilder)"/>
  public static (StringBuilder Min, StringBuilder Max) MinMax(this StringBuilder left, StringBuilder right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return left.Length <= right.Length ? (left, right) : (right, left);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
  public static StringBuilder TryFinallyClear(this StringBuilder builder, Action<StringBuilder> action)
  {
    if (builder is null) throw new ArgumentNullException(nameof(builder));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return builder.TryFinally(action, x => x.Empty());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="format"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  public static StringWriter ToStringWriter(this StringBuilder builder, IFormatProvider format = null) => builder is not null ? new StringWriter(builder, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  public static XmlWriter ToXmlWriter(this StringBuilder builder) => builder is not null ? XmlWriter.Create(builder, new XmlWriterSettings { Indent = true }) : throw new ArgumentNullException(nameof(builder));
}