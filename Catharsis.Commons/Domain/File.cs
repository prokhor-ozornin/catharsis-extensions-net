using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Hash")]
  public class File : EntityBase, IComparable<File>, INameable, ISizable, ITaggable, ITimeable
  {
    private ICollection<string> tags = new HashSet<string>();

    public string ContentType { get; set; }
    public byte[] Data { get; set; }
    public DateTime DateCreated { get; set; }
    public string Hash { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
    public string OriginalName { get; set; }
    public long Size { get; set; }

    [XmlIgnore]
    public ICollection<string> Tags { get { return this.tags; } }

    [XmlArray("Tags")]
    [XmlArrayItem("Tag")]
    public string[] TagsCollection
    {
      get
      {
        return this.tags.ToArray();
      }
      set
      {
        this.tags.Clear();
        if (value != null)
        {
          this.tags.AddAll(value);
        }
      }
    }

    public File()
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    public File(IDictionary<string, object> properties) : this()
    {
      this.SetProperties(properties);
    }

    public File(string id, string contentType, string name, string originalName, byte[] data, string hash, long size) : this()
    {
      this.Id = id;
      this.ContentType = contentType;
      this.Name = name;
      this.OriginalName = originalName;
      this.Data = data;
      this.Hash = hash;
      this.Size = size;
    }

    public override string ToString()
    {
      return this.Name;
    }

    public int CompareTo(File file)
    {
      return this.DateCreated.CompareTo(file.DateCreated);
    }
  }
}