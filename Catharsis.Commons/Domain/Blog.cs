using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  public class Blog : Item
  {
    public Blog()
    {
    }

    public Blog(IDictionary<string, object> properties) : base(properties)
    {
    }

    public Blog(string id, string language, string name, string authorId) : base(id, language, name, null, authorId)
    {
    }
  }
}