using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("Email", "Type")]
  public class Subscription : EntityBase, IComparable<Subscription>, IAuthorable, IEmailable, ITimeable, ITypeable
  {
    public string AuthorId { get; set; }
    public bool Active { get; set; }
    public DateTime DateCreated { get; set; }
    public string Email { get; set; }
    public DateTime? ExpiredOn { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Token { get; set; }
    public int Type { get; set; }

    public Subscription()
    {
      this.Active = true;
      this.DateCreated = DateTime.UtcNow;
      this.LastUpdated = DateTime.UtcNow;
      this.Token = Guid.NewGuid().ToString();
    }

    public Subscription(IDictionary<string, object> properties) : this()
    {
      this.SetProperties(properties);
    }

    public Subscription(string id, string authorId, string email, int type = 0, DateTime? expiredOn = null) : this()
    {
      this.Id = id;
      this.AuthorId = authorId;
      this.Email = email;
      this.Type = type;
      this.ExpiredOn = expiredOn;
    }

    public override string ToString()
    {
      return this.Email;
    }

    public int CompareTo(Subscription subscription)
    {
      return this.DateCreated.CompareTo(subscription.DateCreated);
    }
  }
}