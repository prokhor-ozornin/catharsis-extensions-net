using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Represents a converter between different source and destination <see cref="Type"/>s.</para>
  /// </summary>
  public sealed class Convert
  {
    private static readonly Convert converter = new Convert();

    /// <summary>
    ///   <para>Current converter instance.</para>
    /// </summary>
    public static Convert To
    {
      get { return converter; }
    }
  }
}