using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

#if NET_40
using System.ComponentModel.DataAnnotations;
#endif

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of reflection-related extensions methods.</para>
  /// </summary>
  public static class ReflectionExtensions
  {
    /// <summary>
    ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified <see cref="Type"/>, that is applied to current <see cref="Type"/>'s member.</para>
    ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
    /// </summary>
    /// <param name="self">Member of <see cref="Type"/>, whose attribute is to be returned.</param>
    /// <param name="attributeType">Type of custom attribute.</param>
    /// <returns>Instance of attribute, whose type equals to <paramref name="attributeType"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.GetCustomAttributes(Type, bool)"/>
    public static object Attribute(this MemberInfo self, Type attributeType)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(attributeType);

      return self.Attributes(attributeType).FirstOrDefault();
    }

    /// <summary>
    ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified <see cref="Type"/>, that is applied to current <see cref="Type"/>'s member.</para>
    ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
    /// </summary>
    /// <typeparam name="T">Type of custom attribute.</typeparam>
    /// <param name="self">Member of <see cref="Type"/>, whose attribute is to be returned.</param>
    /// <returns>Instance of attribute, whose type equals to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.GetCustomAttributes(Type, bool)"/>
    public static T Attribute<T>(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.Attributes<T>().FirstOrDefault();
    }

    /// <summary>
    ///   <para>Returns a collection of all custom attributes, identified by specified <see cref="Type"/>, which are applied to current <see cref="Type"/>'s member.</para>
    /// </summary>
    /// <param name="self">Member of <see cref="Type"/>, whose attributes are to be returned.</param>
    /// <param name="attributeType">Type of custom attributes.</param>
    /// <returns>Collection of custom attributes, whose type equals to <paramref name="attributeType"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="attributeType"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.GetCustomAttributes(Type, bool)"/>
    public static IEnumerable<object> Attributes(this MemberInfo self, Type attributeType)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(attributeType);

      return self.GetCustomAttributes(attributeType, true);
    }

    /// <summary>
    ///   <para>Returns a collection of all custom attributes, identified by specified <see cref="Type"/>, which are applied to current <see cref="Type"/>'s member.</para>
    /// </summary>
    /// <typeparam name="T">Type of custom attributes.</typeparam>
    /// <param name="self">Member of <see cref="Type"/>, whose attributes are to be returned.</param>
    /// <returns>Collection of custom attributes, whose type equals to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.GetCustomAttributes(Type, bool)"/>
    public static IEnumerable<T> Attributes<T>(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.Attributes(typeof(T)).Cast<T>();
    }

    /// <summary>
    ///   <para>Concatenates the invocation list of a current delegate and a second one.</para>
    /// </summary>
    /// <param name="self">Current delegate to combine with the second.</param>
    /// <param name="other">Second delegate to compare with the current.</param>
    /// <returns>New delegate which a combined invocation list from <paramref name="self"/> and <paramref name="other"/> delegates.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="other"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Delegate.Combine(Delegate, Delegate)"/>
    public static Delegate And(this Delegate self, Delegate other)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(other);

      return System.Delegate.Combine(self, other);
    }

    /// <summary>
    ///   <para>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</para>
    /// </summary>
    /// <param name="self">The delegate from which to remove the invocation list of <paramref name="other"/>.</param>
    /// <param name="other">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="self"/>.</param>
    /// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="self"/> and removing the last occurrence of the invocation list of <paramref name="other"/>, if the invocation list of <paramref name="other"/> is found within the invocation list of <paramref name="self"/>. Returns <paramref name="self"/> if <paramref name="other"/> is <c>null</c> or if the invocation list of <paramref name="other"/> is not found within the invocation list of <paramref name="self"/>. Returns a <c>null</c> reference if the invocation list of <paramref name="other"/> is equal to the invocation list of <paramref name="self"/> or if <paramref name="self"/> is a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Delegate.Remove(Delegate, Delegate)"/>
    public static Delegate Not(this Delegate self, Delegate other)
    {
      Assertion.NotNull(self);

      return System.Delegate.Remove(self, other);
    }

    /// <summary>
    ///   <para>Determines whether specified class field has a <c>protected</c> access level.</para>
    /// </summary>
    /// <param name="self">Class field to inspect.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is having a <c>protected</c> access level, <c>false</c> otherwise (public/private).</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static bool IsProtected(this FieldInfo self)
    {
      Assertion.NotNull(self);

      return !self.IsPublic && !self.IsPrivate;
    }

    /// <summary>
    ///   <para>Determines whether specified class property has a <c>public</c> access level.</para>
    /// </summary>
    /// <param name="self">Class property to inspect.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is having a <c>public</c> access level, <c>false</c> otherwise (protected/private).</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    public static bool IsPublic(this PropertyInfo self)
    {
      Assertion.NotNull(self);

      if (self.CanRead)
      {
        return self.GetGetMethod() != null;
      }

      if (self.CanWrite)
      {
        return self.GetSetMethod() != null;
      }

      return false;
    }

    /// <summary>
    ///   <para>Returns contents of specified embedded text resource of the assembly.</para>
    ///   <para>Returns a <c>null</c> reference if resource with specified name cannot be found.</para>
    /// </summary>
    /// <param name="self">Assembly with resource.</param>
    /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
    /// <param name="encoding">Optional encoding to be used when reading resource's data. If not specified, <see cref="Encoding.UTF8"/> encoding is used.</param>
    /// <returns>Text data of assembly manifest's resource.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="name"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="name"/> is <see cref="string.Empty"/> string.</exception>
    public static string Resource(this Assembly self, string name, Encoding encoding = null)
    {
      Assertion.NotNull(self);
      Assertion.NotEmpty(name);

      return self.GetManifestResourceStream(name)?.Text(true, encoding);
    }

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

      return self.GetConstructor(new Type[] { });
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

