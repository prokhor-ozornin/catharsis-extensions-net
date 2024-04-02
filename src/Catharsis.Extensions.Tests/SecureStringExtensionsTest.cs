using System.Net.Sockets;
using System.Net;
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

      static void Validate(SecureString secure, IEnumerable<char> characters)
      {
        using (secure)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.With(null, Array.Empty<char>())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => Attributes.EmptySecureString().With(null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      static void Validate(SecureString secure, params char[] characters)
      {
        using (secure)
        {

        }
      }
    }

    throw new NotImplementedException();
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
      AssertionExtensions.Should(() => Attributes.EmptySecureString().Without((IEnumerable<int>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      static void Validate(SecureString secure, IEnumerable<int> positions)
      {
        using (secure)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.Without(null, Array.Empty<int>())).ThrowExactly<ArgumentNullException>().WithParameterName("document");
      AssertionExtensions.Should(() => Attributes.EmptySecureString().Without(null)).ThrowExactly<ArgumentNullException>().WithParameterName("characters");

      static void Validate(SecureString secure, params int[] positions)
      {
        using (secure)
        {

        }
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.IsEmpty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.IsEmpty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

    Validate(true, new SecureString());
    Validate(false, new SecureString().With(char.MinValue));

    return;

    static void Validate(bool result, SecureString secure)
    {
      using (secure)
      {
        secure.IsEmpty().Should().Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.Empty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.Empty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

    Validate(Attributes.EmptySecureString());
    Validate(Attributes.RandomSecureString());

    return;

    static void Validate(SecureString secure)
    {
      using (secure)
      {
        secure.Empty().Should().BeOfType<SecureString>().And.BeSameAs(secure);
        secure.Length.Should().Be(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.Min(SecureString, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void Min_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.Min(null, new SecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => new SecureString().Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

    Validate(Attributes.EmptySecureString(), Attributes.EmptySecureString(), Attributes.EmptySecureString());
    Validate(Attributes.EmptySecureString(), Attributes.EmptySecureString(), Attributes.RandomSecureString());

    return;

    static void Validate(SecureString result, SecureString left, SecureString right)
    {
      using (left)
      {
        using (right)
        {
          left.Min(right).Should().BeOfType<SecureString>().And.BeSameAs(result);
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
    AssertionExtensions.Should(() => SecureStringExtensions.Max(null, new SecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => new SecureString().Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

    Validate(Attributes.EmptySecureString(), Attributes.EmptySecureString(), Attributes.EmptySecureString());
    Validate(Attributes.RandomSecureString(), Attributes.EmptySecureString(), Attributes.RandomSecureString());

    return;

    static void Validate(SecureString result, SecureString left, SecureString right)
    {
      using (left)
      {
        using (right)
        {
          left.Max(right).Should().BeOfType<SecureString>().And.BeSameAs(result);
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
    AssertionExtensions.Should(() => SecureStringExtensions.MinMax(null, Attributes.EmptySecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("min");
    AssertionExtensions.Should(() => Attributes.EmptySecureString().MinMax(null)).ThrowExactly<ArgumentNullException>().WithParameterName("max");

    throw new NotImplementedException();

    return;

    static void Validate(SecureString min, SecureString max) => min.MinMax(max).Should().Be((min, max));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.TryFinallyClear(SecureString, Action{SecureString})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.TryFinallyClear(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("secure");
    AssertionExtensions.Should(() => new SecureString().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    Validate(Attributes.EmptySecureString());
    Validate(Attributes.RandomSecureString());

    return;

    static void Validate(SecureString secure)
    {
      using (secure)
      {
        secure.TryFinallyClear(secure => secure.With(char.MinValue)).Should().BeOfType<SecureString>().And.BeSameAs(secure);
        secure.Length.Should().Be(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.AsReadOnly(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsReadOnly_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.AsReadOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

    throw new NotImplementedException();

    return;

    static void Validate(SecureString secure)
    {
      using (secure)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.ToBytes(SecureString, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

    Validate(Attributes.EmptySecureString());
    Encoding.GetEncodings().ForEach(encoding => Validate(Attributes.RandomSecureString(), encoding.GetEncoding()));

    return;

    static void Validate(SecureString secure, Encoding encoding = null)
    {
      using (secure)
      {
        var text = secure.ToText();
        secure.ToBytes(encoding).Should().BeOfType<byte[]>().And.Equal(text.ToBytes(encoding));

        secure.Clear();
        secure.ToBytes(encoding).Should().BeOfType<byte[]>().And.BeSameAs(secure.ToBytes(encoding)).And.BeEmpty();
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.ToText(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

    using (var secure = new SecureString())
    {
      secure.ToText().Should().BeOfType<string>().And.BeSameAs(secure.ToText()).And.BeEmpty();
    }

    using (var secure = new SecureString())
    {
      var text = Attributes.RandomString();

      text.ForEach(secure.AppendChar);
      secure.ToText().Should().BeOfType<string>().And.Be(text);
    }

    throw new NotImplementedException();

    return;

    static void Validate(SecureString secure)
    {
      using (secure)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="SecureStringExtensions.WriteText(SecureString, IEnumerable{char})"/></description></item>
  ///     <item><description><see cref="SecureStringExtensions.WriteText(SecureString, char[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void WriteText_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Attributes.EmptySecureString().WriteText((IEnumerable<char>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      static void Validate(SecureString secure, IEnumerable<char> characters)
      {
        using (secure)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.WriteText(null, Enumerable.Empty<char>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Attributes.EmptySecureString().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      static void Validate(SecureString secure, char[] characters)
      {
        using (secure)
        {

        }
      }
    }

    throw new NotImplementedException();
  }
}