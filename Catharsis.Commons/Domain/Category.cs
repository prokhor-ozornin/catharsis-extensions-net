using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Name", "Parent")]
  public abstract class Category : EntityBase, IComparable<Category>
  {
    private string language;
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public string Language
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
    public string Name
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
    public Category Parent { get; set; }

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
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <param name="description"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    protected Category(string id, string language, string name, Category parent = null, string description = null)
    {
      this.Id = id;
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
    ///   <para></para>
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public int CompareTo(Category category)
    {
      return this.Name.CompareTo(category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
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