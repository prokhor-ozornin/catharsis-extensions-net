using System.Reflection;
using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="AssemblyExtensions"/>.</para>
/// </summary>
public sealed class AssemblyExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="AssemblyExtensions.Resource(Assembly, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Assembly_Resource_Method()
  {
    AssertionExtensions.Should(() => AssemblyExtensions.Resource(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("assembly");
    AssertionExtensions.Should(() => Assembly.GetExecutingAssembly().Resource(null)).ThrowExactly<ArgumentNullException>().WithParameterName("name");

    //Assembly.GetExecutingAssembly().Resource("invalid").Should().BeNull();
    //Assembly.GetExecutingAssembly().Resource("Catharsis.Commons.Resource.txt").Should().Be("resource");

    // TODO Encoding support

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, Assembly assembly, string name)
    {
    }
  }
}