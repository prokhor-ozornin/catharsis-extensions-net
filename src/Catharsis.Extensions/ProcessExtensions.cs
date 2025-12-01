using System.Diagnostics;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for process/threads types.</para>
/// </summary>
/// <seealso cref="Process"/>
public static class ProcessExtensions
{
  /// <param name="process"></param>
  extension(Process process)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    public string ErrorText => process.ToErrorText();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public byte[] Bytes => process.ToBytes().ToArray();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string Text => process.ToText();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    public Process Run(TimeSpan? timeout = null)
    {
      if (process is null) throw new ArgumentNullException(nameof(process));

      process.Start();
      process.Finish(timeout);

      return process;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    public Process Restart(TimeSpan? timeout = null)
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
    /// <param name="timeout"></param>
    /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    /// <seealso cref="FinishAsync(Process, CancellationToken)"/>
    public Process Finish(TimeSpan? timeout = null)
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

    #if NET10_0_OR_GREATER
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Finish(Process, TimeSpan?)"/>
    public async Task<Process> FinishAsync(CancellationToken cancellation = default)
    {
      if (process is null) throw new ArgumentNullException(nameof(process));

      cancellation.ThrowIfCancellationRequested();

      cancellation.Register(process.Kill);

      await process.WaitForExitAsync(cancellation).ConfigureAwait(false);

      return process;
    }
    #endif

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="process"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public Process TryFinallyKill(Action<Process> action)
    {
      if (process is null) throw new ArgumentNullException(nameof(process));
      if (action is null) throw new ArgumentNullException(nameof(action));

      return process.TryFinally(action, x => x.Kill());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="process"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytesAsync(Process, IEnumerable{byte}, CancellationToken)"/>
    public Process WriteBytes(IEnumerable<byte> bytes)
    {
      if (process is null) throw new ArgumentNullException(nameof(process));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      process.StandardInput.BaseStream.WriteBytes(bytes);

      return process;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="process"/> or <paramref name="bytes"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteBytes(Process, IEnumerable{byte})"/>
    public async Task<Process> WriteBytesAsync(IEnumerable<byte> bytes, CancellationToken cancellation = default)
    {
      if (process is null) throw new ArgumentNullException(nameof(process));
      if (bytes is null) throw new ArgumentNullException(nameof(bytes));

      await process.StandardInput.BaseStream.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false);

      return process;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="process"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteTextAsync(Process, string, CancellationToken)"/>
    public Process WriteText(string text)
    {
      if (process is null) throw new ArgumentNullException(nameof(process));
      if (text is null) throw new ArgumentNullException(nameof(text));

      process.StandardInput.WriteText(text);

      return process;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="cancellation"></param>
    /// <returns>Back self-reference to the given <paramref name="process"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="process"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <seealso cref="WriteText(Process, string)"/>
    public async Task<Process> WriteTextAsync(string text, CancellationToken cancellation = default)
    {
      if (process is null) throw new ArgumentNullException(nameof(process));
      if (text is null) throw new ArgumentNullException(nameof(text));

      await process.StandardInput.WriteTextAsync(text, cancellation).ConfigureAwait(false);

      return process;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToErrorTextAsync(Process)"/>
    public string ToErrorText() => process?.StandardError.ToText() ?? throw new ArgumentNullException(nameof(process));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToErrorText(Process)"/>
    public async Task<string> ToErrorTextAsync() => process is not null ? await process.StandardError.ToTextAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(process));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytesAsync(Process)"/>
    public IEnumerable<byte> ToBytes() => process?.StandardOutput.BaseStream.ToBytes() ?? throw new ArgumentNullException(nameof(process));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToBytes(Process)"/>
    public IAsyncEnumerable<byte> ToBytesAsync() => process?.StandardOutput.BaseStream.ToBytesAsync() ?? throw new ArgumentNullException(nameof(process));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToTextAsync(Process)"/>
    public string ToText() => process?.StandardOutput.ToText() ?? throw new ArgumentNullException(nameof(process));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="process"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToText(Process)"/>
    public async Task<string> ToTextAsync() => process is not null ? await process.StandardOutput.ToTextAsync().ConfigureAwait(false) : throw new ArgumentNullException(nameof(process));
  }
}