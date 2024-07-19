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
  /// <param name="client"></param>
  /// <param name="timeout"></param>
  /// <returns>Back self-reference to the given <paramref name="client"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="client"/> is <see langword="null"/>.</exception>
  public static SmtpClient WithTimeout(this SmtpClient client, TimeSpan timeout)
  {
    if (client is null) throw new ArgumentNullException(nameof(client));

    client.Timeout = (int) timeout.TotalMilliseconds;

    return client;
  }
}