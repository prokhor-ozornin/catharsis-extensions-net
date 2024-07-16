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
  /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  public static Process Run(this Process process, TimeSpan? timeout = null)
  {
    if (process is null) throw new ArgumentNullException(nameof(process));

    process.Start();
    process.Finish(timeout);

    return process;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="timeout"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
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
  /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  /// <seealso cref="FinishAsync(Process, CancellationToken)"/>
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
  /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="process"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
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
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytesAsync(Process)"/>
  public static IEnumerable<byte> ToBytes(this Process process) => process?.StandardOutput.BaseStream.ToBytes() ?? throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToBytes(Process)"/>
  public static IAsyncEnumerable<byte> ToBytesAsync(this Process process) => process?.StandardOutput.BaseStream.ToBytesAsync() ?? throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToTextAsync(Process)"/>
  public static string ToText(this Process process) => process?.StandardOutput.ToText() ?? throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToText(Process)"/>
  public static async Task<string> ToTextAsync(this Process process) => process is not null ? await process.StandardOutput.ToTextAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToErrorTextAsync(Process)"/>
  public static string ToErrorText(this Process process) => process?.StandardError.ToText() ?? throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  /// <seealso cref="ToErrorText(Process)"/>
  public static async Task<string> ToErrorTextAsync(this Process process) => process is not null ? await process.StandardError.ToTextAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns>Back self-reference to the given <paramref name="destination"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytesAsync(Process, IEnumerable{byte}, CancellationToken)"/>
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
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteBytes(Process, IEnumerable{byte})"/>
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
  /// <returns>Back self-reference to the given <paramref name="destination"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteTextAsync(Process, string, CancellationToken)"/>
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
  /// <returns>Back self-reference to the given <paramref name="destination"/>.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="destination"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
  /// <seealso cref="WriteText(Process, string)"/>
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
  /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Finish(Process, TimeSpan?)"/>
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