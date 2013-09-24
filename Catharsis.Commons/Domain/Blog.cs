using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public class Blog : Item, IEquatable<Blog>
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
    ///   <para>Creates new blog.</para>
    /// </summary>
    /// <param name="language">ISO language code of blog's text content.</param>
    /// <param name="name">Title of blog.</param>
    /// <param name="authorId">Identifier of blog's author.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/>, <paramref name="name"/> or <paramref name="authorId"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/>, <paramref name="name"/> or <paramref name="authorId"/> is <see cref="string.Empty"/> string.</exception>
    public Blog(string language, string name, string authorId) : base(language, name, null, authorId)
    {
      Assertion.NotEmpty(authorId);
    }

    /// <summary>
    ///   <para>Creates new blog from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Blog"/> type.</param>
    /// <returns>Recreated blog object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Blog Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var blog = new Blog((string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("AuthorId"));
      if (xml.Element("Id") != null)
      {
        blog.Id = (long) xml.Element("Id");
      }
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

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Blog other)
    {
      return base.Equals(other);
    }
  }
}