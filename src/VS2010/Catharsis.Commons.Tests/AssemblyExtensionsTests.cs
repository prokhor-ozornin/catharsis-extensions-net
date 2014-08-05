using System;
using System.Reflection;
using System.Text;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="AssemblyExtensions"/>.</para>
  /// </summary>
  public sealed class AssemblyExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="AssemblyExtensions.Resource(Assembly, string, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void Resource_Method()
    {
      Assert.Throws<ArgumentNullException>(() => Assembly.GetExecutingAssembly().Resource(null));
      Assert.Throws<ArgumentException>(() => Assembly.GetExecutingAssembly().Resource(string.Empty));

      Assert.Null(Assembly.GetExecutingAssembly().Resource("invalid"));
      Assert.Equal("resource", Assembly.GetExecutingAssembly().Resource("Catharsis.Commons.Resource.txt"));
    }
  }
}