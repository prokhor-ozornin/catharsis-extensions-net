using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public abstract class EntityBase : IEntity
  {
    /// <summary>
    ///   <para>Implementation of <see cref="IEntity.Id"/> property.</para>
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    protected EntityBase()
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="properties"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    protected EntityBase(IDictionary<string, object> properties)
    {
      Assertion.NotNull(properties);

      this.SetProperties(properties);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="properties"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    protected EntityBase(object properties)
    {
      Assertion.NotNull(properties);

      this.SetProperties(properties);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public override bool Equals(object other)
    {
      return this.Equality(other, (string[]) null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
      return this.GetHashCode((string[])null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public virtual XElement Xml()
    {
      return new XElement(this.GetType().Name,
        this.Id > 0 ? new XElement("Id", this.Id) : null);
    }
  }
}