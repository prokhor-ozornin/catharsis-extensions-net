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
  public class Comment : EntityBase, IComparable<Comment>, IAuthorable, INameable, ITextable, ITimeable
  {
    private string authorId;
    private string name;
    private string text;

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
    ///   <para>Date and time or comment's creation.</para>
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    ///   <para>Date and time of comment's last modification.</para>
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public string Name
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
    ///   <para>Creates new comment.</para>
    /// </summary>
    public Comment()
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para>Creates new comment with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on comment after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Comment(IDictionary<string, object> properties) : this()
    {
      this.SetProperties(properties);
    }

    /// <summary>
    ///   <para>Creates new comment.</para>
    /// </summary>
    /// <param name="id">Unique identifier of comment.</param>
    /// <param name="authorId">Identifier of comment's author.</param>
    /// <param name="name">Name of comment.</param>
    /// <param name="text">Comment's body text.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="authorId"/>, <paramref name="name"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="authorId"/>, <paramref name="name"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Comment(string id, string authorId, string name, string text) : this()
    {
      this.Id = id;
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

      var comment = new Comment((string) xml.Element("Id"), (string) xml.Element("AuthorId"), (string) xml.Element("Name"), (string) xml.Element("Text"));
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
    public int CompareTo(Comment other)
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
        new XElement("DateCreated", this.DateCreated.ToRFC1123()),
        new XElement("LastUpdated", this.LastUpdated.ToRFC1123()),
        new XElement("Name", this.Name),
        new XElement("Text", this.Text));
    }
  }
}