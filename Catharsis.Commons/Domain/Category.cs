using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Name,Parent")]
  public abstract class Category : EntityBase, IComparable<Category>
  {
    private string language;
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual string Description { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Language
    {
      get { return this.language; }
      set
      {
        Assertion.NotEmpty(value);

        this.language = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Name
    {
      get { return this.name;}
      set
      {
        Assertion.NotEmpty(value);

        this.name = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual Category Parent { get; set; }

    /// <summary>
    ///   <para>Creates new category.</para>
    /// </summary>
    protected Category()
    {
    }

    /// <summary>
    ///   <para>Creates new category with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on category after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    protected Category(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new category.</para>
    /// </summary>
    /// <param name="language">ISO language code of category's text content.</param>
    /// <param name="name">Name of category.</param>
    /// <param name="parent">Parent of category, or <c>null</c> if there is no parent.</param>
    /// <param name="description">Description of category.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    protected Category(string language, string name, Category parent = null, string description = null)
    {
      this.Language = language;
      this.Name = name;
      this.Parent = parent;
      this.Description = description;
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current category.</para>
    /// </summary>
    /// <returns>A string that represents the current category.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para>Compares the current category with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Category"/> to compare with this instance.</param>
    public virtual int CompareTo(Category other)
    {
      return this.Name.Compare(other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Category"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Description != null ? new XElement("Description", this.Description) : null,
        new XElement("Language", this.Language),
        new XElement("Name", this.Name),
        this.Parent != null ? new XElement("Parent").AddContent(this.Parent.Xml().Elements()) : null);
    }
  }
}