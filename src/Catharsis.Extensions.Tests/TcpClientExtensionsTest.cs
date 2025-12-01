using AutoFixture;
using System.Net.Sockets;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TcpClientExtensions"/>.</para>
/// </summary>
/// <seealso cref="TcpClientExtensions"/>
public sealed class TcpClientExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.get_IsUnset(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsUnset_Property()
  {
    using (new AssertionScope())
    {
    }

    throw new NotImplementedException();

    return;

    static void Test(bool result, TcpClient client)
    {
      using (client)
      {
        client.IsUnset.Should().Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.get_IsEmpty(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Property()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).IsEmpty).ThrowExactly<ArgumentNullException>().WithParameterName("client");
    }
    throw new NotImplementedException();

    return;

    static void Test(bool result, TcpClient client)
    {
      using (client)
      {
        client.IsEmpty.Should().Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.get_Bytes(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void Bytes_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.get_Text(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void Text_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WithTimeout(TcpClient, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithTimeout_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).WithTimeout(null)).ThrowExactly<ArgumentNullException>().WithParameterName("client");

      using var client = new TcpClient();

      var receiveTimeout = client.ReceiveTimeout;
      var sendTimeout = client.SendTimeout;
      receiveTimeout.Should().Be(client.Client.ReceiveTimeout).And.Be(0);
      sendTimeout.Should().Be(client.Client.SendTimeout).And.Be(0);

      client.WithTimeout(null).Should().BeOfType<TcpClient>().And.BeSameAs(Fixture.Create<TcpClient>());
      client.ReceiveTimeout.Should().Be(client.Client.ReceiveTimeout).And.Be(receiveTimeout);
      client.SendTimeout.Should().Be(client.Client.SendTimeout).And.Be(sendTimeout);

      new[] { TimeSpan.MinValue, TimeSpan.Zero, TimeSpan.MaxValue }.ForEach(timespan =>
      {
        client.WithTimeout(timespan).Should().BeOfType<TcpClient>().And.BeSameAs(Fixture.Create<TcpClient>());
        client.ReceiveTimeout.Should().Be(client.Client.ReceiveTimeout).And.Be((int) timespan.TotalMilliseconds);
        client.SendTimeout.Should().Be(client.Client.SendTimeout).And.Be((int) timespan.TotalMilliseconds);
      });
    }

    return;

    static void Test(TcpClient client, TimeSpan? timeout)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.TryFinallyDisconnect(TcpClient, Action{TcpClient})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyDisconnect_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).TryFinallyDisconnect(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => Fixture.Create<TcpClient>().TryFinallyDisconnect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    throw new NotImplementedException();

    return;

    static void Test(TcpClient client)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TcpClientExtensions.ToEnumerable(TcpClient, bool)"/></description></item>
  ///     <item><description><see cref="TcpClientExtensions.ToEnumerable(TcpClient, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("client");

      static void Test(TcpClient client)
      {
        using (client)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("client");

      static void Test(TcpClient client)
      {
        using (client)
        {

        }
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TcpClientExtensions.ToAsyncEnumerable(TcpClient, bool)"/></description></item>
  ///     <item><description><see cref="TcpClientExtensions.ToAsyncEnumerable(TcpClient, int, bool)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToAsyncEnumerable_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToAsyncEnumerable()).ThrowExactly<ArgumentNullException>().WithParameterName("client");

      static void Test(TcpClient client)
      {
        using (client)
        {

        }
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToAsyncEnumerable(1)).ThrowExactly<ArgumentNullException>().WithParameterName("client");

      static void Test(TcpClient client)
      {
        using (client)
        {

        }
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.ToBytes(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToBytes()).ThrowExactly<ArgumentNullException>().WithParameterName("client");
    }

    throw new NotImplementedException();

    return;

    static void Test(byte[] result, TcpClient client)
    {
      using (client)
      {
        client.ToBytes().Should().BeOfType<IEnumerable<byte>>().And.Equal(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.ToBytesAsync(TcpClient)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToBytesAsync().ToArrayAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("client");
    }

    throw new NotImplementedException();

    return;

    static void Test(byte[] result, TcpClient client)
    {
      using (client)
      {
        client.ToBytesAsync().ToArray().Should().BeOfType<byte[]>().And.Equal(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.ToText(TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToText()).ThrowExactly<ArgumentNullException>().WithParameterName("client");
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, TcpClient client, Encoding encoding = null)
    {
      using (client)
      {
        client.ToText(encoding).Should().BeOfType<string>().And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.ToTextAsync(TcpClient, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).ToTextAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("client");
    }

    throw new NotImplementedException();

    return;

    static void Test(string result, TcpClient client, Encoding encoding = null)
    {
      using (client)
      {
        var task = client.ToTextAsync(encoding);
        task.Should().BeAssignableTo<Task<string>>();
        task.Await().Should().BeOfType<string>().And.Be(result);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WriteBytes(TcpClient, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).WriteBytes([])).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => Fixture.Create<TcpClient>().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Test(TcpClient client, byte[] bytes)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WriteBytesAsync(TcpClient, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).WriteBytesAsync([])).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => Fixture.Create<TcpClient>().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => Fixture.Create<TcpClient>().WriteBytesAsync([])).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(TcpClient client, byte[] bytes)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WriteText(TcpClient, string, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).WriteText(string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("client");
      AssertionExtensions.Should(() => Fixture.Create<TcpClient>().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Test(TcpClient client, string text, Encoding encoding = null)
    {
      using (client)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TcpClientExtensions.WriteTextAsync(TcpClient, string, Encoding, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((TcpClient) null).WriteTextAsync(string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("client").Await();
      AssertionExtensions.Should(() => Fixture.Create<TcpClient>().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
      AssertionExtensions.Should(() => Fixture.Create<TcpClient>().WriteTextAsync(string.Empty, null)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Test(TcpClient client, string text, Encoding encoding = null)
    {
      using (client)
      {

      }
    }
  }
}