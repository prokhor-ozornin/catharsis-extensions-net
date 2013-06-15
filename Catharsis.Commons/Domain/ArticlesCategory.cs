using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  public sealed class ArticlesCategory : Category
  {
    public ArticlesCategory()
    {
    }

    public ArticlesCategory(IDictionary<string, object> properties) : base(properties)
    {
    }

    public ArticlesCategory(string id, string language, string name, ArticlesCategory parent = null, string description = null) : base(id, language,name,parent, description)
    {
    }
  }
}