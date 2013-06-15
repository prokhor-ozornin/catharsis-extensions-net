using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  public class TextsCategory : Category
  {
    public TextsCategory()
    {
    }

    public TextsCategory(IDictionary<string, object> properties) : base(properties)
    {
    }

    public TextsCategory(string id, string language, string name, TextsCategory parent = null, string description = null) : base(id, language, name, parent, description)
    {
    }
  }
}