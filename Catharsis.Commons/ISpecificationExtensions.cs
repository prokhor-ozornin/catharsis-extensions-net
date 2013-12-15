using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="ISpecification{T}"/>.</para>
  ///   <seealso cref="ISpecification{T}"/>
  /// </summary>
  public static class ISpecificationExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="specification"></param>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="specification"/> or <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static bool Conforms<T>(this ISpecification<T> specification, T subject)
    {
      Assertion.NotNull(specification);
      Assertion.NotNull(subject);

      return specification.Expression.Compile()(subject);
    }
  }
}