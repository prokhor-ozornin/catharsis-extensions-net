using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [Serializable]
  [EqualsAndHashCode("Text")]
  public class PollOption : EntityBase, ITextable
  {
    private string text;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Text
    {
      get { return this.text; }
      set
      {
        Assertion.NotEmpty(value);

        this.text = value;
      }
    }

    /// <summary>
    ///   <para>Creates new poll option.</para>
    /// </summary>
    public PollOption()
    {
    }

    /// <summary>
    ///   <para>Creates new poll option with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on poll option after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public PollOption(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="text"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public PollOption(string id, string text) : base(id)
    {
      this.Text = text;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static PollOption Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new PollOption((string) xml.Element("Id"), (string) xml.Element("Text"));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("Text", this.Text));
    }
  }
}