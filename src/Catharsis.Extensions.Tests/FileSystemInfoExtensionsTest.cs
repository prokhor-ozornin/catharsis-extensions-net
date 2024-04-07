using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="FileSystemInfoExtensions"/>.</para>
/// </summary>
public sealed class FileSystemInfoExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="FileSystemInfoExtensions.ToUri(FileSystemInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToUri_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => FileSystemInfoExtensions.ToUri(null)).ThrowExactly<ArgumentNullException>().WithParameterName("entry");

      Validate(Attributes.RandomFakeFile());
      Validate(Attributes.RandomFakeDirectory());
    }

    return;

    static void Validate(FileSystemInfo info)
    {
      var uri = info.ToUri();

      uri.Should().BeOfType<Uri>();
      uri.ToString().ToPath().Should().Be(info.FullName);
      uri.IsAbsoluteUri.Should().BeTrue();
      uri.OriginalString.Should().Be(info.FullName);
      uri.AbsolutePath.ToPath().Should().Be(info.FullName);
      uri.AbsoluteUri.ToPath().Should().Be(info.FullName);
      uri.Authority.Should().BeEmpty();
      uri.Fragment.Should().BeEmpty();
      uri.Host.Should().BeEmpty();
      uri.IsDefaultPort.Should().BeTrue();
      uri.IsFile.Should().BeTrue();
      uri.IsLoopback.Should().BeTrue();
      uri.IsUnc.Should().BeFalse();
      uri.LocalPath.Should().Be(info.FullName);
      uri.PathAndQuery.ToPath().Should().Be(info.FullName);
      uri.Port.Should().Be(-1);
      uri.Query.Should().BeEmpty();
      uri.Scheme.Should().Be(Uri.UriSchemeFile);
      uri.UserEscaped.Should().BeFalse();
      uri.UserInfo.Should().BeEmpty();
    }
  }
}