using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="Type"/>.</para>
  ///   <seealso cref="Type"/>
  /// </summary>
  public static class TypeExtensions
  {
    /// <summary>
    ///   <para>Determines whether the <paramref name="type"/> can be assigned to type <typeparamref name="T"/>, or, in other words, whether the instance of type <typeparamref name="T"/> are assignable from <paramref name="type"/>.</para>
    ///   <seealso cref="Type.IsAssignableFrom(Type)"/>
    /// </summary>
    /// <typeparam name="T">Destination type to which the assignment is made.</typeparam>
    /// <param name="type">Source type for assignment.</param>
    /// <returns><c>true</c> if <paramref name="type"/> can be assigned to <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static bool IsAssignableTo<T>(this Type type)
    {
      Assertion.NotNull(type);

      return typeof(T).IsAssignableFrom(type);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static PropertyInfo[] GetAllProperties(this Type type)
    {
      Assertion.NotNull(type);

      return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static EventInfo GetAnyEvent(this Type type, string name)
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
    public static FieldInfo GetAnyField(this Type type, string name)
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
    public static MethodInfo GetAnyMethod(this Type type, string name)
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
    public static PropertyInfo GetAnyProperty(this Type type, string name)
    {
      Assertion.NotNull(type);
      Assertion.NotEmpty(name);

      return type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static object GetAttribute(this Type type, Type attributeType)
    {
      Assertion.NotNull(type);
      Assertion.NotNull(attributeType);

      return type.GetCustomAttributes(attributeType, false).FirstOrDefault();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="ATTRIBUTE"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static ATTRIBUTE GetAttribute<ATTRIBUTE>(this Type type)
    {
      Assertion.NotNull(type);

      return type.GetAttribute(typeof(ATTRIBUTE)).As<ATTRIBUTE>();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    public static object[] GetAttributes(this Type type, Type attributeType)
    {
      Assertion.NotNull(type);
      Assertion.NotNull(attributeType);

      return type.GetCustomAttributes(attributeType, true);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="ATTRIBUTE"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static ATTRIBUTE[] GetAttributes<ATTRIBUTE>(this Type type)
    {
      Assertion.NotNull(type);

      return type.GetAttributes(typeof(ATTRIBUTE)).Cast<ATTRIBUTE>().ToArray();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    public static ConstructorInfo GetDefaultConstructor(this Type type)
    {
      Assertion.NotNull(type);

      return type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
    }

    /// <summary>
    ///   <para>Determines whether instances of <paramref name="type"/> parameter implement interface, specified by <paramref name="interfaceType"/> parameter.</para>
    ///   <seealso cref="Implements{INTERFACE}(Type)"/>
    /// </summary>
    /// <param name="type">The type to evaluate.</param>
    /// <param name="interfaceType">Interface type.</param>
    /// <returns><c>true</c> if <paramref name="type"/> implements interface specified by type <paramref name="interfaceType"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="type"/> or <paramref name="interfaceType"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="interfaceType"/> does not represent interface.</exception>
    public static bool Implements(this Type type, Type interfaceType)
    {
      Assertion.NotNull(type);
      Assertion.NotNull(interfaceType);

      if (!interfaceType.IsInterface)
      {
        throw new ArgumentException("Type {0} does not represent interface".FormatValue(interfaceType), "interfaceType");
      }

      return type.GetInterface(interfaceType.Name) != null;
    }

    /// <summary>
    ///   <para>Returns enumerator to iterate over the set of specified <see cref="Type"/>'s base types and implemented interfaces.</para>
    ///   <seealso cref="Type.BaseType"/>
    ///   <seealso cref="Type.GetInterfaces()"/>
    /// </summary>
    /// <param name="type">Type, whose ancestors (base types up the inheritance hierarchy) and implemented interfaces are returned.</param>
    /// <returns>Enumerator to iterate through <paramref name="type"/>'s base types and interfaces, which it implements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <remarks>The order of the base types and interfaces returned is undetermined.</remarks>
    public static IEnumerable<Type> Inherits(this Type type)
    {
      Assertion.NotNull(type);

      var types = new HashSet<Type>();

      var currentType = type;
      while (currentType.BaseType != null)
      {
        types.Add(currentType);
        currentType = currentType.BaseType;
      }
      types.AddAll(type.GetInterfaces());

      return types;
    }

    /// <summary>
    ///   <para>Determines whether instances of <paramref name="type"/> parameter implement interface, specified by <typeparamref name="INTERFACE"/>.</para>
    ///   <seealso cref="Implements(Type, Type)"/>
    /// </summary>
    /// <typeparam name="INTERFACE">Interface type.</typeparam>
    /// <param name="type">The type to evaluate.</param>
    /// <returns><c>true</c> if <paramref name="type"/> implements interface <typeparamref name="INTERFACE"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <typeparamref name="INTERFACE"/> type does not represent interface.</exception>
    public static bool Implements<INTERFACE>(this Type type)
    {
      Assertion.NotNull(type);

      return type.Implements(typeof(INTERFACE));
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
    public static object NewInstance(this Type type, IDictionary<string, object> properties)
    {
      Assertion.NotNull(type);
      Assertion.NotNull(properties);

      var instance = type.NewInstance();
      instance.SetProperties(properties);
      return instance;
    }
  }
}