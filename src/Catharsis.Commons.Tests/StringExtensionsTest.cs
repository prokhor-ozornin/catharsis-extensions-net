using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StringExtensions"/>.</para>
/// </summary>
public sealed class StringExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsEmpty(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsEmpty_Method()
  {
    StringExtensions.IsEmpty(null).Should().BeTrue();
    string.Empty.IsEmpty().Should().BeTrue();
    " \t\r\n ".IsEmpty().Should().BeTrue();
    " * ".IsEmpty().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsBoolean(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsBoolean_Method()
  {
    StringExtensions.IsBoolean(null).Should().BeFalse();

    string.Empty.IsBoolean().Should().BeFalse();

    bool.FalseString.IsBoolean().Should().BeTrue();
    bool.TrueString.IsBoolean().Should().BeTrue();

    "invalid".IsBoolean().Should().BeFalse();

    "TRUE".IsBoolean().Should().BeTrue();
    "TruE".IsBoolean().Should().BeTrue();
    "true".IsBoolean().Should().BeTrue();
    " true ".IsBoolean().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsSbyte(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsSbyte_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsSbyte(null, culture).Should().BeFalse();

      string.Empty.IsSbyte(culture).Should().BeFalse();

      "invalid".IsSbyte(culture).Should().BeFalse();

      sbyte.MinValue.ToString(culture).IsSbyte(culture).Should().BeTrue();
      sbyte.MaxValue.ToString(culture).IsSbyte(culture).Should().BeTrue();

      $" {sbyte.MinValue.ToString(culture)} ".IsSbyte(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsByte(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsByte_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsByte(null, culture).Should().BeFalse();

      string.Empty.IsByte(culture).Should().BeFalse();

      "invalid".IsByte(culture).Should().BeFalse();

      byte.MinValue.ToString(culture).IsByte(culture).Should().BeTrue();
      byte.MaxValue.ToString(culture).IsByte(culture).Should().BeTrue();

      $" {byte.MinValue.ToString(culture)} ".IsByte(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsShort(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsShort_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsShort(null, culture).Should().BeFalse();

      string.Empty.IsShort(culture).Should().BeFalse();

      "invalid".IsShort(culture).Should().BeFalse();

      short.MinValue.ToString(culture).IsShort(culture).Should().BeTrue();
      short.MaxValue.ToString(culture).IsShort(culture).Should().BeTrue();

      $" {short.MinValue.ToString(culture)} ".IsShort(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUshort(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsUshort_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsUshort(null, culture).Should().BeFalse();

      string.Empty.IsUshort(culture).Should().BeFalse();

      "invalid".IsUshort(culture).Should().BeFalse();

      ushort.MinValue.ToString(culture).IsUshort(culture).Should().BeTrue();
      ushort.MaxValue.ToString(culture).IsUshort(culture).Should().BeTrue();

      $" {ushort.MinValue.ToString(culture)} ".IsUshort(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsInt(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsInt_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsInt(null, culture).Should().BeFalse();

      string.Empty.IsInt(culture).Should().BeFalse();

      "invalid".IsInt(culture).Should().BeFalse();

      int.MinValue.ToString(culture).IsInt(culture).Should().BeTrue();
      int.MaxValue.ToString(culture).IsInt(culture).Should().BeTrue();

      $" {int.MinValue.ToString(culture)} ".IsInt(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUint(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsUint_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsUint(null, culture).Should().BeFalse();

      string.Empty.IsUint(culture).Should().BeFalse();

      "invalid".IsUint(culture).Should().BeFalse();

      uint.MinValue.ToString(culture).IsUint(culture).Should().BeTrue();
      uint.MaxValue.ToString(culture).IsUint(culture).Should().BeTrue();

      $" {uint.MinValue.ToString(culture)} ".IsUint(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsLong(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsLong_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsLong(null, culture).Should().BeFalse();

      string.Empty.IsLong(culture).Should().BeFalse();

      "invalid".IsLong(culture).Should().BeFalse();

      long.MinValue.ToString(culture).IsLong(culture).Should().BeTrue();
      long.MaxValue.ToString(culture).IsLong(culture).Should().BeTrue();

      $" {long.MaxValue.ToString(culture)} ".IsLong(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUlong(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsUlong_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsUlong(null, culture).Should().BeFalse();

      string.Empty.IsUlong(culture).Should().BeFalse();

      "invalid".IsUlong(culture).Should().BeFalse();

      ulong.MinValue.ToString(culture).IsUlong(culture).Should().BeTrue();
      ulong.MaxValue.ToString(culture).IsUlong(culture).Should().BeTrue();

      $" {ulong.MaxValue.ToString(culture)} ".IsUlong(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsFloat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsFloat_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsFloat(null, culture).Should().BeFalse();

      string.Empty.IsFloat(culture).Should().BeFalse();

      "invalid".IsFloat(culture).Should().BeFalse();

      float.MinValue.ToString(culture).IsFloat(culture).Should().BeTrue();
      float.MaxValue.ToString(culture).IsFloat(culture).Should().BeTrue();

      $" {float.MinValue.ToString(culture)} ".IsFloat(culture).Should().BeTrue();

      float.NaN.ToString(culture).IsFloat(culture).Should().BeTrue();
      float.Epsilon.ToString(culture).IsFloat(culture).Should().BeTrue();
      float.NegativeInfinity.ToString(culture).IsFloat(culture).Should().BeTrue();
      float.PositiveInfinity.ToString(culture).IsFloat(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDouble(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsDouble_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsDouble(null, culture).Should().BeFalse();

      string.Empty.IsDouble(culture).Should().BeFalse();

      "invalid".IsDouble(culture).Should().BeFalse();

      double.MinValue.ToString(culture).IsDouble(culture).Should().BeTrue();
      double.MaxValue.ToString(culture).IsDouble(culture).Should().BeTrue();

      $" {double.MinValue.ToString(culture)} ".IsDouble(culture).Should().BeTrue();

      double.NaN.ToString(culture).IsDouble(culture).Should().BeTrue();
      double.Epsilon.ToString(culture).IsDouble(culture).Should().BeTrue();
      double.NegativeInfinity.ToString(culture).IsDouble(culture).Should().BeTrue();
      double.PositiveInfinity.ToString(culture).IsDouble(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDecimal(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsDecimal_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      StringExtensions.IsDecimal(null, culture).Should().BeFalse();

      string.Empty.IsDecimal(culture).Should().BeFalse();

      "invalid".IsDecimal(culture).Should().BeFalse();

      decimal.MinValue.ToString(culture).IsDecimal(culture).Should().BeTrue();
      decimal.MaxValue.ToString(culture).IsDecimal(culture).Should().BeTrue();

      $" {decimal.MinValue.ToString(culture)} ".IsDecimal(culture).Should().BeTrue();

      decimal.MinusOne.ToString(culture).IsDecimal(culture).Should().BeTrue();
      decimal.Zero.ToString(culture).IsDecimal(culture).Should().BeTrue();
      decimal.One.ToString(culture).IsDecimal(culture).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsEnum{T}(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsEnum_Method()
  {
    StringExtensions.IsEnum<DayOfWeek>(null).Should().BeFalse();

    string.Empty.IsEnum<DayOfWeek>().Should().BeFalse();

    "invalid".IsEnum<DayOfWeek>().Should().BeFalse();

    Enum.GetNames<DayOfWeek>().ForEach(day => day.IsEnum<DayOfWeek>().Should().BeTrue());
    Enum.GetNames<DayOfWeek>().ForEach(day => day.ToLower().IsEnum<DayOfWeek>().Should().BeTrue());
    Enum.GetNames<DayOfWeek>().ForEach(day => day.ToUpper().IsEnum<DayOfWeek>().Should().BeTrue());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsGuid(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsGuid_Method()
  {
    StringExtensions.IsGuid(null).Should().BeFalse();

    string.Empty.IsGuid().Should().BeFalse();

    "invalid".IsGuid().Should().BeFalse();

    foreach (var guid in new[] { Guid.Empty, Guid.NewGuid() })
    {
      guid.ToString().IsGuid().Should().BeTrue();
      guid.ToString().ToLowerInvariant().IsGuid().Should().BeTrue();
      guid.ToString().ToUpperInvariant().IsGuid().Should().BeTrue();
      guid.ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty).IsGuid().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUri(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsUri_Method()
  {
    StringExtensions.IsUri(null).Should().BeFalse();

    string.Empty.IsUri().Should().BeTrue();

    "path".IsUri().Should().BeTrue();

    "https:".IsUri().Should().BeTrue();
    "https://".IsUri().Should().BeFalse();
    "https://user:password@localhost:8080/path?query#id".IsUri().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsType(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsType_Method()
  {
    StringExtensions.IsType(null).Should().BeFalse();

    string.Empty.IsType().Should().BeFalse();

    RandomName.IsType().Should().BeFalse();

    nameof(Object).IsType().Should().BeFalse();
    typeof(object).FullName.IsType().Should().BeTrue();
    typeof(object).AssemblyQualifiedName.IsType().Should().BeTrue();

    Assembly.GetExecutingAssembly().DefinedTypes.ForEach(type =>
    {
      nameof(type).IsType().Should().BeFalse();
      type.AssemblyQualifiedName.IsType().Should().BeTrue();
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateTime(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsDateTime_Method()
  {
    static void Validate(IFormatProvider format)
    {
      StringExtensions.IsDateTime(null, format).Should().BeFalse();

      string.Empty.IsDateTime(format).Should().BeFalse();

      "invalid".IsDateTime(format).Should().BeFalse();

      $" {DateTime.MinValue.ToString("o", format)} ".IsDateTime(format).Should().BeTrue();
      $" {DateTime.MaxValue.ToString("o", format)} ".IsDateTime(format).Should().BeTrue();
      $" {DateTime.UtcNow.ToString("o", format)} ".IsDateTime(format).Should().BeTrue();
      $" {DateTime.Now.ToString("o", format)} ".IsDateTime(format).Should().BeTrue();
    }

    using (new AssertionScope())
    {
      Validate(null);
      Validate(CultureInfo.InvariantCulture);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateTimeOffset(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsDateTimeOffset_Method()
  {
    static void Validate(IFormatProvider format)
    {
      StringExtensions.IsDateTimeOffset(null, format).Should().BeFalse();

      string.Empty.IsDateTimeOffset(format).Should().BeFalse();

      "invalid".IsDateTimeOffset(format).Should().BeFalse();

      $" {DateTimeOffset.MinValue.ToString("o", format)} ".IsDateTimeOffset(format).Should().BeTrue();
      $" {DateTimeOffset.MaxValue.ToString("o", format)} ".IsDateTimeOffset(format).Should().BeTrue();
      $" {DateTimeOffset.UtcNow.ToString("o", format)} ".IsDateTimeOffset(format).Should().BeTrue();
      $" {DateTimeOffset.Now.ToString("o", format)} ".IsDateTimeOffset(format).Should().BeTrue();
    }

    using (new AssertionScope())
    {
      Validate(null);
      Validate(CultureInfo.InvariantCulture);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateOnly(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsDateOnly_Method()
  {
    static void Validate(IFormatProvider culture)
    {
      StringExtensions.IsDateOnly(null, culture).Should().BeFalse();

      string.Empty.IsDateOnly(culture).Should().BeFalse();

      "invalid".IsDateOnly(culture).Should().BeFalse();

      DateOnly.MinValue.ToString("D", culture).IsDateOnly(culture).Should().BeTrue();
      DateOnly.MaxValue.ToString("D", culture).IsDateOnly(culture).Should().BeTrue();
      DateOnly.FromDateTime(DateTime.UtcNow).ToString("D", culture).IsDateOnly(culture).Should().BeTrue();
      DateOnly.FromDateTime(DateTime.Now).ToString("D", culture).IsDateOnly(culture).Should().BeTrue();
    }

    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsTimeOnly(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsTimeOnly_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.IsTimeOnly(null)).ThrowExactly<ArgumentNullException>();

    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      string.Empty.IsTimeOnly(culture).Should().BeFalse();

      "invalid".IsTimeOnly(culture).Should().BeFalse();

      TimeOnly.MinValue.ToLongTimeString().IsTimeOnly().Should().BeTrue();
      TimeOnly.MaxValue.ToLongTimeString().IsTimeOnly().Should().BeTrue();
      TimeOnly.FromDateTime(DateTime.UtcNow).ToLongTimeString().IsTimeOnly().Should().BeTrue();
      TimeOnly.FromDateTime(DateTime.Now).ToLongTimeString().IsTimeOnly().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsFile(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsFile_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.IsFile(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(string.Empty.IsFile).ThrowExactly<ArgumentException>();

    Guid.Empty.ToString().IsFile().Should().BeFalse();

    Environment.SystemDirectory.IsFile().Should().BeFalse();

    RandomEmptyFile.TryFinallyDelete(file =>
    {
      file.FullName.IsFile().Should().BeTrue();
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDirectory(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsDirectory_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.IsDirectory(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(string.Empty.IsDirectory).ThrowExactly<ArgumentException>();

    RandomName.IsDirectory().Should().BeFalse();

    Environment.SystemDirectory.IsDirectory().Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsIpAddress(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_IsIpAddress_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.IsIpAddress(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.IsIpAddress().Should().BeFalse();

    "localhost".IsIpAddress().Should().BeFalse();

    foreach (var ip in new[] { IPAddress.None, IPAddress.Any, IPAddress.Loopback, IPAddress.Broadcast, IPAddress.IPv6None, IPAddress.IPv6Any, IPAddress.IPv6Loopback })
    {
      ip.ToString().IsIpAddress().Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Min(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Min_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Min(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => string.Empty.Min(null)).ThrowExactly<ArgumentNullException>();

    var first = string.Empty;
    var second = string.Empty;
    first.Min(second).Should().BeSameAs(first);

    first = string.Empty;
    second = char.MinValue.ToString();
    first.Min(second).Should().BeSameAs(first);

    first = char.MaxValue.ToString();
    second = char.MinValue.ToString();
    first.Min(second).Should().BeSameAs(first);

    first = char.MaxValue.ToString();
    second = char.MinValue.Repeat(2);
    first.Min(second).Should().BeSameAs(first);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Max(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Max_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Max(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => string.Empty.Max(null)).ThrowExactly<ArgumentNullException>();

    var first = string.Empty;
    var second = string.Empty;
    first.Max(second).Should().BeSameAs(first);

    first = string.Empty;
    second = char.MinValue.ToString();
    first.Max(second).Should().BeSameAs(second);

    first = char.MaxValue.ToString();
    second = char.MinValue.ToString();
    first.Max(second).Should().BeSameAs(first);

    first = char.MaxValue.ToString();
    second = char.MinValue.Repeat(2);
    first.Max(second).Should().BeSameAs(second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Compare(string, string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Compare_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Compare(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => string.Empty.Compare(null)).ThrowExactly<ArgumentNullException>();

    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
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
  public void String_CompareAsNumber_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.CompareAsNumber(null, byte.MinValue.ToString())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => byte.MinValue.ToString().CompareAsNumber(null)).ThrowExactly<ArgumentNullException>();

    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      AssertionExtensions.Should(() => string.Empty.CompareAsNumber(byte.MinValue.ToString(culture), culture)).ThrowExactly<FormatException>();
      AssertionExtensions.Should(() => byte.MinValue.ToString(culture).CompareAsNumber(string.Empty, culture)).ThrowExactly<FormatException>();

      sbyte.MinValue.ToString(culture).CompareAsNumber(sbyte.MinValue.ToString(culture), culture).Should().Be(0);
      sbyte.MaxValue.ToString(culture).CompareAsNumber(sbyte.MaxValue.ToString(culture), culture).Should().Be(0);
      sbyte.MinValue.ToString(culture).CompareAsNumber(sbyte.MaxValue.ToString(culture), culture).Should().BeNegative();

      byte.MinValue.ToString(culture).CompareAsNumber(byte.MinValue.ToString(culture), culture).Should().Be(0);
      byte.MaxValue.ToString(culture).CompareAsNumber(byte.MaxValue.ToString(culture), culture).Should().Be(0);
      byte.MinValue.ToString(culture).CompareAsNumber(byte.MaxValue.ToString(culture), culture).Should().BeNegative();

      short.MinValue.ToString(culture).CompareAsNumber(short.MinValue.ToString(culture), culture).Should().Be(0);
      short.MaxValue.ToString(culture).CompareAsNumber(short.MaxValue.ToString(culture), culture).Should().Be(0);
      short.MinValue.ToString(culture).CompareAsNumber(short.MaxValue.ToString(culture), culture).Should().BeNegative();

      ushort.MinValue.ToString(culture).CompareAsNumber(ushort.MinValue.ToString(culture), culture).Should().Be(0);
      ushort.MaxValue.ToString(culture).CompareAsNumber(ushort.MaxValue.ToString(culture), culture).Should().Be(0);
      ushort.MinValue.ToString(culture).CompareAsNumber(ushort.MaxValue.ToString(culture), culture).Should().BeNegative();

      int.MinValue.ToString(culture).CompareAsNumber(int.MinValue.ToString(culture), culture).Should().Be(0);
      int.MaxValue.ToString(culture).CompareAsNumber(int.MaxValue.ToString(culture), culture).Should().Be(0);
      int.MinValue.ToString(culture).CompareAsNumber(int.MaxValue.ToString(culture), culture).Should().BeNegative();

      uint.MinValue.ToString(culture).CompareAsNumber(uint.MinValue.ToString(culture), culture).Should().Be(0);
      uint.MaxValue.ToString(culture).CompareAsNumber(uint.MaxValue.ToString(culture), culture).Should().Be(0);
      uint.MinValue.ToString(culture).CompareAsNumber(uint.MaxValue.ToString(culture), culture).Should().BeNegative();

      long.MinValue.ToString(culture).CompareAsNumber(long.MinValue.ToString(culture), culture).Should().Be(0);
      long.MaxValue.ToString(culture).CompareAsNumber(long.MaxValue.ToString(culture), culture).Should().Be(0);
      long.MinValue.ToString(culture).CompareAsNumber(long.MaxValue.ToString(culture), culture).Should().BeNegative();

      ulong.MinValue.ToString(culture).CompareAsNumber(ulong.MinValue.ToString(culture), culture).Should().Be(0);
      ulong.MaxValue.ToString(culture).CompareAsNumber(ulong.MaxValue.ToString(culture), culture).Should().Be(0);
      ulong.MinValue.ToString(culture).CompareAsNumber(ulong.MaxValue.ToString(culture), culture).Should().BeNegative();

      float.MinValue.ToString(culture).CompareAsNumber(float.MinValue.ToString(culture), culture).Should().Be(0);
      float.MaxValue.ToString(culture).CompareAsNumber(float.MaxValue.ToString(culture), culture).Should().Be(0);
      float.MinValue.ToString(culture).CompareAsNumber(float.MaxValue.ToString(culture), culture).Should().BeNegative();
      float.NaN.ToString(culture).CompareAsNumber(float.NaN.ToString(culture), culture).Should().Be(0);
      float.Epsilon.ToString(culture).CompareAsNumber(float.Epsilon.ToString(culture), culture).Should().Be(0);
      float.NegativeInfinity.ToString(culture).CompareAsNumber(float.NegativeInfinity.ToString(culture), culture).Should().Be(0);
      float.PositiveInfinity.ToString(culture).CompareAsNumber(float.PositiveInfinity.ToString(culture), culture).Should().Be(0);

      double.MinValue.ToString(culture).CompareAsNumber(double.MinValue.ToString(culture), culture).Should().Be(0);
      double.MaxValue.ToString(culture).CompareAsNumber(double.MaxValue.ToString(culture), culture).Should().Be(0);
      double.MinValue.ToString(culture).CompareAsNumber(double.MaxValue.ToString(culture), culture).Should().BeNegative();
      double.NaN.ToString(culture).CompareAsNumber(double.NaN.ToString(culture), culture).Should().Be(0);
      double.Epsilon.ToString(culture).CompareAsNumber(double.Epsilon.ToString(culture), culture).Should().Be(0);
      double.NegativeInfinity.ToString(culture).CompareAsNumber(double.NegativeInfinity.ToString(culture), culture).Should().Be(0);
      double.PositiveInfinity.ToString(culture).CompareAsNumber(double.PositiveInfinity.ToString(culture), culture).Should().Be(0);

      decimal.MinValue.ToString(culture).CompareAsNumber(decimal.MinValue.ToString(culture), culture).Should().Be(0);
      decimal.MaxValue.ToString(culture).CompareAsNumber(decimal.MaxValue.ToString(culture), culture).Should().Be(0);
      decimal.MinValue.ToString(culture).CompareAsNumber(decimal.MaxValue.ToString(culture), culture).Should().BeNegative();
      decimal.MinusOne.ToString(culture).CompareAsNumber(decimal.MinusOne.ToString(culture), culture).Should().Be(0);
      decimal.Zero.ToString(culture).CompareAsNumber(decimal.Zero.ToString(culture), culture).Should().Be(0);
      decimal.One.ToString(culture).CompareAsNumber(decimal.One.ToString(culture), culture).Should().Be(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.CompareAsDate(string, string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_CompareAsDate_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.CompareAsDate(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => string.Empty.CompareAsDate(null)).ThrowExactly<FormatException>();

    foreach (var date in new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.CompareAsDate(date.ToString(culture), culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => date.ToString(culture).CompareAsDate(string.Empty, culture)).ThrowExactly<FormatException>();

        date.ToString("o", culture).CompareAsDate(date.ToString("o", culture), culture).Should().Be(0);
        date.AddMilliseconds(1).ToString("o", culture).CompareAsDate(date.ToString("o", culture), culture).Should().BePositive();
        date.AddMilliseconds(-1).ToString("o", culture).CompareAsDate(date.ToString("o", culture), culture).Should().BeNegative();

        date.ToString("D").CompareAsDate(date.ToString("D")).Should().Be(0);
        date.TruncateToDayStart().AddMilliseconds(1).ToString("D").CompareAsDate(date.ToString("D")).Should().Be(0);
        date.TruncateToDayStart().AddSeconds(1).ToString("D").CompareAsDate(date.ToString("D")).Should().Be(0);
        date.TruncateToDayStart().AddMinutes(1).ToString("D").CompareAsDate(date.ToString("D")).Should().Be(0);
        date.TruncateToDayStart().AddHours(1).ToString("D").CompareAsDate(date.ToString("D")).Should().Be(0);
        date.TruncateToDayStart().AddDays(1).ToString("D").CompareAsDate(date.ToString("D")).Should().BePositive();
        date.TruncateToDayStart().AddMonths(1).ToString("D").CompareAsDate(date.ToString("D")).Should().BePositive();
        date.TruncateToDayStart().AddYears(1).ToString("D").CompareAsDate(date.ToString("D")).Should().BePositive();

        date.ToString("T", culture).CompareAsDate(date.ToString("T", culture), culture).Should().Be(0);
        date.AddMilliseconds(1).ToString("T", culture).CompareAsDate(date.ToString("T", culture), culture).Should().Be(0);
        date.AddSeconds(1).ToString("T", culture).CompareAsDate(date.ToString("T", culture), culture).Should().BePositive();
        date.AddMinutes(1).ToString("T", culture).CompareAsDate(date.ToString("T", culture), culture).Should().BePositive();
        date.AddHours(1).ToString("T", culture).CompareAsDate(date.ToString("T", culture), culture).Should().BePositive();
        date.AddDays(1).ToString("T", culture).CompareAsDate(date.ToString("T", culture), culture).Should().Be(0);
        date.AddMonths(1).ToString("T", culture).CompareAsDate(date.ToString("T", culture), culture).Should().Be(0);
        date.AddYears(1).ToString("T", culture).CompareAsDate(date.ToString("T", culture), culture).Should().Be(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Append(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Append_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Append(null, null)).ThrowExactly<ArgumentNullException>();

    string.Empty.Append(null).Should().BeEmpty();
    string.Empty.Append(string.Empty).Should().BeEmpty();
    "\r\n".Append("\t").Should().BeNullOrWhiteSpace();
    "value".Append(null).Should().Be("value");
    "first".Append(" & ").Append("second").Should().Be("first & second");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Prepend(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Prepend_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Prepend(null, null)).ThrowExactly<ArgumentNullException>();

    string.Empty.Prepend(null).Should().BeEmpty();
    string.Empty.Prepend(string.Empty).Should().BeEmpty();
    "\r\n".Prepend("\t").Should().BeNullOrWhiteSpace();
    "value".Prepend(null).Should().Be("value");
    "second".Prepend(" & ").Prepend("first").Should().Be("first & second");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.RemoveRange(string, int, int?, Predicate{char})"/> method.</para>
  /// </summary>
  [Fact]
  public void String_RemoveRange_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.RemoveRange(null, 0)).ThrowExactly<ArgumentNullException>();

    string.Empty.RemoveRange(-10).Should().BeEmpty();
    string.Empty.RemoveRange(0).Should().BeEmpty();
    string.Empty.RemoveRange(10).Should().BeEmpty();

    const string value = "0123456789";
    value.RemoveRange(-1).Should().Be(value);
    value.RemoveRange(0).Should().Be(value);
    value.RemoveRange(1).Should().Be(value.TakeLast(value.Length - 1).ToArray().ToText());
    value.RemoveRange(value.Length).Should().BeEmpty();
    value.RemoveRange(value.Length + 1).Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Reverse(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Reverse_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Reverse(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.Reverse().Should().NotBeNull().And.BeSameAs(string.Empty.Reverse()).And.BeEmpty();

    var text = RandomString;
    var reversed = text.Reverse();
    reversed.Should().NotBeNull().And.NotBeSameAs(text.Reverse()).And.Be(reversed.ToCharArray().ToText());
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.Replace(string, IEnumerable{(string Name, object Value)})"/></description></item>
  ///     <item><description><see cref="StringExtensions.Replace(string, (string Name, object? Value)[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void String_Replace_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.Replace(null, Array.Empty<(string, object)>()!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => string.Empty.Replace(null)).ThrowExactly<ArgumentNullException>();

      string.Empty.Replace().Should().BeEmpty();
      string.Empty.Replace(("quick", "slow")).Should().BeEmpty();

      const string value = "The quick brown fox jumped over the lazy dog";

      value.Replace(("quick", "slow"), ("dog", "bear"), ("brown", "hazy"), ("UNSPECIFIED", string.Empty)).Should().Be("The slow hazy fox jumped over the lazy bear");
    }

    using (new AssertionScope())
    {
      /*AssertionExtensions.Should(() => StringExtensions.Replace(null, new object())).ThrowExactly<ArgumentNullException>();

      string.Empty.Replace(new object()).Should().BeEmpty();
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
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.SwapCase(string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_SwapCase_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.SwapCase(null)).ThrowExactly<ArgumentNullException>();

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

    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      value.SwapCase(culture).Should().BeUpperCased();
      value.SwapCase(culture).SwapCase(culture).Should().BeLowerCased();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Capitalize(string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Capitalize_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Capitalize(null)).ThrowExactly<ArgumentNullException>();

    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      string.Empty.Capitalize(culture).Should().BeEmpty();

      "Word & Deed".Capitalize(culture).Should().Be("Word & Deed");
      "word & deed".Capitalize(culture).Should().Be("Word & deed");
      "wORD & deed".Capitalize(culture).Should().Be("WORD & deed");

      "Слово & Дело".Capitalize(culture).Should().Be("Слово & Дело");
      "слово & дело".Capitalize(culture).Should().Be("Слово & дело");
      "сЛОВО & дело".Capitalize(culture).Should().Be("СЛОВО & дело");
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.CapitalizeAll(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_CapitalizeAll_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.CapitalizeAll(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.CapitalizeAll().Should().BeEmpty();

    "Word & Deed".CapitalizeAll().Should().Be("Word & Deed");
    "word & deed".CapitalizeAll().Should().Be("Word & Deed");
    "wORD & deed".CapitalizeAll().Should().Be("Word & Deed");

    "Слово & Дело".CapitalizeAll().Should().Be("Слово & Дело");
    "слово & дело".CapitalizeAll().Should().Be("Слово & Дело");
    "сЛОВО & дело".CapitalizeAll().Should().Be("Слово & Дело");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Repeat(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Repeat_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Repeat(null, 0)).ThrowExactly<ArgumentNullException>();

    string.Empty.Repeat(int.MinValue).Should().NotBeNull().And.BeSameAs(string.Empty.Repeat(int.MinValue)).And.BeEmpty();
    string.Empty.Repeat(0).Should().NotBeNull().And.BeSameAs(string.Empty.Repeat(0)).And.BeEmpty();
    string.Empty.Repeat(1).Should().NotBeNull().And.BeSameAs(string.Empty.Repeat(1)).And.BeEmpty();

    const int count = 1000;

    var repeated = char.MinValue.ToString().Repeat(count);
    repeated.Should().NotBeNull().And.NotBeSameAs(char.MinValue.ToString().Repeat(count)).And.HaveLength(count);
    repeated.ToCharArray().Should().AllBeEquivalentTo(char.MinValue);

    repeated = char.MaxValue.ToString().Repeat(count);
    repeated.Should().NotBeNull().And.NotBeSameAs(char.MinValue.ToString().Repeat(count)).And.HaveLength(count);
    repeated.ToCharArray().Should().AllBeEquivalentTo(char.MinValue);


    "*".Repeat(-1).Should().BeEmpty();
    "*".Repeat(0).Should().BeEmpty();
    "*".Repeat(1).Should().Be("*");
    "xyz".Repeat(2).Should().Be("xyzxyz");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Lines(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Lines_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Lines(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.Lines().Should().NotBeNull().And.BeSameAs(string.Empty.Lines()).And.BeEmpty();
    string.Empty.Lines("\t").Should().NotBeNull().And.BeSameAs(string.Empty.Lines("\t")).And.BeEmpty();

    var text = RandomString;
    text.Lines().Should().NotBeNull().And.NotBeSameAs(text.Lines()).And.HaveCount(1).And.HaveElementAt(0, text);

    var strings = 10.Objects(() => RandomString).AsArray();
    text = strings.Join(Environment.NewLine);
    var lines = text.Lines();
    lines.Should().NotBeNull().And.NotBeSameAs(text.Lines()).And.HaveCount(strings.Length).And.Equal(strings);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.FromBase64(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Base64_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.FromBase64(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.FromBase64().Should().NotBeNull().And.BeSameAs(string.Empty.FromBase64()).And.BeEmpty();

    var bytes = RandomBytes;
    bytes.ToBase64().Should().NotBeNull().And.NotBeSameAs(bytes.ToBase64()).And.Be(System.Convert.ToBase64String(bytes));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.FromHex"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Hex_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.FromHex(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.FromHex().Should().NotBeNull().And.BeSameAs(string.Empty.FromHex()).And.BeEmpty();

    var bytes = RandomBytes;
    bytes.ToHex().Should().NotBeNull().And.NotBeSameAs(bytes.ToHex()).And.Be(System.Convert.ToHexString(bytes));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.UrlEncode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_UrlEncode_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.UrlEncode(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.UrlEncode().Should().BeEmpty();

    const string value = "#value?";
    value.UrlEncode().Should().Be(Uri.EscapeDataString(value)).And.Be("%23value%3F");
    Uri.UnescapeDataString(value.UrlEncode()).Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.UrlDecode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_UrlDecode_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.UrlDecode(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.UrlDecode().Should().BeEmpty();

    const string value = "%23value%3F";
    value.UrlDecode().Should().Be(Uri.UnescapeDataString(value)).And.Be("#value?");
    Uri.EscapeDataString(value.UrlDecode()).Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HtmlEncode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HtmlEncode_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.HtmlEncode(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.HtmlEncode().Should().BeEmpty();

    const string value = "<p>word & deed</p>";

    value.HtmlEncode().Should().Be(WebUtility.HtmlEncode(value)).And.Be("&lt;p&gt;word &amp; deed&lt;/p&gt;");
    WebUtility.HtmlDecode(value.HtmlEncode()).Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HtmlDecode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_HtmlDecode_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.HtmlDecode(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.HtmlDecode().Should().BeEmpty();

    const string value = "&lt;p&gt;word &amp; deed&lt;/p&gt;";

    value.HtmlDecode().Should().Be(WebUtility.HtmlDecode(value)).And.Be("<p>word & deed</p>");
    WebUtility.HtmlEncode(value.HtmlDecode()).Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Indent(string, char, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Indent_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Indent(null, char.MinValue)).ThrowExactly<ArgumentNullException>();

    const int count = 2;

    string.Empty.Indent('*', -1).Should().BeEmpty();
    string.Empty.Indent('*', 0).Should().BeEmpty();
    string.Empty.Indent('*').Should().Be("*");
    string.Empty.Indent('*', count).Should().HaveLength(count).And.Be('*'.Repeat(count));

    "***".Indent(' ', count).Should().HaveLength(count + 3).And.Be(' '.Repeat(count) + "***");
    $" 1.{Environment.NewLine} 2. ".Indent('*', count).Should().HaveLength(count * 2 + 4 + Environment.NewLine.Length).And.Be('*'.Repeat(count) + "1." + Environment.NewLine + '*'.Repeat(count) + "2.");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Unindent(string, char)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Unindent_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Unindent(null, char.MinValue)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Spacify(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Spacify_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Spacify(null)).ThrowExactly<ArgumentNullException>();

    const int count = 2;

    string.Empty.Spacify(-1).Should().BeEmpty();
    string.Empty.Spacify(0).Should().BeEmpty();
    string.Empty.Spacify().Should().Be(" ");
    string.Empty.Spacify(count).Should().HaveLength(count).And.Be(' '.Repeat(count));

    "***".Spacify(count).Should().HaveLength(count + 3).And.Be(' '.Repeat(count) + "***");
    $" 1.{Environment.NewLine} 2. ".Spacify(count).Should().HaveLength(count * 2 + 4 + Environment.NewLine.Length).And.Be(' '.Repeat(count) + "1." + Environment.NewLine + ' '.Repeat(count) + "2.");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Unspacify(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Unspacify_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Unspacify(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Tabify(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Tabify_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Tabify(null)).ThrowExactly<ArgumentNullException>();

    const int count = 2;

    string.Empty.Tabify(-1).Should().BeEmpty();
    string.Empty.Tabify(0).Should().BeEmpty();
    string.Empty.Tabify().Should().Be("\t");
    string.Empty.Tabify(count).Should().HaveLength(count).And.Be('\t'.Repeat(count));

    "***".Tabify(count).Should().HaveLength(count + 3).And.Be('\t'.Repeat(count) + "***");
    $" 1.{Environment.NewLine} 2. ".Tabify(count).Should().HaveLength(count * 2 + 4 + Environment.NewLine.Length).And.Be('\t'.Repeat(count) + "1." + Environment.NewLine + '\t'.Repeat(count) + "2.");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Untabify(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Untabify_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Untabify(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.Execute(string, IEnumerable{string})"/></description></item>
  ///     <item><description><see cref="StringExtensions.Execute(string, string[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void String_Execute_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.Execute(null)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => string.Empty.Execute()).ThrowExactly<InvalidOperationException>();

      var command = "cmd";
      var arguments = new[] { "dir" };

      var process = command.Execute(arguments);

      process.Finish(TimeSpan.FromSeconds(5));

      process.Should().NotBeNull();

      process.Id.Should().BePositive();
      process.HasExited.Should().BeTrue();
      process.ExitCode.Should().Be(-1);
      process.StartTime.Should().BeBefore(DateTime.Now);
      process.ExitTime.Should().BeBefore(DateTime.Now);

      process.StartInfo.FileName.Should().Be(command);
      process.StartInfo.ArgumentList.Should().Equal(arguments);
      process.StartInfo.Arguments.Should().BeEmpty();
      process.StartInfo.CreateNoWindow.Should().BeTrue();
      process.StartInfo.Domain.Should().BeEmpty();
      process.StartInfo.Environment.Should().NotBeNullOrEmpty().And.HaveCount(Environment.GetEnvironmentVariables().Count);
      process.StartInfo.ErrorDialog.Should().BeFalse();
      process.StartInfo.ErrorDialogParentHandle.Should().Be(0);
      process.StartInfo.LoadUserProfile.Should().BeFalse();
      process.StartInfo.Password.Should().BeNull();
      process.StartInfo.PasswordInClearText.Should().BeNull();
      process.StartInfo.RedirectStandardError.Should().BeTrue();
      process.StartInfo.RedirectStandardInput.Should().BeTrue();
      process.StartInfo.RedirectStandardOutput.Should().BeTrue();
      process.StartInfo.StandardErrorEncoding.Should().BeNull();
      process.StartInfo.StandardInputEncoding.Should().BeNull();
      process.StartInfo.StandardOutputEncoding.Should().BeNull();
      process.StartInfo.UserName.Should().BeEmpty();
      process.StartInfo.UseShellExecute.Should().BeFalse();
      process.StartInfo.Verb.Should().BeEmpty();
      process.StartInfo.Verbs.Should().BeEmpty();
      process.StartInfo.WindowStyle.Should().Be(ProcessWindowStyle.Normal);
      process.StartInfo.WorkingDirectory.Should().BeEmpty();
    }

    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToBytes(string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToBytes_Method()
  {
    static void Validate(Encoding encoding)
    {
      var text = RandomString;

      string.Empty.ToBytes(encoding).Should().NotBeNull().And.BeSameAs(string.Empty.ToBytes(encoding)).And.BeEmpty();

      var bytes = text.ToBytes(encoding);
      bytes.Should().NotBeNull().And.NotBeSameAs(text.ToBytes(encoding)).And.HaveCount((encoding ?? Encoding.Default).GetByteCount(text));
      bytes.ToText(encoding).Should().Be(text);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>();

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
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
  public void String_ToBoolean_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToBoolean(null)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(string.Empty.ToBoolean).ThrowExactly<FormatException>();
      AssertionExtensions.Should("invalid".ToBoolean).ThrowExactly<FormatException>();

      bool.FalseString.ToBoolean().Should().BeFalse();
      bool.TrueString.ToBoolean().Should().BeTrue();

      "TRUE".ToBoolean().Should().BeTrue();
      "TruE".ToBoolean().Should().BeTrue();
      "true".ToBoolean().Should().BeTrue();
      " true ".ToBoolean().Should().BeTrue();

      "FALSE".ToBoolean().Should().BeFalse();
      "FalsE".ToBoolean().Should().BeFalse();
      "false".ToBoolean().Should().BeFalse();
      " false ".ToBoolean().Should().BeFalse();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToBoolean(null, out _)).ThrowExactly<ArgumentNullException>();

      string.Empty.ToBoolean(out var result).Should().BeFalse();
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
      result.Should().BeFalse();
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
  public void String_ToSbyte_Methods()
  {
    using (new AssertionScope())
    {
      static void Validate(IFormatProvider format)
      {
        AssertionExtensions.Should(() => string.Empty.ToSbyte(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToSbyte(format)).ThrowExactly<FormatException>();

        sbyte.MinValue.ToString(format).ToSbyte(format).Should().Be(sbyte.MinValue);
        sbyte.MaxValue.ToString(format).ToSbyte(format).Should().Be(sbyte.MaxValue);

        $" {sbyte.MinValue.ToString(format)} ".ToSbyte(format).Should().Be(sbyte.MinValue);
      }

      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToSbyte(null)).ThrowExactly<ArgumentNullException>();

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToSbyte(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToSbyte(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToSbyte(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        sbyte.MinValue.ToString(culture).ToSbyte(out result, culture).Should().BeTrue();
        result.Should().Be(sbyte.MinValue);

        sbyte.MaxValue.ToString(culture).ToSbyte(out result, culture).Should().BeTrue();
        result.Should().Be(sbyte.MaxValue);

        $" {sbyte.MinValue.ToString(culture)} ".ToSbyte(out result, culture).Should().BeTrue();
        result.Should().Be(sbyte.MinValue);
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
  public void String_ToByte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToByte(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToByte(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToByte(culture)).ThrowExactly<FormatException>();

        byte.MinValue.ToString(culture).ToByte(culture).Should().Be(byte.MinValue);
        byte.MaxValue.ToString(culture).ToByte(culture).Should().Be(byte.MaxValue);

        $" {byte.MinValue.ToString(culture)} ".ToByte(culture).Should().Be(byte.MinValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToByte(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToByte(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToByte(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        byte.MinValue.ToString(culture).ToByte(out result, culture).Should().BeTrue();
        result.Should().Be(byte.MinValue);

        byte.MaxValue.ToString(culture).ToByte(out result, culture).Should().BeTrue();
        result.Should().Be(byte.MaxValue);

        $" {byte.MinValue.ToString(culture)} ".ToByte(out result, culture).Should().BeTrue();
        result.Should().Be(byte.MinValue);
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
  public void String_ToShort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToShort(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToShort(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToShort(culture)).ThrowExactly<FormatException>();

        short.MinValue.ToString(culture).ToShort(culture).Should().Be(short.MinValue);
        short.MaxValue.ToString(culture).ToShort(culture).Should().Be(short.MaxValue);

        $" {short.MinValue.ToString(culture)} ".ToShort(culture).Should().Be(short.MinValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToShort(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToShort(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToShort(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        short.MinValue.ToString(culture).ToShort(out result, culture).Should().BeTrue();
        result.Should().Be(short.MinValue);

        short.MaxValue.ToString(culture).ToShort(out result, culture).Should().BeTrue();
        result.Should().Be(short.MaxValue);

        $" {short.MinValue.ToString(culture)} ".ToShort(out result, culture).Should().BeTrue();
        result.Should().Be(short.MinValue);
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
  public void String_ToUshort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUshort(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToUshort(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUshort(culture)).ThrowExactly<FormatException>();

        ushort.MinValue.ToString(culture).ToUshort(culture).Should().Be(ushort.MinValue);
        ushort.MaxValue.ToString(culture).ToUshort(culture).Should().Be(ushort.MaxValue);

        $" {ushort.MinValue.ToString(culture)} ".ToUshort(culture).Should().Be(ushort.MinValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUshort(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToUshort(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToUshort(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        ushort.MinValue.ToString(culture).ToUshort(out result, culture).Should().BeTrue();
        result.Should().Be(ushort.MinValue);

        ushort.MaxValue.ToString(culture).ToUshort(out result, culture).Should().BeTrue();
        result.Should().Be(ushort.MaxValue);

        $" {ushort.MinValue.ToString(culture)} ".ToUshort(out result, culture).Should().BeTrue();
        result.Should().Be(ushort.MinValue);
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
  public void String_ToInt_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToInt(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToInt(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToInt(culture)).ThrowExactly<FormatException>();

        int.MinValue.ToString(culture).ToInt(culture).Should().Be(int.MinValue);
        int.MaxValue.ToString(culture).ToInt(culture).Should().Be(int.MaxValue);

        $" {int.MinValue.ToString(culture)} ".ToInt(culture).Should().Be(int.MinValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToInt(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToInt(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToInt(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        int.MinValue.ToString(culture).ToInt(out result, culture).Should().BeTrue();
        result.Should().Be(int.MinValue);

        int.MaxValue.ToString(culture).ToInt(out result, culture).Should().BeTrue();
        result.Should().Be(int.MaxValue);

        $" {int.MinValue.ToString(culture)} ".ToInt(out result, culture).Should().BeTrue();
        result.Should().Be(int.MinValue);
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
  public void String_ToUint_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUint(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToUint(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUint(culture)).ThrowExactly<FormatException>();

        uint.MinValue.ToString(culture).ToUint(culture).Should().Be(uint.MinValue);
        uint.MaxValue.ToString(culture).ToUint(culture).Should().Be(uint.MaxValue);

        $" {uint.MinValue.ToString(culture)} ".ToUint(culture).Should().Be(uint.MinValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUint(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToUint(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToUint(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        uint.MinValue.ToString(culture).ToUint(out result, culture).Should().BeTrue();
        result.Should().Be(uint.MinValue);

        uint.MaxValue.ToString(culture).ToUint(out result, culture).Should().BeTrue();
        result.Should().Be(uint.MaxValue);

        $" {uint.MinValue.ToString(culture)} ".ToUint(out result, culture).Should().BeTrue();
        result.Should().Be(uint.MinValue);
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
  public void String_ToLong_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToLong(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToLong(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToLong(culture)).ThrowExactly<FormatException>();

        long.MinValue.ToString(culture).ToLong(culture).Should().Be(long.MinValue);
        long.MaxValue.ToString(culture).ToLong(culture).Should().Be(long.MaxValue);

        $" {long.MinValue.ToString(culture)} ".ToLong(culture).Should().Be(long.MinValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToLong(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToLong(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToLong(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        long.MinValue.ToString(culture).ToLong(out result, culture).Should().BeTrue();
        result.Should().Be(long.MinValue);

        long.MaxValue.ToString(culture).ToLong(out result, culture).Should().BeTrue();
        result.Should().Be(long.MaxValue);

        $" {long.MinValue.ToString(culture)} ".ToLong(out result, culture).Should().BeTrue();
        result.Should().Be(long.MinValue);
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
  public void String_ToUlong_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUlong(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToUlong(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUlong(culture)).ThrowExactly<FormatException>();

        ulong.MinValue.ToString(culture).ToUlong(culture).Should().Be(ulong.MinValue);
        ulong.MaxValue.ToString(culture).ToUlong(culture).Should().Be(ulong.MaxValue);

        $" {ulong.MinValue.ToString(culture)} ".ToUlong(culture).Should().Be(ulong.MinValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUlong(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToUlong(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToUlong(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        ulong.MinValue.ToString(culture).ToUlong(out result, culture).Should().BeTrue();
        result.Should().Be(ulong.MinValue);

        ulong.MaxValue.ToString(culture).ToUlong(out result, culture).Should().BeTrue();
        result.Should().Be(ulong.MaxValue);

        $" {ulong.MinValue.ToString(culture)} ".ToUlong(out result, culture).Should().BeTrue();
        result.Should().Be(ulong.MinValue);
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
  public void String_ToFloat_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToFloat(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToFloat(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToFloat(culture)).ThrowExactly<FormatException>();

        float.MinValue.ToString(culture).ToFloat(culture).Should().Be(float.MinValue);
        float.MaxValue.ToString(culture).ToFloat(culture).Should().Be(float.MaxValue);
        $" {float.MinValue.ToString(culture)} ".ToFloat(culture).Should().Be(float.MinValue);

        float.NaN.ToString(culture).ToFloat(culture).Should().Be(float.NaN);
        float.Epsilon.ToString(culture).ToFloat(culture).Should().Be(float.Epsilon);
        float.NegativeInfinity.ToString(culture).ToFloat(culture).Should().Be(float.NegativeInfinity);
        float.PositiveInfinity.ToString(culture).ToFloat(culture).Should().Be(float.PositiveInfinity);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToFloat(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToFloat(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToFloat(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        float.MinValue.ToString(culture).ToFloat(out result, culture).Should().BeTrue();
        result.Should().Be(float.MinValue);

        float.MaxValue.ToString(culture).ToFloat(out result, culture).Should().BeTrue();
        result.Should().Be(float.MaxValue);

        $" {float.MinValue.ToString(culture)} ".ToFloat(out result, culture).Should().BeTrue();
        result.Should().Be(float.MinValue);

        float.NaN.ToString(culture).ToFloat(out result, culture).Should().BeTrue();
        result.Should().Be(float.NaN);

        float.Epsilon.ToString(culture).ToFloat(out result, culture).Should().BeTrue();
        result.Should().Be(float.Epsilon);

        float.NegativeInfinity.ToString(culture).ToFloat(out result, culture).Should().BeTrue();
        result.Should().Be(float.NegativeInfinity);

        float.PositiveInfinity.ToString(culture).ToFloat(out result, culture).Should().BeTrue();
        result.Should().Be(float.PositiveInfinity);
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
  public void String_ToDouble_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDouble(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToDouble(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDouble(culture)).ThrowExactly<FormatException>();

        double.MinValue.ToString(culture).ToDouble(culture).Should().Be(double.MinValue);
        double.MaxValue.ToString(culture).ToDouble(culture).Should().Be(double.MaxValue);
        $" {double.MinValue.ToString(culture)} ".ToDouble(culture).Should().Be(double.MinValue);

        double.NaN.ToString(culture).ToDouble(culture).Should().Be(double.NaN);
        double.Epsilon.ToString(culture).ToDouble(culture).Should().Be(double.Epsilon);
        double.NegativeInfinity.ToString(culture).ToDouble(culture).Should().Be(double.NegativeInfinity);
        double.PositiveInfinity.ToString(culture).ToDouble(culture).Should().Be(double.PositiveInfinity);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDouble(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToDouble(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToDouble(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        double.MinValue.ToString(culture).ToDouble(out result, culture).Should().BeTrue();
        result.Should().Be(double.MinValue);

        double.MaxValue.ToString(culture).ToDouble(out result, culture).Should().BeTrue();
        result.Should().Be(double.MaxValue);

        $" {double.MinValue.ToString(culture)} ".ToDouble(out result, culture).Should().BeTrue();
        result.Should().Be(double.MinValue);

        double.NaN.ToString(culture).ToDouble(out result, culture).Should().BeTrue();
        result.Should().Be(double.NaN);

        double.Epsilon.ToString(culture).ToDouble(out result, culture).Should().BeTrue();
        result.Should().Be(double.Epsilon);

        double.NegativeInfinity.ToString(culture).ToDouble(out result, culture).Should().BeTrue();
        result.Should().Be(double.NegativeInfinity);

        double.PositiveInfinity.ToString(culture).ToDouble(out result, culture).Should().BeTrue();
        result.Should().Be(double.PositiveInfinity);
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
  public void String_ToDecimal_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDecimal(null)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToDecimal(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDecimal(culture)).ThrowExactly<FormatException>();

        decimal.MinValue.ToString(culture).ToDecimal(culture).Should().Be(decimal.MinValue);
        decimal.MaxValue.ToString(culture).ToDecimal(culture).Should().Be(decimal.MaxValue);
        $" {decimal.MinValue.ToString(culture)} ".ToDecimal(culture).Should().Be(decimal.MinValue);

        decimal.MinusOne.ToString(culture).ToDecimal(culture).Should().Be(decimal.MinusOne);
        decimal.Zero.ToString(culture).ToDecimal(culture).Should().Be(decimal.Zero);
        decimal.One.ToString(culture).ToDecimal(culture).Should().Be(decimal.One);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDecimal(null, out _)).ThrowExactly<ArgumentNullException>();

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToDecimal(out var result, culture).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToDecimal(out result, culture).Should().BeFalse();
        result.Should().BeNull();

        decimal.MinValue.ToString(culture).ToDecimal(out result, culture).Should().BeTrue();
        result.Should().Be(decimal.MinValue);

        decimal.MaxValue.ToString(culture).ToDecimal(out result, culture).Should().BeTrue();
        result.Should().Be(decimal.MaxValue);

        $" {decimal.MinValue.ToString(culture)} ".ToDecimal(out result, culture).Should().BeTrue();
        result.Should().Be(decimal.MinValue);

        decimal.MinusOne.ToString(culture).ToDecimal(out result, culture).Should().BeTrue();
        result.Should().Be(decimal.MinusOne);

        decimal.Zero.ToString(culture).ToDecimal(out result, culture).Should().BeTrue();
        result.Should().Be(decimal.Zero);

        decimal.One.ToString(culture).ToDecimal(out result, culture).Should().BeTrue();
        result.Should().Be(decimal.One);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToEnum{T}(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToEnum{T}(string, out T)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void String_ToEnum_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToEnum<Guid>(null)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Guid.NewGuid().ToString().ToEnum<Guid>()).ThrowExactly<ArgumentException>();
      AssertionExtensions.Should(string.Empty.ToEnum<DayOfWeek>).ThrowExactly<ArgumentException>();
      AssertionExtensions.Should("invalid".ToEnum<DayOfWeek>).ThrowExactly<ArgumentException>();

      Enum.GetNames<DayOfWeek>().Select(day => day.ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());
      Enum.GetNames<DayOfWeek>().Select(day => day.ToLower().ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());
      Enum.GetNames<DayOfWeek>().Select(day => day.ToUpper().ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToEnum<Guid>(null, out _)).ThrowExactly<ArgumentNullException>();

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
  public void String_ToGuid_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToGuid(null)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(string.Empty.ToGuid).ThrowExactly<FormatException>();
      AssertionExtensions.Should("invalid".ToGuid).ThrowExactly<FormatException>();

      foreach (var guid in new[] { Guid.Empty, Guid.NewGuid() })
      {
        guid.ToString().ToGuid().Should().Be(guid);
        guid.ToString().ToLowerInvariant().ToGuid().Should().Be(guid);
        guid.ToString().ToUpperInvariant().ToGuid().Should().Be(guid);
        guid.ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty).ToGuid().Should().Be(guid);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToGuid(null, out _)).ThrowExactly<ArgumentNullException>();

      string.Empty.ToGuid(out var result).Should().BeFalse();
      result.Should().BeNull();

      "invalid".ToGuid(out result).Should().BeFalse();
      result.Should().BeNull();

      foreach (var guid in new[] { Guid.Empty, Guid.NewGuid() })
      {
        guid.ToString().ToGuid(out result).Should().BeTrue();
        result.Should().Be(guid);

        guid.ToString().ToLowerInvariant().ToGuid(out result).Should().BeTrue();
        result.Should().Be(guid);

        guid.ToString().ToUpperInvariant().ToGuid(out result).Should().BeTrue();
        result.Should().Be(guid);

        guid.ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty).ToGuid(out result).Should().BeTrue();
        result.Should().Be(guid);
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
  public void String_ToUri_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUri(null)).ThrowExactly<ArgumentNullException>();

      string.Empty.ToUri().Should().NotBeSameAs(string.Empty.ToUri());

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
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUri(null, out _)).ThrowExactly<ArgumentNullException>();

      string.Empty.ToUri(out var uri);
      uri.Should().NotBeNull();
      uri.IsAbsoluteUri.Should().BeFalse();
      uri.OriginalString.Should().BeEmpty();
      uri.ToString().Should().BeEmpty();

      "path".ToUri(out uri);
      uri.Should().NotBeNull();
      uri.IsAbsoluteUri.Should().BeFalse();
      uri.OriginalString.Should().Be("path");
      uri.ToString().Should().Be("path");

      "scheme:".ToUri(out uri);
      uri.Should().NotBeNull();
      uri.IsAbsoluteUri.Should().BeTrue();
      uri.OriginalString.Should().Be("scheme:");
      uri.ToString().Should().Be("scheme:");

      "https://user:password@localhost:8080/path?query#id".ToUri(out uri);
      uri.Should().NotBeNull();
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
  public void String_ToType_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToType(null)).ThrowExactly<ArgumentNullException>();

      string.Empty.ToType().Should().BeNull();

      RandomName.ToType().Should().BeNull();

      nameof(Object).ToType().Should().BeNull();
      typeof(object).FullName.ToType().Should().Be(typeof(object));
      typeof(object).AssemblyQualifiedName.ToType().Should().Be(typeof(object));

      Assembly.GetExecutingAssembly().DefinedTypes.ForEach(type =>
      {
        nameof(type).ToType().Should().BeNull();
        type.AssemblyQualifiedName.ToType().Should().Be(type);
        type.AssemblyQualifiedName.ToType().Should().NotBeSameAs(type.AssemblyQualifiedName.ToType());
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToType(null, out _)).ThrowExactly<ArgumentNullException>();

      string.Empty.ToType(out var type).Should().BeFalse();
      type.Should().BeNull();

      RandomName.ToType(out type).Should().BeFalse();
      type.Should().BeNull();

      nameof(Object).ToType(out type).Should().BeFalse();
      type.Should().BeNull();

      typeof(object).FullName.ToType(out type).Should().BeTrue();
      type.Should().Be(typeof(object));

      typeof(object).AssemblyQualifiedName.ToType(out type).Should().BeTrue();
      type.Should().Be(typeof(object));

      Assembly.GetExecutingAssembly().DefinedTypes.ForEach(typeInfo =>
      {
        nameof(typeInfo).ToType(out type).Should().BeFalse();
        type.Should().BeNull();

        typeInfo.AssemblyQualifiedName.ToType(out type).Should().BeTrue();
        type.Should().Be(typeInfo);
      });
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
  public void String_ToDateTime_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDateTime(null)).ThrowExactly<ArgumentNullException>();

      var now = DateTime.UtcNow;

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToDateTime(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDateTime(culture)).ThrowExactly<FormatException>();

        $" {now.ToString("o", culture)} ".ToDateTime(culture).Should().Be(now);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDateTime(null, out _)).ThrowExactly<ArgumentNullException>();

      var now = DateTime.UtcNow;

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToDateTime(out _, culture).Should().BeFalse();
        "invalid".ToDateTime(out _, culture).Should().BeFalse();

        $" {now.ToString("o", culture)} ".ToDateTime(out var result, culture).Should().BeTrue();
        result.Should().Be(now);
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
  public void String_ToDateTimeOffset_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDateTimeOffset(null)).ThrowExactly<ArgumentNullException>();

      var now = DateTimeOffset.UtcNow;

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToDateTimeOffset(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDateTimeOffset(culture)).ThrowExactly<FormatException>();

        $" {now.ToString("o", culture)} ".ToDateTimeOffset(culture).Should().Be(now);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDateTimeOffset(null, out _)).ThrowExactly<ArgumentNullException>();

      var now = DateTimeOffset.UtcNow;

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToDateTimeOffset(out _, culture).Should().BeFalse();
        "invalid".ToDateTimeOffset(out _, culture).Should().BeFalse();

        $" {now.ToString("o", culture)} ".ToDateTimeOffset(out var result, culture).Should().BeTrue();
        result.Should().Be(now);
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
  public void String_ToDateOnly_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDateOnly(null)).ThrowExactly<ArgumentNullException>();

      var now = DateOnly.FromDateTime(DateTime.UtcNow);

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToDateOnly(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDateOnly(culture)).ThrowExactly<FormatException>();

        $" {now.ToLongDateString()} ".ToDateOnly().Should().Be(now);
        $" {now.ToShortDateString()} ".ToDateOnly().Should().Be(now);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDateOnly(null, out _)).ThrowExactly<ArgumentNullException>();

      var now = DateOnly.FromDateTime(DateTime.UtcNow);

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToDateOnly(out _, culture).Should().BeFalse();
        "invalid".ToDateOnly(out _, culture).Should().BeFalse();

        $" {now.ToLongDateString()} ".ToDateOnly(out var result).Should().BeTrue();
        result.Should().Be(now);

        $" {now.ToShortDateString()} ".ToDateOnly(out result).Should().BeTrue();
        result.Should().Be(now);
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
  public void String_ToTimeOnly_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToTimeOnly(null)).ThrowExactly<ArgumentNullException>();

      var now = TimeOnly.FromDateTime(DateTime.UtcNow);

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        AssertionExtensions.Should(() => string.Empty.ToTimeOnly(culture)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToTimeOnly(culture)).ThrowExactly<FormatException>();

        $" {now.ToLongTimeString()} ".ToTimeOnly(culture).Should().Be(now.TruncateToSecondStart());
        $" {now.ToShortTimeString()} ".ToTimeOnly(culture).Should().Be(now.TruncateToMinuteStart());
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToTimeOnly(null, out _)).ThrowExactly<ArgumentNullException>();

      var now = TimeOnly.FromDateTime(DateTime.UtcNow);

      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        string.Empty.ToTimeOnly(out _, culture).Should().BeFalse();
        "invalid".ToTimeOnly(out _, culture).Should().BeFalse();

        $" {now.ToLongTimeString()} ".ToTimeOnly(out var result, culture).Should().BeTrue();
        result.Should().Be(now.TruncateToSecondStart());

        $" {now.ToShortTimeString()} ".ToTimeOnly(out result, culture).Should().BeTrue();
        result.Should().Be(now.TruncateToMinuteStart());
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
  public void String_ToFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToFile(null)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(string.Empty.ToFile).ThrowExactly<ArgumentException>();

      var name = Path.GetTempFileName();

      name.ToFile().Should().NotBeSameAs(name.ToFile());

      name.ToFile().TryFinallyDelete(file =>
      {
        file.Exists.Should().BeTrue();
        file.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        file.FullName.Should().Be(name);
      });

      name = RandomName;
      name.ToFile().TryFinallyDelete(file =>
      {
        file.Exists.Should().BeFalse();
        file.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        file.FullName.Should().Be(Path.Combine(Directory.GetCurrentDirectory(), name));
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToFile(null, out _)).ThrowExactly<ArgumentNullException>();

      var name = Path.GetTempFileName();
      name.ToFile(out var file).Should().BeTrue();
      file.TryFinallyDelete(file =>
      {
        file.Exists.Should().BeTrue();
        file.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        file.FullName.Should().Be(name);
      });

      name = RandomName;
      name.ToFile(out file).Should().BeFalse();
      file.TryFinallyDelete(file =>
      {
        file.Exists.Should().BeFalse();
        file.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        file.FullName.Should().Be(Path.Combine(Directory.GetCurrentDirectory(), name));
      });
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
  public void String_ToDirectory_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDirectory(null)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(string.Empty.ToDirectory).ThrowExactly<ArgumentException>();

      var name = Environment.SystemDirectory;

      name.ToDirectory().Should().NotBeSameAs(name.ToDirectory());

      var directory = name.ToDirectory();
      directory.Exists.Should().BeTrue();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(name);

      name = RandomName;
      directory = name.ToDirectory();
      directory.Exists.Should().BeFalse();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(Path.Combine(Directory.GetCurrentDirectory(), name));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDirectory(null, out _)).ThrowExactly<ArgumentNullException>();

      var name = Environment.SystemDirectory;
      name.ToDirectory(out var directory).Should().BeTrue();
      directory.Exists.Should().BeTrue();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(name);

      name = RandomName;
      name.ToDirectory(out directory).Should().BeFalse();
      directory.Exists.Should().BeFalse();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(Path.Combine(Directory.GetCurrentDirectory(), name));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToPath(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToPath_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToPath(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="StringExtensions.ToIpAddress(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToIpAddress(string, out IPAddress)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void String_ToIpAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToIpAddress(null)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(string.Empty.ToIpAddress).ThrowExactly<FormatException>();
      AssertionExtensions.Should("localhost".ToIpAddress).ThrowExactly<FormatException>();

      foreach (var ip in new[] { IPAddress.None, IPAddress.Any, IPAddress.Loopback, IPAddress.Broadcast, IPAddress.IPv6None, IPAddress.IPv6Any, IPAddress.IPv6Loopback })
      {
        ip.ToString().ToIpAddress().Should().Be(ip);
        ip.ToString().ToIpAddress().Should().NotBeSameAs(ip.ToString().ToIpAddress());
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToIpAddress(null, out _)).ThrowExactly<ArgumentNullException>();

      string.Empty.ToIpAddress(out var result).Should().BeFalse();
      result.Should().BeNull();

      "localhost".ToIpAddress(out result).Should().BeFalse();
      result.Should().BeNull();

      foreach (var ip in new[] { IPAddress.None, IPAddress.Any, IPAddress.Loopback, IPAddress.Broadcast, IPAddress.IPv6None, IPAddress.IPv6Any, IPAddress.IPv6Loopback })
      {
        ip.ToString().ToIpAddress(out result).Should().BeTrue();
        result.Should().Be(ip);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToIpHost(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToIpHost_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToIpHost(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToRegex(string, RegexOptions)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToRegex_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToRegex(null)).ThrowExactly<ArgumentNullException>();

    string.Empty.ToRegex().Should().NotBeSameAs(string.Empty.ToRegex());

    var regex = string.Empty.ToRegex();
    regex.ToString().Should().BeEmpty();
    regex.Options.Should().Be(RegexOptions.None);
    regex.MatchTimeout.Should().Be(Timeout.InfiniteTimeSpan);
    regex.RightToLeft.Should().BeFalse();

    regex = "[a-z]*".ToRegex(RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
    regex.ToString().Should().Be("[a-z]*");
    regex.Options.Should().Be(RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
    regex.MatchTimeout.Should().Be(Timeout.InfiniteTimeSpan);
    regex.RightToLeft.Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToStringBuilder(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToStringBuilder_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToStringBuilder(null)).ThrowExactly<ArgumentNullException>();

    var builder = string.Empty.ToStringBuilder();
    builder.Should().NotBeNull().And.NotBeSameAs(string.Empty.ToStringBuilder());
    builder.ToString().Should().BeEmpty();
    builder.Capacity.Should().Be(16);
    builder.MaxCapacity.Should().Be(int.MaxValue);
    builder.Length.Should().Be(0);

    var value = RandomString;
    builder = value.ToStringBuilder();
    builder.Should().NotBeNull().And.NotBeSameAs(value.ToStringBuilder());
    builder.ToString().Should().Be(value);
    builder.Capacity.Should().Be(value.Length);
    builder.MaxCapacity.Should().Be(int.MaxValue);
    builder.Length.Should().Be(value.Length);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToStringReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToStringReader_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToStringReader(null)).ThrowExactly<ArgumentNullException>();

    var reader = string.Empty.ToStringReader();
    reader.Should().NotBeNull().And.NotBeSameAs(string.Empty.ToStringReader());
    reader.Peek().Should().Be(-1);
    reader.ReadToEnd().Should().BeEmpty();

    var value = RandomString;
    reader = value.ToStringReader();
    reader.Should().NotBeNull().And.NotBeSameAs(value.ToStringReader());
    reader.ReadToEnd().Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToProcess(string, ProcessStartInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_ToProcess_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToProcess(null)).ThrowExactly<ArgumentNullException>();

    foreach (var command in new[] { string.Empty, "cmd" })
    {
      var process = command.ToProcess();

      AssertionExtensions.Should(() => process.Id).ThrowExactly<InvalidOperationException>();

      process.Should().NotBeNull().And.NotBeSameAs(string.Empty.ToProcess());

      process.StartInfo.FileName.Should().Be(command);
      process.StartInfo.ArgumentList.Should().BeEmpty();
      process.StartInfo.Arguments.Should().BeEmpty();
      process.StartInfo.CreateNoWindow.Should().BeFalse();
      process.StartInfo.Domain.Should().BeEmpty();
      process.StartInfo.Environment.Should().NotBeNullOrEmpty().And.HaveCount(Environment.GetEnvironmentVariables().Count);
      process.StartInfo.ErrorDialog.Should().BeFalse();
      process.StartInfo.ErrorDialogParentHandle.Should().Be(0);
      process.StartInfo.LoadUserProfile.Should().BeFalse();
      process.StartInfo.Password.Should().BeNull();
      process.StartInfo.PasswordInClearText.Should().BeNull();
      process.StartInfo.RedirectStandardError.Should().BeFalse();
      process.StartInfo.RedirectStandardInput.Should().BeFalse();
      process.StartInfo.RedirectStandardOutput.Should().BeFalse();
      process.StartInfo.StandardErrorEncoding.Should().BeNull();
      process.StartInfo.StandardInputEncoding.Should().BeNull();
      process.StartInfo.StandardOutputEncoding.Should().BeNull();
      process.StartInfo.UserName.Should().BeEmpty();
      process.StartInfo.UseShellExecute.Should().BeFalse();
      process.StartInfo.Verb.Should().BeEmpty();
      process.StartInfo.Verbs.Should().BeEmpty();
      process.StartInfo.WindowStyle.Should().Be(ProcessWindowStyle.Normal);
      process.StartInfo.WorkingDirectory.Should().BeEmpty();
    }
  }
}