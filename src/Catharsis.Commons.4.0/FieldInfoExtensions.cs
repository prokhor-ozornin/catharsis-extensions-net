using System;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="FieldInfo"/>.</para>
  /// </summary>
  /// <seealso cref="FieldInfo"/>
  public static class FieldInfoExtensions
  {
    /// <summary>
    ///   <para>Determines whether specified class field has a <c>protected</c> access level.</para>
    /// </summary>
    /// <param name="self">Class field to inspect.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is having a <c>protected</c> access level, <c>false</c> otherwise (public/private).</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static bool IsProtected(this FieldInfo self)
    {
      Assertion.NotNull(self);

      return !self.IsPublic && !self.IsPrivate;
    }
  }
}