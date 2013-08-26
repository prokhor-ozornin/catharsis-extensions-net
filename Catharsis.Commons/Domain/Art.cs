using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Album", "Person")]
  public class Art : Item, IComparable<Art>, IImageable
  {
    private Image image;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public ArtsAlbum Album { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public Image Image
    {
      get { return this.image; }
      set
      {
        Assertion.NotNull(value);

        this.image = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Material { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public Person Person { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Place { get; set; }

    /// <summary>
    ///   <para>Creates new art.</para>
    /// </summary>
    public Art()
    {
    }

    /// <summary>
    ///   <para>Creates new art with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on art after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Art(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="language"></param>
    /// <param name="name"></param>
    /// <param name="image"></param>
    /// <param name="album"></param>
    /// <param name="text"></param>
    /// <param name="person"></param>
    /// <param name="place"></param>
    /// <param name="material"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/> or <paramref name="image"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="id"/>, <paramref name="language"/> or <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public Art(string id, string language, string name, Image image, ArtsAlbum album = null, string text = null, Person person = null, string place = null, string material = null) : base(id, language, name, text)
    {
      this.Image = image;
      this.Album = album;
      this.Person = person;
      this.Place = place;
      this.Material = material;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Art Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var art = new Art((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), Image.Xml(xml.Element("Image")), xml.Element("ArtsAlbum") != null ? ArtsAlbum.Xml(xml.Element("ArtsAlbum")) : null, (string) xml.Element("Text"), xml.Element("Person") != null ? Person.Xml(xml.Element("Person")) : null, (string) xml.Element("Place"), (string) xml.Element("Material"));
      if (xml.Element("DateCreated") != null)
      {
        art.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        art.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return art;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Album != null ? this.Album.Xml() : null,
        this.Image.Xml(),
        this.Material != null ? new XElement("Material", this.Material) : null,
        this.Person != null ? this.Person.Xml() : null,
        this.Place != null ? new XElement("Place", this.Place) : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="art"></param>
    /// <returns></returns>
    public int CompareTo(Art art)
    {
      return this.Name.CompareTo(art.Name);
    }
  }
}