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
    await process.WaitForExitAsync(cancellation);
    return process;
  }
#endif

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
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static IAsyncEnumerable<byte> Bytes(this Process process, CancellationToken cancellation = default) => process.StandardOutput.BaseStream.Bytes(cancellation);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="bytes"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Process> Bytes(this Process process, IEnumerable<byte> bytes, CancellationToken cancellation = default)
  {
    await process.StandardInput.BaseStream.Bytes(bytes, cancellation);
    return process;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static async Task<string> Text(this Process process) => await process.StandardOutput.Text();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="text"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Process> Text(this Process process, string text, CancellationToken cancellation = default)
  {
    await process.StandardInput.Text(text, cancellation);

    return process;
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
    await process.Bytes(bytes, cancellation);
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
    await process.Text(text, cancellation);
    return text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static Process UseTemporarily(this Process process, Action<Process> action) => process.UseFinally(action, process => process.Kill());

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="destination"></param>
  /// <param name="instance"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  public static async Task<Process> Print(this Process destination, object instance, CancellationToken cancellation = default)
  {
    await destination.StandardInput.Print(instance, cancellation);

    return destination;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="process"></param>
  /// <returns></returns>
  public static async Task<string> Error(this Process process) => await process.StandardError.Text();
}