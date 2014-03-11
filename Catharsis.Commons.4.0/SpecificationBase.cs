using System;
using System.Linq;
using System.Linq.Expressions;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Basic implementation of <see cref="ISpecification{T}"/> interface.</para>
  /// </summary>
  /// <typeparam name="T">Type of specification's elements.</typeparam>
  public class SpecificationBase<T> : ISpecification<T>
  {
    private static readonly Expression<Func<T, bool>> DefaultExpression = x => true;
    private Expression<Func<T, bool>> expression = DefaultExpression;

    /// <summary>
    ///   <para>Creates new instance of specification.</para>
    /// </summary>
    /// <param name="expression">Lambda expression predicate rule. If not specified, default predicate expression is used, that always evaluates to <c>true</c>.</param>
    public SpecificationBase(Expression<Func<T, bool>> expression = null)
    {
      this.Expression = expression;
    }

    /// <summary>
    ///   <para>Lamba expression that represents a business rule in a predicate form.</para>
    ///   <para>Object of type <typeparam name="T"/>, for which that predicate evaluates to <c>true</c>, is considered to conform with current specification.</para>
    /// </summary>
    public Expression<Func<T, bool>> Expression
    {
      get { return this.expression; }
      set { this.expression = value ?? DefaultExpression; }
    }

    /// <summary>
    ///   <para>Performs a filtering operation by taking <see cref="IQueryable{T}"/> object and returning a new object of the same type, often with a less number of elements.</para>
    ///   <para>Concrete filtering logic, applied to target <see cref="IQueryable{T}"/> source depends upon the implementation.</para>
    /// </summary>
    /// <param name="queryable"><see cref="IQueryable{T}"/> source to be filtered.</param>
    /// <returns>Altered/filtered version of <paramref name="queryable"/> source.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="queryable"/> is a <c>null</c> reference.</exception>
    public IQueryable<T> Filter(IQueryable<T> queryable)
    {
      Assertion.NotNull(queryable);

      return this.FilterQuery(queryable.Where(this.Expression));
    }

    protected virtual IQueryable<T> FilterQuery(IQueryable<T> queryable)
    {
      Assertion.NotNull(queryable);

      return queryable;
    }
  }
}