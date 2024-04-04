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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <returns></returns>
  public static StringBuilder With(this StringBuilder builder, params object[] elements) => builder.With(elements as IEnumerable<object>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="positions"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  public static StringBuilder Without(this StringBuilder builder, params int[] positions) => builder.Without(positions as IEnumerable<int>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static StringBuilder Clone(this StringBuilder builder) => builder is not null ? new StringBuilder(builder.ToString(), builder.Capacity) : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsEmpty(this StringBuilder builder) => builder is not null ? builder.Length == 0 : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static StringBuilder Empty(this StringBuilder builder) => builder?.Clear() ?? throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  public static StringWriter ToStringWriter(this StringBuilder builder, IFormatProvider format = null) => builder is not null ? new StringWriter(builder, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(builder));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static XmlWriter ToXmlWriter(this StringBuilder builder) => builder is not null ? XmlWriter.Create(builder, new XmlWriterSettings { Indent = true }) : throw new ArgumentNullException(nameof(builder));
}