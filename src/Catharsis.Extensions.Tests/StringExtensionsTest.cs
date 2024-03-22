using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using System.Xml;
using Catharsis.Commons;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="StringExtensions"/>.</para>
/// </summary>
public sealed class StringExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsEmpty(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    Validate(null, true);
    Validate(string.Empty, true);
    Validate(" \t\r\n ", true);
    Validate(" * ", false);

    return;

    static void Validate(string text, bool result) => text.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUpperCased(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUpperCased_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.IsUpperCased(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();

    return;

    static void Validate(string text, bool result) => text.IsUpperCased().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsLowerCased(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsLowerCased_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.IsLowerCased(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();

    return;

    static void Validate(string text, bool result) => text.IsLowerCased().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsBoolean(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsBoolean_Method()
  {
    Validate(null, false);
    Validate(string.Empty, false);
    Validate(bool.FalseString, true);
    Validate(bool.TrueString, true);
    Validate("invalid", false);
    Validate("TRUE", true);
    Validate("TruE", true);
    Validate("true", true);
    Validate(" true ", true);

    return;

    static void Validate(string text, bool result) => text.IsBoolean().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsSbyte(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsSbyte_Method()
  {
    StringExtensions.IsSbyte(null).Should().BeFalse();

    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(null, false, culture);
      Validate(string.Empty, false, culture);
      Validate("invalid", false, culture);
      Validate(sbyte.MinValue.ToString(culture), true, culture);
      Validate(sbyte.MaxValue.ToString(culture), true, culture);
      Validate($" {sbyte.MinValue.ToString(culture)} ", true, culture);
    });

    return;

    static void Validate(string text, bool result, IFormatProvider format = null) => text.IsByte(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsByte(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsByte_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(null, false, culture);
      Validate(string.Empty, false, culture);
      Validate("invalid", false, culture);
      Validate(byte.MinValue.ToString(culture), true, culture);
      Validate(byte.MaxValue.ToString(culture), true, culture);
      Validate($" {byte.MinValue.ToString(culture)} ", true, culture);
    });

    return;

    static void Validate(string text, bool result, IFormatProvider format = null) => text.IsByte(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsShort(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsShort_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(null, false, culture);
      Validate(string.Empty, false, culture);
      Validate("invalid", false, culture);
      Validate(short.MinValue.ToString(culture), true, culture);
      Validate(short.MaxValue.ToString(culture), true, culture);
      Validate($" {short.MinValue.ToString(culture)} ", true, culture);
    });

    return;

    static void Validate(string text, bool result, IFormatProvider format = null) => text.IsShort(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUshort(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUshort_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(null, false, culture);
      Validate(string.Empty, false, culture);
      Validate("invalid", false, culture);
      Validate(ushort.MinValue.ToString(culture), true, culture);
      Validate(ushort.MaxValue.ToString(culture), true, culture);
      Validate($" {ushort.MinValue.ToString(culture)} ", true, culture);
    });

    return;

    static void Validate(string text, bool result, IFormatProvider format = null) => text.IsUshort(format).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsInt(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsInt_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null, culture);
      Validate(false, string.Empty, culture);
      Validate(false, "invalid", culture);
      Validate(true, int.MinValue.ToString(culture), culture);
      Validate(true, int.MaxValue.ToString(culture), culture);
      Validate(true, $" {int.MinValue.ToString(culture)} ", culture);
    });

    return;

    static void Validate(bool isInt, string text, IFormatProvider format = null) => text.IsInt(format).Should().Be(isInt);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUint(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUint_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null, culture);
      Validate(false, string.Empty, culture);
      Validate(false, "invalid", culture);
      Validate(true, uint.MinValue.ToString(culture), culture);
      Validate(true, uint.MaxValue.ToString(culture), culture);
      Validate(true, $" {uint.MinValue.ToString(culture)} ", culture);
    });

    return;

    static void Validate(bool isUint, string text, IFormatProvider format = null) => text.IsUint(format).Should().Be(isUint);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsLong(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsLong_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null, culture);
      Validate(false, string.Empty, culture);
      Validate(false, "invalid", culture);
      Validate(true, long.MinValue.ToString(culture), culture);
      Validate(true, long.MaxValue.ToString(culture), culture);
      Validate(true, $" {long.MinValue.ToString(culture)} ", culture);
    });

    return;

    static void Validate(bool isLong, string text, IFormatProvider format = null) => text.IsLong(format).Should().Be(isLong);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUlong(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUlong_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null, culture);
      Validate(false, string.Empty, culture);
      Validate(false, "invalid", culture);
      Validate(true, ulong.MinValue.ToString(culture), culture);
      Validate(true, ulong.MaxValue.ToString(culture), culture);
      Validate(true, $" {ulong.MinValue.ToString(culture)} ", culture);
    });

    return;

    static void Validate(bool isUlong, string text, IFormatProvider format = null) => text.IsUlong(format).Should().Be(isUlong);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsFloat(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFloat_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null, culture);
      Validate(false, string.Empty, culture);
      Validate(false, "invalid", culture);
      Validate(true, float.MinValue.ToString(culture), culture);
      Validate(true, float.MaxValue.ToString(culture), culture);
      Validate(true, $" {float.MinValue.ToString(culture)} ", culture);
      Validate(true, float.NaN.ToString(culture), culture);
      Validate(true, float.Epsilon.ToString(culture), culture);
      Validate(true, float.NegativeInfinity.ToString(culture), culture);
      Validate(true, float.PositiveInfinity.ToString(culture), culture);
    });

    return;

    static void Validate(bool isFloat, string text, IFormatProvider format = null) => text.IsFloat(format).Should().Be(isFloat);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDouble(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDouble_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null, culture);
      Validate(false, string.Empty, culture);
      Validate(false, "invalid", culture);
      Validate(true, double.MinValue.ToString(culture), culture);
      Validate(true, double.MaxValue.ToString(culture), culture);
      Validate(true, $" {double.MinValue.ToString(culture)} ", culture);
      Validate(true, double.NaN.ToString(culture), culture);
      Validate(true, double.Epsilon.ToString(culture), culture);
      Validate(true, double.NegativeInfinity.ToString(culture), culture);
      Validate(true, double.PositiveInfinity.ToString(culture), culture);
    });

    return;

    static void Validate(bool isDouble, string text, IFormatProvider format = null) => text.IsDouble().Should().Be(isDouble);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDecimal(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDecimal_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null, culture);
      Validate(false, string.Empty, culture);
      Validate(false, "invalid", culture);
      Validate(true, decimal.MinValue.ToString(culture), culture);
      Validate(true, decimal.MaxValue.ToString(culture), culture);
      Validate(true, $" {decimal.MinValue.ToString(culture)} ", culture);
      Validate(true, decimal.MinusOne.ToString(culture), culture);
      Validate(true, decimal.Zero.ToString(culture), culture);
      Validate(true, decimal.One.ToString(culture), culture);
    });

    return;

    static void Validate(bool isDecimal, string text, IFormatProvider format = null) => text.IsDecimal(format).Should().Be(isDecimal);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsEnum{T}(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEnum_Method()
  {
    Validate<DayOfWeek>(false, null);
    Validate<DayOfWeek>(false, string.Empty);
    Validate<DayOfWeek>(false, "invalid");

    Enum.GetNames<DayOfWeek>().ForEach(day => Validate<DayOfWeek>(true, day));
    Enum.GetNames<DayOfWeek>().ForEach(day => Validate<DayOfWeek>(true, day.ToLower()));
    Enum.GetNames<DayOfWeek>().ForEach(day => Validate<DayOfWeek>(true, day.ToUpper()));

    return;

    static void Validate<T>(bool isEnum, string text) where T : struct => text.IsEnum<T>().Should().Be(isEnum);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsGuid(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsGuid_Method()
  {
    Validate(false, null);
    Validate(false, string.Empty);
    Validate(false, "invalid");

    new[] { Guid.Empty, Guid.NewGuid() }.ForEach(guid =>
    {
      Validate(true, guid.ToString());
      Validate(true, guid.ToString().ToLowerInvariant());
      Validate(true, guid.ToString().ToUpperInvariant());
      Validate(true, guid.ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty));
    });

    return;

    static void Validate(bool isGuid, string text) => text.IsGuid().Should().Be(isGuid);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsUri(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUri_Method()
  {
    Validate(false, null);
    Validate(true, string.Empty);
    Validate(true, "path");
    Validate(true, "https:");
    Validate(false, "https://");
    Validate(true, "https://user:password@localhost:8080/path?query#id");

    return;

    static void Validate(bool isUri, string text) => text.IsUri().Should().Be(isUri);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsType(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsType_Method()
  {
    Validate(false, null);
    Validate(false, string.Empty);
    Validate(false, Attributes.RandomName());
    Validate(false, nameof(Object));
    Validate(true, typeof(object).FullName);
    Validate(true, typeof(object).AssemblyQualifiedName);  

    Assembly.GetExecutingAssembly().DefinedTypes.ForEach(type =>
    {
      Validate(false, nameof(type));
      Validate(true, type.AssemblyQualifiedName);
    });

    return;

    static void Validate(bool isType, string text) => text.IsType().Should().Be(isType);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateTime(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateTime_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null);
      Validate(false, string.Empty);
      Validate(false, "invalid");
      Validate(true, $" {DateTime.MinValue.ToString("o", culture)} ");
      Validate(true, $" {DateTime.MaxValue.ToString("o", culture)} ");
      Validate(true, $" {DateTime.UtcNow.ToString("o", culture)} ");
      Validate(true, $" {DateTime.Now.ToString("o", culture)} ");
    });

    return;

    static void Validate(bool isDateTime, string text, IFormatProvider format = null) => text.IsDateTime(format).Should().Be(isDateTime);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateTimeOffset(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateTimeOffset_Method()
  {
    CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture =>
    {
      Validate(false, null);
      Validate(false, string.Empty);
      Validate(false, "invalid");
      Validate(true, $" {DateTimeOffset.MinValue.ToString("o", culture)} ");
      Validate(true, $" {DateTimeOffset.MaxValue.ToString("o", culture)} ");
      Validate(true, $" {DateTimeOffset.UtcNow.ToString("o", culture)} ");
      Validate(true, $" {DateTimeOffset.Now.ToString("o", culture)} ");
    });

    return;

    static void Validate(bool isDateTimeOffset, string text, IFormatProvider format = null) => text.IsDateTimeOffset(format).Should().Be(isDateTimeOffset);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsFile(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsFile_Method()
  {
    Validate(false, null);
    Validate(false, string.Empty);
    Validate(false, Attributes.RandomName());
    Validate(false, Environment.SystemDirectory);
    Attributes.RandomEmptyFile().TryFinallyDelete(file => Validate(true, file.FullName));

    return;

    static void Validate(bool isFile, string text) => text.IsFile().Should().Be(isFile);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDirectory(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDirectory_Method()
  {
    Validate(false, null);
    Validate(false, string.Empty);
    Validate(false, Attributes.RandomName());
    Validate(true, Environment.SystemDirectory);
    Attributes.RandomDirectory().TryFinallyDelete(directory => Validate(true, directory.FullName));

    return;

    static void Validate(bool isDirectory, string text) => text.IsDirectory().Should().Be(isDirectory);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsIpAddress(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsIpAddress_Method()
  {
    Validate(null, false);
    Validate(string.Empty, false);
    Validate("localhost", false);

    new[] { IPAddress.None, IPAddress.Any, IPAddress.Loopback, IPAddress.Broadcast, IPAddress.IPv6None, IPAddress.IPv6Any, IPAddress.IPv6Loopback }.ForEach(address => Validate(address.ToString(), true));

    return;

    static void Validate(string text, bool result) => text.IsIpAddress().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Min(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Min(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => string.Empty.Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

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

    return;

    static void Validate(string min, string max) => min.Min(max).Should().Be(min);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Max(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Max(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => string.Empty.Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

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

    return;

    static void Validate(string min, string max) => min.Max(max).Should().Be(max);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Compare(string, string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Compare_Method()
  {
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      Validate(null);
      Validate(culture);
    }

    return;

    static void Validate(CultureInfo culture)
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
    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      Validate(null);
      Validate(culture);
    }

    return;

    static void Validate(IFormatProvider format)
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
    foreach (var date in new[] { DateTimeOffset.Now, DateTimeOffset.UtcNow })
    {
      foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        Validate(date, null);
        Validate(date, culture);
      }
    }

    return;

    static void Validate(DateTimeOffset date, IFormatProvider format)
    {
      AssertionExtensions.Should(() => string.Empty.CompareAsDate(date.ToString(format), format)).ThrowExactly<FormatException>();
      AssertionExtensions.Should(() => date.ToString(format).CompareAsDate(string.Empty, format)).ThrowExactly<FormatException>();

      date.ToString("o", format).CompareAsDate(date.ToString("o", format), format).Should().Be(0);
      date.AddMilliseconds(1).ToString("o", format).CompareAsDate(date.ToString("o", format), format).Should().BePositive();
      date.AddMilliseconds(-1).ToString("o", format).CompareAsDate(date.ToString("o", format), format).Should().BeNegative();

      date.ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.TruncateToDayStart().AddMilliseconds(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.TruncateToDayStart().AddSeconds(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.TruncateToDayStart().AddMinutes(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.TruncateToDayStart().AddHours(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().Be(0);
      date.TruncateToDayStart().AddDays(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().BePositive();
      date.TruncateToDayStart().AddMonths(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().BePositive();
      date.TruncateToDayStart().AddYears(1).ToString("D", format).CompareAsDate(date.ToString("D", format)).Should().BePositive();

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
    StringExtensions.Append(null, null).Should().NotBeNull().And.BeEmpty();
    string.Empty.Append(null).Should().NotBeNull().And.BeEmpty();
    string.Empty.Append(string.Empty).Should().NotBeNull().And.BeEmpty();

    "\r\n".Append("\t").Should().BeNullOrWhiteSpace();
    "value".Append(null).Should().Be("value");
    "first".Append(" & ").Append("second").Should().Be("first & second");

    return;

    static void Validate(string text, string postfix)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Prepend(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Prepend_Method()
  {
    StringExtensions.Prepend(null, null).Should().NotBeNull().And.BeEmpty();
    string.Empty.Prepend(null).Should().NotBeNull().And.BeEmpty();
    string.Empty.Prepend(string.Empty).Should().NotBeNull().And.BeEmpty();
   
    "\r\n".Prepend("\t").Should().BeNullOrWhiteSpace();
    "value".Prepend(null).Should().Be("value");
    "second".Prepend(" & ").Prepend("first").Should().Be("first & second");

    return;

    static void Validate(string text, string prefix)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.RemoveRange(string, int, int?, Predicate{char})"/> method.</para>
  /// </summary>
  [Fact]
  public void RemoveRange_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.RemoveRange(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.RemoveRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
    AssertionExtensions.Should(() => string.Empty.RemoveRange(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    string.Empty.RemoveRange(0).Should().BeEmpty();
    string.Empty.RemoveRange(10).Should().BeEmpty();

    const string value = "0123456789";
    value.RemoveRange(0).Should().Be(value);
    value.RemoveRange(1).Should().Be(value.TakeLast(value.Length - 1).ToArray().ToText());
    value.RemoveRange(value.Length).Should().BeEmpty();
    value.RemoveRange(value.Length + 1).Should().BeEmpty();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Reverse(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Reverse_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Reverse(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.Reverse().Should().NotBeNull().And.BeSameAs(string.Empty.Reverse()).And.BeEmpty();

    var text = Attributes.RandomString();
    var reversed = text.Reverse();
    reversed.Should().NotBeNull().And.NotBeSameAs(text.Reverse()).And.Be(reversed.ToCharArray().ToText());

    return;

    static void Validate()
    {
    }
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
      AssertionExtensions.Should(() => StringExtensions.Replace(null, Enumerable.Empty<(string Name, object Value)>())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
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
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.Replace(null, []!)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.Replace(null)).ThrowExactly<ArgumentNullException>().WithParameterName("replacements");

      /*string.Empty.Replace().Should().BeEmpty();
      string.Empty.Replace(("quick", "slow")).Should().BeEmpty();

      const string value = "The quick brown fox jumped over the lazy dog";

      value.Replace(("quick", "slow"), ("dog", "bear"), ("brown", "hazy"), ("UNSPECIFIED", string.Empty)).Should().Be("The slow hazy fox jumped over the lazy bear");*/
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.SwapCase(string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void SwapCase_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.SwapCase(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    var builder = new StringBuilder();

    for (var i = 'a'; i <= 'z'; i++)
    {
      builder.Append(i);
    }
    for (var i = '�'; i <= '�'; i++)
    {
      builder.Append(i);
    }

    var value = builder.ToString();

    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      value.SwapCase(culture).Should().BeUpperCased();
      value.SwapCase(culture).SwapCase(culture).Should().BeLowerCased();
    }

    return;

    static void Validate(string text, CultureInfo culture = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Capitalize(string, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Capitalize_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Capitalize(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

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

    return;

    static void Validate(string text, CultureInfo culture = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.CapitalizeAll(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void CapitalizeAll_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.CapitalizeAll(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.CapitalizeAll().Should().BeEmpty();

    "Word & Deed".CapitalizeAll().Should().Be("Word & Deed");
    "word & deed".CapitalizeAll().Should().Be("Word & Deed");
    "wORD & deed".CapitalizeAll().Should().Be("Word & Deed");

    "Слово & Дело".CapitalizeAll().Should().Be("Слово & Дело");
    "слово & дело".CapitalizeAll().Should().Be("Слово & Дело");
    "сЛОВО & дело".CapitalizeAll().Should().Be("Слово & Дело");

    return;

    static void Validate(string text, CultureInfo culture = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Repeat(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Repeat_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Repeat(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.Repeat(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    string.Empty.Repeat(0).Should().NotBeNull().And.BeSameAs(string.Empty.Repeat(0)).And.BeEmpty();
    string.Empty.Repeat(1).Should().NotBeNull().And.BeSameAs(string.Empty.Repeat(1)).And.BeEmpty();

    const int count = 1000;

    var repeated = char.MinValue.ToString().Repeat(count);
    repeated.Should().NotBeNull().And.NotBeSameAs(char.MinValue.ToString().Repeat(count)).And.HaveLength(count);
    repeated.ToCharArray().Should().AllBeEquivalentTo(char.MinValue);

    repeated = char.MaxValue.ToString().Repeat(count);
    repeated.Should().NotBeNull().And.NotBeSameAs(char.MinValue.ToString().Repeat(count)).And.HaveLength(count);
    repeated.ToCharArray().Should().AllBeEquivalentTo(char.MinValue);

    "*".Repeat(0).Should().BeEmpty();
    "*".Repeat(1).Should().Be("*");
    "xyz".Repeat(2).Should().Be("xyzxyz");

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Lines(string, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Lines_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Lines(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.Lines().Should().NotBeNull().And.BeSameAs(string.Empty.Lines()).And.BeEmpty();
    string.Empty.Lines("\t").Should().NotBeNull().And.BeSameAs(string.Empty.Lines("\t")).And.BeEmpty();

    var text = Attributes.RandomString();
    text.Lines().Should().NotBeNull().And.NotBeSameAs(text.Lines()).And.HaveCount(1).And.HaveElementAt(0, text);

    var strings = 10.Objects(() => Attributes.RandomString()).AsArray();
    text = strings.Join(Environment.NewLine);
    var lines = text.Lines();
    lines.Should().NotBeNull().And.NotBeSameAs(text.Lines()).And.HaveCount(strings.Length).And.Equal(strings);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsMatch(string, string, RegexOptions?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsMatch_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.IsMatch(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.IsMatch(null)).ThrowExactly<ArgumentNullException>().WithParameterName("pattern");

    /*
    string.Empty.IsMatch(string.Empty).Should().BeTrue();
    string.Empty.IsMatch("anything").Should().BeFalse();
    "ab4Zg95kf".IsMatch("[a-zA-z0-9]").Should().BeTrue();
    "~#$%".IsMatch("[a-zA-z0-9]").Should().BeFalse();*/

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Matches(string, string, RegexOptions?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Matches_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Matches(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.Matches(null)).ThrowExactly<ArgumentNullException>().WithParameterName("pattern");

    /*string.Empty.Matches("anything").Should().BeEmpty();
    var matches = "ab#1".Matches("[a-zA-z0-9]");
    matches.Should().HaveCount(3);
    matches.ElementAt(0).Value.Should().Be("a");
    matches.ElementAt(1).Value.Should().Be("b");
    matches.ElementAt(2).Value.Should().Be("1");*/

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.FromBase64(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Base64_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.FromBase64(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.FromBase64().Should().NotBeNull().And.BeSameAs(string.Empty.FromBase64()).And.BeEmpty();

    var bytes = Attributes.RandomBytes();
    bytes.ToBase64().Should().NotBeNull().And.NotBeSameAs(bytes.ToBase64()).And.Be(System.Convert.ToBase64String(bytes));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.UrlEncode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void UrlEncode_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.UrlEncode(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.UrlEncode().Should().BeEmpty();

    const string value = "#value?";
    value.UrlEncode().Should().Be(Uri.EscapeDataString(value)).And.Be("%23value%3F");
    Uri.UnescapeDataString(value.UrlEncode()).Should().Be(value);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.UrlDecode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void UrlDecode_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.UrlDecode(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.UrlDecode().Should().BeEmpty();

    const string value = "%23value%3F";
    value.UrlDecode().Should().Be(Uri.UnescapeDataString(value)).And.Be("#value?");
    Uri.EscapeDataString(value.UrlDecode()).Should().Be(value);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HtmlEncode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HtmlEncode_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.HtmlEncode(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.HtmlEncode().Should().BeEmpty();

    const string value = "<p>word & deed</p>";

    value.HtmlEncode().Should().Be(WebUtility.HtmlEncode(value)).And.Be("&lt;p&gt;word &amp; deed&lt;/p&gt;");
    WebUtility.HtmlDecode(value.HtmlEncode()).Should().Be(value);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HtmlDecode(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HtmlDecode_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.HtmlDecode(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.HtmlDecode().Should().BeEmpty();

    const string value = "&lt;p&gt;word &amp; deed&lt;/p&gt;";

    value.HtmlDecode().Should().Be(WebUtility.HtmlDecode(value)).And.Be("<p>word & deed</p>");
    WebUtility.HtmlEncode(value.HtmlDecode()).Should().Be(value);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Indent(string, char, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Indent_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Indent(null, char.MinValue)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.Indent(char.MinValue, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 2;



    string.Empty.Indent('*', 0).Should().BeEmpty();
    string.Empty.Indent('*').Should().Be("*");
    string.Empty.Indent('*', count).Should().HaveLength(count).And.Be('*'.Repeat(count));

    "***".Indent(' ', count).Should().HaveLength(count + 3).And.Be(' '.Repeat(count) + "***");
    $" 1.{Environment.NewLine} 2. ".Indent('*', count).Should().HaveLength(count * 2 + 4 + Environment.NewLine.Length).And.Be('*'.Repeat(count) + "1." + Environment.NewLine + '*'.Repeat(count) + "2.");

    return;

    static void Validate(string text, char character, int count, string result) => text.Indent(character, count).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Unindent(string, char)"/> method.</para>
  /// </summary>
  [Fact]
  public void Unindent_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Unindent(null, char.MinValue)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();

    return;

    static void Validate(string text, char value, string result) => text.Unindent(value).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Spacify(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Spacify_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Spacify(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.Spacify(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 2;

    Validate(string.Empty, 0, string.Empty);
    Validate(string.Empty, 1, " ");
    Validate(string.Empty, count, ' '.Repeat(count));
    Validate("***", count, ' '.Repeat(count) + "***");
    Validate($" 1.{Environment.NewLine} 2. ", count, ' '.Repeat(count) + "1." + Environment.NewLine + ' '.Repeat(count) + "2.");

    return;

    static void Validate(string text, int count, string result) => text.Spacify(count).Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Unspacify(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Unspacify_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Unspacify(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();

    return;

    static void Validate(string text, string result) => text.Unspacify().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Tabify(string, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Tabify_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Tabify(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.Tabify(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 2;

    string.Empty.Tabify(0).Should().BeEmpty();
    string.Empty.Tabify().Should().Be("\t");
    string.Empty.Tabify(count).Should().HaveLength(count).And.Be('\t'.Repeat(count));

    "***".Tabify(count).Should().HaveLength(count + 3).And.Be('\t'.Repeat(count) + "***");
    $" 1.{Environment.NewLine} 2. ".Tabify(count).Should().HaveLength(count * 2 + 4 + Environment.NewLine.Length).And.Be('\t'.Repeat(count) + "1." + Environment.NewLine + '\t'.Repeat(count) + "2.");

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Untabify(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Untabify_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.Untabify(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    return;

    static void Validate()
    {
    }

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
  public void Execute_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.Execute(null)).ThrowExactly<ArgumentNullException>().WithParameterName("command");
      AssertionExtensions.Should(() => string.Empty.Execute()).ThrowExactly<InvalidOperationException>();

      string[] arguments = ["dir"];

      var process = Attributes.ShellCommand().Execute(arguments);

      process.Finish(TimeSpan.FromSeconds(5));

      process.Should().NotBeNull();

      process.Id.Should().BePositive();
      process.HasExited.Should().BeTrue();
      process.ExitCode.Should().Be(-1);
      process.StartTime.Should().BeBefore(DateTime.Now);
      process.ExitTime.Should().BeBefore(DateTime.Now);

      process.StartInfo.FileName.Should().Be(Attributes.ShellCommand());
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
      process.StartInfo.WindowStyle.Should().Be(ProcessWindowStyle.Normal);
      process.StartInfo.WorkingDirectory.Should().BeEmpty();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.Execute(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("command");
      AssertionExtensions.Should(() => string.Empty.Execute([])).ThrowExactly<InvalidOperationException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToBytes(string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Validate(Attributes.RandomString(), null);
      Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomString(), encoding.GetEncoding()));
    }

    return;

    static void Validate(string text, Encoding encoding)
    {
      string.Empty.ToBytes(encoding).Should().NotBeNull().And.BeSameAs(string.Empty.ToBytes(encoding)).And.BeEmpty();

      var bytes = text.ToBytes(encoding);
      bytes.Should().NotBeNull().And.NotBeSameAs(text.ToBytes(encoding)).And.HaveCount((encoding ?? Encoding.Default).GetByteCount(text));
      bytes.ToText(encoding).Should().Be(text);
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
      AssertionExtensions.Should(() => StringExtensions.ToBoolean(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
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
      StringExtensions.ToBoolean(null, out var result).Should().BeFalse();
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
  public void ToSbyte_Methods()
  {
    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToSbyte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToSbyte(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToSbyte(format)).ThrowExactly<FormatException>();

        sbyte.MinValue.ToString(format).ToSbyte(format).Should().Be(sbyte.MinValue);
        sbyte.MaxValue.ToString(format).ToSbyte(format).Should().Be(sbyte.MaxValue);

        $" {sbyte.MinValue.ToString(format)} ".ToSbyte(format).Should().Be(sbyte.MinValue);
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToByte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToByte(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToByte(format)).ThrowExactly<FormatException>();

        byte.MinValue.ToString(format).ToByte(format).Should().Be(byte.MinValue);
        byte.MaxValue.ToString(format).ToByte(format).Should().Be(byte.MaxValue);

        $" {byte.MinValue.ToString(format)} ".ToByte(format).Should().Be(byte.MinValue);
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToShort(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToShort(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToShort(format)).ThrowExactly<FormatException>();

        short.MinValue.ToString(format).ToShort(format).Should().Be(short.MinValue);
        short.MaxValue.ToString(format).ToShort(format).Should().Be(short.MaxValue);

        $" {short.MinValue.ToString(format)} ".ToShort(format).Should().Be(short.MinValue);
      }
    }

    using (new AssertionScope())
    {
      static void Validate(IFormatProvider format)
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
      }

      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToUshort(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToUshort(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUshort(format)).ThrowExactly<FormatException>();

        ushort.MinValue.ToString(format).ToUshort(format).Should().Be(ushort.MinValue);
        ushort.MaxValue.ToString(format).ToUshort(format).Should().Be(ushort.MaxValue);

        $" {ushort.MinValue.ToString(format)} ".ToUshort(format).Should().Be(ushort.MinValue);
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToInt(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToInt(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToInt(format)).ThrowExactly<FormatException>();

        int.MinValue.ToString(format).ToInt(format).Should().Be(int.MinValue);
        int.MaxValue.ToString(format).ToInt(format).Should().Be(int.MaxValue);

        $" {int.MinValue.ToString(format)} ".ToInt(format).Should().Be(int.MinValue);
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToUint(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToUint(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUint(format)).ThrowExactly<FormatException>();

        uint.MinValue.ToString(format).ToUint(format).Should().Be(uint.MinValue);
        uint.MaxValue.ToString(format).ToUint(format).Should().Be(uint.MaxValue);

        $" {uint.MinValue.ToString(format)} ".ToUint(format).Should().Be(uint.MinValue);
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToLong(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToLong(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToLong(format)).ThrowExactly<FormatException>();

        long.MinValue.ToString(format).ToLong(format).Should().Be(long.MinValue);
        long.MaxValue.ToString(format).ToLong(format).Should().Be(long.MaxValue);

        $" {long.MinValue.ToString(format)} ".ToLong(format).Should().Be(long.MinValue);
      }
    }

    using (new AssertionScope())
    {
      static void Validate(IFormatProvider format)
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
      }

      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
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
      static void Validate(IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToUlong(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToUlong(format)).ThrowExactly<FormatException>();

        ulong.MinValue.ToString(format).ToUlong(format).Should().Be(ulong.MinValue);
        ulong.MaxValue.ToString(format).ToUlong(format).Should().Be(ulong.MaxValue);

        $" {ulong.MinValue.ToString(format)} ".ToUlong(format).Should().Be(ulong.MinValue);
      }

      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToUlong(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToFloat(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToDouble(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToDecimal(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        Validate(null);
        CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
      }

      static void Validate(IFormatProvider format)
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
  public void ToEnum_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToEnum<Guid>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => Guid.NewGuid().ToString().ToEnum<Guid>()).ThrowExactly<ArgumentException>();
      AssertionExtensions.Should(string.Empty.ToEnum<DayOfWeek>).ThrowExactly<ArgumentException>();
      AssertionExtensions.Should("invalid".ToEnum<DayOfWeek>).ThrowExactly<ArgumentException>();

      Enum.GetNames<DayOfWeek>().Select(day => day.ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());
      Enum.GetNames<DayOfWeek>().Select(day => day.ToLower().ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());
      Enum.GetNames<DayOfWeek>().Select(day => day.ToUpper().ToEnum<DayOfWeek>()).Should().Equal(Enum.GetValues<DayOfWeek>());
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
      AssertionExtensions.Should(() => StringExtensions.ToGuid(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
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
      string.Empty.ToGuid(out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToGuid(out result).Should().BeFalse();
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
  public void ToUri_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToUri(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

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
      StringExtensions.ToUri(null, out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToUri(out result);
      result.Should().NotBeNull();
      result.IsAbsoluteUri.Should().BeFalse();
      result.OriginalString.Should().BeEmpty();
      result.ToString().Should().BeEmpty();

      "path".ToUri(out result);
      result.Should().NotBeNull();
      result.IsAbsoluteUri.Should().BeFalse();
      result.OriginalString.Should().Be("path");
      result.ToString().Should().Be("path");

      "scheme:".ToUri(out result);
      result.Should().NotBeNull();
      result.IsAbsoluteUri.Should().BeTrue();
      result.OriginalString.Should().Be("scheme:");
      result.ToString().Should().Be("scheme:");

      "https://user:password@localhost:8080/path?query#id".ToUri(out result);
      result.Should().NotBeNull();
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
      AssertionExtensions.Should(() => StringExtensions.ToType(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      string.Empty.ToType().Should().BeNull();

      Attributes.RandomName().ToType().Should().BeNull();

      nameof(Object).ToType().Should().BeNull();
      typeof(object).FullName.ToType().Should().Be(typeof(object));
      typeof(object).AssemblyQualifiedName.ToType().Should().Be(typeof(object));

      Assembly.GetExecutingAssembly().DefinedTypes.ForEach(type =>
      {
        nameof(type).ToType().Should().BeNull();
        type.AssemblyQualifiedName.ToType().Should().Be(type);
      });
    }

    using (new AssertionScope())
    {
      StringExtensions.ToType(null, out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToType(out result).Should().BeFalse();
      result.Should().BeNull();

      Attributes.RandomName().ToType(out result).Should().BeFalse();
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
        result.Should().Be(typeInfo);
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
  public void ToDateTime_Methods()
  {
    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToDateTime(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        foreach (var date in new[] {DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow})
        {
          Validate(date, null);
          CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Validate(date, culture));
        }

        static void Validate(DateTime date, IFormatProvider format)
        {
          format ??= CultureInfo.InvariantCulture;

          AssertionExtensions.Should(() => string.Empty.ToDateTime(format)).ThrowExactly<FormatException>();
          AssertionExtensions.Should(() => "invalid".ToDateTime(format)).ThrowExactly<FormatException>();

          $" {date.ToUniversalTime().ToString("o", format)} ".ToDateTime(format).Should().Be(date.ToUniversalTime());
        }
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
        {
          Validate(date, null);
          CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Validate(date, culture));
        }
      }

      static void Validate(DateTime date, IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToDateTimeOffset(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
        {
          Validate(date, null);
          CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Validate(date, culture));
        }
      }

      static void Validate(DateTimeOffset date, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToDateTimeOffset(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDateTimeOffset(format)).ThrowExactly<FormatException>();

        $" {date.ToString("o", format)} ".ToDateTimeOffset(format).Should().Be(date);
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        foreach (var date in new[] { DateTimeOffset.MinValue, DateTimeOffset.MaxValue, DateTimeOffset.Now, DateTimeOffset.UtcNow })
        {
          Validate(date, null);
          CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Validate(date, culture));
        }
      }

      static void Validate(DateTimeOffset date, IFormatProvider format)
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
  ///     <item><description><see cref="StringExtensions.ToFile(string)"/></description></item>
  ///     <item><description><see cref="StringExtensions.ToFile(string, out FileInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToFile(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(string.Empty.ToFile).ThrowExactly<ArgumentException>();

      var name = Path.GetTempFileName();

      name.ToFile().Should().NotBeSameAs(name.ToFile());

      name.ToFile().TryFinallyDelete(file =>
      {
        file.Exists.Should().BeTrue();
        file.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        file.FullName.Should().Be(name);
      });

      name = Attributes.RandomName();
      name.ToFile().TryFinallyDelete(file =>
      {
        file.Exists.Should().BeTrue();
        file.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        file.FullName.Should().Be(Path.Combine(Directory.GetCurrentDirectory(), name));
      });
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

      name = Attributes.RandomName();
      name.ToFile(out file).Should().BeFalse();
      file.TryFinallyDelete(info =>
      {
        info.Exists.Should().BeTrue();
        info.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
        info.FullName.Should().Be(Path.Combine(Directory.GetCurrentDirectory(), name));
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
  public void ToDirectory_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToDirectory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(string.Empty.ToDirectory).ThrowExactly<ArgumentException>();

      var name = Environment.SystemDirectory;

      name.ToDirectory().Should().NotBeSameAs(name.ToDirectory());

      var directory = name.ToDirectory();
      directory.Exists.Should().BeTrue();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(name);

      name = Attributes.RandomName();
      directory = name.ToDirectory();
      directory.Exists.Should().BeFalse();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(Path.Combine(Directory.GetCurrentDirectory(), name));
    }

    using (new AssertionScope())
    {
      var name = Environment.SystemDirectory;
      name.ToDirectory(out var directory).Should().BeTrue();
      directory.Exists.Should().BeTrue();
      directory.CreationTimeUtc.Should().BeBefore(DateTime.UtcNow).And.BeAfter(DateTime.MinValue);
      directory.FullName.Should().Be(name);

      name = Attributes.RandomName();
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
  public void ToPath_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToPath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

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
  public void ToIpAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => StringExtensions.ToIpAddress(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
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
      StringExtensions.ToIpAddress(null, out var result).Should().BeFalse();
      result.Should().BeNull();

      string.Empty.ToIpAddress(out result).Should().BeFalse();
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
  public void ToIpHost_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToIpHost(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToRegex(string, RegexOptions)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToRegex_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToRegex(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

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
  public void ToStringBuilder_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToStringBuilder(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    var builder = string.Empty.ToStringBuilder();
    builder.Should().NotBeNull().And.NotBeSameAs(string.Empty.ToStringBuilder());
    builder.ToString().Should().BeEmpty();
    builder.Capacity.Should().Be(16);
    builder.MaxCapacity.Should().Be(int.MaxValue);
    builder.Length.Should().Be(0);

    var value = Attributes.RandomString();
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
  public void ToStringReader_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToStringReader(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    var reader = string.Empty.ToStringReader();
    reader.Should().NotBeNull().And.NotBeSameAs(string.Empty.ToStringReader());
    reader.Peek().Should().Be(-1);
    reader.ReadToEnd().Should().BeEmpty();

    var value = Attributes.RandomString();
    reader = value.ToStringReader();
    reader.Should().NotBeNull().And.NotBeSameAs(value.ToStringReader());
    reader.ReadToEnd().Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXmlReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlReader_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXmlReader()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXmlDictionaryReader(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryReader_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXmlDictionaryReader()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXmlDocument(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDocument_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXmlDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    /*new XmlDocument().Text().Should().BeEmpty();

    var document = new XmlDocument();
    var element = document.CreateElement("article");
    element.SetAttribute("id", "1");
    element.InnerText = "Text";
    document.AppendChild(element);
    document.Text().Should().Be("<?xml version=\"1.0\" encoding=\"utf-16\"?><article id=\"1\">Text</article>");*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXDocument(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocument_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXDocument()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToXDocumentAsync(string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXDocumentAsync_Method()
  {
    AssertionExtensions.Should(() => ((string) null).ToXDocumentAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.ToXDocumentAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.ToProcess(string, ProcessStartInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToProcess_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.ToProcess(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    foreach (var command in new[] { string.Empty, Attributes.ShellCommand() })
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
      process.StartInfo.WindowStyle.Should().Be(ProcessWindowStyle.Normal);
      process.StartInfo.WorkingDirectory.Should().BeEmpty();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, TextWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_TextWriter_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Stream.Null.ToStreamWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo((TextWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, TextWriter, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_TextWriter_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Stream.Null.ToStreamWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync((TextWriter) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Stream.Null.ToStreamWriter(), Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, BinaryWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_BinaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((string) null).WriteTo(Stream.Null.ToBinaryWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => string.Empty.WriteTo((BinaryWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

      Validate(Stream.Null.ToBinaryWriter(), string.Empty);
      Validate(Attributes.EmptyStream().ToBinaryWriter(), Attributes.RandomString());
    }

    return;

    static void Validate(BinaryWriter writer, string text)
    {
      using (writer)
      {
        text.WriteTo(writer).Should().NotBeNull().And.BeSameAs(text);

        using (var reader = writer.BaseStream.MoveToStart().ToBinaryReader())
        {
          reader.ToText().Should().Be(text);
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
    AssertionExtensions.Should(() => StringExtensions.WriteTo(null, Stream.Null.ToXmlWriter())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo((XmlWriter) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_XmlWriter_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.WriteToAsync(null, Stream.Null.ToXmlWriter())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, FileInfo, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Attributes.RandomFakeFile())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo((FileInfo) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, FileInfo, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_FileInfo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Attributes.RandomFakeFile())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Attributes.RandomFakeFile(), null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_Process_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo((Process) null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_Process_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync((Process) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    //AssertionExtensions.Should(() => string.Empty.WriteToAsync(ShellProcess)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, Uri, Encoding, TimeSpan?, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_Uri_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Attributes.LocalHost())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo((Uri) null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, Uri, Encoding, TimeSpan?, CancellationToken, ValueTuple{string, object}[])"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_Uri_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Attributes.LocalHost())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync((Uri) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Attributes.LocalHost(), null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, HttpClient, Uri)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_HttpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Attributes.Http(), Attributes.LocalHost())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo(null, Attributes.LocalHost())).ThrowExactly<ArgumentNullException>().WithParameterName("http");
    AssertionExtensions.Should(() => string.Empty.WriteTo(Attributes.Http(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("uri");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, HttpClient, Uri, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_HttpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Attributes.Http(), Attributes.LocalHost())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(null, Attributes.LocalHost())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("http").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Attributes.Http(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("uri").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Attributes.Http(), Attributes.LocalHost(), Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_TcpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Attributes.Tcp())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo(Attributes.Tcp())).ThrowExactly<ArgumentNullException>().WithParameterName("tcp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, TcpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_TcpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Attributes.Tcp())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Attributes.Tcp())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("tcp").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Attributes.Tcp(), null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteTo(string, UdpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTo_UdpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Attributes.Udp())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => string.Empty.WriteTo(Attributes.Udp())).ThrowExactly<ArgumentNullException>().WithParameterName("udp");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.WriteToAsync(string, UdpClient, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteToAsync_UdpClient_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Attributes.Udp())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Attributes.Udp())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("udp").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(Attributes.Udp(), null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.DeserializeAsDataContract{T}(string, Type[])"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsDataContract_Method()
  {
    AssertionExtensions.Should(() => ((string) null).DeserializeAsDataContract<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.DeserializeAsXml{T}(string, IEnumerable{Type})"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsXml_Method()
  {
    AssertionExtensions.Should(() => ((string) null).DeserializeAsXml<object>()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    /*var subject = Guid.Empty;
    subject.AsXml().AsXml<Guid>().Should().Be(subject);*/

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.Hash(string, HashAlgorithm)"/> method.</para>
  /// </summary>
  [Fact]
  public void Hash_Method()
  {
    AssertionExtensions.Should(() => string.Empty.Hash(null)).ThrowExactly<ArgumentNullException>().WithParameterName("algorithm");

    using var algorithm = MD5.Create();

    AssertionExtensions.Should(() => ((string) null).Hash(Attributes.HashAlgorithm())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    
    algorithm.Should().NotBeNull();

    string[] texts = [string.Empty, Attributes.RandomString()];

    foreach (var text in texts)
    {
      text.Hash(Attributes.HashAlgorithm()).Should().NotBeNull().And.NotBeSameAs(text.Hash(Attributes.HashAlgorithm())).And.HaveLength(algorithm.HashSize / 4).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashMd5(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashMd5_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashMd5()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = MD5.Create();
    algorithm.Should().NotBeNull();

    string[] texts = [string.Empty, Attributes.RandomString()];

    foreach (var text in texts)
    {
      text.HashMd5().Should().NotBeNull().And.NotBeSameAs(text.HashMd5()).And.HaveLength(32).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashSha1(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha1_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashSha1()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = SHA1.Create();
    algorithm.Should().NotBeNull();

    string[] texts = [string.Empty, Attributes.RandomString()];

    foreach (var text in texts)
    {
      text.HashSha1().Should().NotBeNull().And.NotBeSameAs(text.HashSha1()).And.HaveLength(40).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashSha256(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha256_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashSha256()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = SHA256.Create();
    algorithm.Should().NotBeNull();

    string[] texts = [string.Empty, Attributes.RandomString()];

    foreach (var text in texts)
    {
      text.HashSha256().Should().NotBeNull().And.NotBeSameAs(text.HashSha256()).And.HaveLength(64).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashSha384(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha384_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashSha384()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = SHA384.Create();
    algorithm.Should().NotBeNull();

    string[] texts = [string.Empty, Attributes.RandomString()];

    foreach (var text in texts)
    {
      text.HashSha384().Should().NotBeNull().And.NotBeSameAs(text.HashSha384()).And.HaveLength(96).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.HashSha512(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void HashSha512_Method()
  {
    AssertionExtensions.Should(() => ((string) null).HashSha512()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    using var algorithm = SHA512.Create();
    algorithm.Should().NotBeNull();

    string[] texts = [string.Empty, Attributes.RandomString()];

    foreach (var text in texts)
    {
      text.HashSha512().Should().NotBeNull().And.NotBeSameAs(text.HashSha512()).And.HaveLength(128).And.Be(System.Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsDateOnly(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsDateOnly_Method()
  {
    using (new AssertionScope())
    {
      Validate(null);
      Validate(CultureInfo.InvariantCulture);
    }

    return;

    static void Validate(IFormatProvider format)
    {
      format ??= CultureInfo.InvariantCulture;

      StringExtensions.IsDateOnly(null, format).Should().BeFalse();

      string.Empty.IsDateOnly(format).Should().BeFalse();

      "invalid".IsDateOnly(format).Should().BeFalse();

      $" {DateOnly.MinValue.ToString("D", format)} ".IsDateOnly(format).Should().BeTrue();
      $" {DateOnly.MaxValue.ToString("D", format)} ".IsDateOnly(format).Should().BeTrue();
      $" {DateOnly.FromDateTime(DateTime.UtcNow).ToString("D", format)} ".IsDateOnly(format).Should().BeTrue();
      $" {DateOnly.FromDateTime(DateTime.Now).ToString("D", format)} ".IsDateOnly(format).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.IsTimeOnly(string, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsTimeOnly_Method()
  {
    using (new AssertionScope())
    {
      Validate(null);
      Validate(CultureInfo.InvariantCulture);
    }

    return;

    static void Validate(IFormatProvider format)
    {
      StringExtensions.IsTimeOnly(null, format).Should().BeFalse();

      string.Empty.IsTimeOnly(format).Should().BeFalse();

      "invalid".IsTimeOnly(format).Should().BeFalse();

      $" {TimeOnly.MinValue.ToString("T", format)} ".IsTimeOnly(format).Should().BeTrue();
      $" {TimeOnly.MaxValue.ToString("T", format)} ".IsTimeOnly(format).Should().BeTrue();
      $" {TimeOnly.FromDateTime(DateTime.UtcNow).ToString("T", format)} ".IsTimeOnly(format).Should().BeTrue();
      $" {TimeOnly.FromDateTime(DateTime.Now).ToString("T", format)} ".IsTimeOnly(format).Should().BeTrue();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="StringExtensions.FromHex(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void FromHex_Method()
  {
    AssertionExtensions.Should(() => StringExtensions.FromHex(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    string.Empty.FromHex().Should().NotBeNull().And.BeSameAs(string.Empty.FromHex()).And.BeEmpty();

    var bytes = Attributes.RandomBytes();
    bytes.ToHex().Should().NotBeNull().And.NotBeSameAs(bytes.ToHex()).And.Be(System.Convert.ToHexString(bytes));
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToDateOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        foreach (var date in new[] { DateOnly.MinValue, DateOnly.MaxValue, DateTime.Now.ToDateOnly(), DateTime.UtcNow.ToDateOnly() })
        {
          Validate(date, null);
          Validate(date, CultureInfo.InvariantCulture);
        }
      }

      static void Validate(DateOnly date, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToDateOnly(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToDateOnly(format)).ThrowExactly<FormatException>();

        $" {date.ToString("D", format)} ".ToDateOnly(format).Should().Be(date);
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        foreach (var date in new[] { DateOnly.MinValue, DateOnly.MaxValue, DateTime.Now.ToDateOnly(), DateTime.UtcNow.ToDateOnly() })
        {
          Validate(date, null);
          Validate(date, CultureInfo.InvariantCulture);
        }
      }

      static void Validate(DateOnly date, IFormatProvider format)
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
      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => StringExtensions.ToTimeOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

        foreach (var time in new[] { TimeOnly.MinValue, TimeOnly.MaxValue, DateTime.Now.ToTimeOnly(), DateTime.UtcNow.ToTimeOnly() })
        {
          Validate(time, null);
          Validate(time, CultureInfo.InvariantCulture);
        }
      }

      static void Validate(TimeOnly time, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        AssertionExtensions.Should(() => string.Empty.ToTimeOnly(format)).ThrowExactly<FormatException>();
        AssertionExtensions.Should(() => "invalid".ToTimeOnly(format)).ThrowExactly<FormatException>();

        $" {time.ToString("T", format)} ".ToTimeOnly(format).Should().Be(time.TruncateToSecondStart());
      }
    }

    using (new AssertionScope())
    {
      using (new AssertionScope())
      {
        foreach (var date in new[] { TimeOnly.MinValue, TimeOnly.MaxValue, DateTime.Now.ToTimeOnly(), DateTime.UtcNow.ToTimeOnly() })
        {
          Validate(date, null);
          Validate(date, CultureInfo.InvariantCulture);
        }
      }

      static void Validate(TimeOnly time, IFormatProvider format)
      {
        format ??= CultureInfo.InvariantCulture;

        StringExtensions.ToTimeOnly(null, out var result, format).Should().BeFalse();
        result.Should().BeNull();

        string.Empty.ToTimeOnly(out _, format).Should().BeFalse();
        result.Should().BeNull();

        "invalid".ToTimeOnly(out _, format).Should().BeFalse();
        result.Should().BeNull();

        $" {time.ToString("T", format)} ".ToTimeOnly(out result, format).Should().BeTrue();
        result.Should().Be(time.TruncateToSecondStart());
      }
    }
  }
}