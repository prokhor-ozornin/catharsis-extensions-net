using System.Net.Mail;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for networking I/O types.</para>
/// </summary>
/// <seealso cref="SmtpClient"/>
public static class SmtpClientExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="smtp"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static SmtpClient WithTimeout(this SmtpClient smtp, TimeSpan timeout)
  {
    if (smtp is null) throw new ArgumentNullException(nameof(smtp));

    smtp.Timeout = (int) timeout.TotalMilliseconds;

    return smtp;
  }
}