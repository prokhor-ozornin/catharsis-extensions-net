using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("AuthorId", "Item", "Name")]
  public class Comment : EntityBase, IComparable<Comment>, IAuthorable, INameable, ITextable, ITimeable
  {
    public string AuthorId { get; set; }
    public DateTime DateCreated { get; set; }
    public Item Item { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment()
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    public Comment(IDictionary<string, object> properties) : this()
    {
      this.SetProperties(properties);
    }

    public Comment(string id, string authorId, Item item, string name, string text) : this()
    {
      this.Id = id;
      this.AuthorId = authorId;
      this.Item = item;
      this.Name = name;
      this.Text = text;
    }

    public override string ToString()
    {
      return this.Name;
    }

    public int CompareTo(Comment comment)
    {
      return this.DateCreated.CompareTo(comment.DateCreated);
    }
  }
}