using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("AuthorId,Name,Text")]
  public class Comment : EntityBase, IComparable<Comment>, IEquatable<Comment>, IAuthorable, INameable, ITextable, ITimeable
  {
    private DateTime dateCreated = DateTime.UtcNow;
    private DateTime lastUpdated = DateTime.UtcNow;
    private string name;
    private string text;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual long? AuthorId { get; set; }
    
    /// <summary>
    ///   <para>Date and time or comment's creation.</para>
    /// </summary>
    public virtual DateTime DateCreated
    {
      get { return this.dateCreated; }
      set { this.dateCreated = value; }
    }
    
    /// <summary>
    ///   <para>Date and time of comment's last modification.</para>
    /// </summary>
    public virtual DateTime LastUpdated
    {
      get { return this.lastUpdated; }
      set { this.lastUpdated = value; }
    }
    
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
    ///   <para>Creates new comment.</para>
    /// </summary>
    public Comment()
    {
    }

    /// <summary>
    ///   <para>Creates new comment.</para>
    /// </summary>
    /// <param name="authorId">Identifier of comment's author.</param>
    /// <param name="name">Name of comment.</param>
    /// <param name="text">Comment's body text.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Comment(long authorId, string name, string text)
    {
      this.AuthorId = authorId;
      this.Name = name;
      this.Text = text;
    }

    /// <summary>
    ///   <para>Creates new comment from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Comment"/> type.</param>
    /// <returns>Recreated comment object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Comment Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var comment = new Comment((long) xml.Element("AuthorId"), (string) xml.Element("Name"), (string) xml.Element("Text"));
      if (xml.Element("Id") != null)
      {
        comment.Id = (long) xml.Element("Id");
      }
      if (xml.Element("DateCreated") != null)
      {
        comment.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        comment.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return comment;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Comment other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current comment.</para>
    /// </summary>
    /// <returns>A string that represents the current comment.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para>Compares the current comment with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Comment"/> to compare with this instance.</param>
    public virtual int CompareTo(Comment other)
    {
      return this.DateCreated.CompareTo(other.DateCreated);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Comment"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("AuthorId", this.AuthorId),
        new XElement("DateCreated", this.DateCreated.ToRfc1123()),
        new XElement("LastUpdated", this.LastUpdated.ToRfc1123()),
        new XElement("Name", this.Name),
        new XElement("Text", this.Text));
    }
  }
}