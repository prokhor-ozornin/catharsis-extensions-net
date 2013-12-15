using System;
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

      Assert.True(Convert.To.Byte(null) == null);
      Assert.True(Convert.To.Byte(byte.MinValue) == byte.MinValue);
      Assert.True(Convert.To.Byte(byte.MinValue.ToString()) == byte.MinValue);
      Assert.True(Convert.To.Byte(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.DateTime(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void DateTime_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.DateTime(null, new object()));
      
      Assert.True(Convert.To.DateTime(null) == null);
      Assert.True(Convert.To.DateTime(DateTime.MinValue) == DateTime.MinValue);
      Assert.True(Convert.To.DateTime(DateTime.MinValue.ToString()) == DateTime.MinValue);
      Assert.True(Convert.To.DateTime(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Decimal(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Decimal_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Decimal(null, new object()));

      Assert.True(Convert.To.Decimal(null) == null);
      Assert.True(Convert.To.Decimal(decimal.MinValue) == decimal.MinValue);
      Assert.True(Convert.To.Decimal(decimal.MinValue.ToString()) == decimal.MinValue);
      Assert.True(Convert.To.Decimal(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Double(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Double_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Double(null, new object()));

      Assert.True(Convert.To.Double(null) == null);
      Assert.True(Convert.To.Double(double.MinValue) == double.MinValue);
      Assert.True(Convert.To.Double(double.Epsilon.ToString()) == double.Epsilon);
      Assert.True(Convert.To.Double(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Guid(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Guid_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Guid(null, new object()));

      Assert.True(Convert.To.Guid(null) == null);
      Assert.True(Convert.To.Guid(Guid.Empty) == Guid.Empty);
      Assert.True(Convert.To.Guid(Guid.Empty.ToString()) == Guid.Empty);
      Assert.True(Convert.To.Guid(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Int16(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Int16_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Int16(null, new object()));

      Assert.True(Convert.To.Int16(null) == null);
      Assert.True(Convert.To.Int16(short.MinValue) == short.MinValue);
      Assert.True(Convert.To.Int16(short.MinValue.ToString()) == short.MinValue);
      Assert.True(Convert.To.Int16(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Int32(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Int32_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Int32(null, new object()));

      Assert.True(Convert.To.Int32(null) == null);
      Assert.True(Convert.To.Int32(int.MinValue) == int.MinValue);
      Assert.True(Convert.To.Int32(int.MinValue.ToString()) == int.MinValue);
      Assert.True(Convert.To.Int32(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Int64(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Int64_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Int64(null, new object()));

      Assert.True(Convert.To.Int64(null) == null);
      Assert.True(Convert.To.Int64(long.MinValue) == long.MinValue);
      Assert.True(Convert.To.Int64(long.MinValue.ToString()) == long.MinValue);
      Assert.True(Convert.To.Int64(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.IPAddress(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void IPAddress_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.IPAddress(null, new object()));

      Assert.True(Convert.To.IPAddress(null) == null);
      Assert.True(ReferenceEquals(Convert.To.IPAddress(IPAddress.Loopback), IPAddress.Loopback));
      Assert.True(Convert.To.IPAddress(IPAddress.Loopback.ToString()).Equals(IPAddress.Loopback));
      Assert.True(Convert.To.IPAddress(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Regex(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Regex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Regex(null, new object()));

      Assert.True(Convert.To.Regex(null) == null);
      var regex = new Regex(".");
      Assert.True(ReferenceEquals(Convert.To.Regex(regex), regex));
      Assert.True(Convert.To.Regex(".").ToString() == new Regex(".").ToString());
      Assert.True(Convert.To.Regex(Guid.Empty).ToString() == new Regex(Guid.Empty.ToString()).ToString());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Single(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Single_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Single(null, new object()));

      Assert.True(Convert.To.Single(null) == null);
      Assert.True(Convert.To.Single(Single.MinValue) == Single.MinValue);
      Assert.True(Convert.To.Single(Single.Epsilon.ToString()) == Single.Epsilon);
      Assert.True(Convert.To.Single(new object()) == null);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.String(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void String_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.String(null, new object()));

      Assert.True(Convert.To.String(null) == null);

      const string value = "value";
      Assert.True(ReferenceEquals(Convert.To.String(value), value));
      Assert.True(Convert.To.String(Guid.Empty) == Guid.Empty.ToString());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ConvertExtensions.Uri(Convert, object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Uri_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ConvertExtensions.Uri(null, new object()));

      Assert.True(Convert.To.Uri(null) == null);
      var uri = new Uri("http://url.com");
      Assert.True(ReferenceEquals(Convert.To.Uri(uri), uri));
      Assert.True(Convert.To.Uri("http://url.com") == uri);
    }
  }
}