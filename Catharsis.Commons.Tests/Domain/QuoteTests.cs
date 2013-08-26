using System;
using System.Collections.Generic;
using System.Xml.Linq;
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
    ///   <para>Performs testing of <see cref="Quote.Type"/> property.</para>
    /// </summary>
    [Fact]
    public void Type_Property()
    {
      Assert.True(new Quote { Type = 1 }.Type == 1);
    }

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
      Assert.True(quote.DateCreated <= DateTime.UtcNow);
      Assert.True(quote.Language == null);
      Assert.True(quote.LastUpdated <= DateTime.UtcNow);
      Assert.True(quote.Name == null);
      Assert.True(quote.Text == null);
      Assert.True(quote.Type == 0);

      Assert.Throws<ArgumentNullException>(() => new Quote(null));
      quote = new Quote(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text")
        .AddNext("Type", 1));
      Assert.True(quote.Id == "id");
      Assert.True(quote.AuthorId == null);
      Assert.True(quote.DateCreated <= DateTime.UtcNow);
      Assert.True(quote.Language == "language");
      Assert.True(quote.LastUpdated <= DateTime.UtcNow);
      Assert.True(quote.Name == "name");
      Assert.True(quote.Text == "text");
      Assert.True(quote.Type == 1);

      Assert.Throws<ArgumentNullException>(() => new Quote(null, "language", "name", "text"));
      Assert.Throws<ArgumentNullException>(() => new Quote("id", null, "name", "text"));
      Assert.Throws<ArgumentNullException>(() => new Quote("id", "language", null, "text"));
      Assert.Throws<ArgumentNullException>(() => new Quote("id", "language", "name", null));
      Assert.Throws<ArgumentException>(() => new Quote(string.Empty, "language", "name", "text"));
      Assert.Throws<ArgumentException>(() => new Quote("id", string.Empty, "name", "text"));
      Assert.Throws<ArgumentException>(() => new Quote("id", "language", string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => new Quote("id", "language", "name", string.Empty));
      quote = new Quote("id", "language", "name", "text", 1);
      Assert.True(quote.Id == "id");
      Assert.True(quote.AuthorId == null);
      Assert.True(quote.DateCreated <= DateTime.UtcNow);
      Assert.True(quote.Language == "language");
      Assert.True(quote.LastUpdated <= DateTime.UtcNow);
      Assert.True(quote.Name == "name");
      Assert.True(quote.Text == "text");
      Assert.True(quote.Type == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode()"/> methods for the <see cref="Quote"/> type.</para>
    /// </summary>
    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Type", new[] { (object) 1, (object) 2 }));
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Quote.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Quote.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Quote.Xml(null));

      var xml = new XElement("Quote",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRFC1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRFC1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"),
        new XElement("Type", 1));
      var quote = Quote.Xml(xml);
      Assert.True(quote.Id == "id");
      Assert.True(quote.AuthorId == null);
      Assert.True(quote.Comments.Count == 0);
      Assert.True(quote.DateCreated.ToRFC1123() == DateTime.MinValue.ToRFC1123());
      Assert.True(quote.Language == "language");
      Assert.True(quote.LastUpdated.ToRFC1123() == DateTime.MaxValue.ToRFC1123());
      Assert.True(quote.Name == "name");
      Assert.True(quote.Tags.Count == 0);
      Assert.True(quote.Text == "text");
      Assert.True(quote.Type == 1);
      Assert.True(new Quote("id", "language", "name", "text", 1) { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Quote.Xml(quote.Xml()).Equals(quote));
    }
  }
}