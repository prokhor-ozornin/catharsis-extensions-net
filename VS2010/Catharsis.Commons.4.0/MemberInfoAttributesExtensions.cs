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
    /// <param name="self">Member of the class or <see cref="Type"/> itself.</param>
    /// <returns>Description for a given class <paramref name="self"/>. If <paramref name="self"/> has a <see cref="DescriptionAttribute"/>, its value is returned. If it has a <see cref="DisplayAttribute"/>, its description property is returned. If it has a <see cref="DisplayNameAttribute"/>, its display name property is returned. If there is neither of these attributes on a <paramref name="self"/>, a <c>null</c> is returned.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="DescriptionAttribute"/>
    public static string Description(this MemberInfo self)
    {
      Assertion.NotNull(self);

      var descriptionAttribute = self.Attribute<DescriptionAttribute>();
      if (descriptionAttribute != null)
      {
        return descriptionAttribute.Description;
      }

      var displayAttribute = self.Attribute<DisplayAttribute>();
      if (displayAttribute != null)
      {
        return displayAttribute.Description;
      }

      var displayNameAttribute = self.Attribute<DisplayNameAttribute>();
      if (displayNameAttribute != null)
      {
        return displayNameAttribute.DisplayName;
      }

      return null;
    }
  }
}