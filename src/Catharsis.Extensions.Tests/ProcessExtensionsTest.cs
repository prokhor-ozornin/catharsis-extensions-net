using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ProcessExtensions"/>.</para>
/// </summary>
/// <seealso cref="ProcessExtensions"/>
public sealed class ProcessExtensionsTest : Test
{
  private Process ShellProcess { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public ProcessExtensionsTest()
  {
    ShellProcess = Shell.ToProcess(new ProcessStartInfo { RedirectStandardError = true, RedirectStandardInput = true, RedirectStandardOutput = true });
    ShellProcess.Start();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.get_ErrorText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ErrorText_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.get_Bytes(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Bytes_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.get_Text(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Text_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Run(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Run_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).Run()).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Restart(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Restart_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).Restart()).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Finish(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Finish_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).Finish(TimeSpan.Zero)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.FinishAsync(Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FinishAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).FinishAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
      AssertionExtensions.Should(() => Process.GetCurrentProcess().FinishAsync(CancellationToken.None)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.TryFinallyKill(Process, Action{Process})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyKill_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).TryFinallyKill(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("process");
      
      var process = Shell.ToProcess();
      
      AssertionExtensions.Should(() => process.TryFinallyKill(_ => { })).ThrowExactly<InvalidOperationException>();

      process.Start();
      AssertionExtensions.Should(() => process.TryFinallyKill(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      process.TryFinallyKill(_ => { }).Should().BeOfType<Process>().And.BeSameAs(process);
      process.HasExited.Should().BeTrue();
    }

    return;

    static void Test(Process process)
    {
      using (process)
      {

      }
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytes(Process, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).WriteBytes([])).ThrowExactly<ArgumentNullException>().WithParameterName("process");
      AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Test(Process process, byte[] bytes)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytesAsync(Process, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).WriteBytesAsync([])).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
      AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => ShellProcess.WriteBytesAsync([])).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(Process process, byte[] bytes)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteText(Process, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).WriteText(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
      AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(Process process, string text)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTextAsync(Process, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).WriteTextAsync(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
      AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => ShellProcess.WriteTextAsync(string.Empty)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(Process process, string text)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToErrorText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).ToErrorText()).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToErrorTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).ToErrorTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytes(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test(byte[] result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytesAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).ToBytesAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test(byte[] result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Process) null).ToTextAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public override void Dispose()
  {
    base.Dispose();
    ShellProcess.Kill();
  }
}