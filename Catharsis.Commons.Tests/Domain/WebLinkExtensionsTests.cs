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
    ///   <para>Performs testing of <see cref="WebLinkExtensions.InWebLinksCategory(IEnumerable{WebLink}, WebLinksCategory)"/> method.</para>
    /// </summary>
    [Fact]
    public void InWebLinksCategory_Method()
    {
      Assert.Throws<ArgumentNullException>(() => WebLinkExtensions.InWebLinksCategory(null, new WebLinksCategory()));

      Assert.False(Enumerable.Empty<WebLink>().InWebLinksCategory(null).Any());
      Assert.False(Enumerable.Empty<WebLink>().InWebLinksCategory(new WebLinksCategory()).Any());
      Assert.True(new[] { null, new WebLink { Category = new WebLinksCategory { Id = 1 } }, null, new WebLink { Category = new WebLinksCategory { Id = 2 } } }.InWebLinksCategory(new WebLinksCategory { Id = 1 }).Count() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="WebLinkExtensions.OrderByWebLinksCategoryName(IEnumerable{WebLink})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByWebLinksCategoryName_Method()
    {
      Assert.Throws<ArgumentNullException>(() => WebLinkExtensions.OrderByWebLinksCategoryName(null));

      Assert.Throws<NullReferenceException>(() => new WebLink[] { null }.OrderByWebLinksCategoryName().Any());
      var entities = new[] { new WebLink { Category = new WebLinksCategory { Name = "Second" } }, new WebLink { Category = new WebLinksCategory { Name = "First" } } };
      Assert.True(entities.OrderByWebLinksCategoryName().SequenceEqual(entities.Reverse()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="WebLinkExtensions.OrderByWebLinksCategoryNameDescending(IEnumerable{WebLink})"/> method.</para>
    /// </summary>
    [Fact]
    public void OrderByWebLinksCategoryNameDescending_Method()
    {
      Assert.Throws<ArgumentNullException>(() => WebLinkExtensions.OrderByWebLinksCategoryNameDescending(null));

      Assert.Throws<NullReferenceException>(() => new WebLink[] { null }.OrderByWebLinksCategoryNameDescending().Any());
      var entities = new[] { new WebLink { Category = new WebLinksCategory { Name = "First" } }, new WebLink { Category = new WebLinksCategory { Name = "Second" } } };
      Assert.True(entities.OrderByWebLinksCategoryNameDescending().SequenceEqual(entities.Reverse()));
    }
  }
}