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
public sealed class SecureStringExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.AsReadOnly(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.AsReadOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

      Test(this.EmptySecureString());
      Test(this.RandomSecureString());
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
  ///   <para>Performs testing of <see cref="SecureStringExtensions.IsUnset(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Method()
  {
    using (new AssertionScope())
    {
      Test(true, null);
      Test(true, this.EmptySecureString());
      Test(false, this.RandomSecureString());
    }

    return;

    static void Test(bool result, SecureString text)
    {
      using (text)
      {
        text.IsUnset().Should().Be(text is null || text.IsEmpty()).And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.IsEmpty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.IsEmpty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

      Test(true, this.EmptySecureString());
      Test(false, this.RandomSecureString());
    }

    return;

    static void Test(bool result, SecureString text)
    {
      using (text)
      {
        text.IsEmpty().Should().Be(text.Length == 0).And.Be(result);
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
      AssertionExtensions.Should(() => SecureStringExtensions.Empty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

      Test(this.EmptySecureString());
      Test(this.RandomSecureString());
    }

    return;

    static void Test(SecureString text)
    {
      using (text)
      {
        text.Empty().Should().BeOfType<SecureString>().And.BeSameAs(text);
        text.IsEmpty().Should().BeTrue();
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
      AssertionExtensions.Should(() => SecureStringExtensions.TryFinallyClear(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("secure");
      AssertionExtensions.Should(() => this.EmptySecureString().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Test(this.EmptySecureString());
      Test(this.RandomSecureString());
    }

    return;

    static void Test(SecureString text)
    {
      using (text)
      {
        text.TryFinallyClear(secure => secure.With(char.MinValue, char.MaxValue)).Should().BeOfType<SecureString>().And.BeSameAs(text);
        text.IsEmpty().Should().BeTrue();
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
      AssertionExtensions.Should(() => SecureStringExtensions.With(null, Enumerable.Empty<char>())).ThrowExactly<ArgumentNullException>().WithParameterName("secure");
      AssertionExtensions.Should(() => this.EmptySecureString().With((IEnumerable<char>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

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
      AssertionExtensions.Should(() => SecureStringExtensions.With(null, Array.Empty<char>())).ThrowExactly<ArgumentNullException>().WithParameterName("secure");
      AssertionExtensions.Should(() => this.EmptySecureString().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

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
      AssertionExtensions.Should(() => SecureStringExtensions.Without(null, Enumerable.Empty<int>())).ThrowExactly<ArgumentNullException>().WithParameterName("secure");
      AssertionExtensions.Should(() => this.EmptySecureString().Without((IEnumerable<int>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("positions");
      AssertionExtensions.Should(() => this.EmptySecureString().Without([0])).ThrowExactly<ArgumentOutOfRangeException>();

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
      AssertionExtensions.Should(() => SecureStringExtensions.Without(null, Array.Empty<int>())).ThrowExactly<ArgumentNullException>().WithParameterName("secure");
      AssertionExtensions.Should(() => this.EmptySecureString().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("positions");

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
      AssertionExtensions.Should(() => SecureStringExtensions.Min(null, this.EmptySecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => this.EmptySecureString().Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Test(new SecureString(), new SecureString());
      Test(new SecureString(), this.RandomSecureString());
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
      AssertionExtensions.Should(() => SecureStringExtensions.Max(null, this.EmptySecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => this.EmptySecureString().Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Test(new SecureString(), new SecureString());
      Test(new SecureString(), this.RandomSecureString());
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
      AssertionExtensions.Should(() => SecureStringExtensions.MinMax(null, this.EmptySecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => this.EmptySecureString().MinMax(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Test(new SecureString(), new SecureString());
      Test(new SecureString(), this.RandomSecureString());
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
      AssertionExtensions.Should(() => SecureStringExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

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
      AssertionExtensions.Should(() => SecureStringExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

      Test(string.Empty, this.EmptySecureString());
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