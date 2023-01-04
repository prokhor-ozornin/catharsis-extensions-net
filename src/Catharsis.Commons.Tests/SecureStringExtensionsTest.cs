﻿using System.Security;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="SecureStringExtensions"/>.</para>
/// </summary>
public sealed class SecureStringExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.IsEmpty(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.IsEmpty(null)).ThrowExactly<ArgumentNullException>();

    using var secure = EmptySecureString;

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
  public void SecureString_Empty_Method()
  {
    static void Validate(SecureString secure)
    {
      using (secure)
      {
        secure.Empty().Should().NotBeNull().And.BeSameAs(secure);
        secure.Length.Should().Be(0);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.Empty(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptySecureString);
      Validate(RandomSecureString);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.Min(SecureString, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_Min_Method()
  {
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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.Min(null, new SecureString())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new SecureString().Min(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptySecureString, EmptySecureString);
      Validate(EmptySecureString, RandomSecureString);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.Max(SecureString, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_Max_Method()
  {
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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.Max(null, new SecureString())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new SecureString().Max(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptySecureString, EmptySecureString);
      Validate(EmptySecureString, RandomSecureString);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.TryFinallyClear(SecureString, Action{SecureString})"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_TryFinallyClear_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.TryFinallyClear(null, _ => {})).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new SecureString().TryFinallyClear(null)).ThrowExactly<ArgumentNullException>();

    using var secure = new SecureString();

    secure.TryFinallyClear(secure => secure.AppendChar(char.MinValue)).Should().NotBeNull().And.BeSameAs(secure);
    secure.Length.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.AsReadOnly(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_AsReadOnly_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.AsReadOnly(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.ToBytes(SecureString, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_ToBytes_Method()
  {
    static void Validate(Encoding encoding)
    {
      using (var secure = EmptySecureString)
      {
        secure.ToBytes(encoding).Should().BeSameAs(secure.ToBytes(encoding)).And.BeEmpty();
      }

      using (var secure = RandomSecureString)
      {
        var text = secure.ToText();
        secure.ToBytes(encoding).Should().NotBeNull().And.NotBeSameAs(secure.ToBytes(encoding)).And.Equal(text.ToBytes(encoding));
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => SecureStringExtensions.ToText(null));

      Validate(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.ToText(SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_ToText_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.ToText(null));

    using (var secure = new SecureString())
    {
      secure.ToText().Should().BeSameAs(secure.ToText()).And.BeEmpty();
    }

    using (var secure = new SecureString())
    {
      var text = RandomString;

      text.ForEach(secure.AppendChar);
      secure.ToText().Should().NotBeNull().And.NotBeSameAs(secure.ToText()).And.Be(text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.WriteText(SecureString, IEnumerable{char})"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_WriteText_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.WriteText(null, string.Empty));
    AssertionExtensions.Should(() => EmptySecureString.WriteText(null));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="SecureStringExtensions.WriteTo(IEnumerable{char}, SecureString)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => SecureStringExtensions.WriteTo(null, EmptySecureString)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => SecureStringExtensions.WriteTo(string.Empty, null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
}