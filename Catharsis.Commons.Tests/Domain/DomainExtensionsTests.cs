using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="DomainExtensions"/>.</para>
  /// </summary>
  public sealed class DomainExtensionsTests
  {
    [EqualsAndHashCode("AccessGranted")]
    private sealed class AccessableEntity : EntityBase, IAccessable
    {
      public bool AccessGranted { get; set; }
    }

    [EqualsAndHashCode("AuthorId")]
    private sealed class AuthorableEntity : EntityBase, IAuthorable
    {
      public string AuthorId { get; set; }
    }

    [EqualsAndHashCode("Comments")]
    private sealed class CommentableEntity : EntityBase, ICommentable
    {
      public ICollection<Comment> Comments { get; private set; }
    }

    [EqualsAndHashCode("Description")]
    private sealed class DescriptableEntity : EntityBase, IDescriptable
    {
      public string Description { get; set; }
    }

    [EqualsAndHashCode("Height,Width")]
    private sealed class DimensionableEntity : EntityBase, IDimensionable
    {
      public short Height { get; set; }
      public short Width { get; set; }
    }

    [EqualsAndHashCode("Email")]
    private sealed class EmailableEntity : EntityBase, IEmailable
    {
      public string Email { get; set; }
    }

    [EqualsAndHashCode("Image")]
    private sealed class ImageableEntity : EntityBase, IImageable
    {
      public Image Image { get; set; }
    }

    [EqualsAndHashCode("InetAddress")]
    private sealed class InetAddressableEntity : EntityBase, IInetAddressable
    {
      public string InetAddress { get; set; }
    }

    [EqualsAndHashCode("Language")]
    private sealed class LocalizableEntity : EntityBase, ILocalizable
    {
      public string Language { get; set; }
    }

    [EqualsAndHashCode("Name")]
    private sealed class NameableEntity : EntityBase, INameable
    {
      public string Name { get; set; }
    }

    [EqualsAndHashCode("NameFirst,NameLast,NameMiddle")]
    private sealed class PersonalizableEntity : EntityBase, IPersonalizable
    {
      public string NameFirst { get; set; }
      public string NameLast { get; set; }
      public string NameMiddle { get; set; }
    }

    [EqualsAndHashCode("Size")]
    private sealed class SizableEntity : EntityBase, ISizable
    {
      public long Size { get; set; }
    }

    [EqualsAndHashCode("Status")]
    private sealed class StatusableEntity : EntityBase, IStatusable
    {
      public string Status { get; set; }
    }

    [EqualsAndHashCode("Tags")]
    private sealed class TaggableEntity : EntityBase, ITaggable
    {
      public ICollection<string> Tags { get; private set; }
    }

    [EqualsAndHashCode("Text")]
    private sealed class TextableEntity : EntityBase, ITextable
    {
      public string Text { get; set; }
    }

    [EqualsAndHashCode("DateCreated,LastUpdated")]
    private sealed class TimeableEntity : EntityBase, ITimeable
    {
      public DateTime DateCreated { get; set; }
      public DateTime LastUpdated { get; set; }
    }

    [EqualsAndHashCode("Type")]
    private sealed class TypeableEntity : EntityBase, ITypeable
    {
      public int Type { get; set; }
    }

    [EqualsAndHashCode("Url")]
    private sealed class UrlAddressableEntity : EntityBase, IUrlAddressable
    {
      public string Url { get; set; }
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.Allowed{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Allowed_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.Allowed<IAccessable>(null));

      Assert.False(Enumerable.Empty<IAccessable>().Allowed().Any());
      Assert.True(new[] { null, new AccessableEntity { AccessGranted = true }, null, new AccessableEntity { AccessGranted = false } }.Allowed().Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.Denied{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Denied_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.Denied<IAccessable>(null));

      Assert.False(Enumerable.Empty<IAccessable>().Denied().Any());
      Assert.True(new[] { null, new AccessableEntity { AccessGranted = true }, null, new AccessableEntity { AccessGranted = false } }.Denied().Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithAuthor{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithAuthor_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithAuthor<IAuthorable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IAuthorable>().WithAuthor(null).Any());
      Assert.False(Enumerable.Empty<IAuthorable>().WithAuthor(string.Empty).Any());
      Assert.True(new[] { null, new AuthorableEntity { AuthorId = "AuthorId" }, null, new AuthorableEntity { AuthorId = "AuthorId_2" } }.WithAuthor("AuthorId").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByAuthor{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAuthor_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByAuthor<IAuthorable>(null));
      Assert.Throws<NullReferenceException>(() => new AuthorableEntity[] { null }.OrderByAuthor().Any());

      var entities = new[] { new AuthorableEntity { AuthorId = "Second" }, new AuthorableEntity { AuthorId = "First" } };
      Assert.True(entities.OrderByAuthor().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByAuthorDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAuthorDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByAuthorDescending<IAuthorable>(null));
      Assert.Throws<NullReferenceException>(() => new AuthorableEntity[] { null }.OrderByAuthorDescending().Any());

      var entities = new[] { new AuthorableEntity { AuthorId = "First" }, new AuthorableEntity { AuthorId = "Second" } };
      Assert.True(entities.OrderByAuthorDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithDescription{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithDescription_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithDescription<IDescriptable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IDescriptable>().WithDescription(null).Any());
      Assert.False(Enumerable.Empty<IDescriptable>().WithDescription(string.Empty).Any());
      Assert.True(new[] { null, new DescriptableEntity { Description = "Description" }, new DescriptableEntity { Description = "Description_2" } }.WithDescription("Description").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithHeight{T}(IEnumerable{T}, short?, short?)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithHeight_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithHeight<IDimensionable>(null));

      Assert.False(Enumerable.Empty<IDimensionable>().WithHeight(0, 0).Any());

      var entities = new[] { null, new DimensionableEntity { Height = 1 }, null, new DimensionableEntity { Height = 2 } };
      Assert.False(entities.WithHeight(0, 0).Any());
      Assert.True(entities.WithHeight(0, 1).Count() == 1);
      Assert.True(entities.WithHeight(1, 1).Count() == 1);
      Assert.True(entities.WithHeight(1, 2).Count() == 2);
      Assert.True(entities.WithHeight(2, 3).Count() == 1);
      Assert.False(entities.WithHeight(3, 3).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByHeight{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByHeight_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByHeight<IDimensionable>(null));
      Assert.Throws<NullReferenceException>(() => new DimensionableEntity[] { null }.OrderByHeight().Any());

      var entities = new[] { new DimensionableEntity { Height = 2 }, new DimensionableEntity { Height = 1 } };
      Assert.True(entities.OrderByHeight().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByHeightDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByHeightDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByHeight<IDimensionable>(null));
      Assert.Throws<NullReferenceException>(() => new DimensionableEntity[] { null }.OrderByHeightDescending().Any());

      var entities = new[] { new DimensionableEntity { Height = 1 }, new DimensionableEntity { Height = 2 } };
      Assert.True(entities.OrderByHeightDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithWidth{T}(IEnumerable{T}, short?, short?)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithWidth_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithWidth<IDimensionable>(null));

      Assert.False(Enumerable.Empty<IDimensionable>().WithWidth(0, 0).Any());

      var entities = new[] { null, new DimensionableEntity { Width = 1 }, null, new DimensionableEntity { Width = 2 } };
      Assert.False(entities.WithWidth(0, 0).Any());
      Assert.True(entities.WithWidth(0, 1).Count() == 1);
      Assert.True(entities.WithWidth(1, 1).Count() == 1);
      Assert.True(entities.WithWidth(1, 2).Count() == 2);
      Assert.True(entities.WithWidth(2, 3).Count() == 1);
      Assert.False(entities.WithWidth(3, 3).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByWidth{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByWidth_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByWidth<IDimensionable>(null));
      Assert.Throws<NullReferenceException>(() => new DimensionableEntity[] { null }.OrderByWidth().Any());

      var entities = new[] { new DimensionableEntity { Width = 2 }, new DimensionableEntity { Width = 1 } };
      Assert.True(entities.OrderByWidth().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByWidthDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByWidthDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByWidthDescending<IDimensionable>(null));
      Assert.Throws<NullReferenceException>(() => new DimensionableEntity[] { null }.OrderByWidthDescending().Any());

      var entities = new[] { new DimensionableEntity { Width = 1 }, new DimensionableEntity { Width = 2 } };
      Assert.True(entities.OrderByWidthDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithEmail{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithEmail_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithEmail<IEmailable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IEmailable>().WithEmail(null).Any());
      Assert.False(Enumerable.Empty<IEmailable>().WithEmail(string.Empty).Any());
      Assert.True(new[] { null, new EmailableEntity { Email = "email@mail.ru" }, null, new EmailableEntity { Email = "email@mail2.ru" } }.WithEmail("email@mail.ru").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithInetAddress{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithInetAddress_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithInetAddress<IInetAddressable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IInetAddressable>().WithInetAddress(null).Any());
      Assert.False(Enumerable.Empty<IInetAddressable>().WithInetAddress(string.Empty).Any());
      Assert.True(new[] { null, new InetAddressableEntity { InetAddress = "InetAddress" }, null, new InetAddressableEntity { InetAddress = "InetAddress_2" } }.WithInetAddress("InetAddress").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithLanguage{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithLanguage_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithLanguage<ILocalizable>(null, string.Empty));

      Assert.False(Enumerable.Empty<ILocalizable>().WithLanguage(null).Any());
      Assert.False(Enumerable.Empty<ILocalizable>().WithLanguage(string.Empty).Any());
      Assert.True(new[] { null, new LocalizableEntity { Language = "Language" }, null, new LocalizableEntity { Language = "Language_2" } }.WithLanguage("Language").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithCulture{T}(IEnumerable{T}, CultureInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithCulture_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithCulture<ILocalizable>(null, CultureInfo.CurrentCulture));

      Assert.False(Enumerable.Empty<ILocalizable>().WithCulture(null).Any());
      Assert.False(Enumerable.Empty<ILocalizable>().WithCulture(CultureInfo.CurrentCulture).Any());
      Assert.True(new[] { null, new LocalizableEntity { Language = "en" }, null, new LocalizableEntity { Language = "ru" } }.WithCulture(CultureInfo.GetCultureInfo("ru")).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithName{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithName<INameable>(null, string.Empty));

      Assert.False(Enumerable.Empty<INameable>().WithName(null).Any());
      Assert.False(Enumerable.Empty<INameable>().WithName(string.Empty).Any());
      Assert.True(new[] { null, new NameableEntity { Name = "Name" }, null, new NameableEntity { Name = "Name_2" } }.WithName("Name").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByName{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByName<INameable>(null));
      Assert.Throws<NullReferenceException>(() => new NameableEntity[] { null }.OrderByName().Any());

      var entities = new[] { new NameableEntity { Name = "Second" }, new NameableEntity { Name = "First" } };
      Assert.True(entities.OrderByName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByNameDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByNameDescending<INameable>(null));
      Assert.Throws<NullReferenceException>(() => new NameableEntity[] { null }.OrderByNameDescending().Any());

      var entities = new[] { new NameableEntity { Name = "First" }, new NameableEntity { Name = "Second" } };
      Assert.True(entities.OrderByNameDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithFirstName{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithFirstName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithFirstName<IPersonalizable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IPersonalizable>().WithFirstName(null).Any());
      Assert.False(Enumerable.Empty<IPersonalizable>().WithFirstName(string.Empty).Any());
      Assert.True(new[] { null, new Article { Category = new ArticlesCategory { Id = "1" } }, null, new Article { Category = new ArticlesCategory { Id = "2" } } }.InArticlesCategory(new ArticlesCategory { Id = "1" }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByFirstName{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByFirstName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByFirstName<IPersonalizable>(null));
      Assert.Throws<NullReferenceException>(() => new PersonalizableEntity[] { null }.OrderByFirstName().Any());

      var entities = new[] { new PersonalizableEntity { NameFirst = "Second" }, new PersonalizableEntity { NameFirst = "First" } };
      Assert.True(entities.OrderByFirstName().SequenceEqual(entities.Reverse()));
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByFirstNameDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByFirstNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByFirstNameDescending<IPersonalizable>(null));
      Assert.Throws<NullReferenceException>(() => new PersonalizableEntity[] { null }.OrderByFirstNameDescending().Any());

      var entities = new[] { new PersonalizableEntity { NameFirst = "First" }, new PersonalizableEntity { NameFirst = "Second" } };
      Assert.True(entities.OrderByFirstNameDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithLastName{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithLastName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithLastName<IPersonalizable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IPersonalizable>().WithLastName(null).Any());
      Assert.False(Enumerable.Empty<IPersonalizable>().WithLastName(string.Empty).Any());
      Assert.True(new[] { null, new PersonalizableEntity { NameLast = "NameLast" }, null, new PersonalizableEntity { NameLast = "NameLast_2" } }.WithLastName("NameLast").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByLastName{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByLastName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByLastName<IPersonalizable>(null));
      Assert.Throws<NullReferenceException>(() => new PersonalizableEntity[] { null }.OrderByLastName().Any());

      var entities = new[] { new PersonalizableEntity { NameLast = "Second" }, new PersonalizableEntity { NameLast = "First" } };
      Assert.True(entities.OrderByLastName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithMiddleName{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByLastNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByLastNameDescending<IPersonalizable>(null));
      Assert.Throws<NullReferenceException>(() => new PersonalizableEntity[] { null }.OrderByLastNameDescending().Any());

      var entities = new[] { new PersonalizableEntity { NameLast = "First" }, new PersonalizableEntity { NameLast = "Second" } };
      Assert.True(entities.OrderByLastNameDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithMiddleName{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithMiddleName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithMiddleName<IPersonalizable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IPersonalizable>().WithMiddleName(null).Any());
      Assert.False(Enumerable.Empty<IPersonalizable>().WithMiddleName(string.Empty).Any());
      Assert.True(new[] { null, new PersonalizableEntity { NameMiddle = "NameMiddle" }, null, new PersonalizableEntity { NameMiddle = "NameMiddle_2" } }.WithMiddleName("NameMiddle").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByMiddleName{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByMiddleName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByMiddleName<IPersonalizable>(null));
      Assert.Throws<NullReferenceException>(() => new PersonalizableEntity[] { null }.OrderByMiddleName().Any());

      var entities = new[] { new PersonalizableEntity { NameMiddle = "Second" }, new PersonalizableEntity { NameMiddle = "First" } };
      Assert.True(entities.OrderByMiddleName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByMiddleNameDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByMiddleNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByMiddleNameDescending<IPersonalizable>(null));
      Assert.Throws<NullReferenceException>(() => new PersonalizableEntity[] { null }.OrderByMiddleNameDescending().Any());

      var entities = new[] { new PersonalizableEntity { NameMiddle = "First" }, new PersonalizableEntity { NameMiddle = "Second" } };
      Assert.True(entities.OrderByMiddleNameDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.GetFullName{T}(T)"/> method.</para>
    /// </summary>
    [Fact]
    public void GetFullName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.GetFullName<IPersonalizable>(null));

      Assert.True(new PersonalizableEntity { NameFirst = "NameFirst", NameLast = "NameLast", NameMiddle = string.Empty}.GetFullName() == "NameLast NameFirst");
      Assert.True(new PersonalizableEntity { NameFirst = "NameFirst", NameLast = "NameLast", NameMiddle = "NameMiddle"}.GetFullName() == "NameLast NameFirst NameMiddle");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithSize{T}(IEnumerable{T}, long?, long?)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithSize_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithSize<ISizable>(null));

      Assert.False(Enumerable.Empty<ISizable>().WithSize(0, 0).Any());

      var entities = new[] { null, new SizableEntity { Size = 1 }, null, new SizableEntity { Size = 2 } };
      Assert.False(entities.WithSize(0, 0).Any());
      Assert.True(entities.WithSize(0, 1).Count() == 1);
      Assert.True(entities.WithSize(1, 1).Count() == 1);
      Assert.True(entities.WithSize(1, 2).Count() == 2);
      Assert.True(entities.WithSize(2, 3).Count() == 1);
      Assert.False(entities.WithSize(3, 3).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderBySize{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderBySize_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderBySize<ISizable>(null));
      Assert.Throws<NullReferenceException>(() => new SizableEntity[] { null }.OrderBySize().Any());

      var entities = new[] { new SizableEntity { Size = 2 }, new SizableEntity { Size = 1 } };
      Assert.True(entities.OrderBySize().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderBySizeDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderBySizeDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderBySizeDescending<ISizable>(null));
      Assert.Throws<NullReferenceException>(() => new SizableEntity[] { null }.OrderBySizeDescending().Any());

      var entities = new[] { new SizableEntity { Size = 1 }, new SizableEntity { Size = 2 } };
      Assert.True(entities.OrderBySizeDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithStatus{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithStatus_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithStatus<IStatusable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IStatusable>().WithStatus(null).Any());
      Assert.False(Enumerable.Empty<IStatusable>().WithStatus(string.Empty).Any());
      Assert.True(new[] { null, new StatusableEntity { Status = "Status" }, null, new StatusableEntity { Status = "Status_2" } }.WithStatus("Status").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithText{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithText_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithText<ITextable>(null, string.Empty));

      Assert.False(Enumerable.Empty<ITextable>().WithText(null).Any());
      Assert.False(Enumerable.Empty<ITextable>().WithText(string.Empty).Any());
      Assert.True(new[] { null, new TextableEntity { Text = "Text" }, null, new TextableEntity { Text = "Text_2" } }.WithText("Text").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.CreatedOn{T}(IEnumerable{T}, DateTime?, DateTime?)"/> method.</para>
    /// </summary>
    [Fact]
    public void CreatedOn_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.CreatedOn<ITimeable>(null));

      Assert.False(Enumerable.Empty<ITimeable>().CreatedOn().Any());
      Assert.False(Enumerable.Empty<ITimeable>().CreatedOn(DateTime.MinValue).Any());
      Assert.False(Enumerable.Empty<ITimeable>().CreatedOn(null, DateTime.MaxValue).Any());

      var firstDate = DateTime.Today;
      var secondDate = DateTime.UtcNow;
      var dates = new[] { null, new TimeableEntity { DateCreated = firstDate }, null, new TimeableEntity { DateCreated = secondDate } };
      var filteredDates = new[] { new TimeableEntity { DateCreated = firstDate }, new TimeableEntity { DateCreated = secondDate } };
      Assert.True(ReferenceEquals(dates.CreatedOn(), dates));
      Assert.True(dates.CreatedOn(DateTime.MinValue).SequenceEqual(filteredDates));
      Assert.True(dates.CreatedOn(null, DateTime.MaxValue).SequenceEqual(filteredDates));
      Assert.True(dates.CreatedOn(DateTime.MinValue, DateTime.MaxValue).SequenceEqual(filteredDates));
      Assert.True(dates.CreatedOn(firstDate, secondDate).SequenceEqual(filteredDates));
      Assert.False(dates.CreatedOn(DateTime.MinValue, DateTime.MinValue).Any());
      Assert.False(dates.CreatedOn(DateTime.MaxValue, DateTime.MaxValue).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByCreatedOn{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCreatedOn_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByCreatedOn<ITimeable>(null));
      Assert.Throws<NullReferenceException>(() => new TimeableEntity[] { null }.OrderByCreatedOn().Any());

      var entities = new[] { new TimeableEntity { DateCreated = new DateTime(2000, 1, 2) }, new TimeableEntity { DateCreated = new DateTime(2000, 1, 1) } };
      Assert.True(entities.OrderByCreatedOn().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByCreatedOnDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCreatedOnDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByCreatedOnDescending<ITimeable>(null));
      Assert.Throws<NullReferenceException>(() => new TimeableEntity[] { null }.OrderByCreatedOnDescending().Any());

      var entities = new[] { new TimeableEntity { DateCreated = new DateTime(2000, 1, 1) }, new TimeableEntity { DateCreated = new DateTime(2000, 1, 2) } };
      Assert.True(entities.OrderByCreatedOnDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.UpdatedOn{T}(IEnumerable{T}, DateTime?, DateTime?)"/> method.</para>
    /// </summary>
    [Fact]
    public void UpdatedOn_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.UpdatedOn<ITimeable>(null));

      var firstDate = DateTime.Today;
      var secondDate = DateTime.UtcNow;
      var dates = new[] { null, new TimeableEntity { LastUpdated = firstDate }, null, new TimeableEntity { LastUpdated = secondDate } };
      var filteredDates = new[] { new TimeableEntity { LastUpdated = firstDate }, new TimeableEntity { LastUpdated = secondDate } };
      Assert.True(ReferenceEquals(dates.UpdatedOn(), dates));
      Assert.True(dates.UpdatedOn(DateTime.MinValue).SequenceEqual(filteredDates));
      Assert.True(dates.UpdatedOn(null, DateTime.MaxValue).SequenceEqual(filteredDates));
      Assert.True(dates.UpdatedOn(DateTime.MinValue, DateTime.MaxValue).SequenceEqual(filteredDates));
      Assert.True(dates.UpdatedOn(firstDate, secondDate).SequenceEqual(filteredDates));
      Assert.False(dates.UpdatedOn(DateTime.MinValue, DateTime.MinValue).Any());
      Assert.False(dates.UpdatedOn(DateTime.MaxValue, DateTime.MaxValue).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByUpdatedOn{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByUpdatedOn_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByUpdatedOn<ITimeable>(null));
      Assert.Throws<NullReferenceException>(() => new TimeableEntity[] { null }.OrderByUpdatedOn().Any());

      var entities = new[] { new TimeableEntity { LastUpdated = new DateTime(2000, 1, 2) }, new TimeableEntity { LastUpdated = new DateTime(2000, 1, 1) } };
      Assert.True(entities.OrderByUpdatedOn().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.OrderByUpdatedOnDescending{T}(IEnumerable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByUpdatedDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.OrderByUpdatedOnDescending<ITimeable>(null));
      Assert.Throws<NullReferenceException>(() => new TimeableEntity[] { null }.OrderByUpdatedOnDescending().Any());

      var entities = new[] { new TimeableEntity { LastUpdated = new DateTime(2000, 1, 1) }, new TimeableEntity { LastUpdated = new DateTime(2000, 1, 2) } };
      Assert.True(entities.OrderByUpdatedOnDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithType{T}(IEnumerable{T}, int)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithType_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithType<ITypeable>(null, 0));

      Assert.False(Enumerable.Empty<ITypeable>().WithType(0).Any());
      Assert.True(new[] { null, new TypeableEntity { Type = 1 }, null, new TypeableEntity { Type = 2 } }.WithType(1).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DomainExtensions.WithUrl{T}(IEnumerable{T}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithUrl_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DomainExtensions.WithUrl<IUrlAddressable>(null, string.Empty));

      Assert.False(Enumerable.Empty<IUrlAddressable>().WithUrl(null).Any());
      Assert.False(Enumerable.Empty<IUrlAddressable>().WithUrl(string.Empty).Any());
      Assert.True(new[] { null, new UrlAddressableEntity { Url = "Url" }, null, new UrlAddressableEntity { Url = "Url_2" } }.WithUrl("Url").Count() == 1);
    }
  }
}