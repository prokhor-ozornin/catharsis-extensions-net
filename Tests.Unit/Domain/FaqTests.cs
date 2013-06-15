using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Faq"/>.</para>
  /// </summary>
  public sealed class FaqTests : EntityUnitTests<Faq>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Faq()"/>
    ///   <seealso cref="Faq(IDictionary{string, object})"/>
    ///   <seealso cref="Faq(string, string, string, string)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var faq = new Faq();
      Assert.True(faq.Id == null);
      Assert.True(faq.AuthorId == null);
      Assert.True(faq.Comments.Count == 0);
      Assert.True(faq.DateCreated <= DateTime.UtcNow);
      Assert.True(faq.Language == null);
      Assert.True(faq.LastUpdated <= DateTime.UtcNow);
      Assert.True(faq.Name == null);
      Assert.True(faq.Tags.Count == 0);
      Assert.True(faq.Text == null);

      faq = new Faq(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text"));
      Assert.True(faq.Id == "id");
      Assert.True(faq.AuthorId == null);
      Assert.True(faq.Comments.Count == 0);
      Assert.True(faq.DateCreated <= DateTime.UtcNow);
      Assert.True(faq.Language == "language");
      Assert.True(faq.LastUpdated <= DateTime.UtcNow);
      Assert.True(faq.Name == "name");
      Assert.True(faq.Tags.Count == 0);
      Assert.True(faq.Text == "text");

      faq = new Faq("id", "language", "name", "text");
      Assert.True(faq.Id == "id");
      Assert.True(faq.AuthorId == null);
      Assert.True(faq.Comments.Count == 0);
      Assert.True(faq.DateCreated <= DateTime.UtcNow);
      Assert.True(faq.Language == "language");
      Assert.True(faq.LastUpdated <= DateTime.UtcNow);
      Assert.True(faq.Name == "name");
      Assert.True(faq.Tags.Count == 0);
      Assert.True(faq.Text == "text");
    }
  }
}