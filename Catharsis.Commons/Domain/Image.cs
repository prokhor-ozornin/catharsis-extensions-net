using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Category", "File")]
  public class Image : EntityBase, IComparable<Image>, IDimensionable
  {
    private File file;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public ImagesCategory Category { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public File File
    {
      get { return this.file; }
      set
      {
        Assertion.NotNull(value);

        this.file = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public short Height { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public short Width { get; set; }

    /// <summary>
    ///   <para>Creates new image.</para>
    /// </summary>
    public Image()
    {
    }

    /// <summary>
    ///   <para>Creates new image with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on image after its creation.</param>
    public Image(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="file"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <param name="category"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="id"/> is <see cref="string.Empty"/> string.</exception>
    public Image(string id, File file, short height, short width, ImagesCategory category = null) : base(id)
    {
      this.File = file;
      this.Height = height;
      this.Width = width;
      this.Category = category;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Image Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new Image((string) xml.Element("Id"), File.Xml(xml.Element("File")), (short) xml.Element("Height"), (short) xml.Element("Width"), xml.Element("ImagesCategory") != null ? ImagesCategory.Xml(xml.Element("ImagesCategory")) : null);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current image.</para>
    /// </summary>
    /// <returns>A string that represents the current image.</returns>
    public override string ToString()
    {
      return this.File.ToString();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public int CompareTo(Image image)
    {
      return this.File.CompareTo(image.File);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.Category != null ? this.Category.Xml() : null,
        this.File.Xml(),
        new XElement("Height", this.Height),
        new XElement("Width", this.Width));
    }
  }
}