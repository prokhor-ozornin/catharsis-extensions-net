using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Assertion"/>.</para>
  /// </summary>
  public sealed class AssertionTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Assertion.Empty(IEnumerable, string)"/></description></item>
    ///     <item><description><see cref="Assertion.Empty{T}(IEnumerable{T}, string)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Empty_Methods()
    {
      Assertion.Empty(null);
      Assertion.Empty(Enumerable.Empty<object>());
      Assertion.Empty(new object[] {});
      Assert.Throws<ArgumentException>(() => Assertion.Empty(new [] { new object() }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Assertion.Equal(object, object, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Equal_Method()
    {
      Assertion.Equal(string.Empty, string.Empty);
      Assert.Throws<ArgumentException>(() => Assertion.Equal(null, null));
      Assert.Throws<ArgumentException>(() => Assertion.Equal(null, new object()));
      Assert.Throws<ArgumentException>(() => Assertion.Equal(new object(), null));
      Assert.Throws<ArgumentException>(() => Assertion.Equal(new object(), new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Assertion.False(bool, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void False_Method()
    {
      Assert.Throws<ArgumentException>(() => Assertion.False(true));
      Assertion.False(false);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Assertion.NotEmpty(IEnumerable, string)"/></description></item>
    ///     <item><description><see cref="Assertion.NotEmpty{T}(IEnumerable{T}, string)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void NotEmpty_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Assertion.NotEmpty(null));
      Assert.Throws<ArgumentException>(() => Assertion.NotEmpty(Enumerable.Empty<object>()));
      Assert.Throws<ArgumentException>(() => Assertion.NotEmpty(new object[] { }));
      Assertion.NotEmpty(new[] { new object() });
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Assertion.NotEqual(object, object, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void NotEqual_Method()
    {
      Assert.Throws<ArgumentException>((() => Assertion.NotEqual(string.Empty, string.Empty)));
      Assertion.NotEqual(null, null);
      Assertion.NotEqual(null, new object());
      Assertion.NotEqual(new object(), null);
      Assertion.NotEqual(new object(), new object());
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="Assertion.NotNull(object, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void NotNull_Method()
    {
      Assert.Throws<ArgumentNullException>(() => Assertion.NotNull(null));
      Assertion.NotNull(new object());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Assertion.NotWhitespace(string, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void NotWhitespace_Method()
    {
      Assert.Throws<ArgumentNullException>(() => Assertion.NotWhitespace(null));
      Assert.Throws<ArgumentException>(() => Assertion.NotWhitespace(string.Empty));
      Assert.Throws<ArgumentException>(() => Assertion.NotWhitespace(" "));
      Assertion.NotWhitespace(" a ");
      Assertion.NotWhitespace("test");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Assertion.Null(object, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Null_Method()
    {
      Assertion.Null(null);
      Assert.Throws<ArgumentException>(() => Assertion.Null(new object()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Assertion.True(bool, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void True_Method()
    {
      Assertion.True(true);
      Assert.Throws<ArgumentException>(() => Assertion.True(false));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Assertion.Whitespace(string, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Whitespace_Method()
    {
      Assertion.Whitespace(null);
      Assertion.Whitespace(string.Empty);
      Assertion.Whitespace(" ");
      Assert.Throws<ArgumentException>(() => Assertion.Whitespace(" a "));
      Assert.Throws<ArgumentException>(() => Assertion.Whitespace("test"));
    }
  }
}