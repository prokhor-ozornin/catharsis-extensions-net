using System;
using System.Linq;
using System.Linq.Expressions;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class SpecificationBase<T> : ISpecification<T>
  {
    private static readonly Expression<Func<T, bool>> DefaultExpression = x => true;
    private Expression<Func<T, bool>> expression = DefaultExpression;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="expression"></param>
    public SpecificationBase(Expression<Func<T, bool>> expression = null)
    {
      this.Expression = expression;
    }

    /// <summary>
    ///   <para>Implementation of <see cref="ISpecification{T}.Expression"/> </para>
    /// </summary>
    public Expression<Func<T, bool>> Expression
    {
      get { return this.expression; }
      set { this.expression = value ?? DefaultExpression; }
    }

    /// <summary>
    ///   <para>Implementation of <see cref="ISpecification{T}.Filter(IQueryable{T})"/> method.</para>
    /// </summary>
    public IQueryable<T> Filter(IQueryable<T> queryable)
    {
      Assertion.NotNull(queryable);

      return this.FilterQuery(queryable.Where(this.Expression));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="queryable"/> is a <c>null</c> reference.</exception>
    protected virtual IQueryable<T> FilterQuery(IQueryable<T> queryable)
    {
      Assertion.NotNull(queryable);

      return queryable;
    }
  }
}