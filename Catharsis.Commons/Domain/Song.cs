using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("Album")]
  public class Song : Item, IComparable<Song>, IEquatable<Song>
  {
    private Audio audio;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public SongsAlbum Album { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    public Audio Audio
    {
      get { return this.audio; }
      set
      {
        Assertion.NotNull(value);

        this.audio = value;
      }
    }

    /// <summary>
    ///   <para>Creates new song.</para>
    /// </summary>
    public Song()
    {
    }

    /// <summary>
    ///   <para>Creates new song with specified properties values.</para>
    /// </summary>
    /// <param name="properties">Named collection of properties to set on song after its creation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public Song(IDictionary<string, object> properties) : base(properties)
    {
    }

    /// <summary>
    ///   <para>Creates new song.</para>
    /// </summary>
    /// <param name="id">Unique identifier of song.</param>
    /// <param name="language">ISO language code of song's text content.</param>
    /// <param name="name">Name of song.</param>
    /// <param name="text">Song's lyrics text.</param>
    /// <param name="audio"></param>
    /// <param name="album"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="id"/>, <paramref name="language"/>, <paramref name="name"/>, <paramref name="text"/> or <paramref name="audio"/> is a <c>null</c> reference.</exception>
    public Song(string id, string language, string name, string text, Audio audio, SongsAlbum album = null) : base(id, language, name, text)
    {
      Assertion.NotEmpty(text);

      this.Audio = audio;
      this.Album = album;
    }

    /// <summary>
    ///   <para>Creates new song from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Song"/> type.</param>
    /// <returns>Recreated song object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public new static Song Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var song = new Song((string) xml.Element("Id"), (string) xml.Element("Language"), (string) xml.Element("Name"), (string) xml.Element("Text"), Audio.Xml(xml.Element("Audio")), xml.Element("SongsAlbum") != null ? SongsAlbum.Xml(xml.Element("SongsAlbum")) : null);
      if (xml.Element("DateCreated") != null)
      {
        song.DateCreated = (DateTime) xml.Element("DateCreated");
      }
      if (xml.Element("LastUpdated") != null)
      {
        song.LastUpdated = (DateTime) xml.Element("LastUpdated");
      }
      return song;
    }

    /// <summary>
    ///   <para>Compares the current song with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Song"/> to compare with this instance.</param>
    public int CompareTo(Song other)
    {
      return this.Name.Compare(other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Song"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
       this.Album != null ? this.Album.Xml() : null,
       this.Audio.Xml());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Song other)
    {
      return base.Equals(other);
    }
  }
}