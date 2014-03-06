using System;
using System.Collections.Generic;
using System.Reflection;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="Type"/>.</para>
  /// </summary>
  /// <seealso cref="Type"/>
  public static class TypeExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static EventInfo AnyEvent(this Type type, string name)
    {
      Assertion.NotNull(type);
      Assertion.NotEmpty(name);

      return type.GetEvent(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static FieldInfo AnyField(this Type type, string name)
    {
      Assertion.NotNull(type);
      Assertion.NotEmpty(name);

      return type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static MethodInfo AnyMethod(this Type type, string name)
    {
      Assertion.NotNull(type);
      Assertion.NotEmpty(name);

      return type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static PropertyInfo AnyProperty(this Type type, string name)
    {
      Assertion.NotNull(type);
      Assertion.NotEmpty(name);

      return type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static ConstructorInfo DefaultConstructor(this Type type)
    {
      Assertion.NotNull(type);

      return type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasField(this Type type, string name)
    {
      Assertion.NotNull(type);
      Assertion.NotEmpty(name);

      return type.AnyField(name) != null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasMethod(this Type type, string name)
    {
      Assertion.NotNull(type);
      Assertion.NotEmpty(name);

      return type.AnyMethod(name) != null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasProperty(this Type type, string name)
    {
      Assertion.NotNull(type);
      Assertion.NotEmpty(name);

      return type.AnyProperty(name) != null;
    }

    /// <summary>
    ///   <para>Determines whether instances of <paramref name="type"/> parameter implement interface, specified by <paramref name="interfaceType"/> parameter.</para>
    /// </summary>
    /// <param name="type">The type to evaluate.</param>
    /// <param name="interfaceType">Interface type.</param>
    /// <returns><c>true</c> if <paramref name="type"/> implements interface specified by type <paramref name="interfaceType"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="interfaceType"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="interfaceType"/> does not represent interface.</exception>
    /// <seealso cref="Implements{INTERFACE}(Type)"/>
    public static bool Implements(this Type type, Type interfaceType)
    {
      Assertion.NotNull(type);
      Assertion.NotNull(interfaceType);

      if (!interfaceType.IsInterface)
      {
        throw new ArgumentException("Type {0} does not represent interface".FormatSelf(interfaceType), "interfaceType");
      }

      return type.GetInterface(interfaceType.Name, true) != null;
    }

    /// <summary>
    ///   <para>Determines whether instances of <paramref name="type"/> parameter implement interface, specified by <typeparamref name="T"/>.</para>
    /// </summary>
    /// <typeparam name="T">Interface type.</typeparam>
    /// <param name="type">The type to evaluate.</param>
    /// <returns><c>true</c> if <paramref name="type"/> implements interface <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <typeparamref name="T"/> type does not represent interface.</exception>
    /// <seealso cref="Implements(Type, Type)"/>
    public static bool Implements<T>(this Type type)
    {
      Assertion.NotNull(type);

      return type.Implements(typeof(T));
    }

    /// <summary>
    ///   <para>Returns enumerator to iterate over the set of specified <see cref="Type"/>'s base types and implemented interfaces.</para>
    /// </summary>
    /// <param name="type">Type, whose ancestors (base types up the inheritance hierarchy) and implemented interfaces are returned.</param>
    /// <returns>Enumerator to iterate through <paramref name="type"/>'s base types and interfaces, which it implements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <remarks>The order of the base types and interfaces returned is undetermined.</remarks>
    /// <seealso cref="Type.BaseType"/>
    /// <seealso cref="Type.GetInterfaces()"/>
    public static IEnumerable<Type> Inherits(this Type type)
    {
      Assertion.NotNull(type);

      var types = new List<Type>();

      var currentType = type;
      while (currentType.BaseType != null)
      {
        types.Add(currentType);
        currentType = currentType.BaseType;
      }
      types.Add(type.GetInterfaces());

      return types;
    }

    /// <summary>
    ///   <para>Determines whether the <paramref name="type"/> can be assigned to type <typeparamref name="T"/>, or, in other words, whether the instance of type <typeparamref name="T"/> are assignable from <paramref name="type"/>.</para>
    /// </summary>
    /// <typeparam name="T">Destination type to which the assignment is made.</typeparam>
    /// <param name="type">Source type for assignment.</param>
    /// <returns><c>true</c> if <paramref name="type"/> can be assigned to <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Type.IsAssignableFrom(Type)"/>
    public static bool IsAssignableTo<T>(this Type type)
    {
      Assertion.NotNull(type);

      return typeof(T).IsAssignableFrom(type);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static object NewInstance(this Type type, params object[] args)
    {
      Assertion.NotNull(type);

      return Activator.CreateInstance(type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, args, null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public static object NewInstance(this Type type, IEnumerable<KeyValuePair<string, object>> properties)
    {
      Assertion.NotNull(type);
      Assertion.NotNull(properties);

      return type.NewInstance().Properties(properties);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    public static object NewInstance(this Type type, object properties)
    {
      Assertion.NotNull(type);
      Assertion.NotNull(properties);

      return type.NewInstance().Properties(properties);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static PropertyInfo[] Properties(this Type type)
    {
      Assertion.NotNull(type);

      return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }
  }
}