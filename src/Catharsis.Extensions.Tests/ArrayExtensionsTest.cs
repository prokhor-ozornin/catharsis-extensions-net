using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

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
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.FromBase64(char[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_FromBase64_Method()
  {
    AssertionExtensions.Should(() => ArrayExtensions.FromBase64(null)).ThrowExactly<ArgumentNullException>().WithParameterName("chars");

    var bytes = RandomBytes;

    Enumerable.Empty<byte>().ToBase64().Should().BeEmpty();
    bytes.ToBase64().Should().Be(Convert.ToBase64String(bytes));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToBytes(char[], Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void CharArray_ToBytes_Method()
  {
    void Validate(char[] chars, Encoding encoding)
    {
      Array.Empty<char>().ToBytes(encoding).Should().NotBeNull().And.BeSameAs(Array.Empty<char>().ToBytes(encoding)).And.BeEmpty();

      var bytes = chars.ToBytes(encoding);
      bytes.Should().NotBeNull().And.NotBeSameAs(chars.ToBytes(encoding)).And.HaveCount((encoding ?? Encoding.Default).GetByteCount(chars)).And.Equal((encoding ?? Encoding.Default).GetBytes(chars));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ArrayExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("chars");

      Validate(RandomChars, null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(encoding => Validate(RandomChars, encoding));
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

    var text = RandomString;
    var chars = text.ToCharArray();
    chars.ToText().Should().NotBeNull().And.NotBeSameAs(chars.ToText()).And.Be(text);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToText(byte[], Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ByteArray_ToText_Method()
  {
    void Validate(byte[] bytes, Encoding encoding)
    {
      Array.Empty<byte>().ToText(encoding).Should().NotBeNull().And.BeSameAs(Array.Empty<byte>().ToText(encoding)).And.BeEmpty();
      bytes.ToText(encoding).Should().NotBeNull().And.NotBeSameAs(bytes.ToText(encoding)).And.HaveLength((encoding ?? Encoding.Default).GetCharCount(bytes)).And.Be((encoding ?? Encoding.Default).GetString(bytes));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((byte[]) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

      Validate(RandomBytes, null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(encoding => Validate(RandomBytes, encoding));
    }
  }
}