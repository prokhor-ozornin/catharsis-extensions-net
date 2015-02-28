using System;
using System.Reflection;
using System.Text;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Assembly"/>.</para>
  /// </summary>
  public static class AssemblyExtensions
  {
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

      var stream = self.GetManifestResourceStream(name);
      return stream != null ? stream.Text(true, encoding) : null;
    }
  }
}