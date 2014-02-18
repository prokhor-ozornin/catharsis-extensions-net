using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="SpecificationBase{T}"/>.</para>
  /// </summary>
  public sealed class SpecificationBaseTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="SpecificationBase{T}(Expression{Func{T, bool}})"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      Expression<Func<object, bool>> expression = x => x == null;

      var specification = new SpecificationBase<object>(expression);
      Assert.True(ReferenceEquals(specification.Expression, expression));

      Assert.NotNull(new SpecificationBase<object>().Field("DefaultExpression").To<Expression<Func<object, bool>>>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SpecificationBase{T}.Expression"/> property.</para>
    /// </summary>
    [Fact]
    public void Expression_Property()
    {
      Expression<Func<object, bool>> expression = x => x == null;
      var specification = new SpecificationBase<object>(expression);
      Assert.True(ReferenceEquals(specification.Expression, expression));

      Assert.False(ReferenceEquals(new SpecificationBase<object>().Field("DefaultExpression").To<Expression<Func<object, bool>>>(), expression));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="SpecificationBase{T}.Filter(IQueryable{T})"/> method.</para>
    /// </summary>
    [Fact]
    public void Filter_Method()
    {
      Expression<Func<object, bool>> expression = x => x == null;

      Assert.Throws<ArgumentNullException>(() => new SpecificationBase<object>(expression).Filter(null));
      Assert.Equal(1, new SpecificationBase<object>(expression).Filter(new [] { new object(), null }.AsQueryable()).Count());
      Assert.Equal(2, new SpecificationBase<object>().Filter(new[] { new object(), null }.AsQueryable()).Count());
    }
  }
}