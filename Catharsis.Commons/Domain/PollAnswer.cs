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
  public class PollAnswer : EntityBase, IComparable<PollAnswer>, IEquatable<PollAnswer>, IAuthorable, ITimeable
  {
    private DateTime dateCreated = DateTime.UtcNow;
    private DateTime lastUpdated = DateTime.UtcNow;
    private ICollection<PollOption> options = new HashSet<PollOption>();
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual long? AuthorId { get; set; }
    
    /// <summary>
    ///   <para>Date and time of answer's creation.</para>
    /// </summary>
    public virtual DateTime DateCreated
    {
      get { return this.dateCreated; }
      set { this.dateCreated = value; }
    }
    
    /// <summary>
    ///   <para>Date and time of answers last modification.</para>
    /// </summary>
    public virtual DateTime LastUpdated
    {
      get { return this.lastUpdated; }
      set { this.lastUpdated = value; }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual ICollection<PollOption> Options
    {
      get { return this.options; }
    }
    
    /// <summary>
    ///   <para>Creates new poll answer.</para>
    /// </summary>
    public PollAnswer()
    {
    }

    /// <summary>
    ///   <para>Creates new poll answer.</para>
    /// </summary>
    /// <param name="authorId">Identifier of answer's author.</param>
    public PollAnswer(long authorId)
    {
      this.AuthorId = authorId;
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

      var answer = new PollAnswer((long) xml.Element("AuthorId"));
      if (xml.Element("Id") != null)
      {
        answer.Id = (long) xml.Element("Id");
      }
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
    public virtual int CompareTo(PollAnswer other)
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
        new XElement("DateCreated", this.DateCreated.ToRfc1123()),
        new XElement("LastUpdated", this.LastUpdated.ToRfc1123()),
        this.Options.Count > 0 ? new XElement("Options", this.Options.Select(option => option.Xml())) : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(PollAnswer other)
    {
      return base.Equals(other);
    }
  }
}