#if NET_35
    /// <summary>
    ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
    /// </summary>
    /// <param name="self">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
    /// <param name="type">The <see cref="Type"/> of delegate to create.</param>
    /// <returns>A delegate of the specified type to represent the specified static method.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="type"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="System.Delegate.CreateDelegate(Type, MethodInfo)"/>
    /// <seealso cref="Delegate{T}(MethodInfo)"/>
    public static Delegate Delegate(this MethodInfo self, Type type)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(type);

      return System.Delegate.CreateDelegate(type, self);
    }

    /// <summary>
    ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of delegate to create.</typeparam>
    /// <param name="self">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
    /// <returns>A delegate of the specified type to represent the specified static method.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="System.Delegate.CreateDelegate(Type, MethodInfo)"/>
    /// <seealso cref="Delegate(MethodInfo, Type)"/>
    public static Delegate Delegate<T>(this MethodInfo self)
    {
      Assertion.NotNull(self);

      return self.Delegate(typeof(T));
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
      return self.GetType().GetField(self.ToString()).Attribute<DescriptionAttribute>()?.Description;
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
    ///   <para>Determines whether a targer <see cref="Type"/>'s member represents constructor of a class (a <see cref="ConstructorInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents class constructor, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Constructor"/>
    public static bool IsConstructor(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Constructor;
    }

    /// <summary>
    ///   <para>Determines whether a targer <see cref="Type"/>'s member represents an event (a <see cref="EventInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents an event, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Event"/>
    public static bool IsEvent(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Event;
    }

    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a field (a <see cref="FieldInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents a field, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Field"/>
    public static bool IsField(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Field;
    }

    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a method (a <see cref="MethodInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents a method, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Method"/>
    public static bool IsMethod(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Method;
    }

    /// <summary>
    ///   <para>Determines whether a target <see cref="Type"/>'s member represents a property (a <see cref="PropertyInfo"/> instance).</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
    /// <returns><c>True</c> if specified <paramref name="self"/> represents a property, <c>false</c> otherwise.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberTypes.Property"/>
    public static bool IsProperty(this MemberInfo self)
    {
      Assertion.NotNull(self);

      return self.MemberType == MemberTypes.Property;
    }

        /// <summary>
    ///   <para>Returns the <see cref="Type"/> of the target <see cref="Type"/>'s property/field member.</para>
    /// </summary>
    /// <param name="self">Instance of extended <see cref="MemberInfo"/> class that represents either a field (<see cref="FieldInfo"/> instance) or property (<see cref="PropertyInfo"/> instance).</param>
    /// <returns>Type of the field/property, represented by specified <paramref name="self"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="MemberInfo.DeclaringType"/>
    public static Type Type(this MemberInfo self)
    {
      Assertion.NotNull(self);

      switch (self.MemberType)
      {
        case MemberTypes.Event:
          return self.To<EventInfo>().EventHandlerType;

        case MemberTypes.Field:
          return self.To<FieldInfo>().FieldType;

        case MemberTypes.Method:
          return self.To<MethodInfo>().ReturnType;

        case MemberTypes.Property:
          return self.To<PropertyInfo>().PropertyType;

        default:
          return self.DeclaringType;
      }
    }
#endif

#if NET_40
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
      return displayNameAttribute?.DisplayName;
    }
#endif
  }
}