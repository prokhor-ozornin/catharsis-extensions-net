using System.Collections.Generic;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="ENTITY"></typeparam>
  public abstract class EntityUnitTests<ENTITY>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="properties"></param>
    protected void TestEqualsAndHashCode(IDictionary<string, object[]> properties)
    {
      var entity = typeof(ENTITY).NewInstance();

      Assert.True(entity.Equals(entity));
      Assert.True(entity.GetHashCode() == entity.GetHashCode());
      Assert.True(entity.Equals(typeof(ENTITY).NewInstance()));
      Assert.True(typeof(ENTITY).NewInstance().GetHashCode() == typeof(ENTITY).NewInstance().GetHashCode());

      if (properties == null)
      {
        return;
      }

      properties.Each(property =>
      {
        Assert.True(typeof(ENTITY).NewInstance(new Dictionary<string, object>().AddNext(property.Key, property.Value[0])).Equals(typeof(ENTITY).NewInstance(new Dictionary<string, object>().AddNext(property.Key, property.Value[0]))));
        Assert.True(typeof(ENTITY).NewInstance(new Dictionary<string, object>().AddNext(property.Key, property.Value[0])).GetHashCode() == typeof(ENTITY).NewInstance(new Dictionary<string, object>().AddNext(property.Key, property.Value[0])).GetHashCode());

        Assert.False(typeof(ENTITY).NewInstance(new Dictionary<string, object>().AddNext(property.Key, property.Value[0])).Equals(typeof(ENTITY).NewInstance(new Dictionary<string, object>().AddNext(property.Key, property.Value[1]))));
        Assert.True(typeof(ENTITY).NewInstance(new Dictionary<string, object>().AddNext(property.Key, property.Value[0])).GetHashCode() != typeof(ENTITY).NewInstance(new Dictionary<string, object>().AddNext(property.Key, property.Value[1])).GetHashCode());
      });
    }
  }
}