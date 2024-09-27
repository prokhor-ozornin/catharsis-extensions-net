using System.Globalization;
using System.Net;
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
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.IsUnset(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      Validate(true, null);
      Validate(true, new StringBuilder());
      Validate(true, new StringBuilder().Append(string.Empty));
      Validate(false, new StringBuilder().Append(char.MinValue));
    }

    return;

    static void Validate(bool result, StringBuilder builder) => builder.IsUnset().Should().Be(builder is null || builder.IsEmpty()).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.IsEmpty(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      Validate(true, new StringBuilder());
      Validate(true, new StringBuilder().Append(string.Empty));
      Validate(false, new StringBuilder().Append(char.MinValue));
    }

    return;

    static void Validate(bool result, StringBuilder builder) => builder.IsEmpty().Should().Be(builder.Length == 0).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Empty(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      Validate(new StringBuilder());
      Validate(Attributes.RandomString().ToStringBuilder());
    }

    return;

    static void Validate(StringBuilder builder)
    {
      builder.Empty().Should().BeOfType<StringBuilder>().And.BeSameAs(builder);
      builder.IsEmpty().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Clone(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Clone_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.Clone(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      Validate(new StringBuilder());
      Validate(Attributes.RandomString().ToStringBuilder());
    }

    return;

    static void Validate(StringBuilder original)
    {
      var clone = original.Clone();

      clone.Should().BeOfType<StringBuilder>().And.NotBeSameAs(original);
      clone.ToString().Should().Be(original.ToString());
      clone.Length.Should().Be(original.Length);
      clone.Capacity.Should().Be(original.Capacity);
      clone.MaxCapacity.Should().Be(original.MaxCapacity);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.TryFinallyClear(StringBuilder, Action{StringBuilder})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.TryFinallyClear(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => new StringBuilder().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(string.Empty.ToStringBuilder());
      Validate(Attributes.RandomString().ToStringBuilder());
    }

    return;

    static void Validate(StringBuilder builder)
    {
      builder.TryFinallyClear(builder => builder.With(char.MinValue, char.MaxValue)).Should().BeOfType<StringBuilder>().And.BeSameAs(builder);
      builder.IsEmpty().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringBuilderExtensions.With(StringBuilder, IEnumerable{object})"/></description></item>
  ///     <item><description><see cref="StringBuilderExtensions.With(StringBuilder, object[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.With(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => string.Empty.ToStringBuilder().With((IEnumerable<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      Validate(new StringBuilder(), []);
      Validate(new StringBuilder(), [string.Empty, Attributes.RandomString()]);

      static void Validate(StringBuilder builder, IEnumerable<object> elements)
      {
        var text = builder.ToString();
        builder.With(elements).Should().BeOfType<StringBuilder>().And.BeSameAs(builder);
        builder.ToString().Should().Be(text + elements.Join());
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.With(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => string.Empty.ToStringBuilder().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("elements");

      Validate(new StringBuilder(), []);
      Validate(new StringBuilder(), [string.Empty, Attributes.RandomString()]);

      static void Validate(StringBuilder builder, params object[] elements)
      {
        var text = builder.ToString();
        builder.With(elements).Should().BeOfType<StringBuilder>().And.BeSameAs(builder);
        builder.ToString().Should().Be(text + elements.Join());
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringBuilderExtensions.Without(StringBuilder, IEnumerable{int})"/></description></item>
  ///     <item><description><see cref="StringBuilderExtensions.Without(StringBuilder, int[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null).Without(Enumerable.Empty<int>())).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => string.Empty.ToStringBuilder().Without((IEnumerable<int>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("positions");

      Validate(new StringBuilder(), []);
      Attributes.RandomString().ToStringBuilder().With(text => Validate(text, new int[text.Length].Fill(_ => 0)));

      static void Validate(StringBuilder builder, IEnumerable<int> positions)
      {
        var text = builder.ToString();
        builder.Without(positions).Should().BeOfType<StringBuilder>().And.BeSameAs(builder);
        builder.ToString().Should().Be(text.Without(positions));
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((StringBuilder) null).Without([])).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => Array.Empty<object>().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("positions");

      Validate(new StringBuilder(), []);
      Attributes.RandomString().ToStringBuilder().With(text => Validate(text, new int[text.Length].Fill(_ => 0).AsArray()));

      static void Validate(StringBuilder builder, params int[] positions)
      {
        var text = builder.ToString();
        builder.Without(positions).Should().BeOfType<StringBuilder>().And.BeSameAs(builder);
        builder.ToString().Should().Be(text.Without(positions));
      }
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Min(StringBuilder, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.Min(null, new StringBuilder())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => new StringBuilder().Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(new StringBuilder(), new StringBuilder());
      Validate(new StringBuilder(), char.MinValue.ToString().ToStringBuilder());
      Validate(char.MaxValue.ToString().ToStringBuilder(), char.MinValue.ToString().ToStringBuilder());
    }

    return;

    static void Validate(StringBuilder min, StringBuilder max) => min.Min(max).Should().BeOfType<StringBuilder>().And.BeSameAs(min);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.Max(StringBuilder, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.Max(null, new StringBuilder())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => new StringBuilder().Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(new StringBuilder(), new StringBuilder());
      Validate(new StringBuilder(), char.MinValue.ToString().ToStringBuilder());
      Validate(char.MaxValue.ToString().ToStringBuilder(), char.MinValue.ToString().ToStringBuilder());
    }

    return;

    static void Validate(StringBuilder min, StringBuilder max) => min.Max(max).Should().BeOfType<StringBuilder>().And.BeSameAs(max);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.MinMax(StringBuilder, StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.MinMax(null, string.Empty.ToStringBuilder())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => string.Empty.ToStringBuilder().MinMax(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(new StringBuilder(), new StringBuilder());
      Validate(new StringBuilder(), char.MinValue.ToString().ToStringBuilder());
      Validate(char.MaxValue.ToString().ToStringBuilder(), char.MinValue.ToString().ToStringBuilder());
    }

    return;

    static void Validate(StringBuilder min, StringBuilder max) => min.MinMax(max).Should().Be((min, max));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.ToStringWriter(StringBuilder, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStringWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.ToStringWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      new StringBuilder().With(builder => CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Validate(builder, culture)));
    }

    return;

    static void Validate(StringBuilder builder, IFormatProvider format = null)
    {
      using var writer = builder.ToStringWriter(format);

      writer.Should().BeOfType<StringWriter>();
      writer.GetStringBuilder().Should().BeOfType<StringBuilder>().And.BeSameAs(builder);
      writer.FormatProvider.Should().BeSameAs(format);
      writer.Encoding.Should().Be(new UnicodeEncoding(false, false));
      writer.NewLine.Should().Be(Environment.NewLine);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.ToXmlWriter(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringBuilderExtensions.ToXmlWriter(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      Validate(new StringBuilder(), string.Empty);
      Validate(new StringBuilder(), Attributes.RandomString());
    }

    return;

    static void Validate(StringBuilder builder, string xml)
    {
      var text = builder.ToString();

      using var writer = builder.ToXmlWriter();

      writer.Should().BeAssignableTo<XmlWriter>();
      writer.Settings.Should().BeOfType<XmlWriterSettings>();
      writer.WriteState.Should().Be(WriteState.Start);
      writer.XmlLang.Should().BeNull();
      writer.XmlSpace.Should().Be(XmlSpace.None);
      
      writer.WriteText(xml).Flush();
      builder.ToString().Should().Be(text + $"<?xml version=\"1.0\" encoding=\"utf-16\"?>{xml}");
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringBuilderExtensions.ToBoolean(StringBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBoolean_Method()
  {
    using (new AssertionScope())
    {
      Validate(false, null);
      Validate(false, new StringBuilder());
      Validate(false, new StringBuilder(string.Empty));
      Validate(true, new StringBuilder(char.MinValue.ToString()));
    }

    return;

    static void Validate(bool result, StringBuilder builder) => builder.ToBoolean().Should().Be(result);
  }
}