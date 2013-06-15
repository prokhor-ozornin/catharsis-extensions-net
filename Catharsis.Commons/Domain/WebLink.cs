using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Category", "Url")]
  public class WebLink : Item, IUrlAddressable
  {
    public WebLinksCategory Category { get; set; }
    public string Url { get; set; }

    public WebLink()
    {
    }

    public WebLink(IDictionary<string, object> properties) : base(properties)
    {
    }

    public WebLink(string id, string language, string name, string text, string url, WebLinksCategory category) : base(id, language, name, text)
    {
      this.Url = url;
      this.Category = category;
    }

    public override string ToString()
    {
      return this.Url;
    }
  }
}