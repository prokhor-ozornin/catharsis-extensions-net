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
  [EqualsAndHashCode("Data", "Name")]
  public class File : EntityBase, IComparable<File>, INameable, ISizable, ITaggable, ITimeable
  {
    private string contentType;
    private byte[] data;
    private string name;
    private string originalName;
    private readonly ICollection<string> tags = new HashSet<string>();

    /// <summary>
    ///   <para></para>
    /// </summary>
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
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    ///   <para>Creates new file with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on file after its creation.</param>
    public File(IDictionary<string, object> properties) : this()
    {
      this.SetProperties(properties);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="contentType"></param>
    /// <param name="name"></param>
    /// <param name="originalName"></param>
    /// <param name="data"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="contentType"/>, <paramref name="name"/>, <paramref name="originalName"/> or <paramref name="data"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="contentType"/>, <paramref name="name"/> or <paramref name="originalName"/> is <see cref="string.Empty"/> string.</exception>
    public File(string id, string contentType, string name, string originalName, byte[] data) : this()
    {
      this.Id = id;
      this.ContentType = contentType;
      this.Name = name;
      this.OriginalName = originalName;
      this.Data = data;
      this.Size = data.Length;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
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
    ///   <para>Returns a <see cref="string"/> that represents the current file.</para>
    /// </summary>
    /// <returns>A string that represents the current file.</returns>
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public int CompareTo(File file)
    {
      return this.DateCreated.CompareTo(file.DateCreated);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("ContentType", this.ContentType),
        new XElement("Data", this.Data.EncodeBase64()),
        new XElement("DateCreated", this.DateCreated.ToRFC1123()),
        new XElement("LastUpdated", this.LastUpdated.ToRFC1123()),
        new XElement("Name", this.Name),
        new XElement("OriginalName", this.OriginalName),
        new XElement("Size", this.Size),
        this.Tags.Count > 0 ? new XElement("Tags", this.Tags.Select(tag => new XElement("Tag", tag))) : null);
    }
  }
}