using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public class ArtsAlbum : Item, IComparable<ArtsAlbum>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public DateTime? PublishedOn { get; set; }

    /// <summary>
    ///   <para>Creates new arts album.</para>
    /// </summary>
    public ArtsAlbum()
    {
    }

    /// <summary>
    ///   <para>Creates new arts album with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on arts album after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public ArtsAlbum(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="publishedOn"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public ArtsAlbum(string id, string language, string name, string text = null, DateTime? publishedOn = null) : base(id, language, name, text)
    {
      this.PublishedOn = publishedOn;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static ArtsAlbum Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var album = new ArtsAlbum((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), (DateTime?) xml.Element("PublishedOn"));
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
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.PublishedOn.HasValue ? new XElement("PublishedOn", this.PublishedOn.GetValueOrDefault().ToRFC1123()) : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="album"></param>
    /// <returns></returns>
    public int CompareTo(ArtsAlbum album)
    {
      return this.Name.CompareTo(album.Name);
    }
  }
}