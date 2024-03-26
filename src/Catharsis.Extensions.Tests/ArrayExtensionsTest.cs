using System.Text;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Convert = System.Convert;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ArrayExtensions"/>.</para>
/// </summary>
public sealed class ArrayExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.Range{T}(T[], int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    AssertionExtensions.Should(() => ArrayExtensions.Range<object>(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Array.Empty<object>().Range(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
    AssertionExtensions.Should(() => Array.Empty<object>().Range(null, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.FromBase64(char[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_FromBase64_Method()
  {
    AssertionExtensions.Should(() => ArrayExtensions.FromBase64(null)).ThrowExactly<ArgumentNullException>().WithParameterName("chars");

    var bytes = Attributes.RandomBytes();

    Enumerable.Empty<byte>().ToBase64().Should().BeEmpty();
    bytes.ToBase64().Should().Be(Convert.ToBase64String(bytes));

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, char[] chars)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToBytes(char[], Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ArrayExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("chars");

    //Validate(Attributes.RandomChars());
    //Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomChars(), encoding.GetEncoding()));

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, char[] chars, Encoding encoding = null)
    {
      Array.Empty<char>().ToBytes(encoding).Should().NotBeNull().And.BeSameAs(Array.Empty<char>().ToBytes(encoding)).And.BeEmpty();

      var bytes = chars.ToBytes(encoding);
      bytes.Should().NotBeNull().And.NotBeSameAs(chars.ToBytes(encoding)).And.HaveCount((encoding ?? Encoding.Default).GetByteCount(chars)).And.Equal((encoding ?? Encoding.Default).GetBytes(chars));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToText(char[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_ToText_Method()
  {
    AssertionExtensions.Should(() => ArrayExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("chars");

    Array.Empty<char>().ToText().Should().NotBeNull().And.BeSameAs(Array.Empty<char>().ToText()).And.BeEmpty();

    var text = Attributes.RandomString();
    var chars = text.ToCharArray();
    chars.ToText().Should().NotBeNull().And.NotBeSameAs(chars.ToText()).And.Be(text);

    throw new NotImplementedException();

    return;

    static void Validate(string result, char[] chars)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToText(byte[], Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ByteArray_ToText_Method()
  {
    AssertionExtensions.Should(() => ((byte[]) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    //Validate(Attributes.RandomBytes(), null);
    //Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomBytes(), encoding.GetEncoding()));

    throw new NotImplementedException();

    return;

    static void Validate(string result, byte[] bytes, Encoding encoding = null)
    {
      Array.Empty<byte>().ToText(encoding).Should().NotBeNull().And.BeSameAs(Array.Empty<byte>().ToText(encoding)).And.BeEmpty();
      bytes.ToText(encoding).Should().NotBeNull().And.NotBeSameAs(bytes.ToText(encoding)).And.HaveLength((encoding ?? Encoding.Default).GetCharCount(bytes)).And.Be((encoding ?? Encoding.Default).GetString(bytes));
    }
  }
}