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
    ///   <para></para>
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="field"/> is a <c>null</c> reference.</exception>
    public static bool IsProtected(this FieldInfo field)
    {
      Assertion.NotNull(field);

      return !field.IsPublic && !field.IsPrivate;
    }
  }
}