using System;
using System.Linq;
using System.Linq.Expressions;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface ISpecification<T>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    Expression<Func<T, bool>> Expression { get; set; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="queryable"/> is a <c>null</c> reference.</exception>
    IQueryable<T> Filter(IQueryable<T> queryable);
  }
}
