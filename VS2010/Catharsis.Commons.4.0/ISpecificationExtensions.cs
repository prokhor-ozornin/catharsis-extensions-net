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
    /// <param name="specification">Current instance of specification.</param>
    /// <param name="subject">Target object that is being evaluated for conforming with <paramref name="specification"/>.</param>
    /// <returns><c>true</c> if <paramref name="subject"/> conforms with <paramref name="specification"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="specification"/> or <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static bool Conforms<T>(this ISpecification<T> specification, T subject)
    {
      Assertion.NotNull(specification);
      Assertion.NotNull(subject);

      return specification.Expression.Compile()(subject);
    }
  }
}