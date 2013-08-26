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
  [EqualsAndHashCode("AuthorId", "Language", "Name")]
  public class Item : EntityBase, ICommentable, IComparable<Item>, IAuthorable, ILocalizable, INameable, ITaggable, ITextable, ITimeable
  {
    private readonly ICollection<Comment> comments = new List<Comment>();
    private readonly ICollection<string> tags = new HashSet<string>();

    private string language;
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string AuthorId { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public ICollection<Comment> Comments
    {
      get { return this.comments; }  
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Language
    {
      get { return this.language; }
      set
      {
        Assertion.NotEmpty(value);

        this.language = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Name
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
    public ICollection<string> Tags
    {
      get { return  this.tags; }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///   <para>Creates new item.</para>
    /// </summary>
    public Item()
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para>Creates new item with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on item after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Item(IDictionary<string, object> properties) : this()
    {
      Assertion.NotNull(properties);

      this.SetProperties(properties);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="authorId"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public Item(string id, string language, string name, string text = null, string authorId = null) : this()
    {
      this.Id = id;
      this.Language = language;
      this.Name = name;
      this.Text = text;
      this.AuthorId = authorId;
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Item Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var item = new Item((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (string) xml.Element("AuthorId"));
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
    ///   <para>Returns a <see cref="string"/> that represents the current item.</para>
    /// </summary>
    /// <returns>A string that represents the current item.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int CompareTo(Item item)
    {
      return this.DateCreated.CompareTo(item.DateCreated);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.AuthorId != null ? new XElement("AuthorId", this.AuthorId) : null,
        this.Comments.Count > 0 ? new XElement("Comments", this.Comments.Select(comment => comment.Xml())) : null,
        new XElement("DateCreated", this.DateCreated.ToRFC1123()),
        new XElement("Language", this.Language),
        new XElement("LastUpdated", this.LastUpdated.ToRFC1123()),
        new XElement("Name", this.Name),
        this.Tags.Count > 0 ? new XElement("Tags", this.Tags.Select(tag => new XElement("Tag", tag))) : null,
        this.Text !=null ? new XElement("Text", this.Text) : null);
    }
  }
}