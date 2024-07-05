using System.Reflection;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="Assembly"/>
public static class AssemblyExtensions
{
  /// <summary>
  ///   <para>Returns contents of specified embedded text resource of the assembly.</para>
  ///   <para>Returns a <c>null</c> reference if resource with specified name cannot be found.</para>
  /// </summary>
  /// <param name="assembly">Assembly with resource.</param>
  /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
  /// <returns>Text data of assembly manifest's resource.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="assembly"/> or <paramref name="name"/> is <see langword="null"/>.</exception>
  public static Stream Resource(this Assembly assembly, string name)
  {
    if (assembly is null) throw new ArgumentNullException(nameof(assembly));
    if (name is null) throw new ArgumentNullException(nameof(name));

    return assembly.GetManifestResourceStream(name);
  }
}