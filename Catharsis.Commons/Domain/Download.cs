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
  public class Download : Item, IComparable<Download>, IUrlAddressable
  {
    private string url;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public DownloadsCategory Category { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public long Downloads { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
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
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="category"></param>
    /// <param name="url"></param>
    /// <param name="text"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="category"/> or <paramref name="url"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="url"/> is <see cref="string.Empty"/> string.</exception>
    public Download(string id, string language, string name, string url, DownloadsCategory category = null, string text = null) : base(id, language, name, text)
    {
      this.Category = category;
      this.Url = url;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
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
    ///   <para></para>
    /// </summary>
    /// <param name="download"></param>
    /// <returns></returns>
    public int CompareTo(Download download)
    {
      return this.Name.CompareTo(download.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml() : null,
        new XElement("Downloads", this.Downloads),
        new XElement("Url", this.Url));
    }
  }
}