using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;


namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="StringExtensions"/>.</para>
  /// </summary>
  public sealed class StringExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <se cref="StringExtensions.Bytes(String, Encoding, bool)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Bytes(null));

      var text = Guid.NewGuid().ToString();

      Assert.True(text.Bytes(Encoding.Unicode).Length * 2 == text.Bytes(Encoding.UTF32).Length);
      Assert.True(text.Bytes(Encoding.Unicode, false).Length * 2 == text.Bytes(Encoding.UTF32, false).Length);
      
      Assert.True(text.Bytes().String() != text);
      Assert.True(text.Bytes(null, false).String() == text);
      Assert.True(text.Bytes(Encoding.Unicode).String(Encoding.Unicode) != text);
      Assert.True(text.Bytes(Encoding.Unicode, false).String(Encoding.Unicode) == text);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.Compare(string, string, StringComparison)"/></description></item>
    ///     <item><description><see cref="StringExtensions.Compare(string, string, CultureInfo, CompareOptions)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Compare_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Compare(null, "other", StringComparison.Ordinal));
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Compare(null, "other", CultureInfo.InvariantCulture));

      throw new NotImplementedException();
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.DecodeBase64(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void DecodeBase64_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.DecodeBase64(null));

      Assert.True(string.Empty.DecodeBase64().Length == 0);
      var bytes = Guid.NewGuid().ToByteArray();
      Assert.True(Convert.ToBase64String(bytes).DecodeBase64().SequenceEqual(bytes));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.DecodeHex(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void DecodeHex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.DecodeHex(null));

      Assert.True(Enumerable.Empty<byte>().ToArray().EncodeHex().Length == 0);
      Assert.True(Enumerable.Empty<byte>().ToArray().EncodeHex().DecodeHex().Length == 0);

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.True(bytes.EncodeHex().DecodeHex().SequenceEqual(bytes));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtendedExtensions.DecodeHtml(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void DecodeHtml_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtendedExtensions.DecodeHtml(null));

      Assert.True(ReferenceEquals(string.Empty.DecodeHtml(), string.Empty));
      
      const string Encoded = "<p>5 is &lt; 10 and &gt; 1</p>";
      const string Decoded = "<p>5 is < 10 and > 1</p>";
      Assert.True(Encoded.DecodeHtml() == Decoded);
      Assert.True(Decoded.EncodeHtml().DecodeHtml() == Decoded);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.EachLine(string, Action{string})"/> method.</para>
    /// </summary>
    [Fact]
    public void EachLine_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.EachLine(null, s => { }));
      Assert.Throws<ArgumentNullException>(() => string.Empty.EachLine(null));

      var text = "First{0}Second{0}Third{0}".FormatValue(Environment.NewLine);
      var list = new List<string>();
      text.EachLine(list.Add);
      Assert.True(list.Count == 4);
      Assert.True(list[0] == "First");
      Assert.True(list[1] == "Second");
      Assert.True(list[2] == "Third");
      Assert.True(list[3] == string.Empty);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtendedExtensions.EncodeHtml(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void EncodeHtml_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtendedExtensions.EncodeHtml(null));

      Assert.True(ReferenceEquals(string.Empty.EncodeHtml(), string.Empty));

      const string Decoded = "<p>5 is < 10 and > 1</p>";
      const string Encoded = "<p>5 is &lt; 10 and &gt; 1</p>";

      Assert.True(Encoded.DecodeHtml() == Decoded);
      Assert.True(Decoded.EncodeHtml().DecodeHtml() == Decoded);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.FormatValue(string, object[])"/> method.</para>
    /// </summary>
    [Fact]
    public void FormatValue_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.FormatValue(null));

      Assert.True(string.Empty.FormatValue() == string.Empty);
      const string Text = "{0} is lesser than {1}";
      Assert.True(Text.FormatValue(5, 10) == string.Format(Text, 5, 10));
    }

    /// <summary>
    ///   <para>Performs testing of <se cref="StringExtensions.Match(string, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Match_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.Match(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => string.Empty.Match(null));

      Assert.False(string.Empty.Match("anything"));
      Assert.True("ab4Zg95kf".Match("[a-zA-z0-9]"));
      Assert.False("~#$%".Match("[a-zA-z0-9]"));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="StringExtensions.Replace(string, IEnumerable{KeyValuePair{string, string}})"/></description></item>
    ///     <item><description><see cref="StringExtensions.Replace(string, IList{string}, IList{string})"/></description></item>
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
      
      Assert.True(Original.Replace(new Dictionary<string, string>().AddNext("quick", "slow").AddNext("dog", "bear").AddNext("nothing", string.Empty).AddNext("brown", "hazy")) == Replaced);
      Assert.True(Original.Replace(new [] { "quick", "dog", "nothing", "brown" }.ToList(), new [] { "slow", "bear", string.Empty, "hazy" }.ToList()) == Replaced);
      Assert.True(Original.Replace(new[] { "quick", "dog", "nothing", "brown" }, new[] { "slow", "bear", string.Empty, "hazy" }) == Replaced);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtendedExtensions.Secure(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Secure_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtendedExtensions.Secure(null));
      
      string.Empty.Secure().With(value => Assert.True(value.Length == 0));
      var text = Guid.NewGuid().ToString();
      text.Secure().With(value =>
      {
        Assert.True(value.Length == text.Length);
        Assert.False(ReferenceEquals(value.ToString(), text));
      });
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

      Assert.True(byte.MaxValue.ToString().ToByte() == byte.MaxValue);
      Assert.Throws<FormatException>(() => Invalid.ToByte());

      byte result;
      Assert.True(byte.MaxValue.ToString().ToByte(out result));
      Assert.True(result == byte.MaxValue);
      Assert.False(Invalid.ToByte(out result));
      Assert.True(result == default(byte));
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

      Assert.True(date.EqualsDate(date.ToString().ToDateTime()));
      Assert.True(date.EqualsTime(date.ToString().ToDateTime()));
      Assert.Throws<FormatException>(() => Invalid.ToDateTime());

      DateTime result;
      Assert.True(date.ToString().ToDateTime(out result));
      Assert.True(result.EqualsDate(date));
      Assert.True(result.EqualsTime(date));
      Assert.False(Invalid.ToDateTime(out result));
      Assert.True(result == default(DateTime));
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

      Assert.True(decimal.MaxValue.ToString().ToDecimal() == decimal.MaxValue);
      Assert.Throws<FormatException>(() => Invalid.ToDecimal());

      decimal result;
      Assert.True(decimal.MaxValue.ToString().ToDecimal(out result));
      Assert.True(result == decimal.MaxValue);
      Assert.False(Invalid.ToDecimal(out result));
      Assert.True(result == default(decimal));
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

      Assert.True(double.Epsilon.ToString().ToDouble() == double.Epsilon);
      Assert.Throws<FormatException>(() => Invalid.ToDouble());

      double result;
      Assert.True(double.Epsilon.ToString().ToDouble(out result));
      Assert.True(result == double.Epsilon);
      Assert.False(Invalid.ToDouble(out result));
      Assert.True(result == default(double));
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

      Assert.True(Guid.Empty.ToString().ToGuid() == Guid.Empty);
      Assert.Throws<FormatException>(() => Invalid.ToGuid());

      Guid result;
      Assert.True(Guid.Empty.ToString().ToGuid(out result));
      Assert.True(result == Guid.Empty);
      Assert.False(Invalid.ToGuid(out result));
      Assert.True(result == default(Guid));
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
      Assert.True(short.MaxValue.ToString().ToInt16() == short.MaxValue);
      Assert.Throws<FormatException>(() => Invalid.ToInt16());

      short result;
      Assert.True(short.MaxValue.ToString().ToInt16(out result));
      Assert.True(result == short.MaxValue);
      Assert.False(Invalid.ToInt16(out result));
      Assert.True(result == default(short));
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
      
      Assert.True(int.MaxValue.ToString().ToInt32() == int.MaxValue);
      Assert.Throws<FormatException>(() => Invalid.ToInt32());
      
      int result;
      Assert.True(int.MaxValue.ToString().ToInt32(out result));
      Assert.True(result == int.MaxValue);
      Assert.False(Invalid.ToInt32(out result));
      Assert.True(result == default(int));
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

      Assert.True(long.MaxValue.ToString().ToInt64() == long.MaxValue);
      Assert.Throws<FormatException>(() => Invalid.ToInt64());

      long result;
      Assert.True(long.MaxValue.ToString().ToInt64(out result));
      Assert.True(result == long.MaxValue);
      Assert.False(Invalid.ToInt64(out result));
      Assert.True(result == default(long));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.ToIpAddress(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToIpAddress_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToIpAddress(null));

      const string Invalid = "invalid";

      Assert.True(IPAddress.Loopback.ToString().ToIpAddress().Equals(IPAddress.Loopback));
      Assert.Throws<FormatException>(() => Invalid.ToIpAddress());

      IPAddress result;
      Assert.True(IPAddress.Loopback.ToString().ToIpAddress(out result));
      Assert.True(result.Equals(IPAddress.Loopback));
      Assert.False(Invalid.ToIpAddress(out result));
      Assert.True(result == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="StringExtensions.ToRegex(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void ToRegex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => StringExtensions.ToRegex(null));

      const string Pattern = "pattern";
      Assert.True(Pattern.ToRegex().ToString() == Pattern);
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

      Assert.True(Single.Epsilon.ToString().ToSingle() == Single.Epsilon);
      Assert.Throws<FormatException>(() => Invalid.ToSingle());

      Single result;
      Assert.True(Single.Epsilon.ToString().ToSingle(out result));
      Assert.True(result == Single.Epsilon);
      Assert.False(Invalid.ToSingle(out result));
      Assert.True(result == default(Single));
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

      Assert.True(Uri.ToUri() == new Uri(Uri));
      Assert.Throws<UriFormatException>(() => Invalid.ToUri());

      /*Uri result;
      Assert.True(uri.ToUri(out result));
      Assert.True(result.Equals(new Uri(uri)));
      Assert.True(invalid.ToUri(out result));
      Assert.True(result == default(Uri));*/
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

      Assert.True(Original.Tokenize(new Dictionary<string, object>().AddNext("quick", "slow").AddNext("dog", "bear").AddNext("nothing", string.Empty).AddNext("brown", "hazy")) == Replaced);
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
  }
}