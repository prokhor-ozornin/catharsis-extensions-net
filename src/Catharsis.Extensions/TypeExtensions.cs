using System.Reflection;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="Type"/>
public static class TypeExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsSealed(this Type type) => type is not null ? type.IsSealed && !type.IsAbstract : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsStatic(this Type type) => type is not null ? type.IsAbstract && type.IsSealed : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="type"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsAssignableFrom<T>(this Type type) => type?.IsAssignableFrom(typeof(T)) ?? throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para>Determines whether the <paramref name="type"/> can be assigned to type <typeparamref name="T"/>, or, in other words, whether the instance of type <typeparamref name="T"/> are assignable from <paramref name="type"/>.</para>
  /// </summary>
  /// <typeparam name="T">Destination type to which the assignment is made.</typeparam>
  /// <param name="type">Source type for assignment.</param>
  /// <returns><c>true</c> if <paramref name="type"/> can be assigned to <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsAssignableTo<T>(this Type type) => type is not null ? typeof(T).IsAssignableFrom(type) : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="type"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsArray<T>(this Type type) => type is not null ? type == typeof(T[]) : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="baseType"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
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
  /// <exception cref="ArgumentNullException"></exception>
  public static bool IsDerivedFrom<T>(this Type type) => type.IsDerivedFrom(typeof(T));

  /// <summary>
  ///   <para>Determines whether a <paramref name="type"/> implements specified interface.</para>
  /// </summary>
  /// <param name="type">The type to evaluate.</param>
  /// <param name="interfaceType">Interface that must be implemented by <paramref name="type"/>.</param>
  /// <returns><c>true</c> if <paramref name="type"/> implements interface of type <paramref name="interfaceType"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentException">If <paramref name="interfaceType"/> does not represent interface.</exception>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool Implements(this Type type, Type interfaceType)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (interfaceType is null) throw new ArgumentNullException(nameof(interfaceType));
    if (!interfaceType.IsInterface) throw new ArgumentException(nameof(interfaceType));

    return type.GetInterfaces().Contains(interfaceType);
  }

  /// <summary>
  ///   <para>Determines whether a <paramref name="type"/> implements specified interface.</para>
  /// </summary>
  /// <typeparam name="T">Interface that must be implemented by <paramref name="type"/>.</typeparam>
  /// <param name="type">The type to evaluate.</param>
  /// <returns><c>true</c> if <paramref name="type"/> implements interface of type <typeparamref name="T"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentException">If <typeparamref name="T"/> type does not represent interface.</exception>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool Implements<T>(this Type type) => type?.Implements(typeof(T)) ?? throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para>Returns enumerator to iterate over the set of specified <paramref name="type"/>'s base types and implemented interfaces.</para>
  /// </summary>
  /// <param name="type">Type, whose ancestors (base types up the inheritance hierarchy) and implemented interfaces are returned.</param>
  /// <returns>Enumerator to iterate through <paramref name="type"/>'s base types and interfaces, which it implements.</returns>
  /// <remarks>The order of the base types and interfaces returned is undetermined.</remarks>
  /// <exception cref="ArgumentNullException"></exception>
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
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="assembly"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<Type> Implementors(this Type type, Assembly assembly = null)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));

    return assembly is not null ? assembly.GetTypes().Where(type.IsAssignableFrom) : AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(type.IsAssignableFrom);
  }

  /// <summary>
  ///   <para>Determines whether there is a named field, declared within a specified <paramref name="type"/>.</para>
  /// </summary>
  /// <param name="type">Type whose field is to be located.</param>
  /// <param name="name">Unique name of field.</param>
  /// <returns><c>true</c> if either instance or static field with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasField(this Type type, string name) => AnyField(type, name) is not null;

  /// <summary>
  ///   <para>Determines whether there is a named property, declared within a specified <paramref name="type"/>.</para>
  /// </summary>
  /// <param name="type">Type whose property is to be located.</param>
  /// <param name="name">Unique name of property.</param>
  /// <returns><c>true</c> if either instance or static property with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasProperty(this Type type, string name) => AnyProperty(type, name) is not null;

  /// <summary>
  ///   <para>Determines whether there is a named method, declared within a specified <paramref name="type"/>.</para>
  /// </summary>
  /// <param name="type">Type whose method is to be located.</param>
  /// <param name="name">Unique name of method.</param>
  /// <param name="arguments"></param>
  /// <returns><c>true</c> if either instance or static method with either private or public access level is declared for <paramref name="type"/>, <c>false</c> otherwise.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasMethod(this Type type, string name, IEnumerable<Type> arguments = null) => AnyMethod(type, name, arguments) is not null;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="name"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasMethod(this Type type, string name, params Type[] arguments) => AnyMethod(type, name, arguments) is not null;

  /// <summary>
  ///   <para>Searches for a named event, declared within a specified <paramref name="type"/>.</para>
  ///   <para>Returns <see cref="EventInfo"/> object, representing either instance or static event with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose event is to be located.</param>
  /// <param name="name">Unique name of event.</param>
  /// <returns><see cref="EventInfo"/> object representing the event of <paramref name="type"/>. If event cannot be found, returns <c>null</c>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static EventInfo AnyEvent(this Type type, string name)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (name is null) throw new ArgumentNullException(nameof(name));

    return type.GetEvent(name, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
  }

  /// <summary>
  ///   <para>Searches for a named field, declared within a specified <paramref name="type"/>.</para>
  ///   <para>Returns <see cref="FieldInfo"/> object, representing either instance or static field with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose field is to be located.</param>
  /// <param name="name">Unique name of field.</param>
  /// <returns><see cref="FieldInfo"/> object representing the field of <paramref name="type"/>. If field cannot be found, returns <c>null</c>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static FieldInfo AnyField(this Type type, string name)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (name is null) throw new ArgumentNullException(nameof(name));

    return type.GetField(name, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
  }

  /// <summary>
  ///   <para>Searches for a named property, declared within a specified <paramref name="type"/>.</para>
  ///   <para>Returns <see cref="PropertyInfo"/> object, representing either instance or static property with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose property is to be located.</param>
  /// <param name="name">Unique name of property.</param>
  /// <returns><see cref="PropertyInfo"/> object representing the property of <paramref name="type"/>. If property cannot be found, returns <c>null</c>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static PropertyInfo AnyProperty(this Type type, string name)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (name is null) throw new ArgumentNullException(nameof(name));

    return type.GetProperty(name, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
  }

  /// <summary>
  ///   <para>Searches for a named method, declared within a specified <paramref name="type"/>.</para>
  ///   <para>Returns <see cref="MethodInfo"/> object, representing either instance or static method with either private or public access level.</para>
  /// </summary>
  /// <param name="type">Type whose method is to be located.</param>
  /// <param name="name">Unique name of method.</param>
  /// <param name="arguments"></param>
  /// <returns><see cref="MethodInfo"/> object representing the method of <paramref name="type"/>. If method cannot be found, returns <c>null</c>.</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static MethodInfo AnyMethod(this Type type, string name, IEnumerable<Type> arguments = null)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));
    if (name is null) throw new ArgumentNullException(nameof(name));

    const BindingFlags flags = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

    return arguments is not null ? type.GetMethod(name, flags, null, arguments.AsArray(), null) : type.GetMethod(name, flags);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="name"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static MethodInfo AnyMethod(this Type type, string name, params Type[] arguments) => type.AnyMethod(name, arguments as IEnumerable<Type>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasConstructor(this Type type, IEnumerable<Type> arguments = null)
  {
    if (type is null) throw new ArgumentNullException(nameof(type));

    return type.GetConstructor(arguments?.AsArray() ?? Array.Empty<Type>()) is not null;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasConstructor(this Type type, params Type[] arguments) => type.HasConstructor(arguments as IEnumerable<Type>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static bool HasDefaultConstructor(this Type type) => type.HasConstructor();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Instance<T>(this Type type, IEnumerable<object> arguments = null) => type is not null ? (T) Activator.CreateInstance(type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, arguments?.AsArray(), null) : throw new ArgumentNullException(nameof(type));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="type"></param>
  /// <param name="arguments"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static T Instance<T>(this Type type, params object[] arguments) => type.Instance<T>(arguments as IEnumerable<object>);
}