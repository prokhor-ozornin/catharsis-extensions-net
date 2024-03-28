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
  ///   <para>Performs testing of <see cref="SmtpClientExtensions.WithTimeout(SmtpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    AssertionExtensions.Should(() => ((SmtpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("smtp");

    using var smtp = new SmtpClient();

    AssertionExtensions.Should(() => smtp.WithTimeout(TimeSpan.MinValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");
    AssertionExtensions.Should(() => smtp.WithTimeout(TimeSpan.MaxValue)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("value");

    var timeout = smtp.Timeout;
    timeout.Should().Be(100000);

    smtp.WithTimeout(null).Should().BeOfType<SmtpClient>().And.BeSameAs(smtp);
    smtp.Timeout.Should().Be(timeout);

    var timespan = TimeSpan.Zero;
    smtp.WithTimeout(timespan).Should().BeOfType<SmtpClient>().And.BeSameAs(smtp);
    smtp.Timeout.Should().Be((int) timespan.TotalMilliseconds);

    throw new NotImplementedException();

    return;

    static void Validate(SmtpClient client, TimeSpan? timeout = null)
    {
      using (client)
      {

      }
    }
  }
}