using System;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="EnumExtensions"/>.</para>
  /// </summary>
  public sealed class EnumExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="EnumExtensions.Descriptions{ENUM}()"/> method.</para>
    /// </summary>
    [Fact]
    public void Descriptions_Method()
    {
      Assert.Throws<ArgumentException>(() => EnumExtensions.Descriptions<DateTime>());
      var descriptions = EnumExtensions.Descriptions<MockEnumeration>().ToArray();
      Assert.Equal(3, descriptions.Count());
      Assert.Equal("FirstOption", descriptions[0]);
      Assert.Equal("Second", descriptions[1]);
      Assert.Equal("ThirdOption", descriptions[2]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="EnumExtensions.Description(Enum)"/> method.</para>
    /// </summary>
    [Fact]
    public void Description()
    {
      Assert.Equal("FirstOption", MockEnumeration.First.Description());
      Assert.Null(MockEnumeration.Second.Description());
      Assert.Equal("ThirdOption", MockEnumeration.Third.Description());
    }

    [Description("Enumeration")]
    private enum MockEnumeration
    {
      [Description("FirstOption")]
      First,

      Second,

      [Description("ThirdOption")]
      Third
    }
  }
}