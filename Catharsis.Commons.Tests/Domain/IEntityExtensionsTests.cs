using System;
using System.Collections.Generic;
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
    ///   <para>Performs testing of <see cref="IEntityExtensions.WithId{ENTITY}(IEnumerable{ENTITY}, long)"/> method.</para>
    /// </summary>
    [Fact]
    public void WithId_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IEntityExtensions.WithId<IEntity>(null, 1));

      Assert.True(new[] { null, new Entity { Id = 1 }, null, new Entity { Id = 2 } }.WithId(1).Equals(new Entity { Id = 1 }));
    }
  }
}