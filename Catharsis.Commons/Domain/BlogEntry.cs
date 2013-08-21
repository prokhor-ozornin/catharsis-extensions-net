using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [Serializable]
  [EqualsAndHashCode("Blog")]
  public class BlogEntry : Item, IComparable<BlogEntry>
  {
    private Blog blog;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public Blog Blog
    {
      get { return this.blog; }
      set
      {
        Assertion.NotNull(value);

        this.blog = value;
      }
    }

    /// <summary>
    ///   <para>Creates new blog entry.</para>
    /// </summary>
    public BlogEntry()
    {
    }

    /// <summary>
    ///   <para>Creates new blog entry with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on entry after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public BlogEntry(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="blog"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="blog"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public BlogEntry(string id, string language, string name, string text, Blog blog) : base(id, language, name, text, null)
    {
      Assertion.NotEmpty(text);

      this.Blog = blog;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static BlogEntry Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var entry = new BlogEntry((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), Blog.Xml(xml.Element("Blog")));
      if (xml.Element("DateCreated") != null)
      {
        entry.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        entry.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return entry;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public int CompareTo(BlogEntry entry)
    {
      return base.CompareTo(entry);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(this.Blog.Xml());
    }
  }
}