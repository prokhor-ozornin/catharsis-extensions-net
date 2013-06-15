using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  public sealed class ImagesCategory : Category
  {
    public ImagesCategory()
    {
    }

    public ImagesCategory(IDictionary<string, object> properties) : base(properties)
    {
    }

    public ImagesCategory(string id, string language, string name, ImagesCategory parent = null, string description = null) : base(id, language,name,parent, description)
    {
    }
  }
}