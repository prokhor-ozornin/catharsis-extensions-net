using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="FileExtensions"/>.</para>
  /// </summary>
  public sealed class FileExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="FileExtensions.WithContentType(IEnumerable{File}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithContentType_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileExtensions.WithContentType(null, string.Empty));

      Assert.False(Enumerable.Empty<File>().WithContentType(null).Any());
      Assert.False(Enumerable.Empty<File>().WithContentType(string.Empty).Any());
      Assert.True(new[] { null, new File { ContentType = "ContentType" }, null, new File { ContentType = "ContentType_2" } }.WithContentType("ContentType").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileExtensions.OrderByContentType(IEnumerable{File})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByContentType_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileExtensions.OrderByContentType(null));

      Assert.Throws<NullReferenceException>(() => new File[] { null }.OrderByContentType().Any());
      var entities = new[] { new File { ContentType = "Second" }, new File { ContentType = "First" } };
      Assert.True(entities.OrderByContentType().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileExtensions.OrderByContentTypeDescending(IEnumerable{File})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByContentTypeDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileExtensions.OrderByContentTypeDescending(null));

      Assert.Throws<NullReferenceException>(() => new File[] { null }.OrderByContentTypeDescending().Any());
      var entities = new[] { new File { ContentType = "First" }, new File { ContentType = "Second" } };
      Assert.True(entities.OrderByContentTypeDescending().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileExtensions.WithOriginalName(IEnumerable{File}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithOriginaName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileExtensions.WithOriginalName(null, string.Empty));

      Assert.False(Enumerable.Empty<File>().WithOriginalName(null).Any());
      Assert.False(Enumerable.Empty<File>().WithOriginalName(string.Empty).Any());
      Assert.True(new[] { null, new File { OriginalName = "OriginalName" }, null, new File { OriginalName = "OriginalName_2" } }.WithOriginalName("OriginalName").Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileExtensions.OrderByOriginalName(IEnumerable{File})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByOriginalName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileExtensions.OrderByOriginalName(null));

      Assert.Throws<NullReferenceException>(() => new File[] { null }.OrderByOriginalName().Any());
      var entities = new[] { new File { OriginalName = "Second" }, new File { OriginalName = "First" } };
      Assert.True(entities.OrderByOriginalName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="FileExtensions.OrderByOriginalNameDescending(IEnumerable{File})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByOriginalNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FileExtensions.OrderByOriginalNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new File[] { null }.OrderByOriginalNameDescending().Any());
      var entities = new[] { new File { OriginalName = "First" }, new File { OriginalName = "Second" } };
      Assert.True(entities.OrderByOriginalNameDescending().SequenceEqual(entities.Reverse()));
    }
  }
}