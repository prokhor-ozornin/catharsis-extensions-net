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
  public class Audio : EntityBase, IComparable<Audio>
  {
    private File file;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public short Bitrate { get; set; }
    
    /// <summary>
    ///   <para>Category of audio.</para>
    /// </summary>
    public AudiosCategory Category { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public long Duration { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
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
    /// <param name="id">Unique identifier of audio.</param>
    /// <param name="file"></param>
    /// <param name="bitrate"></param>
    /// <param name="duration"></param>
    /// <param name="category">Category of audio's belongings, or a <c>null</c> reference.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/> or <paramref name="file"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="id"/> is <see cref="string.Empty"/> string.</exception>
    public Audio(string id, File file, short bitrate, short duration, AudiosCategory category = null) : base(id)
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

      return new Audio((string) xml.Element("Id"), File.Xml(xml.Element("File")), (short) xml.Element("Bitrate"), (short) xml.Element("Duration"), xml.Element("AudiosCategory") != null ? AudiosCategory.Xml(xml.Element("AudiosCategory")) : null);
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
    public int CompareTo(Audio other)
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