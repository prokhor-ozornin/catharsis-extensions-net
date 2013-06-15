using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Blog")]
  public class BlogEntry : Item, IComparable<BlogEntry>
  {
    public Blog Blog { get; set; }

    public BlogEntry()
    {
    }

    public BlogEntry(IDictionary<string, object> properties) : base(properties)
    {
    }

    public BlogEntry(string id, string language, string name, string text, Blog blog) : base(id, language, name, text, null)
    {
      this.Blog = blog;
    }

    public int CompareTo(BlogEntry entry)
    {
      return base.CompareTo(entry);
    }
  }
}