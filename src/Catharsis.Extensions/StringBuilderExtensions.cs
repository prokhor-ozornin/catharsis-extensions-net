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
  /// <param name="builder"></param>
  extension(StringBuilder builder)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <value></value>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsEmpty(StringBuilder)"/>
    public bool IsUnset => builder is null || builder.IsEmpty;

    /// <summary>
    ///   <para>Determines whether the specified <see cref="StringBuilder"/> instance can be considered "empty", meaning its length is zero.</para>
    /// </summary>
    /// <value>If the specified <paramref name="builder"/> is "empty", return <see langword="true"/>, otherwise return <see langword="false"/>.</value>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
    /// <seealso cref="IsUnset(StringBuilder)"/>
    public bool IsEmpty => builder is not null ? builder.Length == 0 : throw new ArgumentNullException(nameof(builder));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns>Back self-reference to the given <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
    public StringBuilder Empty() => builder?.Clear() ?? throw new ArgumentNullException(nameof(builder));

    /// <summary>
    ///   <para>Creates a copy of the specified <see cref="StringBuilder"/> with the same text contents and capacity.</para>
    /// </summary>
    /// <returns>Cloning result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
    public StringBuilder Clone() => builder is not null ? new StringBuilder(builder.ToString(), builder.Capacity) : throw new ArgumentNullException(nameof(builder));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public StringBuilder TryFinallyClear(Action<StringBuilder> action)
    {
      if (builder is null) throw new ArgumentNullException(nameof(builder));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return builder.TryFinally(action, x => x.Empty());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With(StringBuilder, object[])"/>
    public StringBuilder With(IEnumerable<object> elements)
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
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With(StringBuilder, IEnumerable{object})"/>
    public StringBuilder With(params object[] elements) => builder.With(elements as IEnumerable<object>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="positions"></param>
    /// <returns>Back self-reference to the given <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="positions"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without(StringBuilder, int[])"/>
    public StringBuilder Without(IEnumerable<int> positions)
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
    /// <param name="positions"></param>
    /// <returns>Back self-reference to the given <paramref name="builder"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without(StringBuilder, IEnumerable{int})"/>
    public StringBuilder Without(params int[] positions) => builder.Without(positions as IEnumerable<int>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Max(StringBuilder, StringBuilder)"/>
    /// <seealso cref="MinMax(StringBuilder, StringBuilder)"/>
    public StringBuilder Min(StringBuilder right)
    {
      if (builder is null) throw new ArgumentNullException(nameof(builder));
      if (right is null) throw new ArgumentNullException(nameof(right));

      return builder.Length <= right.Length ? builder : right;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min(StringBuilder, StringBuilder)"/>
    /// <seealso cref="MinMax(StringBuilder, StringBuilder)"/>
    public StringBuilder Max(StringBuilder right)
    {
      if (builder is null) throw new ArgumentNullException(nameof(builder));
      if (right is null) throw new ArgumentNullException(nameof(right));

      return builder.Length > right.Length ? builder : right;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Min(StringBuilder, StringBuilder)"/>
    /// <seealso cref="Max(StringBuilder, StringBuilder)"/>
    public (StringBuilder Min, StringBuilder Max) MinMax(StringBuilder right)
    {
      if (builder is null) throw new ArgumentNullException(nameof(builder));
      if (right is null) throw new ArgumentNullException(nameof(right));

      return builder.Length <= right.Length ? (builder, right) : (right, builder);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
    public StringWriter ToStringWriter(IFormatProvider format = null) => builder is not null ? new StringWriter(builder, format ?? CultureInfo.InvariantCulture) : throw new ArgumentNullException(nameof(builder));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
    public XmlWriter ToXmlWriter() => builder is not null ? XmlWriter.Create(builder, new XmlWriterSettings { Indent = true }) : throw new ArgumentNullException(nameof(builder));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public bool ToBoolean() => builder is not null && builder.Length > 0;
  }
}