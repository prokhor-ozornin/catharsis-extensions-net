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
  [Serializable]
  [EqualsAndHashCode("AuthorId")]
  public class PollAnswer : EntityBase, IComparable<PollAnswer>, IAuthorable, ITimeable
  {
    private string authorId;
    private readonly ICollection<PollOption> options = new HashSet<PollOption>();
    
    /// <summary>
    ///   <para></para>
    /// </summary>
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
    ///   <para></para>
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    ///   <para></para>
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
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="authorId"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="authorId"/> or <paramref name="poll"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/> or <paramref name="authorId"/> is <see cref="string.Empty"/> string.</exception>
    public PollAnswer(string id, string authorId) : base(id)
    {
      this.AuthorId = authorId;
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
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
    ///   <para></para>
    /// </summary>
    /// <param name="answer"></param>
    /// <returns></returns>
    public int CompareTo(PollAnswer answer)
    {
      return this.DateCreated.CompareTo(answer.DateCreated);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
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