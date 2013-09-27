using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ArtExtensions"/>.</para>
  /// </summary>
  public sealed class ArtsExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ArtExtensions.InAlbum(IEnumerable{Art}, ArtsAlbum)"/> method.</para>
    /// </summary>
    [Fact]
    public void InAlbum_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtExtensions.InAlbum(null, new ArtsAlbum()));

      Assert.False(Enumerable.Empty<Art>().InAlbum(null).Any());
      Assert.False(Enumerable.Empty<Art>().InAlbum(new ArtsAlbum()).Any());
      Assert.True(new[] { null, new Art { Album = new ArtsAlbum { Id = 1 } }, null, new Art { Album = new ArtsAlbum { Id = 2 } } }.InAlbum(new ArtsAlbum { Id = 2 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArtExtensions.OrderByAlbumName(IEnumerable{Art})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAlbumName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtExtensions.OrderByAlbumName(null));
      Assert.Throws<NullReferenceException>(() => new Art[] { null }.OrderByAlbumName().Any());

      var arts = new[] { new Art { Album = new ArtsAlbum { Name = "Second" } }, new Art { Album = new ArtsAlbum { Name = "First" } } };
      Assert.True(arts.OrderByAlbumName().SequenceEqual(arts.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArtExtensions.OrderByAlbumNameDescending(IEnumerable{Art})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByAlbumNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtExtensions.OrderByAlbumNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Art[] { null }.OrderByAlbumNameDescending().Any());

      var arts = new[] { new Art { Album = new ArtsAlbum { Name = "First" } }, new Art { Album = new ArtsAlbum { Name = "Second" } } };
      Assert.True(arts.OrderByAlbumNameDescending().SequenceEqual(arts.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArtExtensions.WithMaterial(IEnumerable{Art}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithMaterial_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtExtensions.WithMaterial(null, "Material"));

      Assert.False(Enumerable.Empty<Art>().WithMaterial(null).Any());
      Assert.False(Enumerable.Empty<Art>().WithMaterial(string.Empty).Any());
      Assert.True(new[] { null, new Art { Material = "Material" }, null, new Art { Material = "Material_2" } }.WithMaterial("Material").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArtExtensions.OrderByMaterial(IEnumerable{Art})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByMaterial_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtExtensions.OrderByMaterial(null));
      Assert.Throws<NullReferenceException>(() => new Art[] { null }.OrderByMaterial().Any());

      var arts = new[] { new Art { Material = "Second" }, new Art { Material = "First" } };
      Assert.True(arts.OrderByMaterial().SequenceEqual(arts.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ArtExtensions.OrderByMaterialDescending(IEnumerable{Art})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByMaterialDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ArtExtensions.OrderByMaterialDescending(null));
      Assert.Throws<NullReferenceException>(() => new Art[] { null }.OrderByMaterialDescending().Any());

      var arts = new[] { new Art { Material = "First" }, new Art { Material = "Second" } };
      Assert.True(arts.OrderByMaterialDescending().SequenceEqual(arts.Reverse()));
    }
  }
}