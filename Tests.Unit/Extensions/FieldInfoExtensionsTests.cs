using System;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="FieldInfoExtensions"/>.</para>
  /// </summary>
  public sealed class FieldInfoExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="FieldInfoExtensions.IsProtected(FieldInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsProtected_Method()
    {
      Assert.Throws<ArgumentNullException>(() => FieldInfoExtensions.IsProtected(null));
 
      Assert.True(typeof(TestObject).GetField("ProtectedField", BindingFlags.Instance | BindingFlags.NonPublic).IsProtected());
    }
  }
}