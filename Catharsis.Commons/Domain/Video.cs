using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Category", "File")]
  public class Video : EntityBase, IComparable<Video>, IDimensionable
  {
    public short Bitrate { get; set; }
    public VideosCategory Category { get; set; }
    public long Duration { get; set; }
    public File File { get; set; }
    public short Height { get; set; }
    public short Width { get; set; }

    public Video()
    {
    }

    public Video(IDictionary<string, object> properties)
    {
      this.SetProperties(properties);
    }

    public Video(string id, File file, short bitrate, long duration, short height, short width, VideosCategory category = null)
    {
      this.Id = id;
      this.File = file;
      this.Bitrate = bitrate;
      this.Duration = duration;
      this.Height = height;
      this.Width = width;
      this.Category = category;
    }

    public override string ToString()
    {
      return this.File.ToString();
    }

    public int CompareTo(Video video)
    {
      return this.File.CompareTo(video.File);
    }
  }
}