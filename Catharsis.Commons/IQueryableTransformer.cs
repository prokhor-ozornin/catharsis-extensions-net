using System;
using System.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="FROM"></typeparam>
  /// <typeparam name="TO"></typeparam>
  public interface IQueryableTransformer<FROM, TO>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="from"/> is a <c>null</c> reference.</exception>
    IQueryable<TO> Transform(IQueryable<FROM> from);
  }
}