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
    ///   <para>Performs testing of <see cref="ArrayExtensions.EncodeBase64(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void EncodeBase64_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtensions.EncodeBase64(null));

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.Equal(string.Empty, Enumerable.Empty<byte>().ToArray().EncodeBase64());
      Assert.Equal(System.Convert.ToBase64String(bytes), bytes.EncodeBase64());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtensions.EncodeHex(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void EncodeHex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtensions.EncodeHex(null));

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.Equal(string.Empty, Enumerable.Empty<byte>().ToArray().EncodeHex());
      Assert.Equal(bytes.Length * 2, bytes.EncodeHex().Length);
      Assert.True(bytes.EncodeHex().Match("[0-9A-Z]"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtendedExtensions.EncodeMd5(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void EncodeMD5_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtendedExtensions.EncodeMd5(null));

      Assert.Equal(16, Enumerable.Empty<byte>().ToArray().EncodeMd5().Length);
      Assert.Equal(16, Guid.NewGuid().ToByteArray().EncodeMd5().Length);
      Assert.False(Guid.NewGuid().ToByteArray().EncodeMd5().SequenceEqual(Guid.NewGuid().ToByteArray().EncodeMd5()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtendedExtensions.EncodeSha1(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void EncodeSHA1_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtendedExtensions.EncodeSha1(null));

      Assert.Equal(20, Enumerable.Empty<byte>().ToArray().EncodeSha1().Length);
      Assert.Equal(20, Guid.NewGuid().ToByteArray().EncodeSha1().Length);
      Assert.False(Guid.NewGuid().ToByteArray().EncodeSha1().SequenceEqual(Guid.NewGuid().ToByteArray().EncodeSha1()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtendedExtensions.EncodeSha256(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void EncodeSha256_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtendedExtensions.EncodeSha256(null));

      Assert.Equal(32, Enumerable.Empty<byte>().ToArray().EncodeSha256().Length);
      Assert.Equal(32, Guid.NewGuid().ToByteArray().EncodeSha256().Length);
      Assert.False(Guid.NewGuid().ToByteArray().EncodeSha256().SequenceEqual(Guid.NewGuid().ToByteArray().EncodeSha256()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArrayExtendedExtensions.EncodeSha512(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void EncodeSHA512_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArrayExtendedExtensions.EncodeSha512(null));

      Assert.Equal(64, Enumerable.Empty<byte>().ToArray().EncodeSha512().Length);
      Assert.Equal(64, Guid.NewGuid().ToByteArray().EncodeSha512().Length);
      Assert.False(Guid.NewGuid().ToByteArray().EncodeSha512().SequenceEqual(Guid.NewGuid().ToByteArray().EncodeSha512()));
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