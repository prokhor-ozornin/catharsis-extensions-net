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
  [EqualsAndHashCode("Category", "File")]
  public class Video : EntityBase, IComparable<Video>, IDimensionable
  {
    private File file;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public short Bitrate { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public VideosCategory Category { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public long Duration { get; set; }
    
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
    ///   <para>Creates new video.</para>
    /// </summary>
    public Video()
    {
    }

    /// <summary>
    ///   <para>Creates new video with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on video after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Video(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="file"></param>
    /// <param name="bitrate"></param>
    /// <param name="duration"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <param name="category"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="id"/> is <see cref="string.Empty"/> string.</exception>
    public Video(string id, File file, short bitrate, long duration, short height, short width, VideosCategory category = null) : base(id)
    {
      this.File = file;
      this.Bitrate = bitrate;
      this.Duration = duration;
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
    public static Video Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      return new Video((string) xml.Element("Id"), File.Xml(xml.Element("File")), (short) xml.Element("Bitrate"), (long) xml.Element("Duration"), (short) xml.Element("Height"), (short) xml.Element("Width"), xml.Element("VideosCategory") != null ? VideosCategory.Xml(xml.Element("VideosCategory")) : null);
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
    ///   <para></para>
    /// </summary>
    /// <param name="video"></param>
    /// <returns></returns>
    public int CompareTo(Video video)
    {
      return this.File.CompareTo(video.File);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
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