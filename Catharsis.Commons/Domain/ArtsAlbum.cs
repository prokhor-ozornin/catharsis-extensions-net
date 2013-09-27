using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public class ArtsAlbum : Item, IComparable<ArtsAlbum>, IEquatable<ArtsAlbum>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual DateTime? PublishedOn { get; set; }

    /// <summary>
    ///   <para>Creates new arts album.</para>
    /// </summary>
    public ArtsAlbum()
    {
    }

    /// <summary>
    ///   <para>Creates new arts album.</para>
    /// </summary>
    /// <param name="language">ISO language code of album's text content.</param>
    /// <param name="name">Name of album.</param>
    /// <param name="text">Album's description text.</param>
    /// <param name="publishedOn"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public ArtsAlbum(string language, string name, string text = null, DateTime? publishedOn = null) : base(language, name, text)
    {
      this.PublishedOn = publishedOn;
    }

    /// <summary>
    ///   <para>Creates new arts album from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="ArtsAlbum"/> type.</param>
    /// <returns>Recreated arts album object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static ArtsAlbum Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var album = new ArtsAlbum((string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (DateTime?) xml.Element("PublishedOn"));
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
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="ArtsAlbum"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.PublishedOn.HasValue ? new XElement("PublishedOn", this.PublishedOn.GetValueOrDefault().ToRfc1123()) : null);
    }

    /// <summary>
    ///   <para>Compares the current arts album with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="ArtsAlbum"/> to compare with this instance.</param>
    public virtual int CompareTo(ArtsAlbum other)
    {
      return this.Name.Compare(other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(ArtsAlbum other)
    {
      return base.Equals(other);
    }
  }
}