using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IEntityExtensions"/>.</para>
  /// </summary>
  public sealed class IEntityExtensionsTests
  {
    [EqualsAndHashCode("Id")]
    private sealed class Entity : EntityBase
    {
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IEntityExtensions.WithId{ENTITY}(IEnumerable{ENTITY}, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithId_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IEntityExtensions.WithId<IEntity>(null, "Id"));
      Assert.Throws<ArgumentNullException>(() => Enumerable.Empty<IEntity>().WithId(null));
      Assert.Throws<ArgumentException>(() => Enumerable.Empty<IEntity>().WithId(string.Empty));

      Assert.True(new[] { null, new Entity { Id = "Id" }, null, new Entity { Id = "Id_2" } }.WithId("Id").Equals(new Entity { Id = "Id" }));
    }
  }
}