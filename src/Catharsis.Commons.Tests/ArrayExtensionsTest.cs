using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ArrayExtensions"/>.</para>
/// </summary>
public sealed class ArrayExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.Range{T}(T[], int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Array_Range_Method()
  {
    AssertionExtensions.Should(() => ArrayExtensions.Range<object>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.FromBase64(char[])"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_FromBase64_Method()
  {
    AssertionExtensions.Should(() => ArrayExtensions.FromBase64(null)).ThrowExactly<ArgumentNullException>();

    var bytes = RandomBytes;

    Enumerable.Empty<byte>().ToBase64().Should().BeEmpty();
    bytes.ToBase64().Should().Be(System.Convert.ToBase64String(bytes));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ArrayExtensions.ToBytes(char[], Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Array_ToBytes_Method()
  {
    void Validate(char[] chars, Encoding encoding = null)
    {
      Array.Empty<char>().ToBytes(encoding).Should().NotBeNull().And.BeSameAs(Array.Empty<char>().ToBytes(encoding)).And.BeEmpty();

      var bytes = chars.ToBytes(encoding);
      bytes.Should().NotBeNull().And.NotBeSameAs(chars.ToBytes(encoding)).And.HaveCount((encoding ?? Encoding.Default).GetByteCount(chars)).And.Equal((encoding ?? Encoding.Default).GetBytes(chars));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ArrayExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>();

      Validate(RandomChars);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(encoding => Validate(RandomChars, encoding));
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ArrayExtensions.ToText(char[])"/></description></item>
  ///     <item><description><see cref="ArrayExtensions.ToText(byte[], Encoding)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Array_ToText_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ArrayExtensions.ToText(null)).ThrowExactly<ArgumentNullException>();

      Array.Empty<char>().ToText().Should().NotBeNull().And.BeSameAs(Array.Empty<char>().ToText()).And.BeEmpty();

      var text = RandomString;
      var chars = text.ToCharArray();
      chars.ToText().Should().NotBeNull().And.NotBeSameAs(chars.ToText()).And.Be(text);
    }

    using (new AssertionScope())
    {
      void Validate(byte[] bytes, Encoding encoding = null)
      {
        Array.Empty<byte>().ToText(encoding).Should().NotBeNull().And.BeSameAs(Array.Empty<byte>().ToText(encoding)).And.BeEmpty();
        bytes.ToText(encoding).Should().NotBeNull().And.NotBeSameAs(bytes.ToText(encoding)).And.HaveLength((encoding ?? Encoding.Default).GetCharCount(bytes)).And.Be((encoding ?? Encoding.Default).GetString(bytes));
      }

      using (new AssertionScope())
      {
        AssertionExtensions.Should(() => ((byte[]) null).ToText()).ThrowExactly<ArgumentNullException>();

        Validate(RandomBytes);
        Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(encoding => Validate(RandomBytes, encoding));
      }
    }
  }
}