using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Category", "File")]
  public class Image : EntityBase, IComparable<Image>, IDimensionable
  {
    public ImagesCategory Category { get; set; }
    public File File { get; set; }
    public short Height { get; set; }
    public short Width { get; set; }

    public Image()
    {
    }

    public Image(IDictionary<string, object> properties)
    {
      this.SetProperties(properties);
    }

    public Image(string id, File file, short height, short width, ImagesCategory category = null)
    {
      this.Id = id;
      this.File = file;
      this.Height = height;
      this.Width = width;
      this.Category = category;
    }

    public override string ToString()
    {
      return this.File.ToString();
    }

    public int CompareTo(Image image)
    {
      return this.File.CompareTo(image.File);
    }
  }
}