using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public static class EnumExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="enumeration"></param>
    /// <returns></returns>
    public static string Description(this Enum enumeration)
    {
      var attribute = enumeration.GetType().GetField(enumeration.ToString()).Attribute<DescriptionAttribute>();
      return attribute != null ? attribute.Description : null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
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