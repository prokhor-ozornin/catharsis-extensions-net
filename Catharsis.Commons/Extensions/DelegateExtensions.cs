using System;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Delegate"/>.</para>
  ///   <seealso cref="Delegate"/>
  /// </summary>
  public static class DelegateExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is a <c>null</c> reference.</exception>
    public static Delegate And(this Delegate left, Delegate right)
    {
      Assertion.NotNull(left);
      Assertion.NotNull(right);

      return Delegate.Combine(left, right);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="left"/> is a <c>null</c> reference.</exception>
    public static Delegate Not(this Delegate left, Delegate right)
    {
      Assertion.NotNull(left);

      return Delegate.Remove(left, right);
    }
  }
}