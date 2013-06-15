using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Type")]
  public class Quote : Item, IComparable<Quote>, ITypeable
  {
    public int Type { get; set; }

    public Quote()
    {
    }

    public Quote(IDictionary<string, object> properties) : base(properties)
    {
    }

    public Quote(string id, string language, string name, string text, int type = 0) : base(id, language, name, text)
    {
      this.Type = type;
    }

    public int CompareTo(Quote quote)
    {
      return base.CompareTo(quote);
    }
  }
}