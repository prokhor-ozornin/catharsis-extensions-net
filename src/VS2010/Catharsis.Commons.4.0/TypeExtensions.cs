using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extensions methods for class <see cref="Type"/>.</para>
  /// </summary>
  /// <seealso cref="Type"/>
  public static class TypeExtensions
  {
    /// <summary>
    ///   <para>Searches for a named event, declared within a specified <see cref="Type"/>.</para>
    ///   <para>Returns <see cref="EventInfo"/> object, representing either instance or static event with either private or public access level.</para>
    /// </summary>
    /// <param name="self">Type whose event is to be located.</param>
    /// <param name="name">Unique name of event.</param>
    /// <returns><see cref="EventInfo"/> object representing the event of <paramref name="self"/>. If event cannot be found, returns <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="Type.GetEvent(string, BindingFlags)"/>
    public static EventInfo AnyEvent(this Type self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.GetEvent(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para>Searches for a named field, declared within a specified <see cref="Type"/>.</para>
    ///   <para>Returns <see cref="FieldInfo"/> object, representing either instance or static field with either private or public access level.</para>
    /// </summary>
    /// <param name="self">Type whose field is to be located.</param>
    /// <param name="name">Unique name of field.</param>
    /// <returns><see cref="FieldInfo"/> object representing the field of <paramref name="self"/>. If field cannot be found, returns <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="Type.GetField(string, BindingFlags)"/>
    public static FieldInfo AnyField(this Type self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para>Searches for a named method, declared within a specified <see cref="Type"/>.</para>
    ///   <para>Returns <see cref="MethodInfo"/> object, representing either instance or static method with either private or public access level.</para>
    /// </summary>
    /// <param name="self">Type whose method is to be located.</param>
    /// <param name="name">Unique name of method.</param>
    /// <returns><see cref="MethodInfo"/> object representing the method of <paramref name="self"/>. If method cannot be found, returns <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="Type.GetMethod(string, BindingFlags)"/>
    public static MethodInfo AnyMethod(this Type self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para>Searches for a named property, declared within a specified <see cref="Type"/>.</para>
    ///   <para>Returns <see cref="PropertyInfo"/> object, representing either instance or static property with either private or public access level.</para>
    /// </summary>
    /// <param name="self">Type whose property is to be located.</param>
    /// <param name="name">Unique name of property.</param>
    /// <returns><see cref="PropertyInfo"/> object representing the property of <paramref name="self"/>. If property cannot be found, returns <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    /// <seealso cref="Type.GetProperty(string, BindingFlags)"/>
    public static PropertyInfo AnyProperty(this Type self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }

    /// <summary>
    ///   <para>Searches for a default no-arguments constructor, declared within a specified <see cref="Type"/>.</para>
    ///   <para>Returns <see cref="ConstructorInfo"/> object, representing no-arguments constructor.</para>
    /// </summary>
    /// <param name="self">Type whose constructor is to be located.</param>
    /// <returns><see cref="ConstructorInfo"/> object representing no-arguments constructor of <paramref name="self"/>. If constructor cannot be found, returns <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Type.GetConstructor(BindingFlags, Binder, Type[], ParameterModifier[])"/>
    public static ConstructorInfo DefaultConstructor(this Type self)
    {
      Assertion.NotNull(self);

      return self.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
    }

    /// <summary>
    ///   <para>Determines whether there is a named field, declared within a specified <see cref="Type"/>.</para>
    /// </summary>
    /// <param name="self">Type whose field is to be located.</param>
    /// <param name="name">Unique name of field.</param>
    /// <returns><c>true</c> if either instance or static field with either private or public access level is declared for <paramref name="self"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasField(this Type self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.AnyField(name) != null;
    }

    /// <summary>
    ///   <para>Determines whether there is a named method, declared within a specified <see cref="Type"/>.</para>
    /// </summary>
    /// <param name="self">Type whose method is to be located.</param>
    /// <param name="name">Unique name of method.</param>
    /// <returns><c>true</c> if either instance or static method with either private or public access level is declared for <paramref name="self"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasMethod(this Type self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.AnyMethod(name) != null;
    }

    /// <summary>
    ///   <para>Determines whether there is a named property, declared within a specified <see cref="Type"/>.</para>
    /// </summary>
    /// <param name="self">Type whose property is to be located.</param>
    /// <param name="name">Unique name of property.</param>
    /// <returns><c>true</c> if either instance or static property with either private or public access level is declared for <paramref name="self"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static bool HasProperty(this Type self, string name)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.AnyProperty(name) != null;
    }

    /// <summary>
    ///   <para>Determines whether a <see cref="Type"/> implements specified interface.</para>
    /// </summary>
    /// <param name="self">The type to evaluate.</param>
    /// <param name="interfaceType">Interface that must be implemented by <paramref name="self"/>.</param>
    /// <returns><c>true</c> if <paramref name="self"/> implements interface of type <paramref name="interfaceType"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="interfaceType"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="interfaceType"/> does not represent interface.</exception>
    /// <seealso cref="Type.GetInterface(string, bool)"/>
    /// <seealso cref="Implements{INTERFACE}(Type)"/>
    public static bool Implements(this Type self, Type interfaceType)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(interfaceType);

      if (!interfaceType.IsInterface)
      {
        throw new ArgumentException("Type {0} does not represent interface".FormatSelf(interfaceType), "interfaceType");
      }

      return self.GetInterface(interfaceType.Name, true) != null;
    }

    /// <summary>
    ///   <para>Determines whether a <see cref="Type"/> implements specified interface.</para>
    /// </summary>
    /// <typeparam name="T">Interface that must be implemented by <paramref name="self"/>.</typeparam>
    /// <param name="self">The type to evaluate.</param>
    /// <returns><c>true</c> if <paramref name="self"/> implements interface of type <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <typeparamref name="T"/> type does not represent interface.</exception>
    /// <seealso cref="Type.GetInterface(string, bool)"/>
    /// <seealso cref="Implements(Type, Type)"/>
    public static bool Implements<T>(this Type self)
    {
      Assertion.NotNull(self);

      return self.Implements(typeof(T));
    }

    /// <summary>
    ///   <para>Returns enumerator to iterate over the set of specified <see cref="Type"/>'s base types and implemented interfaces.</para>
    /// </summary>
    /// <param name="self">Type, whose ancestors (base types up the inheritance hierarchy) and implemented interfaces are returned.</param>
    /// <returns>Enumerator to iterate through <paramref name="self"/>'s base types and interfaces, which it implements.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <remarks>The order of the base types and interfaces returned is undetermined.</remarks>
    /// <seealso cref="Type.BaseType"/>
    /// <seealso cref="Type.GetInterfaces()"/>
    public static IEnumerable<Type> Inherits(this Type self)
    {
      Assertion.NotNull(self);

      var types = new List<Type>();

      var currentType = self;
      while (currentType.BaseType != null)
      {
        types.Add(currentType);
        currentType = currentType.BaseType;
      }
      types.Add(self.GetInterfaces());

      return types;
    }

    /// <summary>
    ///   <para>Determines whether target <see cref="Type"/> is anonymous one.</para>
    /// </summary>
    /// <param name="self">Subject type.</param>
    /// <returns><c>true</c> if <paramref name="self"/> represents an anonymous <see cref="Type"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static bool IsAnonymous(this Type self)
    {
      Assertion.NotNull(self);

      return self.IsClass
             && self.Attribute<CompilerGeneratedAttribute>() != null
             && self.Name.ToLower().Contains("anonymous")
             && self.IsSealed
             && self.BaseType == typeof(object)
             && self.Properties().All(property => property.IsPublic());
    }

    /// <summary>
    ///   <para>Determines whether the <paramref name="self"/> can be assigned to type <typeparamref name="T"/>, or, in other words, whether the instance of type <typeparamref name="T"/> are assignable from <paramref name="self"/>.</para>
    /// </summary>
    /// <typeparam name="T">Destination type to which the assignment is made.</typeparam>
    /// <param name="self">Source type for assignment.</param>
    /// <returns><c>true</c> if <paramref name="self"/> can be assigned to <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Type.IsAssignableFrom(Type)"/>
    public static bool IsAssignableTo<T>(this Type self)
    {
      Assertion.NotNull(self);

      return typeof(T).IsAssignableFrom(self);
    }

    /// <summary>
    ///   <para>Dynamically creates new instance of specified <see cref="Type"/>, passing a set of parameters for its constructor.</para>
    /// </summary>
    /// <param name="self">Type whose instance is to be created.</param>
    /// <param name="args">Optional set of parameters for a constructor of <paramref name="self"/>. Types and number of parameters determine a particular constructor of <paramref name="self"/> to be invoked.</param>
    /// <returns>Newly created instance of <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Activator.CreateInstance(Type, BindingFlags, Binder, object[], CultureInfo)"/>
    /// <seealso cref="NewInstance(Type, IDictionary{string, object})"/>
    /// <seealso cref="NewInstance(Type, object)"/>
    public static object NewInstance(this Type self, params object[] args)
    {
      Assertion.NotNull(self);

      return Activator.CreateInstance(self, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, args, null);
    }

    /// <summary>
    ///   <para>Dynamically creates new instance of specified <see cref="Type"/> by calling its no-arguments constructor, and sets specified properties on a newly created instance.</para>
    /// </summary>
    /// <param name="self">Type whose instance is to be created.</param>
    /// <param name="properties">Collection of properties as a name-value pairs to be set on instance of <paramref name="self"/> after it has been created.</param>
    /// <returns>Newly created instance of <paramref name="self"/> with set <paramref name="properties"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Activator.CreateInstance(Type, BindingFlags, Binder, object[], CultureInfo)"/>
    /// <seealso cref="NewInstance(Type, object[])"/>
    /// <seealso cref="NewInstance(Type, object)"/>
    public static object NewInstance(this Type self, IDictionary<string, object> properties)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(properties);

      return self.NewInstance().Properties(properties);
    }

    /// <summary>
    ///   <para>Dynamically creates new instance of specified <see cref="Type"/> by calling its no-arguments constructor, and sets specified properties on a newly created instance.</para>
    /// </summary>
    /// <param name="self">Type whose instance is to be created.</param>
    /// <param name="properties">Object instance whose public properties values are to be set on a newly created instance of <paramref name="self"/>.</param>
    /// <returns>Newly created instance of <paramref name="self"/> with set <paramref name="properties"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="properties"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Activator.CreateInstance(Type, BindingFlags, Binder, object[], CultureInfo)"/>
    /// <seealso cref="NewInstance(Type, object[])"/>
    /// <seealso cref="NewInstance(Type, IDictionary{string, object})"/>
    public static object NewInstance(this Type self, object properties)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(properties);

      return self.NewInstance().Properties(properties);
    }

    /// <summary>
    ///   <para>Returns an array of either instance or static properties with either private or public access level for specified <see cref="Type"/>.</para>
    /// </summary>
    /// <param name="self">Type whose properties are to be returned.</param>
    /// <returns>Set of instance/static properties, declared within a <paramref name="self"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Type.GetProperties(BindingFlags)"/>
    public static PropertyInfo[] Properties(this Type self)
    {
      Assertion.NotNull(self);

      return self.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    }
  }
}