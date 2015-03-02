using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of reflection-related extensions methods.</para>
  /// </summary>
  public static class ReflectionAttributesExtensions
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

    /// <summary>
    ///   <para>Returns a value of <see cref="DescriptionAttribute"/> for a given enumeration element.</para>
    /// </summary>
    /// <param name="self">Enumeration option/element with a <see cref="DescriptionAttribute"/> on it.</param>
    /// <returns>Description for a given <paramref name="self"/>, which is a value of <see cref="DescriptionAttribute"/>. If there is no attribute on that enumeration member, a <c>null</c> is returned.</returns>
    /// <seealso cref="DescriptionAttribute"/>
    /// <seealso cref="Descriptions{T}()"/>
    public static string Description(this Enum self)
    {
      var attribute = self.GetType().GetField(self.ToString()).Attribute<DescriptionAttribute>();
      return attribute != null ? attribute.Description : null;
    }

    /// <summary>
    ///   <para>Returns a collection of values of <see cref="DescriptionAttribute"/> for a given enumeraton type.</para>
    /// </summary>
    /// <typeparam name="T">Type of enumeration.</typeparam>
    /// <returns>Collection of descriptions for a given enumeration of type <typeparamref name="T"/>.</returns>
    /// <seealso cref="DescriptionAttribute"/>
    /// <seealso cref="Description(Enum)"/>
    public static IEnumerable<string> Descriptions<T>() where T : struct
    {
      Assertion.True(typeof(T).IsEnum);

      var descriptions = new List<string>();
      var type = typeof(T);
      var names = Enum.GetNames(type);
      names.Each(name =>
      {
        var enumeration = Enum.Parse(type, name, true);
        var description = enumeration.To<Enum>().Description();
        descriptions.Add(description.IsEmpty() ? name : description);
      });

      return descriptions;
    }
  }
}