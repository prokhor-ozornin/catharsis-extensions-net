using System;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class AssertionTest
  {
    [Fact]
    public void assert_default()
    {
      0.Default();
      Assert.Throws<ArgumentException>(() => 1.Default());
      default(DateTime).Default();
      Assert.Throws<ArgumentException>(() => DateTime.UtcNow.Default());
    }

    [Fact]
    public void assert_empty()
    {
      Assertion.Empty(null);
      Assertion.Empty(Enumerable.Empty<object>());
      Assertion.Empty(new object[] {});
      Assert.Throws<ArgumentException>(() => Assertion.Empty(new [] { new object() }));
    }

    [Fact]
    public void assert_equal()
    {
      Assertion.Equal(string.Empty, string.Empty);
      Assert.Throws<ArgumentException>(() => Assertion.Equal(null, null));
      Assert.Throws<ArgumentException>(() => Assertion.Equal(null, new object()));
      Assert.Throws<ArgumentException>(() => Assertion.Equal(new object(), null));
      Assert.Throws<ArgumentException>(() => Assertion.Equal(new object(), new object()));
    }

    [Fact]
    public void assert_false()
    {
      Assert.Throws<ArgumentException>(() => Assertion.False(true));
      Assertion.False(false);
    }

    [Fact]
    public void assert_not_empty()
    {
      Assert.Throws<ArgumentNullException>(() => Assertion.NotEmpty(null));
      Assert.Throws<ArgumentException>(() => Assertion.NotEmpty(Enumerable.Empty<object>()));
      Assert.Throws<ArgumentException>(() => Assertion.NotEmpty(new object[] { }));
      Assertion.NotEmpty(new[] { new object() });
    }

    [Fact]
    public void assert_not_equal()
    {
      Assert.Throws<ArgumentException>((() => Assertion.NotEqual(string.Empty, string.Empty)));
      Assertion.NotEqual(null, null);
      Assertion.NotEqual(null, new object());
      Assertion.NotEqual(new object(), null);
      Assertion.NotEqual(new object(), new object());
    }
    
    [Fact]
    public void assert_not_null()
    {
      Assert.Throws<ArgumentNullException>(() => Assertion.NotNull(null));
      Assertion.NotNull(new object());
    }

    [Fact]
    public void assert_not_whitespace()
    {
      Assert.Throws<ArgumentNullException>(() => Assertion.NotWhitespace(null));
      Assert.Throws<ArgumentException>(() => Assertion.NotWhitespace(string.Empty));
      Assert.Throws<ArgumentException>(() => Assertion.NotWhitespace(" "));
      Assertion.NotWhitespace(" a ");
      Assertion.NotWhitespace("test");
    }

    [Fact]
    public void assert_null()
    {
      Assertion.Null(null);
      Assert.Throws<ArgumentException>(() => Assertion.Null(new object()));
    }

    [Fact]
    public void assert_true()
    {
      Assertion.True(true);
      Assert.Throws<ArgumentException>(() => Assertion.True(false));
    }

    [Fact]
    public void assert_whitespace()
    {
      Assertion.Whitespace(null);
      Assertion.Whitespace(string.Empty);
      Assertion.Whitespace(" ");
      Assert.Throws<ArgumentException>(() => Assertion.Whitespace(" a "));
      Assert.Throws<ArgumentException>(() => Assertion.Whitespace("test"));
    }
  }
}