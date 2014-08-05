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
    /// <param name="self">Class property to inspect.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is having a <c>public</c> access level, <c>false</c> otherwise (protected/private).</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static bool IsPublic(this PropertyInfo self)
    {
      Assertion.NotNull(self);

      if (self.CanRead)
      {
        return self.GetGetMethod() != null;
      }

      if (self.CanWrite)
      {
        return self.GetSetMethod() != null;
      }

      return false;
    }
  }
}