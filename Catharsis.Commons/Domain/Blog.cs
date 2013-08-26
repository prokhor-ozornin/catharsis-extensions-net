using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public class Blog : Item
  {
    /// <summary>
    ///   <para>Creates new blog.</para>
    /// </summary>
    public Blog()
    {
    }

    /// <summary>
    ///   <para>Creates new blog with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on blog after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Blog(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="authorId"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="authorId"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="authorId"/> is <see cref="string.Empty"/> string.</exception>
    public Blog(string id, string language, string name, string authorId) : base(id, language, name, null, authorId)
    {
      Assertion.NotEmpty(authorId);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Blog Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var blog = new Blog((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("AuthorId"));
      if (xml.Element("DateCreated") != null)
      {
        blog.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        blog.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return blog;
    }
  }
}