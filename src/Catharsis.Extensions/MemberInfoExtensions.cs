using System.Reflection;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="MemberInfo"/>
public static class MemberInfoExtensions
{
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  extension(MemberInfo member)
  {
    /// <summary>
    ///   <para>Determines whether a target type's member represents constructor of a class (a <see cref="ConstructorInfo"/> instance).</para>
    /// </summary>
    /// <value>
    ///   <c>True</c> if specified <paramref name="member"/> represents class constructor, <c>false</c> otherwise.
    /// </value>
    /// <seealso cref="MemberTypes.Constructor"/>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is <see langword="null"/>.</exception>
    public bool IsConstructor => member is not null ? member.MemberType == MemberTypes.Constructor : throw new ArgumentNullException(nameof(member));

    /// <summary>
    ///   <para>Determines whether a target type's member represents a method (a <see cref="MethodInfo"/> instance).</para>
    /// </summary>
    /// <value>
    ///   <c>True</c> if specified <paramref name="member"/> represents a method, <c>false</c> otherwise.
    /// </value>
    /// <seealso cref="MemberTypes.Method"/>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is <see langword="null"/>.</exception>
    public bool IsMethod => member is not null ? member.MemberType == MemberTypes.Method : throw new ArgumentNullException(nameof(member));

    /// <summary>
    ///   <para>Determines whether a target type's member represents a property (a <see cref="PropertyInfo"/> instance).</para>
    /// </summary>
    /// <value>
    ///   <c>True</c> if specified <paramref name="member"/> represents a property, <c>false</c> otherwise.
    /// </value>
    /// <seealso cref="MemberTypes.Property"/>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is <see langword="null"/>.</exception>
    public bool IsProperty => member is not null ? member.MemberType == MemberTypes.Property : throw new ArgumentNullException(nameof(member));

    /// <summary>
    ///   <para>Determines whether a target type's member represents a field (a <see cref="FieldInfo"/> instance).</para>
    /// </summary>
    /// <value>
    ///   <c>True</c> if specified <paramref name="member"/> represents a field, <c>false</c> otherwise.
    /// </value>
    /// <seealso cref="MemberTypes.Field"/>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is <see langword="null"/>.</exception>
    public bool IsField => member is not null ? member.MemberType == MemberTypes.Field : throw new ArgumentNullException(nameof(member));

    /// <summary>
    ///   <para>Determines whether a target type's member represents an event (a <see cref="EventInfo"/> instance).</para>
    /// </summary>
    /// <value>
    ///   <c>True</c> if specified <paramref name="member"/> represents an event, <c>false</c> otherwise.
    /// </value>
    /// <seealso cref="MemberTypes.Event"/>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is <see langword="null"/>.</exception>
    public bool IsEvent => member is not null ? member.MemberType == MemberTypes.Event : throw new ArgumentNullException(nameof(member));

    /// <summary>
    ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified type, that is applied to current type's member.</para>
    ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
    /// </summary>
    /// <typeparam name="T">Type of custom attribute.</typeparam>
    /// <returns>Instance of attribute, whose type equals to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Attribute(MemberInfo, Type)"/>
    public T Attribute<T>() => member is not null ? member.Attributes<T>().FirstOrDefault() : throw new ArgumentNullException(nameof(member));

    /// <summary>
    ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified type, that is applied to current type's member.</para>
    ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
    /// </summary>
    /// <param name="type">Type of custom attribute.</param>
    /// <returns>Instance of attribute, whose type equals to <paramref name="type"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="member"/> or <paramref name="type"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Attribute{T}(MemberInfo)"/>
    public object Attribute(Type type) => member.Attributes(type).FirstOrDefault();

    /// <summary>
    ///   <para>Returns a collection of all custom attributes, identified by specified type, which are applied to current type's member.</para>
    /// </summary>
    /// <typeparam name="T">Type of custom attributes.</typeparam>
    /// <returns>Collection of custom attributes, whose type equals to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="member"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Attributes(MemberInfo, Type)"/>
    public IEnumerable<T> Attributes<T>() => member.Attributes(typeof(T)).Cast<T>();

    /// <summary>
    ///   <para>Returns a collection of all custom attributes, identified by specified type, which are applied to current type's member.</para>
    /// </summary>
    /// <param name="type">Type of custom attributes.</param>
    /// <returns>Collection of custom attributes, whose type equals to <paramref name="type"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="member"/> or <paramref name="type"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Attributes{T}(MemberInfo)"/>
    public IEnumerable<object> Attributes(Type type)
    {
      if (member is null) throw new ArgumentNullException(nameof(member));
      if (type is null) throw new ArgumentNullException(nameof(type));

      return member.GetCustomAttributes(type, true);
    }
  }
}