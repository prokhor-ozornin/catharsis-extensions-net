using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Name,Type")]
  public class Setting : EntityBase, IEquatable<Setting>, INameable, ITypeable
  {
    private string name;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Name
    {
      get { return this.name; }
      set
      {
        Assertion.NotEmpty(value);

        this.name = value;
      }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual int Type { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual string Value { get; set; }

    /// <summary>
    ///   <para>Creates new setting.</para>
    /// </summary>
    public Setting()
    {
    }

    /// <summary>
    ///   <para>Creates new setting with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on setting after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Setting(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="type"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="name"/> or <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="name"/> or <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public Setting(string name, string value, int type = 0)
    {
      this.Name = name;
      this.Value = value;
      this.Type = type;
    }

    /// <summary>
    ///   <para>Creates new setting from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Setting"/> type.</param>
    /// <returns>Recreated setting object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Setting Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var setting = new Setting((string) xml.Element("Name"), (string) xml.Element("Value"), (int) xml.Element("Type"));
      if (xml.Element("Id") != null)
      {
        setting.Id = (long)xml.Element("Id");
      }
      return setting;
    }

    /// <summary>
    ///   <paramref name=">"/>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Setting other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return this.Value;
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Setting"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("Name", this.Name),
        new XElement("Type", this.Type),
        this.Value != null ? new XElement("Value", this.Value) : null);
    }
  }
}