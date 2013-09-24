using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public class SongsAlbum : Item, IComparable<SongsAlbum>, IEquatable<SongsAlbum>, IImageable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual Image Image { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual DateTime? PublishedOn { get; set; }

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
    ///   <para>Creates new songs album.</para>
    /// </summary>
    /// <param name="language">ISO language code of album's text content.</param>
    /// <param name="name">Name of album.</param>
    /// <param name="text">Album's description text.</param>
    /// <param name="image"></param>
    /// <param name="publishedOn"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public SongsAlbum(string language, string name, string text = null, Image image = null, DateTime? publishedOn = null) : base(language, name, text)
    {
      this.Image = image;
      this.PublishedOn = publishedOn;
    }

    /// <summary>
    ///   <para>Creates new songs album from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="SongsAlbum"/> type.</param>
    /// <returns>Recreated songs album object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static SongsAlbum Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var album = new SongsAlbum((string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null, (DateTime?) xml.Element("PublishedOn"));
      if (xml.Element("Id") != null)
      {
        album.Id = (long) xml.Element("Id");
      }
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
    ///   <para>Compares the current songs album with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="SongsAlbum"/> to compare with this instance.</param>
    public virtual int CompareTo(SongsAlbum other)
    {
      return this.Name.Compare(other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="SongsAlbum"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Image != null ? this.Image.Xml() : null,
        this.PublishedOn.HasValue ? new XElement("PublishedOn", this.PublishedOn.GetValueOrDefault().ToRfc1123()) : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(SongsAlbum other)
    {
      return base.Equals(other);
    }
  }
}