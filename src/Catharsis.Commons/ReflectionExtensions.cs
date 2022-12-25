using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="System.Type"/>
/// <seealso cref="Assembly"/>
/// <seealso cref="MemberInfo"/>
/// <seealso cref="PropertyInfo"/>
/// <seealso cref="MethodInfo"/>
public static class ReflectionExtensions
{
  /// <summary>
  ///   <para>Returns contents of specified embedded text resource of the assembly.</para>
  ///   <para>Returns a <c>null</c> reference if resource with specified name cannot be found.</para>
  /// </summary>
  /// <param name="assembly">Assembly with resource.</param>
  /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
  /// <param name="encoding">Optional encoding to be used when reading resource's data. If not specified, <see cref="Encoding.UTF8"/> encoding is used.</param>
  /// <returns>Text data of assembly manifest's resource.</returns>
  public static async Task<string?> Resource(this Assembly assembly, string name, Encoding? encoding = null)
  {
    var stream = assembly.GetManifestResourceStream(name);

    return stream != null ? await stream.Text(encoding) : null;
  }

  /// <summary>
  ///   <para>Concatenates the invocation list of a current delegate and a second one.</para>
  /// </summary>
  /// <param name="left">Current delegate to combine with the second.</param>
  /// <param name="right">Second delegate to compare with the current.</param>
  /// <returns>New delegate which a combined invocation list from <paramref name="left"/> and <paramref name="right"/> delegates.</returns>
  public static Delegate And(this Delegate left, Delegate right) => System.Delegate.Combine(left, right);

  /// <summary>
  ///   <para>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</para>
  /// </summary>
  /// <param name="left">The delegate from which to remove the invocation list of <paramref name="right"/>.</param>
  /// <param name="right">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="left"/>.</param>
  /// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="left"/> and removing the last occurrence of the invocation list of <paramref name="right"/>, if the invocation list of <paramref name="right"/> is found within the invocation list of <paramref name="left"/>. Returns <paramref name="left"/> if <paramref name="right"/> is <c>null</c> or if the invocation list of <paramref name="right"/> is not found within the invocation list of <paramref name="left"/>. Returns a <c>null</c> reference if the invocation list of <paramref name="right"/> is equal to the invocation list of <paramref name="left"/> or if <paramref name="left"/> is a <c>null</c> reference.</returns>
  public static Delegate? Not(this Delegate left, Delegate? right) => System.Delegate.Remove(left, right);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="type"></param>
  /// <returns></returns>
  public static bool IsArray<T>(this Type type) => type == typeof(T[]);

  /// <summary>
  ///   <para>Determines whether the <paramref name="type"/> can be assigned to type <typeparamref name="T"/>, or, in other words, whether the instance of type <typeparamref name="T"/> are assignable from <paramref name="type"/>.</para>
  /// </summary>
  /// <typeparam name="T">Destination type to which the assignment is made.</typeparam>
  /// <param name="type">Source type for assignment.</param>
  /// <returns><c>true</c> if <paramref name="type"/> can be assigned to <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
  public static bool IsAssignableTo<T>(this Type type) => typeof(T).IsAssignableFrom(type);

  /// <summary>
  ///   <para>Determines whether target <see cref="Type"/> is anonymous one.</para>
  /// </summary>
  /// <param name="type">Subject type.</param>
  /// <returns><c>true</c> if <paramref name="type"/> represents an anonymous <see cref="Type"/>, <c>false</c> otherwise.</returns>
  public static bool IsAnonymous(this Type type) => type.IsClass
        && type.Attribute<CompilerGeneratedAttribute>() != null
        && type.Name.ToLower().Contains("anonymous")
        && type.IsSealed
        && type.BaseType == typeof(object)
        && type.Properties().All(property => property.IsPublic());

  /// <summary>
  ///   <para>Determines whether a <see cref="Type"/> implements specified interface.</para>
  /// </summary>
  /// <param name="type">The type to evaluate.</param>
  /// <param name="interfaceType">Interface that must be implemented by <paramref name="type"/>.</param>
  /// <returns><c>true</c> if <paramref name="type"/> implements interface of type <paramref name="interfaceType"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentException">If <paramref name="interfaceType"/> does not represent interface.</exception>
  public static bool Implements(this Type type, Type interfaceType)
  {
    if (!interfaceType.IsInterface)
    {
      throw new ArgumentException($"Type {interfaceType} does not represent interface");
    }

    return type.GetInterfaces().Contains(interfaceType);
  }

