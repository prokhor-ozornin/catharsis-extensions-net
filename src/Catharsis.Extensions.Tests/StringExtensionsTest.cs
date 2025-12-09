using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StringExtensions"/>.</para>
/// </summary>
/// <seealso cref="StringExtensions"/>
public sealed class StringExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsUnset(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Property()
  {
    using (new AssertionScope())
    {
      Test(true, null);
      Test(true, string.Empty);
      Test(true, " \t\r\n ");
      Test(false, " * ");
    }

    return;

    static void Test(bool result, string text) => text.IsUnset.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsSbyte(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsSbyte_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsByte(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsByte_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsShort(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsShort_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsUshort(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUshort_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsInt(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsInt_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsUint(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUint_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsLong(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsLong_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsUlong(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUlong_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsFloat(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFloat_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsDouble(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDouble_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsDecimal(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDecimal_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsGuid(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsGuid_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsUri(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUri_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsType(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsType_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsDateTime(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateTime_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsDateTimeOffset(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateTimeOffset_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsDateOnly(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateOnly_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsTimeOnly(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsTimeOnly_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsFile(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFile_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsDirectory(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDirectory_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsIpAddress(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsIpAddress_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsBoolean(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsBoolean_Property()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, string.Empty);
      Test(true, bool.FalseString);
      Test(true, bool.TrueString);
      Test(false, "invalid");
      Test(true, "TRUE");
      Test(true, "TruE");
      Test(true, "true");
      Test(true, " true ");
    }

    return;

    static void Test(bool result, string text) => text.IsBoolean.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsUpperCased(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUpperCased_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).IsUpperCased).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(bool result, string text) => text.IsUpperCased.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsLowerCased(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsLowerCased_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).IsLowerCased).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(bool result, string text) => text.IsLowerCased.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_Lines(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_Reversed(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Reversed_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_UpperCased(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void UpperCased_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_LowerCased(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void LowerCased_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_Capitalized(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Capitalized_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_Bytes(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Bytes_Property()
  {
    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Compare(string, string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Compare_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        string.Empty.Compare(string.Empty, culture).Should().Be(0);
        string.Empty.Compare(char.MinValue.ToString(culture), culture).Should().Be(0);
        string.Empty.Compare(char.MaxValue.ToString(culture), culture).Should().BeNegative();

        char.MinValue.ToString(culture).Compare(char.MinValue.ToString(), culture).Should().Be(0);
        char.MinValue.ToString(culture).Compare(string.Empty, culture).Should().Be(0);

        char.MaxValue.ToString(culture).Compare(char.MaxValue.ToString(), culture).Should().Be(0);
        char.MaxValue.ToString(culture).Compare(string.Empty, culture).Should().BePositive();
      });
    }

    return;

    static void Test(string lesser, string bigger, CultureInfo culture = null)
    {
      string.Empty.Compare(string.Empty, culture).Should().Be(0);
      string.Empty.Compare(char.MinValue.ToString(culture), culture).Should().Be(0);
      string.Empty.Compare(char.MaxValue.ToString(culture), culture).Should().BeNegative();

      char.MinValue.ToString(culture).Compare(char.MinValue.ToString(), culture).Should().Be(0);
      char.MinValue.ToString(culture).Compare(string.Empty, culture).Should().Be(0);

      char.MaxValue.ToString(culture).Compare(char.MaxValue.ToString(), culture).Should().Be(0);
      char.MaxValue.ToString(culture).Compare(string.Empty, culture).Should().BePositive();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.CompareAsNumber(string, string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompareAsNumber_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(null);
        Test(culture);
      });
    }

    return;

    static void Test(IFormatProvider format)
    {
      format = format ?? CultureInfo.InvariantCulture;

      AssertionExtensions.Should(() => string.Empty.CompareAsNumber(byte.MinValue.ToString(format), format)).ThrowExactly<FormatException>();
      AssertionExtensions.Should(() => byte.MinValue.ToString(format).CompareAsNumber(string.Empty, format)).ThrowExactly<FormatException>();

      sbyte.MinValue.ToString(format).CompareAsNumber(sbyte.MinValue.ToString(format), format).Should().Be(0);
      sbyte.MaxValue.ToString(format).CompareAsNumber(sbyte.MaxValue.ToString(format), format).Should().Be(0);
      sbyte.MinValue.ToString(format).CompareAsNumber(sbyte.MaxValue.ToString(format), format).Should().BeNegative();

      byte.MinValue.ToString(format).CompareAsNumber(byte.MinValue.ToString(format), format).Should().Be(0);
      byte.MaxValue.ToString(format).CompareAsNumber(byte.MaxValue.ToString(format), format).Should().Be(0);
      byte.MinValue.ToString(format).CompareAsNumber(byte.MaxValue.ToString(format), format).Should().BeNegative();

      short.MinValue.ToString(format).CompareAsNumber(short.MinValue.ToString(format), format).Should().Be(0);
      short.MaxValue.ToString(format).CompareAsNumber(short.MaxValue.ToString(format), format).Should().Be(0);
      short.MinValue.ToString(format).CompareAsNumber(short.MaxValue.ToString(format), format).Should().BeNegative();

      ushort.MinValue.ToString(format).CompareAsNumber(ushort.MinValue.ToString(format), format).Should().Be(0);
      ushort.MaxValue.ToString(format).CompareAsNumber(ushort.MaxValue.ToString(format), format).Should().Be(0);
      ushort.MinValue.ToString(format).CompareAsNumber(ushort.MaxValue.ToString(format), format).Should().BeNegative();

      int.MinValue.ToString(format).CompareAsNumber(int.MinValue.ToString(format), format).Should().Be(0);
      int.MaxValue.ToString(format).CompareAsNumber(int.MaxValue.ToString(format), format).Should().Be(0);
      int.MinValue.ToString(format).CompareAsNumber(int.MaxValue.ToString(format), format).Should().BeNegative();

      uint.MinValue.ToString(format).CompareAsNumber(uint.MinValue.ToString(format), format).Should().Be(0);
      uint.MaxValue.ToString(format).CompareAsNumber(uint.MaxValue.ToString(format), format).Should().Be(0);
      uint.MinValue.ToString(format).CompareAsNumber(uint.MaxValue.ToString(format), format).Should().BeNegative();

      long.MinValue.ToString(format).CompareAsNumber(long.MinValue.ToString(format), format).Should().Be(0);
      long.MaxValue.ToString(format).CompareAsNumber(long.MaxValue.ToString(format), format).Should().Be(0);
      long.MinValue.ToString(format).CompareAsNumber(long.MaxValue.ToString(format), format).Should().BeNegative();

      ulong.MinValue.ToString(format).CompareAsNumber(ulong.MinValue.ToString(format), format).Should().Be(0);
      ulong.MaxValue.ToString(format).CompareAsNumber(ulong.MaxValue.ToString(format), format).Should().Be(0);
      ulong.MinValue.ToString(format).CompareAsNumber(ulong.MaxValue.ToString(format), format).Should().BeNegative();

      float.MinValue.ToString(format).CompareAsNumber(float.MinValue.ToString(format), format).Should().Be(0);
      float.MaxValue.ToString(format).CompareAsNumber(float.MaxValue.ToString(format), format).Should().Be(0);
      float.MinValue.ToString(format).CompareAsNumber(float.MaxValue.ToString(format), format).Should().BeNegative();
      float.NaN.ToString(format).CompareAsNumber(float.NaN.ToString(format), format).Should().Be(0);
      float.Epsilon.ToString(format).CompareAsNumber(float.Epsilon.ToString(format), format).Should().Be(0);
      float.NegativeInfinity.ToString(format).CompareAsNumber(float.NegativeInfinity.ToString(format), format).Should().Be(0);
      float.PositiveInfinity.ToString(format).CompareAsNumber(float.PositiveInfinity.ToString(format), format).Should().Be(0);

      double.MinValue.ToString(format).CompareAsNumber(double.MinValue.ToString(format), format).Should().Be(0);
      double.MaxValue.ToString(format).CompareAsNumber(double.MaxValue.ToString(format), format).Should().Be(0);
      double.MinValue.ToString(format).CompareAsNumber(double.MaxValue.ToString(format), format).Should().BeNegative();
      double.NaN.ToString(format).CompareAsNumber(double.NaN.ToString(format), format).Should().Be(0);
      double.Epsilon.ToString(format).CompareAsNumber(double.Epsilon.ToString(format), format).Should().Be(0);
      double.NegativeInfinity.ToString(format).CompareAsNumber(double.NegativeInfinity.ToString(format), format).Should().Be(0);
      double.PositiveInfinity.ToString(format).CompareAsNumber(double.PositiveInfinity.ToString(format), format).Should().Be(0);

      decimal.MinValue.ToString(format).CompareAsNumber(decimal.MinValue.ToString(format), format).Should().Be(0);
      decimal.MaxValue.ToString(format).CompareAsNumber(decimal.MaxValue.ToString(format), format).Should().Be(0);
      decimal.MinValue.ToString(format).CompareAsNumber(decimal.MaxValue.ToString(format), format).Should().BeNegative();
      decimal.MinusOne.ToString(format).CompareAsNumber(decimal.MinusOne.ToString(format), format).Should().Be(0);
      decimal.Zero.ToString(format).CompareAsNumber(decimal.Zero.ToString(format), format).Should().Be(0);
      decimal.One.ToString(format).CompareAsNumber(decimal.One.ToString(format), format).Should().Be(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.CompareAsDate(string, string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompareAsDate_Method()
  {
    using (new AssertionScope())
    {
      /*new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
        {
          Test(date, null);
          Test(date, culture);
        });*/
    }

    return;

    static void Test(DateTimeOffset date, IFormatProvider format)
    {
      AssertionExtensions.Should(() => string.Empty.CompareAsDate(date.ToString(format), format)).ThrowExactly<FormatException>();
      AssertionExtensions.Should(() => date.ToString(format).CompareAsDate(string.Empty, format)).ThrowExactly<FormatException>();

      date.ToString("o", format).CompareAsDate(date.ToString("o", format), format).Should().Be(0);
      date.AddMilliseconds(1).ToString("o", format).CompareAsDate(date.ToString("o", format), format).Should().BePositive();
      date.AddMilliseconds(-1).ToString("o", format).CompareAsDate(date.ToString("o", format), format).Should().BeNegative();

      date.ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.StartOfDay.AddMilliseconds(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.StartOfDay.AddSeconds(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.StartOfDay.AddMinutes(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.StartOfDay.AddHours(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.StartOfDay.AddDays(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().BePositive();
      date.StartOfDay.AddMonths(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().BePositive();
      date.StartOfDay.AddYears(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().BePositive();

      date.ToString("T", format).CompareAsDate(date.ToString("T", format), format).Should().Be(0);
      date.AddMilliseconds(1).ToString("T", format).CompareAsDate(date.ToString("T", format), format).Should().Be(0);
      date.AddSeconds(1).ToString("T", format).CompareAsDate(date.ToString("T", format), format).Should().BePositive();
      date.AddMinutes(1).ToString("T", format).CompareAsDate(date.ToString("T", format), format).Should().BePositive();
      date.AddHours(1).ToString("T", format).CompareAsDate(date.ToString("T", format), format).Should().BePositive();
      date.AddDays(1).ToString("T", format).CompareAsDate(date.ToString("T", format), format).Should().Be(0);
      date.AddMonths(1).ToString("T", format).CompareAsDate(date.ToString("T", format), format).Should().Be(0);
      date.AddYears(1).ToString("T", format).CompareAsDate(date.ToString("T", format), format).Should().Be(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Append(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Append_Method()
  {
    using (new AssertionScope())
    {
      Test(string.Empty, null, null);
      Test(string.Empty, string.Empty, string.Empty);
      Test("value", "value", null);
    }

    return;

    static void Test(string result, string text, string postfix) => text.Append(postfix).Should().BeOfType<string>().And.Be(text + postfix).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Prepend(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Prepend_Method()
  {
    using (new AssertionScope())
    {
      StringExtensions.Prepend(null, null).Should().BeOfType<string>().And.BeEmpty();
      string.Empty.Prepend(null).Should().BeOfType<string>().And.BeEmpty();
      string.Empty.Prepend(string.Empty).Should().BeOfType<string>().And.BeEmpty();

      "\r\n".Prepend("\t").Should().BeOfType<string>().And.BeNullOrWhiteSpace();
      "value".Prepend(null).Should().BeOfType<string>().And.Be("value");
      "second".Prepend(" & ").Prepend("first").Should().BeOfType<string>().And.Be("first & second");
    }

    return;

    static void Test(string result, string text, string prefix) => text.Prepend(prefix).Should().BeOfType<string>().And.Be(prefix + text).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.Replace(string, IEnumerable{ValueTuple{string, object}})"/></description></item>
  ///     <item><description><see cref="StringExtensions.Replace(string, ValueTuple{string, object?}[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Replace_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Replace(Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Replace((IEnumerable<(string Name, object Value)>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("replacements");

      /*string.Empty.Replace(new object()).Should().BeEmpty();
      string.Empty.Replace(new
      {
      }).Should().BeEmpty();

      const string value = "The quick brown fox jumped over the lazy dog";

      value.Replace(new
      {
        quick = "slow",
        dog = "bear",
        brown = "hazy"
      }).Should().Be("The slow hazy fox jumped over the lazy bear");*/

      static void Test(string text)
      {

      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Replace([]!)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Replace(null)).ThrowExactly<ArgumentNullException>().WithParameterName("replacements");

      /*string.Empty.Replace().Should().BeEmpty();
      string.Empty.Replace(("quick", "slow")).Should().BeEmpty();

      const string value = "The quick brown fox jumped over the lazy dog";

      value.Replace(("quick", "slow"), ("dog", "bear"), ("brown", "hazy"), ("UNSPECIFIED", string.Empty)).Should().Be("The slow hazy fox jumped over the lazy bear");*/

      static void Test(string text)
      {

      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Reverse(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Reverse_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Reverse()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      string.Empty.Reverse().Should().BeOfType<string>().And.BeSameAs(string.Empty.Reverse()).And.BeEmpty();

      var text = Fixture.Create<string>();
      var reversed = text.Reverse();
      reversed.Should().BeOfType<string>().And.Be(reversed.ToCharArray().ToText());
    }

    return;

    static void Test(string result, string text)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Repeat(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Repeat_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Repeat(0)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Repeat(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      string.Empty.Repeat(0).Should().BeOfType<string>().And.BeSameAs(string.Empty.Repeat(0)).And.BeEmpty();
      string.Empty.Repeat(1).Should().BeOfType<string>().And.BeSameAs(string.Empty.Repeat(1)).And.BeEmpty();

      const int count = 1000;

      var repeated = char.MinValue.ToString().Repeat(count);
      repeated.Should().BeOfType<string>().And.HaveLength(count);
      repeated.ToCharArray().Should().BeOfType<char[]>().And.AllBeEquivalentTo(char.MinValue);

      repeated = char.MaxValue.ToString().Repeat(count);
      repeated.Should().BeOfType<string>().And.HaveLength(count);
      repeated.ToCharArray().Should().BeOfType<char[]>().And.AllBeEquivalentTo(char.MinValue);

      "*".Repeat(0).Should().BeOfType<string>().And.BeEmpty();
      "*".Repeat(1).Should().BeOfType<string>().And.Be("*");
      "xyz".Repeat(2).Should().BeOfType<string>().And.Be("xyzxyz");
    }

    return;

    static void Test(string result, string text, int count) => text.Repeat(count).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToLines(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToLines_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToLines()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      string.Empty.ToLines().Should().BeOfType<string>().And.BeSameAs(string.Empty.ToLines()).And.BeEmpty();
      string.Empty.ToLines("\t").Should().BeOfType<string>().And.BeSameAs(string.Empty.ToLines("\t")).And.BeEmpty();

      var text = Fixture.Create<string>();
      text.ToLines().Should().BeOfType<string[]>().And.HaveCount(1).And.HaveElementAt(0, text);

      var strings = 10.Objects(() => Fixture.Create<string>()).AsArray();
      text = strings.Join(Environment.NewLine);
      var lines = text.ToLines();
      lines.Should().BeOfType<string[]>().And.HaveCount(strings.Length).And.Equal(strings);
    }

    return;

    static void Test(IEnumerable<string> result, string text, string separator = null) => text.ToLines(separator).Should().BeOfType<string[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.SwapCase(string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void SwapCase_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).SwapCase()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      var builder = new StringBuilder();

      for (var i = 'a'; i <= 'z'; i++)
      {
        builder.Append(i);
      }
      for (var i = 'а'; i <= 'я'; i++)
      {
        builder.Append(i);
      }

      var value = builder.ToString();

      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        value.SwapCase(culture).Should().BeOfType<string>().And.BeUpperCased();
        value.SwapCase(culture).SwapCase(culture).Should().BeOfType<string>().And.BeLowerCased();
      });
    }

    return;

    static void Test(string result, string text, CultureInfo culture = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Capitalize(string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Capitalize_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Capitalize()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        string.Empty.Capitalize(culture).Should().BeOfType<string>().And.BeEmpty();

        "Word & Deed".Capitalize(culture).Should().BeOfType<string>().And.Be("Word & Deed");
        "word & deed".Capitalize(culture).Should().BeOfType<string>().And.Be("Word & deed");
        "wORD & deed".Capitalize(culture).Should().BeOfType<string>().And.Be("WORD & deed");

        "Слово & Дело".Capitalize(culture).Should().BeOfType<string>().And.Be("Слово & Дело");
        "слово & дело".Capitalize(culture).Should().BeOfType<string>().And.Be("Слово & дело");
        "сЛОВО & дело".Capitalize(culture).Should().BeOfType<string>().And.Be("СЛОВО & дело");
      });
    }

    return;

    static void Test(string result, string text, CultureInfo culture = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.CapitalizeAll(string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void CapitalizeAll_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).CapitalizeAll()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      string.Empty.CapitalizeAll().Should().BeOfType<string>().And.BeEmpty();

      "Word & Deed".CapitalizeAll().Should().BeOfType<string>().And.Be("Word & Deed");
      "word & deed".CapitalizeAll().Should().BeOfType<string>().And.Be("Word & Deed");
      "wORD & deed".CapitalizeAll().Should().BeOfType<string>().And.Be("Word & Deed");

      "Слово & Дело".CapitalizeAll().Should().BeOfType<string>().And.Be("Слово & Дело");
      "слово & дело".CapitalizeAll().Should().BeOfType<string>().And.Be("Слово & Дело");
      "сЛОВО & дело".CapitalizeAll().Should().BeOfType<string>().And.Be("Слово & Дело");
    }

    return;

    static void Test(string result, string text, CultureInfo culture = null) => text.CapitalizeAll(culture).Should().BeOfType<string>().And.Be((culture ?? CultureInfo.CurrentCulture).TextInfo.ToTitleCase(text)).And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Indent(string, char, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Indent_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Indent(char.MinValue)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Indent(char.MinValue, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 2;

      string.Empty.Indent('*', 0).Should().BeOfType<string>().And.BeEmpty();
      string.Empty.Indent('*').Should().BeOfType<string>().And.Be("*");
      string.Empty.Indent('*', count).Should().BeOfType<string>().And.HaveLength(count).And.Be('*'.Repeat(count));

      "***".Indent(' ', count).Should().BeOfType<string>().And.HaveLength(count + 3).And.Be(' '.Repeat(count) + "***");
      $" 1.{Environment.NewLine} 2. ".Indent('*', count).Should().BeOfType<string>().And.HaveLength(count * 2 + 4 + Environment.NewLine.Length).And.Be('*'.Repeat(count) + "1." + Environment.NewLine + '*'.Repeat(count) + "2.");
    }

    return;

    static void Test(string result, string text, char character, int count) => text.Indent(character, count).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Unindent(string, char)"/> method.</para>
  /// </summary>
  [Fact]
  public void Unindent_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Unindent(char.MinValue)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, string text, char value) => text.Unindent(value).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Spacify(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Spacify_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Spacify()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Spacify(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 2;

      Test(string.Empty, string.Empty, 0);
      Test(" ", string.Empty, 1);
      Test(' '.Repeat(count), string.Empty, count);
      Test(' '.Repeat(count) + "***", "***", count);
      Test(' '.Repeat(count) + "1." + Environment.NewLine + ' '.Repeat(count) + "2.", $" 1.{Environment.NewLine} 2. ", count);
    }

    return;

    static void Test(string result, string text, int? count = null)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Unspacify(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Unspacify_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Unspacify()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, string result) => text.Unspacify().Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Tabify(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Tabify_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Tabify()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Tabify(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 2;

      string.Empty.Tabify(0).Should().BeOfType<string>().And.BeEmpty();
      string.Empty.Tabify().Should().BeOfType<string>().And.Be("\t");
      string.Empty.Tabify(count).Should().BeOfType<string>().And.HaveLength(count).And.Be('\t'.Repeat(count));

      "***".Tabify(count).Should().BeOfType<string>().And.HaveLength(count + 3).And.Be('\t'.Repeat(count) + "***");
      $" 1.{Environment.NewLine} 2. ".Tabify(count).Should().BeOfType<string>().And.HaveLength(count * 2 + 4 + Environment.NewLine.Length).And.Be('\t'.Repeat(count) + "1." + Environment.NewLine + '\t'.Repeat(count) + "2.");
    }

    return;

    static void Test(string result, string text, int? count = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Untabify(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Untabify_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Untabify()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, string text) => text.Untabify().Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsMatch(string, string, RegexOptions?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsMatch_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).IsMatch(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.IsMatch(null)).ThrowExactly<ArgumentNullException>().WithParameterName("pattern");

      Test(true, string.Empty, string.Empty);
      Test(false, string.Empty, "anything");
      Test(true, "ab4Zg95kf", "[a-zA-z0-9]");
      Test(false, "~#$%", "[a-zA-z0-9]");
    }

    return;

    static void Test(bool result, string text, string pattern, RegexOptions? options = null) => text.IsMatch(pattern, options).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Matches(string, string, RegexOptions?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Matches_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Matches(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Matches(null)).ThrowExactly<ArgumentNullException>().WithParameterName("pattern");

      /*string.Empty.Matches("anything").Should().BeEmpty();
      var matches = "ab#1".Matches("[a-zA-z0-9]");
      matches.Should().HaveCount(3);
      matches.ElementAt(0).Value.Should().Be("a");
      matches.ElementAt(1).Value.Should().Be("b");
      matches.ElementAt(2).Value.Should().Be("1");*/
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, string pattern, RegexOptions? options = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsSbyteInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsSbyte_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, sbyte.MinValue.ToString(culture), culture);
        Test(true, sbyte.MaxValue.ToString(culture), culture);
        Test(true, $" {sbyte.MinValue.ToString(culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsByteInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsByteInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsByte_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, byte.MinValue.ToString(culture), culture);
        Test(true, byte.MaxValue.ToString(culture), culture);
        Test(true, $" {byte.MinValue.ToString(culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsByteInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsShortInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsShort_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, short.MinValue.ToString(culture), culture);
        Test(true, short.MaxValue.ToString(culture), culture);
        Test(true, $" {short.MinValue.ToString(culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsShortInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUshortInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUshort_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, ushort.MinValue.ToString(culture), culture);
        Test(true, ushort.MaxValue.ToString(culture), culture);
        Test(true, $" {ushort.MinValue.ToString(culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsUshortInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsIntInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsInt_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, int.MinValue.ToString(culture), culture);
        Test(true, int.MaxValue.ToString(culture), culture);
        Test(true, $" {int.MinValue.ToString(culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsIntInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUintInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUint_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, uint.MinValue.ToString(culture), culture);
        Test(true, uint.MaxValue.ToString(culture), culture);
        Test(true, $" {uint.MinValue.ToString(culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsUintInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsLongInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsLong_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, long.MinValue.ToString(culture), culture);
        Test(true, long.MaxValue.ToString(culture), culture);
        Test(true, $" {long.MinValue.ToString(culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsLongInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUlongInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUlong_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, ulong.MinValue.ToString(culture), culture);
        Test(true, ulong.MaxValue.ToString(culture), culture);
        Test(true, $" {ulong.MinValue.ToString(culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsUlongInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsFloatInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFloat_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, float.MinValue.ToString(culture), culture);
        Test(true, float.MaxValue.ToString(culture), culture);
        Test(true, $" {float.MinValue.ToString(culture)} ", culture);
        Test(true, float.NaN.ToString(culture), culture);
        Test(true, float.Epsilon.ToString(culture), culture);
        Test(true, float.NegativeInfinity.ToString(culture), culture);
        Test(true, float.PositiveInfinity.ToString(culture), culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsFloatInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDoubleInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDouble_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, double.MinValue.ToString(culture), culture);
        Test(true, double.MaxValue.ToString(culture), culture);
        Test(true, $" {double.MinValue.ToString(culture)} ", culture);
        Test(true, double.NaN.ToString(culture), culture);
        Test(true, double.Epsilon.ToString(culture), culture);
        Test(true, double.NegativeInfinity.ToString(culture), culture);
        Test(true, double.PositiveInfinity.ToString(culture), culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsDoubleInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDecimalInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDecimal_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, decimal.MinValue.ToString(culture), culture);
        Test(true, decimal.MaxValue.ToString(culture), culture);
        Test(true, $" {decimal.MinValue.ToString(culture)} ", culture);
        Test(true, decimal.MinusOne.ToString(culture), culture);
        Test(true, decimal.Zero.ToString(culture), culture);
        Test(true, decimal.One.ToString(culture), culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsDecimalInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsEnum{T}(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEnum_Method()
  {
    using (new AssertionScope())
    {
      Test<DayOfWeek>(false, null);
      Test<DayOfWeek>(false, string.Empty);
      Test<DayOfWeek>(false, "invalid");

      Enum.GetNames<DayOfWeek>().ForEach(day => Test<DayOfWeek>(true, day));
      Enum.GetNames<DayOfWeek>().ForEach(day => Test<DayOfWeek>(true, day.ToLower()));
      Enum.GetNames<DayOfWeek>().ForEach(day => Test<DayOfWeek>(true, day.ToUpper()));
    }

    return;

    static void Test<T>(bool result, string text) where T : struct => text.IsEnum<T>().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsGuid(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsGuid_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, string.Empty);
      Test(false, "invalid");

      Test(true, Guid.NewGuid().ToString());
      Test(true, Guid.NewGuid().ToString().ToLowerInvariant());
      Test(true, Guid.NewGuid().ToString().ToUpperInvariant());
      Test(true, Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
    }

    return;

    static void Test(bool result, string text) => text.IsGuid.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsUri(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUri_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(true, string.Empty);
      Test(true, "path");
      Test(true, "https:");
      Test(false, "https://");
      Test(true, "https://user:password@localhost:8080/path?query#id");
    }

    return;

    static void Test(bool result, string text) => text.IsUri.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsType(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsType_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, string.Empty);
      Test(false, Fixture.Create<string>());
      Test(false, nameof(Object));
      Test(true, typeof(object).FullName);
      Test(true, typeof(object).AssemblyQualifiedName);
    }

    Assembly.GetExecutingAssembly().DefinedTypes.ForEach(type =>
    {
      Test(false, nameof(type));
      Test(true, type.AssemblyQualifiedName);
    });

    return;

    static void Test(bool result, string text) => text.IsType.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateTimeInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateTime_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null);
        Test(false, string.Empty);
        Test(false, "invalid");
        Test(true, $" {DateTime.MinValue.ToString("o", culture)} ");
        Test(true, $" {DateTime.MaxValue.ToString("o", culture)} ");
        Test(true, $" {DateTime.UtcNow.ToString("o", culture)} ");
        Test(true, $" {DateTime.Now.ToString("o", culture)} ");
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsDateTimeInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateTimeOffsetInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateTimeOffset_Method()
  {
    using (new AssertionScope())
    {
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
      {
        Test(false, null);
        Test(false, string.Empty);
        Test(false, "invalid");
        Test(true, $" {DateTimeOffset.MinValue.ToString("o", culture)} ");
        Test(true, $" {DateTimeOffset.MaxValue.ToString("o", culture)} ");
        Test(true, $" {DateTimeOffset.UtcNow.ToString("o", culture)} ");
        Test(true, $" {DateTimeOffset.Now.ToString("o", culture)} ");
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsDateTimeOffsetInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateOnlyInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateOnly_Method()
  {
    using (new AssertionScope())
    {
      new[] { null, CultureInfo.InvariantCulture }.ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, $" {DateOnly.MinValue.ToString("D", culture)} ", culture);
        Test(true, $" {DateOnly.MaxValue.ToString("D", culture)} ", culture);
        Test(true, $" {DateOnly.FromDateTime(DateTime.UtcNow).ToString("D", culture)} ", culture);
        Test(true, $" {DateOnly.FromDateTime(DateTime.Now).ToString("D", culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsDateOnlyInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsTimeOnlyInFormat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsTimeOnly_Method()
  {
    using (new AssertionScope())
    {
      new[] { null, CultureInfo.InvariantCulture }.ForEach(culture =>
      {
        Test(false, null, culture);
        Test(false, string.Empty, culture);
        Test(false, "invalid", culture);
        Test(true, $" {TimeOnly.MinValue.ToString("T", culture)} ", culture);
        Test(true, $" {TimeOnly.MaxValue.ToString("T", culture)} ", culture);
        Test(true, $" {TimeOnly.FromDateTime(DateTime.UtcNow).ToString("T", culture)} ", culture);
        Test(true, $" {TimeOnly.FromDateTime(DateTime.Now).ToString("T", culture)} ", culture);
      });
    }

    return;

    static void Test(bool result, string text, IFormatProvider format = null) => text.IsTimeOnlyInFormat(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsFile(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFile_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, string.Empty);
      Test(false, Fixture.Create<string>());
      Test(false, Environment.SystemDirectory);
      EmptyFile.TryFinallyDelete(file => Test(true, file.FullName));
    }

    return;

    static void Test(bool result, string text) => text.IsFile.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsDirectory(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDirectory_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, string.Empty);
      Test(false, Fixture.Create<string>());
      Test(true, Environment.SystemDirectory);
      Directory.TryFinallyDelete(directory => Test(true, directory.FullName));
    }

    return;

    static void Test(bool result, string text) => text.IsDirectory.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.get_IsIpAddress(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsIpAddress_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, string.Empty);
      Test(false, "localhost");

      new[]
      {
        IPAddress.None, IPAddress.Any, IPAddress.Loopback, IPAddress.Broadcast, IPAddress.IPv6None, IPAddress.IPv6Any,
        IPAddress.IPv6Loopback
      }.ForEach(address => Test(true, address.ToString()));
    }

    return;

    static void Test(bool result, string text) => text.IsIpAddress.Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.Execute(string, IEnumerable{string})"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToProcess(string,string[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Execute_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.Execute(null)).ThrowExactly<ArgumentNullException>().WithParameterName("command");
      AssertionExtensions.Should(() => string.Empty.Execute()).ThrowExactly<InvalidOperationException>();

      string[] arguments = ["dir"];

      var process = Shell.ToProcess(arguments);

      process.Finish(TimeSpan.FromSeconds(5));

      process.Should().BeOfType<Process>();

      process.Id.Should().BePositive();
      process.HasExited.Should().BeTrue();
      process.ExitCode.Should().Be(-1);
      process.StartTime.Should().BeBefore(DateTime.Now);
      process.ExitTime.Should().BeBefore(DateTime.Now);

      process.StartInfo.FileName.Should().Be(Shell);
      process.StartInfo.ArgumentList.Should().Equal(arguments);
      process.StartInfo.Arguments.Should().BeEmpty();
      process.StartInfo.CreateNoWindow.Should().BeTrue();
      //process.StartInfo.Domain.Should().BeEmpty();
      process.StartInfo.Environment.Should().NotBeNullOrEmpty().And.HaveCount(Environment.GetEnvironmentVariables().Count);
      process.StartInfo.ErrorDialog.Should().BeFalse();
      process.StartInfo.ErrorDialogParentHandle.Should().Be(0);
      //process.StartInfo.LoadUserProfile.Should().BeFalse();
      //process.StartInfo.Password.Should().BeNull();
      //process.StartInfo.PasswordInClearText.Should().BeNull();
      process.StartInfo.RedirectStandardError.Should().BeTrue();
      process.StartInfo.RedirectStandardInput.Should().BeTrue();
      process.StartInfo.RedirectStandardOutput.Should().BeTrue();
      process.StartInfo.StandardErrorEncoding.Should().BeNull();
      process.StartInfo.StandardInputEncoding.Should().BeNull();
      process.StartInfo.StandardOutputEncoding.Should().BeNull();
      process.StartInfo.UserName.Should().BeEmpty();
      process.StartInfo.UseShellExecute.Should().BeFalse();
      process.StartInfo.Verb.Should().BeEmpty();
      process.StartInfo.WindowStyle.Should().Be(ProcessWindowStyle.Normal);
      process.StartInfo.WorkingDirectory.Should().BeEmpty();

      static void Test(string text)
      {

      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToProcess(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("command");
      AssertionExtensions.Should(() => string.Empty.ToProcess([])).ThrowExactly<InvalidOperationException>();

      static void Test(string text)
      {

      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.FromBase64(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void FromBase64_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.FromBase64(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      string.Empty.FromBase64().Should().BeOfType<byte[]>().And.BeSameAs(string.Empty.FromBase64()).And.BeEmpty();

      var bytes = Bytes;
      bytes.ToBase64().Should().BeOfType<string>().And.Be(System.Convert.ToBase64String(bytes));
    }

    return;

    static void Test(byte[] result, string text) => text.FromBase64().Should().BeOfType<byte[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.FromHex(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void FromHex_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.FromHex(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      string.Empty.FromHex().Should().BeOfType<byte[]>().And.BeSameAs(string.Empty.FromHex()).And.BeEmpty();

      var bytes = Bytes;
      bytes.ToHex().Should().BeOfType<string>().And.Be(System.Convert.ToHexString(bytes));
    }

    return;

    static void Test(byte[] result, string text) => text.FromHex().Should().BeOfType<byte[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.UrlEncode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void UrlEncode_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.UrlEncode(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test("#value?");
    }

    return;

    static void Test(string text) => text.UrlEncode().Should().BeOfType<string>().And.Be(Uri.EscapeDataString(text));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.UrlDecode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void UrlDecode_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.UrlDecode(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test("%23value%3F");
    }

    return;

    static void Test(string text) => text.UrlDecode().Should().BeOfType<string>().And.Be(Uri.UnescapeDataString(text));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HtmlEncode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HtmlEncode_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.HtmlEncode(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test("<p>word & deed</p>");
    }

    return;

    static void Test(string text) => text.HtmlEncode().Should().BeOfType<string>().And.Be(WebUtility.HtmlEncode(text));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HtmlDecode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HtmlDecode_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.HtmlDecode(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test("&lt;p&gt;word &amp; deed&lt;/p&gt;");
    }

    return;

    static void Test(string text) => text.HtmlDecode().Should().BeOfType<string>().And.Be(WebUtility.HtmlDecode(text));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Hash(string, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Hash_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => string.Empty.Hash(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

      using var algorithm = MD5.Create();

      AssertionExtensions.Should(() => ((string) null).Hash(HashAlgorithm)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      algorithm.Should().BeOfType<MD5>();

      string[] texts = [string.Empty, Fixture.Create<string>()];

      texts.ForEach(text =>
      {
        text.Hash(HashAlgorithm).Should().BeOfType<string>().And.HaveLength(algorithm.HashSize / 4).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
      });
    }

    return;

    static void Test()
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashMd5(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashMd5_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).HashMd5()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test(Fixture.Create<string>());
    }

    return;

    static void Test(string text)
    {
      using var algorithm = MD5.Create();
      text.HashMd5().Should().BeOfType<string>().And.HaveLength(32).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashSha1(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha1_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).HashSha1()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test(Fixture.Create<string>());
    }

    return;

    static void Test(string text)
    {
      using var algorithm = SHA1.Create();
      text.HashSha1().Should().BeOfType<string>().And.HaveLength(40).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashSha256(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha256_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).HashSha256()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test(Fixture.Create<string>());
    }

    return;

    static void Test(string text)
    {
      using var algorithm = SHA256.Create();
      text.HashSha256().Should().BeOfType<string>().And.HaveLength(64).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashSha384(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha384_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).HashSha384()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test(Fixture.Create<string>());
    }

    return;

    static void Test(string text)
    {
      using var algorithm = SHA384.Create();
      text.HashSha384().Should().BeOfType<string>().And.HaveLength(96).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashSha512(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha512_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).HashSha512()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test(Fixture.Create<string>());
    }

    return;

    static void Test(string text)
    {
      using var algorithm = SHA512.Create();
      text.HashSha512().Should().BeOfType<string>().And.HaveLength(128).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Min(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Min(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

      Test(string.Empty, string.Empty);
      Test(string.Empty, char.MinValue.ToString());
      Test(char.MaxValue.ToString(), char.MinValue.ToString());
      Test(char.MaxValue.ToString(), char.MinValue.Repeat(2));
    }

    return;

    static void Test(string min, string max) => min.Min(max).Should().BeOfType<string>().And.Be(min);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Max(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Max(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

      Test(string.Empty, string.Empty);
      Test(string.Empty, char.MinValue.ToString());
      Test(char.MaxValue.ToString(), char.MinValue.ToString());
      Test(char.MaxValue.ToString(), char.MinValue.Repeat(2));
    }

    return;

    static void Test(string min, string max) => min.Max(max).Should().BeOfType<string>().And.Be(max);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.MinMax(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).MinMax(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.MinMax(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");
    }

    throw new NotImplementedException();

    return;

    static void Test(string min, string max) => min.MinMax(max).Should().Be((min, max));
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.With(string, IEnumerable{char})"/></description></item>
  ///     <item><description><see cref="StringExtensions.With(string, char[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).With(Enumerable.Empty<char>())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.With((IEnumerable<char>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      static void Test<T>(string text, IEnumerable<char> characters)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.With(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      static void Test<T>(string text, params char[] characters)
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.Without(string, IEnumerable{int})"/></description></item>
  ///     <item><description><see cref="StringExtensions.Without(string, int[])"/></description></item>
  ///     <item><description><see cref="StringExtensions.Without(string, int, int?, Predicate{char})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      static void Test(string text, IEnumerable<int> positions)
      {
      }
    }

    using (new AssertionScope())
    {
      static void Test(string text, int[] positions)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).Without(0)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Without(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
      AssertionExtensions.Should(() => string.Empty.Without(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Test(string text)
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.DeserializeAsDataContract{T}(string, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, params Type[] types)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.DeserializeAsXml{T}(string, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      /*var subject = Guid.Empty;
      subject.AsXml().AsXml<Guid>().Should().Be(subject);*/
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, params Type[] types)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, Stream, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(System.IO.Stream.Null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo((Stream) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test(Stream stream, string text, Encoding encoding = null)
    {
      using (stream)
      {
        text.WriteTo(stream, encoding).Should().BeOfType<string>().And.BeSameAs(text);
        //
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, Stream, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_Stream_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(System.IO.Stream.Null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(System.IO.Stream.Null, null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(Stream stream, string text, Encoding encoding = null)
    {
      using (stream)
      {
        var task = text.WriteToAsync(stream, encoding);
        task.Should().BeAssignableTo<Task<string>>();
        task.Await().Should().BeOfType<string>();
        //
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(System.IO.Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, TextWriter to)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, TextWriter, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_TextWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(System.IO.Stream.Null.ToStreamWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync((TextWriter) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(System.IO.Stream.Null.ToStreamWriter())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, TextWriter to)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_BinaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(System.IO.Stream.Null.ToBinaryWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo((BinaryWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

      Test(string.Empty, System.IO.Stream.Null.ToBinaryWriter());
      Test(Fixture.Create<string>(), EmptyStream.ToBinaryWriter());
    }

    return;

    static void Test(string text, BinaryWriter to)
    {
      using (to)
      {
        text.WriteTo(to).Should().BeOfType<string>().And.BeSameAs(text);

        using (var reader = to.BaseStream.MoveToStart().ToBinaryReader())
        {
          reader.ToText().Should().BeOfType<string>().And.Be(text);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(System.IO.Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, XmlWriter to)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_XmlWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(System.IO.Stream.Null.ToXmlWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, XmlWriter to)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(FakeFile)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, FileInfo to, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, FileInfo, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_FileInfo_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(FakeFile)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(FakeFile, null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, FileInfo to, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_Process_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo((Process) null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, Process to)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_Process_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync((Process) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
      //AssertionExtensions.Should(() => string.Empty.WriteToAsync(ShellProcess)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, Process to)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_Uri_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo("localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo((Uri) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, Uri to, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, Uri, Encoding, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_Uri_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync("localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync((Uri) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync("localhost".ToUri(), null, null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, Uri to, Encoding encoding = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_HttpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(Fixture.Create<HttpClient>(), "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo(null, "localhost".ToUri())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
      AssertionExtensions.Should(() => string.Empty.WriteTo(Fixture.Create<HttpClient>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_HttpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(Fixture.Create<HttpClient>(), "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(null, "localhost".ToUri())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(Fixture.Create<HttpClient>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(Fixture.Create<HttpClient>(), "localhost".ToUri())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, HttpClient client, Uri uri)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_TcpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(Fixture.Create<TcpClient>())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo(Fixture.Create<TcpClient>())).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, TcpClient to, Encoding encoding = null)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, TcpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_TcpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(Fixture.Create<TcpClient>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(Fixture.Create<TcpClient>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(Fixture.Create<TcpClient>(), null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, TcpClient to, Encoding encoding = null)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_UdpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(Fixture.Create<UdpClient>())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo(Fixture.Create<UdpClient>())).ThrowExactly<ArgumentNullException>().WithParameterName("udp");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, UdpClient to, Encoding encoding = null)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, UdpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_UdpClient_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteToAsync(Fixture.Create<UdpClient>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(Fixture.Create<UdpClient>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
      AssertionExtensions.Should(() => string.Empty.WriteToAsync(Fixture.Create<UdpClient>(), null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text, UdpClient to, Encoding encoding = null)
    {
      using (to)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToBytes(string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(Fixture.Create<string>());
      Encoding.GetEncodings().ForEach(encoding => Test(Fixture.Create<string>(), encoding.GetEncoding()));
    }

    return;

    static void Test(string text, Encoding encoding = null)
    {
      string.Empty.ToBytes(encoding).Should().BeOfType<byte[]>().And.BeSameAs(string.Empty.ToBytes(encoding)).And.BeEmpty();

      var bytes = text.ToBytes(encoding);
      bytes.Should().BeOfType<byte[]>().And.HaveCount((encoding ?? Encoding.Default).GetByteCount(text));
      bytes.ToText(encoding).Should().BeOfType<string>().And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToBoolean(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToBoolean(string, out bool?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToBoolean_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToBoolean()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(string.Empty.ToBoolean).ThrowExactly<FormatException>();
      AssertionExtensions.Should("invalid".ToBoolean).ThrowExactly<FormatException>();

      /*bool.FalseString.ToBoolean().Should().BeFalse();
      bool.TrueString.ToBoolean().Should().BeTrue();

      "TRUE".ToBoolean().Should().BeTrue();
      "TruE".ToBoolean().Should().BeTrue();
      "true".ToBoolean().Should().BeTrue();
      " true ".ToBoolean().Should().BeTrue();

      "FALSE".ToBoolean().Should().BeFalse();
      "FalsE".ToBoolean().Should().BeFalse();
      "false".ToBoolean().Should().BeFalse();
      " false ".ToBoolean().Should().BeFalse();*/

      static void Test(string text) => text.ToBoolean().Should().Be(bool.Parse(text));
    }

    using (new AssertionScope())
    {
      /*StringExtensions.ToBoolean(null, out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToBoolean(out result).Should().BeFalse();
      result.Should().BeNull();

      "invalid".ToBoolean(out result).Should().BeFalse();
      result.Should().BeNull();

      bool.FalseString.ToBoolean(out result).Should().BeTrue();
      result.Should().BeFalse();

      bool.TrueString.ToBoolean(out result).Should().BeTrue();
      result.Should().BeTrue();

      "TRUE".ToBoolean(out result).Should().BeTrue();
      result.Should().BeTrue();

      "TruE".ToBoolean(out result).Should().BeTrue();
      result.Should().BeTrue();

      "true".ToBoolean(out result).Should().BeTrue();
      result.Should().BeTrue();

      " true ".ToBoolean(out result).Should().BeTrue();
      result.Should().BeTrue();

      "FALSE".ToBoolean(out result).Should().BeTrue();
      result.Should().BeFalse();

      "FalsE".ToBoolean(out result).Should().BeTrue();
      result.Should().BeFalse();

      "false".ToBoolean(out result).Should().BeTrue();
      result.Should().BeFalse();

      " false ".ToBoolean(out result).Should().BeTrue();
      result.Should().BeFalse();*/

      static void Test(bool result, string text)
      {
        text.ToBoolean(out var value).Should().Be(result);

        if (result)
        {
          value.Should().Be(bool.Parse(text));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToSbyte(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToSbyte(string, out sbyte?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToSbyte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToSbyte()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToSbyte(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToSbyte(format)).ThrowExactly<FormatException>();

        sbyte.MinValue.ToString(format).ToSbyte(format).Should().Be(sbyte.MinValue);
        sbyte.MaxValue.ToString(format).ToSbyte(format).Should().Be(sbyte.MaxValue);

        $" {sbyte.MinValue.ToString(format)} ".ToSbyte(format).Should().Be(sbyte.MinValue);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToSbyte(format).Should().Be(sbyte.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToSbyte(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToSbyte(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToSbyte(out result, format).Should().BeFalse();
        result.Should().BeNull();

        sbyte.MinValue.ToString(format).ToSbyte(out result, format).Should().BeTrue();
        result.Should().Be(sbyte.MinValue);

        sbyte.MaxValue.ToString(format).ToSbyte(out result, format).Should().BeTrue();
        result.Should().Be(sbyte.MaxValue);

        $" {sbyte.MinValue.ToString(format)} ".ToSbyte(out result, format).Should().BeTrue();
        result.Should().Be(sbyte.MinValue);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToSbyte(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(sbyte.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToByte(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToByte(string, out byte?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToByte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToByte()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToByte(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToByte(format)).ThrowExactly<FormatException>();

        byte.MinValue.ToString(format).ToByte(format).Should().Be(byte.MinValue);
        byte.MaxValue.ToString(format).ToByte(format).Should().Be(byte.MaxValue);

        $" {byte.MinValue.ToString(format)} ".ToByte(format).Should().Be(byte.MinValue);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToByte(format).Should().Be(byte.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToByte(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToByte(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToByte(out result, format).Should().BeFalse();
        result.Should().BeNull();

        byte.MinValue.ToString(format).ToByte(out result, format).Should().BeTrue();
        result.Should().Be(byte.MinValue);

        byte.MaxValue.ToString(format).ToByte(out result, format).Should().BeTrue();
        result.Should().Be(byte.MaxValue);

        $" {byte.MinValue.ToString(format)} ".ToByte(out result, format).Should().BeTrue();
        result.Should().Be(byte.MinValue);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToByte(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(byte.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToShort(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToShort(string, out short?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToShort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToShort()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToShort(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToShort(format)).ThrowExactly<FormatException>();

        short.MinValue.ToString(format).ToShort(format).Should().Be(short.MinValue);
        short.MaxValue.ToString(format).ToShort(format).Should().Be(short.MaxValue);

        $" {short.MinValue.ToString(format)} ".ToShort(format).Should().Be(short.MinValue);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToShort(format).Should().Be(short.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToShort(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToShort(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToShort(out result, format).Should().BeFalse();
        result.Should().BeNull();

        short.MinValue.ToString(format).ToShort(out result, format).Should().BeTrue();
        result.Should().Be(short.MinValue);

        short.MaxValue.ToString(format).ToShort(out result, format).Should().BeTrue();
        result.Should().Be(short.MaxValue);

        $" {short.MinValue.ToString(format)} ".ToShort(out result, format).Should().BeTrue();
        result.Should().Be(short.MinValue);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToShort(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(short.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToUshort(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToUshort(string, out ushort?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToUshort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToUshort()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToUshort(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUshort(format)).ThrowExactly<FormatException>();

        ushort.MinValue.ToString(format).ToUshort(format).Should().Be(ushort.MinValue);
        ushort.MaxValue.ToString(format).ToUshort(format).Should().Be(ushort.MaxValue);

        $" {ushort.MinValue.ToString(format)} ".ToUshort(format).Should().Be(ushort.MinValue);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToUshort(format).Should().Be(ushort.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToUshort(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToUshort(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToUshort(out result, format).Should().BeFalse();
        result.Should().BeNull();

        ushort.MinValue.ToString(format).ToUshort(out result, format).Should().BeTrue();
        result.Should().Be(ushort.MinValue);

        ushort.MaxValue.ToString(format).ToUshort(out result, format).Should().BeTrue();
        result.Should().Be(ushort.MaxValue);

        $" {ushort.MinValue.ToString(format)} ".ToUshort(out result, format).Should().BeTrue();
        result.Should().Be(ushort.MinValue);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToUshort(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(ushort.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToInt(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToInt(string, out int?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToInt_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToInt()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToInt(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToInt(format)).ThrowExactly<FormatException>();

        int.MinValue.ToString(format).ToInt(format).Should().Be(int.MinValue);
        int.MaxValue.ToString(format).ToInt(format).Should().Be(int.MaxValue);

        $" {int.MinValue.ToString(format)} ".ToInt(format).Should().Be(int.MinValue);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToInt(format).Should().Be(int.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToInt(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToInt(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToInt(out result, format).Should().BeFalse();
        result.Should().BeNull();

        int.MinValue.ToString(format).ToInt(out result, format).Should().BeTrue();
        result.Should().Be(int.MinValue);

        int.MaxValue.ToString(format).ToInt(out result, format).Should().BeTrue();
        result.Should().Be(int.MaxValue);

        $" {int.MinValue.ToString(format)} ".ToInt(out result, format).Should().BeTrue();
        result.Should().Be(int.MinValue);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToInt(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(int.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToUint(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToUint(string, out uint?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToUint_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToUint()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToUint(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUint(format)).ThrowExactly<FormatException>();

        uint.MinValue.ToString(format).ToUint(format).Should().Be(uint.MinValue);
        uint.MaxValue.ToString(format).ToUint(format).Should().Be(uint.MaxValue);

        $" {uint.MinValue.ToString(format)} ".ToUint(format).Should().Be(uint.MinValue);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToUint(format).Should().Be(uint.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToUint(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToUint(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToUint(out result, format).Should().BeFalse();
        result.Should().BeNull();

        uint.MinValue.ToString(format).ToUint(out result, format).Should().BeTrue();
        result.Should().Be(uint.MinValue);

        uint.MaxValue.ToString(format).ToUint(out result, format).Should().BeTrue();
        result.Should().Be(uint.MaxValue);

        $" {uint.MinValue.ToString(format)} ".ToUint(out result, format).Should().BeTrue();
        result.Should().Be(uint.MinValue);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToUint(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(uint.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToLong(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToLong(string, out long?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToLong_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToLong()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToLong(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToLong(format)).ThrowExactly<FormatException>();

        long.MinValue.ToString(format).ToLong(format).Should().Be(long.MinValue);
        long.MaxValue.ToString(format).ToLong(format).Should().Be(long.MaxValue);

        $" {long.MinValue.ToString(format)} ".ToLong(format).Should().Be(long.MinValue);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToLong(format).Should().Be(long.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToLong(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToLong(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToLong(out result, format).Should().BeFalse();
        result.Should().BeNull();

        long.MinValue.ToString(format).ToLong(out result, format).Should().BeTrue();
        result.Should().Be(long.MinValue);

        long.MaxValue.ToString(format).ToLong(out result, format).Should().BeTrue();
        result.Should().Be(long.MaxValue);

        $" {long.MinValue.ToString(format)} ".ToLong(out result, format).Should().BeTrue();
        result.Should().Be(long.MinValue);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToLong(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(long.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToUlong(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToUlong(string, out ulong?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToUlong_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToUlong()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToUlong(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUlong(format)).ThrowExactly<FormatException>();

        ulong.MinValue.ToString(format).ToUlong(format).Should().Be(ulong.MinValue);
        ulong.MaxValue.ToString(format).ToUlong(format).Should().Be(ulong.MaxValue);

        $" {ulong.MinValue.ToString(format)} ".ToUlong(format).Should().Be(ulong.MinValue);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToUlong(format).Should().Be(ulong.Parse(text));
    }

    using (new AssertionScope())
    {

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToUlong(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToUlong(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToUlong(out result, format).Should().BeFalse();
        result.Should().BeNull();

        ulong.MinValue.ToString(format).ToUlong(out result, format).Should().BeTrue();
        result.Should().Be(ulong.MinValue);

        ulong.MaxValue.ToString(format).ToUlong(out result, format).Should().BeTrue();
        result.Should().Be(ulong.MaxValue);

        $" {ulong.MinValue.ToString(format)} ".ToUlong(out result, format).Should().BeTrue();
        result.Should().Be(ulong.MinValue);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToUlong(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(ulong.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToFloat(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToFloat(string, out float?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToFloat_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToFloat()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToFloat(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToFloat(format)).ThrowExactly<FormatException>();

        float.MinValue.ToString(format).ToFloat(format).Should().Be(float.MinValue);
        float.MaxValue.ToString(format).ToFloat(format).Should().Be(float.MaxValue);
        $" {float.MinValue.ToString(format)} ".ToFloat(format).Should().Be(float.MinValue);

        float.NaN.ToString(format).ToFloat(format).Should().Be(float.NaN);
        float.Epsilon.ToString(format).ToFloat(format).Should().Be(float.Epsilon);
        float.NegativeInfinity.ToString(format).ToFloat(format).Should().Be(float.NegativeInfinity);
        float.PositiveInfinity.ToString(format).ToFloat(format).Should().Be(float.PositiveInfinity);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToFloat(format).Should().Be(float.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToFloat(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToFloat(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToFloat(out result, format).Should().BeFalse();
        result.Should().BeNull();

        float.MinValue.ToString(format).ToFloat(out result, format).Should().BeTrue();
        result.Should().Be(float.MinValue);

        float.MaxValue.ToString(format).ToFloat(out result, format).Should().BeTrue();
        result.Should().Be(float.MaxValue);

        $" {float.MinValue.ToString(format)} ".ToFloat(out result, format).Should().BeTrue();
        result.Should().Be(float.MinValue);

        float.NaN.ToString(format).ToFloat(out result, format).Should().BeTrue();
        result.Should().Be(float.NaN);

        float.Epsilon.ToString(format).ToFloat(out result, format).Should().BeTrue();
        result.Should().Be(float.Epsilon);

        float.NegativeInfinity.ToString(format).ToFloat(out result, format).Should().BeTrue();
        result.Should().Be(float.NegativeInfinity);

        float.PositiveInfinity.ToString(format).ToFloat(out result, format).Should().BeTrue();
        result.Should().Be(float.PositiveInfinity);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToFloat(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(float.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToDouble(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToDouble(string, out double?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDouble_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToDouble()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToDouble(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDouble(format)).ThrowExactly<FormatException>();

        double.MinValue.ToString(format).ToDouble(format).Should().Be(double.MinValue);
        double.MaxValue.ToString(format).ToDouble(format).Should().Be(double.MaxValue);
        $" {double.MinValue.ToString(format)} ".ToDouble(format).Should().Be(double.MinValue);

        double.NaN.ToString(format).ToDouble(format).Should().Be(double.NaN);
        double.Epsilon.ToString(format).ToDouble(format).Should().Be(double.Epsilon);
        double.NegativeInfinity.ToString(format).ToDouble(format).Should().Be(double.NegativeInfinity);
        double.PositiveInfinity.ToString(format).ToDouble(format).Should().Be(double.PositiveInfinity);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToDouble(format).Should().Be(double.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToDouble(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToDouble(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToDouble(out result, format).Should().BeFalse();
        result.Should().BeNull();

        double.MinValue.ToString(format).ToDouble(out result, format).Should().BeTrue();
        result.Should().Be(double.MinValue);

        double.MaxValue.ToString(format).ToDouble(out result, format).Should().BeTrue();
        result.Should().Be(double.MaxValue);

        $" {double.MinValue.ToString(format)} ".ToDouble(out result, format).Should().BeTrue();
        result.Should().Be(double.MinValue);

        double.NaN.ToString(format).ToDouble(out result, format).Should().BeTrue();
        result.Should().Be(double.NaN);

        double.Epsilon.ToString(format).ToDouble(out result, format).Should().BeTrue();
        result.Should().Be(double.Epsilon);

        double.NegativeInfinity.ToString(format).ToDouble(out result, format).Should().BeTrue();
        result.Should().Be(double.NegativeInfinity);

        double.PositiveInfinity.ToString(format).ToDouble(out result, format).Should().BeTrue();
        result.Should().Be(double.PositiveInfinity);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToDouble(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(double.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToDecimal(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToDecimal(string, out decimal?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDecimal_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToDecimal()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToDecimal(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDecimal(format)).ThrowExactly<FormatException>();

        decimal.MinValue.ToString(format).ToDecimal(format).Should().Be(decimal.MinValue);
        decimal.MaxValue.ToString(format).ToDecimal(format).Should().Be(decimal.MaxValue);
        $" {decimal.MinValue.ToString(format)} ".ToDecimal(format).Should().Be(decimal.MinValue);

        decimal.MinusOne.ToString(format).ToDecimal(format).Should().Be(decimal.MinusOne);
        decimal.Zero.ToString(format).ToDecimal(format).Should().Be(decimal.Zero);
        decimal.One.ToString(format).ToDecimal(format).Should().Be(decimal.One);
      }*/

      static void Test(string text, IFormatProvider format = null) => text.ToDecimal(format).Should().Be(decimal.Parse(text));
    }

    using (new AssertionScope())
    {
      //Test(null);
      //CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);

      /*static void Test(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToDecimal(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToDecimal(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToDecimal(out result, format).Should().BeFalse();
        result.Should().BeNull();

        decimal.MinValue.ToString(format).ToDecimal(out result, format).Should().BeTrue();
        result.Should().Be(decimal.MinValue);

        decimal.MaxValue.ToString(format).ToDecimal(out result, format).Should().BeTrue();
        result.Should().Be(decimal.MaxValue);

        $" {decimal.MinValue.ToString(format)} ".ToDecimal(out result, format).Should().BeTrue();
        result.Should().Be(decimal.MinValue);

        decimal.MinusOne.ToString(format).ToDecimal(out result, format).Should().BeTrue();
        result.Should().Be(decimal.MinusOne);

        decimal.Zero.ToString(format).ToDecimal(out result, format).Should().BeTrue();
        result.Should().Be(decimal.Zero);

        decimal.One.ToString(format).ToDecimal(out result, format).Should().BeTrue();
        result.Should().Be(decimal.One);
      }*/

      static void Test(bool result, string text, IFormatProvider format = null)
      {
        text.ToDecimal(out var value, format).Should().Be(result);

        if (result)
        {
          value.Should().Be(decimal.Parse(text, format));
        }
        else
        {
          value.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToEnum{T}(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToEnum{T}(string, out Nullable{T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToEnum_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToEnum<Guid>()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => Guid.NewGuid().ToString().ToEnum<Guid>()).ThrowExactly<ArgumentException>();
      AssertionExtensions.Should(string.Empty.ToEnum<DayOfWeek>).ThrowExactly<ArgumentException>();
      AssertionExtensions.Should("invalid".ToEnum<DayOfWeek>).ThrowExactly<ArgumentException>();

      Enum.GetNames<DayOfWeek>().Select(day => day.ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());
      Enum.GetNames<DayOfWeek>().Select(day => day.ToLower().ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());
      Enum.GetNames<DayOfWeek>().Select(day => day.ToUpper().ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());

      static void Test()
      {

      }
    }

    using (new AssertionScope())
    {
      StringExtensions.ToEnum<DayOfWeek>(null, out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToEnum(out result).Should().BeFalse();
      result.Should().BeNull();

      "invalid".ToEnum(out result).Should().BeFalse();
      result.Should().BeNull();

      Enum.GetNames<DayOfWeek>().ForEach(day =>
      {
        day.ToEnum<DayOfWeek>(out var result).Should().BeTrue();
        result.Should().Be(Enum.Parse<DayOfWeek>(day));
      });

      Enum.GetNames<DayOfWeek>().ForEach(day =>
      {
        day.ToLower().ToEnum<DayOfWeek>(out var result).Should().BeTrue();
        result.Should().Be(Enum.Parse<DayOfWeek>(day));
      });

      Enum.GetNames<DayOfWeek>().ForEach(day =>
      {
        day.ToUpper().ToEnum<DayOfWeek>(out var result).Should().BeTrue();
        result.Should().Be(Enum.Parse<DayOfWeek>(day));
      });

      static void Test()
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToGuid(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToGuid(string, out Guid?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToGuid_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToGuid()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(string.Empty.ToGuid).ThrowExactly<FormatException>();
      AssertionExtensions.Should("invalid".ToGuid).ThrowExactly<FormatException>();

      new[] { Guid.Empty, Guid.NewGuid() }.ForEach(guid =>
      {
        Test(guid.ToString());
        Test(guid.ToString().ToLowerInvariant());
        Test(guid.ToString().ToUpperInvariant());
        Test(guid.ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
      });

      static void Test(string text) => text.ToGuid().Should().Be(Guid.Parse(text));
    }

    using (new AssertionScope())
    {
      Test(false, string.Empty);
      Test(false, "invalid");

      new[] { Guid.Empty, Guid.NewGuid() }.ForEach(guid =>
      {
        Test(true, guid.ToString());
        Test(true, guid.ToString().ToLowerInvariant());
        Test(true, guid.ToString().ToUpperInvariant());
        Test(true, guid.ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
      });

      static void Test(bool result, string text)
      {
        text.ToGuid(out var guid).Should().Be(result);

        if (result)
        {
          guid.Should().Be(Guid.Parse(text));
        }
        else
        {
          guid.Should().BeNull();
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToUri(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToUri(string, out Uri)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToUri_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToUri()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      var uri = string.Empty.ToUri();
      uri.IsAbsoluteUri.Should().BeFalse();
      uri.OriginalString.Should().BeEmpty();
      uri.ToString().Should().BeEmpty();

      uri = "path".ToUri();
      uri.IsAbsoluteUri.Should().BeFalse();
      uri.OriginalString.Should().Be("path");
      uri.ToString().Should().Be("path");

      uri = "scheme:".ToUri();
      uri.IsAbsoluteUri.Should().BeTrue();
      uri.OriginalString.Should().Be("scheme:");
      uri.ToString().Should().Be("scheme:");

      uri = "https://user:password@localhost:8080/path?query#id".ToUri();
      uri.IsAbsoluteUri.Should().BeTrue();
      uri.OriginalString.Should().Be("https://user:password@localhost:8080/path?query#id");
      uri.ToString().Should().Be("https://user:password@localhost:8080/path?query#id");
      uri.AbsolutePath.Should().Be("/path");
      uri.AbsoluteUri.Should().Be("https://user:password@localhost:8080/path?query#id");
      uri.Authority.Should().Be("localhost:8080");
      uri.Fragment.Should().Be("#id");
      uri.Host.Should().Be("localhost");
      uri.IsDefaultPort.Should().BeFalse();
      uri.IsFile.Should().BeFalse();
      uri.IsLoopback.Should().BeTrue();
      uri.IsUnc.Should().BeFalse();
      uri.LocalPath.Should().Be("/path");
      uri.PathAndQuery.Should().Be("/path?query");
      uri.Port.Should().Be(8080);
      uri.Query.Should().Be("?query");
      uri.Scheme.Should().Be(Uri.UriSchemeHttps);
      uri.UserEscaped.Should().BeFalse();
      uri.UserInfo.Should().Be("user:password");

      static void Test(string text)
      {

      }
    }

    using (new AssertionScope())
    {
      StringExtensions.ToUri(null, out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToUri(out result);
      result.Should().BeOfType<Uri>();
      result.IsAbsoluteUri.Should().BeFalse();
      result.OriginalString.Should().BeEmpty();
      result.ToString().Should().BeEmpty();

      "path".ToUri(out result);
      result.Should().BeOfType<Uri>();
      result.IsAbsoluteUri.Should().BeFalse();
      result.OriginalString.Should().Be("path");
      result.ToString().Should().Be("path");

      "scheme:".ToUri(out result);
      result.Should().BeOfType<Uri>();
      result.IsAbsoluteUri.Should().BeTrue();
      result.OriginalString.Should().Be("scheme:");
      result.ToString().Should().Be("scheme:");

      "https://user:password@localhost:8080/path?query#id".ToUri(out result);
      result.Should().BeOfType<Uri>();
      result.IsAbsoluteUri.Should().BeTrue();
      result.OriginalString.Should().Be("https://user:password@localhost:8080/path?query#id");
      result.ToString().Should().Be("https://user:password@localhost:8080/path?query#id");
      result.AbsolutePath.Should().Be("/path");
      result.AbsoluteUri.Should().Be("https://user:password@localhost:8080/path?query#id");
      result.Authority.Should().Be("localhost:8080");
      result.Fragment.Should().Be("#id");
      result.Host.Should().Be("localhost");
      result.IsDefaultPort.Should().BeFalse();
      result.IsFile.Should().BeFalse();
      result.IsLoopback.Should().BeTrue();
      result.IsUnc.Should().BeFalse();
      result.LocalPath.Should().Be("/path");
      result.PathAndQuery.Should().Be("/path?query");
      result.Port.Should().Be(8080);
      result.Query.Should().Be("?query");
      result.Scheme.Should().Be(Uri.UriSchemeHttps);
      result.UserEscaped.Should().BeFalse();
      result.UserInfo.Should().Be("user:password");

      static void Test(string text)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToType(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToType(string, out Type)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToType_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToType()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      string.Empty.ToType().Should().BeNull();

      Fixture.Create<string>().ToType().Should().BeNull();

      nameof(Object).ToType().Should().BeNull();
      typeof(object).FullName.ToType().Should().BeOfType<Type>().And.Be(typeof(object));
      typeof(object).AssemblyQualifiedName.ToType().Should().BeOfType<Type>().And.Be(typeof(object));

      Assembly.GetExecutingAssembly().DefinedTypes.ForEach(type =>
      {
        nameof(type).ToType().Should().BeNull();
        type.AssemblyQualifiedName.ToType().Should().BeOfType<Type>().And.Be(type);
      });

      static void Test()
      {

      }
    }

    using (new AssertionScope())
    {
      StringExtensions.ToType(null, out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToType(out result).Should().BeFalse();
      result.Should().BeNull();

      Fixture.Create<string>().ToType(out result).Should().BeFalse();
      result.Should().BeNull();

      nameof(Object).ToType(out result).Should().BeFalse();
      result.Should().BeNull();

      typeof(object).FullName.ToType(out result).Should().BeTrue();
      result.Should().Be(typeof(object));

      typeof(object).AssemblyQualifiedName.ToType(out result).Should().BeTrue();
      result.Should().Be(typeof(object));

      Assembly.GetExecutingAssembly().DefinedTypes.ForEach(typeInfo =>
      {
        nameof(typeInfo).ToType(out result).Should().BeFalse();
        result.Should().BeNull();

        typeInfo.AssemblyQualifiedName.ToType(out result).Should().BeTrue();
        result.Should().BeOfType<Type>().And.Be(typeInfo);
      });

      static void Test()
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToDateTime(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToDateTime(string, out DateTime?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDateTime_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToDateTime()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow }.ForEach(date =>
      {
        Test(date, null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Test(date, culture));
      });

      static void Test(DateTime date, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToDateTime(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDateTime(format)).ThrowExactly<FormatException>();

        $" {date.ToUniversalTime().ToString("o", format)} ".ToDateTime(format).Should().Be(date.ToUniversalTime());
      }
    }

    using (new AssertionScope())
    {
      new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow }.ForEach(date =>
      {
        Test(date, null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Test(date, culture));
      });

      static void Test(DateTime date, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToDateTime(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToDateTime(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToDateTime(out result, format).Should().BeFalse();
        result.Should().BeNull();

        $" {date.ToUniversalTime().ToString("o", format)} ".ToDateTime(out result, format).Should().BeTrue();
        result.Should().Be(date.ToUniversalTime());
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToDateTimeOffset(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToDateTimeOffset(string, out DateTimeOffset?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToDateTimeOffset()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
      {
        Test(date, null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Test(date, culture));
      });

      static void Test(DateTimeOffset date, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToDateTimeOffset(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDateTimeOffset(format)).ThrowExactly<FormatException>();

        $" {date.ToString("o", format)} ".ToDateTimeOffset(format).Should().Be(date);
      }
    }

    using (new AssertionScope())
    {
      new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow }.ForEach(date =>
      {
        Test(date, null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Test(date, culture));
      });

      static void Test(DateTimeOffset date, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToDateTimeOffset(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToDateTimeOffset(out result, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToDateTimeOffset(out result, format).Should().BeFalse();
        result.Should().BeNull();

        $" {date.ToString("o", format)} ".ToDateTimeOffset(out result, format).Should().BeTrue();
        result.Should().Be(date);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToDateOnly(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToDateOnly(string, out DateOnly?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDateOnly_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToDateOnly()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      new[] { DateOnly.MinValue, DateOnly.MaxValue, DateTime.Now.ToDateOnly(), DateTime.UtcNow.ToDateOnly() }.ForEach(date =>
      {
        Test(date, null);
        Test(date, CultureInfo.InvariantCulture);
      });

      static void Test(DateOnly date, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToDateOnly(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDateOnly(format)).ThrowExactly<FormatException>();

        $" {date.ToString("D", format)} ".ToDateOnly(format).Should().Be(date);
      }
    }

    using (new AssertionScope())
    {
      new[] { DateOnly.MinValue, DateOnly.MaxValue, DateTime.Now.ToDateOnly(), DateTime.UtcNow.ToDateOnly() }.ForEach(date =>
      {
        Test(date, null);
        Test(date, CultureInfo.InvariantCulture);
      });

      static void Test(DateOnly date, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToDateOnly(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToDateOnly(out _, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToDateOnly(out _, format).Should().BeFalse();
        result.Should().BeNull();

        $" {date.ToString("D", format)} ".ToDateOnly(out result, format).Should().BeTrue();
        result.Should().Be(date);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToTimeOnly(string, IFormatProvider)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToTimeOnly(string, out TimeOnly?, IFormatProvider)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToTimeOnly()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      new[] { TimeOnly.MinValue, TimeOnly.MaxValue, DateTime.Now.ToTimeOnly(), DateTime.UtcNow.ToTimeOnly() }.ForEach(time =>
      {
        Test(time, null);
        Test(time, CultureInfo.InvariantCulture);
      });

      static void Test(TimeOnly time, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToTimeOnly(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToTimeOnly(format)).ThrowExactly<FormatException>();

        $" {time.ToString("T", format)} ".ToTimeOnly(format).Should().Be(time.StartOfSecond);
      }
    }

    using (new AssertionScope())
    {
      new[] { TimeOnly.MinValue, TimeOnly.MaxValue, DateTime.Now.ToTimeOnly(), DateTime.UtcNow.ToTimeOnly() }.ForEach(date =>
      {
        Test(date, null);
        Test(date, CultureInfo.InvariantCulture);
      });

      static void Test(TimeOnly time, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToTimeOnly(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToTimeOnly(out _, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToTimeOnly(out _, format).Should().BeFalse();
        result.Should().BeNull();

        $" {time.ToString("T", format)} ".ToTimeOnly(out result, format).Should().BeTrue();
        result.Should().Be(time.StartOfSecond);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToFile(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToFile(string, out FileInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToFile()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(string.Empty.ToFile).ThrowExactly<ArgumentException>();

      var name = Path.GetTempFileName();

      name.ToFile().TryFinallyDelete(file =>
      {
        file.Exists.Should().BeTrue();
        file.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        file.FullName.Should().Be(name);
      });

      name = Fixture.Create<string>();
      name.ToFile().TryFinallyDelete(file =>
      {
        file.Exists.Should().BeTrue();
        file.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        file.FullName.Should().Be(Path.Combine(System.IO.Directory.GetCurrentDirectory(), name));
      });

      static void Test(string text)
      {

      }
    }

    using (new AssertionScope())
    {
      var name = Path.GetTempFileName();
      name.ToFile(out var file).Should().BeTrue();
      file.TryFinallyDelete(info =>
      {
        info.Exists.Should().BeTrue();
        info.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        info.FullName.Should().Be(name);
      });

      name = Fixture.Create<string>();
      name.ToFile(out file).Should().BeFalse();
      file.TryFinallyDelete(info =>
      {
        info.Exists.Should().BeTrue();
        info.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        info.FullName.Should().Be(Path.Combine(System.IO.Directory.GetCurrentDirectory(), name));
      });

      static void Test(string text)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToDirectory(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToDirectory(string, out DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDirectory_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToDirectory()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(string.Empty.ToDirectory).ThrowExactly<ArgumentException>();

      var name = Environment.SystemDirectory;

      var directory = name.ToDirectory();
      directory.Exists.Should().BeTrue();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(name);

      name = Fixture.Create<string>();
      directory = name.ToDirectory();
      directory.Exists.Should().BeFalse();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(Path.Combine(System.IO.Directory.GetCurrentDirectory(), name));

      static void Test(string text)
      {

      }
    }

    using (new AssertionScope())
    {
      var name = Environment.SystemDirectory;
      name.ToDirectory(out var directory).Should().BeTrue();
      directory.Exists.Should().BeTrue();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(name);

      name = Fixture.Create<string>();
      name.ToDirectory(out directory).Should().BeFalse();
      directory.Exists.Should().BeFalse();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(Path.Combine(System.IO.Directory.GetCurrentDirectory(), name));

      static void Test(string text)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToPath(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToPath_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToPath()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, string text)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToIpAddress(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToIpAddress(string, out IPAddress)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToIpAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToIpAddress()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(string.Empty.ToIpAddress).ThrowExactly<FormatException>();
      AssertionExtensions.Should("localhost".ToIpAddress).ThrowExactly<FormatException>();

      new[] { IPAddress.None, IPAddress.Any, IPAddress.Loopback, IPAddress.Broadcast, IPAddress.IPv6None, IPAddress.IPv6Any, IPAddress.IPv6Loopback }.ForEach(ip =>
      {
        ip.ToString().ToIpAddress().Should().BeOfType<IPAddress>().And.Be(ip);
      });

      static void Test(string text)
      {
      }
    }

    using (new AssertionScope())
    {
      StringExtensions.ToIpAddress(null, out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToIpAddress(out result).Should().BeFalse();
      result.Should().BeNull();

      "localhost".ToIpAddress(out result).Should().BeFalse();
      result.Should().BeNull();

      new[] { IPAddress.None, IPAddress.Any, IPAddress.Loopback, IPAddress.Broadcast, IPAddress.IPv6None, IPAddress.IPv6Any, IPAddress.IPv6Loopback }.ForEach(ip =>
      {
        ip.ToString().ToIpAddress(out result).Should().BeTrue();
        result.Should().BeOfType<IPAddress>().And.Be(ip);
      });

      static void Test(string text)
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToIpHost(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToIpHost_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToIpHost()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToRegex(string, RegexOptions)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToRegex_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToRegex()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test("[a-z]*");
    }

    return;

    static void Test(string text, RegexOptions options = RegexOptions.None)
    {
      var regex = text.ToRegex(options);

      regex.Should().BeOfType<Regex>();
      regex.ToString().Should().Be("[a-z]*");
      regex.Options.Should().Be(options);
      regex.MatchTimeout.Should().Be(Timeout.InfiniteTimeSpan);
      regex.RightToLeft.Should().Be((options & RegexOptions.RightToLeft) == RegexOptions.RightToLeft);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToStringBuilder(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStringBuilder_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToStringBuilder()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test(Fixture.Create<string>());
    }

    return;

    static void Test(string text)
    {
      var builder = text.ToStringBuilder();

      builder.Should().BeOfType<StringBuilder>();
      builder.ToString().Should().Be(text);
      builder.Capacity.Should().BePositive();
      builder.MaxCapacity.Should().Be(int.MaxValue);
      builder.Length.Should().Be(text.Length);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToStringReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStringReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToStringReader()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test(Fixture.Create<string>());
    }

    return;

    static void Test(string text)
    {
      using var reader = text.ToStringReader();
      reader.Should().BeOfType<StringReader>();
      reader.ReadToEnd().Should().Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToStringContent(string, Encoding, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStringContent_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToStringContent()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Encoding.GetEncodings().ForEach(encoding =>
      {
        Test(string.Empty);
        Test(Fixture.Create<string>(), encoding.GetEncoding(), "application/json");
      });
    }

    return;

    static void Test(string text, Encoding encoding = null, string contentType = null)
    {
      using var content = text.ToStringContent(encoding, contentType);

      content.Should().BeOfType<StringContent>();
      content.Headers.ContentType?.ToString().Should().Contain(contentType ?? "text/plain");
      content.ReadAsStringAsync().Await().Should().Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXmlReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXmlDictionaryReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXmlDocument(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      /*new XmlDocument().Text().Should().BeEmpty();

      var document = new XmlDocument();
      var element = document.CreateElement("article");
      element.SetAttribute("id", "1");
      element.InnerText = "Text";
      document.AppendChild(element);
      document.Text().Should().Be("<?xml version=\"1.0\" encoding=\"utf-16\"?><article id=\"1\">Text</article>");*/
    }

    throw new NotImplementedException();

    return;

    static void Test(string text)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXDocument(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(string text)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXDocumentAsync(string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => string.Empty.ToXDocumentAsync(CancellationToken.None)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string text)
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToProcess(string, ProcessStartInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToProcess_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).ToProcess()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty);
      Test(Shell);
    }

    return;

    static void Test(string text, ProcessStartInfo info = null)
    {
      using var process = text.ToProcess(info);

      AssertionExtensions.Should(() => process.Id).ThrowExactly<InvalidOperationException>();

      process.StartInfo.FileName.Should().Be(text);
      process.StartInfo.ArgumentList.Should().BeEmpty();
      process.StartInfo.Arguments.Should().BeEmpty();
      process.StartInfo.CreateNoWindow.Should().BeFalse();
      //process.StartInfo.Domain.Should().BeEmpty();
      process.StartInfo.Environment.Should().NotBeNullOrEmpty().And.HaveCount(Environment.GetEnvironmentVariables().Count);
      process.StartInfo.ErrorDialog.Should().BeFalse();
      process.StartInfo.ErrorDialogParentHandle.Should().Be(0);
      //process.StartInfo.LoadUserProfile.Should().BeFalse();
      //process.StartInfo.Password.Should().BeNull();
      //process.StartInfo.PasswordInClearText.Should().BeNull();
      process.StartInfo.RedirectStandardError.Should().BeFalse();
      process.StartInfo.RedirectStandardInput.Should().BeFalse();
      process.StartInfo.RedirectStandardOutput.Should().BeFalse();
      process.StartInfo.StandardErrorEncoding.Should().BeNull();
      process.StartInfo.StandardInputEncoding.Should().BeNull();
      process.StartInfo.StandardOutputEncoding.Should().BeNull();
      process.StartInfo.UserName.Should().BeEmpty();
      process.StartInfo.UseShellExecute.Should().BeFalse();
      process.StartInfo.Verb.Should().BeEmpty();
      process.StartInfo.WindowStyle.Should().Be(ProcessWindowStyle.Normal);
      process.StartInfo.WorkingDirectory.Should().BeEmpty();
    }
  }
}