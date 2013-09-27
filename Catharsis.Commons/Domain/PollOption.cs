using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Text")]
  public class PollOption : EntityBase, IEquatable<PollOption>, ITextable
  {
    private string text;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string Text
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
    ///   <para>Creates new poll option.</para>
    /// </summary>
    /// <param name="text">Option's content text.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public PollOption(string text)
    {
      this.Text = text;
    }

    /// <summary>
    ///   <para>Creates new poll option from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="PollOption"/> type.</param>
    /// <returns>Recreated poll option object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static PollOption Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var option = new PollOption((string) xml.Element("Text"));
      if (xml.Element("Id") != null)
      {
        option.Id = (long) xml.Element("Id");
      }
      return option;
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="PollOption"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("Text", this.Text));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(PollOption other)
    {
      return base.Equals(other);
    }
  }
}