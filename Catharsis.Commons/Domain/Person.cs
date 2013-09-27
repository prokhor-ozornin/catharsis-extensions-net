using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [EqualsAndHashCode("BirthDay,BirthMonth,BirthYear,DeathDay,DeathMonth,DeathYear,NameFirst,NameLast,NameMiddle")]
  public class Person : EntityBase, IComparable<Person>, IEquatable<Person>, IDescriptable, IImageable, IPersonalizable
  {
    private string nameFirst;
    private string nameLast;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual byte? BirthDay { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual byte? BirthMonth { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual short? BirthYear { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual byte? DeathDay { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual byte? DeathMonth { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual short? DeathYear { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual string Description { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual Image Image { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string NameFirst
    {
      get { return this.nameFirst; }
      set
      {
        Assertion.NotEmpty(value);

        this.nameFirst = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="value"/> is <see cref="string.Empty"/> string.</exception>
    public virtual string NameLast
    {
      get { return this.nameLast; }
      set
      {
        Assertion.NotEmpty(value);

        this.nameLast = value;
      }
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    public virtual string NameMiddle { get; set; }

    /// <summary>
    ///   <para>Creates new person.</para>
    /// </summary>
    public Person()
    {
    }

    /// <summary>
    ///   <para>Creates new person.</para>
    /// </summary>
    /// <param name="nameFirst"></param>
    /// <param name="nameLast"></param>
    /// <param name="nameMiddle"></param>
    /// <param name="description"></param>
    /// <param name="image"></param>
    /// <param name="birthDay"></param>
    /// <param name="birthMonth"></param>
    /// <param name="birthYear"></param>
    /// <param name="deathDay"></param>
    /// <param name="deathMonth"></param>
    /// <param name="deathYear"></param>
    /// <exception cref="ArgumentNullException">If either <paramref name="nameFirst"/> or <paramref name="nameLast"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="nameFirst"/> or <paramref name="nameLast"/> is <see cref="string.Empty"/> string.</exception>
    public Person(string nameFirst, string nameLast, string nameMiddle = null, string description = null, Image image = null, byte? birthDay = null, byte? birthMonth = null, short? birthYear = null, byte? deathDay = null, byte? deathMonth = null, short? deathYear = null)
    {
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

    /// <summary>
    ///   <para>Creates new person from its XML representation.</para>
    /// </summary>
    /// <param name="xml"><see cref="XElement"/> object, representing instance of <see cref="Person"/> type.</param>
    /// <returns>Recreated person object.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="xml"/> is a <c>null</c> reference.</exception>
    public static Person Xml(XElement xml)
    {
      Assertion.NotNull(xml);

      var person = new Person((string) xml.Element("NameFirst"), (string) xml.Element("NameLast"), (string) xml.Element("NameMiddle"), (string) xml.Element("Description"), xml.Element("Image") != null ? Image.Xml(xml.Element("Image")) : null, (byte?) (short?) xml.Element("BirthDay"), (byte?) (short?) xml.Element("BirthMonth"), (short?) xml.Element("BirthYear"), (byte?) (short?) xml.Element("DeathDay"), (byte?) (short?) xml.Element("DeathMonth"), (short?) xml.Element("DeathYear"));
      if (xml.Element("Id") != null)
      {
        person.Id = (long) xml.Element("Id");
      }
      return person;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public virtual bool Equals(Person other)
    {
      return base.Equals(other);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current person.</para>
    /// </summary>
    /// <returns>A string that represents the current person.</returns>
    public override string ToString()
    {
      return this.GetFullName();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    public virtual int CompareTo(Person person)
    {
      return this.NameLast.Compare(person.NameLast, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///   <para>Transforms current object to XML representation.</para>
    /// </summary>
    /// <returns><see cref="XElement"/> object, representing current <see cref="Person"/>.</returns>
    public override XElement Xml()
    {
      return base.Xml().AddContent(
        this.BirthDay.HasValue ? new XElement("BirthDay", this.BirthDay.GetValueOrDefault()) : null,
        this.BirthMonth.HasValue ? new XElement("BirthMonth", this.BirthMonth.GetValueOrDefault()) : null,
        this.BirthYear.HasValue ? new XElement("BirthYear", this.BirthYear.GetValueOrDefault()) : null,
        this.DeathDay.HasValue ? new XElement("DeathDay", this.DeathDay.GetValueOrDefault()) : null,
        this.DeathMonth.HasValue ? new XElement("DeathMonth", this.DeathMonth.GetValueOrDefault()) : null,
        this.DeathYear.HasValue ? new XElement("DeathYear", this.DeathYear.GetValueOrDefault()) : null,
        this.Description != null ? new XElement("Description", this.Description) : null,
        this.Image != null ? this.Image.Xml() : null,
        new XElement("NameFirst", this.NameFirst),
        new XElement("NameLast", this.NameLast),
        this.NameMiddle != null ? new XElement("NameMiddle", this.NameMiddle) : null);
    }
  }
}