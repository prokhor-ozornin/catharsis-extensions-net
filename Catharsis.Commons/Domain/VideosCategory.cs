using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  public class VideosCategory : Category
  {
    public VideosCategory()
    {
    }

    public VideosCategory(IDictionary<string, object> properties) : base(properties)
    {
    }

    public VideosCategory(string id, string language, string name, VideosCategory parent = null, string description = null) : base(id, language, name, parent, description)
    {
    }
  }
}