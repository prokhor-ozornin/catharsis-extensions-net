namespace Catharsis.Commons
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  /// <summary>
  ///   <para>This attribute is used to specify the list of properties of the target class or structure to be used in equality comparison and hash codes calculation.</para>
  ///   <para>If present, it's used automatically by the following extension methods:</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ObjectExtensions.Equality{T}(T, T, string[])"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.Equality{T}(T, T, Expression{Func{T, object}}[])"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.GetHashCode{T}(T, IEnumerable{string})"/></description></item>
  ///     <item><description><see cref="ObjectExtensions.GetHashCode{T}(T, Expression{Func{T, object}}[])"/></description></item>
  ///   </list>
  /// </summary>
  /// <seealso cref="object.Equals(object)"/>
  /// <seealso cref="object.GetHashCode()"/>
  /// <seealso cref="ObjectExtensions.Equality{T}(T, T, string[])"/>
  /// <seealso cref="ObjectExtensions.Equality{T}(T, T, Expression{Func{T, object}}[])"/>
  /// <seealso cref="ObjectExtensions.GetHashCode{T}(T, IEnumerable{string})"/>
  /// <seealso cref="ObjectExtensions.GetHashCode{T}(T, Expression{Func{T, object}}[])"/>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = true)]
  public sealed class EqualsAndHashCodeAttribute : Attribute
  {
    private readonly string[] properties;

    /// <summary>
    ///   <para>Creates new instance of attribute.</para>
    /// </summary>
    /// <param name="properties">Comma-separated list of properties to be used in equality comparison and hash codes calculation.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="properties"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="properties"/> is <see cref="string.Empty"/> string.</exception>
    public EqualsAndHashCodeAttribute(string properties)
    {
      Assertion.NotEmpty(properties);

      this.properties = properties.Split(',').Select(property => property.Trim()).ToArray();
    }

    /// <summary>
    ///   <para>Set of properties in target <see cref="Type"/>, used by attribute.</para>
    /// </summary>
    public IEnumerable<string> Properties { get { return this.properties; } }
  }
}