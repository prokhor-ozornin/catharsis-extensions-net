using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IEntity"/>.</para>
  ///   <seealso cref="IEntity"/>
  /// </summary>
  public static class IEntityExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static T WithId<T>(this IEnumerable<T> entities, long id) where T : IEntity
    {
      Assertion.NotNull(entities);

      return entities.First(entity => entity != null && entity.Id == id);
    }
  }
}