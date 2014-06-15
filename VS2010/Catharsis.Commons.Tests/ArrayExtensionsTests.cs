using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ArrayExtensions"/>.</para>
  /// </summary>
  public sealed class ArrayExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtensions.Bytes(char[], Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtensions.Bytes(null));

      var text = Guid.NewGuid().ToString();
      Assert.Equal(text.ToCharArray().Bytes(Encoding.Unicode).Length * 2, text.ToCharArray().Bytes(Encoding.UTF32).Length);
      Assert.Equal(text, text.ToCharArray().Bytes(Encoding.Unicode).String(Encoding.Unicode));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtensions.Base64(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void Base64_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtensions.Base64(null));

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.Equal(string.Empty, Enumerable.Empty<byte>().ToArray().Base64());
      Assert.Equal(System.Convert.ToBase64String(bytes), bytes.Base64());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtensions.Hex(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void Hex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtensions.Hex(null));

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.Equal(string.Empty, Enumerable.Empty<byte>().ToArray().Hex());
      Assert.Equal(bytes.Length * 2, bytes.Hex().Length);
      Assert.True(bytes.Hex().Matches("[0-9A-Z]"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtensions.Join{T}(T[], T[])"/> method.</para>
    /// </summary>
    [Fact]
    public void Join_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtensions.Join(null, Enumerable.Empty<object>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().ToArray().Join(null));

      Assert.Equal(0, Enumerable.Empty<object>().ToArray().Join(Enumerable.Empty<object>().ToArray()).Length);
      Assert.True(new [] { "first" }.Join(Enumerable.Empty<object>().ToArray()).SequenceEqual(new [] { "first" }));
      Assert.True(Enumerable.Empty<object>().ToArray().Join(new [] { "second" }).SequenceEqual(new [] { "second" }));
      Assert.True(new [] { "first", "second" }.Join(new [] { "third" }).SequenceEqual(new [] { "first", "second", "third" }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayCryptographyExtensions.MD5(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void MD5_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayCryptographyExtensions.MD5(null));

      Assert.Equal(16, Enumerable.Empty<byte>().ToArray().MD5().Length);
      Assert.Equal(16, Guid.NewGuid().ToByteArray().MD5().Length);
      Assert.False(Guid.NewGuid().ToByteArray().MD5().SequenceEqual(Guid.NewGuid().ToByteArray().MD5()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayCryptographyExtensions.SHA1(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void SHA1_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayCryptographyExtensions.SHA1(null));

      Assert.Equal(20, Enumerable.Empty<byte>().ToArray().SHA1().Length);
      Assert.Equal(20, Guid.NewGuid().ToByteArray().SHA1().Length);
      Assert.False(Guid.NewGuid().ToByteArray().SHA1().SequenceEqual(Guid.NewGuid().ToByteArray().SHA1()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayCryptographyExtensions.SHA256(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void SHA256_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayCryptographyExtensions.SHA256(null));

      Assert.Equal(32, Enumerable.Empty<byte>().ToArray().SHA256().Length);
      Assert.Equal(32, Guid.NewGuid().ToByteArray().SHA256().Length);
      Assert.False(Guid.NewGuid().ToByteArray().SHA256().SequenceEqual(Guid.NewGuid().ToByteArray().SHA256()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayCryptographyExtensions.SHA512(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void SHA512_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayCryptographyExtensions.SHA512(null));

      Assert.Equal(64, Enumerable.Empty<byte>().ToArray().SHA512().Length);
      Assert.Equal(64, Guid.NewGuid().ToByteArray().SHA512().Length);
      Assert.False(Guid.NewGuid().ToByteArray().SHA512().SequenceEqual(Guid.NewGuid().ToByteArray().SHA512()));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ArrayExtensions.String(char[])"/></description></item>
    ///     <item><description><see cref="ArrayExtensions.String(byte[], Encoding)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void String_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtensions.String(null));
      Assert.Throws<ArgumentNullException>(() => ArrayExtensions.String(null, Encoding.Default));

      var text = Guid.NewGuid().ToString();
      Assert.Equal(text, text.ToCharArray().String());
      Assert.Equal(text, Encoding.Default.GetBytes(text).String());
      Assert.Equal(text, Encoding.Unicode.GetBytes(text).String(Encoding.Unicode));
    }
  }
}