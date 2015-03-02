using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ArraysExtensions"/>.</para>
  /// </summary>
  public sealed class ArraysExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ArraysExtensions.Bytes(char[], Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArraysExtensions.Bytes(null));

      var text = Guid.NewGuid().ToString();
      Assert.Equal(text.ToCharArray().Bytes(Encoding.Unicode).Length * 2, text.ToCharArray().Bytes(Encoding.UTF32).Length);
      Assert.Equal(text, text.ToCharArray().Bytes(Encoding.Unicode).String(Encoding.Unicode));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArraysExtensions.Base64(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void Base64_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArraysExtensions.Base64(null));

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.Equal(string.Empty, Enumerable.Empty<byte>().ToArray().Base64());
      Assert.Equal(System.Convert.ToBase64String(bytes), bytes.Base64());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArraysExtensions.Hex(byte[])"/> method.</para>
    /// </summary>
    [Fact]
    public void Hex_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArraysExtensions.Hex(null));

      var bytes = Guid.NewGuid().ToByteArray();
      Assert.Equal(string.Empty, Enumerable.Empty<byte>().ToArray().Hex());
      Assert.Equal(bytes.Length * 2, bytes.Hex().Length);
      Assert.True(bytes.Hex().IsMatch("[0-9A-Z]"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArraysExtensions.Join{T}(T[], T[])"/> method.</para>
    /// </summary>
    [Fact]
    public void Join_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArraysExtensions.Join(null, Enumerable.Empty<object>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<object>().ToArray().Join(null));

      Assert.Equal(0, Enumerable.Empty<object>().ToArray().Join(Enumerable.Empty<object>().ToArray()).Length);
      Assert.True(new [] { "first" }.Join(Enumerable.Empty<object>().ToArray()).SequenceEqual(new [] { "first" }));
      Assert.True(Enumerable.Empty<object>().ToArray().Join(new [] { "second" }).SequenceEqual(new [] { "second" }));
      Assert.True(new [] { "first", "second" }.Join(new [] { "third" }).SequenceEqual(new [] { "first", "second", "third" }));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="ArraysExtensions.String(char[])"/></description></item>
    ///     <item><description><see cref="ArraysExtensions.String(byte[], Encoding)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void String_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => ArraysExtensions.String(null));
      Assert.Throws<ArgumentNullException>(() => ArraysExtensions.String(null, Encoding.Default));

      var text = Guid.NewGuid().ToString();
      Assert.Equal(text, text.ToCharArray().String());
      Assert.Equal(text, Encoding.Default.GetBytes(text).String());
      Assert.Equal(text, Encoding.Unicode.GetBytes(text).String(Encoding.Unicode));
    }
  }
}