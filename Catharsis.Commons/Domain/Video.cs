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
  public class Video : EntityBase, IComparable<Video>, IEquatable<Video>, IDimensionable
  {
    private File file;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual short Bitrate { get; set; }
    
    /// <summary>
    ///   <para>Category of video.</para>
    /// </summary>
    public virtual VideosCategory Category { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual long Duration { get; set; }
    
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
    ///   <para>Creates new video.</para>
    /// </summary>
    public Video()
    {
    }

    /// <summary>
    ///   <para>Creates new video.</para>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="bitrate"></param>
    /// <param name="duration"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <param name="category">Category of video's belongings, or a <c>null</c> reference.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is a <c>null</c> reference.</exception>
    public Video(File file, short bitrate, long duration, short height, short width, VideosCategory category = null)
    {
      this.File = file;
      this.Bitrate = bitrate;
      this.Duration = duration;
      this.Height = height;
      this.Width = width;
      this.Category = category;
    }

    /// <summary>
    ///   <para>Creates new video from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Video"/> type.</param>
    /// <returns>Recreated video object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Video Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var video = new Video(File.Xml(xml.Element("File")), (short) xml.Element("Bitrate"), (long) xml.Element("Duration"), (short) xml.Element("Height"), (short) xml.Element("Width"), xml.Element("VideosCategory") != null ? VideosCategory.Xml(xml.Element("VideosCategory")) : null);
      if (xml.Element("Id") != null)
      {
        video.Id = (long) xml.Element("Id");
      }
      return video;
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    /// <param name="other">An object to compare with this object.</param>
    public virtual bool Equals(Video other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current video.</para>
    /// </summary>
    /// <returns>A string that represents the current video.</returns>
    public override string ToString()
    {
      return this.File.ToString();
    }

    /// <summary>
    ///   <para>Compares the current video with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Video"/> to compare with this instance.</param>
    public virtual int CompareTo(Video other)
    {
      return this.File.CompareTo(other.File);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Video"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("Bitrate", this.Bitrate),
        this.Category != null ? this.Category.Xml() : null,
        new XElement("Duration", this.Duration),
        this.File.Xml(),
        new XElement("Height", this.Height),
        new XElement("Width", this.Width));
    }
  }
}