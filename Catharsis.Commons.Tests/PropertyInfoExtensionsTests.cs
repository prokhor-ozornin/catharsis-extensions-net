using System;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="PropertyInfoExtensions"/>.</para>
  /// </summary>
  public sealed class PropertyInfoExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="PropertyInfoExtensions.Attribute(PropertyInfo, Type)"/></description></item>
    ///     <item><description><see cref="PropertyInfoExtensions.Attribute{T}(PropertyInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Attribute_Methods()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="PropertyInfoExtensions.IsPublic(PropertyInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void IsPublic_Method()
    {
      Assert.Throws<ArgumentNullException>(() => PropertyInfoExtensions.IsPublic(null));

      var type = typeof(TestObject);
      Assert.True(type.GetProperty("PublicProperty", BindingFlags.Public | BindingFlags.Instance).IsPublic());
      Assert.False(type.GetProperty("ProtectedProperty", BindingFlags.NonPublic | BindingFlags.Instance).IsPublic());
      Assert.False(type.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance).IsPublic());
    }
  }
}