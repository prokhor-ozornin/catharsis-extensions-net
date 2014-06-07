using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="MemberInfo"/>.</para>
  /// </summary>
  /// <see cref="MemberInfo"/>
  public static class MemberInfoAttributesExtensions
  {
    /// <summary>
    ///   <para>Returns a value of either <see cref="DescriptionAttribute"/>, <see cref="DisplayAttribute"/> or <see cref="DisplayNameAttribute"/> (whatever is present and found first) for a given class member.</para>
    /// </summary>
    /// <param name="member">Member of the class or <see cref="Type"/> itself.</param>
    /// <returns>Description for a given class <paramref name="member"/>. If <paramref name="member"/> has a <see cref="DescriptionAttribute"/>, its value is returned. If it has a <see cref="DisplayAttribute"/>, its description property is returned. If it has a <see cref="DisplayNameAttribute"/>, its display name property is returned. If there is neither of these attributes on a <paramref name="member"/>, a <c>null</c> is returned.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DescriptionAttribute"/>
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