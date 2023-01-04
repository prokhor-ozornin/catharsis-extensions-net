using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ProcessExtensions"/>.</para>
/// </summary>
public sealed class ProcessExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Restart(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_Restart_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Restart(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Finish(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_Finish_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Finish(null, TimeSpan.Zero)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.FinishAsync(Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_FinishAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.FinishAsync(null, CancellationToken.None)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Print{T}(T, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Print<object>(null, Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => ProcessExtensions.Print(new object(), null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.PrintAsync{T}(T, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_PrintAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.PrintAsync<object>(null, Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => ProcessExtensions.PrintAsync(new object(), null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.TryFinallyKill(Process, Action{Process})"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_TryFinallyKill_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.TryFinallyKill(null, _ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().TryFinallyKill(null)).ThrowExactly<ArgumentNullException>();

    const string command = "cmd.exe";
    var process = Process.Start(command);
    process.TryFinallyKill(_ => { }).Should().NotBeNull().And.BeSameAs(process);
    process.HasExited.Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytes(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToText(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToErrorText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToErrorText(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytesAsync(Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToBytesAsync(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToErrorTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToErrorTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytes(Process, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytes(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteText(Process, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteText(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytesAsync(Process, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTextAsync(Process, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTo(IEnumerable{byte}, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => ProcessExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTo(string, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => ProcessExtensions.WriteTo(string.Empty, null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteToAsync(IEnumerable{byte}, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => ProcessExtensions.WriteToAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteToAsync(string, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => ProcessExtensions.WriteToAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }
}