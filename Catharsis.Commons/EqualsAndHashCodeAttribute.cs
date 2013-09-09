using System;
using System.Collections.Generic;
using System.Linq;

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
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="properties"/> is <see cref="string.Empty"/> string.</exception>
    public EqualsAndHashCodeAttribute(string properties)
    {
      Assertion.NotEmpty(properties);

      this.properties = properties.Split(',').Select(property => property.Trim()).ToArray();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public IEnumerable<string> Properties { get { return this.properties; } }
  }
}