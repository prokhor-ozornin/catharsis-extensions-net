using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  public class PlaycastsCategory : Category
  {
    public PlaycastsCategory()
    {
    }

    public PlaycastsCategory(IDictionary<string, object> properties) : base(properties)
    {
    }

    public PlaycastsCategory(string id, string language, string name, PlaycastsCategory parent = null, string description = null) : base(id, language, name, parent, description)
    {
    }
  }
}