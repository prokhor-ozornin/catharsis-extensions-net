using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Name", "Parent")]
  public abstract class Category : EntityBase, IComparable<Category>
  {
    public string Description { get; set; }
    public string Language { get; set; }
    public string Name { get; set; }
    public Category Parent { get; set; }

    protected Category()
    {
    }

    protected Category(IDictionary<string, object> properties)
    {
      this.SetProperties(properties);
    }

    protected Category(string id, string language, string name, Category parent = null, string description = null)
    {
      this.Id = id;
      this.Language = language;
      this.Name = name;
      this.Parent = parent;
      this.Description = description;
    }

    public override string ToString()
    {
      return this.Name;
    }

    public int CompareTo(Category category)
    {
      return this.Name.CompareTo(category.Name);
    }
  }
}