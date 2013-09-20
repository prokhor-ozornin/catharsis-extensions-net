using System;
using System.Collections.Generic;
using System.Xml.Linq;
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
      Assert.True(faq.DateCreated <= DateTime.UtcNow);
      Assert.True(faq.Language == null);
      Assert.True(faq.LastUpdated <= DateTime.UtcNow);
      Assert.True(faq.Name == null);
      Assert.True(faq.Text == null);

      Assert.Throws<ArgumentNullException>(() => new Faq(null));
      faq = new Faq(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Language", "language")
        .AddNext("Name", "name")
        .AddNext("Text", "text"));
      Assert.True(faq.Id == "id");
      Assert.True(faq.AuthorId == null);
      Assert.True(faq.DateCreated <= DateTime.UtcNow);
      Assert.True(faq.Language == "language");
      Assert.True(faq.LastUpdated <= DateTime.UtcNow);
      Assert.True(faq.Name == "name");
      Assert.True(faq.Text == "text");

      Assert.Throws<ArgumentNullException>(() => new Faq(null, "language", "name", "text"));
      Assert.Throws<ArgumentNullException>(() => new Faq("id", null, "name", "text"));
      Assert.Throws<ArgumentNullException>(() => new Faq("id", "language", null, "text"));
      Assert.Throws<ArgumentNullException>(() => new Faq("id", "language", "name", null));
      Assert.Throws<ArgumentException>(() => new Faq(string.Empty, "language", "name", "text"));
      Assert.Throws<ArgumentException>(() => new Faq("id", string.Empty, "name", "text"));
      Assert.Throws<ArgumentException>(() => new Faq("id", "language", string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => new Faq("id", "language", "name", string.Empty));
      faq = new Faq("id", "language", "name", "text");
      Assert.True(faq.Id == "id");
      Assert.True(faq.AuthorId == null);
      Assert.True(faq.DateCreated <= DateTime.UtcNow);
      Assert.True(faq.Language == "language");
      Assert.True(faq.LastUpdated <= DateTime.UtcNow);
      Assert.True(faq.Name == "name");
      Assert.True(faq.Text == "text");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Faq.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="Faq.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => Faq.Xml(null));

      var xml = new XElement("Faq",
        new XElement("Id", "id"),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("Language", "language"),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("Text", "text"));
      var faq = Faq.Xml(xml);
      Assert.True(faq.Id == "id");
      Assert.True(faq.AuthorId == null);
      Assert.True(faq.Comments.Count == 0);
      Assert.True(faq.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(faq.Language == "language");
      Assert.True(faq.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(faq.Name == "name");
      Assert.True(faq.Tags.Count == 0);
      Assert.True(faq.Text == "text");
      Assert.True(new Faq("id", "language", "name", "text") { DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(Faq.Xml(faq.Xml()).Equals(faq));
    }
  }
}