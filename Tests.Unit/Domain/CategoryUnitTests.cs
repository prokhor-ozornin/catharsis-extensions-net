using System;
using System.Collections.Generic;
using System.Reflection;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="CATEGORY"></typeparam>
  public abstract class CategoryUnitTests<CATEGORY> : EntityUnitTests<CATEGORY> where CATEGORY : Category
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var category = typeof(CATEGORY).NewInstance().To<CATEGORY>();
      Assert.True(category.Id == null);
      Assert.True(category.Language == null);
      Assert.True(category.Name == null);
      Assert.True(category.Parent == null);
      Assert.True(category.Description == null);

      Assert.Throws<ArgumentNullException>(() => typeof(CATEGORY).NewInstance((IDictionary<string, object>) null));
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

      Assert.Throws<TargetInvocationException>(() => typeof(CATEGORY).NewInstance((string) null, "language", "name", null, null));
      Assert.Throws<TargetInvocationException>(() => typeof(CATEGORY).NewInstance("id", (string)null, "name", null, null));
      Assert.Throws<TargetInvocationException>(() => typeof(CATEGORY).NewInstance("id", "language", null, null, null));
      Assert.Throws<TargetInvocationException>(() => typeof(CATEGORY).NewInstance(string.Empty, "language", "name", null, null));
      Assert.Throws<TargetInvocationException>(() => typeof(CATEGORY).NewInstance("id", string.Empty, "name", null, null));
      Assert.Throws<TargetInvocationException>(() => typeof(CATEGORY).NewInstance("id", "language", string.Empty, null, null));
      category = typeof(CATEGORY).NewInstance("id", "language", "name", typeof(CATEGORY).NewInstance().To<CATEGORY>(), "description").To<CATEGORY>();
      Assert.True(category.Id == "id");
      Assert.True(category.Language == "language");
      Assert.True(category.Name == "name");
      Assert.True(category.Parent != null);
      Assert.True(category.Description == "description");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Category.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      var category = typeof(CATEGORY).NewInstance().To<CATEGORY>();
      category.Name = "Name";
      Assert.True(category.ToString() == "Name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Category"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Name", new[] { "Name", "Name_2" })
        .AddNext("Parent", new[] { new ArticlesCategory { Name = "Name" }, new ArticlesCategory { Name = "Name_2" }}));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Category.CompareTo(Category)"/> method.</para>
    /// </summary>
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