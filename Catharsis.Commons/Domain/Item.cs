using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("AuthorId,Language,Name")]
  public class Item : EntityBase, ICommentable, IComparable<Item>, IEquatable<Item>, IAuthorable, ILocalizable, INameable, ITaggable, ITextable, ITimeable
  {
    private ICollection<Comment> comments = new List<Comment>();
    private ICollection<string> tags = new HashSet<string>();
    private DateTime dateCreated = DateTime.UtcNow;
    private string language;
    private DateTime lastUpdated = DateTime.UtcNow;
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual string AuthorId { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual ICollection<Comment> Comments
    {
      get { return this.comments; }  
    }

    /// <summary>
    ///   <para>Date and time of item's creation.</para>
    /// </summary>
    public virtual DateTime DateCreated
    {
      get { return this.dateCreated; }
      set { this.dateCreated = value; }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Language
    {
      get { return this.language; }
      set
      {
        Assertion.NotEmpty(value);

        this.language = value;
      }
    }
    
    /// <summary>
    ///   <para>Date and time of item's last modification.</para>
    /// </summary>
    public virtual DateTime LastUpdated
    {
      get { return this.lastUpdated; }
      set { this.lastUpdated = value; }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Name
    {
      get { return this.name; }
      set
      {
        Assertion.NotEmpty(value);

        this.name = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual ICollection<string> Tags
    {
      get { return  this.tags; }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual string Text { get; set; }

    /// <summary>
    ///   <para>Creates new item.</para>
    /// </summary>
    public Item()
    {
    }

    /// <summary>
    ///   <para>Creates new item with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on item after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Item(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new item.</para>
    /// </summary>
    /// <param name="language">ISO language code of item's text content.</param>
    /// <param name="name">Name of item.</param>
    /// <param name="text">Item's content text.</param>
    /// <param name="authorId">Identifier of item's author.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public Item(string language, string name, string text = null, string authorId = null)
    {
      this.Language = language;
      this.Name = name;
      this.Text = text;
      this.AuthorId = authorId;
    }

    /// <summary>
    ///   <para>Creates new item from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Item"/> type.</param>
    /// <returns>Recreated item object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Item Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var item = new Item((string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (string) xml.Element("AuthorId"));
      if (xml.Element("Id") != null)
      {
        item.Id = (long) xml.Element("Id");
      }
      if (xml.Element("Comments") != null)
      {
        item.Comments.AddAll(xml.Element("Comments").Descendants().Select(Comment.Xml));
      }
      if (xml.Element("DateCreated") != null)
      {
        item.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        item.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      if (xml.Element("Tags") != null)
      {
        item.Tags.AddAll(xml.Element("Tags").Descendants().Select(tag => (string) tag));
      }
      return item;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Item other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current item.</para>
    /// </summary>
    /// <returns>A string that represents the current item.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para>Compares the current item with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Item"/> to compare with this instance.</param>
    public virtual int CompareTo(Item other)
    {
      return this.DateCreated.CompareTo(other.DateCreated);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Item"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.AuthorId != null ? new XElement("AuthorId", this.AuthorId) : null,
        this.Comments.Count > 0 ? new XElement("Comments", this.Comments.Select(comment => comment.Xml())) : null,
        new XElement("DateCreated", this.DateCreated.ToRfc1123()),
        new XElement("Language", this.Language),
        new XElement("LastUpdated", this.LastUpdated.ToRfc1123()),
        new XElement("Name", this.Name),
        this.Tags.Count > 0 ? new XElement("Tags", this.Tags.Select(tag => new XElement("Tag", tag))) : null,
        this.Text !=null ? new XElement("Text", this.Text) : null);
    }
  }
}