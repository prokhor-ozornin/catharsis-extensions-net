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
    ///   <seealso cref="EqualsAndHashCodeAttribute(string[])"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      Assert.True(!new EqualsAndHashCodeAttribute().Properties.Any());
      Assert.True(!new EqualsAndHashCodeAttribute(Enumerable.Empty<string>().ToArray()).Properties.Any());
      Assert.True(new EqualsAndHashCodeAttribute(string.Empty).Properties.SequenceEqual(new [] { string.Empty } ));
      Assert.True(new EqualsAndHashCodeAttribute("first", "second").Properties.SequenceEqual(new [] { "first", "second" }));
    }
  }
}