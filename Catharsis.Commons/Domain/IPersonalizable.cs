namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public interface IPersonalizable
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    string NameFirst { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    string NameLast { get; set; }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    string NameMiddle { get; set; }
  }
}