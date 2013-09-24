using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Tests set for class <see cref="File"/>.</para>
  /// </summary>
  public sealed class FileTests : EntityUnitTests<File>
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="File.ContentType"/> property.</para>
    /// </summary>
    [Fact]
    public void ContentType_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new File { ContentType = null });
      Assert.Throws<ArgumentException>(() => new File { ContentType = string.Empty });
      Assert.True(new File { ContentType = "contentType" }.ContentType == "contentType");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.Data"/> property.</para>
    /// </summary>
    [Fact]
    public void Data_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new File { Data = null });
      var data = new byte[] { 1 };
      Assert.True(ReferenceEquals(new File { Data = data }.Data, data));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.DateCreated"/> property.</para>
    /// </summary>
    [Fact]
    public void DateCreated_Property()
    {
      Assert.True(new File { DateCreated = DateTime.MinValue }.DateCreated == DateTime.MinValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.LastUpdated"/> property.</para>
    /// </summary>
    [Fact]
    public void LastUpdated_Property()
    {
      Assert.True(new File { LastUpdated = DateTime.MaxValue }.LastUpdated == DateTime.MaxValue);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.Name"/> property.</para>
    /// </summary>
    [Fact]
    public void Name_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new File { Name = null });
      Assert.Throws<ArgumentException>(() => new File { Name = string.Empty });
      Assert.True(new File { Name = "name" }.Name == "name");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.OriginalName"/> property.</para>
    /// </summary>
    [Fact]
    public void OriginalName_Property()
    {
      Assert.Throws<ArgumentNullException>(() => new File { OriginalName = null });
      Assert.Throws<ArgumentException>(() => new File { OriginalName = string.Empty });
      Assert.True(new File { OriginalName = "originalName" }.OriginalName == "originalName");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.Size"/> property.</para>
    /// </summary>
    [Fact]
    public void Size_Property()
    {
      Assert.True(new File { Size = 1 }.Size == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.Tags"/> property.</para>
    /// </summary>
    [Fact]
    public void Tags_Property()
    {
      var file = new File();
      Assert.True(file.Tags.Count == 0);
      file.Tags.Add("tag");
      Assert.True(file.Tags.Count == 1);
      Assert.True(file.Tags.Single() == "tag");
      file.Tags.Add("tag");
      Assert.True(file.Tags.Count == 1);
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    ///   <seealso cref="File()"/>
    ///   <seealso cref="File(IDictionary{string, object})"/>
    ///   <seealso cref="File(string, string, string, byte[])"/>
    /// </summary>
    [Fact]
    public void Constructors()
    {
      var file = new File();
      Assert.True(file.Id == 0);
      Assert.True(file.ContentType == null);
      Assert.True(file.Data == null);
      Assert.True(file.DateCreated <= DateTime.UtcNow);
      Assert.True(file.LastUpdated <= DateTime.UtcNow);
      Assert.True(file.Name == null);
      Assert.True(file.OriginalName == null);
      Assert.True(file.Size == 0);
      Assert.True(file.Tags.Count == 0);

      Assert.Throws<ArgumentNullException>(() => new File(null));
      var data = new byte[] { 1, 2, 3 };
      file = new File(new Dictionary<string, object>()
        .AddNext("Id", 1)
        .AddNext("ContentType", "contentType")
        .AddNext("Data", data)
        .AddNext("Name", "name")
        .AddNext("OriginalName", "originalName")
        .AddNext("Size", 1));
      Assert.True(file.Id == 1);
      Assert.True(file.ContentType == "contentType");
      Assert.True(file.Data == data);
      Assert.True(file.DateCreated <= DateTime.UtcNow);
      Assert.True(file.LastUpdated <= DateTime.UtcNow);
      Assert.True(file.Name == "name");
      Assert.True(file.OriginalName == "originalName");
      Assert.True(file.Size == 1);
      Assert.True(file.Tags.Count == 0);

      Assert.Throws<ArgumentNullException>(() => new File(null, "name", "originalName", Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => new File("contentType", null, "originalName", Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => new File("contentType", "name", null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => new File("contentType", "name", "originalName", null));
      Assert.Throws<ArgumentException>(() => new File(string.Empty, "name", "originalName", Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentException>(() => new File("contentType", string.Empty, "originalName", Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentException>(() => new File("contentType", "name", string.Empty, Enumerable.Empty<byte>().ToArray()));
      file = new File("contentType", "name", "originalName", data);
      Assert.True(file.Id == 0);
      Assert.True(file.ContentType == "contentType");
      Assert.True(file.Data == data);
      Assert.True(file.DateCreated <= DateTime.UtcNow);
      Assert.True(file.LastUpdated <= DateTime.UtcNow);
      Assert.True(file.Name == "name");
      Assert.True(file.OriginalName == "originalName");
      Assert.True(file.Size == data.LongLength);
      Assert.True(file.Tags.Count == 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.True(new File { Name = "name" }.ToString() == "name");
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="File.Equals(File)"/></description></item>
    ///     <item><description><see cref="File.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Name", "Name", "Name_2");
      this.TestEquality("OriginalName", "OriginalName", "OriginalName_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Name", "Name", "Name_2");
      this.TestHashCode("OriginalName", "OriginalName", "OriginalName_2");
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="File.CompareTo(File)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      Assert.True(new File { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new File { DateCreated = new DateTime(2000, 1, 1) }) == 0);
      Assert.True(new File { DateCreated = new DateTime(2000, 1, 1) }.CompareTo(new File { DateCreated = new DateTime(2000, 1, 2) }) < 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="File.Xml(XElement)"/></description></item>
    ///     <item><description><see cref="File.Xml()"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Xml_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => File.Xml(null));

      var xml = new XElement("File",
        new XElement("Id", 1),
        new XElement("ContentType", "contentType"),
        new XElement("Data", Guid.Empty.ToByteArray().EncodeBase64()),
        new XElement("DateCreated", DateTime.MinValue.ToRfc1123()),
        new XElement("LastUpdated", DateTime.MaxValue.ToRfc1123()),
        new XElement("Name", "name"),
        new XElement("OriginalName", "originalName"),
        new XElement("Size", Guid.Empty.ToByteArray().Length));
      var file = File.Xml(xml);
      Assert.True(file.Id == 1);
      Assert.True(file.ContentType == "contentType");
      Assert.True(file.Data.SequenceEqual(Guid.Empty.ToByteArray()));
      Assert.True(file.DateCreated.ToRfc1123() == DateTime.MinValue.ToRfc1123());
      Assert.True(file.LastUpdated.ToRfc1123() == DateTime.MaxValue.ToRfc1123());
      Assert.True(file.Name == "name");
      Assert.True(file.OriginalName == "originalName");
      Assert.True(file.Size == Guid.Empty.ToByteArray().Length);
      Assert.True(file.Tags.Count == 0);
      Assert.True(new File("contentType", "name", "originalName", Guid.Empty.ToByteArray()) { Id = 1, DateCreated = DateTime.MinValue, LastUpdated = DateTime.MaxValue }.Xml().ToString() == xml.ToString());
      Assert.True(File.Xml(file.Xml()).Equals(file));
    }
  }
}