  /// <summary>
  ///   <para>Determines whether a <see cref="Type"/> implements specified interface.</para>
  /// </summary>
  /// <typeparam name="T">Interface that must be implemented by <paramref name="type"/>.</typeparam>
  /// <param name="type">The type to evaluate.</param>
  /// <returns><c>true</c> if <paramref name="type"/> implements interface of type <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentException">If <typeparamref name="T"/> type does not represent interface.</exception>
  public static bool Implements<T>(this Type type) => type.Implements(typeof(T));

  /// <summary>
  ///   <para>Returns enumerator to iterate over the set of specified <see cref="Type"/>'s base types and implemented interfaces.</para>
  /// </summary>
  /// <param name="type">Type, whose ancestors (base types up the inheritance hierarchy) and implemented interfaces are returned.</param>
  /// <returns>Enumerator to iterate through <paramref name="type"/>'s base types and interfaces, which it implements.</returns>
  /// <remarks>The order of the base types and interfaces returned is undetermined.</remarks>
  public static IEnumerable<Type> Implements(this Type type)
  {
    var types = new List<Type>();

    var currentType = type;

    var currentTypeInfo = currentType;

    while (currentTypeInfo.BaseType != null)
    {
      types.Add(currentType);
      currentType = currentTypeInfo.BaseType;
    }

    types.AddAll(type.GetInterfaces());

    return types;
  }

  /// <summary>
  ///   <para>Returns an array of either instance or static properties with either private or public access level for specified <see cref="Type"/>.</para>
  /// </summary>
  /// <param name="type">Type whose properties are to be returned.</param>
  /// <returns>Set of instance/static properties, declared within a <paramref name="type"/>.</returns>
  public static IEnumerable<PropertyInfo> Properties(this Type type) => type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="instance"></param>
  /// <returns></returns>
  public static IEnumerable<(string Name, object? Value)> Properties(this Type type, object instance) => type.Properties().Select(property => (property.Name, property.GetValue(instance)));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  public static object? Instance(this Type type, params object?[]? arguments) => Activator.CreateInstance(type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, arguments, null);

  /// <summary>
  ///   <para>Dynamically creates new instance of specified <see cref="Type"/> by calling its no-arguments constructor, and sets specified properties on a newly created instance.</para>
  /// </summary>
  /// <param name="type">Type whose instance is to be created.</param>
  /// <param name="properties">Object instance whose public properties values are to be set on a newly created instance of <paramref name="type"/>.</param>
  /// <returns>Newly created instance of <paramref name="type"/> with set <paramref name="properties"/>.</returns>
  public static object? Instance(this Type type, object properties) => type.Instance().Properties(properties);

