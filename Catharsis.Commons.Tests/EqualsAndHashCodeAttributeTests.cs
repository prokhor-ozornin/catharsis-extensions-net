using System;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="EqualsAndHashCodeAttribute"/>.</para>
  /// </summary>
  public sealed class EqualsAndHashCodeAttributeTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="EqualsAndHashCodeAttribute(string)"/>
    [Fact]
    public void Constructors()
    {
      Assert.Throws<ArgumentNullException>(() => new EqualsAndHashCodeAttribute(null));
      Assert.Throws<ArgumentException>(() => new EqualsAndHashCodeAttribute(string.Empty));
      Assert.True(new EqualsAndHashCodeAttribute("property").Properties.SequenceEqual(new [] { "property" }));
      Assert.True(new EqualsAndHashCodeAttribute("first,second").Properties.SequenceEqual(new[] { "first", "second" }));
    }
  }
}