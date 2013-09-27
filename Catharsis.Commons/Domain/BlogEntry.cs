using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Blog")]
  public class BlogEntry : Item, IComparable<BlogEntry>, IEquatable<BlogEntry>
  {
    private Blog blog;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public virtual Blog Blog
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
    ///   <para>Creates new blog entry.</para>
    /// </summary>
    /// <param name="language">ISO language code of entry's text content.</param>
    /// <param name="name">Name of entry.</param>
    /// <param name="text">Entry's body text.</param>
    /// <param name="blog"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="blog"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public BlogEntry(string language, string name, string text, Blog blog) : base(language, name, text, null)
    {
      Assertion.NotEmpty(text);

      this.Blog = blog;
    }

    /// <summary>
    ///   <para>Creates new blog entry from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="BlogEntry"/> type.</param>
    /// <returns>Recreated blog entry object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static BlogEntry Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var entry = new BlogEntry((string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), Blog.Xml(xml.Element("Blog")));
      if (xml.Element("Id") != null)
      {
        entry.Id = (long) xml.Element("Id");
      }
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
    ///   <para>Compares the current blog entry with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="BlogEntry"/> to compare with this instance.</param>
    public virtual int CompareTo(BlogEntry other)
    {
      return base.CompareTo(other);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="BlogEntry"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Blog.Xml());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(BlogEntry other)
    {
      return base.Equals(other);
    }
  }
}