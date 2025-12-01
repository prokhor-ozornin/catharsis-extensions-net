using AutoFixture;
using System.Security;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="SecureStringExtensions"/>.</para>
/// </summary>
/// <seealso cref="SecureStringExtensions"/>
public sealed class SecureStringExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.get_IsUnset(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Property()
  {
    using (new AssertionScope())
    {
      Test(true, null);
      Test(true, EmptySecureString);
      Test(false, RandomSecureString);
    }

    return;

    static void Test(bool result, SecureString text)
    {
      using (text)
      {
        text.IsUnset.Should().Be(text is null || text.IsEmpty).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.get_IsEmpty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).IsEmpty).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(true, EmptySecureString);
      Test(false, RandomSecureString);
    }

    return;

    static void Test(bool result, SecureString text)
    {
      using (text)
      {
        text.IsEmpty.Should().Be(text.Length == 0).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.get_Bytes(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void Bytes_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.get_Text(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void Text_Property()
  {
    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.AsReadOnly(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).AsReadOnly()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(EmptySecureString);
      Test(RandomSecureString);
    }

    return;

    static void Test(SecureString text)
    {
      using (text)
      {
        text.AsReadOnly().Should().BeOfType<SecureString>().And.BeSameAs(text);
        text.IsReadOnly().Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.Empty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).Empty()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(EmptySecureString);
      Test(RandomSecureString);
    }

    return;

    static void Test(SecureString text)
    {
      using (text)
      {
        text.Empty().Should().BeOfType<SecureString>().And.BeSameAs(text);
        text.IsEmpty.Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.TryFinallyClear(SecureString, Action{SecureString})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).TryFinallyClear(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => EmptySecureString.TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Test(EmptySecureString);
      Test(RandomSecureString);
    }

    return;

    static void Test(SecureString text)
    {
      using (text)
      {
        text.TryFinallyClear(secure => secure.With(char.MinValue, char.MaxValue)).Should().BeOfType<SecureString>().And.BeSameAs(text);
        text.IsEmpty.Should().BeTrue();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="SecureStringExtensions.With(SecureString, IEnumerable{char})"/></description></item>
  ///     <item><description><see cref="SecureStringExtensions.With(SecureString, char[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void With_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).With(Enumerable.Empty<char>())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => EmptySecureString.With((IEnumerable<char>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      Test(new SecureString(), []);
      Test(new SecureString(), Fixture.Create<string>());

      static void Test(SecureString secure, IEnumerable<char> characters)
      {
        using (secure)
        {
          var text = secure.ToText();
          secure.With(characters).Should().BeOfType<SecureString>().And.BeSameAs(secure);
          secure.ToText().Should().Be(text + characters.ToText());
        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).With()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => EmptySecureString.With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      Test(new SecureString(), []);
      Test(new SecureString(), Fixture.Create<string>().AsArray());

      static void Test(SecureString secure, params char[] characters)
      {
        using (secure)
        {
          var text = secure.ToText();
          secure.With(characters).Should().BeOfType<SecureString>().And.BeSameAs(secure);
          secure.ToText().Should().Be(text + characters.ToText());
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="SecureStringExtensions.Without(SecureString, IEnumerable{int})"/></description></item>
  ///     <item><description><see cref="SecureStringExtensions.Without(SecureString, int[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Without_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).Without(Enumerable.Empty<int>())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => EmptySecureString.Without((IEnumerable<int>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("positions");
      AssertionExtensions.Should(() => EmptySecureString.Without([0])).ThrowExactly<ArgumentOutOfRangeException>();

      Test(new SecureString(), []);
      new SecureString().With(Fixture.Create<string>()).With(secure => Test(secure, new int[secure.Length].Fill(_ => 0)));

      static void Test(SecureString secure, IEnumerable<int> positions)
      {
        using (secure)
        {
          var text = secure.ToText();
          secure.Without(positions).Should().BeOfType<SecureString>().And.BeSameAs(secure);
          secure.ToText().Should().Be(text.Without(positions));
        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).Without()).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => EmptySecureString.Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("positions");

      Test(new SecureString(), []);
      new SecureString().With(Fixture.Create<string>()).With(secure => Test(secure, new int[secure.Length].Fill(_ => 0).ToArray()));

      static void Test(SecureString secure, params int[] positions)
      {
        using (secure)
        {
          var text = secure.ToText();
          secure.Without(positions).Should().BeOfType<SecureString>().And.BeSameAs(secure);
          secure.ToText().Should().Be(text.Without(positions));
        }
      }
    }
  }


  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.Min(SecureString, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).Min(EmptySecureString)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => EmptySecureString.Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

      Test(new SecureString(), new SecureString());
      Test(new SecureString(), RandomSecureString);
    }

    return;

    static void Test(SecureString min, SecureString max)
    {
      using (min)
      {
        using (max)
        {
          min.Min(max).Should().BeOfType<SecureString>().And.BeSameAs(min);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.Max(SecureString, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void Max_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).Max(EmptySecureString)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => EmptySecureString.Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

      Test(new SecureString(), new SecureString());
      Test(new SecureString(), RandomSecureString);
    }

    return;

    static void Test(SecureString min, SecureString max)
    {
      using (min)
      {
        using (max)
        {
          min.Max(max).Should().BeOfType<SecureString>().And.BeSameAs(max);
        }
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.MinMax(SecureString, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void MinMax_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).MinMax(EmptySecureString)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
      AssertionExtensions.Should(() => EmptySecureString.MinMax(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

      Test(new SecureString(), new SecureString());
      Test(new SecureString(), RandomSecureString);
    }

    return;

    static void Test(SecureString min, SecureString max) => min.MinMax(max).Should().Be((min, max));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.ToBytes(SecureString, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Encoding.GetEncodings().Select(encoding => encoding.GetEncoding()).ForEach(encoding =>
      {
        Test(string.Empty, new SecureString(), encoding);
        Fixture.Create<string>().With(text => Test(text, new SecureString().With(text), encoding));
      });
    }

    return;

    static void Test(string text, SecureString secure, Encoding encoding = null)
    {
      using (secure)
      {
        secure.ToBytes(encoding).Should().BeOfType<byte[]>().And.Equal(text.ToBytes(encoding));
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.ToText(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((SecureString) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Test(string.Empty, EmptySecureString);
      Fixture.Create<string>().With(text => Test(text, new SecureString().With(text)));
    }

    return;

    static void Test(string result, SecureString text)
    {
      using (text)
      {
        text.ToText().Should().BeOfType<string>().And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.ToBoolean(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBoolean_Method()
  {
    using (new AssertionScope())
    {
      Test(false, null);
      Test(false, new SecureString());
      Test(true, new SecureString().With(char.MinValue));
    }

    return;

    static void Test(bool result, SecureString text)
    {
      using (text)
      {
        text.ToBoolean().Should().Be(result);
      }
    }
  }
}