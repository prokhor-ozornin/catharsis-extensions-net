using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("AuthorId", "Language", "Name")]
  public class Item : EntityBase, IComparable<Item>, IAuthorable, ICommentable, ILocalizable, INameable, ITaggable, ITextable, ITimeable
  {
    private ICollection<Comment> comments = new List<Comment>();
    private ICollection<string> tags = new HashSet<string>();

    public string AuthorId { get; set; }

    [XmlIgnore]
    public ICollection<Comment> Comments { get { return this.comments; } }
    
    [XmlArray("Comments")]
    [XmlArrayItem("Comment")]
    public Comment[] CommentsCollection
    {
      get
      {
        return this.comments.ToArray();
      }
      set
      {
        this.comments.Clear();
        if (value != null)
        {
          this.comments.AddAll(value);
        }
      }
    }

    public DateTime DateCreated { get; set; }
    public string Language { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }

    [XmlIgnore]
    public ICollection<string> Tags { get { return this.tags; } }

    [XmlArray("Tags")]
    [XmlArrayItem("Tag")]
    public string[] TagsCollection
    {
      get
      {
        return this.tags.ToArray();
      }
      set
      {
        this.tags.Clear();
        if (value != null)
        {
          this.tags.AddAll(value);
        }
      }
    }

    public string Text { get; set; }

    public Item()
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    public Item(string id, string language, string name, string text = null, string authorId = null) : this()
    {
      this.Id = id;
      this.Language = language;
      this.Name = name;
      this.Text = text;
      this.AuthorId = authorId;
    }

    public Item(IDictionary<string, object> properties) : this()
    {
      this.SetProperties(properties);
    }

    public override string ToString()
    {
      return this.Name;
    }

    public int CompareTo(Item item)
    {
      return this.DateCreated.CompareTo(item.DateCreated);
    }
  }
}