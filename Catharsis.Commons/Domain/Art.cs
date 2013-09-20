using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Art represents a creative image, usually displayed publically.</para>
  /// </summary>
  [EqualsAndHashCode("Album,Person")]
  public class Art : Item, IComparable<Art>, IEquatable<Art>, IImageable
  {
    private Image image;

    /// <summary>
    ///   <para>Album that contains the art.</para>
    /// </summary>
    public ArtsAlbum Album { get; set; }
    
    /// <summary>
    ///   <para>Associated graphical image, representing a digital painting of the art.</para>
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
    ///   <para>Name of art's material that used in the process of painting, if it has a physical form.</para>
    /// </summary>
    public string Material { get; set; }
    
    /// <summary>
    ///   <para>A person that is the author or creator of the art.</para>
    /// </summary>
    public Person Person { get; set; }
    
    /// <summary>
    ///   <para>Place where the art in the physical form is displayed in public (for example, name of the arts gallery).</para>
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
    ///   <para>Creates new art.</para>
    /// </summary>
    /// <param name="id">Unique identifier of art.</param>
    /// <param name="language">ISO language code of art's text content.</param>
    /// <param name="name">Name of art.</param>
    /// <param name="image">Associated graphical image.</param>
    /// <param name="album">Art's album, or a <c>null</c> reference.</param>
    /// <param name="text">Art's description text.</param>
    /// <param name="person">Author of the art, or a <c>null</c> reference.</param>
    /// <param name="place">Place of art's public disposition, or a <c>null</c> reference.</param>
    /// <param name="material">Physical material, used in art's painting, or a <c>null</c> reference.</param>
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
    ///   <para>Creates new art from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Art"/> type.</param>
    /// <returns>Recreated art object.</returns>
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
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Art"/>.</returns>
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
    ///   <para>Compares the current art with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Art"/> to compare with this instance.</param>
    public int CompareTo(Art other)
    {
      return this.Name.Compare(other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Art other)
    {
      return base.Equals(other);
    }
  }
}