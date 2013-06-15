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
    /// <exception cref="ArgumentNullException">If either <paramref name="entities"/> or <paramref name="id"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="id"/> is <see cref="string.Empty"/> string.</exception>
    public static T WithId<T>(this IEnumerable<T> entities, string id) where T : IEntity
    {
      Assertion.NotNull(entities);
      Assertion.NotEmpty(id);

      return entities.First(entity => entity != null && entity.Id == id);
    }
  }
}