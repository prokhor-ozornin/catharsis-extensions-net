using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Delegate"/>.</para>
  /// </summary>
  /// <seealso cref="Delegate"/>
  public static class DelegateExtensions
  {
    /// <summary>
    ///   <para>Concatenates the invocation list of a current delegate and a second one.</para>
    /// </summary>
    /// <param name="left">Current delegate to combine with the second.</param>
    /// <param name="right">Second delegate to compare with the current.</param>
    /// <returns>New delegate which a combined invocation list from <paramref name="left"/> and <paramref name="right"/> delegates.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Delegate.Combine(Delegate, Delegate)"/>
    public static Delegate And(this Delegate left, Delegate right)
    {
      Assertion.NotNull(left);
      Assertion.NotNull(right);

      return Delegate.Combine(left, right);
    }

    /// <summary>
    ///   <para>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</para>
    /// </summary>
    /// <param name="source">The delegate from which to remove the invocation list of <paramref name="value"/>.</param>
    /// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="source"/>.</param>
    /// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="source"/> and removing the last occurrence of the invocation list of <paramref name="value"/>, if the invocation list of <paramref name="value"/> is found within the invocation list of <paramref name="source"/>. Returns <paramref name="source"/> if <paramref name="value"/> is <c>null</c> or if the invocation list of <paramref name="value"/> is not found within the invocation list of <paramref name="source"/>. Returns a <c>null</c> reference if the invocation list of <paramref name="value"/> is equal to the invocation list of <paramref name="source"/> or if <paramref name="source"/> is a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="source"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Delegate.Remove(Delegate, Delegate)"/>
    public static Delegate Not(this Delegate source, Delegate value)
    {
      Assertion.NotNull(source);

      return Delegate.Remove(source, value);
    }
  }
}