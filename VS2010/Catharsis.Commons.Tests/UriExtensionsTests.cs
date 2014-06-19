using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="UriExtensions"/>.</para>
  /// </summary>
  public sealed class UriExtensionsTests
  {
    const string Yandex = "http://yandex.ru";

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="UriExtensions.Bytes(Uri)"/></description></item>
    ///     <item><description><see cref="UriExtensions.Bytes(Uri, IDictionary{string, object})"/></description></item>
    ///     <item><description><see cref="UriExtensions.Bytes(Uri, IDictionary{string, string})"/></description></item>
    ///     <item><description><see cref="UriExtensions.Bytes(Uri, object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Bytes_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Bytes(null));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Bytes(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Bytes(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Bytes(null, new object()));

      var file = Assembly.GetExecutingAssembly().Location;
      Assert.Equal(new FileInfo(file).Length, new Uri(file).Bytes().Length);
      
      Assert.True(new Uri(Yandex).Bytes().Length > 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="UriExtensions.Stream(Uri)"/></description></item>
    ///     <item><description><see cref="UriExtensions.Stream(Uri, IDictionary{string, object})"/></description></item>
    ///     <item><description><see cref="UriExtensions.Stream(Uri, IDictionary{string, string})"/></description></item>
    ///     <item><description><see cref="UriExtensions.Stream(Uri, object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Stream_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Stream(null));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Stream(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Stream(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Stream(null, new object()));

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

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="UriExtensions.Text(Uri)"/></description></item>
    ///     <item><description><see cref="UriExtensions.Text(Uri, IDictionary{string, object})"/></description></item>
    ///     <item><description><see cref="UriExtensions.Text(Uri, IDictionary{string, string})"/></description></item>
    ///     <item><description><see cref="UriExtensions.Text(Uri, object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Text_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Text(null));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Text(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Text(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Text(null, new object()));

      var file = Assembly.GetExecutingAssembly().Location;
      var text = new Uri(file).Text();
      Assert.True(text.Length > 0);

      text = new Uri(Yandex).Text();
      Assert.True(text.Length > 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="UriExtensions.Query(Uri, IDictionary{string, object})"/></description></item>
    ///     <item><description><see cref="UriExtensions.Query(Uri, IDictionary{string, string})"/></description></item>
    ///     <item><description><see cref="UriExtensions.Query(Uri, object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Query_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Query(null, new Dictionary<string, object>()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Query(null, new Dictionary<string, string>()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Query(null, new object()));

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
  }
}