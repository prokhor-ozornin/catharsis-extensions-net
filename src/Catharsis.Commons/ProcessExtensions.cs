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
  public static async Task<Process> Finish(this Process process, CancellationToken cancellation = default)
  {
    cancellation.Register(process.Kill);
    await process.WaitForExitAsync(cancellation).ConfigureAwait(false);
    return process;
  }
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<T> Print<T>(this T instance, Process destination, CancellationToken cancellation = default) => await instance.Print(destination.StandardInput, cancellation).ConfigureAwait(false);

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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> ToBytes(this Process process, CancellationToken cancellation = default) => process.StandardOutput.BaseStream.ToBytes(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static async Task<string> ToText(this Process process) => await process.StandardOutput.ToText().ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static async Task<string> ToErrorText(this Process process) => await process.StandardError.ToText().ConfigureAwait(false);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Process> WriteBytes(this Process destination, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    await destination.StandardInput.BaseStream.WriteBytes(bytes, cancellation).ConfigureAwait(false);
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Process> WriteText(this Process destination, string text, CancellationToken cancellation = default)
  {
    await destination.StandardInput.WriteText(text, cancellation).ConfigureAwait(false);
    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="bytes"></param>
  /// <param name="process"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<IEnumerable<byte>> WriteTo(this IEnumerable<byte> bytes, Process process, CancellationToken cancellation = default)
  {
    await process.WriteBytes(bytes, cancellation).ConfigureAwait(false);
    return bytes;
  }

 
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  /// <param name="process"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<string> WriteTo(this string text, Process process, CancellationToken cancellation = default)
  {
    await process.WriteText(text, cancellation).ConfigureAwait(false);
    return text;
  }
}