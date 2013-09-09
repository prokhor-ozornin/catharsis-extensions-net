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
    private string id;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public string Id
    {
      get { return this.id; }
      set
      {
        Assertion.NotEmpty(value);
        
        this.id = value;
      }
    }

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
    /// <param name="id">Unique identifier of entity.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="id"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="id"/> is <see cref="string.Empty"/> string.</exception>
    public EntityBase(string id)
    {
      this.Id = id;
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
        new XElement("Id", this.Id));
    }
  }
}