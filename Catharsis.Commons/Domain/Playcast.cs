using System;
using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Category")]
  public class Playcast : Item, IImageable
  {
    public Audio Audio { get; set; }
    public PlaycastsCategory Category { get; set; }
    public Image Image { get; set; }

    public Playcast()
    {
    }

    public Playcast(IDictionary<string, object> properties) : base(properties)
    {
    }

    public Playcast(string id, string authorId, string language, string name, string text, PlaycastsCategory category, Audio audio = null, Image image = null) : base(id, language, name, text, authorId)
    {
      this.Category = category;
      this.Audio = audio;
      this.Image = image;
    }
  }
}