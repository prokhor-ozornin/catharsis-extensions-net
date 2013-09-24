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
  public class Audio : EntityBase, IComparable<Audio>, IEquatable<Audio>
  {
    private File file;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual short Bitrate { get; set; }
    
    /// <summary>
    ///   <para>Category of audio.</para>
    /// </summary>
    public virtual AudiosCategory Category { get; set; }
    
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
    ///   <para>Creates new audio.</para>
    /// </summary>
    public Audio()
    {
    }

    /// <summary>
    ///   <para>Creates new audio with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on audio after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Audio(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new audio.</para>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="bitrate"></param>
    /// <param name="duration"></param>
    /// <param name="category">Category of audio's belongings, or a <c>null</c> reference.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="file"/> is a <c>null</c> reference.</exception>
    public Audio(File file, short bitrate, short duration, AudiosCategory category = null)
    {
      this.File = file;
      this.Bitrate = bitrate;
      this.Duration = duration;
      this.Category = category;
    }

    /// <summary>
    ///   <para>Creates new audio from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Audio"/> type.</param>
    /// <returns>Recreated audio object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Audio Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var audio = new Audio(File.Xml(xml.Element("File")), (short) xml.Element("Bitrate"), (short) xml.Element("Duration"), xml.Element("AudiosCategory") != null ? AudiosCategory.Xml(xml.Element("AudiosCategory")) : null);
      if (xml.Element("Id") != null)
      {
        audio.Id = (long) xml.Element("Id");
      }
      return audio;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Audio other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current audio.</para>
    /// </summary>
    /// <returns>A string that represents the current audio.</returns>
    public override string ToString()
    {
      return this.File.ToString();
    }

    /// <summary>
    ///   <para>Compares the current audio with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Audio"/> to compare with this instance.</param>
    public virtual int CompareTo(Audio other)
    {
      return this.File.CompareTo(other.File);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Audio"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        new XElement("Bitrate", this.Bitrate),
        this.Category != null ? this.Category.Xml() : null,
        new XElement("Duration", this.Duration),
        this.File.Xml());
    }
  }
}