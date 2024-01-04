using Catharsis.Commons;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="UriBuilderExtensions"/>.</para>
/// </summary>
public sealed class UriBuilderExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="UriBuilderExtensions.Empty(UriBuilder)"/> method.</para>
  /// </summary>
  [Fact]
  public void Empty_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriBuilderExtensions.Empty(null)).ThrowExactly<ArgumentNullException>().WithParameterName("builder");

      Validate(new UriBuilder());
      Validate(new UriBuilder("https://user:password@192.168.0.1/path?query#id"));
    }

    return;

    static void Validate(UriBuilder builder)
    {
      builder.Empty().Should().NotBeNull().And.BeSameAs(builder);
      builder.Fragment.Should().BeEmpty();
      builder.Host.Should().BeEmpty();
      builder.Password.Should().BeEmpty();
      builder.Path.Should().Be("/");
      builder.Port.Should().Be(-1);
      builder.Query.Should().BeEmpty();
      builder.Scheme.Should().BeEmpty();
      builder.UserName.Should().BeEmpty();

      AssertionExtensions.Should(() => builder.Uri).ThrowExactly<UriFormatException>();
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="UriBuilderExtensions.WithQuery(UriBuilder, IReadOnlyDictionary{string, object})"/></description></item>
  ///     <item><description><see cref="UriBuilderExtensions.WithQuery(UriBuilder, (string Name, object Value)[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void WithQuery_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriBuilderExtensions.WithQuery(null, new Dictionary<string, object>())).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => new UriBuilder().WithQuery((IReadOnlyDictionary<string, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("parameters");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => UriBuilderExtensions.WithQuery(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("builder");
      AssertionExtensions.Should(() => new UriBuilder().WithQuery(((string Name, object Value)[]) null)).ThrowExactly<ArgumentNullException>().WithParameterName("parameters");

    }

    throw new NotImplementedException();
  }
}