using System;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class EqualsAndHashCodeAttributeTest
  {
    [Fact]
    public void constructors()
    {
      Assert.Throws<ArgumentNullException>(() => new EqualsAndHashCodeAttribute(null));
      Assert.Throws<ArgumentException>(() => new EqualsAndHashCodeAttribute(string.Empty));
      Assert.True(new EqualsAndHashCodeAttribute("property").Properties.SequenceEqual(new [] { "property" }));
      Assert.True(new EqualsAndHashCodeAttribute("first,second").Properties.SequenceEqual(new[] { "first", "second" }));
    }
  }
}