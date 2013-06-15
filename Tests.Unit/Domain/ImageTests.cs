using System;
using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Image"/>.</para>
  /// </summary>
  public sealed class ImageTests : EntityUnitTests<Image>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="Image()"/>
    ///   <seealso cref="Image(IDictionary{string, object})"/>
    ///   <seealso cref="Image(string, File, short, short, ImagesCategory)"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var image = new Image();
      Assert.True(image.Id == null);
      Assert.True(image.Category == null);
      Assert.True(image.File == null);
      Assert.True(image.Height == 0);
      Assert.True(image.Width == 0);

      image = new Image(new Dictionary<string, object>()
        .AddNext("Id", "id")
        .AddNext("Category", new ImagesCategory())
        .AddNext("File", new File())
        .AddNext("Height", (short) 1)
        .AddNext("Width", (short) 2));
      Assert.True(image.Id == "id");
      Assert.True(image.Category != null);
      Assert.True(image.File != null);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);

      image = new Image("id", new File(), 1, 2, new ImagesCategory());
      Assert.True(image.Id == "id");
      Assert.True(image.Category != null);
      Assert.True(image.File != null);
      Assert.True(image.Height == 1);
      Assert.True(image.Width == 2);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Image.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new Image { File = new File { Name = "name" } }.ToString() == "name");
    }

    [Fact]
    public void EqualsAndHashCode()
    {
      this.TestEqualsAndHashCode(new Dictionary<string, object[]>()
        .AddNext("Category", new[] { new ImagesCategory { Name = "Name" }, new ImagesCategory { Name = "Name_2" } })
        .AddNext("File", new[] { new File { Hash = "Hash" }, new File { Hash = "Hash_2" } }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Image.CompareTo(Image)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new Image { File = new File { DateCreated = new DateTime(2000, 1, 1) } }.CompareTo(new Image { File = new File { DateCreated = new DateTime(2000, 1, 1) } }) == 0);
      Assert.True(new Image { File = new File { DateCreated = new DateTime(2000, 1, 1) } }.CompareTo(new Image { File = new File { DateCreated = new DateTime(2000, 1, 2) } }) < 0);
    }
  }
}