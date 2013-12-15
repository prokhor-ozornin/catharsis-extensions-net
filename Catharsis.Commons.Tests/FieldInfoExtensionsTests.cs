using System;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="FieldInfoExtensions"/>.</para>
  /// </summary>
  public sealed class FieldInfoExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="FieldInfoExtensions.Attribute(FieldInfo, Type)"/></description></item>
    ///     <item><description><see cref="FieldInfoExtensions.Attribute{T}(FieldInfo)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Attribute_Methods()
    {
      throw new NotImplementedException();
    }

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