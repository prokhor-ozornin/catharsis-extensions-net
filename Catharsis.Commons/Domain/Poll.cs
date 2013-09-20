using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public class Poll : Item, IEquatable<Poll>
  {
    private readonly ICollection<PollAnswer> answers = new HashSet<PollAnswer>();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public ICollection<PollAnswer> Answers
    {
      get { return this.answers; }  
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public bool MultiSelect { get; set; }

    /// <summary>
    ///   <para>Creates new poll.</para>
    /// </summary>
    public Poll()
    {
    }

    /// <summary>
    ///   <para>Creates new poll with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on poll after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Poll(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new poll.</para>
    /// </summary>
    /// <param name="id">Unique identifier of poll.</param>
    /// <param name="language">ISO language code of poll's text content.</param>
    /// <param name="name">Title of poll.</param>
    /// <param name="text">Poll's question text.</param>
    /// <param name="multiSelect"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Poll(string id, string language, string name, string text, bool multiSelect) : base(id, language, name, text)
    {
      Assertion.NotEmpty(text);

      this.MultiSelect = multiSelect;
    }

    /// <summary>
    ///   <para>Creates new poll from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Poll"/> type.</param>
    /// <returns>Recreated poll object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Poll Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var poll = new Poll((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (bool) xml.Element("MultiSelect"));
      if (xml.Element("DateCreated") != null)
      {
        poll.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        poll.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      if (xml.Element("Answers") != null)
      {
        poll.Answers.AddAll(xml.Element("Answers").Descendants().Select(PollAnswer.Xml));
      }
      return poll;
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Poll"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Answers.Count > 0 ? new XElement("Answers", this.Answers.Select(answer => answer.Xml())) : null,
        new XElement("MultiSelect", this.MultiSelect));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Poll other)
    {
      return base.Equals(other);
    }
  }
}