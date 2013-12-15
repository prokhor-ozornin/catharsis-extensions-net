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
      Assert.True(descriptions.Count() == 3);
      Assert.True(descriptions[0] == "FirstOption");
      Assert.True(descriptions[1] == "Second");
      Assert.True(descriptions[2] == "ThirdOption");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="EnumExtensions.Description(Enum)"/> method.</para>
    /// </summary>
    [Fact]
    public void Description()
    {
      Assert.True(MockEnumeration.First.Description() == "FirstOption");
      Assert.True(MockEnumeration.Second.Description() == null);
      Assert.True(MockEnumeration.Third.Description() == "ThirdOption");
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