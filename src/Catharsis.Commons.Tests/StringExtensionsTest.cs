using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class StringExtensionsTest
  {
    [Fact]
    public void base64()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Base64(null));

      Assert.Empty(string.Empty.Base64());
      var bytes = Guid.NewGuid().ToByteArray();
      Assert.True(System.Convert.ToBase64String(bytes).Base64().SequenceEqual(bytes));
    }

    [Fact]
    public void bytes()
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

    [Fact]
    public void compare_to()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.CompareTo(null, "other", StringComparison.Ordinal));

      Assert.Equal(0, string.Empty.CompareTo(string.Empty, StringComparison.InvariantCulture));
      Assert.True("first".CompareTo("second", StringComparison.InvariantCulture) < 0);
      Assert.True("a".CompareTo("A", StringComparison.Ordinal) > 0);
      Assert.Equal(0, "a".CompareTo("A", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void hex()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Hex(null));

      Assert.Equal(0, Enumerable.Empty<byte>().ToArray().Hex().Length);
      Assert.Empty(Enumerable.Empty<byte>().ToArray().Hex().Hex());

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.True(bytes.Hex().Hex().SequenceEqual(bytes));
    }

    [Fact]
    public void html_decode()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.HtmlDecode(null));

      Assert.True(ReferenceEquals(string.Empty.HtmlDecode(), string.Empty));

      const string Encoded = "<p>5 is &lt; 10 and &gt; 1</p>";
      const string Decoded = "<p>5 is < 10 and > 1</p>";
      Assert.Equal(Decoded, Encoded.HtmlDecode());
      Assert.Equal(Decoded, Decoded.HtmlEncode().HtmlDecode());
    }

    [Fact]
    public void html_encode()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.HtmlEncode(null));

      Assert.True(ReferenceEquals(string.Empty.HtmlEncode(), string.Empty));

      const string Decoded = "<p>5 is < 10 and > 1</p>";
      const string Encoded = "<p>5 is &lt; 10 and &gt; 1</p>";

      Assert.Equal(Decoded, Encoded.HtmlDecode());
      Assert.Equal(Decoded, Decoded.HtmlEncode().HtmlDecode());
    }

    [Fact]
    public void is_empty()
    {
      Assert.True(StringExtensions.IsEmpty(null));
      Assert.True(string.Empty.IsEmpty());
      Assert.False(" ".IsEmpty());
      Assert.False(" value".IsEmpty());
      Assert.False("value".IsEmpty());
    }

    [Fact]
    public void is_match()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsMatch(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => string.Empty.IsMatch(null));

      Assert.False(string.Empty.IsMatch("anything"));
      Assert.True("ab4Zg95kf".IsMatch("[a-zA-z0-9]"));
      Assert.False("~#$%".IsMatch("[a-zA-z0-9]"));
    }

    [Fact]
    public void matches()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Matches(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => string.Empty.Matches(null));

      Assert.Empty(string.Empty.Matches("anything"));
      var matches = "ab#1".Matches("[a-zA-z0-9]");
      Assert.Equal(3, matches.Count);
      Assert.Equal("a", matches[0].Value);
      Assert.Equal("b", matches[1].Value);
      Assert.Equal("1", matches[2].Value);
    }

    [Fact]
    public void replace()
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

    [Fact]
    public void is_boolean()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsBoolean(null));

      Assert.False(string.Empty.IsBoolean());
      Assert.True("TRUE".IsBoolean());
      Assert.True("True".IsBoolean());
      Assert.True("true".IsBoolean());
      Assert.False(string.Empty.IsBoolean());
      Assert.False("value".IsBoolean());
    }

    [Fact]
    public void is_date_time()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsDateTime(null));

      Assert.False(string.Empty.IsDateTime());
      Assert.True(DateTime.MinValue.ToString().IsDateTime());
      Assert.True(DateTime.MaxValue.ToString().IsDateTime());
      Assert.False("value".IsDateTime());
    }

    [Fact]
    public void is_double()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsDouble(null));

      Assert.False(string.Empty.IsDouble());
      Assert.True("-1".IsDouble());
      Assert.True("0,0".IsDouble());
      Assert.True("1".IsDouble());
      Assert.True("+1".IsDouble());
      Assert.False("value".IsDouble());
    }

    [Fact]
    public void is_guid()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsGuid(null));
      
      Assert.False(string.Empty.IsGuid());
      Assert.True(Guid.NewGuid().ToString().IsGuid());
      Assert.True("{C126A89D-5C9D-461F-839A-25E4D265424C}".IsGuid());
      Assert.True("C126A89D-5C9D-461F-839A-25E4D265424C".IsGuid());
      Assert.False("value".IsGuid());
    }

    [Fact]
    public void to_guid()
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

    [Fact]
    public void is_integer()
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

    [Fact]
    public void is_uri()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.IsUri(null));

      Assert.True(string.Empty.IsUri());
      Assert.True("http://localhost:8080?param=value".IsUri());
      Assert.True("scheme://127.0.0.1".IsUri());
      Assert.True("path".IsUri());
      Assert.False("http://".IsUri());
    }

    [Fact]
    public void to_boolean()
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

    [Fact]
    public void to_byte()
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

    [Fact]
    public void to_date_time()
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

    [Fact]
    public void to_decimal()
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

    [Fact]
    public void to_double()
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

    [Fact]
    public void to_enum()
    {
      Assert.Throws<ArgumentException>(() => string.Empty.ToEnum<DateTime>());
      Assert.Throws<ArgumentException>(() => string.Empty.ToEnum<MockEnumeration>());
      Assert.Throws<ArgumentException>(() => "Invalid".ToEnum<MockEnumeration>());
      Assert.Equal(MockEnumeration.First, MockEnumeration.First.ToString().ToEnum<MockEnumeration>());
    }
    
    [Fact]
    public void to_int16()
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

    [Fact]
    public void to_int32()
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
    
    [Fact]
    public void to_int64()
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

    [Fact]
    public void to_regex()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToRegex(null));

      const string Pattern = "pattern";
      Assert.Equal(Pattern, Pattern.ToRegex().ToString());
    }

    [Fact]
    public void to_single()
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

    [Fact]
    public void to_uri()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToUri(null));
      Assert.Throws<ArgumentException>(() => string.Empty.ToUri());

      const string Invalid = "invalid";
      const string Uri = "http://yandex.ru";

      Assert.Equal(new Uri(Uri), Uri.ToUri());
      Assert.Throws<UriFormatException>(() => Invalid.ToUri());
    }

    [Fact]
    public void whitespace()
    {
      Assert.True(StringExtensions.Whitespace(null));
      Assert.True(string.Empty.Whitespace());
      Assert.True(" ".Whitespace());
      Assert.False(" a ".Whitespace());
    }

    [Fact]
    public void lines()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Lines(null));

      Assert.Empty(string.Empty.Lines());
      Assert.True("value".Lines().SequenceEqual(new[] { "value" }));
      Assert.True($"first{Environment.NewLine}second".Lines().SequenceEqual(new[] { "first", "second" }));
    }

    [Fact]
    public void prepend()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Prepend(null, "other"));

      Assert.Equal(string.Empty, string.Empty.Prepend(null));
      Assert.Equal(string.Empty, string.Empty.Prepend(string.Empty));
      Assert.Equal("value", "value".Prepend(string.Empty));
      Assert.Equal("first,second", "second".Prepend("first,"));
    }

    [Fact]
    public void swap_case()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.SwapCase(null));

      Assert.Equal(string.Empty, string.Empty.SwapCase());
      Assert.Equal("vAlUe", "VaLuE".SwapCase());
      //Assert.Equal("ÁÕ‡◊ÂÕË≈", "«Ì¿˜≈Ì»Â".SwapCase());
    }

    [Fact]
    public void drop()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Drop(null, 0));
      Assert.Throws<ArgumentOutOfRangeException>(() => string.Empty.Drop(1));
      
      Assert.Equal(string.Empty, string.Empty.Drop(0));
      Assert.Equal("value", "value".Drop(0));
      Assert.Equal("alue", "value".Drop(1));
      Assert.Equal(string.Empty, "value".Drop(5));
    }

    [Fact]
    public void multiply()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Multiply(null, 0));

      Assert.Equal(string.Empty, string.Empty.Multiply(1));
      Assert.Equal(string.Empty, "1".Multiply(-1));
      Assert.Equal(string.Empty, "1".Multiply(0));
      Assert.Equal("1", "1".Multiply(1));
      Assert.Equal("11", "1".Multiply(2));
    }

    [Fact]
    public void url_decode()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.UrlDecode(null));

      Assert.Equal(string.Empty, string.Empty.UrlDecode());
      Assert.Equal("#value?", "%23value%3F".UrlDecode());
    }

    [Fact]
    public void url_encode()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.UrlEncode(null));

      Assert.Equal(string.Empty, string.Empty.UrlEncode());
      Assert.Equal("%23value%3F", "#value?".UrlEncode());
    }
  }

  internal enum MockEnumeration
  {
    First,

    Second,

    Third
  }
}