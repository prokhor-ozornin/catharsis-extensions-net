using System.Reflection;

namespace Catharsis.Extensions;

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
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  public static bool IsStatic(this Type type) => type is not null ? type.IsAbstract && type.IsSealed : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="type"></param>
  /// <returns></returns>
  public static bool IsArray<T>(this Type type) => type is not null ? type == typeof(T[]) : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para>Determines whether the <paramref name="type"/> can be assigned to type <typeparamref name="T"/>, or, in other words, whether the instance of type <typeparamref name="T"/> are assignable from <paramref name="type"/>.</para>
  /// </summary>
  /// <typeparam name="T">Destination type to which the assignment is made.</typeparam>
  /// <param name="type">Source type for assignment.</param>
  /// <returns><c>true</c> if <paramref name="type"/> can be assigned to <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
  public static bool IsAssignableTo<T>(this Type type) => type is not null ? typeof(T).IsAssignableFrom(type) : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="baseType"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsDerivedFrom(this Type type, Type baseType)
  {
    static bool IsDerivedFromGeneric(Type type, Type definition)
    {
      if (type == definition)
      {
        return false;
      }

      for (var baseType = type; baseType is not null; baseType = baseType.BaseType)
      {
        if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == definition)
        {
          return true;
        }
      }

      return false;
    }

    if (type is null) throw new ArgumentNullException(nameof(type));
    if (baseType is null) throw new ArgumentNullException(nameof(baseType));

    return baseType.IsGenericTypeDefinition ? IsDerivedFromGeneric(type, baseType) : type.IsSubclassOf(baseType);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="type"></param>
  /// <returns></returns>
  public static bool IsDerivedFrom<T>(this Type type) => type.IsDerivedFrom(typeof(T));

  /// <summary>
  ///   <para>Determines whether a <see cref="ToType"/> implements specified interface.</para>
  /// </summary>
  /// <param name="type">The type to evaluate.</param>
  /// <param name="interfaceType">Interface that must be implemented by <paramref name="type"/>.</param>
  /// <returns><c>true</c> if <paramref name="type"/> implements interface of type <paramref name="interfaceType"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentException">If <paramref name="interfaceType"/> does not represent interface.</exception>
  public static bool Implements(this Type type, Type interfaceType)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (interfaceType is null) throw new ArgumentNullException(nameof(interfaceType));
    if (!interfaceType.IsInterface) throw new ArgumentException(nameof(interfaceType));

    return type.GetInterfaces().Contains(interfaceType);
  }

  /// <summary>
  ///   <para>Determines whether a <see cref="ToType"/> implements specified interface.</para>
  /// </summary>
  /// <typeparam name="T">Interface that must be implemented by <paramref name="type"/>.</typeparam>
  /// <param name="type">The type to evaluate.</param>
  /// <returns><c>true</c> if <paramref name="type"/> implements interface of type <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentException">If <typeparamref name="T"/> type does not represent interface.</exception>
  public static bool Implements<T>(this Type type) => type?.Implements(typeof(T)) ?? throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para>Returns enumerator to iterate over the set of specified <see cref="ToType"/>'s base types and implemented interfaces.</para>
  /// </summary>
  /// <param name="type">Type, whose ancestors (base types up the inheritance hierarchy) and implemented interfaces are returned.</param>
  /// <returns>Enumerator to iterate through <paramref name="type"/>'s base types and interfaces, which it implements.</returns>
  /// <remarks>The order of the base types and interfaces returned is undetermined.</remarks>
  public static IEnumerable<Type> Implementations(this Type type)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));

    var types = new List<Type>();

    var currentType = type;

    var currentTypeInfo = currentType;

    while (currentTypeInfo.BaseType is not null)
    {
      types.Add(currentType);
      currentType = currentTypeInfo.BaseType;
    }

    types.AddRange(type.GetInterfaces());

    return types;
  }

  /// <summary>
  ///   <para>Determines whether there is a named field, declared within a specified <see cref="ToType"/>.</para>
  /// </summary>
  /// <param name="type">Type whose field is to be located.</param>
  /// <param name="name">Unique name of field.</param>
  /// <returns><c>true</c> if either instance or static field with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  public static bool HasField(this Type type, string name) => AnyField(type, name) is not null;

  /// <summary>
  ///   <para>Determines whether there is a named property, declared within a specified <see cref="ToType"/>.</para>
  /// </summary>
  /// <param name="type">Type whose property is to be located.</param>
  /// <param name="name">Unique name of property.</param>
  /// <returns><c>true</c> if either instance or static property with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  public static bool HasProperty(this Type type, string name) => AnyProperty(type, name) is not null;

  /// <summary>
  ///   <para>Determines whether there is a named method, declared within a specified <see cref="ToType"/>.</para>
  /// </summary>
  /// <param name="type">Type whose method is to be located.</param>
  /// <param name="name">Unique name of method.</param>
  /// <param name="arguments"></param>
  /// <returns><c>true</c> if either instance or static method with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  public static bool HasMethod(this Type type, string name, IEnumerable<Type> arguments = null) => AnyMethod(type, name, arguments) is not null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="name"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  public static bool HasMethod(this Type type, string name, params Type[] arguments) => AnyMethod(type, name, arguments) is not null;

  /// <summary>
  ///   <para>Searches for a named event, declared within a specified <see cref="ToType"/>.</para>
  ///   <para>Returns <see cref="EventInfo"/> object, representing either instance or static event with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose event is to be located.</param>
  /// <param name="name">Unique name of event.</param>
  /// <returns><see cref="EventInfo"/> object representing the event of <paramref name="type"/>. If event cannot be found, returns <c>null</c>.</returns>
  public static EventInfo AnyEvent(this Type type, string name)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (name is null) throw new ArgumentNullException(nameof(name));

    return type.GetEvent(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
  }

  /// <summary>
  ///   <para>Searches for a named field, declared within a specified <see cref="ToType"/>.</para>
  ///   <para>Returns <see cref="FieldInfo"/> object, representing either instance or static field with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose field is to be located.</param>
  /// <param name="name">Unique name of field.</param>
  /// <returns><see cref="FieldInfo"/> object representing the field of <paramref name="type"/>. If field cannot be found, returns <c>null</c>.</returns>
  public static FieldInfo AnyField(this Type type, string name)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (name is null) throw new ArgumentNullException(nameof(name));

    return type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
  }

  /// <summary>
  ///   <para>Searches for a named property, declared within a specified <see cref="ToType"/>.</para>
  ///   <para>Returns <see cref="PropertyInfo"/> object, representing either instance or static property with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose property is to be located.</param>
  /// <param name="name">Unique name of property.</param>
  /// <returns><see cref="PropertyInfo"/> object representing the property of <paramref name="type"/>. If property cannot be found, returns <c>null</c>.</returns>
  public static PropertyInfo AnyProperty(this Type type, string name)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (name is null) throw new ArgumentNullException(nameof(name));

    return type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
  }

  /// <summary>
  ///   <para>Searches for a named method, declared within a specified <see cref="ToType"/>.</para>
  ///   <para>Returns <see cref="MethodInfo"/> object, representing either instance or static method with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose method is to be located.</param>
  /// <param name="name">Unique name of method.</param>
  /// <param name="arguments"></param>
  /// <returns><see cref="MethodInfo"/> object representing the method of <paramref name="type"/>. If method cannot be found, returns <c>null</c>.</returns>
  public static MethodInfo AnyMethod(this Type type, string name, IEnumerable<Type> arguments = null)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (name is null) throw new ArgumentNullException(nameof(name));

    const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

    return arguments is not null ? type.GetMethod(name, flags, null, arguments.AsArray(), null) : type.GetMethod(name, flags);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="name"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  public static MethodInfo AnyMethod(this Type type, string name, params Type[] arguments) => type.AnyMethod(name, arguments as IEnumerable<Type>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasConstructor(this Type type, IEnumerable<Type> arguments)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (arguments is null) throw new ArgumentNullException(nameof(arguments));

    return type.GetConstructor(arguments.AsArray()) is not null;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  public static bool HasConstructor(this Type type, params Type[] arguments) => type.HasConstructor(arguments as IEnumerable<Type>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasDefaultConstructor(this Type type) => type.HasConstructor(Array.Empty<Type>());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  public static object Instance(this Type type, IEnumerable<object> arguments = null) => type is not null ? Activator.CreateInstance(type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, arguments?.AsArray(), null) : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  public static object Instance(this Type type, params object[] arguments) => type.Instance(arguments as IEnumerable<object>);

  /// <summary>
  ///   <para>Determines whether a target <see cref="ToType"/>'s member represents an event (a <see cref="EventInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents an event, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Event"/>
  public static bool IsEvent(this MemberInfo member) => member is not null ? member.MemberType == MemberTypes.Event : throw new ArgumentNullException(nameof(member));

  /// <summary>
  ///   <para>Determines whether a target <see cref="ToType"/>'s member represents a field (a <see cref="FieldInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents a field, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Field"/>
  public static bool IsField(this MemberInfo member) => member is not null ? member.MemberType == MemberTypes.Field : throw new ArgumentNullException(nameof(member));

  /// <summary>
  ///   <para>Determines whether a target <see cref="ToType"/>'s member represents a property (a <see cref="PropertyInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents a property, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Property"/>
  public static bool IsProperty(this MemberInfo member) => member is not null ? member.MemberType == MemberTypes.Property : throw new ArgumentNullException(nameof(member));

  /// <summary>
  ///   <para>Determines whether a target <see cref="ToType"/>'s member represents a method (a <see cref="MethodInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents a method, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Method"/>
  public static bool IsMethod(this MemberInfo member) => member is not null ? member.MemberType == MemberTypes.Method : throw new ArgumentNullException(nameof(member));

  /// <summary>
  ///   <para>Determines whether a target <see cref="ToType"/>'s member represents constructor of a class (a <see cref="ConstructorInfo"/> instance).</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class to be evaluated.</param>
  /// <returns><c>True</c> if specified <paramref name="member"/> represents class constructor, <c>false</c> otherwise.</returns>
  /// <seealso cref="MemberTypes.Constructor"/>
  public static bool IsConstructor(this MemberInfo member) => member is not null ? member.MemberType == MemberTypes.Constructor : throw new ArgumentNullException(nameof(member));

  /// <summary>
  ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified <see cref="ToType"/>, that is applied to current <see cref="ToType"/>'s member.</para>
  ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
  /// </summary>
  /// <typeparam name="T">Type of custom attribute.</typeparam>
  /// <param name="member">Member of <see cref="ToType"/>, whose attribute is to be returned.</param>
  /// <returns>Instance of attribute, whose type equals to <typeparamref name="T"/>.</returns>
  public static T Attribute<T>(this MemberInfo member) => member is not null ? member.Attributes<T>().FirstOrDefault() : throw new ArgumentNullException(nameof(member));

  /// <summary>
  ///   <para>Returns a custom <see cref="Attribute"/>, identified by specified <see cref="ToType"/>, that is applied to current <see cref="ToType"/>'s member.</para>
  ///   <para>Returns a <c>null</c> reference if attribute cannot be found.</para>
  /// </summary>
  /// <param name="member">Member of <see cref="ToType"/>, whose attribute is to be returned.</param>
  /// <param name="type">Type of custom attribute.</param>
  /// <returns>Instance of attribute, whose type equals to <paramref name="type"/>.</returns>
  public static object Attribute(this MemberInfo member, Type type) => member.Attributes(type).FirstOrDefault();

  /// <summary>
  ///   <para>Returns a collection of all custom attributes, identified by specified <see cref="ToType"/>, which are applied to current <see cref="ToType"/>'s member.</para>
  /// </summary>
  /// <typeparam name="T">Type of custom attributes.</typeparam>
  /// <param name="member">Member of <see cref="ToType"/>, whose attributes are to be returned.</param>
  /// <returns>Collection of custom attributes, whose type equals to <typeparamref name="T"/>.</returns>
  public static IEnumerable<T> Attributes<T>(this MemberInfo member) => member.Attributes(typeof(T)).Cast<T>();

  /// <summary>
  ///   <para>Returns a collection of all custom attributes, identified by specified <see cref="ToType"/>, which are applied to current <see cref="ToType"/>'s member.</para>
  /// </summary>
  /// <param name="member">Member of <see cref="ToType"/>, whose attributes are to be returned.</param>
  /// <param name="type">Type of custom attributes.</param>
  /// <returns>Collection of custom attributes, whose type equals to <paramref name="type"/>.</returns>
  public static IEnumerable<object> Attributes(this MemberInfo member, Type type)
  {
    if (member is null) throw new ArgumentNullException(nameof(member));
    if (type is null) throw new ArgumentNullException(nameof(type));

    return member.GetCustomAttributes(type, true);
  }

  /// <summary>
  ///   <para>Returns the <see cref="ToType"/> of the target <see cref="ToType"/>'s property/field member.</para>
  /// </summary>
  /// <param name="member">Instance of extended <see cref="MemberInfo"/> class that represents either a field (<see cref="FieldInfo"/> instance) or property (<see cref="PropertyInfo"/> instance).</param>
  /// <returns>Type of the field/property, represented by specified <paramref name="member"/> instance.</returns>
  public static Type ToType(this MemberInfo member) => member is not null ? member.MemberType switch
  {
    MemberTypes.Event => member.To<EventInfo>().EventHandlerType,
    MemberTypes.Field => member.To<FieldInfo>().FieldType,
    MemberTypes.Method => member.To<MethodInfo>().ReturnType,
    MemberTypes.Property => member.To<PropertyInfo>().PropertyType,
    _ => member.DeclaringType
  } : throw new ArgumentNullException(nameof(member));

  /// <summary>
  ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
  /// </summary>
  /// <typeparam name="T">The <see cref="ToType"/> of delegate to create.</typeparam>
  /// <param name="method">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
  /// <returns>A delegate of the specified type to represent the specified static method.</returns>
  public static Delegate ToDelegate<T>(this MethodInfo method) => method.ToDelegate(typeof(T));

  /// <summary>
  ///   <para>Creates a delegate of the specified type to represent a specified static method.</para>
  /// </summary>
  /// <param name="method">The <see cref="MethodInfo"/> describing the static or instance method the delegate is to represent.</param>
  /// <param name="type">The <see cref="ToType"/> of delegate to create.</param>
  /// <returns>A delegate of the specified type to represent the specified static method.</returns>
  public static Delegate ToDelegate(this MethodInfo method, Type type)
  {
    if (method is null) throw new ArgumentNullException(nameof(method));
    if (type is null) throw new ArgumentNullException(nameof(type));

    return Delegate.CreateDelegate(type, method);
  }

  /// <summary>
  ///   <para>Concatenates the invocation list of a current delegate and a second one.</para>
  /// </summary>
  /// <param name="left">Current delegate to combine with the second.</param>
  /// <param name="right">Second delegate to compare with the current.</param>
  /// <returns>New delegate which a combined invocation list from <paramref name="left"/> and <paramref name="right"/> delegates.</returns>
  public static Delegate And(this Delegate left, Delegate right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return Delegate.Combine(left, right);
  }

  /// <summary>
  ///   <para>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</para>
  /// </summary>
  /// <param name="left">The delegate from which to remove the invocation list of <paramref name="right"/>.</param>
  /// <param name="right">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="left"/>.</param>
  /// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="left"/> and removing the last occurrence of the invocation list of <paramref name="right"/>, if the invocation list of <paramref name="right"/> is found within the invocation list of <paramref name="left"/>. Returns <paramref name="left"/> if <paramref name="right"/> is <c>null</c> or if the invocation list of <paramref name="right"/> is not found within the invocation list of <paramref name="left"/>. Returns a <c>null</c> reference if the invocation list of <paramref name="right"/> is equal to the invocation list of <paramref name="left"/> or if <paramref name="left"/> is a <c>null</c> reference.</returns>
  public static Delegate Not(this Delegate left, Delegate right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return Delegate.Remove(left, right);
  }

  /// <summary>
  ///   <para>Returns contents of specified embedded text resource of the assembly.</para>
  ///   <para>Returns a <c>null</c> reference if resource with specified name cannot be found.</para>
  /// </summary>
  /// <param name="assembly">Assembly with resource.</param>
  /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
  /// <returns>Text data of assembly manifest's resource.</returns>
  public static Stream Resource(this Assembly assembly, string name)
  {
    if (assembly is null) throw new ArgumentNullException(nameof(assembly));
    if (name is null) throw new ArgumentNullException(nameof(name));

    return assembly.GetManifestResourceStream(name);
  }
}