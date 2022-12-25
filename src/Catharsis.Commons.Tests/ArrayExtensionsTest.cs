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
  ///   <para>Performs testing of <see cref="ArrayExtensions.Bytes(char[], Encoding?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Array_Bytes_Method()
  {
    void Validate(char[] chars, Encoding? encoding = null)
    {
      Array.Empty<char>().Bytes(encoding).Should().NotBeNull().And.BeSameAs(Array.Empty<char>().Bytes(encoding)).And.BeEmpty();

      var bytes = chars.Bytes(encoding);
      bytes.Should().NotBeNull().And.NotBeSameAs(chars.Bytes(encoding)).And.HaveCount((encoding ?? Encoding.Default).GetByteCount(chars)).And.Equal((encoding ?? Encoding.Default).GetBytes(chars));
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ArrayExtensions.Bytes(null!)).ThrowExactly<ArgumentNullException>();

      Validate(RandomChars, null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(encoding => Validate(RandomChars, encoding));
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ArrayExtensions.Text(char[])"/></description></item>
  ///     <item><description><see cref="ArrayExtensions.Text(byte[], Encoding?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Array_Text_Methods()
  {
    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ArrayExtensions.Text(null!)).ThrowExactly<ArgumentNullException>();

      Array.Empty<char>().Text().Should().NotBeNull().And.BeSameAs(Array.Empty<char>().Text()).And.BeEmpty();

      var text = RandomString;
      var chars = text.ToCharArray();
      chars.Text().Should().NotBeNull().And.NotBeSameAs(chars.Text()).And.Be(text);
    }

    using (new AssertionScope())
    {
      void Validate(byte[] bytes, Encoding? encoding = null)
      {
        Array.Empty<byte>().Text(encoding).Should().NotBeNull().And.BeSameAs(Array.Empty<byte>().Text(encoding)).And.BeEmpty();
        bytes.Text(encoding).Should().NotBeNull().And.NotBeSameAs(bytes.Text(encoding)).And.HaveLength((encoding ?? Encoding.Default).GetCharCount(bytes)).And.Be((encoding ?? Encoding.Default).GetString(bytes));
      }

      using (new AssertionScope())
      {
        //AssertionExtensions.Should(() => ((byte[]) null!).Text()).ThrowExactly<ArgumentNullException>();

        Validate(RandomBytes, null);
        Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(encoding => Validate(RandomBytes, encoding));
      }
    }
  }
}