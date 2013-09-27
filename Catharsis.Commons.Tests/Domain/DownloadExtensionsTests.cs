using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="DownloadExtensions"/>.</para>
  /// </summary>
  public sealed class DownloadExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="DownloadExtensions.InCategory(IEnumerable{Download}, DownloadsCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DownloadExtensions.InCategory(null, new DownloadsCategory()));

      Assert.False(Enumerable.Empty<Download>().InCategory(null).Any());
      Assert.False(Enumerable.Empty<Download>().InCategory(new DownloadsCategory()).Any());
      Assert.True(new[] { null, new Download { Category = new DownloadsCategory { Id = 1 } }, null, new Download { Category = new DownloadsCategory { Id = 2 } } }.InCategory(new DownloadsCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DownloadExtensions.OrderByCategoryName(IEnumerable{Download})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DownloadExtensions.OrderByCategoryName(null));
      Assert.Throws<NullReferenceException>(() => new Download[] { null }.OrderByCategoryName().Any());

      var downloads = new[] { new Download { Category = new DownloadsCategory { Name = "Second" } }, new Download { Category = new DownloadsCategory { Name = "First" } } };
      Assert.True(downloads.OrderByCategoryName().SequenceEqual(downloads.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DownloadExtensions.OrderByCategoryNameDescending(IEnumerable{Download})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DownloadExtensions.OrderByCategoryNameDescending(null));
      Assert.Throws<NullReferenceException>(() => new Download[] { null }.OrderByCategoryNameDescending().Any());

      var downloads = new[] { new Download { Category = new DownloadsCategory { Name = "First" } }, new Download { Category = new DownloadsCategory { Name = "Second" } } };
      Assert.True(downloads.OrderByCategoryNameDescending().SequenceEqual(downloads.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DownloadExtensions.WithDownloads(IEnumerable{Download}, long?, long?)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithDownloads_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DownloadExtensions.WithDownloads(null));

      Assert.False(Enumerable.Empty<Download>().WithDownloads(0, 0).Any());

      var downloads = new[] { null, new Download { Downloads = 1 }, null, new Download { Downloads = 2 } };
      Assert.False(downloads.WithDownloads(0, 0).Any());
      Assert.True(downloads.WithDownloads(0, 1).Count() == 1);
      Assert.True(downloads.WithDownloads(1, 1).Count() == 1);
      Assert.True(downloads.WithDownloads(1, 2).Count() == 2);
      Assert.True(downloads.WithDownloads(2, 3).Count() == 1);
      Assert.False(downloads.WithDownloads(3, 3).Any());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DownloadExtensions.OrderByDownloads(IEnumerable{Download})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByDownloads_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DownloadExtensions.OrderByDownloads(null));
      Assert.Throws<NullReferenceException>(() => new Download[] { null }.OrderByDownloads().Any());

      var downloads = new[] { new Download { Downloads = 2 }, new Download { Downloads = 1 } };
      Assert.True(downloads.OrderByDownloads().SequenceEqual(downloads.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DownloadExtensions.OrderByDownloadsDescending(IEnumerable{Download})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByDownloadsDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DownloadExtensions.OrderByDownloadsDescending(null));
      Assert.Throws<NullReferenceException>(() => new Download[] { null }.OrderByDownloadsDescending().Any());

      var downloads = new[] { new Download { Downloads = 1 }, new Download { Downloads = 2 } };
      Assert.True(downloads.OrderByDownloadsDescending().SequenceEqual(downloads.Reverse()));
    }
  }
}