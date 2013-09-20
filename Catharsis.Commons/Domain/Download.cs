using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Category")]
  public class Download : Item, IComparable<Download>, IEquatable<Download>, IUrlAddressable
  {
    private string url;

    /// <summary>
    ///   <para>Category of download.</para>
    /// </summary>
    public DownloadsCategory Category { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public long Downloads { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public string Url
    {
      get { return this.url; }
      set
      {
        Assertion.NotEmpty(value);

        this.url = value;
      }
    }

    /// <summary>
    ///   <para>Creates new dowload.</para>
    /// </summary>
    public Download()
    {
    }

    /// <summary>
    ///   <para>Creates new download with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on dowload after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Download(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new download.</para>
    /// </summary>
    /// <param name="id">Unique identifier of download.</param>
    /// <param name="language">ISO language code of download's text content.</param>
    /// <param name="name">Name of download.</param>
    /// <param name="category">Category of download's belongings, or a <c>null</c> reference.</param>
    /// <param name="url"></param>
    /// <param name="text">Download's description text.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="category"/> or <paramref name="url"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="url"/> is <see cref="string.Empty"/> string.</exception>
    public Download(string id, string language, string name, string url, DownloadsCategory category = null, string text = null) : base(id, language, name, text)
    {
      this.Category = category;
      this.Url = url;
    }

    /// <summary>
    ///   <para>Creates new download from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Download"/> type.</param>
    /// <returns>Recreated download object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Download Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var download = new Download((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Url"), xml.Element("DownloadsCategory") != null ? DownloadsCategory.Xml(xml.Element("DownloadsCategory")) : null, (string) xml.Element("Text"));
      if (xml.Element("DateCreated") != null)
      {
        download.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        download.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      if (xml.Element("Downloads") != null)
      {
        download.Downloads = (long) xml.Element("Downloads");
      }
      return download;
    }

    /// <summary>
    ///   <para>Compares the current download with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Download"/> to compare with this instance.</param>
    public int CompareTo(Download other)
    {
      return this.Name.Compare(other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Download"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml() : null,
        new XElement("Downloads", this.Downloads),
        new XElement("Url", this.Url));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Download other)
    {
      return base.Equals(other);
    }
  }
}