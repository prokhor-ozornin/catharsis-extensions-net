using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="StringExtensions"/>.</para>
  /// </summary>
  public sealed class StringExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Base64(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Base64_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Base64(null));

      Assert.Equal(0, string.Empty.Base64().Length);
      var bytes = Guid.NewGuid().ToByteArray();
      Assert.True(System.Convert.ToBase64String(bytes).Base64().SequenceEqual(bytes));
    }

    /// <summary>
    ///   <para>Performs testing of <se cref="StringExtensions.Bytes(String, Encoding, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Bytes(null));

      var text = Guid.NewGuid().ToString();

      Assert.Equal(text.Bytes(Encoding.UTF32).Length, text.Bytes(Encoding.Unicode).Length * 2);
      Assert.Equal(text.Bytes(Encoding.UTF32, false).Length, text.Bytes(Encoding.Unicode, false).Length * 2);
      
      Assert.NotEqual(text, text.Bytes().String());
      Assert.Equal(text, text.Bytes(null, false).String());
      Assert.NotEqual(text, text.Bytes(Encoding.Unicode).String(Encoding.Unicode));
      Assert.Equal(text, text.Bytes(Encoding.Unicode, false).String(Encoding.Unicode));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.CompareTo(string, string, StringComparison)"/></description></item>
    ///     <item><description><see cref="StringExtensions.CompareTo(string, string, CompareOptions, CultureInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void CompareTo_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.CompareTo(null, "other", StringComparison.Ordinal));
      Assert.Throws<ArgumentNullException>(() => StringExtensions.CompareTo(null, "other", CompareOptions.None, CultureInfo.InvariantCulture));

      Assert.Equal(0, string.Empty.CompareTo(string.Empty, StringComparison.InvariantCulture));
      Assert.True("first".CompareTo("second", StringComparison.InvariantCulture) < 0);
      Assert.True("a".CompareTo("A", StringComparison.Ordinal) > 0);
      Assert.Equal(0, "a".CompareTo("A", StringComparison.OrdinalIgnoreCase));

      Assert.Equal(0, string.Empty.CompareTo(string.Empty, CompareOptions.None));
      Assert.True("first".CompareTo("second", CompareOptions.None) < 0);
      Assert.True("a".CompareTo("A", CompareOptions.None) < 0);
      Assert.True("a".CompareTo("A", CompareOptions.Ordinal) > 0);
      Assert.Equal(0, "a".CompareTo("A", CompareOptions.OrdinalIgnoreCase));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.FormatSelf(string, object[])"/> method.</para>
    /// </summary>
    [Fact]
    public void FormatSelf_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.FormatSelf(null));

      Assert.Equal(string.Empty, string.Empty.FormatSelf());

      const string Text = "{0} is lesser than {1}";
      Assert.Equal(string.Format(CultureInfo.CurrentCulture, Text, 15.5, true), Text.FormatSelf(15.5, true));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.FormatInvariant(string, object[])"/> method.</para>
    /// </summary>
    [Fact]
    public void FormatInvariant_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.FormatInvariant(null));

      Assert.Equal(string.Empty, string.Empty.FormatInvariant());

      const string Text = "{0} and {1}";
      Assert.Equal(string.Format(CultureInfo.InvariantCulture, Text, 15.5, true), Text.FormatInvariant(15.5, true));
      Assert.NotEqual(string.Format(CultureInfo.GetCultureInfo("ru"), Text, 15.5, true), Text.FormatInvariant(15.5, true));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Hex(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Hex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Hex(null));

      Assert.Equal(0, Enumerable.Empty<byte>().ToArray().Hex().Length);
      Assert.Equal(0, Enumerable.Empty<byte>().ToArray().Hex().Hex().Length);

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.True(bytes.Hex().Hex().SequenceEqual(bytes));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringHtmlExtensions.HtmlDecode(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void HtmlDecode_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringHtmlExtensions.HtmlDecode(null));

      Assert.True(ReferenceEquals(string.Empty.HtmlDecode(), string.Empty));

      const string Encoded = "<p>5 is &lt; 10 and &gt; 1</p>";
      const string Decoded = "<p>5 is < 10 and > 1</p>";
      Assert.Equal(Decoded, Encoded.HtmlDecode());
      Assert.Equal(Decoded, Decoded.HtmlEncode().HtmlDecode());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringHtmlExtensions.HtmlEncode(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void HtmlEncode_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringHtmlExtensions.HtmlEncode(null));

      Assert.True(ReferenceEquals(string.Empty.HtmlEncode(), string.Empty));

      const string Decoded = "<p>5 is < 10 and > 1</p>";
      const string Encoded = "<p>5 is &lt; 10 and &gt; 1</p>";

      Assert.Equal(Decoded, Encoded.HtmlDecode());
      Assert.Equal(Decoded, Decoded.HtmlEncode().HtmlDecode());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.IsEmpty(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsEmpty()
    {
      Assert.True(StringExtensions.IsEmpty(null));
      Assert.True(string.Empty.IsEmpty());
      Assert.False(" ".IsEmpty());
      Assert.False(" value".IsEmpty());
      Assert.False("value".IsEmpty());
    }

    /// <summary>
    ///   <para>Performs testing of <se cref="StringExtensions.IsMatch(string, string, RegexOptions?)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsMatch_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsMatch(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => string.Empty.IsMatch(null));

      Assert.False(string.Empty.IsMatch("anything"));
      Assert.True("ab4Zg95kf".IsMatch("[a-zA-z0-9]"));
      Assert.False("~#$%".IsMatch("[a-zA-z0-9]"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Matches(string, string, RegexOptions?)"/> method.</para>
    /// </summary>
    [Fact]
    public void Matches_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Matches(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => string.Empty.Matches(null));

      Assert.Equal(0, string.Empty.Matches("anything").Count);
      var matches = "ab#1".Matches("[a-zA-z0-9]");
      Assert.Equal(3, matches.Count);
      Assert.Equal("a", matches[0].Value);
      Assert.Equal("b", matches[1].Value);
      Assert.Equal("1", matches[2].Value);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.Replace(string, IDictionary{string, string})"/></description></item>
    ///     <item><description><see cref="StringExtensions.Replace(string, IEnumerable{string}, IEnumerable{string})"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Replace_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Replace(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => "where".Replace(null));
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Replace(null, Enumerable.Empty<string>().ToList(), Enumerable.Empty<string>().ToList()));
      Assert.Throws<ArgumentNullException>(() => "where".Replace(null, Enumerable.Empty<string>().ToList()));
      Assert.Throws<ArgumentNullException>(() => "where".Replace(Enumerable.Empty<string>().ToList(), null));
      Assert.Throws<ArgumentException>(() => "where".Replace(new [] { "first" }.ToList(), new [] { "second", "third" }.ToList()));
      Assert.Throws<ArgumentNullException>(() => "where".Replace(null, Enumerable.Empty<string>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => "where".Replace(Enumerable.Empty<string>().ToArray(), null));
      Assert.Throws<ArgumentException>(() => "where".Replace(new [] { "first" }, new [] { "second", "third" }));
      
      const string Original = "The quick brown fox jumped over the lazy dog";
      const string Replaced = "The slow hazy fox jumped over the lazy bear";

      Assert.Equal(Replaced, Original.Replace(new Dictionary<string, string> { { "quick", "slow" }, { "dog", "bear" }, { "nothing", string.Empty }, { "brown", "hazy" } }));
      Assert.Equal(Replaced, Original.Replace(new[] { "quick", "dog", "nothing", "brown" }.ToList(), new[] { "slow", "bear", string.Empty, "hazy" }.ToList()));
      Assert.Equal(Replaced, Original.Replace(new[] { "quick", "dog", "nothing", "brown" }, new[] { "slow", "bear", string.Empty, "hazy" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringCryptographyExtensions.Secure(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Secure_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringCryptographyExtensions.Secure(null));

      using (var value = string.Empty.Secure())
      {
        Assert.Equal(0, value.Length);
      }
      var text = Guid.NewGuid().ToString();
      using (var value = text.Secure())
      {
        Assert.Equal(text.Length, value.Length);
        Assert.False(ReferenceEquals(value.ToString(), text));
      }
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.IsBoolean(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsBoolean_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsBoolean(null));

      Assert.False(string.Empty.IsBoolean());
      Assert.True("TRUE".IsBoolean());
      Assert.True("True".IsBoolean());
      Assert.True("true".IsBoolean());
      Assert.False(string.Empty.IsBoolean());
      Assert.False("value".IsBoolean());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.IsDateTime(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsDateTime_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsDateTime(null));

      Assert.False(string.Empty.IsDateTime());
      Assert.True(DateTime.MinValue.ToString().IsDateTime());
      Assert.True(DateTime.MaxValue.ToString().IsDateTime());
      Assert.False("value".IsDateTime());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.IsDouble(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsDouble_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsDouble(null));

      Assert.False(string.Empty.IsDouble());
      Assert.True("-1".IsDouble());
      Assert.True("0,0".IsDouble());
      Assert.True("1".IsDouble());
      Assert.True("+1".IsDouble());
      Assert.False("value".IsDouble());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.IsGuid(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsGuid_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsGuid(null));
      
      Assert.False(string.Empty.IsGuid());
      Assert.True(Guid.NewGuid().ToString().IsGuid());
      Assert.True("{C126A89D-5C9D-461F-839A-25E4D265424C}".IsGuid());
      Assert.True("C126A89D-5C9D-461F-839A-25E4D265424C".IsGuid());
      Assert.False("value".IsGuid());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.IsInteger(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsInteger_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsInteger(null));

      Assert.False(string.Empty.IsInteger());
      Assert.True(long.MinValue.ToString(CultureInfo.InvariantCulture).IsInteger());
      Assert.True("-1".IsInteger());
      Assert.True("0".IsInteger());
      Assert.True("1".IsInteger());
      Assert.True("+1".IsInteger());
      Assert.True(long.MaxValue.ToString(CultureInfo.InvariantCulture).IsInteger());
      Assert.False("value".IsInteger());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.IsIpAddress(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsIpAddress_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsIpAddress(null));

      Assert.False(string.Empty.IsIpAddress());
      Assert.True("127.0.0.1".IsIpAddress());
      Assert.False("127,0,0,1".IsIpAddress());
      Assert.False("localhost".IsIpAddress());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.IsUri(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsUri_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsUri(null));

      Assert.True(string.Empty.IsUri());
      Assert.True("http://localhost:8080?param=value".IsUri());
      Assert.True("scheme://127.0.0.1".IsUri());
      Assert.True("path".IsUri());
      Assert.False("http://".IsUri());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToBoolean(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToBoolean(string, out bool)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToBoolean_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToBoolean(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToBoolean());

      const string Invalid = "invalid";

      Assert.False(bool.FalseString.ToBoolean());
      Assert.True(bool.TrueString.ToBoolean());
      Assert.Throws<FormatException>(() => Invalid.ToBoolean());
      
      bool result;
      Assert.True(bool.TrueString.ToBoolean(out result));
      Assert.True(result);
      Assert.True(bool.FalseString.ToBoolean(out result));
      Assert.False(result);
      Assert.False(Invalid.ToBoolean(out result));
      Assert.False(result);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToByte(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToByte(string, out byte)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToByte_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToByte(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToByte());

      const string Invalid = "invalid";

      Assert.Equal(byte.MaxValue, byte.MaxValue.ToString(CultureInfo.InvariantCulture).ToByte());
      Assert.Throws<FormatException>(() => Invalid.ToByte());

      byte result;
      Assert.True(byte.MaxValue.ToString(CultureInfo.InvariantCulture).ToByte(out result));
      Assert.Equal(byte.MaxValue, result);
      Assert.False(Invalid.ToByte(out result));
      Assert.Equal(default(byte), result);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToDateTime(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToDateTime(string, out DateTime)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToDateTime_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToDateTime(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToDateTime());

      const string Invalid = "invalid";
      var date = DateTime.UtcNow;

      Assert.Throws<FormatException>(() => Invalid.ToDateTime());

      DateTime result;
      Assert.True(date.ToString().ToDateTime(out result));
      Assert.True(result.IsSameDate(date));
      Assert.True(result.IsSameTime(date));
      Assert.False(Invalid.ToDateTime(out result));
      Assert.Equal(default(DateTime), result);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToDecimal(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToDecimal(string, out decimal)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToDecimal_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToDecimal(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToDecimal());

      const string Invalid = "invalid";

      Assert.Equal(decimal.MaxValue, decimal.MaxValue.ToString(CultureInfo.InvariantCulture).ToDecimal());
      Assert.Throws<FormatException>(() => Invalid.ToDecimal());

      decimal result;
      Assert.True(decimal.MaxValue.ToString(CultureInfo.InvariantCulture).ToDecimal(out result));
      Assert.Equal(decimal.MaxValue, result);
      Assert.False(Invalid.ToDecimal(out result));
      Assert.Equal(default(decimal), result);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToDouble(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToDouble(string, out double)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToDouble_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToDouble(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToDouble());

      const string Invalid = "invalid";

      Assert.Equal(double.Epsilon, double.Epsilon.ToString().ToDouble());
      Assert.Throws<FormatException>(() => Invalid.ToDouble());

      double result;
      Assert.True(double.Epsilon.ToString().ToDouble(out result));
      Assert.Equal(double.Epsilon, result);
      Assert.False(Invalid.ToDouble(out result));
      Assert.Equal(default(double), result);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.ToEnum{T}(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToEnum_Method()
    {
      Assert.Throws<ArgumentException>(() => string.Empty.ToEnum<DateTime>());
      Assert.Throws<ArgumentException>(() => string.Empty.ToEnum<MockEnumeration>());
      Assert.Throws<ArgumentException>(() => "Invalid".ToEnum<MockEnumeration>());
      Assert.Equal(MockEnumeration.First, MockEnumeration.First.ToString().ToEnum<MockEnumeration>());
    }
    
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToGuid(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToGuid(string, out Guid)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToGuid_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToGuid(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToGuid());

      const string Invalid = "invalid";

      Assert.Equal(Guid.Empty, Guid.Empty.ToString().ToGuid());
      Assert.Throws<FormatException>(() => Invalid.ToGuid());

      Guid result;
      Assert.True(Guid.Empty.ToString().ToGuid(out result));
      Assert.Equal(Guid.Empty, result);
      Assert.False(Invalid.ToGuid(out result));
      Assert.Equal(default(Guid), result);
    }
    
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToInt16(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToInt16(string, out short)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToInt16_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToInt16(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToInt16());

      const string Invalid = "invalid";
      Assert.Equal(short.MaxValue, short.MaxValue.ToString(CultureInfo.InvariantCulture).ToInt16());
      Assert.Throws<FormatException>(() => Invalid.ToInt16());

      short result;
      Assert.True(short.MaxValue.ToString(CultureInfo.InvariantCulture).ToInt16(out result));
      Assert.Equal(short.MaxValue, result);
      Assert.False(Invalid.ToInt16(out result));
      Assert.Equal(default(short), result);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToInt32(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToInt32(string, out int)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToInt32_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToInt32(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToInt32());

      const string Invalid = "invalid";

      Assert.Equal(int.MaxValue, int.MaxValue.ToString(CultureInfo.InvariantCulture).ToInt32());
      Assert.Throws<FormatException>(() => Invalid.ToInt32());
      
      int result;
      Assert.True(int.MaxValue.ToString(CultureInfo.InvariantCulture).ToInt32(out result));
      Assert.Equal(int.MaxValue, result);
      Assert.False(Invalid.ToInt32(out result));
      Assert.Equal(default(int), result);
    }
    
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToInt64(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToInt64(string, out long)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToInt64_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToInt64(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToInt64());

      const string Invalid = "invalid";

      Assert.Equal(long.MaxValue, long.MaxValue.ToString(CultureInfo.InvariantCulture).ToInt64());
      Assert.Throws<FormatException>(() => Invalid.ToInt64());

      long result;
      Assert.True(long.MaxValue.ToString(CultureInfo.InvariantCulture).ToInt64(out result));
      Assert.Equal(long.MaxValue, result);
      Assert.False(Invalid.ToInt64(out result));
      Assert.Equal(default(long), result);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.ToIpAddress(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToIpAddress_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToIpAddress(null));

      const string Invalid = "invalid";

      Assert.Equal(IPAddress.Loopback, IPAddress.Loopback.ToString().ToIpAddress());
      Assert.Throws<FormatException>(() => Invalid.ToIpAddress());

      IPAddress result;
      Assert.True(IPAddress.Loopback.ToString().ToIpAddress(out result));
      Assert.Equal(IPAddress.Loopback, result);
      Assert.False(Invalid.ToIpAddress(out result));
      Assert.Null(result);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.ToRegex(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToRegex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToRegex(null));

      const string Pattern = "pattern";
      Assert.Equal(Pattern, Pattern.ToRegex().ToString());
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.ToSingle(string)"/></description></item>
    ///     <item><description><see cref="StringExtensions.ToSingle(string, out Single)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void ToSingle_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToSingle(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToSingle());

      const string Invalid = "invalid";

      Assert.Equal(Single.Epsilon, Single.Epsilon.ToString().ToSingle());
      Assert.Throws<FormatException>(() => Invalid.ToSingle());

      Single result;
      Assert.True(Single.Epsilon.ToString().ToSingle(out result));
      Assert.Equal(Single.Epsilon, result);
      Assert.False(Invalid.ToSingle(out result));
      Assert.Equal(default(Single), result);
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
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToUri(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToUri());

      const string Invalid = "invalid";
      const string Uri = "http://yandex.ru";

      Assert.Equal(new Uri(Uri), Uri.ToUri());
      Assert.Throws<UriFormatException>(() => Invalid.ToUri());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Tokenize(string, IDictionary{string, object}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Tokenize_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Tokenize(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => string.Empty.Tokenize(null));
      Assert.Throws<ArgumentNullException>(() => string.Empty.Tokenize(new Dictionary<string, object>(), null));
      Assert.Throws<ArgumentException>(() => string.Empty.Tokenize(new Dictionary<string, object>(), string.Empty));

      const string Original = "The :quick :brown fox jumped over the lazy :dog";
      const string Replaced = "The slow hazy fox jumped over the lazy bear";

      Assert.Equal(Replaced, Original.Tokenize(new Dictionary<string, object> { { "quick", "slow" }, { "dog", "bear" }, { "nothing", string.Empty }, { "brown", "hazy" } }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Whitespace(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Whitespace_Method()
    {
      Assert.True(StringExtensions.Whitespace(null));
      Assert.True(string.Empty.Whitespace());
      Assert.True(" ".Whitespace());
      Assert.False(" a ".Whitespace());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringXmlExtensions.AsXml{T}(string, Type[])"/> method.</para>
    /// </summary>
    [Fact]
    public void AsXml_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringXmlExtensions.AsXml<object>(null));
      Assert.Throws<ArgumentException>(() => string.Empty.AsXml<object>());

      var subject = Guid.Empty;
      Assert.Equal(subject, subject.ToXml().AsXml<Guid>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtendedExtensions.Capitalize(string, CultureInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void Capitalize_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtendedExtensions.Capitalize(null));

      Assert.Equal(string.Empty, string.Empty.Capitalize());
      Assert.Equal("Capitalized String", "capitalized stRing".Capitalize());
      Assert.Equal("Capitalized String", "capitalized stRing".Capitalize(CultureInfo.CurrentCulture));
      Assert.Equal("CAPITALIZED STRING", "CAPITALIZED STRING".Capitalize());
      Assert.Equal("«‡„Î‡‚Ì‡ˇ —ÚÓÍ‡", "Á‡„Î‡‚Ì‡ˇ ÒÚ–ÓÍ‡".Capitalize());
      Assert.Equal("«‡„Î‡‚Ì‡ˇ —ÚÓÍ‡", "Á‡„Î‡‚Ì‡ˇ ÒÚ–ÓÍ‡".Capitalize(CultureInfo.CurrentCulture));
      Assert.Equal("«¿√À¿¬Õ¿ﬂ —“–Œ ¿", "«¿√À¿¬Õ¿ﬂ —“–Œ ¿".Capitalize());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Lines(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Lines_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Lines(null));

      Assert.False(string.Empty.Lines().Any());
      Assert.True("value".Lines().SequenceEqual(new[] { "value" }));
      Assert.True("first{0}second".FormatSelf(Environment.NewLine).Lines().SequenceEqual(new[] { "first", "second" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Prepend(string, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Prepend_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Prepend(null, "other"));

      Assert.Equal(string.Empty, string.Empty.Prepend(null));
      Assert.Equal(string.Empty, string.Empty.Prepend(string.Empty));
      Assert.Equal("value", "value".Prepend(string.Empty));
      Assert.Equal("first,second", "second".Prepend("first,"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.SwapCase(string, CultureInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void SwapCase_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.SwapCase(null));

      Assert.Equal(string.Empty, string.Empty.SwapCase());
      Assert.Equal("vAlUe", "VaLuE".SwapCase());
      Assert.Equal("vAlUe", "VaLuE".SwapCase(CultureInfo.CurrentCulture));
      Assert.Equal("ÁÕ‡◊ÂÕË≈", "«Ì¿˜≈Ì»Â".SwapCase());
      Assert.Equal("ÁÕ‡◊ÂÕË≈", "«Ì¿˜≈Ì»Â".SwapCase(CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Drop(string, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void Drop_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Drop(null, 0));
      Assert.Throws<ArgumentOutOfRangeException>(() => string.Empty.Drop(1));
      
      Assert.Equal(string.Empty, string.Empty.Drop(0));
      Assert.Equal("value", "value".Drop(0));
      Assert.Equal("alue", "value".Drop(1));
      Assert.Equal(string.Empty, "value".Drop(5));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.Multiply(string, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void Multiply_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Multiply(null, 0));

      Assert.Equal(string.Empty, string.Empty.Multiply(1));
      Assert.Equal(string.Empty, "1".Multiply(-1));
      Assert.Equal(string.Empty, "1".Multiply(0));
      Assert.Equal("1", "1".Multiply(1));
      Assert.Equal("11", "1".Multiply(2));
    }
  }

  internal enum MockEnumeration
  {
    First,

    Second,

    Third
  }
}