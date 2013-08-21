using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public interface ICommentable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    ICollection<Comment> Comments { get; }
  }
}