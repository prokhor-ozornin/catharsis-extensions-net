using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Category,File")]
  public class Image : EntityBase, IComparable<Image>, IEquatable<Image>, IDimensionable
  {
    private File file;

    /// <summary>
    ///   <para>Category of image.</para>
    /// </summary>
    public virtual ImagesCategory Category { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public virtual File File
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
    public virtual short Height { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual short Width { get; set; }

    /// <summary>
    ///   <para>Creates new image.</para>
    /// </summary>
    public Image()
    {
    }

    /// <summary>
    ///   <para>Creates new image.</para>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <param name="category">Category of image's belongings, or a <c>null</c> reference.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is a <c>null</c> reference.</exception>
    public Image(File file, short height, short width, ImagesCategory category = null)
    {
      this.File = file;
      this.Height = height;
      this.Width = width;
      this.Category = category;
    }

    /// <summary>
    ///   <para>Creates new image from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Image"/> type.</param>
    /// <returns>Recreated image object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Image Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var image = new Image(File.Xml(xml.Element("File")), (short) xml.Element("Height"), (short) xml.Element("Width"), xml.Element("ImagesCategory") != null ? ImagesCategory.Xml(xml.Element("ImagesCategory")) : null);
      if (xml.Element("Id") != null)
      {
        image.Id = (long) xml.Element("Id");
      }
      return image;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Image other)
    {
      return base.Equals(other);
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
    public virtual int CompareTo(Image image)
    {
      return this.File.CompareTo(image.File);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Image"/>.</returns>
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