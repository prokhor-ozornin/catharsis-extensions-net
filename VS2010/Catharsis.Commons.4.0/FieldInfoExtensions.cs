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
    /// <param name="field">Class field to inspect.</param>
    /// <returns><c>true</c> if <paramref name="field"/> is having a <c>protected</c> access level, <c>false</c> otherwise (public/private).</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is a <c>null</c> reference.</exception>
    public static bool IsProtected(this FieldInfo field)
    {
      Assertion.NotNull(field);

      return !field.IsPublic && !field.IsPrivate;
    }
  }
}