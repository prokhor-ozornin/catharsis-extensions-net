using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="MemberInfo"/>.</para>
  ///   <see cref="MemberInfo"/>
  /// </summary>
  public static class MemberInfoAttributesExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    public static string Description(this MemberInfo member)
    {
      Assertion.NotNull(member);

      var descriptionAttribute = member.Attribute<DescriptionAttribute>();
      if (descriptionAttribute != null)
      {
        return descriptionAttribute.Description;
      }

      var displayAttribute = member.Attribute<DisplayAttribute>();
      if (displayAttribute != null)
      {
        return displayAttribute.Description;
      }

      var displayNameAttribute = member.Attribute<DisplayNameAttribute>();
      if (displayNameAttribute != null)
      {
        return displayNameAttribute.DisplayName;
      }

      return null;
    }
  }
}