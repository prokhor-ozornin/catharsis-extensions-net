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
  [EqualsAndHashCode("AuthorId")]
  public class PollAnswer : EntityBase, IComparable<PollAnswer>, IAuthorable, ITimeable
  {
    private string authorId;
    private readonly ICollection<PollOption> options = new HashSet<PollOption>();
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public string AuthorId
    {
      get { return this.authorId; }
      set
      {
        Assertion.NotEmpty(value);

        this.authorId = value;
      }
    }
    
    /// <summary>
    ///   <para>Date and time of answer's creation.</para>
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    ///   <para>Date and time of answers last modification.</para>
    /// </summary>
    public DateTime LastUpdated { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public ICollection<PollOption> Options
    {
      get { return this.options; }
    }
    
    /// <summary>
    ///   <para>Creates new poll answer.</para>
    /// </summary>
    public PollAnswer()
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para>Creates new poll answer with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on poll answer after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public PollAnswer(IDictionary<string, object> properties) : base(properties)
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para>Creates new poll answer.</para>
    /// </summary>
    /// <param name="id">Unique identifier of poll answer.</param>
    /// <param name="authorId">Identifier of answer's author.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/> or <paramref name="authorId"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/> or <paramref name="authorId"/> is <see cref="string.Empty"/> string.</exception>
    public PollAnswer(string id, string authorId) : base(id)
    {
      this.AuthorId = authorId;
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para>Creates new poll answer from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="PollAnswer"/> type.</param>
    /// <returns>Recreated poll answer object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static PollAnswer Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var answer = new PollAnswer((string) xml.Element("Id"), (string) xml.Element("AuthorId"));
      if (xml.Element("DateCreated") != null)
      {
        answer.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        answer.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return answer;
    }

    /// <summary>
    ///   <para>Compares the current poll answer with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="PollAnswer"/> to compare with this instance.</param>
    public int CompareTo(PollAnswer other)
    {
      return this.DateCreated.CompareTo(other.DateCreated);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="PollAnswer"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("AuthorId", this.AuthorId),
        new XElement("DateCreated", this.DateCreated.ToRFC1123()),
        new XElement("LastUpdated", this.LastUpdated.ToRFC1123()),
        this.Options.Count > 0 ? new XElement("Options", this.Options.Select(option => option.Xml())) : null);
    }
  }
}