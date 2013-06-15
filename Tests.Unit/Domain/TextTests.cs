using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Text"/>.</para>
  /// </summary>
  public sealed class TextTests : EntityUnitTests<Text>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Blog()"/>
    ///   <seealso cref="Blog(IDictionary{string, object})"/>
    ///   <seealso cref="Blog(string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var text = new Text();
      Assert.True(text.Id == null);
      Assert.True(text.AuthorId == null);
      Assert.True(text.Comments.Count == 0);
      Assert.True(text.DateCreated <= DateTime.UtcNow);
      Assert.True(text.Language == null);
      Assert.True(text.LastUpdated <= DateTime.UtcNow);
      Assert.True(text.Name == null);
      Assert.True(text.Tags.Count == 0);
      Assert.True(text.Text == null);
      Assert.True(text.Category == null);
      Assert.True(text.Person == null);

      text = new Text(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("AuthorId", "authorId")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Category", new TextsCategory())
        .AddNext("Person", new Person()));
      Assert.True(text.Id == "id");
      Assert.True(text.AuthorId == "authorId");
      Assert.True(text.Comments.Count == 0);
      Assert.True(text.DateCreated <= DateTime.UtcNow);
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated <= DateTime.UtcNow);
      Assert.True(text.Name == "name");
      Assert.True(text.Tags.Count == 0);
      Assert.True(text.Text == "text");
      Assert.True(text.Category != null);
      Assert.True(text.Person != null);

      text = new Text("id", "authorId", "language", "name", "text", new TextsCategory(), new Person());
      Assert.True(text.Id == "id");
      Assert.True(text.AuthorId == "authorId");
      Assert.True(text.Comments.Count == 0);
      Assert.True(text.DateCreated <= DateTime.UtcNow);
      Assert.True(text.Language == "language");
      Assert.True(text.LastUpdated <= DateTime.UtcNow);
      Assert.True(text.Name == "name");
      Assert.True(text.Tags.Count == 0);
      Assert.True(text.Text == "text");
      Assert.True(text.Category != null);
      Assert.True(text.Person != null);
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new TextsCategory { Name = "Name" }, new TextsCategory { Name = "Name_2" } })
        .AddNext("Person", new[] { new Person { NameFirst = "NameFirst" }, new Person { NameFirst = "NameFirst_2" } }));
    }
  }
}