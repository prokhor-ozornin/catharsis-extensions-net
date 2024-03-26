using System.Globalization;
using System.Text;
using System.Xml;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StringBuilderExtensions"/>.</para>
/// </summary>
public sealed class StringBuilderExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Clone(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    AssertionExtensions.Should(() => StringBuilderExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

    throw new NotImplementedException();

    return;

    static void Validate(StringBuilder original)
    {
      var clone = original.Clone();

      clone.Should().NotBeSameAs(original).And.Be(original);
      clone.ToString().Should().Be(original.ToString());
      clone.Length.Should().Be(original.Length);
      clone.Capacity.Should().Be(original.Capacity);
      clone.MaxCapacity.Should().Be(original.MaxCapacity);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.IsEmpty(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((StringBuilder) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

    Validate(true, new StringBuilder());
    Validate(true, new StringBuilder().Append(string.Empty));
    Validate(false, new StringBuilder().Append(char.MinValue));
    Validate(true, Attributes.RandomString().ToStringBuilder().Clear());

    return;

    static void Validate(bool result, StringBuilder builder) => builder.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Empty(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    AssertionExtensions.Should(() => ((StringBuilder) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

    Validate(new StringBuilder());
    Validate(Attributes.RandomString().ToStringBuilder());

    return;

    static void Validate(StringBuilder builder)
    {
      builder.Empty().Should().BeSameAs(builder);
      builder.Length.Should().Be(0);
      builder.ToString().Should().BeEmpty();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Min(StringBuilder, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    AssertionExtensions.Should(() => StringBuilderExtensions.Min(null, new StringBuilder())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => new StringBuilder().Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

    var first = new StringBuilder();
    var second = new StringBuilder();
    Validate(first, first, second);

    first = new StringBuilder();
    second = new StringBuilder(char.MinValue.ToString());
    Validate(first, first, second);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.ToString());
    Validate(first, first, second);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.Repeat(2));
    Validate(first, first, second);

    return;

    static void Validate(StringBuilder result, StringBuilder left, StringBuilder right) => left.Min(right).Should().BeSameAs(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Max(StringBuilder, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    AssertionExtensions.Should(() => StringBuilderExtensions.Max(null, new StringBuilder())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => new StringBuilder().Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

    var first = new StringBuilder();
    var second = new StringBuilder();
    Validate(first, first, second);

    first = new StringBuilder();
    second = new StringBuilder(char.MinValue.ToString());
    Validate(second, first, second);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.ToString());
    Validate(first, first, second);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.Repeat(2));
    Validate(second, first, second);

    return;

    static void Validate(StringBuilder result, StringBuilder left, StringBuilder right) => left.Max(right).Should().BeSameAs(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.TryFinallyClear(StringBuilder, Action{StringBuilder})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => StringBuilderExtensions.TryFinallyClear(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
    AssertionExtensions.Should(() => new StringBuilder().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    Validate(string.Empty);
    Validate(Attributes.RandomString());

    return;

    static void Validate(string text)
    {
      var builder = new StringBuilder();
      builder.TryFinallyClear(x => x.Append(text)).Should().BeSameAs(builder);
      builder.Length.Should().Be(0);
      builder.ToString().Should().BeEmpty();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.ToStringWriter(StringBuilder, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStringWriter_Method()
  {
    AssertionExtensions.Should(() => StringBuilderExtensions.ToStringWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

    Validate(Attributes.RandomString());
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Validate(Attributes.RandomString(), culture));

    return;

    static void Validate(string text, IFormatProvider format = null)
    {
      var builder = new StringBuilder();

      using var writer = builder.ToStringWriter(format);

      writer.Should().NotBeNull().And.NotBeSameAs(builder.ToStringWriter());

      writer.FormatProvider.Should().Be(format);
      writer.Write(text);
      builder.ToString().Should().Be(writer.ToString()).And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.ToXmlWriter(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => StringBuilderExtensions.ToXmlWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

    var value = Attributes.RandomName();

    var builder = new StringBuilder();

    using var writer = builder.ToXmlWriter();

    writer.Should().NotBeNull().And.NotBeSameAs(builder.ToXmlWriter());

    writer.Settings.Should().NotBeNull();
    writer.WriteState.Should().Be(WriteState.Start);
    writer.XmlLang.Should().BeNull();
    writer.XmlSpace.Should().Be(XmlSpace.None);

    writer.WriteRaw(value);
    writer.Flush();

    builder.ToString().Should().Be($"<?xml version=\"1.0\" encoding=\"utf-16\"?>{value}");

    return;

    static void Validate(string text)
    {
    }
  }
}