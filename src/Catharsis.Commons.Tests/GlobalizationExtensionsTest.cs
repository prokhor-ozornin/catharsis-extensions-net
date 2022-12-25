using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="GlobalizationExtensions"/>.</para>
/// </summary>
public sealed class GlobalizationExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="GlobalizationExtensions.ReadOnly(CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void CultureInfo_ReadOnly_Method()
  {
    AssertionExtensions.Should(() => GlobalizationExtensions.ReadOnly(null!)).ThrowExactly<ArgumentNullException>();

    foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
    {
      var readOnly = culture.ReadOnly();

      readOnly.Should().NotBeNull().And.BeSameAs(culture).And.BeSameAs(culture.ReadOnly());

      readOnly.LCID.Should().Be(culture.LCID);
      readOnly.IsReadOnly.Should().BeTrue();
      readOnly.Calendar.IsReadOnly.Should().BeTrue();
      readOnly.DateTimeFormat.IsReadOnly.Should().BeTrue();
      readOnly.NumberFormat.IsReadOnly.Should().BeTrue();
      readOnly.TextInfo.IsReadOnly.Should().BeTrue();
    }
  }
}