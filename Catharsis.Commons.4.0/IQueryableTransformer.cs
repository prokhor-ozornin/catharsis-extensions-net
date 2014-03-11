using System;
using System.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Represents a transformer between <see cref="IQueryable{T}"/> instances of different underlying <see cref="Type"/>s.</para>
  /// </summary>
  /// <typeparam name="FROM">Source type of <see cref="IQueryable{T}"/> object.</typeparam>
  /// <typeparam name="TO">Destination type of <see cref="IQueryable{T}"/> object.</typeparam>
  public interface IQueryableTransformer<FROM, TO>
  {
    /// <summary>
    ///   <para>Transforms one <see cref="IQueryable{T}"/> source to another.</para>
    /// </summary>
    /// <param name="from"><see cref="IQueryable{T}"/> source that is to be transformed.</param>
    /// <returns>Transformed version of <paramref name="from"/> source.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="from"/> is a <c>null</c> reference.</exception>
    IQueryable<TO> Transform(IQueryable<FROM> from);
  }
}