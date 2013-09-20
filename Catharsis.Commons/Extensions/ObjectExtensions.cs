using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="object"/>.</para>
  ///   <seealso cref="object"/>
  /// </summary>
  public static class ObjectExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool And(this object left, object right)
    {
      return True(left) && True(right);
    }

    /// <summary>
    ///   <para>Tries to convert given object to specified type and returns <c>null</c> reference on failure.</para>
    ///   <seealso cref="To{T}"/>
    /// </summary>
    /// <typeparam name="T">Type to convert object to.</typeparam>
    /// <param name="subject">Object to convert.</param>
    /// <returns>Object, converted to the specified type, or <c>null</c> reference, if the conversion cannot be performed.</returns>
    /// <remarks>If specified object instance is a <c>null</c> reference, a <c>null</c> reference will be returned as a result.</remarks>
    public static T As<T>(this object subject)
    {
      return subject is T ? (T) subject : default(T);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static string Dump(this object subject)
    {
      Assertion.NotNull(subject);

      return subject.ToString(subject.GetType().GetProperties().Select(property => property.Name).ToArray());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static bool Equality<T>(this T self, T other, params string[] properties)
    {
      if (self == null && other == null)
      {
        return true;
      }

      if (self == null || other == null)
      {
        return false;
      }

      if (ReferenceEquals(self, other))
      {
        return true;
      }

      if (properties == null || !properties.Any())
      {
        var metaProperties = self.GetType().GetAttributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? self.Equals(other) : self.Equality(other, metaProperties);
      }

      var subjectProperties = properties.Select(property => self.GetType().GetAnyProperty(property)).Where(property => property != null);
      if (!subjectProperties.Any())
      {
        return self.Equals(other);
      }

      return subjectProperties.All(property =>
      {
        var first = property.GetValue(self, null);
        object second = null;
        try
        {
          second = property.GetValue(other, null);
        }
        catch (TargetException)
        {
        }
        
        if (first == null && second == null)
        {
          return true;
        }

        if (first == null || second == null)
        {
          return false;
        }

        return first.Equals(second);
      });
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static bool Equality<T>(this T self, T other, params Expression<Func<T, object>>[] properties)
    {
      if (self == null && other == null)
      {
        return true;
      }

      if (self == null || other == null)
      {
        return false;
      }

      if (ReferenceEquals(self, other))
      {
        return true;
      }

      if (properties == null || properties.Length == 0)
      {
        var metaProperties = self.GetType().GetAttributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? self.Equals(other) : self.Equality(other, metaProperties);
      }

      return properties.Select(property => property.Compile()).All(property => property(self).Equals(property(other)));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public static bool False(this object subject)
    {
      return !True(subject);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="finalize"></param>
    /// <remarks></remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static object Finalize(this object subject, bool finalize = true)
    {
      Assertion.NotNull(subject);

      if (finalize)
      {
        GC.ReRegisterForFinalize(subject);
      }
      else
      {
        GC.SuppressFinalize(subject);
      }

      return subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static int GetHashCode<T>(this T subject, IEnumerable<string> properties)
    {
      if (subject == null)
      {
        return 0;
      }

      if (properties == null || !properties.Any())
      {
        var metaProperties = subject.GetType().GetAttributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? subject.GetHashCode() : subject.GetHashCode(metaProperties);
      }

      return properties.Select(property => subject.GetType().GetAnyProperty(property)).Where(property => property != null).Select(property => property.GetValue(subject, null)).Where(value => value != null).Sum(value => value.GetHashCode());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static int GetHashCode<T>(this T subject, params Expression<Func<T, object>>[] properties)
    {
      if (subject == null)
      {
        return 0;
      }

      if (properties == null || properties.Length == 0)
      {
        var metaProperties = typeof(T).GetAttributes<EqualsAndHashCodeAttribute>().SelectMany(attribute => attribute.Properties).ToArray();
        return metaProperties.Length == 0 ? subject.GetHashCode() : subject.GetHashCode(metaProperties);
      }

      return properties.Select(property => property.Compile()(subject)).Where(value => value != null).Sum(value => value.GetHashCode());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static object GetField(this object subject, string name)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      var subjectField = subject.GetType().GetAnyField(name);
      return subjectField != null ? subjectField.GetValue(subject) : null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static object GetProperty(this object subject, string name)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      var subjectProperty = subject.GetType().GetAnyProperty(name);
      return subjectProperty != null ? subjectProperty.GetValue(subject, null) : null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasField(this object subject, string name)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      return subject.GetType().GetAnyField(name) != null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasMethod(this object subject, string name)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      return subject.GetType().GetAnyMethod(name) != null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasProperty(this object subject, string name)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      return subject.GetType().GetAnyProperty(name) != null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="name"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static object InvokeMethod(this object subject, string name, params object[] parameters)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(name);

      var subjectMethod = subject.GetType().GetAnyMethod(name);
      return subjectMethod != null ? subjectMethod.Invoke(subject, parameters) : null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="MEMBER"></typeparam>
    /// <param name="subject"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="expression"/> is a <c>null</c> reference.</exception>
    public static MEMBER Member<T, MEMBER>(this T subject, Expression<Func<T, MEMBER>> expression)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(expression);

      return expression.Compile()(subject);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public static bool Not(this object subject)
    {
      return False(subject);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool Or(this object left, object right)
    {
      return True(left) || True(right);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="property"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="property"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="property"/> is <see cref="string.Empty"/> string.</exception>
    public static object SetProperty(this object subject, string property, object value)
    {
      Assertion.NotNull(subject);
      Assertion.NotEmpty(property);

      var subjectProperty = subject.GetType().GetAnyProperty(property);
      if (subjectProperty != null)
      {
        subjectProperty.SetValue(subject, value, null);
      }
      return subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public static object SetProperties(this object subject, IDictionary<string, object> properties)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(properties);

      subject.GetType().GetAllProperties().Each(property =>
      {
        if (properties.ContainsKey(property.Name))
        {
          property.SetValue(subject, properties[property.Name], null);
        }
      });

      return subject;
    }

    /// <summary>
    ///   <para>Tries to convert given object to specified type and throws exception on failure.</para>
    ///   <seealso cref="As{T}"/>
    /// </summary>
    /// <typeparam name="T">Type to convert object to.</typeparam>
    /// <param name="subject">Object to convert.</param>
    /// <returns>Object, converted to the specified type.</returns>
    /// <exception cref="InvalidCastException">If conversion to specified type cannot be performed.</exception>
    /// <remarks>If specified object instance is a <c>null</c> reference, a <c>null</c> reference will be returned as a result.</remarks>
    public static T To<T>(this object subject)
    {
      return (T) subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static string ToString<T>(this T subject, IEnumerable<string> properties)
    {
      Assertion.NotNull(subject);

      const string Separator = ", ";
      var sb = new StringBuilder();
      if (properties != null)
      {
        properties.Where(property => subject.HasProperty(property)).Each(property => sb.AppendFormat("{0}:\"{1}\"{2}", property, subject.GetProperty(property), Separator));
      }
      if (sb.Length > 0)
      {
        sb.Remove(sb.Length - Separator.Length, Separator.Length);
      }
      return "[{0}]".FormatValue(sb.ToString());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/>If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    public static string ToString<T>(this T subject, params Expression<Func<T, object>>[] properties)
    {
      Assertion.NotNull(subject);

      const string Separator = ", ";
      var sb = new StringBuilder();
      if (properties != null)
      {
        properties.Each(property => sb.AppendFormat("{0}:\"{1}\"{2}", property.Body.To<UnaryExpression>().Operand.To<MemberExpression>().Member.Name, property.Compile()(subject), Separator));
      }
      if (sb.Length > 0)
      {
        sb.Remove(sb.Length - Separator.Length, Separator.Length);
      }
      return "[{0}]".FormatValue(sb.ToString());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public static bool True(this object subject)
    {
      if (subject == null)
      {
        return false;
      }

      switch (Type.GetTypeCode(subject.GetType()))
      {
        case TypeCode.Boolean :
          return ((bool) subject);

        case TypeCode.Byte :
          return ((byte) subject) > 0;

        case TypeCode.Char :
          return ((char) subject) != Char.MinValue;

        case TypeCode.Decimal :
          return ((decimal) subject) > 0;

        case TypeCode.Double :
          return ((double) subject) > 0;

        case TypeCode.Int16 :
          return ((short) subject) > 0;

        case TypeCode.Int32 :
          return ((int) subject) > 0;

        case TypeCode.Int64 :
          return ((long) subject) > 0;

        case TypeCode.SByte :
          return ((sbyte) subject) > 0;

        case TypeCode.Single :
          return ((Single) subject) > 0;

        case TypeCode.String :
          return ((string) subject).Length > 0;

        case TypeCode.UInt16 :
          return ((ushort) subject) > 0;

        case TypeCode.UInt32 :
          return ((uint) subject) > 0;

        case TypeCode.UInt64 :
          return ((ulong) subject) > 0;
      }

      if (subject is IEnumerable)
      {
        return subject.To<IEnumerable>().Cast<object>().Any();
      }

      if (subject is Match)
      {
        return subject.To<Match>().Success;
      }

      return true;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subject"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static T With<T>(this T subject, Action<T> action)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(action);

      if (subject is IDisposable)
      {
        using (subject as IDisposable)
        {
          action(subject);
        }
      }
      else
      {
        action(subject);
      }

      return subject;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="RESULT"></typeparam>
    /// <param name="subject"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="subject"/> or <paramref name="action"/> is a <c>null</c> reference.</exception>
    public static RESULT With<T, RESULT>(this T subject, Func<T, RESULT> action)
    {
      Assertion.NotNull(subject);
      Assertion.NotNull(action);

      var result = default(RESULT);
      subject.With<T>(x => result = action(x));
      return result;
    }
  }
}