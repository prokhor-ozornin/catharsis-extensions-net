using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
  public sealed class EqualsAndHashCodeAttribute : Attribute
  {
    private readonly string[] properties;

    public EqualsAndHashCodeAttribute(params string[] properties)
    {
      this.properties = properties;
    }

    public IEnumerable<string> Properties { get { return this.properties; } }
  }
}