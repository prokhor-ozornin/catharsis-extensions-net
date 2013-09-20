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
  [EqualsAndHashCode("Name,OriginalName")]
  public class File : EntityBase, IComparable<File>, IEquatable<File>, INameable, ISizable, ITaggable, ITimeable
  {
    private string contentType;
    private byte[] data;
    private DateTime dateCreated = DateTime.UtcNow;
    private DateTime lastUpdated = DateTime.UtcNow;
    private string name;
    private string originalName;
    private readonly ICollection<string> tags = new HashSet<string>();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public string ContentType
    {
      get { return this.contentType; }
      set
      {
        Assertion.NotEmpty(value);

        this.contentType = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public byte[] Data
    {
      get { return this.data; }
      set
      {
        Assertion.NotNull(value);

        this.data = value;
      }
    }
    
    /// <summary>
    ///   <para>Date and time of file's creation.</para>
    /// </summary>
    public DateTime DateCreated
    {
      get { return this.dateCreated; }
      set { this.dateCreated = value; }
    }
   
    /// <summary>
    ///   <para>Date and time of file's last modification.</para>
    /// </summary>
    public DateTime LastUpdated
    {
      get { return this.lastUpdated; }
      set { this.lastUpdated = value; }
    }
    
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
    public string OriginalName
    {
      get { return this.originalName; }
      set
      {
        Assertion.NotEmpty(value);

        this.originalName = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public ICollection<string> Tags
    {
      get { return this.tags; }
    }
    
    /// <summary>
    ///   <para>Creates new file.</para>
    /// </summary>
    public File()
    {
    }

    /// <summary>
    ///   <para>Creates new file with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on file after its creation.</param>
    public File(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new file.</para>
    /// </summary>
    /// <param name="id">Unique identifier of file.</param>
    /// <param name="contentType"></param>
    /// <param name="name">Name of file.</param>
    /// <param name="originalName"></param>
    /// <param name="data"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="contentType"/>, <paramref name="name"/>, <paramref name="originalName"/> or <paramref name="data"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="contentType"/>, <paramref name="name"/> or <paramref name="originalName"/> is <see cref="string.Empty"/> string.</exception>
    public File(string id, string contentType, string name, string originalName, byte[] data) : base(id)
    {
      this.ContentType = contentType;
      this.Name = name;
      this.OriginalName = originalName;
      this.Data = data;
      this.Size = data.Length;
    }

    /// <summary>
    ///   <para>Creates new file from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="File"/> type.</param>
    /// <returns>Recreated file object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static File Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var file = new File((string) xml.Element("Id"), (string) xml.Element("ContentType"), (string) xml.Element("Name"), (string) xml.Element("OriginalName"), ((string) xml.Element("Data")).DecodeBase64());
      if (xml.Element("DateCreated") != null)
      {
        file.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        file.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      if (xml.Element("Tags") != null)
      {
        file.Tags.AddAll(xml.Element("Tags").Descendants().Select(tag => (string) tag));
      }
      return file;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(File other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current file.</para>
    /// </summary>
    /// <returns>A string that represents the current file.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para>Compares the current file with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="File"/> to compare with this instance.</param>
    public int CompareTo(File other)
    {
      return this.DateCreated.CompareTo(other.DateCreated);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="File"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("ContentType", this.ContentType),
        new XElement("Data", this.Data.EncodeBase64()),
        new XElement("DateCreated", this.DateCreated.ToRfc1123()),
        new XElement("LastUpdated", this.LastUpdated.ToRfc1123()),
        new XElement("Name", this.Name),
        new XElement("OriginalName", this.OriginalName),
        new XElement("Size", this.Size),
        this.Tags.Count > 0 ? new XElement("Tags", this.Tags.Select(tag => new XElement("Tag", tag))) : null);
    }
  }
}