  /// <summary>
  ///   <para>Searches for a named event, declared within a specified <see cref="Type"/>.</para>
  ///   <para>Returns <see cref="EventInfo"/> object, representing either instance or static event with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose event is to be located.</param>
  /// <param name="name">Unique name of event.</param>
  /// <returns><see cref="EventInfo"/> object representing the event of <paramref name="type"/>. If event cannot be found, returns <c>null</c>.</returns>
  public static EventInfo? Event(this Type type, string name) => type.GetEvent(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

  /// <summary>
  ///   <para>Searches for a named field, declared within a specified <see cref="Type"/>.</para>
  ///   <para>Returns <see cref="FieldInfo"/> object, representing either instance or static field with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose field is to be located.</param>
  /// <param name="name">Unique name of field.</param>
  /// <returns><see cref="FieldInfo"/> object representing the field of <paramref name="type"/>. If field cannot be found, returns <c>null</c>.</returns>
  public static FieldInfo? Field(this Type type, string name) => type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

  /// <summary>
  ///   <para>Searches for a named method, declared within a specified <see cref="Type"/>.</para>
  ///   <para>Returns <see cref="MethodInfo"/> object, representing either instance or static method with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose method is to be located.</param>
  /// <param name="name">Unique name of method.</param>
  /// <returns><see cref="MethodInfo"/> object representing the method of <paramref name="type"/>. If method cannot be found, returns <c>null</c>.</returns>
  public static MethodInfo? Method(this Type type, string name) => type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

  /// <summary>
  ///   <para>Searches for a named property, declared within a specified <see cref="Type"/>.</para>
  ///   <para>Returns <see cref="PropertyInfo"/> object, representing either instance or static property with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose property is to be located.</param>
  /// <param name="name">Unique name of property.</param>
  /// <returns><see cref="PropertyInfo"/> object representing the property of <paramref name="type"/>. If property cannot be found, returns <c>null</c>.</returns>
  public static PropertyInfo? Property(this Type type, string name) => type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

  /// <summary>
  ///   <para>Searches for a default no-arguments constructor, declared within a specified <see cref="Type"/>.</para>
  ///   <para>Returns <see cref="ConstructorInfo"/> object, representing no-arguments constructor.</para>
  /// </summary>
  /// <param name="type">Type whose constructor is to be located.</param>
  /// <param name="types"></param>
  /// <returns><see cref="ConstructorInfo"/> object representing no-arguments constructor of <paramref name="type"/>. If constructor cannot be found, returns <c>null</c>.</returns>
  public static ConstructorInfo? Constructor(this Type type, params Type[] types) => type.GetConstructor(types);

  /// <summary>
  ///   <para>Determines whether there is a named field, declared within a specified <see cref="Type"/>.</para>
  /// </summary>
  /// <param name="type">Type whose field is to be located.</param>
  /// <param name="name">Unique name of field.</param>
  /// <returns><c>true</c> if either instance or static field with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  public static bool HasField(this Type type, string name) => type.Field(name) != null;

  /// <summary>
  ///   <para>Determines whether there is a named method, declared within a specified <see cref="Type"/>.</para>
  /// </summary>
  /// <param name="type">Type whose method is to be located.</param>
  /// <param name="name">Unique name of method.</param>
  /// <returns><c>true</c> if either instance or static method with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  public static bool HasMethod(this Type type, string name) => type.Method(name) != null;

  /// <summary>
  ///   <para>Determines whether there is a named property, declared within a specified <see cref="Type"/>.</para>
  /// </summary>
  /// <param name="type">Type whose property is to be located.</param>
  /// <param name="name">Unique name of property.</param>
  /// <returns><c>true</c> if either instance or static property with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  public static bool HasProperty(this Type type, string name) => type.Property(name) != null;

  /// <summary>
  ///   <para>Returns the <see cref="Type"/> of the target <see cref="Type"/>'s property/field member.</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class that represents either a field (<see cref="FieldInfo"/> instance) or property (<see cref="PropertyInfo"/> instance).</param>
  /// <returns>Type of the field/property, represented by specified <paramref name="member"/> instance.</returns>
  public static Type? Type(this MemberInfo member) => member.MemberType switch
    {
      MemberTypes.Event => member.To<EventInfo>().EventHandlerType,
      MemberTypes.Field => member.To<FieldInfo>().FieldType,
      MemberTypes.Method => member.To<MethodInfo>().ReturnType,
      MemberTypes.Property => member.To<PropertyInfo>().PropertyType,
      _ => member.DeclaringType
    };

  /// <summary>
  ///   <para>Determines whether a targer <see cref="Type"/>'s member represents constructor of a class (a <see cref="ConstructorInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents class constructor, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Constructor"/>
  public static bool IsConstructor(this MemberInfo member) => member.MemberType == MemberTypes.Constructor;

  /// <summary>
  ///   <para>Determines whether a target <see cref="Type"/>'s member represents an event (a <see cref="EventInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents an event, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Event"/>
  public static bool IsEvent(this MemberInfo member) => member.MemberType == MemberTypes.Event;

  /// <summary>
  ///   <para>Determines whether a target <see cref="Type"/>'s member represents a field (a <see cref="FieldInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents a field, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Field"/>
  public static bool IsField(this MemberInfo member) => member.MemberType == MemberTypes.Field;

  /// <summary>
  ///   <para>Determines whether a target <see cref="Type"/>'s member represents a method (a <see cref="MethodInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents a method, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Method"/>
  public static bool IsMethod(this MemberInfo member) => member.MemberType == MemberTypes.Method;

  /// <summary>
  ///   <para>Determines whether a target <see cref="Type"/>'s member represents a property (a <see cref="PropertyInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents a property, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Property"/>
  public static bool IsProperty(this MemberInfo member) => member.MemberType == MemberTypes.Property;

  /// <summary>
  ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified <see cref="Type"/>, that is applied to current <see cref="Type"/>'s member.</para>
  ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
  /// </summary>
  /// <param name="member">Member of <see cref="Type"/>, whose attribute is to be returned.</param>
  /// <param name="attributeType">Type of custom attribute.</param>
  /// <returns>Instance of attribute, whose type equals to <paramref name="attributeType"/>.</returns>
  public static object? Attribute(this MemberInfo member, Type attributeType) => member.Attributes(attributeType).FirstOrDefault();

  /// <summary>
  ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified <see cref="Type"/>, that is applied to current <see cref="Type"/>'s member.</para>
  ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
  /// </summary>
  /// <typeparam name="T">Type of custom attribute.</typeparam>
  /// <param name="member">Member of <see cref="Type"/>, whose attribute is to be returned.</param>
  /// <returns>Instance of attribute, whose type equals to <typeparamref name="T"/>.</returns>
  public static T? Attribute<T>(this MemberInfo member) => member.Attributes<T>().FirstOrDefault();

  /// <summary>
  ///   <para>Returns a collection of all custom attributes, identified by specified <see cref="Type"/>, which are applied to current <see cref="Type"/>'s member.</para>
  /// </summary>
  /// <param name="member">Member of <see cref="Type"/>, whose attributes are to be returned.</param>
  /// <param name="attribute">Type of custom attributes.</param>
  /// <returns>Collection of custom attributes, whose type equals to <paramref name="attribute"/>.</returns>
  public static IEnumerable<object> Attributes(this MemberInfo member, Type attribute) => member.GetCustomAttributes(attribute, true);

  /// <summary>
  ///   <para>Returns a collection of all custom attributes, identified by specified <see cref="Type"/>, which are applied to current <see cref="Type"/>'s member.</para>
  /// </summary>
  /// <typeparam name="T">Type of custom attributes.</typeparam>
  /// <param name="member">Member of <see cref="Type"/>, whose attributes are to be returned.</param>
  /// <returns>Collection of custom attributes, whose type equals to <typeparamref name="T"/>.</returns>
  public static IEnumerable<T> Attributes<T>(this MemberInfo member) => member.Attributes(typeof(T)).Cast<T>();

  /// <summary>
  ///   <para>Determines whether specified class property has a <c>public</c> access level.</para>
  /// </summary>
  /// <param name="property">Class property to inspect.</param>
  /// <returns><c>true</c> if <paramref name="property"/> is having a <c>public</c> access level, <c>false</c> otherwise (protected/private).</returns>
  public static bool IsPublic(this PropertyInfo property)
  {
    if (property.CanRead)
    {
      return property.GetGetMethod() != null;
    }

    if (property.CanWrite)
    {
      return property.GetSetMethod() != null;
    }

    return false;
  }
  
  /// <summary>
  ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
  /// </summary>
  /// <param name="method">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
  /// <param name="type">The <see cref="Type"/> of delegate to create.</param>
  /// <returns>A delegate of the specified type to represent the specified static method.</returns>
  public static Delegate Delegate(this MethodInfo method, Type type) => System.Delegate.CreateDelegate(type, method);

  /// <summary>
  ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
  /// </summary>
  /// <typeparam name="T">The <see cref="Type"/> of delegate to create.</typeparam>
  /// <param name="method">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
  /// <returns>A delegate of the specified type to represent the specified static method.</returns>
  public static Delegate Delegate<T>(this MethodInfo method) => method.Delegate(typeof(T));
}