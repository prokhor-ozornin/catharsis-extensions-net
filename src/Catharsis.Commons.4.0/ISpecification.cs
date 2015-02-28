using System;
using System.Linq;
using System.Linq.Expressions;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Represents a business rule or requirement that must be fulfilled by objects of specified type.</para>
  /// </summary>
  /// <typeparam name="T">Type of specification's elements.</typeparam>
  public interface ISpecification<T>
  {
    /// <summary>
    ///   <para>Lamba expression that represents a business rule in a predicate form.</para>
    ///   <para>Object of type <typeparam name="T"/>, for which that predicate evaluates to <c>true</c>, is considered to conform with current specification.</para>
    /// </summary>
    /// <typeparam name="T">Type of elements, supported by the predicate rule.</typeparam>
    Expression<Func<T, bool>> Expression { get; set; }

    /// <summary>
    ///   <para>Performs a filtering operation by taking <see cref="IQueryable{T}"/> object and returning a new object of the same type, often with a less number of elements.</para>
    ///   <para>Concrete filtering logic, applied to target <see cref="IQueryable{T}"/> source depends upon the implementation.</para>
    /// </summary>
    /// <param name="queryable"><see cref="IQueryable{T}"/> source to be filtered.</param>
    /// <returns>Altered/filtered version of <paramref name="queryable"/> source.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="queryable"/> is a <c>null</c> reference.</exception>
    IQueryable<T> Filter(IQueryable<T> queryable);
  }
}
