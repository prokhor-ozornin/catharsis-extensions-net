using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  public class Faq : Item, IComparable<Faq>
  {
    public Faq()
    {
    }

    public Faq(IDictionary<string, object> properties) : base(properties)
    {
    }

    public Faq(string id, string language, string name, string text) : base(id, language, name, text)
    {
    }

    public int CompareTo(Faq faq)
    {
      return base.CompareTo(faq);
    }
  }
}