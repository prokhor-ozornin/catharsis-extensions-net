using System.Security;
using System.Text;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="SecureStringExtensions"/>.</para>
/// </summary>
public sealed class SecureStringExtensionsTest : UnitTest
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

      Validate(Attributes.EmptySecureString());
      Validate(Attributes.RandomSecureString());
    }

    return;

    static void Validate(SecureString secure)
    {
      using (secure)
      {
        secure.AsReadOnly().Should().BeOfType<SecureString>().And.BeSameAs(secure);
        secure.IsReadOnly().Should().BeTrue();
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
      Validate(true, null);
      Validate(true, Attributes.EmptySecureString());
      Validate(false, Attributes.RandomSecureString());
    }

    return;

    static void Validate(bool result, SecureString secure)
    {
      using (secure)
      {
        secure.IsUnset().Should().Be(secure is null || secure.IsEmpty()).And.Be(result);
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

      Validate(true, Attributes.EmptySecureString());
      Validate(false, Attributes.RandomSecureString());
    }

    return;

    static void Validate(bool result, SecureString secure)
    {
      using (secure)
      {
        secure.IsEmpty().Should().Be(secure.Length == 0).And.Be(result);
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

      Validate(Attributes.EmptySecureString());
      Validate(Attributes.RandomSecureString());
    }

    return;

    static void Validate(SecureString secure)
    {
      using (secure)
      {
        secure.Empty().Should().BeOfType<SecureString>().And.BeSameAs(secure);
        secure.IsEmpty().Should().BeTrue();
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
      AssertionExtensions.Should(() => Attributes.EmptySecureString().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(Attributes.EmptySecureString());
      Validate(Attributes.RandomSecureString());
    }

    return;

    static void Validate(SecureString secure)
    {
      using (secure)
      {
        secure.TryFinallyClear(secure => secure.With(char.MinValue, char.MaxValue)).Should().BeOfType<SecureString>().And.BeSameAs(secure);
        secure.IsEmpty().Should().BeTrue();
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
      AssertionExtensions.Should(() => Attributes.EmptySecureString().With((IEnumerable<char>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      Validate(new SecureString(), []);
      Validate(new SecureString(), Attributes.RandomString());

      static void Validate(SecureString secure, IEnumerable<char> characters)
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
      AssertionExtensions.Should(() => Attributes.EmptySecureString().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      Validate(new SecureString(), []);
      Validate(new SecureString(), Attributes.RandomString().AsArray());

      static void Validate(SecureString secure, params char[] characters)
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
      AssertionExtensions.Should(() => Attributes.EmptySecureString().Without((IEnumerable<int>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("positions");
      AssertionExtensions.Should(() => Attributes.EmptySecureString().Without([0])).ThrowExactly<ArgumentOutOfRangeException>();

      Validate(new SecureString(), []);
      new SecureString().With(Attributes.RandomString()).With(secure => Validate(secure, new int[secure.Length].Fill(_ => 0)));

      static void Validate(SecureString secure, IEnumerable<int> positions)
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
      AssertionExtensions.Should(() => Attributes.EmptySecureString().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("positions");

      Validate(new SecureString(), []);
      new SecureString().With(Attributes.RandomString()).With(secure => Validate(secure, new int[secure.Length].Fill(_ => 0).ToArray()));

      static void Validate(SecureString secure, params int[] positions)
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
      AssertionExtensions.Should(() => SecureStringExtensions.Min(null, Attributes.EmptySecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => Attributes.EmptySecureString().Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(new SecureString(), new SecureString());
      Validate(new SecureString(), Attributes.RandomSecureString());
    }

    return;

    static void Validate(SecureString min, SecureString max)
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
      AssertionExtensions.Should(() => SecureStringExtensions.Max(null, Attributes.EmptySecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => Attributes.EmptySecureString().Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(new SecureString(), new SecureString());
      Validate(new SecureString(), Attributes.RandomSecureString());
    }

    return;

    static void Validate(SecureString min, SecureString max)
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
      AssertionExtensions.Should(() => SecureStringExtensions.MinMax(null, Attributes.EmptySecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => Attributes.EmptySecureString().MinMax(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(new SecureString(), new SecureString());
      Validate(new SecureString(), Attributes.RandomSecureString());
    }

    return;

    static void Validate(SecureString min, SecureString max) => min.MinMax(max).Should().Be((min, max));
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
        Validate(string.Empty, new SecureString(), encoding);
        Attributes.RandomString().With(text => Validate(text, new SecureString().With(text), encoding));
      });
    }

    return;

    static void Validate(string text, SecureString secure, Encoding encoding = null)
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

      Validate(string.Empty, Attributes.EmptySecureString());
      Attributes.RandomString().With(text => Validate(text, new SecureString().With(text)));
    }

    return;

    static void Validate(string result, SecureString secure)
    {
      using (secure)
      {
        secure.ToText().Should().BeOfType<string>().And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.ToBoolean(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBoolean_Method()
  {
    throw new NotImplementedException();
  }
}