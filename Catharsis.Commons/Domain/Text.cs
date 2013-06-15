using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Category", "Person")]
  public class Text : Item
  {
    public TextsCategory Category { get; set; }
    public Person Person { get; set; }

    public Text()
    {
    }

    public Text(IDictionary<string, object> properties) : base(properties)
    {
    }

    public Text(string id, string authorId, string language, string name, string text, TextsCategory category, Person person) : base(id, language, name, text, authorId)
    {
      this.Category = category;
      this.Person = person;
    }
  }
}