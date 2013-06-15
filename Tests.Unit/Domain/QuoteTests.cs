using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Quote"/>.</para>
  /// </summary>
  public sealed class QuoteTests : EntityUnitTests<Quote>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Quote()"/>
    ///   <seealso cref="Quote(IDictionary{string, object})"/>
    ///   <seealso cref="Quote(string, string, string, string, int)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var quote = new Quote();
      Assert.True(quote.Id == null);
      Assert.True(quote.AuthorId == null);
      Assert.True(quote.Comments.Count == 0);
      Assert.True(quote.DateCreated <= DateTime.UtcNow);
      Assert.True(quote.Language == null);
      Assert.True(quote.LastUpdated <= DateTime.UtcNow);
      Assert.True(quote.Name == null);
      Assert.True(quote.Tags.Count == 0);
      Assert.True(quote.Text == null);
      Assert.True(quote.Type == 0);

      quote = new Quote(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Type", 1));
      Assert.True(quote.Id == "id");
      Assert.True(quote.AuthorId == null);
      Assert.True(quote.Comments.Count == 0);
      Assert.True(quote.DateCreated <= DateTime.UtcNow);
      Assert.True(quote.Language == "language");
      Assert.True(quote.LastUpdated <= DateTime.UtcNow);
      Assert.True(quote.Name == "name");
      Assert.True(quote.Tags.Count == 0);
      Assert.True(quote.Text == "text");
      Assert.True(quote.Type == 1);

      quote = new Quote("id", "language", "name", "text", 1);
      Assert.True(quote.Id == "id");
      Assert.True(quote.AuthorId == null);
      Assert.True(quote.Comments.Count == 0);
      Assert.True(quote.DateCreated <= DateTime.UtcNow);
      Assert.True(quote.Language == "language");
      Assert.True(quote.LastUpdated <= DateTime.UtcNow);
      Assert.True(quote.Name == "name");
      Assert.True(quote.Tags.Count == 0);
      Assert.True(quote.Text == "text");
      Assert.True(quote.Type == 1);
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Type", new[] { (object) 1, (object) 2 }));
    }
  }
}