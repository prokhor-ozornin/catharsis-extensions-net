using System;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public interface ITimeable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    DateTime DateCreated { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    DateTime LastUpdated { get; set; }
  }
}