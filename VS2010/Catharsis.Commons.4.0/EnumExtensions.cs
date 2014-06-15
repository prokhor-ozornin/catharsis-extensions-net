using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for <see cref="System.Enum"/> structure.</para>
  /// </summary>
  /// <seealso cref="System.Enum"/>
  public static class EnumExtensions
  {
    /// <summary>
    ///   <para>Returns a value of <see cref="DescriptionAttribute"/> for a given enumeration element.</para>
    /// </summary>
    /// <param name="enumeration">Enumeration option/element with a <see cref="DescriptionAttribute"/> on it.</param>
    /// <returns>Description for a given <paramref name="enumeration"/>, which is a value of <see cref="DescriptionAttribute"/>. If there is no attribute on that enumeration member, a <c>null</c> is returned.</returns>
    /// <seealso cref="DescriptionAttribute"/>
    /// <seealso cref="Descriptions{T}()"/>
    public static string Description(this Enum enumeration)
    {
      var attribute = enumeration.GetType().GetField(enumeration.ToString()).Attribute<DescriptionAttribute>();
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