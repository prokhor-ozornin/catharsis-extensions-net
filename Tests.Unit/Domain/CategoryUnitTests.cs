using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  public abstract class CategoryUnitTests<CATEGORY> : EntityUnitTests<CATEGORY> where CATEGORY : Category
  {
    [Fact]
    public void Constructors()
    {
      var category = typeof(CATEGORY).NewInstance().To<CATEGORY>();
      Assert.True(category.Id == null);
      Assert.True(category.Language == null);
      Assert.True(category.Name == null);
      Assert.True(category.Parent == null);
      Assert.True(category.Description == null);

      category = typeof(CATEGORY).NewInstance(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Parent", new ArticlesCategory())
        .AddNext("Description", "description")).To<CATEGORY>();
      Assert.True(category.Id == "id", "ID = " + category.Id);
      Assert.True(category.Language == "language");
      Assert.True(category.Name == "name");
      Assert.True(category.Parent != null);
      Assert.True(category.Description == "description");

      category = typeof(CATEGORY).NewInstance("id", "language", "name", typeof(CATEGORY).NewInstance().To<CATEGORY>(), "description").To<CATEGORY>();
      Assert.True(category.Id == "id");
      Assert.True(category.Language == "language");
      Assert.True(category.Name == "name");
      Assert.True(category.Parent != null);
      Assert.True(category.Description == "description");
    }

    [Fact]
    public void ToString_Method()
    {
      var category = typeof(CATEGORY).NewInstance().To<CATEGORY>();
      category.Name = "Name";
      Assert.True(category.ToString() == "Name");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Name", new[] { "Name", "Name_2" })
        .AddNext("Parent", new[] { new ArticlesCategory { Name = "Name" }, new ArticlesCategory { Name = "Name_2" }}));
    }

    [Fact]
    public void CompareTo_Method()
    {
      var first = typeof(CATEGORY).NewInstance().To<CATEGORY>();
      first.Name = "Name";

      var second = typeof(CATEGORY).NewInstance().To<CATEGORY>();
      second.Name = "Name";

      Assert.True(first.CompareTo(second) == 0);

      first.Name = "a";
      second.Name = "b";
      Assert.True(first.CompareTo(second) < 0);
    }
  }
}