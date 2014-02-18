using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ConvertExtensions"/>.</para>
  /// </summary>
  public sealed class ConvertExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Boolean(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Boolean_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Boolean(null, new object()));

      Assert.False(Convert.To.Boolean(null));
      Assert.False(Convert.To.Boolean(false));
      Assert.True(Convert.To.Boolean(true));
      Assert.False(Convert.To.Boolean(byte.MinValue));
      Assert.True(Convert.To.Boolean(byte.MaxValue));
      Assert.False(Convert.To.Boolean(char.MinValue));
      Assert.True(Convert.To.Boolean(char.MaxValue));
      Assert.False(Convert.To.Boolean(decimal.MinValue));
      Assert.True(Convert.To.Boolean(decimal.MaxValue));
      Assert.False(Convert.To.Boolean(double.MinValue));
      Assert.True(Convert.To.Boolean(double.MaxValue));
      Assert.False(Convert.To.Boolean(short.MinValue));
      Assert.True(Convert.To.Boolean(short.MaxValue));
      Assert.False(Convert.To.Boolean(int.MinValue));
      Assert.True(Convert.To.Boolean(int.MaxValue));
      Assert.False(Convert.To.Boolean(long.MinValue));
      Assert.True(Convert.To.Boolean(long.MaxValue));
      Assert.False(Convert.To.Boolean(sbyte.MinValue));
      Assert.True(Convert.To.Boolean(sbyte.MaxValue));
      Assert.False(Convert.To.Boolean(Single.MinValue));
      Assert.True(Convert.To.Boolean(Single.MaxValue));
      Assert.False(Convert.To.Boolean(ushort.MinValue));
      Assert.True(Convert.To.Boolean(ushort.MaxValue));
      Assert.False(Convert.To.Boolean(uint.MinValue));
      Assert.True(Convert.To.Boolean(uint.MaxValue));
      Assert.False(Convert.To.Boolean(ulong.MinValue));
      Assert.True(Convert.To.Boolean(ulong.MaxValue));
      Assert.False(Convert.To.Boolean(string.Empty));
      Assert.True(Convert.To.Boolean("string"));
      Assert.False(Convert.To.Boolean(new object[] {}));
      Assert.True(Convert.To.Boolean(new [] { new object() }));
      Assert.False(Convert.To.Boolean(new FileInfo(Path.GetTempFileName())));
      Assert.True(Convert.To.Boolean(new FileInfo(Assembly.GetExecutingAssembly().Location)));
      Assert.False(Convert.To.Boolean(Stream.Null));
      Assert.False(Convert.To.Boolean(Match.Empty));
      Assert.True(Convert.To.Boolean(Regex.Match("string", ".")));
      Assert.True(Convert.To.Boolean(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Byte(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Byte_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Byte(null, new object()));

      Assert.Null(Convert.To.Byte(null));
      Assert.Equal(byte.MinValue, Convert.To.Byte(byte.MinValue));
      Assert.Equal(byte.MinValue, Convert.To.Byte(byte.MinValue.ToString(CultureInfo.InvariantCulture)));
      Assert.Null(Convert.To.Byte(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.DateTime(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void DateTime_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.DateTime(null, new object()));
      
      Assert.Null(Convert.To.DateTime(null));
      Assert.Equal(DateTime.MinValue, Convert.To.DateTime(DateTime.MinValue));
      Assert.Equal(DateTime.MinValue, Convert.To.DateTime(DateTime.MinValue.ToString(CultureInfo.InvariantCulture)));
      Assert.Null(Convert.To.DateTime(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Decimal(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Decimal_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Decimal(null, new object()));

      Assert.Null(Convert.To.Decimal(null));
      Assert.Equal(decimal.MinValue, Convert.To.Decimal(decimal.MinValue));
      Assert.Equal(decimal.MinValue, Convert.To.Decimal(decimal.MinValue.ToString(CultureInfo.InvariantCulture)));
      Assert.Null(Convert.To.Decimal(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Double(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Double_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Double(null, new object()));

      Assert.Null(Convert.To.Double(null));
      Assert.Equal(double.MinValue, Convert.To.Double(double.MinValue));
      Assert.Equal(double.Epsilon, Convert.To.Double(double.Epsilon.ToString(CultureInfo.InvariantCulture)));
      Assert.Null(Convert.To.Double(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Guid(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Guid_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Guid(null, new object()));

      Assert.Null(Convert.To.Guid(null));
      Assert.Equal(Guid.Empty, Convert.To.Guid(Guid.Empty));
      Assert.Equal(Guid.Empty, Convert.To.Guid(Guid.Empty.ToString()));
      Assert.Null(Convert.To.Guid(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Int16(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Int16_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Int16(null, new object()));

      Assert.Null(Convert.To.Int16(null));
      Assert.Equal(short.MinValue, Convert.To.Int16(short.MinValue));
      Assert.Equal(short.MinValue, Convert.To.Int16(short.MinValue.ToString(CultureInfo.InvariantCulture)));
      Assert.Null(Convert.To.Int16(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Int32(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Int32_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Int32(null, new object()));

      Assert.Null(Convert.To.Int32(null));
      Assert.Equal(int.MinValue, Convert.To.Int32(int.MinValue));
      Assert.Equal(int.MinValue, Convert.To.Int32(int.MinValue.ToString(CultureInfo.InvariantCulture)));
      Assert.Null(Convert.To.Int32(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Int64(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Int64_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Int64(null, new object()));

      Assert.Null(Convert.To.Int64(null));
      Assert.Equal(long.MinValue, Convert.To.Int64(long.MinValue));
      Assert.Equal(long.MinValue, Convert.To.Int64(long.MinValue.ToString(CultureInfo.InvariantCulture)));
      Assert.Null(Convert.To.Int64(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.IPAddress(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void IPAddress_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.IPAddress(null, new object()));

      Assert.Null(Convert.To.IPAddress(null));
      Assert.True(ReferenceEquals(Convert.To.IPAddress(IPAddress.Loopback), IPAddress.Loopback));
      Assert.Equal(IPAddress.Loopback, Convert.To.IPAddress(IPAddress.Loopback.ToString()));
      Assert.Null(Convert.To.IPAddress(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Regex(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Regex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Regex(null, new object()));

      Assert.Null(Convert.To.Regex(null));
      var regex = new Regex(".");
      Assert.True(ReferenceEquals(Convert.To.Regex(regex), regex));
      Assert.Equal(new Regex(".").ToString(), Convert.To.Regex(".").ToString());
      Assert.Equal(new Regex(Guid.Empty.ToString()).ToString(), Convert.To.Regex(Guid.Empty).ToString());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Single(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Single_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Single(null, new object()));

      Assert.Null(Convert.To.Single(null));
      Assert.Equal(Single.MinValue, Convert.To.Single(Single.MinValue));
      Assert.Equal(Single.Epsilon, Convert.To.Single(Single.Epsilon.ToString(CultureInfo.InvariantCulture)));
      Assert.Null(Convert.To.Single(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.String(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void String_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.String(null, new object()));

      Assert.Null(Convert.To.String(null));

      const string value = "value";
      Assert.True(ReferenceEquals(Convert.To.String(value), value));
      Assert.Equal(Guid.Empty.ToString(), Convert.To.String(Guid.Empty));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Uri(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Uri_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Uri(null, new object()));

      Assert.Null(Convert.To.Uri(null));
      var uri = new Uri("http://url.com");
      Assert.True(ReferenceEquals(Convert.To.Uri(uri), uri));
      Assert.Equal(uri, Convert.To.Uri("http://url.com"));
    }
  }
}