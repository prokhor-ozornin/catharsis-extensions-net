using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [Serializable]
  public class SongsAlbum : Item, IComparable<SongsAlbum>, IImageable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public Image Image { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public DateTime? PublishedOn { get; set; }

    /// <summary>
    ///   <para>Creates new songs album.</para>
    /// </summary>
    public SongsAlbum()
    {
    }

    /// <summary>
    ///   <para>Creates new songs album with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on songs album after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public SongsAlbum(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="image"></param>
    /// <param name="publishedOn"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public SongsAlbum(string id, string language, string name, string text = null, Image image = null, DateTime? publishedOn = null) : base(id, language, name, text)
    {
      this.Image = image;
      this.PublishedOn = publishedOn;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static SongsAlbum Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var album = new SongsAlbum((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null, (DateTime?) xml.Element("PublishedOn"));
      if (xml.Element("DateCreated") != null)
      {
        album.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        album.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return album;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="album"></param>
    /// <returns></returns>
    public int CompareTo(SongsAlbum album)
    {
      return this.Name.CompareTo(album.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Image != null ? this.Image.Xml() : null,
        this.PublishedOn.HasValue ? new XElement("PublishedOn", this.PublishedOn.GetValueOrDefault().ToRFC1123()) : null);
    }
  }
}