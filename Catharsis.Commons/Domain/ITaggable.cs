using System.Collections.Generic;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public interface ITaggable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    ICollection<string> Tags { get; }
  }
}