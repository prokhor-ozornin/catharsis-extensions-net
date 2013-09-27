using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="WebLinkExtensions"/>.</para>
  /// </summary>
  public sealed class WebLinkExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="WebLinkExtensions.InCategory"/> method.</para>
    /// </summary>
    [Fact]
    public void InWebLinksCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => WebLinkExtensions.InCategory(null, new WebLinksCategory()));

      Assert.False(Enumerable.Empty<WebLink>().InCategory(null).Any());
      Assert.False(Enumerable.Empty<WebLink>().InCategory(new WebLinksCategory()).Any());
      Assert.True(new[] { null, new WebLink { Category = new WebLinksCategory { Id = 1 } }, null, new WebLink { Category = new WebLinksCategory { Id = 2 } } }.InCategory(new WebLinksCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="WebLinkExtensions.OrderByCategoryName"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByWebLinksCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => WebLinkExtensions.OrderByCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new WebLink[] { null }.OrderByCategoryName().Any());
      var entities = new[] { new WebLink { Category = new WebLinksCategory { Name = "Second" } }, new WebLink { Category = new WebLinksCategory { Name = "First" } } };
      Assert.True(entities.OrderByCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="WebLinkExtensions.OrderByCategoryNameDescending"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByWebLinksCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => WebLinkExtensions.OrderByCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new WebLink[] { null }.OrderByCategoryNameDescending().Any());
      var entities = new[] { new WebLink { Category = new WebLinksCategory { Name = "First" } }, new WebLink { Category = new WebLinksCategory { Name = "Second" } } };
      Assert.True(entities.OrderByCategoryNameDescending().SequenceEqual(entities.Reverse()));
    }
  }
}