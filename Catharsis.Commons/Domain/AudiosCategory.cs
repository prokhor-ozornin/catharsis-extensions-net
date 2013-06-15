using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  public sealed class AudiosCategory : Category
  {
    public AudiosCategory()
    {
    }

    public AudiosCategory(IDictionary<string, object> properties)
    {
      this.SetProperties(properties);
    }

    public AudiosCategory(string id, string language, string name, AudiosCategory parent = null, string description = null) : base(id, language, name, parent, description)
    {
    }
  }
}