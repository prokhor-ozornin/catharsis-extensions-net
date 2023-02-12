using System.Globalization;
using System.Text;
using System.Xml;
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
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.IsEmpty(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => ((StringBuilder) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

    var builder = new StringBuilder();
    builder.IsEmpty().Should().BeTrue();

    builder.Append(char.MinValue);
    builder.IsEmpty().Should().BeFalse();

    builder.Clear();
    builder.IsEmpty().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Empty(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    void Validate(StringBuilder builder)
    {
      builder.Empty().Should().NotBeNull().And.BeSameAs(builder);
      builder.Length.Should().Be(0);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      Validate(new StringBuilder());
      Validate(RandomString.ToStringBuilder());
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
    first.Min(second).Should().BeSameAs(first);

    first = new StringBuilder();
    second = new StringBuilder(char.MinValue.ToString());
    first.Min(second).Should().BeSameAs(first);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.ToString());
    first.Min(second).Should().BeSameAs(first);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.Repeat(2));
    first.Min(second).Should().BeSameAs(first);
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
    first.Max(second).Should().BeSameAs(first);

    first = new StringBuilder();
    second = new StringBuilder(char.MinValue.ToString());
    first.Max(second).Should().BeSameAs(second);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.ToString());
    first.Max(second).Should().BeSameAs(first);

    first = new StringBuilder(char.MaxValue.ToString());
    second = new StringBuilder(char.MinValue.Repeat(2));
    first.Max(second).Should().BeSameAs(second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.TryFinallyClear(StringBuilder, Action{StringBuilder})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => StringBuilderExtensions.TryFinallyClear(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
    AssertionExtensions.Should(() => new StringBuilder().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    var builder = new StringBuilder();
    builder.TryFinallyClear(builder => builder.Append(RandomString)).Should().NotBeNull().And.BeSameAs(builder);
    builder.Length.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.ToStringWriter(StringBuilder, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStringWriter_Method()
  {
    void Validate(IFormatProvider format)
    {
      var value = RandomString;
      var builder = new StringBuilder();

      using var writer = builder.ToStringWriter(format);

      writer.Should().NotBeNull().And.NotBeSameAs(builder.ToStringWriter());

      writer.FormatProvider.Should().Be(format);
      writer.Write(value);
      builder.ToString().Should().Be(writer.ToString()).And.Be(value);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.ToStringWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.ToXmlWriter(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    AssertionExtensions.Should(() => StringBuilderExtensions.ToXmlWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

    var value = RandomName;

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
  }
}