using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="ISpecification{T}"/>.</para>
  /// </summary>
  /// <seealso cref="ISpecification{T}"/>
  public static class ISpecificationExtensions
  {
    /// <summary>
    ///   <para>Determines whether given object conforms with a specification (specification's rule predicate evaluates to <c>true</c>).</para>
    /// </summary>
    /// <typeparam name="T">Type of specification's elements.</typeparam>
    /// <param name="self">Current instance of specification.</param>
    /// <param name="subject">Target object that is being evaluated for conforming with <paramref name="self"/>.</param>
    /// <returns><c>true</c> if <paramref name="subject"/> conforms with <paramref name="self"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static bool Conforms<T>(this ISpecification<T> self, T subject)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(subject);

      return self.Expression.Compile()(subject);
    }
  }
}