using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
  public sealed class EqualsAndHashCodeAttribute : Attribute
  {
    private readonly string[] properties;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="properties"></param>
    public EqualsAndHashCodeAttribute(params string[] properties)
    {
      this.properties = properties;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public IEnumerable<string> Properties { get { return this.properties; } }
  }
}