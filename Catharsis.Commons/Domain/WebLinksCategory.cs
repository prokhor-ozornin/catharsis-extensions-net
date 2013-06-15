using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  public class WebLinksCategory : Category
  {
    public WebLinksCategory()
    {
    }

    public WebLinksCategory(IDictionary<string, object> properties) : base(properties)
    {
    }

    public WebLinksCategory(string id, string language, string name, WebLinksCategory parent = null, string description = null) : base(id, language, name, parent, description)
    {
    }
  }
}