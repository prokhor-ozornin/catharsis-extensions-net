using System.Diagnostics;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for process/threads types.</para>
/// </summary>
/// <seealso cref="Process"/>
public static class ProcessExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Process Restart(this Process process, TimeSpan? timeout = null)
  {
    if (process is null) throw new ArgumentNullException(nameof(process));

    process.Finish(timeout);

    var restarted = new Process();
    restarted.StartInfo = process.StartInfo;
    restarted.Start();

    return restarted;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Process Finish(this Process process, TimeSpan? timeout = null)
  {
    if (process is null) throw new ArgumentNullException(nameof(process));

    if (timeout is not null)
    {
      if (!process.WaitForExit((int) timeout.Value.TotalMilliseconds))
      {
        process.Kill();
      }
    }
    else
    {
      process.WaitForExit();
    }

    return process;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Process TryFinallyKill(this Process process, Action<Process> action)
  {
    if (process is null) throw new ArgumentNullException(nameof(process));
    if (action is null) throw new ArgumentNullException(nameof(action));

    return process.TryFinally(action, x => x.Kill());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IEnumerable<byte> ToBytes(this Process process) => process?.StandardOutput.BaseStream.ToBytes() ?? throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static IAsyncEnumerable<byte> ToBytesAsync(this Process process) => process?.StandardOutput.BaseStream.ToBytesAsync() ?? throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToText(this Process process) => process?.StandardOutput.ToText() ?? throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<string> ToTextAsync(this Process process) => process is not null ? await process.StandardOutput.ToTextAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string ToErrorText(this Process process) => process?.StandardError.ToText() ?? throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<string> ToErrorTextAsync(this Process process) => process is not null ? await process.StandardError.ToTextAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Process WriteBytes(this Process destination, IEnumerable<byte> bytes)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    destination.StandardInput.BaseStream.WriteBytes(bytes);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Process> WriteBytesAsync(this Process destination, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));

    await destination.StandardInput.BaseStream.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static Process WriteText(this Process destination, string text)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    destination.StandardInput.WriteText(text);
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Process> WriteTextAsync(this Process destination, string text, CancellationToken cancellation = default)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    await destination.StandardInput.WriteTextAsync(text, cancellation).ConfigureAwait(false);

    return destination;
  }

  #if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static async Task<Process> FinishAsync(this Process process, CancellationToken cancellation = default)
  {
    if (process is null) throw new ArgumentNullException(nameof(process));

    cancellation.ThrowIfCancellationRequested();

    cancellation.Register(process.Kill);

    await process.WaitForExitAsync(cancellation).ConfigureAwait(false);

    return process;
  }
  #endif
}