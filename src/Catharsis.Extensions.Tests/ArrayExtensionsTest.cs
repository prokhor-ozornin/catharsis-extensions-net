using System.Text;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Convert = System.Convert;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ArrayExtensions"/>.</para>
/// </summary>
/// <seealso cref="ArrayExtensions"/>
public sealed class ArrayExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.Range{T}(T[], int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((object[]) null).Range()).ThrowExactly<ArgumentNullException>().WithParameterName("array");
      AssertionExtensions.Should(() => Array.Empty<object>().Range(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
      AssertionExtensions.Should(() => Array.Empty<object>().Range(null, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();
    }

    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.get_Bytes(char[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_Bytes_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.get_Text(char[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_Text_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.FromBase64(char[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_FromBase64_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((char[]) null).FromBase64()).ThrowExactly<ArgumentNullException>().WithParameterName("array");

      var bytes = Bytes;

      Enumerable.Empty<byte>().ToBase64().Should().BeOfType<string>().And.BeEmpty();
      bytes.ToBase64().Should().BeOfType<string>().And.Be(Convert.ToBase64String(bytes));
    }

    throw new NotImplementedException();

    return;

    static void Test(byte[] result, char[] chars)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToBytes(char[], Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((char[]) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("array");

      //Test(this.RandomChars());
      //Encoding.GetEncodings().ForEach(encoding => Test(this.RandomChars(), encoding.GetEncoding()));
    }

    throw new NotImplementedException();

    return;

    static void Test(byte[] result, char[] chars, Encoding encoding = null)
    {
      Array.Empty<char>().ToBytes(encoding).Should().BeOfType<char[]>().And.BeSameAs(Array.Empty<char>().ToBytes(encoding)).And.BeEmpty();

      var bytes = chars.ToBytes(encoding);
      bytes.Should().BeOfType<byte[]>().And.HaveCount((encoding ?? Encoding.Default).GetByteCount(chars)).And.Equal((encoding ?? Encoding.Default).GetBytes(chars));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToText(char[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((char[]) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("array");

      Array.Empty<char>().ToText().Should().BeOfType<char[]>().And.BeSameAs(Array.Empty<char>().ToText()).And.BeEmpty();

      var text = Fixture.Create<string>();
      var chars = text.ToCharArray();
      chars.ToText().Should().BeOfType<string>().And.Be(text);
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, char[] chars)
    {
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.get_Text(byte[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ByteArray_Text_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToByteArrayContent(byte[], int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void ByteArray_ToByteArrayContent_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((byte[]) null).ToByteArrayContent()).ThrowExactly<ArgumentNullException>().WithParameterName("array");

      Test([]);
      Test(Bytes);
    }

    return;

    static void Test(byte[] bytes)
    {
      using var content = bytes.ToByteArrayContent();

      content.Should().BeOfType<ByteArrayContent>();
      content.Headers.Should().BeEmpty();
      content.ReadAsByteArrayAsync().Await().Should().Equal(bytes);
    }
  }


  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToText(byte[], Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ByteArray_ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((byte[]) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("array");

      //Test(Bytes, null);
      //Encoding.GetEncodings().ForEach(encoding => Test(Bytes, encoding.GetEncoding()));
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, byte[] bytes, Encoding encoding = null)
    {
      Array.Empty<byte>().ToText(encoding).Should().BeOfType<string>().And.BeSameAs(Array.Empty<byte>().ToText(encoding)).And.BeEmpty();
      bytes.ToText(encoding).Should().BeOfType<string>().And.HaveLength((encoding ?? Encoding.Default).GetCharCount(bytes)).And.Be((encoding ?? Encoding.Default).GetString(bytes));
    }
  }
}