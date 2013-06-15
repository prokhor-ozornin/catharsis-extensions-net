using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  [Serializable]
  [EqualsAndHashCode("BirthDay", "BirthMonth", "BirthYear", "DeathDay", "DeathMonth", "DeathYear", "NameFirst", "NameLast", "NameMiddle")]
  public class Person : EntityBase, IComparable<Person>, IDescriptable, IImageable, IPersonalizable
  {
    public byte? BirthDay { get; set; }
    public byte? BirthMonth { get; set; }
    public short? BirthYear { get; set; }
    public byte? DeathDay { get; set; }
    public byte? DeathMonth { get; set; }
    public short? DeathYear { get; set; }
    public string Description { get; set; }
    public Image Image { get; set; }
    public string NameFirst { get; set; }
    public string NameLast { get; set; }
    public string NameMiddle { get; set; }

    public Person()
    {
    }

    public Person(IDictionary<string, object> properties)
    {
      this.SetProperties(properties);
    }

    public Person(string id, string nameFirst, string nameLast, string nameMiddle = null, string description = null, Image image = null, byte? birthDay = null, byte? birthMonth = null, short? birthYear = null, byte? deathDay = null, byte? deathMonth = null, short? deathYear = null)
    {
      this.Id = id;
      this.NameFirst = nameFirst;
      this.NameLast = nameLast;
      this.NameMiddle = nameMiddle;
      this.Description = description;
      this.Image = image;
      this.BirthDay = birthDay;
      this.BirthMonth = birthMonth;
      this.BirthYear = birthYear;
      this.DeathDay = deathDay;
      this.DeathMonth = deathMonth;
      this.DeathYear = deathYear;
    }

    public override string ToString()
    {
      return this.GetFullName();
    }

    public int CompareTo(Person person)
    {
      return this.NameLast.CompareTo(person.NameLast);
    }
  }
}