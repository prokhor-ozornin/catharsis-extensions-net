using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="FieldInfoExtensions"/>.</para>
/// </summary>
public sealed class FieldInfoExtensionsTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="FieldInfoExtensions.IsOfType{T}(FieldInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsOfType_Method()
  {
    AssertionExtensions.Should(() => FieldInfoExtensions.IsOfType<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("field");

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="FieldInfoExtensions.IsProtected(FieldInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsProtected_Method()
  {
    AssertionExtensions.Should(() => FieldInfoExtensions.IsProtected(null)).ThrowExactly<ArgumentNullException>().WithParameterName("field");

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="FieldInfoExtensions.IsInternal(FieldInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsInternal_Method()
  {
    AssertionExtensions.Should(() => FieldInfoExtensions.IsInternal(null)).ThrowExactly<ArgumentNullException>().WithParameterName("field");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="FieldInfoExtensions.IsProtectedInternal(FieldInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsProtectedInternal_Method()
  {
    AssertionExtensions.Should(() => FieldInfoExtensions.IsProtectedInternal(null)).ThrowExactly<ArgumentNullException>().WithParameterName("field");

    throw new NotImplementedException();
  }
}