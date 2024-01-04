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
  ///   <para>Performs testing of <see cref="SecureStringExtensions.IsEmpty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.IsEmpty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("secure");

    using var secure = Attributes.EmptySecureString();

    secure.IsEmpty().Should().BeTrue();

    secure.AppendChar(char.MinValue);
    secure.IsEmpty().Should().BeFalse();

    secure.RemoveAt(0);
    secure.IsEmpty().Should().BeTrue();

    secure.AppendChar(char.MinValue);
    secure.Clear();
    secure.IsEmpty().Should().BeTrue();
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
        secure.Empty().Should().NotBeNull().And.BeSameAs(secure);
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
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.Min(null, new SecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => new SecureString().Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(Attributes.EmptySecureString(), Attributes.EmptySecureString());
      Validate(Attributes.EmptySecureString(), Attributes.RandomSecureString());
    }

    return;

    static void Validate(SecureString min, SecureString max)
    {
      using (min)
      {
        using (max)
        {
          min.Min(min).Should().NotBeNull().And.BeSameAs(min);
          max.Min(max).Should().NotBeNull().And.BeSameAs(max);
          min.Min(max).Should().NotBeNull().And.BeSameAs(min);
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
      AssertionExtensions.Should(() => SecureStringExtensions.Max(null, new SecureString())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => new SecureString().Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

      Validate(Attributes.EmptySecureString(), Attributes.EmptySecureString());
      Validate(Attributes.EmptySecureString(), Attributes.RandomSecureString());
    }

    return;

    static void Validate(SecureString min, SecureString max)
    {
      using (min)
      {
        using (max)
        {
          min.Max(min).Should().NotBeNull().And.BeSameAs(min);
          max.Max(max).Should().NotBeNull().And.BeSameAs(max);
          max.Max(min).Should().NotBeNull().And.BeSameAs(max);
        }
      }
    }
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
        secure.TryFinallyClear(_ => { }).Should().NotBeNull().And.BeSameAs(secure);
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

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }

    return;

    void Validate(Encoding encoding)
    {
      using (var secure = Attributes.EmptySecureString())
      {
        secure.ToBytes(encoding).Should().BeSameAs(secure.ToBytes(encoding)).And.BeEmpty();
      }

      using (var secure = Attributes.RandomSecureString())
      {
        var text = secure.ToText();
        secure.ToBytes(encoding).Should().NotBeNull().And.NotBeSameAs(secure.ToBytes(encoding)).And.Equal(text.ToBytes(encoding));
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
      secure.ToText().Should().BeSameAs(secure.ToText()).And.BeEmpty();
    }

    using (var secure = new SecureString())
    {
      var text = Attributes.RandomString();

      text.ForEach(secure.AppendChar);
      secure.ToText().Should().NotBeNull().And.NotBeSameAs(secure.ToText()).And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.WriteText(SecureString, IEnumerable{char})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Attributes.EmptySecureString().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }
}