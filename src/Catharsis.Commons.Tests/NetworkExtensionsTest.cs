using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons
{
  public sealed class NetworkExtensionsTest
  {
    const string Yandex = "http://yandex.ru";

    [Fact]
    public void ip_address()
    {
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.IPAddress(null, new object()));

      Assert.Null(Convert.To.IPAddress(null));
      Assert.True(ReferenceEquals(Convert.To.IPAddress(IPAddress.Loopback), IPAddress.Loopback));
      Assert.Equal(IPAddress.Loopback, Convert.To.IPAddress(IPAddress.Loopback.ToString()));
      Assert.Null(Convert.To.IPAddress(new object()));
    }

    [Fact]
    public void bytes()
    {
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Bytes(null));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Bytes(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Bytes(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Bytes(null, new object()));

      var file = Assembly.GetExecutingAssembly().Location;
      Assert.Equal(new FileInfo(file).Length, new Uri(file).Bytes().Length);

      Assert.True(new Uri(Yandex).Bytes().Length > 0);
    }

    [Fact]
    public void stream()
    {
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Stream(null));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Stream(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Stream(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Stream(null, new object()));

      var file = Assembly.GetExecutingAssembly().Location;
      var stream = new Uri(file).Stream();
      using (stream)
      {
        Assert.True(stream is FileStream);
        Assert.Equal(new FileInfo(file).Length, stream.Length);
        Assert.True(stream.CanRead);
        Assert.False(stream.CanWrite);
        Assert.True(stream.CanSeek);
        Assert.False(stream.CanTimeout);
      }
      Assert.False(stream.CanRead);

      stream = new Uri(Yandex).Stream();
      using (stream)
      {
        Assert.True(stream.CanRead);
        Assert.False(stream.CanWrite);
        Assert.False(stream.CanSeek);
        Assert.True(stream.CanTimeout);
      }
      Assert.False(stream.CanRead);
    }

    [Fact]
    public void text()
    {
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Text(null));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Text(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Text(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Text(null, new object()));

      var file = Assembly.GetExecutingAssembly().Location;
      var text = new Uri(file).Text();
      Assert.True(text.Length > 0);

      text = new Uri(Yandex).Text();
      Assert.True(text.Length > 0);
    }

    [Fact]
    public void query()
    {
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Query(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Query(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => NetworkExtensions.Query(null, new object()));

      var uri = new Uri("http://yandex.ru");
      Assert.Equal(0, uri.Query.Length);

      Assert.False(ReferenceEquals(uri, uri.Query(new Dictionary<string, object>())));
      Assert.Equal(uri, uri.Query(new Dictionary<string, object>()));

      Assert.False(ReferenceEquals(uri, uri.Query(new Dictionary<string, string>())));
      Assert.Equal(uri, uri.Query(new Dictionary<string, string>()));

      Assert.False(ReferenceEquals(uri, uri.Query(new { })));
      Assert.Equal(uri, uri.Query(new { }));

      Assert.Equal("http://yandex.ru/?first=1", uri.Query(new Dictionary<string, object> { { "first", 1 } }).ToString());
      Assert.Equal("?first=1&second%23=second%3F", uri.Query(new Dictionary<string, object> { { "first", 1 }, { "second#", "second?" } }).Query);

      Assert.Equal("http://yandex.ru/?first=1", uri.Query(new Dictionary<string, string> { { "first", "1" } }).ToString());
      Assert.Equal("?first=1&second%23=second%3F", uri.Query(new Dictionary<string, string> { { "first", "1" }, { "second#", "second?" } }).Query);

      Assert.Equal("http://yandex.ru/?first=1", uri.Query(new { first = 1 }).ToString());
      Assert.Equal("?first=1&second=second%3F", uri.Query(new Dictionary<string, string> { { "first", "1" }, { "second", "second?" } }).Query);


      uri = new Uri("http://yandex.ru?first=1");
      Assert.Equal(8, uri.Query.Length);

      Assert.Equal("http://yandex.ru/?first=1", uri.Query(new Dictionary<string, object>()).ToString());
      Assert.Equal("?first=1&second%23=second%3F", uri.Query(new Dictionary<string, object> { { "second#", "second?" } }).Query);

      Assert.Equal("http://yandex.ru/?first=1", uri.Query(new Dictionary<string, string>()).ToString());
      Assert.Equal("?first=1&second%23=second%3F", uri.Query(new Dictionary<string, string> { { "second#", "second?" } }).Query);
    }

    [Fact]
    public void host()
    {
      var host = new Uri("http://yandex.ru").Host();
      Assert.True(host.AddressList.Any());
      host.AddressList.SequenceEqual(Dns.GetHostEntry("yandex.ru").AddressList);
      Assert.False(host.Aliases.Any());
      Assert.Equal("yandex.ru", host.HostName);
    }
  }
}