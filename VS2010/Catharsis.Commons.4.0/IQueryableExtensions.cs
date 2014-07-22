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
    ///   <para>Performs "pagination" of specified <see cref="IQueryable{T}"/> source, returning a fragment ("page") of its contents.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in <see cref="IQueryable{T}"/> source.</typeparam>
    /// <param name="self">Source from which a fragment is to be taken.</param>
    /// <param name="page">Number of fragment/slice that is to be taken. Numbering starts from 1.</param>
    /// <param name="pageSize">Size of fragment ("page"), number of entities to be taken. Must be a positive number.</param>
    /// <returns>Source that represent a fragment of the original <paramref name="self"/> and consists of no more than <paramref name="pageSize"/> elements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> self, int page = 1, int pageSize = 10)
    {
      Assertion.NotNull(self);

      if (page <= 0)
      {
        page = 1;
      }

      if (pageSize <= 0)
      {
        pageSize = 10;
      }

      return self.Skip((page - 1) * pageSize).Take(pageSize);
    }

    /// <summary>
    ///   <para>Picks up random element from <see cref="IQueryable{T}"/> source and returns it.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements in <see cref="IQueryable{T}"/> source.</typeparam>
    /// <param name="self">Source from which random element is to be taken.</param>
    /// <returns>Random member of <paramref name="self"/> source. If <paramref name="self"/> contains no elements, returns <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static T Random<T>(this IQueryable<T> self)
    {
      Assertion.NotNull(self);

      var max = self.Count();
      return (T) (max > 0 ? self.ElementAt(new Random().Next(max)) : (object) null);
    }
  }
}