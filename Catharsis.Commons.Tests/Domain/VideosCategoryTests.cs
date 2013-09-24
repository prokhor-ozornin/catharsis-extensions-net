using System;
using System.Xml.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="VideosCategory"/>.</para>
  /// </summary>
  public sealed class VideosCategoryTests : CategoryUnitTests<VideosCategory>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="VideosCategory.Xml(XElement)"/> method.</para>
    /// </summary>
    [Fact]
    public void Xml_Method()
    {
      Assert.Throws<ArgumentNullException>(() => VideosCategory.Xml(null));

      var xml = new XElement("VideosCategory",
        new XElement("Id", 1),
        new XElement("Language", "language"),
        new XElement("Name", "name"));
      var category = VideosCategory.Xml(xml);
      Assert.True(category.Id == 1);
      Assert.True(category.Description == null);
      Assert.True(category.Language == "language");
      Assert.True(category.Name == "name");
      Assert.True(category.Parent == null);
      Assert.True(new VideosCategory("language", "name") { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(VideosCategory.Xml(category.Xml()).Equals(category));

      xml = new XElement("VideosCategory",
        new XElement("Id", 1),
        new XElement("Description", "description"),
        new XElement("Language", "language"),
        new XElement("Name", "name"),
        new XElement("Parent",
          new XElement("Id", 2),
          new XElement("Language", "parent.language"),
          new XElement("Name", "parent.name")));
      category = VideosCategory.Xml(xml);
      Assert.True(category.Id == 1);
      Assert.True(category.Description == "description");
      Assert.True(category.Language == "language");
      Assert.True(category.Name == "name");
      Assert.True(category.Parent.Id == 2);
      Assert.True(category.Parent.Language == "parent.language");
      Assert.True(category.Parent.Name == "parent.name");
      Assert.True(new VideosCategory("language", "name", new VideosCategory("parent.language", "parent.name") { Id = 2 }, "description") { Id = 1 }.Xml().ToString() == xml.ToString());
      Assert.True(VideosCategory.Xml(category.Xml()).Equals(category));
    }
  }
}