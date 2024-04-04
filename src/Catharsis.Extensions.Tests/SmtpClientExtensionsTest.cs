using System.Net.Mail;
using Catharsis.Commons;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="SmtpClientExtensions"/>.</para>
/// </summary>
public sealed class SmtpClientExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="SmtpClientExtensions.WithTimeout(SmtpClient, TimeSpan)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    using var client = new SmtpClient();

    Validate(client, TimeSpan.Zero);

    return;

    static void Validate(SmtpClient client, TimeSpan timeout)
    {
      using (client)
      {
        AssertionExtensions.Should(() => client.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
        AssertionExtensions.Should(() => client.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");

        client.WithTimeout(timeout).Should().BeOfType<SmtpClient>().And.BeSameAs(client);
        client.Timeout.Should().Be((int) timeout.TotalMilliseconds);
      }
    }
  }
}