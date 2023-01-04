using System.Diagnostics;

namespace Catharsis.Commons;

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
  public static Process Finish(this Process process, TimeSpan? timeout = null)
  {
    if (process is null) throw new ArgumentNullException(nameof(process));

    if (timeout != null)
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

#if NET6_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Process> FinishAsync(this Process process, CancellationToken cancellation = default)
  {
    if (process is null) throw new ArgumentNullException(nameof(process));

    cancellation.Register(process.Kill);

    await process.WaitForExitAsync(cancellation).ConfigureAwait(false);

    return process;
  }
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="instance"></param>
  /// <param name="destination"></param>
  /// <returns></returns>
  public static T Print<T>(this T instance, Process destination) => instance.Print(destination.StandardInput);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> PrintAsync<T>(this T instance, Process destination, CancellationToken cancellation = default) => await instance.PrintAsync(destination.StandardInput, cancellation).ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static Process TryFinallyKill(this Process process, Action<Process> action) => process.TryFinally(action, process => process.Kill());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static IEnumerable<byte> ToBytes(this Process process) => process is not null ? process.StandardOutput.BaseStream.ToBytes() : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static string ToText(this Process process) => process is not null ? process.StandardOutput.ToText() : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static string ToErrorText(this Process process) => process is not null ? process.StandardError.ToText() : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToBytesAsync(this Process process, CancellationToken cancellation = default) => process is not null ? process.StandardOutput.BaseStream.ToBytesAsync(cancellation) : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static async Task<string> ToTextAsync(this Process process) => process is not null ? await process.StandardOutput.ToTextAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static async Task<string> ToErrorTextAsync(this Process process) => process is not null ? await process.StandardError.ToTextAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(process));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <returns></returns>
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
  /// <param name="text"></param>
  /// <returns></returns>
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
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Process> WriteTextAsync(this Process destination, string text, CancellationToken cancellation = default)
  {
    if (destination is null) throw new ArgumentNullException(nameof(destination));
    if (text is null) throw new ArgumentNullException(nameof(text));

    await destination.StandardInput.WriteTextAsync(text, cancellation).ConfigureAwait(false);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="process"></param>
  /// <returns></returns>
  public static IEnumerable<byte> WriteTo(this IEnumerable<byte> bytes, Process process)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (process is null) throw new ArgumentNullException(nameof(process));

    process.WriteBytes(bytes);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="process"></param>
  /// <returns></returns>
  public static string WriteTo(this string text, Process process)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (process is null) throw new ArgumentNullException(nameof(process));

    process.WriteText(text);

    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="process"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteToAsync(this IEnumerable<byte> bytes, Process process, CancellationToken cancellation = default)
  {
    if (bytes is null) throw new ArgumentNullException(nameof(bytes));
    if (process is null) throw new ArgumentNullException(nameof(process));

    await process.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

    return bytes;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="process"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteToAsync(this string text, Process process, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (process is null) throw new ArgumentNullException(nameof(process));

    await process.WriteTextAsync(text, cancellation).ConfigureAwait(false);

    return text;
  }
}