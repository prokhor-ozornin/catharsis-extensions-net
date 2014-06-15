using System;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="PropertyInfo"/>.</para>
  /// </summary>
  /// <seealso cref="PropertyInfo"/>
  public static class PropertyInfoExtensions
  {
    /// <summary>
    ///   <para>Determines whether specified class property has a <c>public</c> access level.</para>
    /// </summary>
    /// <param name="property">Class property to inspect.</param>
    /// <returns><c>true</c> if <paramref name="property"/> is having a <c>public</c> access level, <c>false</c> otherwise (protected/private).</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="property"/> is a <c>null</c> reference.</exception>
    public static bool IsPublic(this PropertyInfo property)
    {
      Assertion.NotNull(property);

      if (property.CanRead)
      {
        return property.GetGetMethod() != null;
      }

      if (property.CanWrite)
      {
        return property.GetSetMethod() != null;
      }

      return false;
    }
  }
}