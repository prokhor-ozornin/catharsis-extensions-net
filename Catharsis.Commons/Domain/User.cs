using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Username")]
  public class User : EntityBase, IComparable<User>, IEmailable, INameable, ITimeable
  {
    public DateTime DateCreated { get; set; }
    public string Email { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }

    public User()
    {
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
    }

    public User(IDictionary<string, object> properties) : this()
    {
      this.SetProperties(properties);
    }

    public User(string id, string username, string email, string name) : this()
    {
      this.Id = id;
      this.Username = username;
      this.Email = email;
      this.Name = name;
    }

    public override string ToString()
    {
      return this.Name;
    }
    
    public int CompareTo(User user)
    {
      return this.Username.CompareTo(user.Username);
    }
  }
}