using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("AuthorId", "Type", "Username")]
  public class Profile : EntityBase, IComparable<Profile>, IAuthorable, INameable, IUrlAddressable
  {
    public string AuthorId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public string Username { get; set; }

    public Profile()
    {
    }

    public Profile(IDictionary<string, object> properties)
    {
      this.SetProperties(properties);
    }

    public Profile(string id, string authorId, string name, string username, string type, string url, string email = null, string photo = null)
    {
      this.Id = id;
      this.AuthorId = authorId;
      this.Name = name;
      this.Username = username;
      this.Type = type;
      this.Url = url;
      this.Email = email;
      this.Photo = photo;
    }

    public override string ToString()
    {
      return this.Name;
    }

    public int CompareTo(Profile profile)
    {
      return this.Username.CompareTo(profile.Username);
    }
  }
}