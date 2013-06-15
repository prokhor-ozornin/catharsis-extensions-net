using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Category", "File")]
  public class Audio : EntityBase, IComparable<Audio>
  {
    public short Bitrate { get; set; }
    public AudiosCategory Category { get; set; }
    public long Duration { get; set; }
    public File File { get; set; }

    public Audio()
    {
    }

    public Audio(IDictionary<string, object> properties)
    {
      this.SetProperties(properties);
    }

    public Audio(string id, File file, short bitrate, short duration, AudiosCategory category = null)
    {
      this.Id = id;
      this.File = file;
      this.Bitrate = bitrate;
      this.Duration = duration;
      this.Category = category;
    }

    public override string ToString()
    {
      return this.File.ToString();
    }

    public int CompareTo(Audio audio)
    {
      return this.File.CompareTo(audio.File);
    }
  }
}