using System;
using System.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IQueryable{T}"/>.</para>
  /// </summary>
  /// <seealso cref="IQueryable{T}"/>
  public static class IQueryableExtensions
  {
    /// <summary>
    ///   <para>Picks up random element from <see cref="IQueryable{T}"/> source and returns it.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in <see cref="IQueryable{T}"/> source.</typeparam>
    /// <param name="queryable">Source from which random element is to be taken.</param>
    /// <returns>Random member of <paramref name="queryable"/> source. If <paramref name="queryable"/> contains no elements, returns <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="queryable"/> is a <c>null</c> reference.</exception>
    public static T Random<T>(this IQueryable<T> queryable)
    {
      Assertion.NotNull(queryable);

      var max = queryable.Count();
      return (T) (max > 0 ? queryable.ElementAt(new Random().Next(max)) : (object) null);
    }
  }
}